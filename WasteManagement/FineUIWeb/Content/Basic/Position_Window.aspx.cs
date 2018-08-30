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
    public partial class Position_Window : PageBase
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
                Entity.Position entity = DAL.Position.GetPosition(int.Parse(sId));
                txt_name.Text = entity.Name;
                //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();

                CheckStop.SelectedValue = entity.IsShow.ToString();
                Orderid.Text = entity.OrderID.ToString();
            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (txt_name.Text.Trim() == "") msg += "请输入监测位置名称！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Position where Name='" + txt_name.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该监测位置名称已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Position where Name='" + txt_name.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "监测位置名称不能重复！";
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
                Entity.Position entity = new Entity.Position();
                entity.Name = txt_name.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.OrderID = int.Parse(Orderid.Text.ToString());
                entity.IsShow = int.Parse(CheckStop.SelectedValue.ToString());
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.Position.AddPosition(entity);
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

                    int success = DAL.Position.UpdatePosition(entity);
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