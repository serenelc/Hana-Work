Imports System.Data.SqlClient

Public Class userSurveyList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If

        If IsPostBack() = False Then
            Dim dt As New DataTable
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Try
                con.ConnectionString = My.Settings.ConnStringDatabaseSurvey
                con.Open()
                cmd.Connection = con
                cmd.CommandText = "Select subjectName, subjectDetail, openDate From surveyMaster Where statusComp = 0"

                'create a DataReader and execute the SqlCommand
                Dim MyDataReader As SqlDataReader = cmd.ExecuteReader()

                'load the reader into the datatable
                dt.Load(MyDataReader)

                'clean up
                MyDataReader.Close()

                If dt.Rows.Count > 0 Then
                    'surveyList.DataSource = dt
                    'surveyList.DataSourceID = SqlDataSource1.ToString()
                    surveyList.DataBind()
                End If

            Catch ex As Exception
                Dim errorMsg = "Error While getting information from database"
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('" & errorMsg & "')", True)
            Finally
                con.Close()
            End Try
        End If

    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("index.aspx")
    End Sub

    Private Sub surveyList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles surveyList.RowCommand
        If (e.CommandName = "Go") Then
            Response.Redirect("userAnswer.aspx?subjectId=" + e.CommandArgument.ToString())
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminHome.aspx")
    End Sub
End Class