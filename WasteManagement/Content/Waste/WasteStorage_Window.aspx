<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WasteStorage_Window.aspx.cs" Inherits="WasteManagement.Content.Waste.WasteStorage_Window" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../../res/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../res/jqueryuiautocomplete/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../res/jqueryuiautocomplete/theme-start/theme.css" />
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
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增入库表"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <f:HiddenField ID="hf_StoreID" runat="server"></f:HiddenField>
                    <f:NumberBox ID="txt_BillID" Label="联单ID"  Readonly="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:NumberBox>
                    <f:TextBox ID="txt_BillNumber" Label="联单编号" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:TriggerBox ID="tb_PlanNumber"  Required="true" ShowRedStar="true" ShowLabel="true" Label="计划编号" TriggerIcon="Search" LabelWidth="100px" OnTriggerClick="txt_source_TriggerClick"  runat="server" Width="480px" EmptyText="请点右侧按钮从中选择企业，双击选中。">
                           </f:TriggerBox>
                    <f:HiddenField ID="hf_PlanID" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_enterprise"  Label="企业名称" ShowLabel="true"    Required="true" ShowRedStar="true" LabelWidth="100px" Readonly="true"
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_EID" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_Waste"  Label="废物名称" ShowLabel="true"    Required="true" ShowRedStar="true" LabelWidth="100px" Readonly="true"
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hid_WasteCode" runat="server"></f:HiddenField>
                      <f:DropDownList ID="drop_Pond" EmptyText="请先选计划编号" LabelWidth="100px" Label="罐池号" ShowLabel="true" AutoPostBack="true"  
                        runat="server" Width="220px"></f:DropDownList>
                    <f:DatePicker ID="Date_Start" AutoPostBack="true" Label="日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>
                    <f:NumberBox ID="NB_Amount" Label="数量" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server" NoNegative="true" DecimalPrecision="4">
                        </f:NumberBox>
                        <f:TextBox ID="txt_CarNumber" EmptyText="请输入车牌号来搜寻" Label="车牌号" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_CarID" runat="server"></f:HiddenField>
                      <f:TextBox ID="txt_Driver" EmptyText="请输入驾驶员名称来搜寻" Label="驾驶员" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_DriverID" runat="server"></f:HiddenField>
                      <f:TextBox ID="txt_Receiver" EmptyText="请输入签收人名称来搜寻" Label="签收人" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>   
                    <f:HiddenField ID="hf_ReceiverID" runat="server"></f:HiddenField>                                   
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
        <f:Window ID="Window_DepartQuery"  Title="计划选择" BodyPadding="10px" IsModal="true" Hidden="true" EnableMaximize="true" EnableResize="true" Target="Top" Width="1120px" Height="500px" runat="server">
                     <Items>
                      <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" Title="条件查询" runat="server" Width="1120px" BodyPadding="5">
         <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="合同号" ShowLabel="true" ID="txt_ContractNumber"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="企业名称" ShowLabel="true" ID="txt_Name"></f:TextBox>
                    <f:TextBox runat="server" Width="220px" LabelWidth="100px" Label="废物名称" ShowLabel="true" ID="txt_WasteName"></f:TextBox>
                </Items>
            </f:FormRow>            
             <f:FormRow>
                 <Items>
                    
                     <f:DatePicker runat="server" ID="DateStart" Width="220px" LabelWidth="100px" Label="计划日期大于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    <f:DatePicker runat="server" ID="DateEnd"  Width="220px" LabelWidth="100px" Label="计划日期小于" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                     <f:Button ID="Button1" Text="查询" CssClass="btn" ValidateForms="Form1"  OnClick="btn_Search_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                 </Items>
             </f:FormRow>
        </Rows>
    </f:Form>
    <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="PlanID" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange"
        AllowSorting="true" SortField="PlanID" SortDirection="ASC" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true">
                    <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="PlanID" Hidden="true" />
                <f:BoundField DataField="ContractID" Hidden="true" />
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
    </f:Grid>
   </Items>        </f:Window>

    </form>
    <script src="../../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../res/jqueryuiautocomplete/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var textbox1ID = '<%= txt_Driver.ClientID %>';

        F.ready(function () {
            var textbox1ID = '<%= txt_Driver.ClientID %>';

            var cache = {};

            var textbox2ID = '<%= txt_Receiver.ClientID %>';

            var cache2 = {};

            var textbox3ID = '<%= txt_CarNumber.ClientID %>';

            var cache3 = {};

            $('#' + textbox1ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache) {
                        response(cache[term]);
                        return;
                    }

                    $.getJSON("DriverName.ashx", request, function (data, status, xhr) {
                        cache[term] = data;
                        response(data);
                    });
                }
            });


            $('#' + textbox3ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache3) {
                        response(cache3[term]);
                        return;
                    }

                    $.getJSON("CarNumber.ashx", request, function (data3, status, xhr) {
                        cache3[term] = data3;
                        response(data3);
                    });
                }
            });


            $('#' + textbox2ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache2) {
                        response(cache2[term]);
                        return;
                    }

                    $.getJSON("ReceiverName.ashx", request, function (data2, status, xhr) {
                        cache2[term] = data2;
                        response(data2);
                    });
                }
            });
        });

    </script>
</body>
</html>

