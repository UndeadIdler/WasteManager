<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pond_Window.aspx.cs" Inherits="WasteManagement.Content.Basic.Pond_Window" %>

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
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增罐池"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <%--<f:HiddenField ID="hfUsed" runat="server"></f:HiddenField>--%>
                    <f:TextBox ID="txt_name" Label="罐池名" Required="true" Width="350px" LabelWidth="100px"  CssClass="marginr" runat="server">
                    </f:TextBox>
                    <f:NumberBox ID="txt_areacode" Label="容量" Required="true" Width="350px" LabelWidth="100px" ShowRedStar="true"  CssClass="marginr" runat="server">
                    </f:NumberBox>
                    <f:DropdownList ID="drop_Waste"  Label="贮存品" ShowLabel="true" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server">
                    </f:DropdownList>        
                    <f:NumberBox ID="Orderid" Label="编号" Required="true" Width="350px" ShowRedStar="true" LabelWidth="100px" runat="server">
                        </f:NumberBox>
                     <f:RadioButtonList ID="CheckStop"  Required="true" ShowRedStar="true" runat="server"  Label="是否停用" >
                        <f:RadioItem Text="是"  Value="1" />
                        <f:RadioItem Text="否"  Value="0" Selected="true"/>
                    </f:RadioButtonList>

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


