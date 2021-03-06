﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="results.aspx.vb" Inherits="Survey.results" %>

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
                        <%--<asp:Label ID="questionIdLabel" runat="server" Text='<%# Eval("questionId") %>' Visible="<%# false %>" />--%>
                        <asp:Label ID="questionTypeLabel" runat="server" Text='<%# Eval("questionType") %>' Visible="<%# false %>" />
                        <br />

                        <%--Pie Charts for radio answers--%>
                        <center><asp:Label ID="radioLabel" runat="server"></asp:Label>
                        <asp:Chart ID="ChartPie" runat="server" DataSourceID="SqlDataSourcePie" Width="892px" Palette="None" PaletteCustomColors="244, 229, 65; 127, 244, 65; 65, 238, 244; 65, 115, 244; 106, 65, 244; 166, 65, 244; 244, 65, 217; 244, 65, 97">
                            <Series>
                                <asp:Series ChartType="Pie" IsValueShownAsLabel="True" Legend="Legend1" Name="Series1" XValueMember="answerName" YValueMembers="cnt">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1" Alignment="Center">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Title1" Text="">
                                </asp:Title>
                            </Titles>
                        </asp:Chart></center>
                        <asp:SqlDataSource ID="SqlDataSourcePie" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>"
                            SelectCommand="Select a.sectionName, c.questionType, c.questionName, c.questionId, f.answerName,  count(d.answerId) As cnt  
                                        From surveyMaster b   
                                        inner Join surveySection a On a.subjectId = b.subjectId   
                                        inner Join surveyQuestion c On a.sectionId = c.sectionId   
                                        inner Join surveyAnswer f On c.questionId = f.questionId   
                                        Left Join surveyUserAnswer d On d.answerId = f.answerId  
                                        where questionType like 'radio%'  and b.subjectId =  @subId and a.sectionId = @secId and c.questionType = @qType
                                        Group by b.subjectName, a.sectionId, a.sectionName, c.questionId, c.questionName, c.questionType, f.answerId, f.answerName  
                                        order by a.sectionId, c.questionId ">
                            <SelectParameters>
                                <asp:SessionParameter Name="subId" SessionField="QsubjectId" />
                                <asp:ControlParameter ControlID="sectionIdLabel" Name="secId" PropertyName="Text" />
                                <asp:ControlParameter ControlID="questionTypeLabel" Name="qType" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        &nbsp;
                        
                        <%--Bar charts for multi grid answers--%>
                        <center><asp:Label ID="multiLabel" runat="server"></asp:Label>
                        <asp:Chart ID="Chart21" runat="server" BorderlineDashStyle="Solid" Height="620px" Width="900px" Palette="None" PaletteCustomColors="244, 229, 65; 127, 244, 65; 65, 238, 244; 65, 115, 244; 106, 65, 244; 166, 65, 244; 244, 65, 217; 244, 65, 97">
                            <Titles>
                                <asp:Title Name="Items" Text="" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" LegendStyle="Row" Name="Default" />
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea BorderWidth="0" Name="ChartArea1" />
                            </ChartAreas>
                        </asp:Chart></center>
                        <asp:SqlDataSource ID="SqlDataSourceChart" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="Select a.sectionName , c.questionType , c.questionName , c.questionId /* ,[answerName]= isnull(f.answerName,space(10)) */ ,  count(d.answerId) As cnt  
                                        From surveyMaster b   
                                        inner Join surveySection a On a.subjectId = b.subjectId   
                                        inner Join surveyQuestion c On a.sectionId = c.sectionId   
                                        inner Join surveyAnswer f On c.questionId = f.questionId   
                                        Left Join surveyUserAnswer d On d.answerId = f.answerId  
                                        where questionType ='grid'  and b.subjectId =  @subId and a.sectionId = @secId 
                                        Group by a.sectionName , c.questionType , c.questionName, c.questionId /* , isnull(f.answerName,space(10))*/
                                     ">
                            <SelectParameters>
                                <asp:SessionParameter Name="subId" SessionField="QsubjectId" DefaultValue="5" />
                                <asp:ControlParameter ControlID="sectionIdLabel" Name="secId" PropertyName="Text" DefaultValue="11" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />


                        <%--Table for short answers--%>
                        <center><asp:Label ID="shortAnsLabel" runat="server"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource31" Width="90%" BackColor="White" BorderColor="Black" BorderStyle="None" BorderWidth="0px" CellPadding="10"
                            ForeColor="Black" GridLines="Horizontal" AllowSorting="True" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="sectionName" HeaderText="sectionName" SortExpression="sectionName" Visible="False" />
                                <asp:BoundField DataField="questionName" HeaderText="questionName" SortExpression="questionName" Visible="False" />
                                <asp:BoundField DataField="answerComment" HeaderText="Answer" SortExpression="answerComment" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView> </center>
                        <asp:SqlDataSource ID="SqlDataSource31" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="Select distinct  a.sectionName, c.questionName, d.answerComment
                        FROM surveyMaster AS b 
                        INNER JOIN surveySection AS a ON a.subjectId = b.subjectId 
                        INNER JOIN surveyQuestion AS c ON a.sectionId = c.sectionId And c.questionType like 'shortanswer%'
                        INNER JOIN surveyAnswer AS f ON c.questionId = f.questionId 
                        INNER JOIN surveyUserAnswer AS d ON d.answerId = f.answerId 
                        WHERE (b.subjectId = @subId) AND (a.sectionId = @secId) AND (c.questionType = @qType)">
                            <SelectParameters>
                                <asp:SessionParameter Name="subId" SessionField="QsubjectId" />
                                <asp:ControlParameter ControlID="sectionIdLabel" DefaultValue="9" Name="secId" PropertyName="Text" />
                                <asp:ControlParameter ControlID="questionTypeLabel" Name="qType" PropertyName="Text" />
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
                    Style="float: right" Text="Logout" OnClick="btnLogout_Click" OnClientClick="javascript: if (!OpenTaskDialogLogout()) { return false; };" />
            </div>

        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>"
            SelectCommand=" select A.subjectId,A.sectionId,A.sectionName,B.questionType
                            from surveySection A
                            inner join surveyQuestion B on A.sectionId = B.sectionId
                            where A.subjectId = @subId
                            group by A.subjectId,A.sectionId,A.sectionName,B.questionType">
            <SelectParameters>
                <asp:SessionParameter Name="subId" SessionField="QsubjectId" DefaultValue="12" />
            </SelectParameters>
        </asp:SqlDataSource>

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
