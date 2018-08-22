Public Class adminHome
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
        Response.Redirect("index.aspx")
    End Sub
End Class