<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferPlan_Window.aspx.cs" Inherits="WasteManagement.Content.Plan.TransferPlan_Window" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="res/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="res/jqueryuiautocomplete/jquery-ui.min.css" />
    <link rel="stylesheet" href="res/jqueryuiautocomplete/theme-start/theme.css" />
    <style>
        .ui-autocomplete-loading {
            background: white url('res/images/ui-anim_basic_16x16.gif') right center no-repeat;
        }

        .mytable td.x-table-layout-cell {
            padding: 5px;
        }

        .mytable td.f-layout-table-cell {
            padding: 5px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"/>  
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增计划"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <f:HiddenField ID="hfGuid" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_PlanNumber"   Label="计划编号" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:TriggerBox ID="txt_source" ShowLabel="true" Label="合同编号" TriggerIcon="Search" LabelWidth="100px" OnTriggerClick="txt_source_TriggerClick"  runat="server" Width="480px" EmptyText="请点右侧按钮从中选择企业，双击选中。" Required="true">
                           </f:TriggerBox>
                    <f:HiddenField ID="hid_htid" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_Waste" EmptyText="废物名称" Label="废物名称" ShowLabel="true"    Required="true" ShowRedStar="true" LabelWidth="100px" Readonly="true"
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hid_WasteCode" runat="server"></f:HiddenField>
                    <f:DatePicker ID="Date_Start" AutoPostBack="true" Label="起始日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>
                    <f:DatePicker ID="Date_End" AutoPostBack="true" Label="结束日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" CompareControl="Date_Start" CompareOperator="GreaterThanEqual">
                    </f:DatePicker>
                    <f:NumberBox ID="NB_Amount" Label="计划数量" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server" NoNegative="true" DecimalPrecision="4">
                        </f:NumberBox>
                    <f:DatePicker ID="Date_Sign" AutoPostBack="true" Label="批准日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>                    
                     <f:TextBox ID="txt_Remark" EmptyText="说明" Label="说明" ShowLabel="true"   LabelWidth="100px" 
                        runat="server" Width="350px"></f:TextBox>
                </Items>
            </f:Panel>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Center" Position="Bottom">
                    <Items>
                        <f:Button ID="btn_save" Text="保存" CssClass="btn"  ValidateForms="Form1"  OnClick="btn_save_Click" runat="server" ValidateMessageBox="false" >
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form> 
        <f:Window ID="Window_DepartQuery"  Title="合同选择" BodyPadding="10px" IsModal="true" Hidden="true" EnableMaximize="true" EnableResize="true" Target="Top" Width="1120px" Height="500px" runat="server">
                     <Items>
                      <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="95px" Label="合同号" ID="txt_ContractNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="95px" Label="企业名称" ShowLabel="true" ID="txt_Name"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="95px" Label="废物名称" ShowLabel="true" ID="txt_WasteName"></f:TextBox>
                    <%--<f:DropDownList ID="drop_Status" EmptyText="状态" LabelWidth="95px" Label="状态" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>--%>
                     
                </Items>
            </f:FormRow>            
             <f:FormRow>
                 <Items>
                    
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="95px" Label="合同日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="95px" Label="合同日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <%--<f:Label runat="server" Width="220px" ></f:Label>--%>
                     <f:Button ID="btn_Search" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="ContractID,ContractNumber" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange"
        AllowSorting="true" SortField="ContractID" SortDirection="ASC" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true">
                    <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="ContractID" Hidden="true" />
                <f:BoundField DataField="EnterpriseID" Hidden="true" />
                     <f:BoundField DataField="SaleID" Hidden="true" />   
                <f:BoundField DataField="ContractNumber" SortField="ContractNumber" Width="80px" HeaderText="合同编号" />
                       <f:BoundField DataField="ProduceName" SortField="ProduceName" Width="120px" HeaderText="企业名称" />
                       <f:BoundField DataField="WasteName" SortField="WasteName" Width="120px" HeaderText="废物名称" />
                       <f:BoundField DataField="StartDate" SortField="StartDate" Width="80px" HeaderText="起始时间" />
                       <f:BoundField DataField="EndDate" SortField="EndDate" Width="80px" HeaderText="结束时间" />
                        <f:BoundField DataField="Amount" SortField="Amount" Width="120px" HeaderText="合同数量（吨）" />
                       <f:BoundField DataField="SignDate" SortField="SignDate" Width="80px" HeaderText="签约时间" /> 
                       <f:BoundField DataField="StatusName" SortField="StatusName" Width="80px" HeaderText="状态" />                      
                <f:BoundField DataField="Remark" SortField="Remark" Width="100px" HeaderText="备注" />
            </Columns>
    </f:Grid>
   </Items>        </f:Window>

    </form>
</body>
</html>
