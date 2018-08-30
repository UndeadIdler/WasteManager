using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;
using FineUI;

namespace WasteManagement.Content.Plan
{
    public partial class Contract_Window : PageBase
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
                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                }
            }
        }


        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                Entity.Contract entity = DAL.Contract.GetContract(int.Parse(thisGuid));
                txt_ContractNumber.Text=entity.ContractNumber;
                //entity.ProduceID = 1;
                drop_handle.SelectedValue = entity.HandleID.ToString();
                drop_Waste.SelectedValue = entity.WasteCode;
                Date_Start.SelectedDate = entity.StartDate;
                Date_End.SelectedDate = entity.EndDate;
                Date_Sign.SelectedDate=entity.SignDate ;
                txt_Remark.Text=entity.Remark;
                NB_Amount.Text = entity.Amount.ToString();
                txt_Enterprise.Text = DAL.Enterprise.GetEnterpriseName(entity.ProduceID);
                if (entity.StatusID == 2)
                {
                    txt_ContractNumber.Readonly = true;
                    txt_Enterprise.Readonly = true;
                    drop_handle.Readonly = true;
                    drop_Waste.Readonly = true;
                    Date_Start.Readonly = true;
                    Date_End.Readonly = true;
                    Date_Sign.Readonly = true;
                    txt_Remark.Readonly = true;
                    NB_Amount.Readonly = true;
                    btn_save.Enabled = false;
                }

            }
        }


        private void InitDropDownList()//清空详细页面中所有的TextBox
        {
            DataTable dt = DAL.Waste.GetPartWaste(1);
            drop_Waste.DataTextField = "WasteName";
            drop_Waste.DataValueField = "WasteCode";
            drop_Waste.DataSource = dt;
            drop_Waste.DataBind();

            DataTable dt2 = DAL.Enterprise.GetPartEnterprise(2);
            drop_handle.DataTextField = "Name";
            drop_handle.DataValueField = "EnterpriseID";
            drop_handle.DataSource = dt2;
            drop_handle.DataBind();
        }

        #region 保存数据
        protected string checkInfo()
        {
            string ret = "";
            //if (txt_htnumber.Text.Trim() == "")
            //{
            //    ret += "请输入合同编号！";
            //}

            //if (hid_qyid.Text.Trim() == "")
            //{
            //    ret += "请输入企业信息！";
            //}
            //if (hid_qyid.Text.Trim() == "")
            //    ret += "企业信息不在数据库中，请先进行信息维护！";
            ////if (hid_qyid.Text.Trim() == "")
            ////    ret += "企业信息不在数据库中，请先进行信息维护！";
            //if (txt_jydate.Text == "")
            //    ret += "请输入交易时间！";
            //if (drop_type.SelectedValue.ToString().Trim() == "0")
            //    ret += "请输入交易类型！";
            //switch (drop_type.SelectedValue.ToString().Trim())
            //{
            //    case "2"://购入新增量
            //    case "4"://在外地购入
            //        if (drop_source.SelectedValue.ToString().Trim() == "0")
            //            ret += "请输入排污权来源类型！";

            //        if (txt_project.Text.Trim() == "")
            //            ret += "请输入项目信息！";
            //        break;
            //}
            int EID = DAL.Enterprise.GetEnterpriseID(txt_Enterprise.Text.Trim());
            if (EID == 0)
            {
                ret += "请选择正确的产生单位！";
            }
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Contract where ContractNumber='" + txt_ContractNumber.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ret += "该合同编号已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Contract where ContractNumber='" + txt_ContractNumber.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ret += "合同编号不能重复！";
                    }

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
                Entity.Contract entity = new Entity.Contract();
                entity.ContractNumber = txt_ContractNumber.Text.Trim();
                //int EID = DAL.Enterprise.GetEnterpriseID(txt_Enterprise.Text.Trim());
                //entity.ProduceID = EID;
                Entity.Enterprise enterprise = DAL.Enterprise.GetEnterpriseInfo(txt_Enterprise.Text.Trim());
                entity.ProduceID = enterprise.EnterpriseID;
                entity.ProduceArea = enterprise.AreaCode;
                entity.HandleID = int.Parse(drop_handle.SelectedValue.Trim());
                entity.WasteCode = drop_Waste.SelectedValue.Trim();
                entity.StartDate = Date_Start.SelectedDate;
                entity.EndDate = Date_End.SelectedDate;
                entity.SignDate = Date_Sign.SelectedDate;
                entity.Remark = txt_Remark.Text.Trim();
                entity.Amount = decimal.Parse(NB_Amount.Text.Trim());
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.StatusID = 1;
                entity.Total = 0;
                int iReturn = 0;
                if (sGuid == string.Empty || sGuid == null)
                {
                    iReturn = DAL.Contract.AddContract(entity);
                }
                else
                {

                    entity.ContractID = int.Parse(sGuid);
                    iReturn = DAL.Contract.UpdateContract(entity);
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
    }
}