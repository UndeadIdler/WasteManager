<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="Enterprise.aspx.cs" Inherits="WasteManagement.Content.Basic.Enterprise" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="250px" LabelWidth="100px" Label="法人代码" ID="ser_vDirectiveNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="250px" LabelWidth="100px" Label="企业名称" ShowLabel="true" ID="ser_vSaveNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="250px" LabelWidth="100px" Label="组织机构代码" ShowLabel="true" ID="txt_OrgCode"></f:TextBox>
                    
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="DropDownList1" EmptyText="区域" ShowLabel="true" AutoPostBack="true" Label="区域"
                        runat="server" Width="250px">
                    </f:DropDownList>
                    <f:DropDownList ID="DropDownList2" EmptyText="企业类型" ShowLabel="true" AutoPostBack="true"  Label="企业类型"
                        runat="server" Width="250px">
                    </f:DropDownList>
                   
                  <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>

                </Items>
                </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="EnterpriseID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="EnterpriseID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="EnterpriseID" Hidden="true" />
                <f:BoundField DataField="Name" SortField="Name" Width="120px" HeaderText="企业名称" />
                       <f:BoundField DataField="PastName" SortField="PastName" Width="80px" HeaderText="曾用名" />
                       <f:BoundField DataField="LawManCode" SortField="LawManCode" Width="100px" HeaderText="法人代码" />
                       <f:BoundField DataField="AreaCode" SortField="AreaCode" Width="100px" HeaderText="行政区域代码" />
                        <f:BoundField DataField="OrganizationCode" SortField="OrganizationCode" Width="100px" HeaderText="组织机构代码" />
                       <f:BoundField DataField="SetUpDate" SortField="SetUpDate" Width="100px" HeaderText="建厂时间" />                       
              <f:BoundField DataField="Address" SortField="Address" Width="100px" HeaderText="地址" />
                      
                <f:BoundField DataField="Remark" SortField="" Width="100px" HeaderText="备注" />
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