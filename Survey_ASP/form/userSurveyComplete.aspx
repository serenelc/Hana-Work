<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userSurveyComplete.aspx.vb" Inherits="Survey.userSurveyComplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Survey Completed</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server">

        <div style="background-color: inherit; height: 100px">
            <div id="userInfo" class="sidenav">
                <label id="info" style="visibility: hidden;">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px" />
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-8" style="padding: 20px; background-color: white; min-height: 100%; width: 70%;">
            <div>
                <h1 style="text-align: center;">Survey Completed</h1>
            </div>
            <p style="text-align: center;">Your responses have been successfully submitted. Thank you for completing the survey.</p>
            <img style="margin-left: auto; margin-right: auto; display: block; width: 80%" src="../images/thankyou.jpg" />

            <div id="footer" style="background-color: white; height: 70px;">
                <br />
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                    Style="float: left;" Text="Complete Another Survey" />
                <asp:Button runat="server" ID="btnLogout" type="button" class="btn btn-danger"
                    Style="float: right; visibility: hidden;" Text="Logout" OnClick="btnLogout_Click" OnClientClick="javascript: if (!OpenTaskDialogLogout()) { return false; };"/>
            </div>

        </div>


        <%--Script to allow use of dialog box in server--%>
        <script>
            function OpenTaskDialogLogout() {
                return confirm("Are you sure you want to logout?");
            }
        </script>

          <%--Script to check if user had to log in to get to this survey--%>
        <script>
            function isAdmin() {
                    let uType = "<%=Session("UserType").ToString()%>";
                    let btnLogout = document.getElementById("btnLogout")
                    if (uType = "") {
                        btnLogout.style = "visibility: hidden;";
                        document.getElementById("info").style = "visibility: hidden;";
                    }
                    else {
                        btnLogout.style = "visibility: visible;";
                        document.getElementById("info").style = "visibility: visible;";
                    }
            }
            isAdmin();
        </script>

        <script src="../node_modules/jquery/dist/jquery.min.js"></script>
        <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
        <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
    </form>

</body>

</html>
