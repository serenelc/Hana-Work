Imports System.Data.SqlClient

Public Class userAnswer
    Inherits System.Web.UI.Page

    Public listContent As New List(Of String)
    Public txtTitle = ""
    Public txtDesc = ""
    Public xcreateDate = Date.Now

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
            cmd.CommandText = "SELECT a.*, b.sectionId, b.sectionName, c.questionId, c.questionName, c.questionType, 
                               c.sectionorderId, d.answerId, d.answerName
                               From surveyMaster a left Join surveySection b on a.subjectId = b.subjectId
                               Left Join surveyQuestion c on b.sectionId = c.sectionId 
                               Left Join surveyAnswer d on c.questionId = d.questionId
                               where b.subjectId =" & xSubId.ToString()

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
            Dim xanswerId As Integer = 0
            Dim xanswerName As String = ""


            Dim xTitle As Integer = 0
            Dim xSection As Integer = 0
            Dim xQuestion As Integer = 0
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
                    If IsDBNull(r("answerId")) = True Then
                        xanswerId = 0
                    Else
                        xanswerId = r("answerId")
                    End If
                    If IsDBNull(r("answerName")) = True Then
                        xanswerName = ""
                    Else
                        xanswerName = r("answerName")
                    End If

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

                    If xQuestion <> xquestionId Then
                        listContent.Add("questionId=" + xquestionId.ToString())
                        listContent.Add("questionName=" + xquestionName)
                        listContent.Add("questionType=" + xquestionType)

                        xQuestion = xquestionId
                    End If

                    listContent.Add("answerId=" + xanswerId.ToString())
                    listContent.Add("answerName=" + xanswerName)

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
        Dim answerIdLength As New Integer
        Dim answerNameLength As New Integer
        txtTitleLength = 9
        txtDescLength = 8
        sectionIdLength = 10
        sectionNameLength = 12
        questionIdLength = 11
        questionNameLength = 13
        questionTypeLength = 13
        answerIdLength = 9
        answerNameLength = 11

        Dim t As String = "<p><h1 style='text-align: center' id='txtTitle' name='txtTitle'>"
        t += (listContent.Item(1)).Remove(0, txtTitleLength) + "</h1></p>"

        Dim d As String = "<p  id='txtDesc' name='txtDesc'>" + (listContent.Item(2)).Remove(0, txtDescLength) + "</p>"

        Dim i As Integer
        Dim s As String = ""
        Dim rFlag As Integer = 0
        Dim gFlag As Integer = 0
        Dim rNameFlag As Integer = 0
        Dim gNumRad As Integer = 0
        For i = 0 To (listContent.Count - 1)
            Dim v = listContent.Item(i)
            If (v.Contains("sectionName")) Then
                s += "<br> <h3 style='text-decoration: underline' id='sectionTitle_name' name='sectionTitle_name'>" + v.Remove(0, sectionNameLength) + "</h3>"
            End If
            If (v.Contains("questionName")) Then
                s += "<div><p style='font-weight: bold' id='questionInput_name' name='questionInput_name'>" + v.Remove(0, questionNameLength) + "</p>"
            End If
            If (v.Contains("questionId")) Then
                rNameFlag = v.Remove(0, questionIdLength)
            End If
            If (v.Contains("questionType")) Then
                s += "<p>"
                If (v.Contains("radio")) Then
                    rFlag = 1
                    gFlag = 0
                    s += "<div class='form-check-inline'>"
                End If
                If (v.Contains("shortanswer")) Then
                    rFlag = 0
                    gFlag = 0
                    s += "<textarea id = 'short' name='short' class = 'form-control' rows = '2' placeholder = 'Answer'></textarea></div>"
                End If
                If (v.Contains("grid")) Then
                    Dim w = listContent.Item(i + 1)
                    If (Not w.Contains("answerId=0")) Then
                        gFlag = 1
                        s += "<div class='form-check-inline'>"
                    End If
                    rFlag = 0

                End If
                s += "</p>"
            End If
            If (v.Contains("answerName") And rFlag = 1) Then
                s += "<input type='radio' class='form-check-input' id='rad' name='rad" + rNameFlag.ToString() + "'"
                s += "style ='padding-left: 30px;'>"
                s += "<label class='form-check-label' style='padding-right: 20px;'>" + v.Remove(0, answerNameLength) + "</label>"
            End If
            If (v.Contains("answerName") And gFlag = 1) Then
                Dim j As Integer
                gNumRad = v.Substring(v.IndexOf("=") + 1)
                For j = 1 To gNumRad
                    s += "<input type='radio' class='form-check-input' id='grid' name='grid" + rNameFlag.ToString() + "'"
                    s += "style ='padding-left: 30px;'>"
                    s += "<label class='form-check-label' style='padding-right: 20px;'>" + j.ToString() + "</label>"
                Next
                s += "</div>"
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And rFlag = 1) Then
                s += "</div><br></div>"
                rFlag = 0
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And gFlag = 1) Then
                s += "</div><br>"
                gFlag = 0
            End If
        Next

        title.Text = t
        description.Text = d
        frm.Text = s
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("userSurveyList.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        txtTitle = Request.QueryString("txtTitle")
        txtDesc = Request.QueryString("txtDesc")

        If (validateData() = False) Then
            Exit Sub
        End If

        Dim ClientQueryList = Request.QueryString
        Dim aa = ClientQueryList
        Dim bb = ClientQueryString

        Dim strRep = ClientQueryString.Replace("+", " ")
        Dim strArr() = strRep.Split("&")

        'Dim SQLConn As New SqlConnection(My.Settings.ConnStringDatabaseSurvey)
        'Dim SQLTran As SqlTransaction

        'SQLConn.Open()
        'SQLTran = SQLConn.BeginTransaction

        'Try
        '    'Save 
        '    prmSubjid = 0
        '    For i As Integer = 0 To strArr.Count - 1
        '        Dim val As String = strArr(i).ToString()
        '        If val.Contains("txtTitle=") = True Or val.Contains("txtDesc=") = True Then
        '            If (SaveSurveyMaster(SQLConn, SQLTran) = False) Then Throw New Exception("Save surveyMaster fail!")
        '        Else
        '            If val.Contains("sectionTitle_name") = True Then
        '                prmSectionID = 0
        '                prmSectionOrder = 0
        '                prmQuestionID = 0
        '                prmQuestionOrder = 0
        '                prmAnswerID = 0
        '                prmAnswerOrder = 0
        '                Dim updateValue As String = val.Substring(val.IndexOf("=") + 1)
        '                If (SaveSurveySection(SQLConn, SQLTran, updateValue) = False) Then Throw New Exception("Save surveyMaster fail!")
        '            Else
        '                If val.Contains("questionInput_name") = True Then
        '                    Dim updateValue As String = val.Substring(val.IndexOf("=") + 1)
        '                    Dim val2 As String = strArr(i + 1).ToString()
        '                    Dim updateValueType As String = ""
        '                    If val2.Contains("rad") = True Then
        '                        updateValueType = "radio"
        '                    Else
        '                        If val2.Contains("short") = True Then
        '                            updateValueType = "shortanswer"
        '                        Else
        '                            If val2.Contains("grid") = True Then
        '                                updateValueType = "grid"
        '                            End If
        '                        End If
        '                    End If

        '                    xupdateValueType = updateValueType


        '                    If (SaveSurveyQuestion(SQLConn, SQLTran, updateValue, updateValueType) = False) Then Throw New Exception("Save surveyMaster fail!")
        '                Else
        '                    If xupdateValueType = "grid" Then
        '                        Dim updateValue As String = val.Substring(val.IndexOf("=") + 1)
        '                        If (SaveSurveyQuestion(SQLConn, SQLTran, updateValue, xupdateValueType) = False) Then Throw New Exception("Save surveyMaster fail!")
        '                        Dim updateValue2 As String = val.Substring(val.IndexOf("=") - 2, 2)
        '                        Dim updateValue3 As String = ""
        '                        If updateValue2.Contains("_") = True Then
        '                            updateValue3 = Right(updateValue2, 1)
        '                        Else
        '                            updateValue3 = updateValue2
        '                        End If
        '                        If (SaveSurveyAnswer(SQLConn, SQLTran, updateValue3) = False) Then Throw New Exception("Save surveyMaster fail!")
        '                    Else
        '                        If val.Contains("rad") = True Or val.Contains("short") = True Or val.Contains("grid") = True Then
        '                            Dim updateValue As String = val.Substring(val.IndexOf("=") + 1)
        '                            If (SaveSurveyAnswer(SQLConn, SQLTran, updateValue) = False) Then Throw New Exception("Save surveyMaster fail!")
        '                        End If
        '                    End If

        '                End If
        '            End If
        '        End If
        '    Next

        '    SQLTran.Commit()
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Save successful!')", True)
        '    Response.Redirect("adminHome.aspx")
        'Catch ex As Exception
        '    If (SQLTran IsNot Nothing) Then SQLTran.Rollback()
        '    Dim errorMsg = "Error While inserting record On table..." & ex.Message & ",Insert Records"
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "Save')", True)
        'Finally
        '    SQLConn.Close()
        'End Try
    End Sub

    Function validateData()
        Try
            'If Session("En") Is Nothing Then Throw New Exception("Please login first!")
            If txtTitle = "" Then Throw New Exception("Please fill Title!")
            If txtDesc = "" Then Throw New Exception("Please fill Description!")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & ex.Message & "')", True)
            Return False
        End Try
        Return True
    End Function

End Class