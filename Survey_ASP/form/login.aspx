<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Survey.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../../../favicon.ico">
    <title>Login</title>

    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;" data-gr-c-s-loaded="true" class="text-center">

    <form class="form-signin" runat="server" method="post">

        <div style="background-color: inherit; height: 100px">
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-6" style="padding: 20px; background-color: white; min-height: 100%;">

            <div>
                <h1 style="text-align: center;">Please Login To Complete This Survey</h1>
            </div>
            </br>
            <label for="text" class="sr-only">En</label>
            <input type="text" id="inputEn" runat="server" class="form-control" placeholder="En" required autofocus autocomplete="off">
            <label for="inputPassword" class="sr-only">Password</label>
            <input type="password" id="inputPassword" runat="server" class="form-control" placeholder="Password" required>
            <br>
            <asp:Button ID="btnLogin"
                class="btn btn-lg btn-primary btn-block"
                type="button"
                Text="Login"
                runat="server"
                OnClick="btnLogin_Click"/>
            <button runat="server" class="btn btn-lg btn-secondary btn-block" type="reset">Clear</button>
 
            <div id="footer" style="background-color: white; height: 70px;">
                <br />
                <a href="userSurveyList.aspx" class="btn btn-warning" role="button" id="btnBack" aria-pressed="true" style="float: left;" target="">Back</a>
            </div>

        </div>


    </form>
</body>


<script src="../node_modules/jquery/dist/jquery.min.js"></script>
<script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
<script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
</html>
