<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userSurveyComplete.aspx.vb" Inherits="Survey.userSurveyComplete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Survey Completed</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet">
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
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
             <p>Survey Complete</p>
             
             <div id = "footer" style="background-color: white; height: 70px;">
                <br>
                <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning"
                    style="float: left;" Text="Back"/>
             </div>
        
        </div>
    </form>
</bod>

</html>
