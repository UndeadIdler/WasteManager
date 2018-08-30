<%@ Page Title="" Language="C#" MasterPageFile="~/Content/SingleGridContracts.Master" AutoEventWireup="true" CodeBehind="TransferPlan.aspx.cs" Inherits="WasteManagement.Content.Plan.TransferPlan" %>
<%@ MasterType VirtualPath="~/Content/SingleGridContracts.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="mainCPH" runat="server">
   <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="合同号" ShowLabel="true" ID="txt_ContractNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="企业名称" ShowLabel="true" ID="txt_Name"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="废物名称" ShowLabel="true" ID="txt_WasteName"></f:TextBox>
                     <f:DropDownList ID="drop_Status" EmptyText="状态" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="150px"></f:DropDownList>
                </Items>
            </f:FormRow>            
             <f:FormRow>
                 <Items>
                    
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="100px" Label="计划日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="100px" Label="计划日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:Label runat="server" Width="220px" ></f:Label>
                     <f:Button ID="btn_save" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
        DataKeyNames="PlanID" IsDatabasePaging="true" OnSort="Grid1_Sort"
        AllowSorting="true" SortField="PlanID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="PlanID" Hidden="true" />
                <f:BoundField DataField="ProduceID" Hidden="true" />
                <f:BoundField DataField="ContractNumber" SortField="ContractNumber" Width="80px" HeaderText="合同编号" />
                       <f:BoundField DataField="PlanNumber" SortField="PlanNumber" Width="80px" HeaderText="计划编号" />
                       <f:BoundField DataField="ProduceName" SortField="ProduceName" Width="120px" HeaderText="企业名称" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="120px" HeaderText="废物名称" />
                       <f:BoundField DataField="StartDate" SortField="StartDate" DataFormatString="{0:d}" Width="80px" HeaderText="起始时间" />
                       <f:BoundField DataField="EndDate" SortField="EndDate" DataFormatString="{0:d}" Width="80px" HeaderText="结束时间" />
                        <f:BoundField DataField="PlanAmount" SortField="PlanAmount" Width="120px" HeaderText="计划数量（吨）" />
                       <f:BoundField DataField="Status" SortField="Status" Width="120px" HeaderText="状态" />
                       <f:BoundField DataField="ApprovalDate" SortField="ApprovalDate" DataFormatString="{0:d}" Width="80px" HeaderText="批准时间" />                       
                <f:BoundField DataField="Remark" SortField="Remark" Width="100px" HeaderText="备注" />
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

