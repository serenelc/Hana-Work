<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="results.aspx.vb" Inherits="Survey.results" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Statistics</title>
    <!-- Custom styles for this template -->
    <link href="../css/userInfo.css" rel="stylesheet" />
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
                <asp:Label ID="title" runat="server"></asp:Label>
                <asp:Label ID="description" runat="server"></asp:Label>
            </div>

            <div>
                <p>
                    <asp:ImageButton ID="excelImg" runat="server" ImageUrl="../images/excel.png" Width="40" CommandName="excelImg" />
                    Click on the image to download an excel spreadsheet version of the results
                </p>
            </div>

            <div class="col-sm-8" style="width: 70%;">
                <asp:DataList ID="DataList1" runat="server" DataKeyField="sectionId" DataSourceID="SqlDataSource1" HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="sectionIdLabel" runat="server" Text='<%# Eval("sectionId") %>' Visible="<%# false %>" />
                        <asp:Label ID="subjectIdLabel" runat="server" Text='<%# Eval("subjectId") %>' Visible="<%# false %>" />
                        <br />

                        <%--Pie Charts for radio answers--%>
                        <asp:Chart ID="ChartPie" runat="server" DataSourceID="SqlDataSourcePie" Width="892px">
                            <Series>
                                <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" XValueMember="answerName" YValueMembers="cnt">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1" Alignment="Center" >
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Title1" Text="1234">
                                </asp:Title>
                            </Titles>
                        </asp:Chart>
                        <asp:SqlDataSource ID="SqlDataSourcePie" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>"
                            SelectCommand="Select  a.sectionName, c.questionType, f.answerName,  count(d.answerId) As cnt  
                                        From surveyMaster b   
                                        inner Join surveySection a On a.subjectId = b.subjectId   
                                        inner Join surveyQuestion c On a.sectionId = c.sectionId   
                                        inner Join surveyAnswer f On c.questionId = f.questionId   
                                        Left Join surveyUserAnswer d On d.answerId = f.answerId  
                                        where questionType = 'radio'  and b.subjectId =  @subId and a.sectionId = @secId
                                        Group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName  
                                        order by a.sectionId, c.questionId ">
                            <SelectParameters>
                                <asp:SessionParameter Name="subId" SessionField="QsubjectId" />
                                <asp:ControlParameter ControlID="sectionIdLabel" Name="secId" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        &nbsp;
                        
                        <%--Bar charts for multi grid answers--%>
                        <asp:Chart ID="Chart21" runat="server" BackColor="white" BorderlineDashStyle="Solid" Height="620px" Width="900px">
                            <Titles>
                                <asp:Title Name="Items" Text="" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" LegendStyle="Row" Name="Default" />
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea BorderWidth="0" Name="ChartArea1" />
                            </ChartAreas>
                        </asp:Chart>
                        <br />

                        <%--Table for short answers--%>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource31" Width="100%" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="0px" CellPadding="10"
                            ForeColor="Black" GridLines="Horizontal" AllowSorting="True" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="sectionName" HeaderText="sectionName" SortExpression="sectionName" />
                                <asp:BoundField DataField="questionName" HeaderText="questionName" SortExpression="questionName" />
                                <asp:BoundField DataField="answerComment" HeaderText="answerComment" SortExpression="answerComment" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource31" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="Select distinct  a.sectionName,
                         c.questionName,d.answerComment
                        FROM surveyMaster AS b 
                        INNER JOIN surveySection AS a ON a.subjectId = b.subjectId 
                        INNER JOIN surveyQuestion AS c ON a.sectionId = c.sectionId And c.questionType = 'shortanswer'
                        INNER JOIN surveyAnswer AS f ON c.questionId = f.questionId 
                        INNER JOIN surveyUserAnswer AS d ON d.answerId = f.answerId 
                        WHERE (b.subjectId = @subId) AND (a.sectionId = @secId)">
                            <SelectParameters>
                                <asp:SessionParameter Name="subId" SessionField="QsubjectId" />
                                <asp:ControlParameter ControlID="sectionIdLabel" DefaultValue="9" Name="secId" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <br />
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <div id="footer" style="background-color: white; height: 70px;">
                <br>
                <asp:Button runat="server" ID="btnBack" type="button" class="btn btn-warning"
                    Style="float: left;" Text="Back" />
                <asp:Button runat="server" ID="btnLogout" type="button" class="btn btn-danger"
                    Style="float: right" Text="Logout" OnClick="btnLogout_Click" OnClientClick="return confirmLogout()" />
            </div>

        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand=" select * from surveySection where subjectId = @subId">
            <SelectParameters>
                <asp:SessionParameter Name="subId" SessionField="QsubjectId" />
            </SelectParameters>
        </asp:SqlDataSource>

        </div>

          <%--Script to confirm logout--%>
        <script>
            function confirmLogout() {
                return confirm("Are you sure you would like to logout?");
            }
        </script>

        <script src="../node_modules/jquery/dist/jquery.min.js"></script>
        <script src="../node_modules/popper.js/dist/umd/popper.min.js"></script>
        <script src="../node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    </form>
</body>

</html>
