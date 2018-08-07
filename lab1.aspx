<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="lab1.aspx.vb" Inherits="Lab1.lab1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="stylesheet.css">
</head>
<body>
    <form id="form1" runat="server">
        <p>My mother has <span style="color:blue">blue</span> eyes.</p>
        <iframe src="https://www.kapook.com"></iframe> 
        <div class="" style="background-color: #ffffaa;"><p> hello </p>
            
        </div>
         <h1 class="title">RESUME</h1>
            <hr>
            <p>
                <img src="images/resume-bg.jpg" alt=""  width="100%">
            </p>
            <h1 class="subject">Suriyong Thaphiang</h1>
             <blockquote class="slogan"><strong>Slogan : <q>Trust me I am a Developer.</q></strong></blockquote>
            <h2>Contact</h2>
            <ul>
                <li>Email: suriyongt@ayt.hanabk.th.com</li>
                <li>Line @: hansomeboyGoodJob</li>
                <li>Website:
                    <a href="http://hanagroup.com">hanagroup.com</a>
                </li>
            </ul>
            <h2>Graduated</h2>
            <ol>
                <li>Srivikorn School</li>
                <li>Electrical engineering at KMITL (Bachelor)</li>
                <li>Computer engineering at KMITL (Master)</li>
            </ol>
            <h2>Skill</h2>
            <ul>
                <li  class="more-space">Programming Language: HTML, CSS, Javascript, PHP, Swift, Java</li>
                <li  class="more-space">Database: MongoDB, MySQL</li>
            </ul>   

             <h2 id="experience">Experience</h2>
            <table border="1" width="100%" class="tb-experience">
                <tr>
                    <td class="year">2010</td>
                    <td class="project">Call center project at KLeasing</td>
                </tr>
                <tr>
                    <td class="year">2011 - 2012</td>
                    <td class="project">Dtac Rewards at Dtac</td>
                </tr>
                <tr>
                    <td class="year">2013 - 2014</td>
                    <td class="project">Number Port Gateway at AIS</td>
                </tr>
                <tr>
                    <td class="year">2015 - present</td>
                    <td class="project">Others</td>
                </tr>
            </table>
    </form>
</body>
</html>
