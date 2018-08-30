<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductOut_Window.aspx.cs" Inherits="WasteManagement.Content.Waste.ProductOut_Window" %>

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
                    <f:DatePicker ID="Date_Start" AutoPostBack="true" Label="日期" LabelWidth="100px" Required="true" ShowLabel="true" ShowRedStar="true" runat="server" Width="350px" >
                    </f:DatePicker>
                      <f:DropDownList ID="drop_Pond"  LabelWidth="100px" Label="罐池号" ShowLabel="true" AutoPostBack="true"   OnSelectedIndexChanged="drop_PondChange"
                        runat="server" Width="220px"></f:DropDownList>
                    <f:TextBox ID="txt_Waste"  Label="成品名称" ShowLabel="true"    Required="true" ShowRedStar="true" LabelWidth="100px" Readonly="true"
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hid_WasteCode" runat="server"></f:HiddenField>

                    <f:NumberBox ID="NB_Amount" Label="数量" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server" NoNegative="true" DecimalPrecision="4">
                        </f:NumberBox>
                    <f:TextBox ID="txt_enterprise"  Label="收货单位" ShowLabel="true"    Required="true" ShowRedStar="true" LabelWidth="100px" 
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_EID" runat="server"></f:HiddenField>

                     <f:TextBox ID="txt_CarNumber" EmptyText="请输入车牌号来搜寻" Label="车牌号" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_CarID" runat="server"></f:HiddenField>

                      <f:TextBox ID="txt_Driver" EmptyText="请输入驾驶员名称来搜寻" Label="驾驶员" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_DriverID" runat="server"></f:HiddenField>
                      <f:TextBox ID="txt_Consignor" EmptyText="请输入发货人名称来搜寻" Label="发货人" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>   
                    <f:HiddenField ID="hf_ConsignorID" runat="server"></f:HiddenField>                                   
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
        

    </form>
    <script src="../../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../res/jqueryuiautocomplete/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var textbox1ID = '<%= txt_Driver.ClientID %>';

        F.ready(function () {
            var textbox1ID = '<%= txt_Driver.ClientID %>';

            var cache = {};

            var textbox2ID = '<%= txt_Consignor.ClientID %>';

            var cache2 = {};

            var textbox3ID = '<%= txt_enterprise.ClientID %>';

            var cache3 = {};

            var textbox4ID = '<%= txt_CarNumber.ClientID %>';

            var cache4 = {};

            $('#' + textbox4ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache4) {
                        response(cache4[term]);
                        return;
                    }

                    $.getJSON("CarNumber.ashx", request, function (data4, status, xhr) {
                        cache4[term] = data4;
                        response(data4);
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

                    $.getJSON("ReceiverEnterprise.ashx", request, function (data3, status, xhr) {
                        cache3[term] = data3;
                        response(data3);
                    });
                }
            });

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


            $('#' + textbox2ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache2) {
                        response(cache2[term]);
                        return;
                    }

                    $.getJSON("Consignor.ashx", request, function (data2, status, xhr) {
                        cache2[term] = data2;
                        response(data2);
                    });
                }
            });
        });

    </script>
</body>
</html>


