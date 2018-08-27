Imports System.Data.SqlClient
Imports MISClassLibrary

Public Class sendmail
    Inherits System.Web.UI.Page
    Dim xsubjectId = 0
    Dim surveySql
    Dim dt As DataTable
    Dim surveyName As String = ""
    Dim surveyDesc As String = ""
    Dim surveyOpen As Date
    Dim surveyClose As Date


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("En") Is Nothing Then
            Response.Redirect("index.aspx")
        End If

        xsubjectId = Request.QueryString("subjectId")
        surveySql = "Select * from surveyMaster where subjectId = " + xsubjectId.ToString
        dt = GetData(surveySql)
        For Each r In dt.Rows
            surveyName = r("subjectName")
            surveyDesc = r("subjectDetail")
            surveyOpen = r("openDate")
            surveyClose = r("closeDate")
        Next

        Dim info As String = "Survey Name: " + surveyName + ", Survey Description: " + surveyDesc + ", Survey Open Date: " + surveyOpen + ", Survey Close Date: " + surveyClose + "."
        aboutSurvey.InnerText = info
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

        Dim mail As New EmailController(SystemInfo.SystemList.QLR)

        Dim mailTo As String = txtTo.Value.Trim
        Dim mailSubject As String = txtSubject.Value.Trim
        Dim mailMessage As String = txtMessage.Value.Trim

        Try
            mail.Subject = mailSubject
            Dim strbody = ""
            strbody &= "<br/><B>SURVEY HANA ONLINE</B>"
            strbody &= "<br/><B>Subject Id:</B> " & dt.Rows(0)("subjectId").ToString()
            strbody &= "<br/><B>Subject Name:</B> " & dt.Rows(0)("subjectName")
            strbody &= "<br/><B>Open Date:</B> " & dt.Rows(0)("openDate").ToString()
            strbody &= "<br/>" & mailMessage & "<br/>"
            strbody &= "<br/><a href='http://10.12.12.101/surveyOnline/form/index.aspx'><img src='\\hsadols\Applications\MIS_App\CAR_3D_ONLINE\PICS\mailLogin.jpg'></a>"
            strbody &= "<br/> REMARK:THIS EMAIL HAS BEEN SENT AUTOMATICALLY. DO NOT REPLY TO SENDER."

            mail.Body = strbody

            mail.BccAddress.Clear()
            mail.ToAddress.Clear()
            mail.CcAddress.Clear()

            Dim strArrEmail() = mailTo.Split(";")
            For i As Integer = 0 To strArrEmail.Count - 1
                mail.ToAddress.Add(strArrEmail(i).ToString())
            Next
            mail.Send()
            Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail successfully!')</script>")
            Return True
        Catch ex As Exception
            Response.Write("<script LANGUAGE='JavaScript' >alert('Send mail error!')</script>")
            Return False
        End Try
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("adminAllSurveys.aspx")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        If MsgBox("Are you sure you want to logout?", vbQuestion + vbYesNo) = vbYes Then
            Response.Redirect("index.aspx")
        End If
    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        If MsgBox("Are you sure you want to send this email?", vbQuestion + vbYesNo) = vbYes Then
            If SendEmail(xsubjectId) = True Then
                Response.Redirect("adminAllSurveys.aspx")
            End If
        End If
    End Sub
End Class