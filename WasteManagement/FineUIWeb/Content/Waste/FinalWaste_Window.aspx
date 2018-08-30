<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalWaste_Window.aspx.cs" Inherits="WasteManagement.Content.Waste.FinalWaste_Window" %>

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
        <f:Form ID="Form4" Width="1050px" LabelAlign="Right" MessageTarget="Qtip" Title="新危废记录"
            BodyPadding="5px" ShowBorder="true" ShowHeader="true" runat="server">
            <Items>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Width="1000px" Title="基础信息" runat="server">
                    <Items>
                         <f:Panel ID="Panel3" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                             <f:TextBox ID="txt_LogNumber" Label="记录编号" Readonly="true" Width="300px" LabelWidth="85px"  CssClass="marginr" Required="true" ShowRedStar="true"  runat="server">
                                </f:TextBox>
                            <f:DatePicker ID="FDate" Label="日期" Width="300px" LabelWidth="85px" CssClass="marginr" Required="true" ShowRedStar="true"  runat="server" DateFormatString="yyyy-MM-dd">
                            </f:DatePicker>
                            <f:DropdownList ID="drop_Man" Label="填写人" Width="300px" LabelWidth="85px" CssClass="marginr" Required="true" ShowRedStar="true"  runat="server" >
                            </f:DropdownList>

                                </Items>
                        </f:Panel> 
                        

                        </Items>
                        </f:GroupPanel>
                    <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Width="1000px" Title="新危废" runat="server">
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

     
</body>
</html>

