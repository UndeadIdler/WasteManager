using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using FineUI;
using DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace WasteManagement.Content.Basic
{
    public partial class Pond_Window : PageBase
    {
        private DataBasic dataBasic = new DataBasic();
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
            DataTable dt = DAL.Waste.GetAllWaste();
            DataRow dr = dt.NewRow();
            //dr["AreaCode"] = "0";
            //dr["ShortName"] = "全部";
            //dt.Rows.InsertAt(dr, 0);
            drop_Waste.DataTextField = "WasteName";
            drop_Waste.DataValueField = "WasteCode";
            drop_Waste.DataSource = dt;
            drop_Waste.DataBind();
        }



        #region 加载数据
        private void LoadData(string sId)
        {
            if (sId != string.Empty)
            {
                Entity.Pond entity = DAL.Pond.GetPond(int.Parse(sId));
                txt_name.Text = entity.Name;
                //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
                txt_areacode.Text = entity.Capacity.ToString();
                drop_Waste.SelectedValue = entity.Stores;
                CheckStop.SelectedValue = entity.IsDelete.ToString();
                Orderid.Text = entity.Number.ToString();
                //hfUsed.Text = entity.Used.ToString();
            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (txt_name.Text.Trim() == "") msg += "请输入罐池名称！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Pond where Name='" + txt_name.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该罐池名称已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Pond where Name='" + txt_name.Text.Trim() + "' and PondID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "罐池名称不能重复！";
                    }

            }
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Pond where Number='" + Orderid.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "编号已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Pond where Number='" + Orderid.Text.Trim() + "' and PondID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "编号不能重复！";
                    }

            }
            decimal Capacity = decimal.Parse(txt_areacode.Text.Trim());
            decimal used = 0;
            //得到PondUsed中的值
            //if (!string.IsNullOrEmpty(hfUsed.Text))
            //{
            //    used = decimal.Parse(hfUsed.Text);
            //}
            if (!string.IsNullOrEmpty(sGuid))
            {
                used = DAL.Pond.GetPond(int.Parse(sGuid)).Used;
                //used = DAL.PondUsed.GetPondUsedAmount(int.Parse(sGuid));
            }

            decimal Remain = Capacity - used;
            if (Remain < 0)
            {
                msg += "已使用量已超所设的容量！";
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
                Entity.Pond entity = new Entity.Pond();
                entity.Name = txt_name.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.Number = int.Parse(Orderid.Text.ToString());
                entity.Capacity = decimal.Parse(txt_areacode.Text.Trim());// = ds.Tables[0].Rows[0]["单位法人代码"].ToString();
                entity.IsDelete = int.Parse(CheckStop.SelectedValue.ToString());
                entity.Stores = drop_Waste.SelectedValue.Trim();
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.Pond.AddPond(entity);
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
                    entity.PondID = int.Parse(sGuid);
                    //decimal used = decimal.Parse(hfUsed.Text);
                    //entity.Remain = entity.Capacity - used;
                    int success = DAL.Pond.UpdatePond(entity);
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