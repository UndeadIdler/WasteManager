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
    public partial class User_Window : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }

        private List<int> OldIDs//所选择操作列记录对应的id
        {
            get { return (List<int>)ViewState["OldIDs"]; }
            set { ViewState["OldIDs"] = value; }
        }

        private List<int> NewIDs//所选择操作列记录对应的id
        {
            get { return (List<int>)ViewState["NewIDs"]; }
            set { ViewState["NewIDs"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            OldIDs = new List<int>();
            NewIDs = new List<int>();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                if (Request.QueryString["GUID"] != null)
                {
                    string sId = Request.QueryString["GUID"].ToString().Trim();
                    sGuid = Request.QueryString["GUID"].ToString().Trim();
                    InitRadio();
                    LoadData(sId);
                }
            }
        }


        private void InitRadio()
        {
            List<Entity.Role> roles = DAL.Role.GetAllRole();
            foreach (Entity.Role role in roles)
            {
                CheckItem ci = new CheckItem();
                ci.Text = role.Description;
                ci.Value = role.ID.ToString();
                Role.Items.Add(ci);
            }
        }

        #region 加载数据
        private void LoadData(string sId)
        {
            if (sId != string.Empty)
            {
                txt_vPassWord1.Hidden = true;
                txt_vPassWord2.Hidden = true;
                Entity.UserRole userrole = DAL.UserRole.GetUserRoleByGUID(sId);
                if (!string.IsNullOrEmpty(userrole.user.UserName))
                {
                    hfGuid.Text = sId;
                    txt_vUserName.Text = userrole.user.UserName;
                    txt_RealName.Text = userrole.user.RealName;
                    List<Entity.Role> roles = userrole.role;
                    string[] Roles = new string[roles.Count];
                    for (int i = 0; i < roles.Count; i++)
                    {
                        Roles[i] = roles[i].ID.ToString();
                        OldIDs.Add(roles[i].ID);
                    }
                    Role.SelectedValueArray = Roles;
                    CheckStop.SelectedValue = userrole.user.IsStop.ToString();

                }             
            }
            else
            {
                CheckStop.SelectedValue = "True";
            }
        }

        
        #endregion

        #region 保存数据
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sGuid))
            {
                //Add
                Guid guid = Guid.NewGuid();
                Md5 md5 = new Md5();
                if (txt_vPassWord1.Text.Trim() != txt_vPassWord2.Text.Trim())
                {
                    Alert.ShowInTop("2次输入的密码不一致。");
                    return;
                }
                Entity.UserRole userrole = new Entity.UserRole();
                userrole.user.GUID = guid.ToString();
                userrole.user.PassWord = md5.Md5Encrypt(txt_vPassWord1.Text.Trim());

                userrole.user.CreateDate = DateTime.Now;
                userrole.user.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                userrole.user.IsStop = bool.Parse(CheckStop.SelectedValue);
                userrole.user.PwdChgDate = DateTime.Now;
                userrole.user.RealName = txt_RealName.Text;
                userrole.user.UserName = txt_vUserName.Text;
                userrole.user.UpdateDate = DateTime.Now;
                userrole.user.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                List<Entity.Role> roles = new List<Entity.Role>();
                foreach (string roleID in Role.SelectedValueArray)
                {
                    Entity.Role role = new Entity.Role();
                    role.ID = int.Parse(roleID);
                    roles.Add(role);
                }
                userrole.role = roles;
                //userrole.role.ID = int.Parse(drop_Role.SelectedValue.ToString());
                int success = DAL.UserRole.AddUserRole(userrole);
                if (success == 1)
                {
                    Alert.ShowInTop(" 保存成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference("self.opener.location.reload();"));
                    //Response.Redirect("~/Basic/UserList.aspx");
                }
                else
                {
                    Alert.ShowInTop("保存失败！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                //Update
                //Guid guid = Guid.NewGuid();
                //Md5 md5 = new Md5();               
                Entity.UserRole userrole = new Entity.UserRole();
                userrole.user.GUID = sGuid;


                userrole.user.IsStop = bool.Parse(CheckStop.SelectedValue);
                //userrole.user.PwdChgDate = DateTime.Now;
                userrole.user.RealName = txt_RealName.Text;
                userrole.user.UserName = txt_vUserName.Text;
                userrole.user.UpdateDate = DateTime.Now;
                userrole.user.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                Entity.UserRole userrole2 = DAL.UserRole.GetUserRoleByGUID(sGuid);
                if (!string.IsNullOrEmpty(userrole2.user.UserName))
                {
                    List<Entity.Role> roles = userrole2.role;
                    string[] Roles = new string[roles.Count];
                    for (int i = 0; i < roles.Count; i++)
                    {
                        Roles[i] = roles[i].ID.ToString();
                        OldIDs.Add(roles[i].ID);
                    }

                }        

                List<Entity.Role> roles2 = new List<Entity.Role>();
                foreach (string roleID in Role.SelectedValueArray)
                {
                    Entity.Role role = new Entity.Role();
                    role.ID = int.Parse(roleID);
                    NewIDs.Add(role.ID);
                }
                userrole.role = roles2;
                //var add = NewIDs.Except(OldIDs);
                //var delete = OldIDs.Except(NewIDs);

                //var add = NewIDs;
                //var delete = OldIDs;

                List<int> Add = ListHelper.ExceptList(NewIDs, OldIDs);
                List<int> Delete = ListHelper.ExceptList(OldIDs, NewIDs);

                //List<int> Add = new List<int>();
                //List<int> Delete = new List<int>();
                //foreach(string a in add)
                //{
                //    Add.Add(int.Parse(a));
                //}
                //foreach (string b in delete)
                //{
                //    Delete.Add(int.Parse(b));
                //}
                userrole.Add = Add;
                userrole.Delete = Delete;
                int success = DAL.UserRole.UpdateUserRole(userrole);
                if (success == 1)
                {
                    Alert.ShowInTop(" 修改成功！", MessageBoxIcon.Information);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference("self.opener.location.reload();"));
                    //Response.Redirect("~/Basic/UserList.aspx");
                }
                else
                {
                    Alert.ShowInTop("修改失败！", MessageBoxIcon.Warning);
                }

            }


        }
        #endregion
    }
}