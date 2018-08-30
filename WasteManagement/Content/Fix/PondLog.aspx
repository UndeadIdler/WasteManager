<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="PondLog.aspx.cs" Inherits="WasteManagement.Content.Fix.PondLog" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="罐池名" ID="ser_vDirectiveNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="贮存品名称" ShowLabel="true" ID="ser_vSaveNumber"></f:TextBox>
                  <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                </Items>
            </f:FormRow>            
            
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="LogID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="LogID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="LogID" Hidden="true" />
                        <f:BoundField DataField="SourceName" SortField="SourceName" Width="80px" HeaderText="来源罐池" />
                <f:BoundField DataField="ToName" SortField="ToName" Width="120px" HeaderText="导入罐池" />
                <f:BoundField DataField="WasteCode" SortField="WasteCode" Width="120px" HeaderText="物料代码" />
                <f:BoundField DataField="WasteName" SortField="WasteName" Width="120px" HeaderText="物料名称" />
                <f:BoundField DataField="Amount" SortField="Amount" Width="120px" HeaderText="倒库量" />      
            </Columns>
        <Toolbars>
        </Toolbars>
    </f:Grid>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="headCPH">
    <meta name="sourcefiles" content="SingleGridContracts.Master;ISingleGridPage.cs" />
</asp:Content>
