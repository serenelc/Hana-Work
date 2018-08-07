<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="labBS4.aspx.vb" Inherits="Lab1.labBS4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Contact</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css">
</head>

<body>
    <!-- NAVIGATOR -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <a class="navbar-brand" href="#"></a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup"
                aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                    <a class="nav-item nav-link text-uppercase active" href="labBS1.aspx">Home</a>
                    <a class="nav-item nav-link text-uppercase " href="labBS3.aspx">About</a>
                    <a class="nav-item nav-link text-uppercase " href="labBS4.aspx">Contact</a>
                </div>
            </div>
        </div>
    </nav>

    <!-- BODY -->
    <div class="container">
        <div class="text-center">
            <h1 class="text-uppercase">Contact</h1>
        </div>
        <div class="row justify-content-center">
            <div class="col-12 col-md-6 ">
                    <hr>
                <form>
                    <div class="form-group">
                        <label>Your name</label>
                        <input type="text" class="form-control" id="name" >
                    </div>
                    <div class="form-group">
                        <label>Email</label>
                        <input type="text" class="form-control" id="email" >
                    </div>
                    <div class="form-group">
                        <label>Subject</label>
                        <input type="text" class="form-control" id="subject" >
                    </div>
                    <div class="form-group">
                        <label>Message</label>
                        <textarea class="form-control" name="message" id="message" cols="30" rows="5"></textarea>
                    </div>
                    <button class="btn btn-block btn-primary">Submit</button>
                </form>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js"></script>
</body>
</body>
</html>
