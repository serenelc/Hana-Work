<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminHome.aspx.vb" Inherits="Survey.adminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Home</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>

<body style="background-color: #a9c5f2;">

    <form id="form1" runat="server"   method="get">

        <div style="background-color:#619af4; height: 100px" >
                 <%--<button type="button" class="btn btn-outline-secondary">Logout</button>--%> 
        </div>
        
        <div id ="main" class="container" style="padding: 20px; background-color:white; min-height: 100%;">
            
            <div>
                <h1 style="text-align: center;"> Admin Home </h1>
                <br>
            </div>

            <div>
                    <button type="button" class="btn btn-secondary btn-lg btn-block">
                        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                        <a href="adminCreate.aspx" style="text-decoration: none; color: white;">Create a survey</a>
                    </button>
                    <br>
                    <button type="button" class="btn btn-secondary btn-lg btn-block">
                        <a href="adminStatsList.aspx" style="text-decoration: none; color: white;">Survey Statistics</a>
                    </button>
                    <br>
                    <button type="button" class="btn btn-secondary btn-lg btn-block">
                        <a href="userSurveyList.aspx" style="text-decoration: none; color: white;">Answer a survey</a>
                    </button>
                    <br>
            </div>

            <div id = "footer" style="background-color: white; height: 70px;">
                    <br>
                    <asp:button runat="server" id="btnLogout" type="button" class="btn btn-danger"
                    style="float: right" Text="Logout" onclick="btnLogout_Click" OnClientClick="return confirmLogout()"/>
                    <%-- <asp:button runat="server" id="btnDelete" type="button" class="btn btn-danger">Delete</asp:button> --%>
                </div>
        
        </div>

        <script>
            function confirmLogout() {
                return confirm("Are you sure you would like to logout?");
            }
        </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
</body>
</html>
