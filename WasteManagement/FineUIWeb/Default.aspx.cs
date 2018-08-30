using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;
using System.Text;

namespace WasteManagement
{
    public partial class _Default : PageBase
    {
        #region Page_Init
        private DataBasic dataBasic = new DataBasic();
        private string menuType = "menu";
        private bool showOnlyNew = false;
        private int examplesCount = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.Cookies["Cookies"] == null) Response.Redirect("Login.aspx");
            string ss = mainRegion.Height.ToString();
            HttpCookie menuCookie = Request.Cookies["MenuStyle_v4"];
            if (menuCookie != null)
            {
                menuType = menuCookie.Value;
            }

            // 从Cookie中读取是否仅显示最新示例
            HttpCookie menuShowOnlyNew = Request.Cookies["ShowOnlyNew_v4"];
            if (menuShowOnlyNew != null)
            {
                showOnlyNew = Convert.ToBoolean(menuShowOnlyNew.Value);
            }


            // 注册客户端脚本，服务器端控件ID和客户端ID的映射关系
            //JObject ids = GetClientIDS(btnExpandAll, btnCollapseAll, windowSourceCode, mainTabStrip, leftRegion, menuSettings);
            JObject ids = GetClientIDS(btnExpandAll, btnCollapseAll, null, mainTabStrip, leftRegion, menuSettings);
            if (menuType == "accordion")
            {
                //Accordion accordionMenu = InitAccordionMenu();
                //ids.Add("mainMenu", accordionMenu.ClientID);
                //ids.Add("menuType", "accordion");
            }
            else
            {
                //Tree treeMenu = InitTreeMenu();
                ids.Add("mainMenu", Tree1.ClientID);
                ids.Add("menuType", "menu");
            }

            ids.Add("theme", PageManager.Instance.Theme.ToString());

            // 只在页面第一次加载时注册客户端用到的脚本
            if (!Page.IsPostBack)
            {
                string idsScriptStr = String.Format("window.IDS={0};", ids.ToString(Newtonsoft.Json.Formatting.None));
                PageContext.RegisterStartupScript(idsScriptStr);
            }

        }

        private void LoadData()
        {
            // 模拟从数据库返回数据表
            //DataTable table = CreateDataTable();

            DataSet ds = dataBasic.GetUserTree(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
            examplesCount = ds.Tables[0].Rows.Count;
            leftRegion.Title = String.Format("菜单（{0}）", examplesCount);
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["ID"], ds.Tables[0].Columns["FatherID"]);




            //List<Entity.Menu> menus = DAL.User.GetMenu(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
            //examplesCount = menus.Capacity;
            //leftRegion.Title = String.Format("菜单（{0}）", examplesCount);
            //DataTable dt = new DataTable();
            //ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["ID"], ds.Tables[0].Columns["FatherID"]);




            //DataTable dt = DAL.User.GetMenuEx(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
            //examplesCount = dt.Rows.Count;
            //leftRegion.Title = String.Format("菜单（{0}）", examplesCount);
            //dt.ChildRelations.Add("TreeRelation", dt.Columns["ID"], dt.Columns["FatherID"]);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("FatherID"))
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.Text = row["MenuName"].ToString();
                    if (!string.IsNullOrEmpty(row["ImageUrl"].ToString()))
                    {
                        node.IconUrl = row["ImageUrl"].ToString();
                    }
                    node.Expanded = true;
                    Tree1.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }

            //DataTable dt = dataBasic.GetDataTable(new string[] { "Ac_Users", " and vUserGuid='" + Request.Cookies["Cookies"].Values["UserGuid"].ToString() + "'", "", "" });
            //if (dt.Rows.Count == 1) LoginUser.Text = dt.Rows[0]["vUserRole"].ToString().Trim();
            #region 原来的
            //List<Entity.Role> roles = DAL.User.GetUserRole(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
            //if (roles.Count > 0)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i < roles.Count; i++)
            //    {
            //        sb.Append(roles[i].Description);
            //        if (i < roles.Count - 1)
            //        {
            //            sb.Append("，");
            //        }
            //    }
            //    LoginUser.Text = sb.ToString().Trim();
            //}
            #endregion
            Entity.User user = DAL.User.GetUser(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
            LoginUser.Text = user.RealName.Trim();            
            //if (dt.Rows.Count == 1) 
            LoginTime.Text = DateTime.Now.ToString();
        }

        protected void btnRelogin_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["Cookies"] != null)
            {
                HttpCookie mycookies = new HttpCookie("Cookies");
                mycookies["isLogin"] = null;
                mycookies.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookies);
            }
            Response.Redirect("Login.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["Cookies"] != null)
            {
                HttpCookie mycookies = new HttpCookie("Cookies");
                mycookies["isLogin"] = null;
                mycookies.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(mycookies);
            }
            //Response.Write("<script>window.opener=null;self.close();</script>");
        }

        private void ResolveSubTree(DataRow dataRow, FineUI.TreeNode treeNode)
        {
            DataRow[] rows = dataRow.GetChildRows("TreeRelation");
            if (rows.Length > 0)
            {
                treeNode.Expanded = true;
                foreach (DataRow row in rows)
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.Text = row["MenuName"].ToString();
                    if (!string.IsNullOrEmpty(row["ImageUrl"].ToString()))
                    { 
                        node.IconUrl = row["ImageUrl"].ToString();                    
                    }
                    node.NavigateUrl = row["MenuUrl"].ToString();
                    node.Target = "_Blank";
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }

        //private Accordion InitAccordionMenu()
        //{
        //    Accordion accordionMenu = new Accordion();
        //    accordionMenu.ID = "accordionMenu";
        //    accordionMenu.ShowBorder = false;
        //    accordionMenu.ShowHeader = false;
        //    leftRegion.Items.Add(accordionMenu);


        //    XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        //    XmlNodeList xmlNodes = xmlDoc.SelectNodes("/Tree/TreeNode");
        //    foreach (XmlNode xmlNode in xmlNodes)
        //    {
        //        if (xmlNode.HasChildNodes)
        //        {
        //            AccordionPane accordionPane = new AccordionPane();
        //            accordionPane.Title = xmlNode.Attributes["Text"].Value;
        //            accordionPane.Layout = Layout.Fit;
        //            accordionPane.ShowBorder = false;
        //            accordionPane.BodyPadding = "2px 0 0 0";
        //            accordionMenu.Items.Add(accordionPane);

        //            Tree innerTree = new Tree();
        //            innerTree.ShowBorder = false;
        //            innerTree.ShowHeader = false;
        //            innerTree.EnableIcons = true;
        //            innerTree.AutoScroll = true;
        //            innerTree.EnableSingleClickExpand = true;
        //            accordionPane.Items.Add(innerTree);

        //            XmlDocument innerXmlDoc = new XmlDocument();
        //            innerXmlDoc.LoadXml(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Tree>{0}</Tree>", xmlNode.InnerXml));

        //            // 绑定AccordionPane内部的树控件
        //            innerTree.NodeDataBound += treeMenu_NodeDataBound;
        //            innerTree.PreNodeDataBound += treeMenu_PreNodeDataBound;
        //            innerTree.DataSource = innerXmlDoc;
        //            innerTree.DataBind();

        //            //// 重新设置每个节点的图标
        //            //ResolveTreeNode(innerTree.Nodes);
        //        }
        //    }

        //    return accordionMenu;
        //}

        //private Tree InitTreeMenu()
        //{
        //    Tree treeMenu = new Tree();
        //    treeMenu.ID = "treeMenu";
        //    treeMenu.ShowBorder = false;
        //    treeMenu.ShowHeader = false;
        //    treeMenu.EnableIcons = true;
        //    treeMenu.AutoScroll = true;
        //    treeMenu.EnableSingleClickExpand = true;
        //    treeMenu.IFrameUrl = "vMenuUrl";
        //    treeMenu.IFrameName = "vMenuName";

        //    leftRegion.Items.Add(treeMenu);

        //    // 绑定 XML 数据源到树控件
        //    DataSet ds = dataBasic.GetUserTree(Request.Cookies["Cookies"].Values["UserGuid"].ToString());
        //    ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["iFatherId"]);


        //    treeMenu.NodeDataBound += treeMenu_NodeDataBound;
        //    treeMenu.PreNodeDataBound += treeMenu_PreNodeDataBound;
        //    treeMenu.DataSource = ds;
        //    treeMenu.DataBind();

        //    //// 重新设置每个节点的图标
        //    //ResolveTreeNode(treeMenu.Nodes);

        //    return treeMenu;
        //}


        /// <summary>
        /// 树节点的绑定后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_NodeDataBound(object sender, FineUI.TreeNodeEventArgs e)
        {
            string isNewHtml = GetIsNewHtml(e.XmlNode);
            if (!String.IsNullOrEmpty(isNewHtml))
            {
                e.Node.Text += isNewHtml;
            }

            // 如果仅显示最新示例 并且 当前节点不是子节点，则展开当前节点
            if (showOnlyNew && !e.Node.Leaf)
            {
                e.Node.Expanded = true;
            }

        }


        /// <summary>
        /// 树节点的预绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMenu_PreNodeDataBound(object sender, TreePreNodeEventArgs e)
        {
            // 如果仅显示最新示例
            if (showOnlyNew)
            {
                string isNewHtml = GetIsNewHtml(e.XmlNode);
                if (String.IsNullOrEmpty(isNewHtml))
                {
                    e.Cancelled = true;
                }
            }

            // 更新示例总数
            if (e.XmlNode.ChildNodes.Count == 0)
            {
                examplesCount++;
            }
        }


        private string GetIsNewHtml(XmlNode node)
        {
            string result = String.Empty;

            XmlAttribute isNewAttr = node.Attributes["IsNew"];
            if (isNewAttr != null)
            {
                if (Convert.ToBoolean(isNewAttr.Value))
                {
                    result = "&nbsp;<span class=\"isnew\">New!</span>";
                }
            }

            return result;
        }



        private JObject GetClientIDS(params ControlBase[] ctrls)
        {
            JObject jo = new JObject();
            foreach (ControlBase ctrl in ctrls)
            {
                if (ctrl != null)
                    jo.Add(ctrl.ID, ctrl.ClientID);
            }

            return jo;
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("Login.aspx");


                LoadData();

                InitMenuStyleButton();
                InitLangMenuButton();
                InitThemeMenuButton();
                InitMenuShowOnlyNew();

                //litVersion.Text = FineUI.GlobalConfig.ProductVersion;
                //litOnlineUserCount.Text = Application["OnlineUserCount"].ToString();

            }
        }


        //private void LoadData()
        //{
        //    // 模拟从数据库返回数据表
        //    DataTable table = dataBasic.GetUserTree(Request.Cookies["Cookies"].Values["UserGuid"].ToString()).Tables[0];

        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(table);
        //    ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["id"], ds.Tables[0].Columns["iFatherId"]);

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        if (row.IsNull("ParentId"))
        //        {
        //            FineUI.TreeNode node = new FineUI.TreeNode();
        //            node.Text = row["vMenuName"].ToString();
        //            node.Expanded = true;
        //            Tree1.Nodes.Add(node);

        //            ResolveSubTree(row, node);
        //        }
        //    }
        //}

        //private void ResolveSubTree(DataRow dataRow, FineUI.TreeNode treeNode)
        //{
        //    DataRow[] rows = dataRow.GetChildRows("TreeRelation");
        //    if (rows.Length > 0)
        //    {
        //        treeNode.Expanded = true;
        //        foreach (DataRow row in rows)
        //        {
        //            FineUI.TreeNode node = new FineUI.TreeNode();
        //            node.Text = row["vMenuName"].ToString();
        //            treeNode.Nodes.Add(node);

        //            ResolveSubTree(row, node);
        //        }
        //    }
        //}






        private void InitMenuShowOnlyNew()
        {
            //cbxShowOnlyNew.Checked = showOnlyNew;

            //if (showOnlyNew)
            //{
            //    leftRegion.Title = "最新示例";
            //}
        }


        private void InitMenuStyleButton()
        {
            string menuStyleID = "MenuStyleTree";

            HttpCookie menuStyleCookie = Request.Cookies["MenuStyle_v4"];
            if (menuStyleCookie != null)
            {
                switch (menuStyleCookie.Value)
                {
                    case "menu":
                        menuStyleID = "MenuStyleTree";
                        break;
                    case "accordion":
                        menuStyleID = "MenuStyleAccordion";
                        break;
                }
            }


            SetSelectedMenuID(MenuStyle, menuStyleID);
        }


        private void InitLangMenuButton()
        {
            //string langMenuID = "MenuLangZHCN";

            //string langValue = PageManager1.Language.ToString().ToLower();
            //switch (langValue)
            //{
            //    case "zh_cn":
            //        langMenuID = "MenuLangZHCN";
            //        break;
            //    case "zh_tw":
            //        langMenuID = "MenuLangZHTW";
            //        break;
            //    case "en":
            //        langMenuID = "MenuLangEN";
            //        break;
            //}


            //SetSelectedMenuID(MenuLang, langMenuID);
        }

        private void InitThemeMenuButton()
        {
            string themeMenuID = "MenuThemeBlue";

            string themeValue = PageManager1.Theme.ToString().ToLower();
            switch (themeValue)
            {
                case "blue":
                    themeMenuID = "MenuThemeBlue";
                    break;
                case "gray":
                    themeMenuID = "MenuThemeGray";
                    break;
                case "access":
                    themeMenuID = "MenuThemeAccess";
                    break;
                case "neptune":
                    themeMenuID = "MenuThemeNeptune";
                    break;
            }

            SetSelectedMenuID(MenuTheme, themeMenuID);
        }

        #endregion

        #region Event

        protected void MenuLang_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框菜单按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            //string langValue = FineUI.Language.ZH_CN.ToString();
            //string langID = GetSelectedMenuID(MenuLang);

            //switch (langID)
            //{
            //    case "MenuLangZHCN":
            //        langValue = FineUI.Language.ZH_CN.ToString();
            //        break;
            //    case "MenuLangZHTW":
            //        langValue = FineUI.Language.ZH_TW.ToString();
            //        break;
            //    case "MenuLangEN":
            //        langValue = FineUI.Language.EN.ToString();
            //        break;
            //}

            //SaveToCookieAndRefresh("Language", langValue);
        }

        protected void MenuTheme_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框菜单按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string themeValue = FineUI.Theme.Neptune.ToString();
            string themeID = GetSelectedMenuID(MenuTheme);

            switch (themeID)
            {
                case "MenuThemeNeptune":
                    themeValue = FineUI.Theme.Neptune.ToString();
                    break;
                case "MenuThemeBlue":
                    themeValue = FineUI.Theme.Blue.ToString();
                    break;
                case "MenuThemeGray":
                    themeValue = FineUI.Theme.Gray.ToString();
                    break;
                case "MenuThemeAccess":
                    themeValue = FineUI.Theme.Access.ToString();
                    break;
            }

            SaveToCookieAndRefresh("Theme", themeValue);
        }

        protected void MenuStyle_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框菜单按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string menuValue = "menu";
            string menuStyleID = GetSelectedMenuID(MenuStyle);

            switch (menuStyleID)
            {
                case "MenuStyleTree":
                    menuValue = "tree";
                    break;
                case "MenuStyleAccordion":
                    menuValue = "accordion";
                    break;

            }
            SaveToCookieAndRefresh("MenuStyle", menuValue);
        }

        protected void cbxShowOnlyNew_CheckedChanged(object sender, CheckedEventArgs e)
        {
            SaveToCookieAndRefresh("ShowOnlyNew", e.Checked.ToString());
        }

        private string GetSelectedMenuID(MenuButton menuButton)
        {
            foreach (FineUI.MenuItem item in menuButton.Menu.Items)
            {
                if (item is MenuCheckBox && (item as MenuCheckBox).Checked)
                {
                    return item.ID;
                }
            }
            return null;
        }

        private void SetSelectedMenuID(MenuButton menuButton, string selectedMenuID)
        {
            foreach (FineUI.MenuItem item in menuButton.Menu.Items)
            {
                MenuCheckBox menu = (item as MenuCheckBox);
                if (menu != null && menu.ID == selectedMenuID)
                {
                    menu.Checked = true;
                }
                else
                {
                    menu.Checked = false;
                }
            }
        }


        private void SaveToCookieAndRefresh(string cookieName, string cookieValue)
        {
            HttpCookie cookie = new HttpCookie(cookieName + "_v4", cookieValue);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            PageContext.Refresh();
        }
        #endregion
    }
}