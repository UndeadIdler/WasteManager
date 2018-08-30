<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="WasteManagement.Content.User.User" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>用户管理</title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
    <style>
        body.f-body {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1"/>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" BodyPadding="5">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false"
                    Width="600px" RegionPosition="Left" Layout="Fit"
                    runat="server">
                    <Items>
                        <f:Grid ID="GridUser" ShowBorder="true" ShowHeader="true" Title="用户列表" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
                            DataKeyNames="GUID" EnableMultiSelect="false" EnableRowSelectEvent="true" OnRowSelect="GridUser_RowSelect" ShowGridHeader="True">
                            <Columns>    
                                <f:BoundField DataField="GUID" Hidden="true" />
                                <f:RowNumberField EnablePagingNumber="true" />                            
                                <f:BoundField ExpandUnusedSpace="true" DataField="UserName" DataFormatString="{0}" HeaderText="姓名" />
                                <f:BoundField ExpandUnusedSpace="true" DataField="Description" DataFormatString="{0}" HeaderText="角色类型" />
                                <f:BoundField ExpandUnusedSpace="true" DataField="IsStop" DataFormatString="{0}" HeaderText="是否停用" />
                                
                            </Columns>
                            <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                   <%-- <f:Button ID="btnImport" runat="server" Text="导入数据" OnClick="btnImport_Click"></f:Button>--%>
                    <f:Button ID="btnAddUser" runat="server" Text="添加用户" OnClick="btnAddUser_Click" ></f:Button>
                    <f:Button ID="btnUpdUser" runat="server" Text="修改用户" OnClick="btnUpdUser_Click" ></f:Button>
                    <f:Button ID="btnDelUser" runat="server" Text="删除用户" OnClick="btnDelUser_Click" ></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
               </f:Grid>
                         <f:Window ID="Window1" Title="" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="480px" Height="300px" OnClose="Window1_Close">  </f:Window>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="true" ShowHeader="true" Title="角色"
                    Layout="Fit" RegionPosition="Left" runat="server" Width="300px">
                    <Items>
<%--                        <f:RadioButtonList ID="radioRole" Label="角色类型" runat="server" ShowLabel="false" >

                        </f:RadioButtonList>--%>

                        <f:CheckBoxList ID="Role" Label="角色类型" runat="server" ShowLabel="true" ColumnNumber="1">
                           
                            </f:CheckBoxList>
                       
                    
                    </Items>
                     <%--<Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:Button ID="btnAddRole" runat="server" Text="添加角色" OnClick="btnAddRole_Click" ></f:Button>
                    <f:Button ID="btnUpdRole" runat="server" Text="修改角色" OnClick="btnUpdRole_Click" ></f:Button>
                    <f:Button ID="btnDelRole" runat="server" Text="删除角色" OnClick="btnDelRole_Click" ></f:Button>
                            </Items>
                        </f:Toolbar>
                     </Toolbars>--%>
                </f:Region>
                <f:Region ID="Region3" ShowBorder="true" ShowHeader="true"  Title="菜单选择"
                    Layout="Fit"  RegionPosition="Center" runat="server" Width="400px">
                    <Items>                       
                        <f:Tree ID="Tree2" Width="400px" ShowHeader="false" EnableCollapse="true" runat="server"  OnNodeCheck="Tree2_NodeCheck"></f:Tree>  
                    </Items>                    
                </f:Region>
               
            </Regions>

        </f:RegionPanel>

    </form>
</body>
</html>

