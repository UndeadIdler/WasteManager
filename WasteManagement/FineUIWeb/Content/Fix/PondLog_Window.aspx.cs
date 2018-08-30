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
    public partial class PondLog_Window : PageBase
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
            DataTable dt = DAL.Waste.GetUseWaste();
            drop_Waste.DataTextField = "WasteName";
            drop_Waste.DataValueField = "Stores";
            drop_Waste.DataSource = dt;
            drop_Waste.DataBind();

            GetInfo();

            //string WasteCode = drop_Waste.SelectedValue;
            //DataTable dt2 = DAL.Pond.QueryPond(WasteCode, "");
            //drop_Source.DataTextField = "Name";
            //drop_Source.DataValueField = "PondID";
            //drop_Source.DataSource = dt2;
            //drop_Source.DataBind();

            //drop_To.DataTextField = "Name";
            //drop_To.DataValueField = "PondID";
            //drop_To.DataSource = dt2;
            //drop_To.DataBind();
        }


        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (NB_New.Text.Trim() == "") msg += "请输入倒库量！";
            int PondID1 = int.Parse(drop_Source.SelectedValue.Trim());
            Entity.Pond entity1 = DAL.Pond.GetPond(PondID1);
            decimal Change = decimal.Parse(NB_New.Text.Trim());
            decimal Capacity = entity1.Capacity;
            decimal used = entity1.Used;
            //decimal used = DAL.PondUsed.GetPondUsedAmount(entity1.PondID);
            decimal Remain = used-Change;
            if (Remain < 0)
            {
                msg += "来源库库存不够！";
            }
            int PondID2 = int.Parse(drop_To.SelectedValue.Trim());
            Entity.Pond entity2 = DAL.Pond.GetPond(PondID2);
            decimal Capacity2 = entity2.Capacity;
            decimal used2 = entity2.Used;
            //decimal used2 = DAL.PondUsed.GetPondUsedAmount(entity2.PondID);
            decimal Remain2 = Capacity2 - used2-Change;
            if (Remain2 < 0)
            {
                msg += "修正后的导入库已超所设的容量！";
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
                Entity.PondLog entity = new Entity.PondLog();
                entity.SourceID = int.Parse(drop_Source.SelectedValue.Trim());
                entity.ToID = int.Parse(drop_To.SelectedValue.Trim());
                entity.CreateDate = DateTime.Now;
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.Amount = decimal.Parse(NB_New.Text.Trim());
                //Add
                //decimal capacity = decimal.Parse(txt_Capacity.Text.Trim());
                int PondID1 = int.Parse(drop_Source.SelectedValue.Trim());
                Entity.Pond entity1 = DAL.Pond.GetPond(PondID1);
                decimal used1 = entity1.Used;
                //decimal used1 = DAL.PondUsed.GetPondUsedAmount(entity1.PondID);
                decimal Capacity = entity1.Capacity;
                int PondID2 = int.Parse(drop_To.SelectedValue.Trim());
                Entity.Pond entity2 = DAL.Pond.GetPond(PondID2);
                decimal used2 = entity2.Used;
                //decimal used2 = DAL.PondUsed.GetPondUsedAmount(entity2.PondID);
                decimal Capacity2 = entity2.Capacity;

                decimal U1 = used1 - entity.Amount;
                decimal U2 = used2 + entity.Amount;
                //decimal R1 = Capacity - used1 + entity.Amount;
                //decimal R2 = Capacity2 - used2 - entity.Amount;
                //int success = DAL.PondLog.AddPondLog(entity, U1,R1,U2,R2);
                int success = DAL.PondLog.AddPondLogEx(entity, U1,U2);
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

        protected void drop_Waste_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string WasteCode = drop_Waste.SelectedValue;
            //DataTable dt = DAL.Pond.QueryPond(WasteCode, "");
            //drop_Source.DataTextField = "Name";
            //drop_Source.DataValueField = "PondID";
            //drop_Source.DataSource = dt;
            //drop_Source.DataBind();

            //drop_To.DataTextField = "Name";
            //drop_To.DataValueField = "PondID";
            //drop_To.DataSource = dt;
            //drop_To.DataBind();
            GetInfo();
        }

        protected void drop_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PondID = int.Parse(drop_Source.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            //txt_Used1.Text = entity.Used.ToString();
            txt_Capacity1.Text = entity.Capacity.ToString();
            decimal Used = entity.Used;
            //decimal Used = DAL.PondUsed.GetPondUsedAmount(PondID);
            txt_Used1.Text = Used.ToString();
        }

        protected void drop_To_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PondID = int.Parse(drop_To.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            //txt_Used2.Text = entity.Used.ToString();
            txt_Capacity2.Text = entity.Capacity.ToString();
            decimal Used = entity.Used;
            //decimal Used = DAL.PondUsed.GetPondUsedAmount(PondID);
            txt_Used2.Text = Used.ToString();
        }

        protected void GetInfo()
        {
            string WasteCode = drop_Waste.SelectedValue;
            DataTable dt = DAL.Pond.QueryPond(WasteCode, "");
            drop_Source.DataTextField = "Name";
            drop_Source.DataValueField = "PondID";
            drop_Source.DataSource = dt;
            drop_Source.DataBind();

            int PondID = int.Parse(drop_Source.SelectedValue.Trim());
            Entity.Pond entity = DAL.Pond.GetPond(PondID);
            decimal Used = entity.Used;
            //decimal Used = DAL.PondUsed.GetPondUsedAmount(PondID);
            //txt_Used1.Text = entity.Used.ToString();
            txt_Capacity1.Text = entity.Capacity.ToString();
            txt_Capacity2.Text = entity.Capacity.ToString();
            txt_Used1.Text = Used.ToString();
            txt_Used2.Text = Used.ToString();

            drop_To.DataTextField = "Name";
            drop_To.DataValueField = "PondID";
            drop_To.DataSource = dt;
            drop_To.DataBind();

            //int PondID2 = int.Parse(drop_To.SelectedValue.Trim());
            //Entity.Pond entity2 = DAL.Pond.GetPond(PondID2);


            //txt_Used2.Text = entity.Used.ToString();
            //txt_Capacity2.Text = entity2.Capacity.ToString();
        }
    }
}