<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alarm.aspx.cs" Inherits="WasteManagement.Content.State.Alarm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <f:PageManager ID="PageManager1" runat="server"  AutoSizePanelID="Panel1"/>
        <f:Panel ID="Panel1" runat="server" ShowBorder="true" ShowHeader="true" Title=""
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start">
            <Items>
                <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
        DataKeyNames="PlanID" IsDatabasePaging="true"  OnPageIndexChange="Grid1_PageIndexChange" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="PlanID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="PlanID" Hidden="true" />
                <f:BoundField DataField="ContractNumber" SortField="ContractNumber" Width="80px" HeaderText="合同编号" />
                       <f:BoundField DataField="PlanNumber" SortField="PlanNumber" Width="80px" HeaderText="计划编号" />
                       <f:BoundField DataField="ProduceName" SortField="ProduceName" Width="120px" HeaderText="企业名称" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="120px" HeaderText="废物名称" />
                       <f:BoundField DataField="StartDate" SortField="StartDate" DataFormatString="{0:d}" Width="80px" HeaderText="起始时间" />
                       <f:BoundField DataField="EndDate" SortField="EndDate" DataFormatString="{0:d}" Width="80px" HeaderText="结束时间" />
                        <f:BoundField DataField="PlanAmount" SortField="PlanAmount" Width="120px" HeaderText="计划数量（吨）" />
                        <f:BoundField DataField="Used" SortField="Used" Width="120px" HeaderText="已使用数量（吨）" />
            </Columns>
        <Toolbars>
        <f:Toolbar ID="Toolbar1" runat="server">
            <Items>
            <f:Button ID="Refresh" runat="server" OnClick="Click"  Text="刷新" CssClass="btn"></f:Button>
            </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
