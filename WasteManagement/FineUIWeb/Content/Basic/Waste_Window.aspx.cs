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
    public partial class Waste_Window : PageBase
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
                InitDropDownList();
                if (Request.QueryString["ID"] != null)
                {
                    string sId = Request.QueryString["ID"].ToString().Trim();
                    sGuid = Request.QueryString["ID"].ToString().Trim();
                    LoadData(sId);
                }
            }
        }


        private void InitDropDownList()//清空详细页面中所有的TextBox
        {
            DataTable dt = DAL.Type.GetAllType2();
            DataRow dr = dt.NewRow();
            drop_Type.DataTextField = "Description";
            drop_Type.DataValueField = "Code";
            drop_Type.DataSource = dt;
            drop_Type.DataBind();
        }


        #region 加载数据
        private void LoadData(string sId)
        {
            if (sId != string.Empty)
            {
                Entity.Waste entity=DAL.Waste.GetWaste(int.Parse(sId));
                if (entity != null)
                { 
                    WasteCode.Text = entity.WasteCode;
                    //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
                    WasteName.Text = entity.WasteName;
                    List.Text = entity.List;
                    Unit.Text = entity.Unit;
                    drop_Type.SelectedValue = entity.Type.ToString();
                    CheckStop.SelectedValue = entity.IsShow.ToString();
                    Orderid.Text = entity.OrderID.ToString();
                
                }

                
            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (WasteCode.Text.Trim() == "") msg += "请输入污染物代码！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Waste where WasteCode='" + WasteCode.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该污染物代码已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Waste where WasteName='" + WasteName.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "污染物名称不能重复！";
                    }

            }
            if (List.Text.Trim() == "") msg += "请输入污染物全称！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Waste where List='" + List.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该污染物全称已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Waste where List='" + List.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "污染物全称不能重复！";
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
                Entity.Waste entity = new Entity.Waste();
                entity.WasteCode = WasteCode.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.WasteName = WasteName.Text.Trim();// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.List = List.Text.Trim();
                entity.OrderID = int.Parse(Orderid.Text.ToString());
                entity.Unit = Unit.Text.Trim();// = ds.Tables[0].Rows[0]["单位法人代码"].ToString();
                entity.IsShow = int.Parse(CheckStop.SelectedValue.ToString());
                entity.Type = int.Parse(drop_Type.SelectedValue.ToString());
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.Waste.AddWaste(entity);
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

                    int success = DAL.Waste.UpdateWaste(entity);
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