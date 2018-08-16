Imports System.Data.SqlClient

Public Class userAnswer
    Inherits System.Web.UI.Page

    Public listContent As New List(Of String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
        If IsPostBack() = False Then
            Dim subId = Request.QueryString("subjectId")
            Call getSurveyContent(subId)
        End If
    End Sub

    Protected Sub getSurveyContent(xSubId)
        Dim dt As New DataTable
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try
            con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "SELECT a.*, b.sectionId, b.sectionName, c.questionId, c.questionName, c.questionType, c.sectionorderId 
                               from surveyMaster a left join surveySection b on a.subjectId = b.subjectId
                               left join surveyQuestion c on b.sectionId = c.sectionId 
                               where b.subjectId = " & xSubId.ToString()

            'create a DataReader and execute the SqlCommand
            Dim MyDataReader As SqlDataReader = cmd.ExecuteReader()

            'load the reader into the datatable
            dt.Load(MyDataReader)

            'clean up
            MyDataReader.Close()

            Dim xsubjectId As Integer = 0
            Dim xsubjectName As String = ""
            Dim xsubjectDetail As String = ""
            Dim xstatus As String = ""
            Dim xstatusComp As String = ""
            Dim xopenDate As Date = Now
            Dim xcloseDate As Date = Now
            Dim xcreateDate As Date = Now
            Dim xcreateBy As String = ""
            Dim xsectionId As Integer = 0
            Dim xsectionName As String = ""
            Dim xquestionId As Integer = 0
            Dim xquestionName As String = ""
            Dim xquestionType As String = ""
            'Dim xsectionorderId As Integer = 0



            Dim xTitle As Integer = 0
            Dim xSection As Integer = 0
            If dt.Rows.Count > 0 Then
                For Each r In dt.Rows
                    xsubjectId = r("subjectId")
                    xsubjectName = r("subjectName")
                    xsubjectDetail = r("subjectDetail")
                    xstatus = r("status")
                    xstatusComp = r("statusComp")
                    xopenDate = r("openDate")
                    ' xcloseDate = r("closeDate")
                    xcreateDate = r("createDate")
                    xcreateBy = r("createBy")

                    xsectionId = r("sectionId")
                    xsectionName = r("sectionName")
                    'xsectionorderId = r("sectionorderId")

                    xquestionId = r("questionId")
                    xquestionName = r("questionName")
                    xquestionType = r("questionType")


                    If xTitle = 0 Then
                        listContent.Add("surveyId=" + xsubjectId.ToString())
                        listContent.Add("txtTitle=" + xsubjectName)
                        listContent.Add("txtDesc=" + xsubjectDetail.ToString())
                        listContent.Add("status=" + xstatus)
                        listContent.Add("statusComp=" + xstatusComp.ToString())
                        'listContent.Add("openDate=" + xopenDate.ToString())
                        'listContent.Add("closeDate=" + xcloseDate.ToString())
                        'listContent.Add("createDate=" + xcreateDate.ToString())
                        listContent.Add("createBy=" + xcreateBy)
                        xTitle = 1
                    End If

                    If xSection <> xsectionId Then
                        listContent.Add("sectionId=" + xsectionId.ToString())
                        listContent.Add("sectionName=" + xsectionName)
                        'listContent.Add("sectionorderId=" + xsectionorderId.ToString())

                        xSection = xsectionId
                    End If

                    listContent.Add("questionId=" + xquestionId.ToString())
                    listContent.Add("questionName=" + xquestionName)
                    listContent.Add("questionType=" + xquestionType)

                Next


            End If


        Catch ex As Exception
            Dim errorMsg = "Error While getting information from database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            con.Close()
        End Try

        printListContent()

    End Sub

    'ok so the title will go in place of the title already created on the page
    'and then do a huge for loop through everything else to check for the prefix
    'and then remove it up to the = and then output the content as per.
    Protected Sub printListContent()
        Dim txtTitleLength As New Integer
        Dim txtDescLength As New Integer
        Dim sectionIdLength As New Integer
        Dim sectionNameLength As New Integer
        Dim questionIdLength As New Integer
        Dim questionNameLength As New Integer
        Dim questionTypeLength As New Integer
        txtTitleLength = 9
        txtDescLength = 8
        sectionIdLength = 10
        sectionNameLength = 12
        questionIdLength = 11
        questionNameLength = 13
        questionTypeLength = 13

        Dim t As String = "<p><h1 style='text-align: center'>"
        t += (listContent.Item(1)).Remove(0, txtTitleLength) + "</h1></p>"

        Dim d As String = "<p>" + (listContent.Item(2)).Remove(0, txtDescLength) + "</p>"

        Dim i As Integer
        Dim s As String = ""
        For i = 0 To (listContent.Count - 1)
            Dim v = listContent.Item(i)
            If (v.Contains("sectionName")) Then
                s += "<h3 style='text-decoration: underline'>" + v.Remove(0, sectionNameLength) + "</h3>"
            End If
            If (v.Contains("questionName")) Then
                s += "<p style='font-weight: bold'>" + v.Remove(0, questionNameLength) + "</p>"
            End If
            If (v.Contains("questionType")) Then
                s += "<p>"
                If (v.Contains("radio")) Then
                    s += "radio: " + v.Remove(0, questionTypeLength)
                End If
                If (v.Contains("shortanswer")) Then
                    s += "<textarea id = 'shortAns' class = 'form-control' rows = '2' placeholder = 'Answer'></textarea>"
                End If
                If (v.Contains("grid")) Then
                    s += "grid: " + v.Remove(0, questionTypeLength)
                End If
                s += "</p>"
            End If
        Next

        title.Text = t
        description.Text = d
        frm.Text = s
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("userSurveyList.aspx")
    End Sub
End Class