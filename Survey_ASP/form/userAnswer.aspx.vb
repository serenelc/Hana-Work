Imports System.Data.SqlClient

Public Class userAnswer
    Inherits System.Web.UI.Page

    Public listContent As New List(Of String)
    Public xcreateDate = Date.Now
    Public xsurveyId As Integer = 0
    Public xquestionId As Integer = 0
    Public prmSubmitId As Integer = 0
    Public prmSurveyId As Integer = 0
    Public prmUserAnswerId As Integer = 0
    Public prmQuestionID As Integer = 0
    Public prmAnswerID As Integer = 0
    Public prmAnswerComment As String = ""
    Public c As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If

        If IsPostBack() = False Then
            Dim subId = Request.QueryString("subjectId")
            Call getSurveyContent(subId)
            Session("surveyId") = xsurveyId.ToString()
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
                               d.answerId, d.answerName
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
                        listContent.Add("createBy=" + xcreateBy)
                        xTitle = 1
                    End If

                    If xSection <> xsectionId Then
                        listContent.Add("sectionId=" + xsectionId.ToString())
                        listContent.Add("sectionName=" + xsectionName)

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

    Protected Sub printListContent()

        Dim t As String = "<p><h1 style='text-align: center' id='txtTitle' name='txtTitle'>"
        t += (listContent.Item(1)).Substring(listContent.Item(1).IndexOf("=") + 1) + "</h1></p>"

        Dim d As String = "<p  id='txtDesc' name='txtDesc'>" + (listContent.Item(2)).Substring(listContent.Item(2).IndexOf("=") + 1) + "</p>"

        Dim i As Integer
        Dim s As String = ""
        Dim rFlag As Integer = 0
        Dim gFlag As Integer = 0
        Dim aFlag As Integer = 0
        Dim gNumRad As Integer = 0
        Dim answerIdFlag As Integer = 0
        For i = 0 To (listContent.Count - 1)
            Dim v = listContent.Item(i)

            If (v.Contains("questionId=")) Then
                xquestionId = v.Substring(v.IndexOf("=") + 1)
            End If
            If (v.Contains("answerId")) Then
                answerIdFlag = v.Substring(v.IndexOf("=") + 1)
            End If
            If (v.Contains("sectionName")) Then
                s += "<br><h3 style='background-color:" + colorWheel() + ";"
                s += "border-radius: 5px; padding-left: 5px;' id='sectionTitle_name' name='sectionTitle_name'>" + v.Substring(v.IndexOf("=") + 1) + "</h3>"
            End If
            If (v.Contains("questionName")) Then
                Dim gT = listContent.Item(i + 1)
                Dim gI = listContent.Item(i + 2)
                If (Not gI.Contains("answerId=0") And gT.Contains("grid")) Then
                    s += "<div class='form-check-inline col-12'><div class='col-6'><p id='questionInput_name' name='questionInput_name'>" + v.Substring(v.IndexOf("=") + 1) + "</div><div class='col-6' style='overflow:scroll;height:auto;width:100%;overflow-y:hidden;overflow-x:scroll;'>"
                Else
                    s += "<div><p style='font-weight: bold' id='questionInput_name' name='questionInput_name'>" + v.Substring(v.IndexOf("=") + 1) + "</p>"
                End If
            End If
            If (v.Contains("questionType")) Then
                If (v.Contains("radio")) Then
                    rFlag = 1
                    gFlag = 0
                    aFlag = 0
                    s += "<p><div class='form-check-inline'>"
                End If
                If (v.Contains("shortanswer")) Then
                    rFlag = 0
                    gFlag = 0
                    aFlag = 1

                End If
                If (v.Contains("grid")) Then
                    Dim w = listContent.Item(i + 1)
                    If (Not w.Contains("answerId=0")) Then
                        gFlag = 1
                    End If
                    rFlag = 0
                    aFlag = 0
                End If
            End If
            If (v.Contains("answerName") And rFlag = 1) Then
                s += "<input type='radio' class='form-check-input' id='rad' name='rad" + xquestionId.ToString() + "' value='" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                s += "style ='padding-left: 30px;'>"
                s += "<label class='form-check-label' style='padding-right: 20px;'>" + v.Substring(v.IndexOf("=") + 1) + "</label>"
            End If
            If (v.Contains("answerName") And gFlag = 1) Then
                gNumRad = v.Substring(v.IndexOf("=") + 1)
                s += "<input type='radio' class='form-check-input' id='grid' name='grid" + xquestionId.ToString() + "' value='" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                s += "style ='padding-left: 30px;'>"
                s += "<label class='form-check-label' style='padding-right: 20px;'>" + gNumRad.ToString() + "</label>"
                If (i = listContent.Count - 1) Then
                    s += "</div>"
                End If
            End If
            If (v.Contains("answerName") And aFlag = 1) Then
                s += "<p><textarea id='short' class='form-control' rows='2' placeholder='Answer' name='short" + "_Q" + xquestionId.ToString() + "_A" + answerIdFlag.ToString() + "'"
                s += "></textarea></div>"
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And rFlag = 1) Then
                s += "</div><br></div>"
                rFlag = 0
            End If
            If ((v.Contains("sectionId") Or v.Contains("questionId")) And gFlag = 1) Then
                s += "</div></p></div><br>"
                gFlag = 0
                Dim n = listContent.Item(i + 2)
                If (Not n.Contains("grid")) Then
                    s += "</div>"
                End If
            End If
            If (v.Contains("surveyId=")) Then
                xsurveyId = v.Substring(v.IndexOf("=") + 1)
            End If
        Next
        title.Text = t
        description.Text = d
        frm.Text = s
    End Sub

    Private Function colorWheel()
        Dim colorList As New List(Of String)
        colorList.Add("#f46e42")
        colorList.Add("#f4e541")
        colorList.Add("#7ff441")
        colorList.Add("#41f467")
        colorList.Add("#41eef4")
        colorList.Add("#4173f4")
        colorList.Add("#6a41f4")
        colorList.Add("#a641f4")
        colorList.Add("#f441d9")
        colorList.Add("#f44161")
        c = c + 1
        If (c = colorList.Count - 1) Then
            c = 0
        End If
        Return colorList.Item(c)

    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("userSurveyList.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '   Dim ClientQueryList = Request.QueryString
        Dim ClientQueryList = Request.Form

        Dim arrKey As New List(Of String)
        Dim arrItem As New List(Of String)
        For i As Integer = 0 To ClientQueryList.Count - 1
            'ignore the first 3 items which are random gobbeldy goop
            If (i > 2) Then
                arrKey.Add(ClientQueryList.Keys(i))
                arrItem.Add(ClientQueryList.Item(i))
            End If
        Next

        'Open connection database
        Dim SQLConn As New SqlConnection(My.Settings.ConnStringDatabaseSurvey)
        Dim SQLTran As SqlTransaction

        Dim SaveComp = False

        SQLConn.Open()
        SQLTran = SQLConn.BeginTransaction

        Try
            prmSurveyId = Session("surveyId")
            If (SaveSurveyUserSubmit(SQLConn, SQLTran) = False) Then Throw New Exception("Save SurveyUserSubmit fail!")

            For i As Integer = 0 To arrKey.Count - 1
                Dim xval As String = arrItem(i).ToString()
                Dim xKey As String = arrKey(i).ToString()

                If (Not xKey.Contains("btnSave")) Then
                    Dim indexQ As Integer = xval.IndexOf("_Q")
                    Dim indexA As Integer = xval.IndexOf("_A")
                    Dim qIdLength As Integer = indexA - indexQ - 2

                    'Short answers have a different layout
                    If (xKey.Contains("short_Q")) Then
                        indexA = xKey.IndexOf("_A")
                        indexQ = xKey.IndexOf("_Q")
                        qIdLength = indexA - indexQ - 2
                        prmQuestionID = xKey.Substring(indexQ + 2, qIdLength)
                        prmAnswerID = xKey.Substring(indexA + 2)
                        prmAnswerComment = xval
                    Else
                        prmQuestionID = xval.Substring(indexQ + 2, qIdLength)
                        prmAnswerID = xval.Substring(indexA + 2)
                        prmAnswerComment = ""
                    End If

                    If (SaveSurveyUserAnswer(SQLConn, SQLTran) = False) Then Throw New Exception("Save SurveyUserAnswer fail!")
                End If

            Next

            SaveComp = True
            SQLTran.Commit()
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Save successful!')", True)

        Catch ex As Exception

            Dim errorMsg = "Error While inserting record On table..." & ex.Message & ", Insert Records"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "Save')", True)
            SQLTran.Commit()
            SQLConn.Close()

        Finally
            If SaveComp Then
                SQLConn.Close()
                Response.Redirect("userSurveyComplete.aspx")
            End If
        End Try
    End Sub

    'Save SaveSurveyUserSubmit
    Private Function SaveSurveyUserSubmit(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction) As Boolean

        Dim str As String = String.Empty
        'New

        str = "INSERT INTO SurveyUserSubmit(subjectId, submitDate, submitBy) "
        str = str + " VALUES(@subjectId, @submitDate, @submitBy); Set @submitID = SCOPE_IDENTITY() "
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
            With SQLCmd
                .Parameters.Clear()

                .Parameters.AddWithValue("@subjectId", prmSurveyId)
                .Parameters.AddWithValue("@submitDate", xcreateDate)
                .Parameters.AddWithValue("@submitBy", Session("En"))

                Dim prm_submitid As System.Data.SqlClient.SqlParameter = New SqlParameter("@submitID", SqlDbType.Int)
                prm_submitid.Direction = ParameterDirection.Output
                prm_submitid.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_submitid)
                .ExecuteNonQuery()

                prmSubmitId = prm_submitid.Value
            End With
        End Using

        Return True

    End Function

    'Save SaveSurveyUserAnswer
    Private Function SaveSurveyUserAnswer(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction) As Boolean

        Dim str As String = String.Empty
        'New

        str = "INSERT INTO SurveyUserAnswer(submitId, subjectId, questionId, answerId, answerComment) "
        str = str + " VALUES(@submitId, @subjectId, @questionId, @answerId, @answerComment); Set @userAnswerId = SCOPE_IDENTITY() "
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
            With SQLCmd
                .Parameters.Clear()

                .Parameters.AddWithValue("@submitId", prmSubmitId)
                .Parameters.AddWithValue("@subjectId", prmSurveyId)
                .Parameters.AddWithValue("@questionId", prmQuestionID)
                .Parameters.AddWithValue("@answerId", prmAnswerID)
                .Parameters.AddWithValue("@answerComment", prmAnswerComment)

                Dim prm_userAnswerId As System.Data.SqlClient.SqlParameter = New SqlParameter("@userAnswerId", SqlDbType.Int)
                prm_userAnswerId.Direction = ParameterDirection.Output
                prm_userAnswerId.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_userAnswerId)
                .ExecuteNonQuery()

                prmUserAnswerId = prm_userAnswerId.Value
            End With
        End Using

        Return True

    End Function

End Class
