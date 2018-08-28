Imports MISClassLibrary
Imports System.Data.SqlClient

Public Class adminAllSurveys
    Inherits System.Web.UI.Page
    Dim now As Date = Date.Now
    Dim inAMonth As Date = Date.Now.AddMonths(1)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
        updateDatabase()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminHome.aspx")
    End Sub

    Private Sub SurveyList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles surveyList.RowCommand
        If (e.CommandName = "Results") Then
            Response.Redirect("results.aspx?subjectId=" + e.CommandArgument.ToString())
        End If
        If (e.CommandName = "SendMail") Then
            If (checkIfClosed(e.CommandArgument.ToString())) Then
                Response.Write("<script LANGUAGE='JavaScript' >alert('This survey is closed. You cannot send mail!')</script>")
                Exit Sub
            End If
            Response.Redirect("sendmail.aspx?subjectId=" + e.CommandArgument.ToString())
        End If
        If (e.CommandName = "Toggle") Then
            If (checkIfClosed(e.CommandArgument.ToString())) Then
                'If MsgBox("Are you sure you want to open this survey?", vbQuestion + vbYesNo) = vbYes Then
                '    openSurvey(e.CommandArgument.ToString())
                'End If
                Dim message As String = "Are you sure you want to open this survey?"
                Dim sb As New System.Text.StringBuilder()
                sb.Append("return confirm('")
                sb.Append(message)
                sb.Append("');")
                ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
                openSurvey(e.CommandArgument.ToString())
            Else
                'If MsgBox("Are you sure you want to close this survey?", vbQuestion + vbYesNo) = vbYes Then
                '    closeSurvey(e.CommandArgument.ToString())
                'End If
                Dim message As String = "Are you sure you want to close this survey?"
                Dim sb As New System.Text.StringBuilder()
                sb.Append("return confirm('")
                sb.Append(message)
                sb.Append("');")
                ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
                closeSurvey(e.CommandArgument.ToString())
            End If


            Response.Redirect("adminAllSurveys.aspx")
        End If
    End Sub

    Private Sub openSurvey(subjId)
        Dim dataT As New DataTable
        Dim connect As New SqlConnection
        Dim command As New SqlCommand
        Try
            connect.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            connect.Open()
            command.Connection = connect
            command.CommandText = "UPDATE SurveyMaster SET status='OPEN', statusComp=0, closeDate = '" + inAMonth + "' WHERE subjectId= " + subjId

                    'create a DataReader and execute the SqlCommand
                    Dim MyDataReader As SqlDataReader = command.ExecuteReader()

            'load the reader into the datatable
            dataT.Load(MyDataReader)

            'clean up
            MyDataReader.Close()
        Catch ex As Exception
            Dim errorMsg = "Error while updating database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            connect.Close()
        End Try
    End Sub

    Private Sub closeSurvey(subjId)
        Dim dataT As New DataTable
        Dim connect As New SqlConnection
        Dim command As New SqlCommand
        Try
            connect.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            connect.Open()
            command.Connection = connect
            command.CommandText = "UPDATE SurveyMaster SET status='CLOSE', statusComp=1, closeDate = '" + now + "' WHERE subjectId= " + subjId

            'create a DataReader and execute the SqlCommand
            Dim MyDataReader As SqlDataReader = command.ExecuteReader()

            'load the reader into the datatable
            dataT.Load(MyDataReader)

            'clean up
            MyDataReader.Close()
        Catch ex As Exception
            Dim errorMsg = "Error while updating database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            connect.Close()
        End Try
    End Sub

    Function checkIfClosed(xsubId) As Boolean
        Dim sqlClosed = " Select * from  surveyMaster where subjectId = " + xsubId.ToString + " and statusComp = 1"
        Dim dt As DataTable = GetData(sqlClosed)

        If (dt.Rows.Count > 0) Then
            Return True
        End If

        Return False
    End Function

    Private Sub updateDatabase()
        Dim dataT As New DataTable
        Dim connect As New SqlConnection
        Dim command As New SqlCommand
        Try
            connect.ConnectionString = My.Settings.ConnStringDatabaseSurvey
            connect.Open()
            command.Connection = connect
            command.CommandText = "UPDATE SurveyMaster SET status='CLOSED', statusComp=1 WHERE closeDate < GETDATE()"

            'create a DataReader and execute the SqlCommand
            Dim MyDataReader As SqlDataReader = command.ExecuteReader()

            'load the reader into the datatable
            dataT.Load(MyDataReader)

            'clean up
            MyDataReader.Close()
        Catch ex As Exception
            Dim errorMsg = "Error while updating database"
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
        Finally
            connect.Close()
        End Try
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

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("index.aspx")
    End Sub

End Class