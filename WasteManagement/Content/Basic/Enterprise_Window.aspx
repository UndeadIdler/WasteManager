<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enterprise_Window.aspx.cs" Inherits="WasteManagement.Content.Basic.Enterprise_Window" %>

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
        <f:Form ID="Form4" Width="1050px" LabelAlign="Right" MessageTarget="Qtip" Title="企业信息"
            BodyPadding="5px" ShowBorder="true" ShowHeader="true" runat="server">
            <Items>
                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Width="1000px" Title="基础信息" runat="server">
                    <Items>
                         <f:Panel ID="Panel3" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="txt_qymc" Label="企业名称" Width="240px" LabelWidth="100px"  CssClass="marginr" Required="true" ShowRedStar="true"  runat="server">
                                </f:TextBox>
                                <f:DropDownList ID="drp_sd" EmptyText="区域" Label="区域" ShowLabel="true" LabelWidth="100px" Required="true" runat="server" Width="240px" ShowRedStar="true" >   

                                </f:DropDownList> 
                                <f:DropDownList ID="drop_type" EmptyText="企业种类" Label="企业种类" ShowLabel="true"  LabelWidth="100px"  Required="true" runat="server" Width="240px" ShowRedStar="true">   

                                </f:DropDownList>                                
                                </Items>
                        </f:Panel> 
                        <f:Panel ID="Panel2" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="txt_jgdm" Label="法人代码" Width="240px" LabelWidth="100px"  CssClass="marginr" runat="server">
                                </f:TextBox>
                                <f:TextBox ID="txt_cname" Label="曾用名" Width="240px" LabelWidth="100px"  CssClass="marginr"   runat="server">
                                </f:TextBox> 
                               <f:TextBox ID="txt_OrgCode" Label="组织机构代码" Width="240px" LabelWidth="100px"  CssClass="marginr"   runat="server">
                                </f:TextBox> 
                                </Items>
                        </f:Panel> 
                                <f:Panel ID="Panel5" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:DatePicker runat="server" ID="txt_createdate" Width="240px" LabelWidth="100px" Label="成立时间" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                                <f:TextBox ID="txt_czhm" Width="240px" LabelWidth="100px"  Label="传真号码" runat="server">
                                    </f:TextBox>
                                  <f:DropDownList ID="drop_industry"  EmptyText="行业类别"  Label="行业类别" ShowLabel="true" runat="server" Width="240px" Required="true" ShowRedStar="true">   </f:DropDownList> 
                                 <f:Label ID="Label2" runat="server" Width="100px" Text=""></f:Label> 
                                
                            
                                </Items>
                        </f:Panel>
                                <f:Panel ID="Panel4" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                                    
                                <f:TextBox ID="txt_dz" Label="地址" Width="480px" LabelWidth="100px"  CssClass="marginr"  runat="server">
                                </f:TextBox>
                                   <f:TextBox ID="txt_Postcode" Width="240px" LabelWidth="100px" Required="true"  Label="邮政编码" runat="server">
                                    </f:TextBox>
                            </Items>
                        </f:Panel>
                        
                        <f:Panel ID="Panel1" Layout="HBox" BoxConfigAlign="Stretch" CssClass="formitem" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:TextBox ID="txt_frdb" Width="240px" LabelWidth="100px" Required="true" ShowRedStar="true" Label="法人" runat="server">
                                </f:TextBox>
                               
                                  <f:TextBox ID="txt_email" Width="240px" LabelWidth="100px"   Label="电子邮箱" runat="server">
                                </f:TextBox>
                                 <f:TextBox ID="txt_tel1" Width="240px" LabelWidth="100px"   Label="固定电话" runat="server">
                                </f:TextBox>
                                 <f:TextBox ID="txt_mobile1" Width="240px" LabelWidth="100px"   Label="手机" runat="server">
                                </f:TextBox>
                            </Items>
                        </f:Panel>                       
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
 
</body>
</html>

