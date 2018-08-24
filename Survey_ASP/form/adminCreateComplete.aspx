<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminCreateComplete.aspx.vb" Inherits="Survey.adminCreateComplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Survey Created</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet"/>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css"/>
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server">

        <div style="background-color:inherit; height: 100px" >
            <div id="userInfo" class="sidenav">
                <label id= "info">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px"/>
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float:left;" />
        </div>

         <div id ="main" class="container col-8" style="padding: 20px; background-color:white; min-height: 100%; width: 70%;">
             <div>
                <h1 style="text-align: center;">Survey Created</h1>
            </div>
             <p style="text-align: center;"> Your survey has been successfully created. It will now be available for users to complete. 
                 Click <a href="adminAllSurveys.aspx" style="font-weight: bold;">HERE</a> to see a list of all surveys that have been created. There you will have the option
                 to send an email informing employees that the survey is now open, view results and manually open/close surveys.
             </p>
             <img style="margin-left: auto; margin-right: auto; display: block; width: 70%" src="../images/success.jpg"/>

             <div id = "footer" style="background-color: white; height: 70px;">
                <br/>
                <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning"
                    style="float: left;" Text="Back"/>
                 <asp:button runat="server" id="btnLogout" type="button" class="btn btn-danger"
                    style="float: right" Text="Logout" onclick="btnLogout_Click" OnClientClick="return confirmLogout()"/>
             </div>
        
        </div>
    </form>

     <%--Script to confirm logout--%>
     <script>
         function confirmLogout() {
           return confirm("Are you sure you would like to logout?");
         }
     </script>

</body>

</html>
