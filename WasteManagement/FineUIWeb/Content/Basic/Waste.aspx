<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Waste.aspx.cs" Inherits="WasteManagement.Content.Basic.Waste" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="200px" LabelWidth="100px" Label="污染物代码" ID="ser_vDirectiveNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="200px" LabelWidth="100px" Label="污染物名称" ShowLabel="true" ID="ser_vSaveNumber"></f:TextBox>                  
                  <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                </Items>
            </f:FormRow>            
            
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="ID" IsDatabasePaging="true"  OnSort="Grid1_Sort"
        AllowSorting="true" SortField="ID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                       <f:BoundField DataField="ID" Hidden="true" />
                <f:BoundField DataField="WasteCode" Hidden="true" />
                        <f:BoundField DataField="WasteName" SortField="WasteName" Width="120px" HeaderText="污染物名称" />
                <f:BoundField DataField="List" SortField="List" Width="120px" HeaderText="污染物全称" />
                       <f:BoundField DataField="WasteCode" SortField="WasteCode" Width="80px" HeaderText="污染物代码" />
                <f:BoundField DataField="Unit" SortField="Unit" Width="80px" HeaderText="单位" />
                      <f:BoundField DataField="OrderID" SortField="OrderID" Width="80px" HeaderText="显示顺序" />
                       <f:BoundField DataField="IsShow" SortField="IsShow" Width="80px" HeaderText="是否显示" />
            </Columns>
        <Toolbars>
        <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
