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

        QsubjectId = Request.QueryString("subjectId")
        QsubjectId = 16

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

                If r("QuestionType") = "radio" Then
                    Call ChartradioShow(r("QuestionId"), r("QuestionName"))
                End If
                If r("QuestionType") = "shortanswer" Then
                    Call ChartshortanswerShow(r("QuestionId"), r("QuestionName"))
                End If
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
            'pieChart.Series.Clear()
            'pieChart.Titles.Clear()
            Dim T As Title = pieChart.Titles.Add(xQuestionName)
            With T
                .ForeColor = Color.Black
                .BackColor = Color.LightBlue
                .Font = New System.Drawing.Font("Times New Roman", 11.0F, System.Drawing.FontStyle.Bold)
                .BorderColor = Color.Black
            End With
            pieChart.Series(0).Points.DataBindXY(x, y)
            pieChart.Series(0).ChartType = SeriesChartType.Pie
            pieChart.Series(0).Legend = "Legend1"
            pieChart.Series(0).IsValueShownAsLabel = "True"
            pieChart.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True
            ' pieChart.Legends(0).Enabled = True
        End If

    End Sub
    Private Sub ChartshortanswerShow(xQuestionId, xQuestionName)

        Session("subjectId") = QsubjectId.ToString()
        Session("questionId") = xQuestionId.ToString()
        Dim xSql = " Select B.questionName ,A.answerComment from  surveyUserAnswer A  "
        xSql = xSql + " inner Join surveyQuestion B On A.questionId = B.questionId "
        xSql = xSql + " where A.subjectId = " + Session("subjectId")
        xSql = xSql + " And A.questionId = " + Session("questionId")
        Dim dt3 As DataTable = GetData(xSql)
        If dt3.Rows.Count > 0 Then
            DataList1.DataBind()
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

            'Dim xSql = " Select B.answerName,A.questionId,C.questionName,count(*) As cntAnswer from surveyUserAnswer A "
            'xSql = xSql + " Left Join surveyAnswer B  on A.questionId = B.questionId And A.answerId = B.answerId "
            'xSql = xSql + " Left Join surveyQuestion C on C.questionId = A.questionId "
            'xSql = xSql + " where A.subjectId = 16  "
            'xSql = xSql + " And A.questionId in ( " + strSplit + ") "
            'xSql = xSql + " Group by A.questionId, B.answerName, C.questionName "
            Dim xSqlField As String = ""
            Dim xSqlWhere As String = ""
            Dim xSqlmaxQuestion As String = ""
            Dim xSqlmax = " select max(answerName) as maxQuestion from surveyAnswer A where  questionId in (" + strSplit + ") "
            Dim dtmax As DataTable = GetData(xSqlmax)
            If dtmax.Rows.Count > 0 Then
                xSqlmaxQuestion = CInt(Int(dtmax.Rows(0).Item("maxQuestion")))


                For ii As Integer = 1 To xSqlmaxQuestion
                    If xSqlField = "" Then
                        xSqlField = "isnull([" + ii.ToString() + "],0) As [" + ii.ToString() + "]"
                        xSqlWhere = "[" + ii.ToString() + "]"
                    Else
                        xSqlField = xSqlField + ",isnull([" + ii.ToString() + "],0) As [" + ii.ToString() + "]"
                        xSqlWhere = xSqlWhere + ",[" + ii.ToString() + "]"
                    End If
                Next
            End If

            Dim xSql = " Select questionName," + xSqlField + " from ( "
            xSql = xSql + " Select B.answerName,A.questionId,C.questionName,count(*) As cntAnswer from surveyUserAnswer A "
            xSql = xSql + " Left Join surveyAnswer B  on A.questionId = B.questionId And A.answerId = B.answerId "
            xSql = xSql + " Left Join surveyQuestion C on C.questionId = A.questionId "
            xSql = xSql + " where A.subjectId =  " + xobjStrListGrid
            xSql = xSql + " And A.questionId in ( " + strSplit + ") "
            xSql = xSql + " Group by A.questionId, B.answerName, C.questionName "
            xSql = xSql + " ) PivotExample "

            xSql = xSql + " PIVOT "
            xSql = xSql + " ( "
            xSql = xSql + " SUM(cntAnswer) "
            xSql = xSql + " For answerName IN (" + xSqlWhere + ") "

            xSql = xSql + " ) A "
            xSql = xSql + " order by questionId "


            Dim dt4 As DataTable = GetData(xSql)
            If dt4.Rows.Count > 0 Then
                chtCategoriesProductCountBarChart.Series("Answer 1").XValueMember = "questionName"
                chtCategoriesProductCountBarChart.Series("Answer 1").YValueMembers = "1"

                chtCategoriesProductCountBarChart.Series("Answer 2").XValueMember = "questionName"
                chtCategoriesProductCountBarChart.Series("Answer 2").YValueMembers = "2"

                chtCategoriesProductCountBarChart.Series("Answer 3").XValueMember = "questionName"
                chtCategoriesProductCountBarChart.Series("Answer 3").YValueMembers = "3"

                chtCategoriesProductCountBarChart.Series("Answer 4").XValueMember = "questionName"
                chtCategoriesProductCountBarChart.Series("Answer 4").YValueMembers = "4"

                chtCategoriesProductCountBarChart.DataSource = dt4
                chtCategoriesProductCountBarChart.DataBind()


            End If

        End If
    End Sub


End Class