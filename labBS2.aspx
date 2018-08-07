﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="labBS2.aspx.vb" Inherits="Lab1.labBS2" %>

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
    <title>Product</title>
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
    <div class="container" style="margin-top: 30px;">
        <div class="row">
            <div class="col-md-5">
                <div>
                    <img class="img-fluid" src="images/thumbnails/5.jpg" alt="Card image cap">
                </div>
                <h3 style="margin-top: 20px; margin-bottom: 20px;">Location</h3>
                <ul class="list-group">
                    <li class="list-group-item">Wat Phra That Doi Suthep</li>
                    <li class="list-group-item">Wat Phra Singh</li>
                    <li class="list-group-item">Wat Chedi Luang</li>
                    <li class="list-group-item">Talat Warorot</li>
                </ul>
            </div>
            <div class="col-md-7">
                <h1 style="margin-top: 10px;">Chiang Mai</h1>
                <hr>
                <p class="text-justify">Chiang Mai is a city in mountainous northern Thailand. Founded in 1296, it was capital of the independent
                    Lanna Kingdom until 1558. Its Old City area still retains vestiges of walls and moats from its history
                    as a cultural and religious center. It’s also home to hundreds of elaborate Buddhist temples, including
                    14th-century Wat Phra Singh and 15th-century Wat Chedi Luang, adorned with carved serpents.</p>
                <button class="btn btn-info btn-block" data-toggle="modal"   data-target="#modal">Booking</button>
            </div>
        </div>
    </div>

    <div class="modal" id="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Booking</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Your booking already success.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js"></script>
</body>
</body>
</html>
