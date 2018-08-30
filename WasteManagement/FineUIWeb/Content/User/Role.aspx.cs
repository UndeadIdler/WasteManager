using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using FineUI;

namespace WasteManagement.Content.User
{
    public partial class Role : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        private string strUserGuid
        {
            get { return (string)ViewState["strUserGuid"]; }
            set { ViewState["strUserGuid"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../Login.aspx");
                BindUserGrid();
                InitTree();
                CheckUserRole();
            }

        }

        private void CheckUserRole()
        {
            string pageName = this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString();
            //if (!blPageWrite(pageName, Request.Cookies["Cookies"].Values["UserGuid"].ToString()))
            //{
            //    btnAddUser.Enabled = false;
            //    btnDelUser.Enabled = false;
            //    btnSaveTree1.Enabled = false;
            //    btnSaveTree2.Enabled = false;
            //}
        }




        #region Bind

        private void BindUserGrid()
        {
            DataTable dt = DAL.Role.GetAllRoles();
            GridUser.DataSource = dt;
            GridUser.DataBind();
        }


        private void InitTree()
        {
            DataSet ds = dataBasic.GetMenuTree();
            ds.Relations.Add("TreeRelation", ds.Tables[0].Columns["ID"], ds.Tables[0].Columns["FatherID"]);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row.IsNull("FatherID"))
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.EnableCheckBox = true;
                    node.EnableCheckEvent = true;
                    node.Text = row["MenuName"].ToString();
                    node.NodeID = row["ID"].ToString();
                    node.Expanded = true;
                    //Tree1.Nodes.Add(node);
                    Tree2.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
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
                    node.EnableCheckBox = true;
                    node.EnableCheckEvent = true;
                    node.Text = row["MenuName"].ToString();
                    node.NavigateUrl = row["MenuUrl"].ToString();
                    node.NodeID = row["ID"].ToString();
                    node.Target = "_Blank";
                    treeNode.Nodes.Add(node);

                    ResolveSubTree(row, node);
                }
            }
        }



        protected void Tree2_NodeCheck(object sender, FineUI.TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                Tree2.CheckAllNodes(e.Node.Nodes);
            }
            else
            {
                Tree2.UncheckAllNodes(e.Node.Nodes);
            }
        }


        private void CheckTree2()
        {
            if (GridUser.SelectedRowIndex < 0)
            {
                return;
            }
            Tree2.UncheckAllNodes(Tree2.Nodes);

            strUserGuid = GridUser.DataKeys[GridUser.SelectedRowIndex][0].ToString();
            //List<Entity.Menu> menus = DAL.User.GetMenu(strUserGuid);
            List<Entity.Menu> menus = DAL.RoleMenu.GetRoleMenu(strUserGuid).menuList;
            foreach (Entity.Menu menu in menus)
            {
                int menuID = menu.ID;
                FineUI.TreeNode tn = Tree2.FindNode(menuID.ToString());
                tn.Checked = true;
            }
        }

        private void TraversalTree(FineUI.TreeNodeCollection FatherNodes, string ThisID, string check)
        {
            foreach (FineUI.TreeNode nodes in FatherNodes)
            {

                if (nodes.NodeID == ThisID)
                {
                    if (check == "True") nodes.Checked = true;
                }
                if (nodes.Nodes.Count > 0)
                {
                    TraversalTree(nodes.Nodes, ThisID, check);
                }
            }
        }

        #endregion

        #region Events

        protected void GridUser_RowSelect(object sender, FineUI.GridRowSelectEventArgs e)
        {
            CheckTree2();
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelRole_Click(object sender, EventArgs e)
        {
            int selectedCount = GridUser.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }


            int rowIndex = GridUser.SelectedRowIndexArray[0];
            object[] dataKeys = GridUser.DataKeys[rowIndex];
            int iReturn = DAL.Role.DeleteRole(int.Parse(HttpUtility.UrlEncode(dataKeys[0].ToString())));

            if (iReturn == 1)
            {
                Alert.ShowInTop(" 删除成功！", MessageBoxIcon.Information);
                BindUserGrid();
            }
            else
            {
                Alert.ShowInTop(" 删除失败！", MessageBoxIcon.Warning);
            }
        }




        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            string openUrl = String.Format("Role_Window.aspx?id={0}", "");
            PageContext.RegisterStartupScript(Window1.GetShowReference(openUrl, "新增角色"));
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdRole_Click(object sender, EventArgs e)
        {
            string sIDStr = string.Empty;
            int selectedCount = GridUser.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = GridUser.SelectedRowIndexArray[0];
            object[] dataKeys = GridUser.DataKeys[rowIndex];
            sIDStr = HttpUtility.UrlEncode(dataKeys[0].ToString());

            string openUrl = String.Format("Role_Window.aspx?ID={0}", sIDStr);
            PageContext.RegisterStartupScript(Window1.GetShowReference(openUrl, "修改角色"));
        }



        #endregion

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindUserGrid();
        }

    }
}