<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Survey.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Welcome</title>
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body>

    <div class="container align-items-center" style="background-image: url(images/bg.jpeg); margin-top: 20px; background-color: white; height: 700px;">
        <div class="mx-auto text-center" style="padding-top: 100px;">
            <h1 class="mx-auto text-uppercase" style="color: white; font-weight: bold;">Employee Satisfaction Surveys</h1>
            <a href="/form/userSurveyList.aspx" class="btn btn-success" role="button" aria-pressed="true">Go</a>
            <br />
            <br />
            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">Admin Login</button>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

        </div>

        <%--            <div style="color: white; float: left;">
                <a href="../docs/SurveyOnlineManual.docx" class="btn btn-secondary active" role="button" aria-pressed="true">Manual</a>
                <a href="../docs/ProcessFlow.jpg" class="btn btn-secondary active" role="button" aria-pressed="true" target="_blank">Process Flow</a>
                <div style="color: white; float:right;">&copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.</div>
            </div>--%>

        <div class="container">
            <div class="row justify-content-between">
                <div class="col-4 mt-5 md-3">
                    <a href="../docs/SurveyOnlineManual.docx" class="btn btn-warning" role="button" aria-pressed="true">Manual</a>
                    <a href="../docs/ProcessFlow.jpg" class="btn btn-warning" role="button" aria-pressed="true" target="_blank">Process Flow</a>
                </div>
                <div class="col-6 mt-5 md-3" style="color: white; float: right; text-align: right;">
                    &copy; 2018 Hana Semiconductor (Ayutthaya)  Co., Ltd. Company, Inc.
                    <br /> Created by: Serene Chongtrakul & Suriyong Thaphiang (Tel: 726)
                </div>
            </div>
        </div>

        <br />
    </div>



    <!--login menu-->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                        <asp:Button ID="btnLogin"
                            class="btn btn-lg btn-primary btn-block"
                            type="button"
                            Text="Login"
                            runat="server" />
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
