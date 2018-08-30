<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitor_Window.aspx.cs" Inherits="WasteManagement.Content.State.Monitor_Window" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../../res/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../res/jqueryuiautocomplete/jquery-ui.min.css" />
    <link rel="stylesheet" href="../../res/jqueryuiautocomplete/theme-start/theme.css" />
    <style>
        .ui-autocomplete-loading {
            background: white url('../../res/images/ui-anim_basic_16x16.gif') right center no-repeat;
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
        <f:PageManager ID="PageManager1" runat="server" />  
        <f:Form ID="Form4" Width="1050px" LabelAlign="Right" MessageTarget="Qtip" Title="入厂特性分析"
            BodyPadding="5px" ShowBorder="true" ShowHeader="true" runat="server">
            <Items>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Width="1000px" Title="基础信息" runat="server">
                    <Items>
                         <f:Panel ID="Panel3" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:DropDownList ID="drop_PositionName" Label="监测位置" Width="240px" LabelWidth="85px"  CssClass="marginr" Required="true" ShowRedStar="true"  runat="server">
                                </f:DropDownList>
                                <f:DatePicker runat="server" ID="Date" Width="240px" LabelWidth="85px" Label="日期" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                                <f:TextBox ID="txt_Analysis" EmptyText="请输入分析员名称来搜寻" Label="分析员" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="100px"  
                        runat="server" Width="350px"></f:TextBox>
                    <f:HiddenField ID="hf_AnalysisID" runat="server"></f:HiddenField>
                                </Items>
                        </f:Panel> 
                        <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="监测结果（双击编辑）" EnableCollapse="true" Width="850px"
            runat="server"  AllowCellEditing="true" ClicksToEdit="2" OnPreDataBound="Grid1_PreDataBound" DataKeyNames="ResultID,ItemName,Result"
            EnableCheckBoxSelect="true" >
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:Button ID="btnNew" Text="新增数据" Icon="Add" EnablePostBack="false" runat="server" >
                        </f:Button>
                        <f:Button ID="btnDelete" Text="删除选中行" Icon="Delete" EnablePostBack="false" runat="server">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:TemplateField ColumnID="Number" Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </f:TemplateField>
                <f:BoundField Width="100px" ColumnID="ResultID" DataField="ResultID" Hidden="true">

                </f:BoundField>
                <f:RenderField Width="100px" ColumnID="ItemName" DataField="ItemName"
                    HeaderText="监测项目">
                    <Editor>
                        <f:DropDownList ID="drop_Item"  runat="server">

                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="600px" ColumnID="Result" DataField="Result" FieldType="Float" 
                    ExpandUnusedSpace="true" HeaderText="分析结果">
                    <Editor>
                        <f:NumberBox ID="tbxEditorResult" Required="true" runat="server" DecimalPrecision="4">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                <f:LinkButtonField ColumnID="Delete" Width="80px" EnablePostBack="false"
                    Icon="Delete" />
            </Columns>
        </f:Grid>

                        </Items>
                        </f:GroupPanel>                                       
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Center" Position="Bottom">
                    <Items>
                        <f:Button ID="btn_save" Text="保存" CssClass="btn" ValidateForms="Form1"  OnClick="btn_save_Click" ValidateMessageBox="false" runat="server">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form> 
    </form>

     <script src="../../res/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../res/jqueryuiautocomplete/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var textbox1ID = '<%= txt_Analysis.ClientID %>';

        F.ready(function () {
            var textbox1ID = '<%= txt_Analysis.ClientID %>';

            var cache = {};


            $('#' + textbox1ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache) {
                        response(cache[term]);
                        return;
                    }

                    $.getJSON("AnalysisMan.ashx", request, function (data, status, xhr) {
                        cache[term] = data;
                        response(data);
                    });
                }
            });


        });

    </script>
</body>
</html>


