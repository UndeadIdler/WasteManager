<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="WasteManagement.Content.State.Monitor" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
<%--        <meta name="sourcefiles" content="AnalysisGrid.ashx" />
    <link href="../../res/css/main.css" rel="stylesheet" type="text/css" />--%>
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>                
             <f:FormRow>
                 <Items>
                    <f:DropDownList ID="drop_Position" EmptyText="监测位置" LabelWidth="95px" Label="监测位置" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="95px" Label="监测日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="95px" Label="监测日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:DropDownList ID="drop_Analysis" EmptyText="分析人" LabelWidth="95px" Label="分析人" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                     <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <%--<f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" runat="server" />--%>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true" OnSort="Grid1_Sort"
        DataKeyNames="MonitorID" IsDatabasePaging="true"  EnableCollapseEvent="true"
        AllowSorting="true" SortField="MonitorID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="MonitorID" Hidden="true" />  
                <f:BoundField DataField="PositionName" SortField="PositionName" Width="80px" HeaderText="监测位置" />
                       <f:BoundField DataField="DateTime" SortField="DateTime" DataFormatString="{0:d}" Width="120px" HeaderText="日期" />
                       <f:BoundField DataField="RealName" SortField="RealName" Width="120px" HeaderText="分析人" />
                       <f:BoundField DataField="StatusName" SortField="StatusName" Width="80px" HeaderText="状态" />
                <f:GroupField ID="GF1" HeaderText="分析结果" TextAlign="Center">
                            <Columns>                
                            </Columns>
                        </f:GroupField>                             
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <%--<f:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click"></f:Button>--%>
                    <f:Button ID="btn_Pass" runat="server" Text="审核通过" OnClick="btn_Pass_Click"></f:Button>
                    <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
    
</asp:Content>


