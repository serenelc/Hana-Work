Imports System.Data.SqlClient

Public Class userSurveyList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserType") Is Nothing Then
            Session("UserType") = ""
        ElseIf Session("UserType") = "USER" Then
            Session("UserType") = ""
            Session("Name") = Nothing
            Session("En") = Nothing
        End If
        updateDatabase()
    End Sub

    Private Sub updateDatabase()
        Dim dt As New DataTable
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Try
            con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            con.Open()
            cmd.Connection = con
            cmd.CommandText = "UPDATE SurveyMaster SET status='CLOSED', statusComp=1 WHERE closeDate < GETDATE()"

            'create a DataReader and execute the SqlCommand
            Dim MyDataReader As SqlDataReader = cmd.ExecuteReader()

            'load the reader into the datatable
            dt.Load(MyDataReader)

            'clean up
            MyDataReader.Close()
        Catch ex As Exception
            Dim errorMsg = "Error while updating database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            con.Close()
        End Try
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("index.aspx")
    End Sub

    Private Sub surveyList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles surveyList.RowCommand
        If (e.CommandName = "Go") Then
            Dim subjId = e.CommandArgument.ToString()
            Dim xenReq As Integer = 0

            Dim dt As New DataTable
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Try
                con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
                con.Open()
                cmd.Connection = con
                cmd.CommandText = "SELECT * from surveyMaster where enRequired = 1 and subjectId = " + subjId

                'create a DataReader And execute the SqlCommand
                Dim MyDataReader As SqlDataReader = cmd.ExecuteReader()

                'load the reader into the datatable
                dt.Load(MyDataReader)

                'clean up
                MyDataReader.Close()

                If dt.Rows.Count > 0 Then
                    xenReq = 1
                    'For Each r In dt.Rows
                    '    xenReq = r("enRequired")
                    'Next
                End If
            Catch ex As Exception
                Dim errorMsg = "Error While getting information from database"
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
            Finally
                con.Close()
            End Try

            If (xenReq = 1) Then
                Response.Redirect("login.aspx?subjectId=" + e.CommandArgument.ToString())
            Else
                Response.Redirect("userAnswer.aspx?subjectId=" + e.CommandArgument.ToString())
            End If

        End If
    End Sub

End Class