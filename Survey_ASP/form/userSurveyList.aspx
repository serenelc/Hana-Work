<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userSurveyList.aspx.vb" Inherits="Survey.userSurveyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>List of Surveys</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
</head>

<body style="background: linear-gradient(#a9c5f2, #619af4) fixed;">

    <form id="form1" runat="server" method="get">

        <div style="background: inherit; height: 100px">
            <div id="userInfo" class="sidenav">
                <label id="info">
                    <img src="../images/user.png" style="height: 40px; padding-right: 10px" />
                    <%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%>
                </label>
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float: left;" />
        </div>

        <div id="main" class="container col-8" style="padding: 20px; background-color: white; min-height: 100%; width: 70%;">

            <div>
                <h1 style="text-align: center;">Surveys </h1>
                <br>
            </div>

            <div>
                <p>
                    The following surveys are currently open. Click on the column title to sort the table.
                </p>
                <div>

                    <asp:SqlDataSource ID="SqlSurveyListSource" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>"
                        SelectCommand="Select subjectName, subjectDetail, openDate, closeDate, subjectId, enRequired  From surveyMaster where statusComp = 0">
                    </asp:SqlDataSource>

                    <asp:GridView ID="surveyList" runat="server" Width="100%" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="0px" CellPadding="10"
                        ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="subjectId" DataSourceID="SqlSurveyListSource" AllowSorting="True">
                        <Columns>
                            <asp:BoundField DataField="subjectName" HeaderText="Survey Title" SortExpression="subjectName" />
                            <asp:BoundField DataField="subjectDetail" HeaderText="Details" SortExpression="subjectDetail" />
                            <asp:BoundField DataField="openDate" HeaderText="Open Date" SortExpression="openDate" />
                            <asp:BoundField DataField="closeDate" HeaderText="Close Date" SortExpression="closeDate" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="go" runat="server" class="btn btn-success" CommandName="Go"
                                        Text="Go" CommandArgument='<%# Eval("subjectId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="subjectId" HeaderText="subjectId" InsertVisible="False" ReadOnly="True" SortExpression="subjectId" Visible="False" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#242121" />
                    </asp:GridView>

                </div>
            </div>

            <div id="footer" style="background-color: white; height: 70px;">
                <br>
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                    Style="float: left; visibility: hidden;" Text="Back" />
               <%-- <asp:Button runat="server" ID="btnLogout" type="button" class="btn btn-danger"
                    Style="float: right" Text="Logout" OnClick="btnLogout_Click" OnClientClick="javascript: if (!OpenTaskDialogLogout()) { return false; };" />--%>
            </div>

        </div>

        <%--Script to allow use of dialog box in server--%>
        <script>
            function OpenTaskDialogLogout() {
                return confirm("Are you sure you want to logout?");
            }
        </script>

        <%--Script to check if user is admin--%>
        <script>
            function isAdmin() {
                let uType = "<%=Session("UserType").ToString()%>";
                let btn = document.getElementById("btnBack");
                if ("ADMIN" == uType) {
                    btn.style = "visibility: visible;";
                }
                else {
                    btn.style = "visibility: hidden;";
                }
            }
            isAdmin();
        </script>

        <script src="../node_modules/jquery/dist/jquery.min.js"></script>
        <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
        <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
</body>
</html>
