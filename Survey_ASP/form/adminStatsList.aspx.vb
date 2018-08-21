Public Class adminStatsList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminHome.aspx")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("login.aspx")
    End Sub

    Private Sub surveyList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles surveyList.RowCommand
        If (e.CommandName = "Go") Then
            Response.Redirect("results.aspx?subjectId=" + e.CommandArgument.ToString())
        End If
    End Sub

End Class