using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Data;
using System.Text;

namespace WasteManagement.Content.User
{
    public partial class Role_Window : System.Web.UI.Page
    {
        private DataBasic dataBasic = new DataBasic();
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }

        private string old//原本拥有的所有菜单ID
        {
            get { return (string)ViewState["old"]; }
            set { ViewState["old"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            //InitDropdownlist();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                InitTree();
                if (Request.QueryString["ID"] != null)
                {
                    string sId = Request.QueryString["ID"].ToString().Trim();
                    sGuid = Request.QueryString["ID"].ToString().Trim();
                    LoadData(sId);
                }
                else
                {
                    //drop_area.SelectedIndex = 0;
                    //drop_inCharge.SelectedIndex = 0;
                    //drop_Role.SelectedIndex = 0;
                }
            }
        }


        #region 加载数据
        private void LoadData(string sId)
        {
            if (!string.IsNullOrEmpty(sId))
            {
                StringBuilder sb = new StringBuilder();
                Entity.RoleMenu rolemenu = DAL.RoleMenu.GetRoleMenu(sId);
                txt_RoleName.Text = rolemenu.role.RoleName;
                txt_Description.Text = rolemenu.role.Description;
                foreach (Entity.Menu menu in rolemenu.menuList)
                {
                    int menuID = menu.ID;
                    FineUI.TreeNode tn = Tree2.FindNode(menuID.ToString());
                    tn.Checked = true;
                    sb.Append(menuID.ToString() + ",");
                }
                old = sb.ToString().Substring(0, sb.Length - 1);
            }
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
        #endregion

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


        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sGuid))
            {
                //Add
                Entity.RoleMenu roleMenu = new Entity.RoleMenu();
                roleMenu.role.RoleName = txt_RoleName.Text;
                roleMenu.role.Description = txt_Description.Text;
                string[] checkmenus = Tree2.GetCheckedNodeIDs();
                foreach (string checkID in checkmenus)
                {
                    Entity.Menu menu = new Entity.Menu();
                    menu.ID = int.Parse(checkID.ToString());
                    roleMenu.menuList.Add(menu);
                }

                int success = DAL.RoleMenu.AddRoleMenu(roleMenu);
                if (success == 1)
                {
                    Alert.ShowInTop(" 保存成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInTop("保存失败！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                //Update
                Entity.RoleMenu roleMenu = new Entity.RoleMenu();
                roleMenu.role.ID = int.Parse(sGuid);
                roleMenu.role.RoleName = txt_RoleName.Text;
                roleMenu.role.Description = txt_Description.Text;
                string[] checkmenus = Tree2.GetCheckedNodeIDs();

                int[] check = new int[checkmenus.Length];
                for (int i = 0; i < checkmenus.Length; i++)
                {
                    check[i] = int.Parse(checkmenus[i]);
                }



                StringBuilder sb = new StringBuilder();
                Entity.RoleMenu rolemenu = DAL.RoleMenu.GetRoleMenu(sGuid);
                txt_RoleName.Text = rolemenu.role.RoleName;
                txt_Description.Text = rolemenu.role.Description;
                foreach (Entity.Menu menu in rolemenu.menuList)
                {
                    int menuID = menu.ID;
                    FineUI.TreeNode tn = Tree2.FindNode(menuID.ToString());
                    tn.Checked = true;
                    sb.Append(menuID.ToString() + ",");
                }
                old = sb.ToString().Substring(0, sb.Length - 1);
                string[] oldIds = old.Split(',');
                int[] oldid = new int[oldIds.Length];
                for (int s = 0; s < oldIds.Length; s++)
                {
                    oldid[s] = int.Parse(oldIds[s]);
                }

                List<int> Checked = new List<int>();
                for (int i = 0; i < checkmenus.Length; i++)
                {
                    Checked.Add(int.Parse(checkmenus[i]));
                }
                List<int> OldID = new List<int>();
                for (int i = 0; i < oldIds.Length; i++)
                {
                    OldID.Add(int.Parse(oldIds[i]));
                }
                List<int> Delete = ListHelper.ExceptList(OldID, Checked);
                List<int> Add = ListHelper.ExceptList(Checked, OldID);
                foreach (int a in Delete)
                {
                    roleMenu.Delete.Add(a.ToString());
                }
                foreach (int b in Add)
                {
                    roleMenu.NewAdd.Add(b.ToString());
                }
                //var c = oldid.Intersect(check);
                //var d = oldid.Except(check);
                //var f = check.Except(oldid);
                //foreach (var q in d)
                //{
                //    roleMenu.Delete.Add(q.ToString());
                //}
                //foreach (var w in f)
                //{
                //    roleMenu.NewAdd.Add(w.ToString());
                //}
                int success = DAL.RoleMenu.UpdateRoleMenu(roleMenu);
                if (success == 1)
                {
                    Alert.ShowInTop(" 修改成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInTop("修改失败！", MessageBoxIcon.Warning);
                }

            }
        }
    }
}