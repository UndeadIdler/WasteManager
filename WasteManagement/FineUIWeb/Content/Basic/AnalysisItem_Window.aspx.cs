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
    public partial class AnalysisItem_Window : PageBase
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
                Entity.AnalysisItem entity = DAL.AnalysisItem.GetAnalysisItemByID(int.Parse(sId));
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

            if (WasteCode.Text.Trim() == "") msg += "请输入分析项目代码！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from AnalysisItem where ItemCode='" + WasteCode.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该分析项目代码已存在！";
                    }
                string checkstr2 = "select * from AnalysisItem where ItemName='" + WasteName.Text.Trim() + "'";
                DataSet dscheck2 = new MyDataOp().CreateDataSet(checkstr2);
                if (dscheck2 != null)
                    if (dscheck2.Tables[0].Rows.Count > 0)
                    {
                        msg += "分析项目名称不能重复！";
                    }
            }
            else
            {
                string checkstr2 = "select * from AnalysisItem where ItemCode='" + WasteCode.Text.Trim() + "' and ItemID!='" + sGuid + "'";
                DataSet dscheck2 = new MyDataOp().CreateDataSet(checkstr2);
                if (dscheck2 != null)
                    if (dscheck2.Tables[0].Rows.Count > 0)
                    {
                        msg += "该分析项目代码已存在！";
                    }


                string checkstr = "select * from AnalysisItem where ItemName='" + WasteName.Text.Trim() + "' and ItemID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "分析项目名称不能重复！";
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
                Entity.AnalysisItem entity = new Entity.AnalysisItem();
                entity.ItemCode = WasteCode.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.ItemName = WasteName.Text.Trim();// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.OrderID = int.Parse(Orderid.Text.ToString());
                entity.IsShow = int.Parse(CheckStop.SelectedValue.ToString());
                entity.Unit = Unit.Text.Trim();
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.AnalysisItem.AddAnalysisItem(entity);
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

                    int success = DAL.AnalysisItem.UpdateAnalysisItem(entity);
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