<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contract_Window.aspx.cs" Inherits="WasteManagement.Content.Plan.Contract_Window" %>

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

        /*.mytable td.x-table-layout-cell {
            padding: 5px;
        }

        .mytable td.f-layout-table-cell {
            padding: 5px;
        }*/
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"/>  
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增合同"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server" EnableCollapse="true">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <f:HiddenField ID="hfGuid" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_ContractNumber" Label="合同编号" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="txt_Enterprise" EmptyText="请输入企业名称来搜寻" Label="企业名称" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                      <f:DropDownList ID="drop_handle" EmptyText="处理企业名称" Label="处理企业名称" ShowLabel="true" AutoPostBack="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:DropDownList>
                    <f:DropDownList ID="drop_Waste" EmptyText="废物名称" Label="废物名称" ShowLabel="true" AutoPostBack="true"   Required="true" ShowRedStar="true" LabelWidth="100px"
                        runat="server" Width="350px"></f:DropDownList>
                    <f:DatePicker ID="Date_Start" AutoPostBack="true" Label="起始日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>
                    <f:DatePicker ID="Date_End" AutoPostBack="true" Label="结束日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" CompareControl="Date_Start" CompareOperator="GreaterThanEqual">
                    </f:DatePicker>
                    <f:NumberBox ID="NB_Amount" Label="合同数量" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server" NoNegative="true" DecimalPrecision="4">
                        </f:NumberBox>
                    <f:DatePicker ID="Date_Sign" AutoPostBack="true" Label="签约日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>                    
                   <f:TextBox ID="txt_Remark" Label="说明"  Width="350px" LabelWidth="100px"  CssClass="marginr" runat="server">
                    </f:TextBox>
                </Items>
            </f:Panel>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Center" Position="Bottom">
                    <Items>
                        <f:Button ID="btn_save" Text="保存" CssClass="btn" ValidateForms="Form2"  OnClick="btn_save_Click"  runat="server">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form> 



    </form>

    <script src="../../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../res/jqueryuiautocomplete/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var textbox1ID = '<%= txt_Enterprise.ClientID %>';
        
        F.ready(function () {
            var textbox1ID = '<%= txt_Enterprise.ClientID %>';

            var cache = {};

            $('#' + textbox1ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache) {
                        response(cache[term]);
                        return;
                    }

                    $.getJSON("EnterpriseName.ashx", request, function (data, status, xhr) {
                        cache[term] = data;
                        response(data);
                    });
                }
            });

        });

    </script>
</body>
</html>

