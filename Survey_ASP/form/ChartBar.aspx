<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChartBar.aspx.vb" Inherits="Survey.ChartBar" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Survey Chart"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <asp:Chart ID="pieChart" runat="server" Width="907px">
                <series>
                    <asp:Series ChartType="Pie" Name="Series1" >
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
                <Legends>
                    <asp:Legend Name="Legend1">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
            <br />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="select B.answerName,count(*) as cntAnswer from surveyUserAnswer A
left join surveyAnswer B  on A.questionId = B.questionId and A.answerId = B.answerId
where A.subjectId = 16 
and A.questionId = 34
group by A.subjectId,B.answerName
"></asp:SqlDataSource>
            <br />
            <asp:Label ID="lblSubject0" runat="server" Font-Bold="True" ForeColor="#0066CC" Text="Label2"></asp:Label>
            <br />
            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource3" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Width="912px">
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <ItemTemplate>
                    &nbsp;<asp:Label ID="answerCommentLabel" runat="server" Text='<%# Eval("answerComment") %>' />
                    <br />
                    <br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            </asp:DataList>
            <br />
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="Select B.questionName ,A.answerComment from  surveyUserAnswer A  
 inner Join surveyQuestion B On A.questionId = B.questionId 
where A.subjectId = @xSubjectId
And A.questionId = @xQuestionId">
                <SelectParameters>
                    <asp:SessionParameter Name="xSubjectId" SessionField="subjectId" Type="String" />
                    <asp:SessionParameter Name="xQuestionId" SessionField="questionId" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <asp:Chart ID="chtCategoriesProductCountBarChart" runat="server" EnableViewState="True" Width="710px"> 
                <Legends>
                    <asp:Legend Name="Legend1">
                    </asp:Legend>
                </Legends>
               <Titles> 
                  <asp:Title Text="Number of Products in Categories"></asp:Title> 
               </Titles> 

               <Series> 
                  <asp:Series Name="Answer 1" ChartType="StackedBar" ChartArea="MainChartArea" BorderWidth="5" Color="Red" IsValueShownAsLabel="True" Legend="Legend1"></asp:Series> 
                   <asp:Series ChartArea="MainChartArea" ChartType="StackedBar" IsValueShownAsLabel="True" Legend="Legend1" Name="Answer 2">
                   </asp:Series>
                   <asp:Series ChartArea="MainChartArea" ChartType="StackedBar" IsValueShownAsLabel="True" Legend="Legend1" Name="Answer 3">
                   </asp:Series>
                   <asp:Series ChartArea="MainChartArea" ChartType="StackedBar" Legend="Legend1" Name="Answer 4">
                   </asp:Series>
               </Series>

               <ChartAreas> 
                  <asp:ChartArea Name="MainChartArea"> 
                  </asp:ChartArea> 
               </ChartAreas> 
            </asp:Chart>
            <br />
            <br />
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand=" select B.answerName,A.questionId,C.questionName,count(*) as cntAnswer from surveyUserAnswer A
--select A.*,B.answerName from surveyUserAnswer A
left join surveyAnswer B  on A.questionId = B.questionId and A.answerId = B.answerId
left join surveyQuestion C on C.questionId = A.questionId
where A.subjectId = 16 
and A.questionId in (37,38,39)
--order by A.questionId
group by A.questionId,B.answerName,C.questionName"></asp:SqlDataSource>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4">
                <Columns>
                    <asp:BoundField DataField="answerName" HeaderText="answerName" SortExpression="answerName" />
                    <asp:BoundField DataField="questionId" HeaderText="questionId" SortExpression="questionId" />
                    <asp:BoundField DataField="questionName" HeaderText="questionName" SortExpression="questionName" />
                    <asp:BoundField DataField="cntAnswer" HeaderText="cntAnswer" SortExpression="cntAnswer" ReadOnly="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SURVEYConnectionString %>" SelectCommand="select A.*,B.answerName from surveyUserAnswer A
left join surveyAnswer B  on A.questionId = B.questionId and A.answerId = B.answerId
where A.subjectId = 16 
and A.questionId = 34"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
