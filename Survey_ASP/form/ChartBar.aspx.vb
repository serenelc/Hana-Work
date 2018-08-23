Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports System.Data

Public Class ChartBar
    Inherits System.Web.UI.Page
    Dim TotalUserSubmit As Integer = 0
    Dim QsubjectId As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Call xxx()
        'Exit Sub

        QsubjectId = Request.QueryString("subjectId")
        QsubjectId = 1

        'Count user submit and question
        Dim xSql = " select A.subjectId,A.questionId,B.questionName,B.questionType "
        xSql = xSql + " ,CntUserSubmit = (select count(*) as CntUserSubmit from surveyUserSubmit A where subjectId = " + QsubjectId.ToString() + ") "
        xSql = xSql + " from surveyUserAnswer A "
        xSql = xSql + " inner join surveyQuestion B on A.questionId = B.questionId "
        xSql = xSql + " where subjectId = " + QsubjectId.ToString()
        xSql = xSql + " group by A.subjectId,A.questionId,B.questionName,B.questionType "

        Dim dt As DataTable = GetData(xSql)
        Dim objArrListGrid As ArrayList
        objArrListGrid = New ArrayList
        Dim objStrListGrid As String = ""
        Dim gridTrue As Boolean = False
        If dt.Rows.Count > 0 Then
            For Each r In dt.Rows
                TotalUserSubmit = r("CntUserSubmit")

                If r("QuestionType") = "grid" Then
                    gridTrue = True
                    Dim xx = r("QuestionId")
                    objArrListGrid.Add(xx)
                    objStrListGrid = r("subjectId")

                Else
                    If gridTrue = True Then
                        objStrListGrid = r("subjectId")
                        Call ChartgridShow(objArrListGrid, objStrListGrid)
                    End If

                    gridTrue = False
                    objArrListGrid.Clear()
                End If

                If r("QuestionType") = "radio" Then
                    Call ChartradioShow(r("QuestionId"), r("QuestionName"))
                    'Exit For
                End If

                'If r("QuestionType") = "shortanswer" Then
                '    Call ChartshortanswerShow(r("QuestionId"), r("QuestionName"))
                'End If

            Next
        End If

    End Sub

    Private Shared Function GetData(query As String) As DataTable
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

    Private Sub ChartradioShow(xQuestionId, xQuestionName)

        Dim xSql = " select aa.answerName,isnull(bb.answerSum,0) as answerSum from ( "
        xSql = xSql + " select A.answerId,A.questionId,B.questionName,A.answerName from surveyAnswer A  "
        xSql = xSql + " inner join surveyQuestion B on B.questionId = A.questionId "
        xSql = xSql + " where A.questionId = " + xQuestionId.ToString() + " ) aa "
        xSql = xSql + " left join ( "
        xSql = xSql + " select answerId, count(*) as answerSum   "
        xSql = xSql + " from surveyUserAnswer  "
        xSql = xSql + " where subjectId = " + QsubjectId.ToString() + " and  questionId = " + xQuestionId.ToString()
        xSql = xSql + " group by answerId "
        xSql = xSql + " ) bb on aa.answerId = bb.answerId "
        Dim dt2 As DataTable = GetData(xSql)
        If dt2.Rows.Count > 0 Then
            Dim x As String() = New String(dt2.Rows.Count - 1) {}
            Dim y As Integer() = New Integer(dt2.Rows.Count - 1) {}
            For i As Integer = 0 To dt2.Rows.Count - 1
                x(i) = dt2.Rows(i)(0).ToString()
                y(i) = Convert.ToInt32(dt2.Rows(i)(1))
            Next

            Dim div = New HtmlGenericControl("div")
            div.Attributes.Add("id", "divInferior")
            divImage.Controls.Add(div)
            Dim pieChart = New Chart()
            'pieChart.Series.Clear()
            'pieChart.Titles.Clear()
            Dim T As Title = pieChart.Titles.Add(xQuestionName)
            With T
                .ForeColor = Color.Black
                .BackColor = Color.LightBlue
                .Font = New System.Drawing.Font("Times New Roman", 11.0F, System.Drawing.FontStyle.Bold)
                .BorderColor = Color.Black
            End With

            pieChart.Series.Add("Default")
            pieChart.ChartAreas.Add("ChartArea1")
            pieChart.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            pieChart.Series(0).ChartArea = "ChartArea1"
            pieChart.Series(0).ChartType = SeriesChartType.Pie
            pieChart.Legends.Add("Legend1")
            pieChart.Series(0).Legend = "Legend1"
            pieChart.Series(0).IsValueShownAsLabel = True
            pieChart.Series("Default").Points.DataBindXY(x, y)
            div.Controls.Add(pieChart)
        End If

    End Sub
    Private Sub ChartshortanswerShow(xQuestionId, xQuestionName)

        Session("xSubjectId") = QsubjectId.ToString()
        Session("xQuestionId") = xQuestionId.ToString()
        Dim xSql = " Select B.questionName ,A.answerComment from  surveyUserAnswer A  "
        xSql = xSql + " inner Join surveyQuestion B On A.questionId = B.questionId "
        xSql = xSql + " where A.subjectId = " + Session("xSubjectId")
        xSql = xSql + " And A.questionId = " + Session("xQuestionId")
        Dim dt3 As DataTable = GetData(xSql)
        If dt3.Rows.Count > 0 Then
            'DataList1.DataBind()

            Dim div = New HtmlGenericControl("div")
            div.Attributes.Add("id", "DataList1")
            divImage.Controls.Add(div)
            Dim commnetChart = New DataList()
            commnetChart.DataSource = SqlDataSource3
            commnetChart.DataBind()
            div.Controls.Add(commnetChart)
        End If
    End Sub
    Private Sub ChartgridShow(xobjArrListGrid, xobjStrListGrid)
        Dim strSplit As String = ""
        For i = 0 To xobjArrListGrid.Count - 1
            If strSplit = "" Then
                strSplit = xobjArrListGrid(i)
            Else
                strSplit = strSplit + "," + xobjArrListGrid(i).ToString()
            End If
        Next

        If strSplit <> "" Then

            'Dim xSqlField As String = ""
            'Dim xSqlWhere As String = ""
            'Dim xSqlmaxQuestion As String = ""
            'Dim xSqlmax = " select max(answerName) as maxQuestion from surveyAnswer A where  questionId in (" + strSplit + ") "
            'Dim dtmax As DataTable = GetData(xSqlmax)
            'If dtmax.Rows.Count > 0 Then
            '    xSqlmaxQuestion = CInt(Int(dtmax.Rows(0).Item("maxQuestion")))


            '    For ii As Integer = 1 To xSqlmaxQuestion
            '        If xSqlField = "" Then
            '            xSqlField = "isnull([" + ii.ToString() + "],0) As [" + ii.ToString() + "]"
            '            xSqlWhere = "[" + ii.ToString() + "]"
            '        Else
            '            xSqlField = xSqlField + ",isnull([" + ii.ToString() + "],0) As [" + ii.ToString() + "]"
            '            xSqlWhere = xSqlWhere + ",[" + ii.ToString() + "]"
            '        End If
            '    Next
            'End If

            'Dim xSql = " Select questionName," + xSqlField + " from ( "
            'xSql = xSql + " Select B.answerName,A.questionId,C.questionName,count(*) As cntAnswer from surveyUserAnswer A "
            'xSql = xSql + " Left Join surveyAnswer B  on A.questionId = B.questionId And A.answerId = B.answerId "
            'xSql = xSql + " Left Join surveyQuestion C on C.questionId = A.questionId "
            'xSql = xSql + " where A.subjectId =  " + xobjStrListGrid
            'xSql = xSql + " And A.questionId in ( " + strSplit + ") "
            'xSql = xSql + " Group by A.questionId, B.answerName, C.questionName "
            'xSql = xSql + " ) PivotExample "

            'xSql = xSql + " PIVOT "
            'xSql = xSql + " ( "
            'xSql = xSql + " SUM(cntAnswer) "
            'xSql = xSql + " For answerName IN (" + xSqlWhere + ") "

            'xSql = xSql + " ) A "
            'xSql = xSql + " order by questionId "
            Dim xSql = " Select convert(int,B.answerName) as answerName,A.questionId,C.questionName,count(*) As cntAnswer from surveyUserAnswer A "
            xSql = xSql + " Left Join surveyAnswer B  on A.questionId = B.questionId And A.answerId = B.answerId "
            xSql = xSql + " Left Join surveyQuestion C on C.questionId = A.questionId "
            xSql = xSql + " where A.subjectId =  " + QsubjectId.ToString()
            xSql = xSql + " And A.questionId in ( " + strSplit + ") "
            xSql = xSql + " Group by A.questionId, B.answerName, C.questionName "
            xSql = xSql + "order by answerName,questionId, cntAnswer "

            Dim dt4 As DataTable = GetData(xSql)
            If dt4.Rows.Count > 0 Then

                'Get the DISTINCT Countries.
                Dim countries As List(Of String) = (From p In dt4.AsEnumerable()
                                                    Select p.Field(Of String)("questionName")).Distinct().ToList()

                Dim div2 = New HtmlGenericControl("div")
                div2.Attributes.Add("id", "divInferior")
                divImage.Controls.Add(div2)
                'Dim StackedBarChart2 = New Chart()

                'Remove the Default Series.
                'If Chart1.Series.Count() = 1 Then
                '    Chart1.Series.Remove(Chart1.Series(0))
                'End If

                'Loop through the Countries.
                For Each country As String In countries

                    'Get the Year for each Country.
                    Dim x As Integer() = (From p In dt4.AsEnumerable()
                                          Where p.Field(Of String)("questionName") = country
                                          Order By p.Field(Of Integer)("answerName")
                                          Select p.Field(Of Integer)("answerName")).ToArray()

                    'Get the Total of Orders for each Country.
                    Dim y As Integer() = (From p In dt4.AsEnumerable()
                                          Where p.Field(Of String)("questionName") = country
                                          Order By p.Field(Of Integer)("answerName")
                                          Select p.Field(Of Integer)("cntAnswer")).ToArray()


                    StackedBarChart2.Series.Add(New Series(country))
                    'StackedBarChart.Legends.Add("Legend1")
                    'StackedBarChart.Series(country).Legend = "Legend1"
                    StackedBarChart2.Series(country).IsValueShownAsLabel = True
                    StackedBarChart2.Series(country).ChartType = SeriesChartType.StackedBar
                    StackedBarChart2.Series(country).Points.DataBindXY(x, y)
                Next

                'StackedBarChart.Legends(0).Enabled = True
                div2.Controls.Add(StackedBarChart2)


            End If

        End If
    End Sub


    Private Sub xxx2()
        'chart1.Series(0).ChartType = SeriesChartType.StackedColumn
        ' StackedBarChart.Series(0).IsValueShownAsLabel = True
        'chart1.Series(1).ChartType = SeriesChartType.StackedColumn
        'chart1.Series(2).ChartType = SeriesChartType.StackedColumn
        'chart1.Series(3).ChartType = SeriesChartType.StackedColumn

        Dim div2 = New HtmlGenericControl("div")
        div2.Attributes.Add("id", "divInferior2")
        divImage.Controls.Add(div2)
        Dim chart1 = New Chart()


        ' September Data        
        chart1.Series.Add(New Series())
        chart1.Legends.Add("Legend1")

        chart1.Series(0).Legend = "Legend1"
        ' chart1.Series(0).Name = "Series1"
        'chart1.ChartAreas.Add("ChartArea1")
        'chart1.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
        chart1.Series(0).Name = "1"
        chart1.Series(0).Label = "1"
        chart1.Series(0).Points.Add(New DataPoint(5, 10))
        chart1.Series(0).Points.Add(New DataPoint(7, 15))

        ' October Data
        ' chart1.Series(1).Name = "Series2"
        chart1.Series(1).Name = "2"
        chart1.Series(1).Label = "2"
        chart1.Series(1).Points.Add(New DataPoint(5, 20))
        chart1.Series(1).Points.Add(New DataPoint(7, 16))

        ' April Data
        ' chart1.Series(2).Name = "Series3"
        chart1.Series(2).Name = "3"
        chart1.Series(2).Label = "3"
        chart1.Series(2).Points.Add(New DataPoint(5, 15))
        chart1.Series(2).Points.Add(New DataPoint(7, 18))

        ' chart1.Series(3).Name = "Series4"
        chart1.Series(3).Name = "4"
        chart1.Series(3).Label = "4"
        chart1.Series(3).Points.Add(New DataPoint(5, 15))
        chart1.Series(3).Points.Add(New DataPoint(7, 18))

        div2.Controls.Add(chart1)
    End Sub

    Private Sub xxx()
        'Fetch the Statistical data from database.
        Dim query As String = "SELECT * "
        query += " FROM testData "
        query += " "
        Dim dt As DataTable = GetData(query)

        'Get the DISTINCT Countries.
        Dim countries As List(Of String) = (From p In dt.AsEnumerable()
                                            Select p.Field(Of String)("CountryName")).Distinct().ToList()

        Dim div = New HtmlGenericControl("div")
        div.Attributes.Add("id", "divInferior2")
        divImage.Controls.Add(div)
        ' Dim StackedBarChart = New Chart()

        'Remove the Default Series.
        'If Chart1.Series.Count() = 1 Then
        '    Chart1.Series.Remove(Chart1.Series(0))
        'End If

        'Loop through the Countries.
        For Each country As String In countries

            'Get the Year for each Country.
            Dim x As Integer() = (From p In dt.AsEnumerable()
                                  Where p.Field(Of String)("CountryName") = country
                                  Order By p.Field(Of Integer)("Yearz")
                                  Select p.Field(Of Integer)("Yearz")).ToArray()

            'Get the Total of Orders for each Country.
            Dim y As Integer() = (From p In dt.AsEnumerable()
                                  Where p.Field(Of String)("CountryName") = country
                                  Order By p.Field(Of Integer)("Yearz")
                                  Select p.Field(Of Integer)("Totalz")).ToArray()

            'Add Series to the Chart.

            StackedBarChart2.Series.Add(New Series(country))

            'StackedBarChart.Legends.Add("Legend1")
            'StackedBarChart.Series(country).Legend = "Legend1"

            StackedBarChart2.Series(country).IsValueShownAsLabel = True
            StackedBarChart2.Series(country).ChartType = SeriesChartType.StackedBar
            StackedBarChart2.Series(country).Points.DataBindXY(x, y)
        Next



        'StackedBarChart.Legends(0).Enabled = True
        div.Controls.Add(StackedBarChart2)

    End Sub

    Private Sub xxx3()


        Dim tablestring = ""

        tablestring = tablestring + "<table width='100%'><tr><td>First Column Heading</td><td>Second column Heading</td><td>Third column Heading</td></tr>"

        tablestring = tablestring + "<tr><td>" + "11" + "</td><td>" + "22" + "</td><td>" + "33" + "</td></tr>"

        tablestring = tablestring + "</table>"

        divTable.innerHTML = tablestring


        Using myConnection As New SqlConnection

            myConnection.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            Dim xQuestionId = 34
            Dim QsubjectId = 16
            Dim xSql = " select aa.answerName,isnull(bb.answerSum,0) as answerSum from ( "
            xSql = xSql + " select A.answerId,A.questionId,B.questionName,A.answerName from surveyAnswer A  "
            xSql = xSql + " inner join surveyQuestion B on B.questionId = A.questionId "
            xSql = xSql + " where A.questionId = " + xQuestionId.ToString() + " ) aa "
            xSql = xSql + " left join ( "
            xSql = xSql + " select answerId, count(*) as answerSum   "
            xSql = xSql + " from surveyUserAnswer  "
            xSql = xSql + " where subjectId = " + QsubjectId.ToString() + " and  questionId = " + xQuestionId.ToString()
            xSql = xSql + " group by answerId "
            xSql = xSql + " ) bb on aa.answerId = bb.answerId "

            Dim myCommand As New SqlCommand
            myCommand.Connection = myConnection
            myCommand.CommandText = xSql

            myConnection.Open()
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()

            Dim chtCategoriesProductCount As New Chart()
            chtCategoriesProductCount.Series.Add(New Series())
            chtCategoriesProductCount.Series(0).Name = "Categories"
            chtCategoriesProductCount.Legends.Add("Legend1")
            chtCategoriesProductCount.Series(0).Legend = "Legend1"

            'Bind the data using the DataBindTable method 
            chtCategoriesProductCount.Series("Categories").Points.DataBindXY(myReader, "answerName", myReader, "answerSum")

            'Dim div2 = New HtmlGenericControl("div")
            'div2.Attributes.Add("id", "divInferior")
            'divImage.Controls.Add(div2)
            divImage.Controls.Add(chtCategoriesProductCount)


            ''Dim div2 = New HtmlGenericControl("PlaceHolderForCharts")
            ''div2.Attributes.Add("id", "divInferior")
            ''divImage.Controls.Add(div2)
            'divImage.Controls.Add(ChartForKPIInLoop)


            myReader.Close()
            myConnection.Close()

        End Using


        'Dim xQuestionId = 34
        'Dim QsubjectId = 16
        'Dim xSql = " select aa.answerName,isnull(bb.answerSum,0) as answerSum from ( "
        'xSql = xSql + " select A.answerId,A.questionId,B.questionName,A.answerName from surveyAnswer A  "
        'xSql = xSql + " inner join surveyQuestion B on B.questionId = A.questionId "
        'xSql = xSql + " where A.questionId = " + xQuestionId.ToString() + " ) aa "
        'xSql = xSql + " left join ( "
        'xSql = xSql + " select answerId, count(*) as answerSum   "
        'xSql = xSql + " from surveyUserAnswer  "
        'xSql = xSql + " where subjectId = " + QsubjectId.ToString() + " and  questionId = " + xQuestionId.ToString()
        'xSql = xSql + " group by answerId "
        'xSql = xSql + " ) bb on aa.answerId = bb.answerId "
        'Dim dt2 As DataTable = GetData(xSql)
        'If dt2.Rows.Count > 0 Then

        'End If
        'Dim ChartForKPIInLoop = New System.Web.UI.DataVisualization.Charting.Chart
        'ChartForKPIInLoop.ID = 1
        'ChartForKPIInLoop.DataSource = dt2

        '' Create new data series and set it's visual attributes 
        'Dim series As New DataVisualization.Charting.Series("Spline")
        'series.ChartType = DataVisualization.Charting.SeriesChartType.Bar
        'series.BorderWidth = 3
        'series.ShadowOffset = 2
        'series.XValueMember = "answerName"
        'series.YValueMembers = "answerSum"

        '' Add series into the chart's series collection 
        'ChartForKPIInLoop.Series.Add(series)
        'ChartForKPIInLoop.DataBind()

        ''Dim div2 = New HtmlGenericControl("PlaceHolderForCharts")
        ''div2.Attributes.Add("id", "divInferior")
        ''divImage.Controls.Add(div2)
        'divImage.Controls.Add(ChartForKPIInLoop)

    End Sub

End Class