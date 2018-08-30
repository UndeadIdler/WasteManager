<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="UserExport.aspx.cs" Inherits="WasteManagement.Content.User.UserExport" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="250px" LabelWidth="100px" Label="姓名" ID="txt_Name"></f:TextBox>
                    <f:DropDownList ID="Drop_Role" EmptyText="角色" ShowLabel="true" AutoPostBack="true" Label="角色"
                        runat="server" Width="250px">
                        </f:DropDownList>
                 <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                    
                </Items>
            </f:FormRow>            
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="UserID" IsDatabasePaging="true"
        AllowSorting="true" SortField="UserID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="UserID" Hidden="true" />
                <f:BoundField DataField="RealName" SortField="RealName" Width="120px" HeaderText="姓名" />
                       <f:BoundField DataField="Description" SortField="Description" Width="80px" HeaderText="角色" />
            </Columns>
        <Toolbars>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
