<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="labBS1.aspx.vb" Inherits="Lab1.lab2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
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

    <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="d-block w-100" src="images/1.jpg" alt="First slide">
        <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Singapore</h2>
                </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="images/2.jpg" alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Thailand</h2>
                </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src="images/3.jpg" alt="Third slide">
        <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Japan</h2>
                </div>
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>

<%--    <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
            <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
        </ol>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img class="d-block w-100" src="images/3.jpg" alt="First slide">
                <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Singapore</h2>
                </div>
            </div>
            <div class="carousel-item">
                <img class="d-block w-100" src="images/2.jpg" alt="Second slide">
                <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Japan</h2>
                </div>
            </div>
            <div class="carousel-item">
                <img class="d-block w-100" src="images/1.jpg" alt="Third slide">
                <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Thailand</h2>
                </div>
            </div>
            <div class="carousel-item">
                <img class="d-block w-100" src="images/4.jpg" alt="Third slide">
                <div class="carousel-caption d-none d-md-block">
                    <h2 class="text-uppercase">Iceland</h2>
                </div>
            </div>
        </div>
        <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>--%>

    <div class="container">
        <h1 class="text-center" style="margin-top: 30px; margin-bottom: 30px;">The Wonderful World Travel</h1>
        <p class="lead text-justify">การเดินทางของคุณจะไม่น่าเบื่ออีกต่อไปเมื่อไปกับ The Wonderful World Travel พบกับโปรแกรมทัวร์ที่ไม่เหมือนใคร การเดินทางไปจักรวาล
            ไต่ภูเขาหินที่สูงที่สุดในโลก กินปลาใต้ทะเลลึก และ ถ้าคุณเป็นคนชอบทัวร์ราคาประหยัดเพิ่ม Line @ ของเราเลย เพื่อรับการแจ้งเตือนทัวร์ฟ้าผ่า
            และ ไฟไหม้ของเราทุกวัน
        </p>
        <hr>
        <h2 style="margin-top: 30px; margin-bottom: 30px;">Thailand</h2>
        <div class="row">
            <div class="col-md-4" style="margin-bottom: 20px;">
                <div class="card">
                    <img class="card-img-top" src="images/5.jpg" alt="Card image cap">
                    <div class="card-body">
                        <p class="card-text"><a href="labBS2.aspx">Chiang Mai</a></p>
                    </div>
                </div>
            </div>
            <div class="col-md-4" style="margin-bottom: 20px;">
                <div class="card">
                    <img class="card-img-top" src="images/6.jpg" alt="Card image cap">
                    <div class="card-body">
                        <p class="card-text">Island</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4" style="margin-bottom: 20px;">
                <div class="card">
                    <img class="card-img-top" src="images/8.jpg" alt="Card image cap">
                    <div class="card-body">
                        <p class="card-text">Culture</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js"></script>
</body>
</html>
