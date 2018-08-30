<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="WasteManagement.Content.Basic.Car" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>车辆管理</title>
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

                        <f:Grid ID="GridUser" ShowBorder="true" ShowHeader="true" Title="车辆列表" runat="server" EnableCheckBoxSelect="True" CheckBoxSelectOnly="true"
                            DataKeyNames="ID" EnableMultiSelect="false" EnableRowSelectEvent="true"  ShowGridHeader="True"
                            IsDatabasePaging="true" EnableCollapse="false" PageSize="10" AllowPaging="true">
                            <Columns>    
                               <f:RowNumberField />
                <f:BoundField DataField="ID" Hidden="true" />                
                <f:BoundField DataField="CarNumber" SortField="CarNumber" Width="120px" HeaderText="车牌" />
                <f:BoundField DataField="IsStop" SortField="IsStop" Width="120px" HeaderText="是否停用" />
            </Columns>
                            <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                   <%-- <f:Button ID="btnImport" runat="server" Text="导入数据" OnClick="btnImport_Click"></f:Button>--%>
                    <f:Button ID="btnAddUser" runat="server" Text="添加车辆" OnClick="btnAddUser_Click" ></f:Button>
                    <f:Button ID="btnUpdUser" runat="server" Text="修改车辆" OnClick="btnUpdUser_Click" ></f:Button>
                    <f:Button ID="btnDelUser" runat="server" Text="删除车辆" OnClick="btnDelUser_Click" ></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
               </f:Grid>
                         <f:Window ID="Window1" Title="" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="480px" Height="300px" OnClose="Window1_Close">  </f:Window>
                    </Items>
                </f:Region>
                <f:Region ID="Region2" ShowBorder="true" ShowHeader="true" Title="驾驶员"
                    Layout="Fit" RegionPosition="Left" runat="server" Width="600px">
                    <Items>

                        <f:Grid ID="Grid1" EnableCollapse="false" PageSize="10" ShowBorder="true" ShowHeader="false" Title="驾驶员列表"
        BoxFlex="1"
        AllowPaging="true" runat="server" EnableCheckBoxSelect="True"
        DataKeyNames="ID" IsDatabasePaging="true" 
        AllowSorting="true" SortField="ID" SortDirection="ASC">
                   <Columns>
                <f:RowNumberField />
                <f:BoundField DataField="ID" Hidden="true" />                
                <f:BoundField DataField="RealName" SortField="RealName" Width="80px" HeaderText="驾驶员" />
                <f:BoundField DataField="IsStop" SortField="IsStop" Width="120px" HeaderText="是否停用" />
            </Columns>
                            <Toolbars>
            <f:Toolbar ID="Toolbar3" runat="server">
                <Items>
                    <f:Button ID="btnAddRole" runat="server" Text="添加" OnClick="btnAddRole_Click" ></f:Button>
                    <f:Button ID="btnUpdRole" runat="server" Text="修改" OnClick="btnUpdRole_Click" ></f:Button>
                    <f:Button ID="btnDelRole" runat="server" Text="删除" OnClick="btnDelRole_Click" ></f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
               </f:Grid>
                       
                    
                    <f:Window ID="Window2" Title="" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="480px" Height="300px" OnClose="Window2_Close">  </f:Window>
                    </Items>
                </f:Region>
               
            </Regions>

        </f:RegionPanel>

    </form>
</body>
</html>

