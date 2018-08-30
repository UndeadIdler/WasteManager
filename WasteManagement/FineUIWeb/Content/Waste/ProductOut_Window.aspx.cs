using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Waste
{
    public partial class ProductOut_Window : PageBase
    {
        private string sGuid//所选择操作列记录对应的id
        {
            get { return (string)ViewState["sGuid"]; }
            set { ViewState["sGuid"] = value; }
        }

        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase pb = new PageBase();
            if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");
            if (!IsPostBack)
            {
                BindDropdownList();
                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                }
            }
        }

        private void BindDropdownList()
        {

            DataTable dt = DAL.Pond.GetPartPond(2);
            drop_Pond.DataTextField = "Name";
            drop_Pond.DataValueField = "PondID";
            drop_Pond.DataSource = dt;
            drop_Pond.DataBind();
            if (drop_Pond.SelectedIndex == 0)
            {
                Entity.Pond pond = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue));
                txt_Waste.Text = pond.WasteName;
                hid_WasteCode.Text = pond.Stores;            
            }
        }


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                Entity.ProductOut entity = DAL.ProductOut.GetProductOut(int.Parse(thisGuid));
                txt_enterprise.Text = entity.EnterpriseName;
                hf_EID.Text = entity.ReceiverEnterpriseID.ToString();
                txt_Waste.Text = entity.WasteName;
                hid_WasteCode.Text = entity.WasteCode;
                //BindDropdownList(entity.WasteCode);
                drop_Pond.SelectedValue = entity.PondID.ToString();
                Date_Start.SelectedDate = entity.DateTime;
                NB_Amount.Text = entity.Amount.ToString();
                txt_Driver.Text = entity.DriverName;
                hf_DriverID.Text = entity.DriverID.ToString();
                txt_Consignor.Text = entity.ConsignorName;
                hf_ConsignorID.Text = entity.ConsignorID.ToString();
                hf_CarID.Text = entity.CarID.ToString();
                txt_CarNumber.Text = entity.CarNumber;
                if (entity.Status == 2)
                {
                    //全部只读，而且不可保存
                    txt_enterprise.Readonly = true;
                    txt_Waste.Readonly = true;
                    drop_Pond.Readonly = true;
                    Date_Start.Readonly = true;
                    NB_Amount.Readonly = true;
                    txt_Driver.Readonly = true;
                    txt_Consignor.Readonly = true;
                    btn_save.Enabled = false;
                    txt_CarNumber.Readonly = true;
                }
            }
        }
        #endregion
 

        #region 保存数据
        protected string checkInfo()
        {
            //判断各个ID是否存在，并赋值
            string ret = "";
            int DID = DAL.Driver.GetDriverID(txt_Driver.Text.Trim());
            if (DID == 0)
            {
                ret += "请选择正确的驾驶员！";
            }
            else
            {
                hf_DriverID.Text = DID.ToString();
            }
            int RID = DAL.User.GetUserID(txt_Consignor.Text.Trim(), 3);
            if (RID == 0)
            {
                ret += "请选择正确的发货人！";
            }
            else
            {
                hf_ConsignorID.Text = RID.ToString();
            }
            int EID = DAL.Enterprise.GetEnterpriseID(txt_enterprise.Text.Trim());
            if (EID == 0)
            {
                ret += "请选择正确的收货单位！";
            }
            else
            {
                hf_EID.Text = EID.ToString();
            }

            int CID = DAL.CarNumber.GetCarNumberID(txt_CarNumber.Text.Trim());
            if (CID == 0)
            {
                ret += "请选择正确的车牌！";
            }
            else
            {
                hf_CarID.Text = CID.ToString();
            }

            Entity.Pond pond = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue.ToString()));
            decimal amount = decimal.Parse(NB_Amount.Text.Trim());
            decimal used = pond.Used;
            if (used < amount)
            {
                ret += "罐池的库存量不足,罐池的库存量为" + used.ToString() + "!";
            }
            return ret;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string str = checkInfo();
            if (str != "")
            { Alert.ShowInTop(str, MessageBoxIcon.Warning); }
            else
            {
                Entity.ProductOut entity = new Entity.ProductOut();
                entity.ReceiverEnterpriseID = int.Parse(hf_EID.Text.Trim());
                entity.DriverID = int.Parse(hf_DriverID.Text.Trim());
                entity.PondID = int.Parse(drop_Pond.SelectedValue.ToString());
                entity.ConsignorID = int.Parse(hf_ConsignorID.Text.Trim());
                entity.WasteCode = hid_WasteCode.Text;
                entity.DateTime = Date_Start.SelectedDate;
                entity.Amount = decimal.Parse(NB_Amount.Text.Trim());
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.CarID = int.Parse(hf_CarID.Text.Trim());
                entity.Status = 1;
                int iReturn = 0;
                if (sGuid == string.Empty || sGuid == null)
                {
                    iReturn = DAL.ProductOut.AddProductOut(entity);
                }
                else
                {

                    entity.OutID = int.Parse(sGuid);
                    iReturn = DAL.ProductOut.UpdateProductOut(entity);
                }
                if (iReturn == 1)
                {
                    Alert.ShowInTop(" 保存成功！", MessageBoxIcon.Information);
                    //            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    Alert.ShowInTop(" 保存失败！", MessageBoxIcon.Warning);
                }
            }
        }


        #endregion


        protected void drop_PondChange(object sender, EventArgs e)
        {
            Entity.Pond pond = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue));
            txt_Waste.Text = pond.WasteName;
            hid_WasteCode.Text = pond.Stores;
        }

    }
}