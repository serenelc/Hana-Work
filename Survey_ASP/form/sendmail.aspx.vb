Imports System.Data.SqlClient
Imports MISClassLibrary

Public Class sendmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If
        'SendEmail(16)
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
    Function SendEmail(xsubjectId) As Boolean
        Dim xSql = " Select * from  surveyMaster where subjectId = " + xsubjectId.ToString
        Dim dt As DataTable = GetData(xSql)
        Dim mail As New EmailController(SystemInfo.SystemList.QLR)

        Dim mailTo As String = "suriyongt@ayt.hanabk.th.com"
        Dim mailTo2 As String = "SereneC@ayt.hanabk.th.com"
        Dim mailSubject As String = "Survey hana online"
        Dim mailMessage As String = ""

        Try
            mail.Subject = mailSubject
            Dim strbody = ""
            strbody &= "<br/><B>SURVEY HANA ONLINE</B>"
            strbody &= "<br/><B>Subject Id:</B> " & dt.Rows(0)("subjectId").ToString()
            strbody &= "<br/><B>Subject Name:</B> " & dt.Rows(0)("subjectName")
            strbody &= "<br/><B>Open Date:</B> " & dt.Rows(0)("openDate").ToString()
            strbody &= "<br/><a href='http://localhost:55240/form/index.aspx'><img src='\\hsadols\Applications\MIS_App\CAR_3D_ONLINE\PICS\mailLogin.jpg'></a>"
            strbody &= "<br/> REMARK:THIS EMAIL HAS BEEN SENT AUTOMATICALLY. DO NOT REPLY TO SENDER."


            mail.Body = strbody

            mail.BccAddress.Clear()
            mail.ToAddress.Clear()
            mail.CcAddress.Clear()

            mail.ToAddress.Add(mailTo)
            mail.ToAddress.Add(mailTo2)
            mail.Send()
            Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail successfully!')</script>")
            Return True
        Catch ex As Exception
            Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail error!')</script>")
            Return False
        End Try
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminHome.aspx")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Response.Redirect("index.aspx")
    End Sub
End Class