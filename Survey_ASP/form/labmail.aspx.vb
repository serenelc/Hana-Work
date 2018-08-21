Imports MISClassLibrary
Public Class labmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Function SendEmail() As Boolean
        'Dim db As New DbController(DbController.DbSource.IMS_IMP)
        'Dim dt As DataTable
        'Dim mail As New EmailController(SystemInfo.SystemList.QLR)
        'Dim mailTo As String = txtEmail.Value
        'Dim mailSubject As String = txtSubject.Value
        'Dim mailMessage As String = txtMessage.Value

        'Try
        '    mail.Subject = mailSubject
        '    mail.Body = "<p><font face='Calibri(body)' style='color:#002b80'>"
        '    mail.Body &= mailMessage
        '    mail.Body &= " </font></p>"

        '    mail.BccAddress.Clear()
        '    mail.ToAddress.Clear()
        '    mail.CcAddress.Clear()

        '    mail.ToAddress.Add(mailTo)
        '    mail.Send()
        '    Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail successfully!')</script>")
        '    Return True
        'Catch ex As Exception
        '    Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail error!')</script>")
        '    Return False
        'End Try
    End Function

    Protected Sub btnSendmail_Click(sender As Object, e As EventArgs) Handles btnSendmail.Click
        If (SendEmail() = True) Then
            txtEmail.Value = ""
            txtSubject.Value = ""
            txtMessage.Value = ""
        End If
    End Sub
End Class