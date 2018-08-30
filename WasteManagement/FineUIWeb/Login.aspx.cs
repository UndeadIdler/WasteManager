using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FineUI;
using DAL;

namespace WasteManagement
{
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadData();
            }
        }


        //private void LoadData()
        //{
        //    InitCaptchaCode();
        //}

        /// <summary>
        /// 初始化验证码
        /// </summary>
        //private void InitCaptchaCode()
        //{
        //    // 创建一个 6 位的随机数并保存在 Session 对象中
        //    Session["CaptchaImageText"] = GenerateRandomCode();
        //    imgCaptcha.ImageUrl = "~/captcha/captcha.ashx?w=150&h=30&t=" + DateTime.Now.Ticks;
        //}

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        //private string GenerateRandomCode()
        //{
        //    string s = String.Empty;
        //    Random random = new Random();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        s += random.Next(10).ToString();
        //    }
        //    return s;
        //}

        //protected void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    InitCaptchaCode();
        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Md5 md5 = new Md5();
            string sUserName = tbxUserName.Text.Trim();
            string sPassWord = tbxPassword.Text.Trim();
            string userguid = DAL.User.Login(sUserName, md5.Md5Encrypt(sPassWord));
            if (userguid != string.Empty)
            {
                HttpCookie Cookieobj = new HttpCookie("Cookies");
                DateTime dt = DateTime.Now;
                TimeSpan ts = new TimeSpan(0, 8, 0, 0); //有效期8小时；
                Cookieobj.Expires = dt.Add(ts);
                Entity.User user = DAL.User.GetUser(userguid);
                Cookieobj.Values.Add("isLogin", "yes");
                Cookieobj.Values.Add("UserName", tbxUserName.Text.Trim());
                Cookieobj.Values.Add("UserGuid", userguid);
                //Cookieobj.Values.Add("AreaInCharge", user.AreaInCharge);
                Response.AppendCookie(Cookieobj);

                Response.Redirect("default.aspx", false);
            }
            else
            {
                Alert.ShowInTop("用户名或密码错误或账户已被停用！", MessageBoxIcon.Error);
            }
        }

    }
}

