<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Survey.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Welcome</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 50px;"></div>
        <%--Want background imageto repeat on the sides but not top and bottom--%>
        <div class="container align-items-center" style="background-image:url(../images/bg.jpeg); background-color: white; height: 736px;">
                <div class="mx-auto text-center" style="padding-top: 100px;">
                    <h1 class="mx-auto text-uppercase" style="color: white; font-weight:bold;">Employee Satisfaction Surveys</h1>
                    <asp:button runat="server" id="btnLogin" type="button" class="btn btn-primary"
                    text="Login" Height="47px" Width="92px"/>
                </div>
        </div>
        <div style="text-align: center; margin-bottom: 10px;"> 
            <p class="mt-5 mb-3 text-muted">&copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.</p>
            <br />
        </div>
        

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
   </form>
</body>
</html>
