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

namespace WasteManagement.Content.Fix
{
    public partial class PondChange_Window : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");
            if (!IsPostBack)
            {

                InitDropDownList();
            }
        }

        private void InitDropDownList()//清空详细页面中所有的TextBox
        {
            DataTable dt = DAL.Pond.GetAllPond();
            drop_pond.DataTextField = "Name";
            drop_pond.DataValueField = "PondID";
            drop_pond.DataSource = dt;
            drop_pond.DataBind();

            int PondID = int.Parse(drop_pond.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            txt_Waste.Text = entity.WasteName;
            //txt_Old.Text = entity.Used.ToString();
            txt_Capacity.Text = entity.Capacity.ToString();
            //txt_Old.Text = DAL.PondUsed.GetPondUsedAmount(PondID).ToString();
            txt_Old.Text = entity.Used.ToString();
        }
      

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (NB_New.Text.Trim() == "") msg += "请输入修正后的量！";
            int PondID = int.Parse(drop_pond.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            decimal Capacity = entity.Capacity;
            decimal Fix = decimal.Parse(NB_New.Text.Trim());
            decimal Remain = Capacity - Fix;
            if (Remain < 0)
            {
                msg += "修正量已超所设的容量！";
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
                Entity.PondChange entity = new Entity.PondChange();
                entity.PondID = int.Parse(drop_pond.SelectedValue.Trim());// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.OldAmount = decimal.Parse(txt_Old.Text.Trim());// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.NewAmount = decimal.Parse(NB_New.Text.Trim());
                entity.Remark = txt_Remark.Text.Trim();
                entity.CreateDate = DateTime.Now;
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                //Add
                decimal capacity = decimal.Parse(txt_Capacity.Text.Trim());
                //int success = DAL.PondChange.AddPondChange(entity,capacity);
                int success = DAL.PondChange.AddPondChangeEx(entity);
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


        }
        #endregion

        protected void drop_pond_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PondID = int.Parse(drop_pond.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            txt_Waste.Text = entity.WasteName;
            //txt_Old.Text = entity.Used.ToString();
            txt_Capacity.Text = entity.Capacity.ToString();
            txt_Old.Text = entity.Used.ToString();
            //txt_Old.Text = DAL.PondUsed.GetPondUsedAmount(PondID).ToString();
        }
    }
}