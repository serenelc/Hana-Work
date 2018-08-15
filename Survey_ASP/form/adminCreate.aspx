<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminCreate.aspx.vb" Inherits="Survey.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Create</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet">
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>

    <%--Need background colour to extend to bottom of page--%>

<body style="background: linear-gradient(#a9c5f2, #619af4);">
    
    <form id="form1" runat="server" method="get" >

    <div style="background-color:inherit; height: 100px" >
        <div id="userInfo" class="sidenav">
            <label id= "info"><%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%></label> 
        </div>
        <img src="../images/HANA-Logo-BW.png" style="float:left;" />
    </div>
        
    <div id ="main" class="container" style="padding: 20px; background-color:white;">
        
        <div>
            <h1 style="text-align: center;"> Create A Survey </h1>
        </div>
        
        <form>
            <div class="form-group">
                <label ><h3>Title</h3></label>
                <input runat="server" type="text" class="form-control" id="txtTitle" style="font-weight:bold;" placeholder="Survey Title">
            </div>

            <div class="form-group">
                <label>Description</label>
                <textarea class="form-control" id="txtDesc" rows="2" 
                placeholder="Short description about your survey" runat="server"></textarea>
            </div>

            <hr>

            <button type="button" class="btn btn-info"  id="newSection" 
            onclick="addSection();"
            >Add a Section</button>
            <button type="button" class="btn btn-info"  id="addQ2" 
            onclick="addQuestion();"
            >Add a Question</button>
            
            <div id = "save" style="background-color: white; height: 70px;">
                <br>
                <asp:button runat="server" id="btnSave" type="button" class="btn btn-success"
                style="float: right" text="Save" onclick="btnSave_Click" OnClientClick="return confirmSave()"/>
                <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning" Text="Back"/>
                    
            </div>
            
        
        
    </div>
        
        <div style="background-color:inherit; height: 100px" >
                 
        </div>

        </form>

    <%--Script to confirm save--%>
    <script>
        function confirmSave() {
            return confirm("Are you sure you have finished creating the survey?");
        }
    </script>
   

     <%--Script to add a section/sub topic--%> 
    <script>

        var countSec = 0;

        function addSection() {

            let divSection = document.createElement("div");
            divSection.className = "container";
            divSection.id = "divSection_id" + countSec;
            divSection.name = "divSection_name" + countSec;
            
            let divSecTitle = document.createElement("input");
            divSecTitle.className = "form-control-lg";
            divSecTitle.type = "text";
            divSecTitle.placeholder = "Section Title";           
            divSecTitle.id = "divSecTitle_id" + countSec;
            divSecTitle.name = "divSecTitle_name" + countSec ;
            divSecTitle.value = "<%=divSecTitle_val%>" ;
            divSecTitle.style = "margin-bottom: 20px; font-weight: bold; float: center; border-right: none; border-left: none; border-top: none; border-bottom-width: medium; border-bottom-color: black; width: 50%;";
            
            divSection.appendChild(divSecTitle, document.createElement("br"));
            newSection.before(divSection);
            console.log("adding a section");
            countSec = parseInt(countSec) + parseInt(1);
        }

    </script>

     <%--Script to add a question and choose an answer type--%> 
    <script>

        var countQ = 100;  
        function addQuestion() {

            var newCountQ = countQ.toString().slice(-2);
            
            let divGroup= document.createElement("div"); 
            divGroup.id = "divGroupQ_id" + newCountQ;  
            divGroup.name = "divGroupQ_name" + newCountQ;  
            divGroup.className = "input-group";
            divGroup.innerHTML = '<input type="text" class="form-control" style="font-weight:bold;" placeholder="Question">';           

            let divbutton= document.createElement("div");
            divbutton.id = "divbuttonQ_id" + newCountQ;  
            divbutton.name = "divbuttonQ_name" + newCountQ;  
            divbutton.className = "input-group-append";
            divbutton.innerHTML = '<button class="btn btn-outline-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Answer Type</button>';        
 
            let divdropdown= document.createElement("div");
            divdropdown.id = "divdropdownQ_id" + newCountQ;  
            divdropdown.name = "divdropdownQ_name" + newCountQ;
            divdropdown.className ="dropdown-menu";
            divdropdown.innerHTML = '<a class="dropdown-item" href="javascript:makeRadBut()">Radio Button</a>'; 
            let adropdown2= document.createElement("a");
            adropdown2.id = "adropdown2Q_id" + newCountQ;
            adropdown2.name = "adropdown2Q_name" + newCountQ;
            adropdown2.innerHTML = '<a class="dropdown-item" href="javascript:makeGrid()">Multiple Choice Grid</a>';   
            let adropdown3= document.createElement("a");
            adropdown3.id = "adropdown3Q_id" + newCountQ; 
            adropdown3.name = "adropdown3Q_name" + newCountQ;
            adropdown3.innerHTML = '<a class="dropdown-item" href="javascript:makeShortAns()">Short Answer</a>';   
            divdropdown.appendChild(adropdown2).appendChild(adropdown3);
                         
            divGroup.appendChild(divbutton).appendChild(divdropdown);
            
            let buttonClose= document.createElement('button');   
            buttonClose.id = "buttonCloseQ_id" + newCountQ;   
            buttonClose.name =  "buttonCloseQ_name" + newCountQ;  
            buttonClose.innerHTML = '<span aria-hidden="true">&times;</span> ';
            divGroup.appendChild(buttonClose);
            // divGroup.appendchild();
            let br = document.createElement('br');
            br.id = "break_id" + newCountQ;
            br.name = "break_name" + newCountQ;
            newSection.before(divGroup, br);
            buttonClose.onclick = removeElement;

            countQ = parseInt(countQ) + parseInt(1);
            console.log("Adding a question");
        }
         
    </script>

<%--  
There is also the issue where if you add multiple questions and then remove them
without adding an answer type, when you try to delete the question, the answer box 
won't delete because its id number won't match its question number. --%>

     <%--TODO: Script to remove a question--%> 
    <script>
        var called = false;
        var idCount = 0; //GLOBAL VARIABLE
        function removeElement() {
            var idNum = this.id.slice(-2);
            document.getElementById("divGroupQ_id" + idNum).remove();
            document.getElementById("break_id" + idNum).remove();
            if (document.getElementById("divRadio_id" + idNum) != null) {
                document.getElementById("divRadio_id" + idNum).remove();
                console.log("REMOVING RADIO BUTTON");
            }
            else if (document.getElementById("divShortAns_id" + idNum) != null) {
                document.getElementById("divShortAns_id" + idNum).remove();
                console.log("REMOVING SHORT ANSWER");
            }
            else if (document.getElementById("divTable_id" + idNum) != null) {
                document.getElementById("divTable_id" + idNum).remove();
                console.log("REMOVING MULTIPLE CHOICE GRID");
            }
            else {
                //do nothing
            }
            idCount--;
            return false;
        }
    </script>

     <%--Script to get ID attribute--%> 
    <script>
        function getID() {
            idCount = parseInt(idCount);
            console.log(idCount);
            console.log(document.getElementsByClassName("input-group"));
            var elem = document.getElementsByClassName("input-group")[idCount];
            idCount = parseInt(idCount) + parseInt(1);
            console.log(elem);
            return elem.getAttribute("id");
        }
    </script>

         <%--  Script to create answer options. Currently if you click on the drop down menu for the question below
    having not chosen an answer type for the current question, the answer type automatically goes to the top question --%>  
  
    <script>
        var cnt = 100;
        var i;

        function makeShortAns() {
            var shortAnsCnt = cnt.toString().slice(-2);
            let divShortAns= document.createElement('div');
            divShortAns.id = "divShortAns_id" + shortAnsCnt;
            divShortAns.name = "divShortAns_name" + shortAnsCnt;
            divShortAns.innerHTML = "<textarea id = 'txtDesc' class = 'form-control' rows = '2' placeholder = 'Answer'></textarea>";
            i = getID();
            document.getElementById(i).after(divShortAns);
            cnt = parseInt(cnt) + parseInt(1);
            console.log("making a short answer");
        }

        function makeRadBut() {
            var radCnt = cnt.toString().slice(-2);
            let divRadio = document.createElement("div");
            divRadio.id = "divRadio_id" + radCnt;
            divRadio.name = "divRadio_name" + radCnt;
            divRadio.className = "form-check";
            
            var numRadBut = prompt("How many options would you like? (You need at least 2)", 2);
            var n;
            for (n = 0; n < numRadBut; n++) {
    
                let radio = document.createElement("input");
                radio.type = "radio";
                radio.className = "form-check-input";
                radio.name = "radios" + parseInt(radCnt);
                // radio.value = "";
                radio.id = "radioId" + parseInt(radCnt);
                radio.name = "radioName" + parseInt(radCnt);
                radio.disabled = true;
                radio.style = "margin-right: 4px; margin-top: 10px;";

                let radLabel = document.createElement("input");
                radLabel.type = "text";
                radLabel.className = "input-group-sm";
                radLabel.placeholder = "Option " + parseInt(n);
                radLabel.id = "radLabeldId" + parseInt(radCnt);
                radLabel.name = "radLabeldName" + parseInt(radCnt);
                radLabel.style = "margin-right: 10px; margin-top: 10px;";

                divRadio.appendChild(document.createElement("br"));
                divRadio.appendChild(radio);
                divRadio.appendChild(radLabel);
            }
            i = getID();
            document.getElementById(i).after(divRadio);
            cnt = parseInt(cnt) + parseInt(1);
            console.log("making radio buttons")
        }

        function makeGrid() {
            var gridCnt = cnt.toString().slice(-2);
            var numRows = prompt("How many rows would you like (at least 1)?", 1);
            var numCols = prompt("How many columns would you like (at least 2)?", 2);
            let divTable = document.createElement("div");
            divTable.id = "divTable_id" + gridCnt;
            divTable.name = "divTable_name" + gridCnt;
            divTable.className = "container";

            let divRow = document.createElement("div");
            divRow.id = "divRow_id" + parseInt(gridCnt);
            divRow.name = "divRow_name" + parseInt(gridCnt);
            divRow.className = "row";
            
            var gridBody = "<div class='col-7'><div></div>";

            for (var r = 0; r < numRows; r++) {
                gridBody += "<div><textarea type='text' class='form-control' placeholder='question' border:none ></textarea></div>";
            }

            gridBody += "</div><div  class='col-5' style='overflow:scroll;height:auto;width:100%;overflow-y:hidden;overflow-x:scroll;'>";
            for (var r = 0; r < numRows; r++) {
                gridBody += "<div style='width:450px;'>";
                for (var c = 0; c < numCols; c++) {
                    gridBody += "<input type='radio' style='margin: 30px' name='gridRadio" + r + "'";
                    gridBody += " disabled><label>" + (c + 1) + "</label>";
                }
                gridBody += "</div>";
            }

            gridBody += "</div>"
            divRow.innerHTML = gridBody;
            divTable.appendChild(divRow);

            i = getID();
            document.getElementById(i).after(divTable);
            cnt = parseInt(cnt) + parseInt(1);
            console.log("making multiple choice grid");
        }

    </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>

</body>
</html>