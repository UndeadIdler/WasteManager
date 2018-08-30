<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WasteManagement._Default" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>浙江省危废经营企业量化管理系统</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <meta name="Title" content="" />
    <meta name="Description" content="" />
    <meta name="Keywords" content="" />
    <style>
        #header {
            position: relative;
            border-bottom-width: 2px;
            border-bottom-style: solid;
            padding: 8px 12px 6px;
        }

            #header a.logo {
                display: inline-block;
                margin-right: 5px;
            }

            #header a.title {
                font-weight: bold;
                font-size: 24px;
                text-decoration: none;
                line-height: 30px;
                color: #fff;
            }


            #header .themeroller {
                position: absolute;
                top: 10px;
                right: 10px;
            }

                #header .themeroller a {
                    font-size: 20px;
                    text-decoration: none;
                    line-height: 30px;
                    color: #fff;
                }



        .f-theme-neptune #header {
            background-color: #005999;
            border-bottom: 1px solid #1E95EC;
        }

            .f-theme-neptune #header .title a {
                color: #fff;
            }

        .f-theme-blue #header {
            background-color: #004BA8;
            border-bottom: 1px solid #034699;
        }

            .f-theme-blue #header .title a {
                color: #fff;
            }

        .f-theme-gray #header {
            background-color: #d3d3d3;
            border-bottom: 1px solid #bab9b9;
        }

            .f-theme-gray #header .title a {
                color: #333;
            }

        .f-theme-access #header {
            background-color: #343b48;
            border-bottom: 1px solid #1f232b;
        }

            .f-theme-access #header .title a {
                color: #fff;
            }



        #logo {
            position: absolute;
            bottom: 20px;
            right: 0;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            opacity: 0.8;
            z-index: 100000;
        }


        .f-theme-access .maincontent .x-panel-body {
            background-image: none;
        }

        .isnew {
            color: red;
        }

        .bottomtable {
            width: 100%;
            font-size: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" ShowHeader="false"
                    Position="Top" Layout="Fit" runat="server">
                    <Content>
                        <div id="header">
                            <table>
                                <tr>
                                    <td>                                        
                                    </td>
                                    <td>
                                        <a class="title" href="./default.aspx">浙江省危废经营企业量化管理系统</a>
                                    </td>
                                </tr>
                            </table>
                            <div class="themeroller">
                                <f:Button ID="btnRelogin" OnClick="btnRelogin_Click"  IconUrl="~/res/icon/relogin.gif" Text="注销" runat="server"></f:Button>
                                <f:Button ID="btnLogout" OnClientClick="javascript:window.opener=null;self.close();" OnClick="btnLogout_Click"  IconUrl="~/res/icon/logout.gif" Text="退出"  runat="server"></f:Button>
                            </div>
                        </div>
                    </Content>
                </f:Region>
                <f:Region ID="leftRegion" RegionSplit="true" Width="220px" ShowHeader="true" ShowBorder="true" Title="菜单"
                    EnableCollapse="true" Layout="Fit" Collapsed="false" RegionPosition="Left" runat="server">
                    <Items>
                    <f:Tree ID="Tree1" Width="200px" ShowHeader="false" EnableCollapse="true" runat="server"></f:Tree>  
                    </Items>
                </f:Region>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" ShowBorder="true" Position="Center"
                    runat="server">
                    <Items>
                        <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                            <Tabs>
                                <f:Tab ID="Tab1" Title="首页" Layout="Fit" Icon="House" CssClass="maincontent" runat="server">
                                    <Toolbars>
                                        <f:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                                </f:ToolbarFill>
                                               <%-- <f:Button ID="btnGotoOpenSourceSite" Icon="DiskDownload" Text="下载全部源码" OnClientClick="window.open('http://fineui.codeplex.com/', '_blank');"
                                                    EnablePostBack="false" runat="server">
                                                </f:Button>--%>
                                                <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                                </f:ToolbarSeparator>
                                                <%--<f:Button ID="Button1" Icon="PageGo" Text="论坛交流" OnClientClick="window.open('http://fineui.com/bbs/', '_blank');"
                                                    EnablePostBack="false" runat="server">
                                                </f:Button>--%>
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Items>
                                        <f:ContentPanel ID="ContentPanel1" ShowBorder="false" BodyPadding="10px" ShowHeader="false" AutoScroll="true"
                                            runat="server">
                                           

                                        </f:ContentPanel>
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
                <f:Region ID="bottomPanel" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server" Layout="Fit">
                    <Items>
                        <f:ContentPanel ID="ContentPanel2" runat="server" ShowBorder="false" ShowHeader="false">
                            <table class="bottomtable">
                                <tr>
                                    <td style="width: 300px;">&nbsp;当前用户：<asp:Literal runat="server" ID="LoginUser"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;登录时间：<asp:Literal runat="server" ID="LoginTime"></asp:Literal></td>
                                    <td style="text-align: center;">Copyright &copy; 2016 浙江创源</td>
                                    <%--<td style="width: 300px; text-align: right;">在线人数：<asp:Literal runat="server" ID="litOnlineUserCount"></asp:Literal>&nbsp;</td>--%>
                                    <td style="width: 300px; text-align: right;">版本：v1.0&nbsp;&nbsp; </td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
<%--        <f:Window ID="windowSourceCode" Icon="PageWhiteCode" Title="" Hidden="true" EnableIFrame="true"
            runat="server" IsModal="true" Width="950px" Height="550px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>--%>
        <f:Menu ID="menuSettings" runat="server">
            <f:MenuButton ID="btnExpandAll" IconUrl="~/res/images/expand-all.gif" Text="展开菜单" EnablePostBack="false"
                runat="server">
            </f:MenuButton>
            <f:MenuButton ID="btnCollapseAll" IconUrl="~/res/images/collapse-all.gif" Text="折叠菜单"
                EnablePostBack="false" runat="server">
            </f:MenuButton>
            <f:MenuSeparator ID="MenuSeparator4" runat="server">
            </f:MenuSeparator>        
            <f:MenuSeparator ID="MenuSeparator1" runat="server">
            </f:MenuSeparator>
            <f:MenuButton EnablePostBack="false" Text="菜单样式" ID="MenuStyle" runat="server">
                <Menu ID="Menu3" runat="server">
                    <f:MenuCheckBox Text="树菜单" ID="MenuStyleTree" Checked="true" GroupName="MenuStyle"
                        AutoPostBack="true" OnCheckedChanged="MenuStyle_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                   <%-- <f:MenuCheckBox Text="手风琴+树菜单" ID="MenuStyleAccordion" GroupName="MenuStyle" AutoPostBack="true"
                        OnCheckedChanged="MenuStyle_CheckedChanged" runat="server">
                    </f:MenuCheckBox>--%>
                </Menu>
            </f:MenuButton>
            <%--<f:MenuButton EnablePostBack="false" Text="语言" ID="MenuLang" runat="server">
                <Menu ID="Menu2" runat="server">
                    <f:MenuCheckBox Text="简体中文" ID="MenuLangZHCN" Checked="true" GroupName="MenuLang"
                        AutoPostBack="true" OnCheckedChanged="MenuLang_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                    <f:MenuCheckBox Text="繁體中文" ID="MenuLangZHTW" GroupName="MenuLang" AutoPostBack="true"
                        OnCheckedChanged="MenuLang_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                    <f:MenuCheckBox Text="English" ID="MenuLangEN" GroupName="MenuLang" AutoPostBack="true"
                        OnCheckedChanged="MenuLang_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                </Menu>
            </f:MenuButton>--%>
            <f:MenuButton ID="MenuTheme" EnablePostBack="false" Text="主题" runat="server">
                <Menu ID="Menu4" runat="server">
                    <f:MenuCheckBox Text="Neptune" ID="MenuThemeNeptune" Checked="true" GroupName="MenuTheme"
                        AutoPostBack="true" OnCheckedChanged="MenuTheme_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                  <%--  <f:MenuCheckBox Text="Blue" ID="MenuThemeBlue" GroupName="MenuTheme"
                        AutoPostBack="true" OnCheckedChanged="MenuTheme_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                    <f:MenuCheckBox Text="Gray" ID="MenuThemeGray" GroupName="MenuTheme" AutoPostBack="true"
                        OnCheckedChanged="MenuTheme_CheckedChanged" runat="server">
                    </f:MenuCheckBox>
                    <f:MenuCheckBox Text="Access" ID="MenuThemeAccess" GroupName="MenuTheme" AutoPostBack="true"
                        OnCheckedChanged="MenuTheme_CheckedChanged" runat="server">
                    </f:MenuCheckBox>--%>
                </Menu>
            </f:MenuButton>
            <%--<f:MenuSeparator ID="MenuSeparator2" runat="server">
            </f:MenuSeparator>
            <f:MenuHyperLink ID="MenuHyperLink2" runat="server" Text="转到 v3.x 中文示例" NavigateUrl="http://fineui.com/demo_v3/" Target="_blank">
            </f:MenuHyperLink>
            <f:MenuHyperLink ID="MenuHyperLink1" runat="server" Text="转到 v3.x 英文示例" NavigateUrl="http://fineui.com/demo_en/" Target="_blank">
            </f:MenuHyperLink>
            <f:MenuSeparator ID="MenuSeparator3" runat="server">
            </f:MenuSeparator>
            <f:MenuHyperLink ID="MenuHyperLink3" runat="server" Text="转到专业版示例" NavigateUrl="http://fineui.com/demo_pro/" Target="_blank">
            </f:MenuHyperLink>--%>
        </f:Menu>
       <%-- <asp:XmlDataSource ID="XmlDataSource1" runat="server"></asp:XmlDataSource>--%>
    </form>
    <%--<img src="./res/images/logo/logo3.png" alt="FineUI 图标" id="logo" />--%>
    <script src="./res/js/jquery.min.js"></script>
    <script>


        F.ready(function () {
            // IDS：default.aspx.cs 中向页面输出的控件客户端ID集合
            var btnExpandAll = F(IDS.btnExpandAll);
            var btnCollapseAll = F(IDS.btnCollapseAll);
            var mainMenu = F(IDS.mainMenu);
            var mainTabStrip = F(IDS.mainTabStrip);
            var windowSourceCode = F(IDS.windowSourceCode);
            var leftRegion = F(IDS.leftRegion);
            var menuSettings = F(IDS.menuSettings);


            if (window.Ext) {
                F.Button = Ext.Button;
                F.Toolbar = Ext.Toolbar;
            }


            // 当前展开的手风琴面板
            function getExpandedPanel() {
                var panel = null;
                mainMenu.items.each(function (item) {
                    if (!item.getCollapsed()) {
                        panel = item;
                    }
                });
                return panel;
            }

            // 点击展开菜单
            btnExpandAll.on('click', function () {
                if (IDS.menuType == 'menu') {
                    // 左侧为树控件
                    mainMenu.expandAll();
                } else {
                    // 左侧为树控件+手风琴控件
                    var expandedPanel = getExpandedPanel();
                    if (expandedPanel) {
                        expandedPanel.items.getAt(0).expandAll();
                    }
                }
            });

            // 点击折叠菜单
            btnCollapseAll.on('click', function () {
                if (IDS.menuType == 'menu') {
                    // 左侧为树控件
                    mainMenu.collapseAll();
                } else {
                    // 左侧为树控件+手风琴控件
                    var expandedPanel = getExpandedPanel();
                    if (expandedPanel) {
                        expandedPanel.items.getAt(0).collapseAll();
                    }
                }
            });


            function createToolbar(tabConfig) {

                // 由工具栏上按钮获得当前标签页中的iframe节点
                //function getCurrentIFrameNode(btn) {
                //    return $('#' + btn.id).parents('.f-tab').find('iframe');
                //}

                //var sourcecodeButton = new F.Button({
                //    text: '源代码',
                //    type: 'button',
                //    icon: './res/icon/page_white_code.png',
                //    listeners: {
                //        click: function () {
                //            var iframeNode = getCurrentIFrameNode(this);
                //            var iframeWnd = iframeNode[0].contentWindow

                //            var files = [iframeNode.attr('src')];
                //            var sourcefilesNode = $(iframeWnd.document).find('head meta[name=sourcefiles]');
                //            if (sourcefilesNode.length) {
                //                $.merge(files, sourcefilesNode.attr('content').split(';'));
                //            }
                //            windowSourceCode.f_show('./common/source.aspx?files=' + encodeURIComponent(files.join(';')));
                //        }
                //    }
                //});

                //var openNewWindowButton = new F.Button({
                //    text: '新标签页中打开',
                //    type: 'button',
                //    icon: './res/icon/tab_go.png',
                //    listeners: {
                //        click: function () {
                //            var iframeNode = getCurrentIFrameNode(this);
                //            window.open(iframeNode.attr('src'), '_blank');
                //        }
                //    }
                //});

                //var refreshButton = new F.Button({
                //    text: '刷新',
                //    type: 'button',
                //    icon: './res/icon/reload.png',
                //    listeners: {
                //        click: function () {
                //            var iframeNode = getCurrentIFrameNode(this);
                //            iframeNode[0].contentWindow.location.reload();
                //        }
                //    }
                //});

                //var toolbar = new F.Toolbar({
                //    items: ['->', sourcecodeButton, '-', refreshButton, '-', openNewWindowButton]
                //});

                //tabConfig['tbar'] = toolbar;
            }



            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // createToolbar： 创建选项卡前的回调函数（接受tabConfig参数）
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.util.initTreeTabStrip(mainMenu, mainTabStrip, createToolbar, true, false, false);



            // 添加示例标签页
            window.addExampleTab = function (id, url, text, icon, refreshWhenExist) {
                // 动态添加一个标签页
                // mainTabStrip： 选项卡实例
                // id： 选项卡ID
                // url: 选项卡IFrame地址 
                // text： 选项卡标题
                // icon： 选项卡图标
                // addTabCallback： 创建选项卡前的回调函数（接受tabConfig参数）
                // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
                F.util.addMainTab(mainTabStrip, id, url, text, icon, null, refreshWhenExist);
            };

            // 移除选中标签页
            window.removeActiveTab = function () {
                var activeTab = mainTabStrip.getActiveTab();
                mainTabStrip.removeTab(activeTab.id);
            };


            // 添加工具图标，并在点击时显示上下文菜单
            leftRegion.addTool({
                type: 'gear',
                //tooltip: '系统设置',
                handler: function (event) {
                    menuSettings.showBy(this);
                }
            });

        });


    </script>
</body>
</html>

