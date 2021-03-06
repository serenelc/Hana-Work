﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userAnswer.aspx.vb" Inherits="Survey.userAnswer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Survey</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server" method="post">

        <div style="background-color: inherit; height: 100px">
            <div id="userInfo" class="sidenav">
                <label id="info" style="visibility: hidden;">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px" />
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-8" style="padding: 20px; background-color: white; width: 70%;">

            <div>
                <asp:Label ID="title" runat="server"></asp:Label>
                <asp:Label ID="description" runat="server"></asp:Label>
            </div>

            <%--Data got from the database will go into the frm section--%>
            <div>
                <asp:Label ID="frm" runat="server"></asp:Label>
            </div>

            <div id="footer" style="background-color: white; height: 70px;">
                <br>
                <asp:Button runat="server" ID="btnSave" type="button" class="btn btn-success"
                    Style="float: right" Text="Save" CommandArgument='<%# Eval("subjectId") %>' OnClientClick="javascript: if (!OpenTaskDialogSave()) { return false; };" />
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                    Style="float: left;" Text="Back" onclick="btnBack_Click"/>
            </div>

        </div>

        <%--Script to allow use of dialog box in server--%>
        <script>
            function OpenTaskDialogSave() {
                return confirm("Are you sure you have finished answering the survey?");
            }
        </script>

        <%--Script to check if user has logged in to take this survey--%>
        <script>
            function isAdmin() {
                    let uType = "<%=Session("UserType").ToString()%>";
                    if ("" == uType) {
                        //Do nothing
                    }
                    else {
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
