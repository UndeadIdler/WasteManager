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
    public partial class MonitorItem_Window : PageBase
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
                Entity.MonitorItem entity = DAL.MonitorItem.GetMonitorItemByID(int.Parse(sId));
                if (entity != null)
                {
                    WasteCode.Text = entity.ItemCode;
                    //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
                    WasteName.Text = entity.ItemName;
                    CheckStop.SelectedValue = entity.IsShow.ToString();
                    Orderid.Text = entity.OrderID.ToString();
                    Unit.Text = entity.Unit;
                }


            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (WasteCode.Text.Trim() == "") msg += "请输入监测项目代码！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from MonitorItem where ItemCode='" + WasteCode.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该监测项目代码已存在！";
                    }
                string checkstr2 = "select * from MonitorItem where ItemName='" + WasteName.Text.Trim() + "'";
                DataSet dscheck2 = new MyDataOp().CreateDataSet(checkstr2);
                if (dscheck2 != null)
                    if (dscheck2.Tables[0].Rows.Count > 0)
                    {
                        msg += "监测项目名称不能重复！";
                    }
            }
            else
            {
                string checkstr2 = "select * from MonitorItem where ItemCode='" + WasteCode.Text.Trim() + "' and ItemID!='" + sGuid + "'";
                DataSet dscheck2 = new MyDataOp().CreateDataSet(checkstr2);
                if (dscheck2 != null)
                    if (dscheck2.Tables[0].Rows.Count > 0)
                    {
                        msg += "该监测项目代码已存在！";
                    }


                string checkstr = "select * from MonitorItem where ItemName='" + WasteName.Text.Trim() + "' and ItemID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "监测项目名称不能重复！";
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
                Entity.MonitorItem entity = new Entity.MonitorItem();
                entity.ItemCode = WasteCode.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.ItemName = WasteName.Text.Trim();// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.OrderID = int.Parse(Orderid.Text.ToString());
                entity.IsShow = int.Parse(CheckStop.SelectedValue.ToString());
                entity.Unit = Unit.Text.Trim();
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.MonitorItem.AddMonitorItem(entity);
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
                    entity.ItemID = int.Parse(sGuid);

                    int success = DAL.MonitorItem.UpdateMonitorItem(entity);
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