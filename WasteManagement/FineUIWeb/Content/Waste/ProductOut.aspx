<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="ProductOut.aspx.cs" Inherits="WasteManagement.Content.Waste.ProductOut" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="罐池号" ShowLabel="true" ID="txt_ContractNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="收货单位" ShowLabel="true" ID="txt_Name"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="成品名称" ShowLabel="true" ID="txt_WasteName"></f:TextBox>

                        <f:Label runat="server" Width="80px"></f:Label>
                </Items>
            </f:FormRow>            
             <f:FormRow>
                 <Items>
                    
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="100px" Label="出库日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="100px" Label="出库日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:DropDownList ID="drop_Status" EmptyText="状态" ShowLabel="true" AutoPostBack="true"  Label="状态"  LabelWidth="100px"
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
        DataKeyNames="OutID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="OutID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="OutID" Hidden="true" />
                       <f:BoundField DataField="DateTime" SortField="DateTime" Width="80px" HeaderText="日期" />
                       <f:BoundField DataField="PondName" SortField="PondName" Width="80px" HeaderText="罐池号" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="80px" HeaderText="成品名称" />
                       <f:BoundField DataField="Amount" SortField="Amount" Width="120px" HeaderText="数量（吨）" />
                        <f:BoundField DataField="Unit" SortField="Unit" Width="120px" HeaderText="单位" />
                       <f:BoundField DataField="Name" SortField="Name" Width="120px" HeaderText="收货单位" />
                       <f:BoundField DataField="CarNumber" SortField="CarNumber" Width="80px" HeaderText="车牌号" />
                       <f:BoundField DataField="RealName" SortField="RealName" Width="80px" HeaderText="驾驶员" />
                       <f:BoundField DataField="UserRealName" SortField="UserRealName" Width="80px" HeaderText="发货人" />
                       <f:BoundField DataField="StatusName" SortField="StatusName" Width="120px" HeaderText="状态" />
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


