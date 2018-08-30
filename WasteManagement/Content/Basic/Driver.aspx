<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Driver.aspx.cs" Inherits="WasteManagement.Content.Basic.Driver" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="200px" LabelWidth="100px" Label="驾驶员姓名" ShowLabel="true" ID="ser_vSaveNumber"></f:TextBox>
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
                <f:BoundField DataField="CarNumber" SortField="CarNumber" Width="120px" HeaderText="车牌" />
                <f:BoundField DataField="RealName" SortField="RealName" Width="80px" HeaderText="常用驾驶员" />
                <f:BoundField DataField="IsStop" SortField="IsStop" Width="120px" HeaderText="是否停用" />
            </Columns>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:Button ID="btn_Export" runat="server" Text="导出" OnClick="btn_Export_Click" EnableAjax="false" DisableControlBeforePostBack="false"></f:Button>
                    <%--<f:Button ID="btnSelectColumns" runat="server" Text="选择需要导出的列（注意Window控件的EnableAjax属性）" EnablePostBack="false">
                --%></Items>
            </f:Toolbar>
        </Toolbars>
    </f:Grid>
<%--    <f:Window ID="Window2" Title="选择需要导出的列" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Top" EnableResize="true" runat="server" OnClose="Window2_Close"
            IsModal="true" Width="450px" Height="250px" EnableAjax="false">
        </f:Window>--%>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
