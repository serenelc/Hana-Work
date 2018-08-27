Public Class adminHome
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
        If MsgBox("Are you sure you want to logout?", vbQuestion + vbYesNo) = vbYes Then
            Response.Redirect("index.aspx")
        End If
    End Sub
End Class