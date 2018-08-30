<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChgPwd.aspx.cs" Inherits="WasteManagement.ChgPwd" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../res/jqueryuiautocomplete/jquery-ui.min.css" />
    <link rel="stylesheet" href="../res/jqueryuiautocomplete/theme-start/theme.css" />
    <style>
        .ui-autocomplete-loading {
            background: white url('../res/images/ui-anim_basic_16x16.gif') right center no-repeat;
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
        <f:Form ID="Form4" Width="500px" LabelAlign="Right" MessageTarget="Qtip" Title="修改密码"
            BodyPadding="5px" ShowBorder="true" ShowHeader="true" runat="server">
            <Items>
     
                        <f:Panel ID="Panel2" Layout="VBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>                               
                                <f:TextBox ID="txt_vUser" Label="用户名" Width="300px" LabelWidth="100px"  CssClass="marginr" Readonly="true" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="txt_pwdold" Label="原密码" TextMode="Password" Width="300px" LabelWidth="100px"  CssClass="marginr" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="txt_pwd1" Label="新密码" TextMode="Password" Width="300px" LabelWidth="100px"  CssClass="marginr" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="txt_pwd2" Label="新密码确认" TextMode="Password" Width="300px" LabelWidth="100px"  CssClass="marginr" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:Panel>
               
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

