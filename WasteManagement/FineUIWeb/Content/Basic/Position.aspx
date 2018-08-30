<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Position.aspx.cs" Inherits="WasteManagement.Content.Basic.Position" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="ID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="ID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="ID" Hidden="true" />
                        <f:BoundField DataField="Name" SortField="Name" Width="80px" HeaderText="监测位置" />
                <f:BoundField DataField="OrderID" SortField="OrderID" Width="120px" HeaderText="显示顺序" />
                <f:BoundField DataField="IsShow" SortField="IsShow" Width="80px" HeaderText="是否停用" />
                      
            </Columns>
        <Toolbars>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
