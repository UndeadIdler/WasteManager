<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="FinalWaste.aspx.cs" Inherits="WasteManagement.Content.Waste.FinalWaste" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
<%--        <meta name="sourcefiles" content="AnalysisGrid.ashx" />
    <link href="../../res/css/main.css" rel="stylesheet" type="text/css" />--%>
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>                
             <f:FormRow>
                 <Items>
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="95px" Label="产生日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="95px" Label="产生日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:DropDownList ID="drop_Handle" EmptyText="填写人" LabelWidth="95px" Label="填写人" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                      <f:DropDownList ID="drop_Status" EmptyText="状态" LabelWidth="95px" Label="状态" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                     <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
        DataKeyNames="LogID" IsDatabasePaging="true"  EnableCollapseEvent="true" 
        AllowSorting="true" SortField="LogID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="LogID" Hidden="true" />  
                <f:BoundField DataField="DateTime" SortField="DateTime" Width="180px" HeaderText="日期" />
                <f:BoundField DataField="LogNumber" SortField="LogNumber" Width="180px" HeaderText="记录编号" />
                <f:BoundField DataField="RealName" SortField="RealName" Width="120px" HeaderText="填写人" />
                <f:BoundField DataField="StatusName" SortField="StatusName" Width="80px" HeaderText="状态" />
                                     <f:GroupField ID="GF2" HeaderText="新危废" TextAlign="Center">
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

