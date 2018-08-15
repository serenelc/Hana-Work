<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="userSurveyList.aspx.vb" Inherits="Survey.userSurveyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List of Surveys</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet">
    <link rel="stylesheet" href="../node_modules/bootstrap/dist/css/bootstrap.min.css">
</head>
<body style="background: linear-gradient(#a9c5f2, #619af4);">

    <form id="form1" runat="server" method="get">

        <div style="background-color:inherit; height: 100px" >
            <div id="userInfo" class="sidenav">
                <label id= "info"><%=Session("En")%>, <%=Session("Name")%>, <%=Session("UserType")%></label> 
            </div>
            <img src="../images/HANA-Logo-BW.png" style="float:left;" />
        </div>
        
        <div id ="main" class="container" style="padding: 20px; background-color:white; min-height: 100%; width: 70%;">
            
            <div>
                <h1 style="text-align: center;"> Surveys </h1>
                <br>
            </div>

            <div>
                <p> 
                    The following surveys are currently open.
                </p>
                <div>
                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Survey.My.MySettings.ConnStringDatabaseSurvey %>" 
                        SelectCommand="SELECT subjectName, subjectDetail, createDate, subjectId from surveyMaster where statusComp = 0"
                        ></asp:SqlDataSource>
                    
                    <asp:GridView ID="surveyList" runat="server" Width="615px" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="0px" CellPadding="10" 
                        ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataKeyNames="subjectId" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="subjectName" HeaderText="Survey Name" SortExpression="subjectName" />
                            <asp:BoundField DataField="subjectDetail" HeaderText="Details" SortExpression="subjectDetail" />
                            <asp:BoundField DataField="createDate" HeaderText="Date Created" SortExpression="createDate" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" class="btn btn-success" CommandName="Go"
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

            <div id = "footer" style="background-color: white; height: 70px;">
                <br>
                <asp:button runat="server" id="btnLogout" type="button" class="btn btn-danger"
                    style="float: right" Text="Logout" onclick="btnLogout_Click" OnClientClick="return confirmLogout()"/>
                <asp:button runat="server" id="btnBack" type="button" class="btn btn-warning"
                    style="float: left; visibility: hidden;" Text="Back"/>
             </div>
        
        </div>

        <%--Script to confirm logout--%>
        <script>
            function confirmLogout() {
                return confirm("Are you sure you would like to logout?");
            }
        </script>

        <%--Script to check if user is admin--%>
        <script>
            function isAdmin() {
                <%--if (<%=Session("UserType")%> = "ADMIN") {
                    console.log("user is admin");
                }
                else {
                    console.log("user is not admin");
                }--%>
                console.log(<%=Session("UserType")%>);
            }
            isAdmin();
        </script>

    <script src="../node_modules/jquery/dist/jquery.min.js"></script>
    <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
    <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
 </body>
</html>
