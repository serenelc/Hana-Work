Imports System.Data.SqlClient

Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Function ConnectDB(ConnectionString As String, Sql As String)
        Dim MyDataTable As New DataTable
        Try
            'make a SqlConnection using the supplied ConnectionString 
            Dim MySqlConnection As New SqlConnection(ConnectionString)
            Using MySqlConnection
                'make a query using the supplied Sql
                Dim MySqlCommand As SqlCommand = New SqlCommand(Sql, MySqlConnection)

                'open the connection
                MySqlConnection.Open()

                'create a DataReader and execute the SqlCommand
                Dim MyDataReader As SqlDataReader = MySqlCommand.ExecuteReader()

                'load the reader into the datatable
                MyDataTable.Load(MyDataReader)

                'clean up
                MyDataReader.Close()
            End Using

            'return the datatable
            Return MyDataTable
        Catch ex As Exception
            Return MyDataTable
        End Try

    End Function
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim strConnString, strSQLHanaone As String
        strConnString = My.Settings.ConnStringDatabaseSurvey

        'Dim intNumRows As Integer
        Dim en As String = inputEn.Value
        Dim pwd As String = inputPassword.Value
        en = en.PadLeft(6, "0")
        Dim dtRight, dtpw, dtCheckHanaOne As DataTable

        strSQLHanaone = "select  * from  HanaOne..hosemployee h right outer join   [hanadata].dbo.EMP_PASS t on h.EmpNo=t.FempNo "
        strSQLHanaone = strSQLHanaone + " where h.EmpStatus <> 'D'  and EmpNo='" & inputEn.Value & "' "
        'Function connect database
        dtCheckHanaOne = ConnectDB(My.Settings.ConnStringDatabaseSurvey, strSQLHanaone)
        If dtCheckHanaOne.Rows.Count > 0 Then

            strSQLHanaone = "Select HanaOne.dbo.DecryptPW((select idcard from HanaOne..hosemployee where empno='" & inputEn.Value & "') "
            strSQLHanaone = strSQLHanaone + " ,(Select pw from HanaOne..hosemployee where empno='" & inputEn.Value & "'))PW "
            dtpw = ConnectDB(My.Settings.ConnStringDatabaseSurvey, strSQLHanaone)

            If dtpw.Rows.Count > 0 And dtpw.Rows(0)("PW").ToString <> "" Then
                If dtpw.Rows(0)("PW").ToString = pwd Then
                    Session("En") = en
                    Session("Name") = dtCheckHanaOne.Rows(0)("EngName") & " " & dtCheckHanaOne.Rows(0)("EngSurname")

                    strSQLHanaone = "Select * from surveyUsers where userEn='" & inputEn.Value & "'"
                    dtRight = ConnectDB(My.Settings.ConnStringDatabaseSurvey, strSQLHanaone)


                    If (dtRight IsNot Nothing) And (dtRight.Rows.Count > 0) Then
                        Session("UserType") = "ADMIN"
                        Response.Redirect("adminHome.aspx")
                    Else
                        Session("UserType") = "USER"
                        Response.Redirect("userSurveyList.aspx")
                    End If

                    'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Connection sucessful')", True)


                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Password incorrect!')", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Password incorrect!')", True)
            End If


        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('User en Incorrect!')", True)
        End If

        '***********************************************
    End Sub
End Class