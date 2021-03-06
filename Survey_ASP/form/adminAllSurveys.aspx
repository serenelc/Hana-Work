﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="adminAllSurveys.aspx.vb" Inherits="Survey.adminAllSurveys" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>All Surveys</title>
    <!-- Custom styles for this template -->
    <link rel="stylesheet" href="../css/userInfo.css" />
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css" />
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
                <h1 style="text-align: center;">All Surveys </h1>
                <br>
            </div>

            <div>
                <p>
                    Click on the column title to sort the table. Click on the Open or Close button to manually reopen your survey or manually close your survey early. 
                    If you choose to reopen a survey, the new close date will be automatically set to this time and date next month.
                </p>
                <div>

                    <div>

                        <asp:SqlDataSource ID="SqlSurveyListSource" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>"
                            SelectCommand="SELECT [subjectName], [subjectDetail], [subjectId], [status], [closeDate] FROM [surveyMaster]"></asp:SqlDataSource>

                        <asp:GridView ID="surveyList" runat="server" Width="100%" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="0px" CellPadding="10"
                            ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="subjectId" DataSourceID="SqlSurveyListSource" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="subjectName" HeaderText="Survey Name" SortExpression="subjectName" />
                                <asp:BoundField DataField="subjectDetail" HeaderText="Details" SortExpression="subjectDetail" />
                                <asp:BoundField DataField="subjectId" HeaderText="subjectId" SortExpression="subjectId" InsertVisible="False" ReadOnly="True" Visible="False" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <asp:TemplateField HeaderText="Results">
                                    <ItemTemplate>
                                        <asp:Button ID="results" runat="server" class="btn btn-success" CommandName="Results"
                                            Text="Results" CommandArgument='<%# Eval("subjectId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="sendMail" runat="server" ImageUrl="../images/mail.png" Width="40" CommandName="SendMail"
                                            Text="Send Email" CommandArgument='<%# Eval("subjectId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Open/Close Survey">
                                    <ItemTemplate>
                                        <asp:Button ID="openClose" runat="server" class="btn btn-primary" CommandName="Toggle"
                                            Text="Open or Close" CommandArgument='<%# Eval("subjectId") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="closeDate" HeaderText="closeDate" SortExpression="closeDate" Visible="False" />
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

                    <div id="footer" style="background-color: white; height: 70px;">
                        <br>
                        <asp:Button runat="server" ID="btnLogout" type="button" class="btn btn-danger"
                            Style="float: right" Text="Logout" OnClick="btnLogout_Click" OnClientClick="javascript: if (!OpenTaskDialogLogout()) { return false; };" />
                        <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                            Style="float: left;" Text="Back" />
                    </div>

                </div>

                <%--Script to allow use of dialog box in server--%>
                <script>
                    function OpenTaskDialogLogout() {
                        return confirm("Are you sure you want to logout?");
                    }
                </script>

                <script src="../node_modules/jquery/dist/jquery.min.js"></script>
                <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
                <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
    </form>

</body>
</html>
