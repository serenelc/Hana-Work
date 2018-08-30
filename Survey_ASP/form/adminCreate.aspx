<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminCreate.aspx.vb" Inherits="Survey.adminCreate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Admin Create</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server" method="get">

        <div style="background-color: inherit; height: 100px">
            <div id="userInfo" class="sidenav">
                <label id="info">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px" />
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-8" style="padding: 20px; background-color: white; width: 70%;">

            <div>
                <h1 style="text-align: center;">Create A Survey </h1>
            </div>

            <div class="form-group">
                <label>
                    <h3>Title</h3>
                </label>
                <input runat="server" type="text" class="form-control" id="txtTitle" style="font-weight: bold;" placeholder="Survey Title" autocomplete="off" />
            </div>

            <div class="form-group">
                <label>Description</label>
                <textarea runat="server" class="form-control" id="txtDesc" rows="2"
                    placeholder="Short description about your survey" autocomplete="off"></textarea>
            </div>

            <div class="form-check form-check-inline">
                <div class="form-group">
                    <label>Survey close date (The survey will close automatically at 00:00 of the day chosen)</label>
                    <input class="w-input input" id="closeDate" type="date" name="close" style="width: 50%; border-radius: 5px; border-style: solid; border-color: #d8d8d8;" />
                </div>
                <%--1 means EN is required--%>
                <input class="form-check-input" type="checkbox" name="enReq" value="1" id="enReq">
                <label class="form-check-label" for="enReq">
                    Require User En
                </label>
            </div>

            <hr>

            <button type="button" class="btn btn-info" id="newSection"
                onclick="addSection();">
                Add a Section</button>
            <button type="button" class="btn btn-info" id="addQ2"
                onclick="addQuestion()">
                Add a Question</button>

            <div id="save" style="background-color: white; height: 70px;">
                <br>
                <asp:Button runat="server" ID="btnSave" type="button" class="btn btn-success"
                    Style="float: right" Text="Save" OnClick="btnSave_Click" OnClientClick="javascript: if (!OpenTaskDialog()) { return false; };" />
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning" Text="Back" />

            </div>
        </div>

    </form>

    <%--Script to allow use of dialog box in server--%>
    <script>
        function OpenTaskDialog() {
            return confirm("Are you sure you have finished creating this survey?");
        }

    </script>

    <%--Script to add a section/sub topic--%>
    <script>
        var countSec = 100;
        var newCountSec;

        function addSection() {

            newCountSec = countSec.toString().slice(-2);

            let divSection = document.createElement("div");
            divSection.className = "container";
            divSection.id = "divSection_id" + newCountSec;
            divSection.name = "divSection_name" + newCountSec;

            let divSecTitle = document.createElement("input");
            divSecTitle.className = "form-control-lg";
            divSecTitle.type = "text";
            divSecTitle.required = true;
            divSecTitle.placeholder = "Section Title";
            divSecTitle.autocomplete = "off"
            divSecTitle.id = "sectionTitle_id" + newCountSec;
            divSecTitle.name = "sectionTitle_name" + newCountSec;
            divSecTitle.value = "<%=divSecTitle_val%>";
            divSecTitle.style = "margin-bottom: 20px; font-weight: bold; float: center; border-right: none; border-left: none; border-top: none; border-bottom-width: medium; border-bottom-color: black; width: 50%;";
            divSection.appendChild(divSecTitle)

            let sectionClose = document.createElement('button');
            sectionClose.id = "sectionCloseQ_id" + newCountSec;
            sectionClose.name = "sectionCloseQ_name" + newCountSec;
            sectionClose.class = "btn"
            sectionClose.style = "border: none; background-color: white;"
            sectionClose.innerHTML = '<span aria-hidden="true">X</span> ';
            sectionClose.onclick = removeSection;
            divSection.appendChild(sectionClose);

            divSection.appendChild(document.createElement("br"));
            newSection.before(divSection);
            console.log("adding a section");
            countSec = parseInt(countSec) + parseInt(1);

        }

    </script>

    <%--Script to add a question and choose an answer type, then disable the drop down once an answer type has been chosen--%>
    <script>

        var countQ = 100;
        var newCountQ;
        function addQuestion() {

            newCountQ = countQ.toString().slice(-2);

            let divGroup = document.createElement("div");
            divGroup.id = "divGroupQ_id" + newCountQ;
            divGroup.name = "divGroupQ_name" + newCountQ;
            divGroup.className = "input-group";

            let questionInput = document.createElement("input");
            questionInput.type = "text";
            questionInput.class = "form-control";
            questionInput.style = "font-weight:bold; width: 80%; border-radius: 5px; border-style: solid; border-color: #d8d8d8;"
            questionInput.placeholder = "  Question";
            questionInput.required = true;
            questionInput.autocomplete = "off";
            questionInput.name = "questionInput_name" + newCountQ;

            divGroup.appendChild(questionInput);

            let divbutton = document.createElement("div");
            divbutton.id = "divbuttonQ_id" + newCountQ;
            divbutton.name = "divbuttonQ_name" + newCountQ;
            divbutton.className = "input-group-append";
            divbutton.innerHTML = '<button id="answerType' + newCountQ + '"class="btn btn-primary dropdown-toggle" style ="border-radius: 5px; border-color: #d15ecc; background-color: #d15ecc;" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Answer Type</button>';

            let divdropdown = document.createElement("div");
            divdropdown.id = "divdropdownQ_id" + newCountQ;
            divdropdown.name = "divdropdownQ_name" + newCountQ;
            divdropdown.className = "dropdown-menu";
            divdropdown.innerHTML = '<a class="dropdown-item" href="javascript:makeRadBut()" onclick=disable()>Radio Button</a>';
            let adropdown2 = document.createElement("a");
            adropdown2.id = "adropdown2Q_id" + newCountQ;
            adropdown2.name = "adropdown2Q_name" + newCountQ;
            adropdown2.innerHTML = '<a class="dropdown-item" href="javascript:makeGrid()" onclick=disable()>Multiple Choice Grid</a>';
            let adropdown3 = document.createElement("a");
            adropdown3.id = "adropdown3Q_id" + newCountQ;
            adropdown3.name = "adropdown3Q_name" + newCountQ;
            adropdown3.innerHTML = '<a class="dropdown-item" href="javascript:makeShortAns()" onclick=disable()>Short Answer</a>';
            divdropdown.appendChild(adropdown2).appendChild(adropdown3);

            divGroup.appendChild(divbutton).appendChild(divdropdown);

            let buttonClose = document.createElement('button');
            buttonClose.id = "buttonCloseQ_id" + newCountQ;
            buttonClose.name = "buttonCloseQ_name" + newCountQ;
            buttonClose.class = "btn"
            buttonClose.style = "border-radius: 5px; border-style: solid; border-color: #cccccc"
            buttonClose.innerHTML = '<span aria-hidden="true">&times;</span> ';
            divGroup.appendChild(buttonClose);

            let br = document.createElement('br');
            br.id = "break_id" + newCountQ;
            br.name = "break_name" + newCountQ;
            newSection.before(divGroup, br);
            buttonClose.onclick = removeElement;

            countQ = parseInt(countQ) + parseInt(1);
            console.log("Adding a question");
        }

        function disable() {
            var toDisable = "answerType" + newCountQ;
            document.getElementById(toDisable).disabled = true;
        }

    </script>

    <%--Script to remove a question and section. TODO: removeSection()--%>
    <script>
        var idCount = 0; //GLOBAL VARIABLE

        function removeSection() {
            var secIdNum = this.id.slice(-2);
            document.getElementById("divSection_id" + secIdNum);
        }

        function removeElement() {

            var idNum = this.id.slice(-2);


            document.getElementById("divGroupQ_id" + idNum).remove();
            document.getElementById("break_id" + idNum).remove();
            if (document.getElementById("divRadio_id" + idNum) != null) {
                document.getElementById("divRadio_id" + idNum).remove();
                console.log("REMOVING RADIO BUTTON");
                idCount--;
            }
            else if (document.getElementById("divShortAns_id" + idNum) != null) {
                document.getElementById("divShortAns_id" + idNum).remove();
                console.log("REMOVING SHORT ANSWER");
                idCount--;
            }
            else if (document.getElementById("divTable_id" + idNum) != null) {
                document.getElementById("divTable_id" + idNum).remove();
                console.log("REMOVING MULTIPLE CHOICE GRID");
                idCount--;
            }
            else {
                //do nothing
            }


            //if (document.getElementById("divGroupQ_idS:" + secNum + "_" + idNum) != null) {
            //    secNum = this.id.substr(indexS + 2, indexU - indexS);
            //    document.getElementById("divGroupQ_idS:" + secNum + "_" + idNum).remove();
            //    document.getElementById("break_idS:" + secNum + "_" + idNum).remove();
            //}

            //if (document.getElementById("divRadio_idS:" + secNum + "_" + idNum) != null) {
            //    document.getElementById("divRadio_idS:" + secNum + "_" + idNum).remove();
            //    console.log("REMOVING RADIO BUTTON");
            //    idCount--;
            //}
            //else if (document.getElementById("divShortAns_idS:" + secNum + "_" + idNum) != null) {
            //    document.getElementById("divShortAns_idS:" + secNum + "_" + idNum).remove();
            //    console.log("REMOVING SHORT ANSWER");
            //    idCount--;
            //}
            //else if (document.getElementById("divTable_idS:" + secNum + "_" + idNum) != null) {
            //    document.getElementById("divTable_idS:" + secNum + "_" + idNum).remove();
            //    console.log("REMOVING MULTIPLE CHOICE GRID");
            //    idCount--;
            //}
            //else {

            //}

            return false;
        }
    </script>


    <%--Script to get ID attribute--%>
    <script>
        function getID() {
            idCount = parseInt(idCount);
            console.log(document.getElementsByClassName("input-group"));
            var elem = document.getElementsByClassName("input-group")[idCount];
            idCount = parseInt(idCount) + parseInt(1);
            console.log(elem);
            return elem.getAttribute("id");
        }
    </script>

    <%--  Script to create answer options. Currently if you click on the drop down menu for the question below
    having not chosen an answer type for the current question, the answer type automatically goes to the top question. This 
        is because when a question is created it gets given an id, say 1, so if you add 3 questions in a row, the questions
        will have id 1, 2 and 3. However, the number indication for the answer will still be at 1 so when you click on the
        third question and try to add an answer type, it will match its id of 1 to the question id 1.--%>

    <script>
        var cnt = 100;
        var i;

        function makeShortAns() {
            var shortAnsCnt = cnt.toString().slice(-2);
            let divShortAns = document.createElement('div');
            divShortAns.id = "divShortAns_id" + shortAnsCnt;
            divShortAns.name = "divShortAns_name" + shortAnsCnt;

            let shortAns = document.createElement("textarea");
            shortAns.id = "shortAns" + shortAnsCnt;
            shortAns.class = "form-control";
            shortAns.rows = "2";
            shortAns.style = "width: 95%; border-radius: 5px; border-style: solid; border-color: #d8d8d8;"
            shortAns.placeholder = "Answer";
            shortAns.name = "shortAns_name" + shortAnsCnt;

            divShortAns.appendChild(shortAns);

            i = getID();
            document.getElementById(i).after(divShortAns);
            cnt = parseInt(cnt) + parseInt(1);

            console.log("making a short answer");
            console.log(divShortAns);
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
                radio.id = "radioId" + parseInt(radCnt);
                radio.name = "radioName" + parseInt(radCnt);
                radio.disabled = true;
                radio.style = "margin-right: 4px; margin-top: 10px;";

                let radLabel = document.createElement("input");
                radLabel.type = "text";
                radLabel.required = true;
                radLabel.className = "input-group-sm";
                radLabel.placeholder = "Option " + parseInt(n);
                radLabel.id = "radLabelId" + parseInt(radCnt);
                radLabel.name = "radLabelName" + parseInt(radCnt);
                radLabel.autocomplete = "off";
                radLabel.style = "margin-right: 10px; margin-bottom: 10px; border-radius: 5px; border-style: solid; border-color: #d8d8d8;";

                divRadio.appendChild(document.createElement("br"));
                divRadio.appendChild(radio);
                divRadio.appendChild(radLabel);
            }
            i = getID();
            document.getElementById(i).after(divRadio);
            cnt = parseInt(cnt) + parseInt(1);
            console.log("making radio buttons");
            console.log(divRadio);
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
                if (r == 0) {
                    gridBody += "<div><textarea type='text' class='form-control' placeholder='question' required=true autocomplete= 'off' style='border-radius: 5px; border-style: solid; border-color: #d8d8d8;' name = 'gridQQ_name"
                        + gridCnt + "_" + r + "_" + numCols + "' id='gridQ_Id" + gridCnt + "_" + r + "'></textarea></div>";
                }
                else {
                    gridBody += "<div><textarea type='text' class='form-control' placeholder='question' required=true autocomplete= 'off' style='border-radius: 5px; border-style: solid; border-color: #d8d8d8;' name = 'gridQ_name"
                        + gridCnt + "_" + r + "_" + numCols + "' id='gridQ_Id" + gridCnt + "_" + r + "'></textarea></div>";
                }
            }

            gridBody += "</div><div class='col-5' style='overflow:scroll;height:auto;width:100%;overflow-y:hidden;overflow-x:scroll;'>";
            for (var r = 0; r < numRows; r++) {
                gridBody += "<div style='width:450px;'>";
                for (var c = 0; c < numCols; c++) {
                    gridBody += "<input type='radio' style='margin: 24px' name='gridRadio" + r + "'";
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
            console.log(divTable);
        }

    </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>

</body>
</html>
