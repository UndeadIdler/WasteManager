using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using FineUI;
using System.Collections;

namespace WasteManagement.Content.Basic
{
    public partial class Driver_Window : PageBase
    {
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    string sId = Request.QueryString["ID"].ToString().Trim();
                    sGuid = Request.QueryString["ID"].ToString().Trim();
                    LoadData(sId);
                }

            }
        }

        #region 加载数据
        private void LoadData(string sId)
        {
            if (sId != string.Empty)
            {
                Entity.Driver entity = DAL.Driver.GetDriverByID(int.Parse(sId));
                txt_name.Text = entity.RealName;
                //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
                //txt_bm.Text = entity.CarNumber;

                CheckStop.SelectedValue = entity.IsStop.ToString();
            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (txt_name.Text.Trim() == "") msg += "请输入姓名！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Driver where RealName='" + txt_name.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该姓名已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Driver where RealName='" + txt_name.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "姓名不能重复！";
                    }

            }
            
            return msg;
        }



        protected void btn_save_Click(object sender, EventArgs e)
        {
            string msg = checkInput();
            if (msg != "")
            {
                Alert.Show(msg);
                return;
            }
            else
            {
                Entity.Driver entity = new Entity.Driver();
                entity.RealName= txt_name.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                //entity.CarNumber = txt_bm.Text.Trim();
                entity.CarNumber = "";
                entity.IsStop = bool.Parse(CheckStop.SelectedValue.ToString());
                entity.CreateDate = DateTime.Now;
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.UpdateDate = DateTime.Now;
                entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.Driver.AddDriver(entity);
                    if (success == 1)
                    {
                        Alert.ShowInTop(" 保存成功！", MessageBoxIcon.Information);
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    }
                    else
                    {
                        Alert.ShowInTop(" 保存失败！", MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    //Update
                    entity.ID = int.Parse(sGuid);

                    int success = DAL.Driver.UpdateDriver(entity);
                    if (success == 1)
                    {
                        Alert.ShowInTop(" 修改成功！", MessageBoxIcon.Information);
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    }
                    else
                    {
                        Alert.ShowInTop(" 修改失败！", MessageBoxIcon.Warning);
                    }
                }
            }


        }
        #endregion
    }
}