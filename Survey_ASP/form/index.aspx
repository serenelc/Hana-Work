﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Survey.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Welcome</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body>
    <%--<form id="form1"  runat="server">
        <div style="height: 50px;"></div>--%>
        <div class="container align-items-center" style="background-image:url(../images/bg.jpeg); margin-top: 20px; background-color: white; height: 700px;">
                <div class="mx-auto text-center" style="padding-top: 100px;">
                    <h1 class="mx-auto text-uppercase" style="color: white; font-weight:bold;">Employee Satisfaction Surveys</h1>
<%--                    <asp:button runat="server" id="btnLogin" type="button" class="btn btn-primary"
                    text="Login" Height="47px" Width="92px"
                    data-toggle="modal" data-target="#exampleModal"/>--%>
                    <p class="card-text" style="" ><a href="" data-toggle="modal" data-target="#exampleModal">Login</a></p>
                </div>
        </div>
        <div style="text-align: center;"> 
            <p class="mt-5 mb-3 text-muted">&copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.</p>
            <br />
        </div>
    <%--</form>--%>

        <!--login menu-->
        <div class="modal fade " id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Login</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                    <form id="form2" class="form-signin " runat="server">                   
                        <label for="text" class="sr-only">En</label>
                        <input type="text" id="inputEn" runat="server" class="form-control" placeholder="En" required autofocus>
                        <label for="inputPassword" class="sr-only">Password</label>
                        <input type="password" id="inputPassword" runat="server" class="form-control" placeholder="Password" required>
                        <br>
                        <asp:button id="btnLogin" 
                            class="btn btn-lg btn-primary btn-block"                     
                            type="button" 
                            Text="Login" 
                            runat="server" 
                            />
                        <button class="btn btn-lg btn-secondary btn-block" type="reset">Cancel</button>
                        <p class="mt-5 mb-3 text-muted">&copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.</p>
                    </form>
              </div>
      
            </div>
          </div>
        </div>


    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
 
</body>
</html>