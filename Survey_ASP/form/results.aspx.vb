Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Web.UI.DataVisualization.Charting

Public Class results
    Inherits System.Web.UI.Page
    Dim TotalUserSubmit As Integer = 0
    ' Public QsubjectId As Integer
    Dim str As String = ""
    Public listTitleDesc As New List(Of String)
    Dim dtExcel As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If

        If Not IsPostBack() Then
            Session("QsubjectId") = Request.QueryString("subjectId")
            getTitleDesc()
        Else
            DataList1.DataBind()
        End If

    End Sub

    Private Shared Function getContent(query As String) As DataTable
        Dim dt As New DataTable()
        Dim cmd As New SqlCommand(query)
        Dim constr As [String] = My.Settings.ConnStringDatabaseSurvey
        Dim con As New SqlConnection(constr)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        sda.SelectCommand = cmd
        sda.Fill(dt)
        Return dt
    End Function

    Private Sub getTitleDesc()

        'Get the survey title and description
        Dim sqlTitleDesc = "select subjectName, subjectDetail from surveyMaster where subjectId = " + Session("QsubjectId").ToString()
        Dim dtTitleDesc As DataTable = getContent(sqlTitleDesc)
        Dim xTitle As String = ""
        Dim xDesc As String = ""

        If dtTitleDesc.Rows.Count > 0 Then
            For Each r In dtTitleDesc.Rows
                xTitle = r("subjectName")
                xDesc = r("subjectDetail")
                listTitleDesc.Add(xTitle)
                listTitleDesc.Add(xDesc)
            Next
        End If

        Dim t As String = "<p><h1 style='text-align: center' id='txtTitle' name='txtTitle'>"
        t += listTitleDesc.Item(0) + "</h1></p>"
        Dim d As String = "<p  id='txtDesc' name='txtDesc'>Survey Description: " + listTitleDesc.Item(1) + "</p>"

        title.Text = t
        description.Text = d
    End Sub

    Private Shared Function GetData(ByVal query As String) As DataTable
        Dim constr As String = My.Settings.ConnStringDatabaseSurvey
        Using con As SqlConnection = New SqlConnection(constr)
            Using sda As SqlDataAdapter = New SqlDataAdapter(query, con)
                Dim dt As DataTable = New DataTable()
                sda.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

    Private Sub DataList1_ItemDataBound(sender As Object, e As DataListItemEventArgs) Handles DataList1.ItemDataBound
        Try
            Dim ChartPie As Chart = e.Item.FindControl("ChartPie")
            Dim Chartx1 As Chart = e.Item.FindControl("Chart21")
            Dim xsectionIdLabel As Label = e.Item.FindControl("sectionIdLabel")
            Dim xquestionIdLabel As Label = e.Item.FindControl("questionIdLabel")
            ChartPie.Visible = False
            Chartx1.Visible = False

            'Tables for short answers
            Dim queryShort As String = "Select distinct a.sectionName, c.questionName "
            queryShort += "From surveyMaster b "
            queryShort += "INNER Join surveySection a ON a.subjectId = b.subjectId "
            queryShort += "INNER Join surveyQuestion c ON a.sectionId = c.sectionId And c.questionType = 'shortanswer' "
            queryShort += "INNER Join surveyAnswer f ON c.questionId = f.questionId "
            queryShort += "INNER Join surveyUserAnswer d ON d.answerId = f.answerId "
            queryShort += "WHERE b.subjectId = " + Session("QsubjectId").ToString() + " and a.sectionId = " + xsectionIdLabel.Text
            queryShort += " AND c.questionId = " + xquestionIdLabel.Text

            Dim dtShort As DataTable = GetData(queryShort)

            If dtShort.Rows.Count > 0 Then
                Dim qTitle As String
                qTitle = "<p style='text-decoration: underline; font-size: 25px; font-weight: bold;'>" + dtShort.Rows(0)("sectionName").ToString() + "</p><div style='font-size: 20px;'>" + dtShort.Rows(0)("questionName").ToString()
                Dim shortLabel As Label = e.Item.FindControl("shortAnsLabel")
                shortLabel.Text = qTitle
            End If


            'Bar charts for multiple choice grid
            Dim queryBarChart As String = "Select b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName, "
            queryBarChart = queryBarChart + " count(d.answerId) As cnt "
            queryBarChart = queryBarChart + " From surveyMaster b  "
            queryBarChart = queryBarChart + " inner Join surveySection a On a.subjectId = b.subjectId  "
            queryBarChart = queryBarChart + " inner Join surveyQuestion c On a.sectionId = c.sectionId  "
            queryBarChart = queryBarChart + " inner Join surveyAnswer f On c.questionId = f.questionId  "
            queryBarChart = queryBarChart + " Left Join surveyUserAnswer d On d.answerId = f.answerId "
            queryBarChart = queryBarChart + " where questionType = 'grid' and b.subjectId = " + Session("QsubjectId").ToString() + " and a.sectionId = " + xsectionIdLabel.Text
            'queryBarChart = queryBarChart + " and c.questionId = " + xquestionIdLabel.Text
            queryBarChart = queryBarChart + " Group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName "
            queryBarChart = queryBarChart + " order by a.sectionId, c.questionId "

            Dim dtBarChart As DataTable = GetData(queryBarChart)

            If dtBarChart.Rows.Count > 0 Then
                Dim qTitle As String
                qTitle = "<p style='text-decoration: underline; font-size: 25px; font-weight: bold;'>" + dtBarChart.Rows(0)("sectionName").ToString() + "</p><div style='font-size: 20px;'>" + dtBarChart.Rows(0)("questionName").ToString() + "</div>"
                Dim multiLabel As Label = e.Item.FindControl("multiLabel")
                multiLabel.Text = qTitle

                'Get the DISTINCT answerName.
                Dim answerNames As List(Of String) = (From p In dtBarChart.AsEnumerable()
                                                      Select p.Field(Of String)("answerName")).Distinct().ToList()

                'Loop through the answerName.
                For Each answerName As String In answerNames

                    'Get the questionName for each answerName.
                    Dim qName As String() = (From p In dtBarChart.AsEnumerable()
                                             Where p.Field(Of String)("answerName") = answerName
                                             Order By p.Field(Of String)("questionName")
                                             Select p.Field(Of String)("questionName")).ToArray()



                    'Get the number of people who answered for each answerName.
                    Dim num As Integer() = (From p In dtBarChart.AsEnumerable()
                                            Where p.Field(Of String)("answerName") = answerName
                                            Order By p.Field(Of String)("questionName")
                                            Select p.Field(Of Integer)("cnt")).ToArray()

                    'Add Series to the Chart.
                    Chartx1.Series.Add(New Series(answerName))
                    Chartx1.Titles("Items").Font = New System.Drawing.Font("Helvetica Neue", 20, System.Drawing.FontStyle.Bold)
                    Chartx1.Titles("Items").Text = "Section: " + dtBarChart.Rows(0)("sectionName").ToString() + "\n" + "Question: " + dtBarChart.Rows(0)("subjectName").ToString()
                    Chartx1.Series(answerName).IsValueShownAsLabel = True
                    Chartx1.Series(answerName).ChartType = SeriesChartType.Bar
                    Chartx1.Series(answerName).Points.DataBindXY(qName, num)
                Next

                Chartx1.Legends(0).Enabled = True
                Chartx1.Visible = True
            End If

            'Pie Chart for radio buttons

            Dim queryPie As String = "Select a.sectionName, c.questionType, c.questionName "
            queryPie = queryPie + " , f.answerName,  count(d.answerId) As cnt   "
            queryPie = queryPie + " From surveyMaster b    "
            queryPie = queryPie + " inner Join surveySection a On a.subjectId = b.subjectId    "
            queryPie = queryPie + " inner Join surveyQuestion c On a.sectionId = c.sectionId    "
            queryPie = queryPie + " inner Join surveyAnswer f On c.questionId = f.questionId    "
            queryPie = queryPie + " Left Join surveyUserAnswer d On d.answerId = f.answerId   "
            queryPie = queryPie + " where questionType = 'radio' and b.subjectId = " + Session("QsubjectId").ToString() + " and a.sectionId = " + xsectionIdLabel.Text
            queryPie = queryPie + " and c.questionId = " + xquestionIdLabel.Text
            queryPie = queryPie + " Group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName  "
            Dim dtpie As DataTable = GetData(queryPie)
            If dtpie.Rows.Count > 0 Then
                Dim qTitle As String
                qTitle = "<p style='text-decoration: underline; font-size: 25px; font-weight: bold;'>" + dtpie.Rows(0)("sectionName").ToString() + "</p><div style='font-size: 20px;'>" + dtpie.Rows(0)("questionName").ToString()
                Dim radioLabel As Label = e.Item.FindControl("radioLabel")
                radioLabel.Text = qTitle

                'ChartPie.Titles("Title1").Font = New System.Drawing.Font("Helvetica Neue", 20, System.Drawing.FontStyle.Bold)
                'ChartPie.Titles("Title1").Text = "Section: " + dtpie.Rows(0)("sectionName").ToString() + "\n" + "Question: " + dtpie.Rows(0)("questionName").ToString()
                ChartPie.Visible = True
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('There was an issue getting results from the database')", True)
        End Try

    End Sub
    Private Sub getExcel()

        'Get all the sections in the survey
        Dim sqlSection = "Select a.subjectId, a.subjectName, b.sectionId, b.sectionName from surveyMaster a"
        sqlSection = sqlSection + " inner Join surveySection b on a.subjectId = b.subjectId"
        sqlSection = sqlSection + " where a.subjectId = " + Session("QsubjectId").ToString()
        Dim dtSection As DataTable = getContent(sqlSection)
        Dim dtQuestion As DataTable
        Dim dtAnswer As DataTable

        dtExcel.Columns.Add("subjectName", GetType(String))
        dtExcel.Columns.Add("sectionId", GetType(Integer))
        dtExcel.Columns.Add("sectionName", GetType(String))
        dtExcel.Columns.Add("questionId", GetType(Integer))
        dtExcel.Columns.Add("questionName", GetType(String))
        dtExcel.Columns.Add("questionType", GetType(String))
        dtExcel.Columns.Add("answerId", GetType(Integer))
        dtExcel.Columns.Add("answerName", GetType(String))
        dtExcel.Columns.Add("cnt", GetType(Integer))
        dtExcel.Columns.Add("surveyType", GetType(String))

        If (dtSection.Rows.Count > 0) Then
            For Each s In dtSection.Rows
                'Call getData(QsubjectId, r("sectionId"))
                Dim sec = s("sectionId")

                'Get all the questions in that section of the survey
                Dim sqlQuestion = "Select a.sectionId, a.sectionName, b.questionId, b.questionName from surveySection a"
                sqlQuestion = sqlQuestion + " inner Join surveyQuestion b on a.sectionId = b.sectionId"
                sqlQuestion = sqlQuestion + " where a.sectionId = " + sec.ToString()
                dtQuestion = getContent(sqlQuestion)

                For Each q In dtQuestion.Rows
                    Dim ques = q("questionId")

                    Dim sqlCount = "Select b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName, count(d.answerId) as cnt"
                    sqlCount = sqlCount + " , '' as surveyType "
                    sqlCount = sqlCount + " from surveyMaster b"
                    sqlCount = sqlCount + " inner join surveySection a On a.subjectId = b.subjectId"
                    sqlCount = sqlCount + " inner join surveyQuestion c On a.sectionId = c.sectionId"
                    sqlCount = sqlCount + " inner join surveyAnswer f On c.questionId = f.questionId"
                    sqlCount = sqlCount + " left join surveyUserAnswer d On d.answerId = f.answerId"
                    sqlCount = sqlCount + " where b.subjectId = " + Session("QsubjectId").ToString()
                    sqlCount = sqlCount + " And c.questionId = " + ques.ToString()
                    sqlCount = sqlCount + " group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName"
                    sqlCount = sqlCount + " order by answerId"

                    dtAnswer = getContent(sqlCount)

                    If dtAnswer.Rows.Count > 0 Then
                        For Each a In dtAnswer.Rows
                            Dim nr As DataRow = dtExcel.NewRow
                            nr("subjectName") = a("subjectName")
                            nr("sectionId") = a("sectionId")
                            nr("sectionName") = a("sectionName")
                            nr("questionId") = a("questionId")
                            nr("questionName") = a("questionName")
                            nr("questionType") = a("questionType")
                            nr("answerId") = a("answerId")
                            nr("answerName") = a("answerName")
                            nr("cnt") = a("cnt")
                            nr("surveyType") = a("surveyType")
                            dtExcel.Rows.Add(nr)

                        Next

                    Else
                        Dim nr2 As DataRow = dtExcel.NewRow

                        nr2("subjectName") = s("subjectName")
                        nr2("sectionId") = q("sectionId")
                        nr2("sectionName") = q("sectionName")
                        nr2("questionId") = q("questionId")
                        nr2("questionName") = q("questionName")
                        nr2("questionType") = "grid"
                        nr2("answerId") = 0
                        nr2("answerName") = ""
                        nr2("cnt") = 0
                        nr2("surveyType") = "Section"
                        dtExcel.Rows.Add(nr2)
                    End If

                Next
            Next
        End If
    End Sub

    Private Sub ExportToExcel(GridView)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            GridView.AllowPaging = False
            'Me.BindGrid()

            GridView.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridView.HeaderRow.Cells
                cell.BackColor = GridView.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridView.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminAllSurveys.aspx")
    End Sub

    Protected Sub excelImg_Click(sender As Object, e As ImageClickEventArgs) Handles excelImg.Click
        getExcel()

        If dtExcel.Rows.Count > 0 Then
            Dim GridView As New GridView
            GridView.DataSource = dtExcel
            GridView.DataBind()

            Call ExportToExcel(GridView)
            Dim warningMsg = "Export(Excel file) Success."
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & warningMsg & "Save')", True)
            Exit Sub
        End If

    End Sub

End Class