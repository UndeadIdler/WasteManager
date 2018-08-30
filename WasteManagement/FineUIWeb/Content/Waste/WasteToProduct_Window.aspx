<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WasteToProduct_Window.aspx.cs" Inherits="WasteManagement.Content.Waste.WasteToProduct_Window" %>

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
        <f:PageManager ID="PageManager1" runat="server" />  
        <f:Form ID="Form4" Width="1050px" LabelAlign="Right" MessageTarget="Qtip" Title="废物出库和成品入库"
            BodyPadding="5px" ShowBorder="true" ShowHeader="true" runat="server">
            <Items>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Width="1000px" Title="废酸出库" runat="server">
                    <Items>
                         <f:Panel ID="Panel3" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
<%--                            <f:FormRow>
                            <Items>--%>
                            <f:DatePicker ID="FDate" Label="日期" Width="300px" LabelWidth="85px" CssClass="marginr" Required="true" ShowRedStar="true"  runat="server" DateFormatString="yyyy-MM-dd">
                            </f:DatePicker>
                            <f:DropdownList ID="drop_Pond" Label="罐池号" Width="300px" LabelWidth="85px" CssClass="marginr" Required="true" ShowRedStar="true"  runat="server"  OnSelectedIndexChanged="drop_Pond_Change" AutoPostBack="true">
                            </f:DropdownList>
                             <f:TextBox ID="txt_WasteName" Label="废物名称" Readonly="true" Width="300px" LabelWidth="85px"  CssClass="marginr" Required="true" ShowRedStar="true"  runat="server">
                                </f:TextBox>
<%--                            </Items>
                            </f:FormRow>
                            <f:FormRow>
                            <Items>--%>
                        
<%--                            </Items>
                            </f:FormRow>--%>

                                </Items>
                        </f:Panel> 
                        <f:Panel ID="Panel1" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                        <Items>
                                <f:NumberBox ID="NB_Amount" Label="数量" Width="300px" LabelWidth="85px"  CssClass="marginr" Required="true" ShowRedStar="true"  runat="server">
                                </f:NumberBox>
                                <%--<f:DatePicker runat="server" ID="Date" Width="240px" LabelWidth="85px" Label="日期" DateFormatString="yyyy-MM-dd"></f:DatePicker>--%>
                                <f:TextBox ID="txt_HandleMan" EmptyText="请输入处置人名称来搜寻" Label="处置人" ShowLabel="true"  Required="true" ShowRedStar="true" LabelWidth="85px"  
                        runat="server" Width="300px"></f:TextBox>
                    <f:HiddenField ID="hf_HandelManID" runat="server"></f:HiddenField>
                                <f:TextBox ID="txt_Receiver" EmptyText="请输入签收人名称来搜寻" Label="签收人员" ShowLabel="true"  LabelWidth="90px"  
                        runat="server" Width="305px"></f:TextBox>
                    <f:HiddenField ID="hf_ReceiverID" runat="server"></f:HiddenField>    
                        </Items>

                        </f:Panel>

                        </Items>
                        </f:GroupPanel>
                             <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Width="1000px" Title="成品入库" runat="server">
                    <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="成品（双击编辑）" EnableCollapse="true" Width="850px"
            runat="server"  AllowCellEditing="true" ClicksToEdit="2" OnPreDataBound="Grid1_PreDataBound" DataKeyNames="DetailID,WasteName,Amount,Name"
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
                <f:BoundField Width="100px" ColumnID="DetailID" DataField="DetailID" Hidden="true">

                </f:BoundField>
                <f:RenderField Width="300px" ColumnID="WasteName" DataField="WasteName"
                    HeaderText="名称">
                    <Editor>
                        <f:DropDownList ID="drop_Product"  runat="server">

                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="200px" ColumnID="Amount" DataField="Amount" FieldType="Float"
                    ExpandUnusedSpace="true" HeaderText="数量">
                    <Editor>
                        <f:NumberBox ID="tbxEditorResult" Required="true" runat="server" DecimalPrecision="4">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="200px" ColumnID="Name" DataField="Name"
                    HeaderText="罐池号">
                    <Editor>
                        <f:DropDownList ID="drop_Pond2"  runat="server">

                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:LinkButtonField ColumnID="Delete" Width="80px" EnablePostBack="false"
                    Icon="Delete" />
            </Columns>
        </f:Grid>

                            
                    </Items>
                    </f:GroupPanel> 
                    <%--<f:GroupPanel ID="GroupPanel3" Layout="Anchor" Width="1000px" Title="新危废" runat="server">
                    <Items>
                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="true" Title="新危废（双击编辑）" EnableCollapse="true" Width="850px"
            runat="server"  AllowCellEditing="true" ClicksToEdit="2" OnPreDataBound="Grid2_PreDataBound" DataKeyNames="FWID,WasteName,Result"
            EnableCheckBoxSelect="true" >
            <Toolbars>
                <f:Toolbar ID="Toolbar3" runat="server">
                    <Items>
                        <f:Button ID="btnNew2" Text="新增数据" Icon="Add" EnablePostBack="false" runat="server" >
                        </f:Button>
                        <f:Button ID="btnDelete2" Text="删除选中行" Icon="Delete" EnablePostBack="false" runat="server">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill2" runat="server">
                        </f:ToolbarFill>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:TemplateField ColumnID="Number" Width="60px">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </f:TemplateField>
                <f:BoundField Width="100px" ColumnID="FWID" DataField="FWID" Hidden="true">

                </f:BoundField>
                <f:RenderField Width="200px" ColumnID="WasteName2" DataField="WasteName"
                    HeaderText="名称">
                    <Editor>
                        <f:DropDownList ID="drop_NewWaste"  runat="server">

                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="200px" ColumnID="Result2" DataField="Result" FieldType="Float"
                    ExpandUnusedSpace="true" HeaderText="数量">
                    <Editor>
                        <f:NumberBox ID="NB_NewWaste" Required="true" runat="server" DecimalPrecision="4">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                <f:LinkButtonField ColumnID="Delete2" Width="80px" EnablePostBack="false"
                    Icon="Delete" />
            </Columns>
        </f:Grid>
                    </Items>
                    </f:GroupPanel>--%>
                                                     
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
        var textbox1ID = '<%= txt_HandleMan.ClientID %>';

        F.ready(function () {
            var textbox1ID = '<%= txt_HandleMan.ClientID %>';

            var cache = {};

            var textbox2ID = '<%= txt_Receiver.ClientID %>';

            var cache2 = {};


            $('#' + textbox1ID + ' input').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var term = request.term;
                    if (term in cache) {
                        response(cache[term]);
                        return;
                    }

                    $.getJSON("HandleManName.ashx", request, function (data, status, xhr) {
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
