<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PondLog_Window.aspx.cs" Inherits="WasteManagement.Content.Fix.PondLog_Window" %>

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
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增区域"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <f:HiddenField ID="hfGuid" runat="server"></f:HiddenField>
                    <f:DropdownList ID="drop_Waste" Label="物料名称"  Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server" OnSelectedIndexChanged="drop_Waste_SelectedIndexChanged" AutoPostBack="true">
                    </f:DropdownList>
                    <f:DropdownList ID="drop_Source" Label="来源罐池名"  Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server" OnSelectedIndexChanged="drop_Source_SelectedIndexChanged" AutoPostBack="true">
                    </f:DropdownList>
                    <f:TextBox ID="txt_Capacity1" Label="容量" Readonly="true" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="txt_Used1" Label="已使用" Readonly="true" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:DropdownList ID="drop_To" Label="导入罐池名"  Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server" OnSelectedIndexChanged="drop_To_SelectedIndexChanged" AutoPostBack="true">
                    </f:DropdownList>
                    <f:TextBox ID="txt_Capacity2" Label="容量" Readonly="true" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:TextBox ID="txt_Used2" Label="已使用" Readonly="true" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:NumberBox ID="NB_New" Label="倒库量" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:NumberBox>                                                          

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
</body>
</html>


