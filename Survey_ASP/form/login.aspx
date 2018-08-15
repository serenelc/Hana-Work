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

    
        <!-- Custom styles for this template -->
        <link href="../css/login.css" rel="stylesheet">
        <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">

    </head>
    
    <%--Want the form to be wider--%>
    <body style="background: linear-gradient(#a9c5f2, #619af4);">
        <form runat="server" class="form-signin" method="post">
            <div data-gr-c-s-loaded="true" class="container text-center" style="padding: 20px; background-color: white; min-height: 100%;">
                <img src="../images/hana-simple-logo.jpg" />
                <p></p>
                <label for="text" class="sr-only">En</label>
                <input type="text" id="inputEn" runat="server" class="form-control" placeholder="En" required autofocus>
                <p></p>
                <label for="inputPassword" class="sr-only">Password</label>
                <input type="password" id="inputPassword" runat="server" class="form-control" placeholder="Password" required>
                <br>
                <asp:button id="btnLogin" 
                    class="btn btn-lg btn-primary btn-block"                     
                    type="button" 
                    Text="Login" 
                    runat="server" 
                    onclick="btnLogin_Click" />
                <button runat="server"  class="btn btn-lg btn-secondary btn-block">
                    <a href="index.aspx" style="text-decoration: none; color: white;">Cancel</a>
                </button>
                <p class="mt-5 mb-3 text-muted">&copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.</p>
            </div>         
      </form>

  </body>
  
  <script src="../node_modules/jquery/dist/jquery.min.js"></script>
  <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
  <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

</html>
