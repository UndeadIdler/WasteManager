<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Pond.aspx.cs" Inherits="WasteManagement.Content.Basic.Pond" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="200px" LabelWidth="100px" Label="污染物名称" ID="ser_vDirectiveNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="200px" LabelWidth="100px" Label="罐池名称" ShowLabel="true" ID="ser_vSaveNumber"></f:TextBox>
                  <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                </Items>
            </f:FormRow>            
            
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="PondID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="PondID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                       <f:BoundField DataField="PondID" Hidden="true" />
                <f:BoundField DataField="Stores" Hidden="true" />                     
                <f:BoundField DataField="Name" SortField="Name" Width="120px" HeaderText="罐池名称" />
                <f:BoundField DataField="WasteName" SortField="WasteName" Width="180px" HeaderText="物料名称" />
                <f:BoundField DataField="Capacity" SortField="Capacity" Width="180px" HeaderText="容量" />
                      <f:BoundField DataField="Number" SortField="Number" Width="80px" HeaderText="编号" />
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
