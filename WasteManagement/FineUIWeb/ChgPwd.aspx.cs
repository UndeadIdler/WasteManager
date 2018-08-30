using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using FineUI;

namespace WasteManagement
{
    public partial class ChgPwd : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) 
                Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                txt_vUser.Text = Request.Cookies["Cookies"].Values["UserName"].ToString().Trim();
            }
        }

        #region 保存数据
        protected void btn_save_Click(object sender, EventArgs e)
        {
            Md5 md5 = new Md5();
            string userguid = dataBasic.GetUserGuid(txt_vUser.Text.Trim(), md5.Md5Encrypt(txt_pwdold.Text.Trim()));
            if (userguid != string.Empty)
            {
                if (txt_pwd1.Text.Trim() == txt_pwd2.Text.Trim())
                {
                    if (dataBasic.ChangeUserPassword(md5.Md5Encrypt(txt_pwd1.Text.Trim()), userguid))
                    {
                        Alert.ShowInTop("密码修改成功！", MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Alert.ShowInTop(" 两次输入的密码不一致！", MessageBoxIcon.Warning);
                }
            }
            else
            {
                Alert.ShowInTop(" 原密码输入错误！", MessageBoxIcon.Warning);
            }
        }
        #endregion

    }
}
