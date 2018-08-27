<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sendmail.aspx.vb" Inherits="Survey.sendmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Send Mail</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server">

        <div style="background-color: inherit; height: 100px">
            <div id="userInfo" class="sidenav">
                <label id="info">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px" />
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-8" style="padding: 20px; background-color: white; min-height: 100%; width: 70%;">
            <div>
                <h1 style="text-align: center;">Send Email</h1>
            </div>
            <p style="text-align: center;" id="aboutSurvey" runat="server"></p>

            <div class=" form-group">

                <div class="container">
                    <label>To</label>
                    <input runat="server" id="txtTo" class="form-control" type="text" name="To" autocomplete="off"/>
                </div>
                <div class="container">
                    <label>Subject</label>
                    <input runat="server" id="txtSubject" class="form-control" type="text" name="Subject" autocomplete="off"/>
                </div>
                <div class="container">
                    <label>Message</label>
                    <textarea runat="server" class="form-control" name="message" id="txtMessage" cols="30" rows="15"></textarea>
                    <br/>
                    <asp:Button runat="server" ID="btnSend" type="submit" class="btn btn-success" Text="Send"></asp:Button>
                </div>

            </div>

            <div id="footer" style="background-color: white; height: 70px;">
                <br />
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                    Style="float: left;" Text="Back" />
                <asp:Button runat="server" ID="btnLogout" type="button" class="btn btn-danger"
                    Style="float: right" Text="Logout" OnClick="btnLogout_Click"/>
            </div>

        </div>
    </form>

</body>

</html>
