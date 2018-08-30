<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="AnalysisEx.aspx.cs" Inherits="WasteManagement.Content.Waste.AnalysisEx" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
<f:TextBox ID="BillNumber" runat="server" Width="120px"></f:TextBox>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
        DataKeyNames="StorageID" IsDatabasePaging="true" 
        AllowSorting="true" SortField="StorageID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="StorageID" Hidden="true" />
                       <f:BoundField DataField="BillNumber" SortField="BillNumber" Width="80px" HeaderText="联单号" />
                       <f:BoundField DataField="PlanNumber" SortField="PlanNumber" Width="80px" HeaderText="计划编号" />
                       <f:BoundField DataField="DateTime" SortField="DateTime" Width="80px" HeaderText="日期" />
                       <f:BoundField DataField="ProduceName" SortField="ProduceName" Width="80px" HeaderText="企业名称" />
                       <f:BoundField DataField="PondName" SortField="PondName" Width="80px" HeaderText="罐池号" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="80px" HeaderText="废物名称" />
                       <f:BoundField DataField="Amount" SortField="Amount" Width="120px" HeaderText="数量" />
                       <f:BoundField DataField="Unit" SortField="Unit" Width="120px" HeaderText="单位" />
                       <f:BoundField DataField="RealName" SortField="RealName" Width="80px" HeaderText="驾驶员" />
                       <f:BoundField DataField="ReceiverName" SortField="ReceiverName" Width="80px" HeaderText="签收人" />
                       <f:BoundField DataField="StatusName" SortField="StatusName" Width="120px" HeaderText="状态" />
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
            <Items>
            <f:Button ID="Refresh" runat="server" OnClick="Click"  Text="刷新" CssClass="btn"></f:Button>
            </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
    
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="refresh" content="60">
</asp:Content>




