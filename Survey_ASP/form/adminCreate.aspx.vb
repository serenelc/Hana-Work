Imports System.Data.SqlClient

Public Class adminCreate
    Inherits System.Web.UI.Page

    Public divSecTitle_val = ""
    Public prmCloseDate As DateTime
    Public prmSubjid As Integer = 0
    Public prmSectionID As Integer = 0
    Public prmSectionOrder As Integer = 0
    Public prmQuestionID As Integer = 0
    Public prmQuestionOrder As Integer = 0
    Public prmAnswerID As Integer = 0
    Public prmAnswerOrder As Integer = 0
    Public xupdateValueType = ""
    Public xcreateDate As DateTime = Date.Now
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        txtTitle.Value = Request.QueryString("txtTitle")
        txtDesc.Value = Request.QueryString("txtDesc")

        xcreateDate = Date.Now

        Dim ClientQueryList = Request.QueryString

        Dim strRep = ClientQueryString.Replace("+", " ")
        'strArr has all the inputted data including flags indicating what each of the fields are
        Dim strArr() = strRep.Split("&")
        'strArr2 has all the inputted data and can take Thai characters, but does not have flags indicating what each of the fields are.
        Dim strArr2 As New List(Of String)
        For i As Integer = 0 To ClientQueryList.Count - 2
            If i > 2 Then
                'radio fields put all the radio options into 1 field so they need to be split.
                Dim strSplit() = ClientQueryList.Item(i).Split(",")
                If strSplit.Count > 1 Then
                    For y As Integer = 0 To strSplit.Count - 1
                        strArr2.Add(strSplit(y).ToString())
                    Next
                Else
                    strArr2.Add(ClientQueryList.Item(i))
                End If
            End If
        Next

        Dim SaveComp = False
        Dim SQLConn As New SqlConnection(My.Settings.ConnStringDatabaseSurvey)
        Dim SQLTran As SqlTransaction

        SQLConn.Open()
        SQLTran = SQLConn.BeginTransaction

        Try
            'Save 
            prmSubjid = 0
            For i As Integer = 0 To strArr.Count - 2
                Dim val As String = strArr(i).ToString()
                If val.Contains("close=") Then
                    Dim d = strArr2.Item(i)
                    Dim parsed = Date.Parse(d)

                    prmCloseDate = New DateTime(parsed.Year, parsed.Month, parsed.Day, 0, 0, 0)

                    If (Not validateData()) Then
                        Exit Sub
                    End If

                    Dim hasQuestion As Integer = 0
                    For Each el In strArr
                        If (el.Contains("questionInput_name")) Then
                            hasQuestion = 1
                            Exit For
                        End If
                    Next

                    If (hasQuestion = 0) Then
                        'Admin has not inputted a section or a question.
                        Throw New Exception("Please create at least 1 question!")
                    End If

                    If (SaveSurveyMaster(SQLConn, SQLTran) = False) Then Throw New Exception("Save surveyMaster fail!")
                Else
                    If val.Contains("sectionTitle_name") = True Then
                        prmSectionID = 0
                        prmSectionOrder = 0
                        prmQuestionID = 0
                        prmQuestionOrder = 0
                        prmAnswerID = 0
                        prmAnswerOrder = 0
                        Dim updateValue = strArr2.Item(i)
                        If (SaveSurveySection(SQLConn, SQLTran, updateValue) = False) Then Throw New Exception("Save surveySection fail!")
                    Else
                        If val.Contains("questionInput_name") = True Then
                            Dim updateValue = strArr2.Item(i)
                            Dim val2 As String = strArr(i + 1).ToString()
                            Dim updateValueType As String = ""
                            If val2.Contains("rad") = True Then
                                updateValueType = "radio"
                            Else
                                If val2.Contains("short") = True Then
                                    updateValueType = "shortanswer"
                                Else
                                    If val2.Contains("grid") = True Then
                                        updateValueType = "grid"
                                    End If
                                End If
                            End If

                            xupdateValueType = updateValueType

                            If (SaveSurveyQuestion(SQLConn, SQLTran, updateValue, updateValueType) = False) Then Throw New Exception("Error while saving to database! Save surveyQuestion fail!")
                        Else
                            If xupdateValueType = "grid" Then
                                Dim updateValue = strArr2.Item(i)
                                If (SaveSurveyQuestion(SQLConn, SQLTran, updateValue, xupdateValueType) = False) Then Throw New Exception("Error while saving to database! Save surveyQuestion fail!")
                                Dim updateValue2 As String = val.Substring(val.IndexOf("=") - 2, 2)
                                Dim updateValue3 As String = ""
                                If updateValue2.Contains("_") = True Then
                                    updateValue3 = Right(updateValue2, 1)
                                Else
                                    updateValue3 = updateValue2
                                End If
                                For ii As Integer = 1 To updateValue3
                                    If (SaveSurveyAnswer(SQLConn, SQLTran, ii) = False) Then Throw New Exception("Error while saving to database! Save surveyAnswer fail!")
                                Next
                            Else
                                If val.Contains("rad") = True Or val.Contains("short") = True Or val.Contains("grid") = True Then
                                    Dim updateValue = strArr2.Item(i)
                                    If (SaveSurveyAnswer(SQLConn, SQLTran, updateValue) = False) Then Throw New Exception("Error while saving to database! Save surveyAnswer fail!")
                                End If
                            End If

                        End If
                    End If
                End If

            Next

            SaveComp = True
            SQLTran.Commit()

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & ex.Message & "')", True)
            SQLTran.Rollback()
            SQLConn.Close()
        Finally
            If SaveComp Then
                SQLConn.Close()
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Save successful!')", True)
                Response.Redirect("adminCreateComplete.aspx")
            End If
        End Try

    End Sub

    Function validateData()
        Try
            If txtTitle.Value = "" Then Throw New Exception("Please fill in the Title")
            If txtDesc.Value = "" Then Throw New Exception("Please fill in the Description")
            If (prmCloseDate < xcreateDate) Then Throw New Exception("Please choose a close date in the future")
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & ex.Message & "')", True)
            Return False
        End Try
        Return True
    End Function

    'Save surveyMaster
    Private Function SaveSurveyMaster(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction) As Boolean

        Dim str As String = String.Empty
        'New
        If prmSubjid = 0 Then
            str = "INSERT INTO surveyMaster(subjectName,subjectDetail,status,openDate,closeDate,createBy,createDate) "
            str = str + " VALUES(@subjectName, @subjectDetail, @status,@openDate,@closeDate,@createBy,@createDate); Set @subj_ID = SCOPE_IDENTITY() "
            Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
                With SQLCmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@subjectName", txtTitle.Value)
                    .Parameters.AddWithValue("@subjectDetail", txtDesc.Value)
                    .Parameters.AddWithValue("@status", "OPEN")
                    .Parameters.AddWithValue("@openDate", xcreateDate)
                    .Parameters.AddWithValue("@closeDate", prmCloseDate)
                    .Parameters.AddWithValue("@createBy", Session("En"))
                    .Parameters.AddWithValue("@createDate", xcreateDate)

                    Dim prm_subjid As System.Data.SqlClient.SqlParameter = New SqlParameter("@subj_ID", SqlDbType.Int)
                    prm_subjid.Direction = ParameterDirection.Output
                    prm_subjid.SqlDbType = SqlDbType.Int
                    .Parameters.Add(prm_subjid)
                    .ExecuteNonQuery()

                    prmSubjid = prm_subjid.Value
                End With
            End Using
        Else 'Update
            str = "UPDATE surveyMaster set subjectDetail=@subjectDetail where subjectId = @subjectId "
            Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                             .Transaction = SQLTran,
                                                             .CommandType = CommandType.Text,
                                                             .CommandText = str}
                With SQLCmd
                    .Parameters.Clear()
                    .Parameters.AddWithValue("@subjectDetail", txtDesc.Value)
                    .Parameters.AddWithValue("@subjectId", prmSubjid)
                    .ExecuteNonQuery()
                End With
            End Using
        End If
        Return True

    End Function

    'Save surveySection
    Private Function SaveSurveySection(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction, updateValue As String) As Boolean

        Dim str As String = String.Empty
        prmSectionOrder += 10
        str = "INSERT INTO surveySection(subjectId,subjectorderId,sectionName,createDate,createBy) "
        str = str + " VALUES(@subjectId, @subjectorderId, @sectionName, @createDate,@createBy); Set @section_ID = SCOPE_IDENTITY()"
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                            .Transaction = SQLTran,
                                                            .CommandType = CommandType.Text,
                                                            .CommandText = str}
            With SQLCmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@subjectId", prmSubjid)
                .Parameters.AddWithValue("@subjectorderId", prmSectionOrder)
                .Parameters.AddWithValue("@sectionName", updateValue)
                .Parameters.AddWithValue("@createDate", xcreateDate)
                .Parameters.AddWithValue("@createBy", Session("En"))
                Dim prm_sectionid As System.Data.SqlClient.SqlParameter = New SqlParameter("@section_ID", SqlDbType.Int)
                prm_sectionid.Direction = ParameterDirection.Output
                prm_sectionid.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_sectionid)
                .ExecuteNonQuery()

                prmSectionID = prm_sectionid.Value
            End With
        End Using
        Return True

    End Function
    'Save surveyQuestion
    Private Function SaveSurveyQuestion(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction, updateValue As String, updateValueType As String) As Boolean

        Dim str As String = String.Empty
        prmQuestionOrder += 10


        str = "INSERT INTO surveyQuestion(sectionId,sectionorderId,questionName,questionanswerReq,questionType,createDate ,createBy) "
        str = str + " VALUES(@sectionId,@sectionorderId,@questionName,@questionanswerReq,@questionType,@createDate ,@createBy); Set @question_ID = SCOPE_IDENTITY()"
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                            .Transaction = SQLTran,
                                                            .CommandType = CommandType.Text,
                                                            .CommandText = str}
            With SQLCmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@sectionId", prmSectionID)
                .Parameters.AddWithValue("@sectionorderId", prmQuestionOrder)
                .Parameters.AddWithValue("@questionName", updateValue)
                .Parameters.AddWithValue("@questionanswerReq", 0)
                .Parameters.AddWithValue("@questionType", updateValueType)
                .Parameters.AddWithValue("@createDate", xcreateDate)
                .Parameters.AddWithValue("@createBy", Session("En"))
                Dim prm_questionid As System.Data.SqlClient.SqlParameter = New SqlParameter("@question_ID", SqlDbType.Int)
                prm_questionid.Direction = ParameterDirection.Output
                prm_questionid.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_questionid)
                .ExecuteNonQuery()

                prmQuestionID = prm_questionid.Value
            End With
        End Using
        Return True

    End Function
    'Save surveyAnswer
    Private Function SaveSurveyAnswer(ByRef SQLConn As SqlConnection, ByRef SQLTran As SqlTransaction, updateValue As String) As Boolean

        Dim str As String = String.Empty
        prmAnswerOrder += 10

        str = "INSERT INTO surveyAnswer(questionId,answerorderId,answerName,createDate,createBy) "
        str = str + " VALUES(@questionId,@answerorderId,@answerName,@createDate,@createBy); Set @answer_ID = SCOPE_IDENTITY()"
        Using SQLCmd As New SqlCommand With {.Connection = SQLConn,
                                                            .Transaction = SQLTran,
                                                            .CommandType = CommandType.Text,
                                                            .CommandText = str}
            With SQLCmd
                .Parameters.Clear()
                .Parameters.AddWithValue("@questionId", prmQuestionID)
                .Parameters.AddWithValue("@answerorderId", prmAnswerOrder)
                .Parameters.AddWithValue("@answerName", updateValue)
                .Parameters.AddWithValue("@createDate", xcreateDate)
                .Parameters.AddWithValue("@createBy", Session("En"))

                Dim prm_answerid As System.Data.SqlClient.SqlParameter = New SqlParameter("@answer_ID", SqlDbType.Int)
                prm_answerid.Direction = ParameterDirection.Output
                prm_answerid.SqlDbType = SqlDbType.Int
                .Parameters.Add(prm_answerid)
                .ExecuteNonQuery()

                prmAnswerID = prm_answerid.Value
            End With
        End Using
        Return True

    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        'TODO:   Ask the user if they are sure that they want to go back having Not saved
        Response.Redirect("adminHome.aspx")
    End Sub
End Class