<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminHome.aspx.vb" Inherits="Survey.adminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Home</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet">
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server" method="get">

        <div style="background-color:inherit; height: 100px" >
            <div id="userInfo" class="sidenav">
                <label id= "info">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px"/>
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float:left;" />
        </div>
        
        <div id ="main" class="container col-8" style="padding: 20px; background-color:white; width: 70%;">
            
            <div>
                <h1 style="text-align: center;"> Admin Home </h1>
                <br>
            </div>

            <div>
                <div>
                    <a href="adminCreate.aspx" style="text-decoration: none; color: white; display: block;">
                        <button type="button" class="btn btn-secondary btn-lg btn-block"> Create a survey</button>
                    </a>
                    <br>
                </div>
                
                <div>
                     <a href="adminAllSurveys.aspx" style="text-decoration: none; color: white; display: block;">
                        <button type="button" class="btn btn-secondary btn-lg btn-block">All Surveys</button>
                     </a>
                    <br>
                </div>

                <div>
                    <a href="userSurveyList.aspx" style="text-decoration: none; color: white; display: block;">
                        <button type="button" class="btn btn-secondary btn-lg btn-block">Answer a survey</button>
                    </a>
                    <br>
                </div>
                  
            </div>

            <div id = "footer" style="background-color: white; height: 70px;">
                    <br>
                    <asp:button runat="server" id="btnLogout" type="button" class="btn btn-danger"
                    style="float: right" Text="Logout" onclick="btnLogout_Click"/>
             </div>
        
        </div>

        <div style="background-color:inherit; height: 100px" >
                 
        </div>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
</body>
</html>
