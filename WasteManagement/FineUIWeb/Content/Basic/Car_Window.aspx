﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car_Window.aspx.cs" Inherits="WasteManagement.Content.Basic.Car_Window" %>

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
        <f:Form ID="Form2" Width="450px" LabelAlign="Right" MessageTarget="Qtip" Title="新增车牌"
            BodyPadding="5px" ShowBorder="true" ShowHeader="false" runat="server">
            <Items>
     
                <f:Panel ID="Panel3" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                <Items>
                    <f:HiddenField ID="hfGuid" runat="server"></f:HiddenField>
                    <f:TextBox ID="txt_bm"  Required="true" Label="车牌"  Width="350px"  LabelWidth="100px" runat="server" ShowRedStar="true">
                    </f:TextBox>                                    

                     <f:RadioButtonList ID="CheckStop"  Required="true" ShowRedStar="true" runat="server"  Label="是否停用" >
                        <f:RadioItem Text="是"  Value="True" />
                        <f:RadioItem Text="否"  Value="False"  Selected="true"/>
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



