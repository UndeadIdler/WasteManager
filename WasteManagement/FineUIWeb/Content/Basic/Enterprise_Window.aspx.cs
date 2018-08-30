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

namespace WasteManagement.Content.Basic
{
    public partial class Enterprise_Window : PageBase
    {
        // private string sGuid =string.Empty;
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
                InitDropDownList();
                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                }
            }
        }




        private void InitDropDownList()//清空详细页面中所有的TextBox
        {
            DataTable dt = DAL.Area.GetAllAreaEx2(0);
            //DataRow dr = dt.NewRow();
            //dr["AreaCode"] = "0";
            //dr["ShortName"] = "全部";
            //dt.Rows.InsertAt(dr, 0);
            drp_sd.DataTextField = "ShortName";
            drp_sd.DataValueField = "AreaCode";
            drp_sd.DataSource = dt;
            drp_sd.DataBind();

            DataTable dt2 = DAL.Industry.GetAllIndustry();
            //DataRow dr2 = dt2.NewRow();
            //dr2["Code"] = "0";
            //dr2["Description"] = "请选择";
            //dt2.Rows.InsertAt(dr2, 0);
            drop_industry.DataTextField = "Description";
            drop_industry.DataValueField = "Code";
            drop_industry.DataSource = dt2;
            drop_industry.DataBind();


            DataTable dt3 = DAL.Type.GetAllType();
            //DataRow dr3 = dt3.NewRow();
            //dr3["Code"] = "0";
            //dr3["Description"] = "请选择";
            //dt3.Rows.InsertAt(dr3, 0);
            drop_type.DataTextField = "Description";
            drop_type.DataValueField = "Code";
            drop_type.DataSource = dt3;
            drop_type.DataBind();
        }
      

        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                //BindGrid();

                Entity.Enterprise entity = DAL.Enterprise.GetEnterprise(int.Parse(thisGuid));
                txt_qymc.Text = entity.Name;
                if (!string.IsNullOrEmpty(entity.AreaCode))
                {
                    drp_sd.SelectedValue = entity.AreaCode;
                }
                if (!string.IsNullOrEmpty(entity.Type.ToString()))
                {
                    drp_sd.SelectedValue = entity.Type.ToString();
                }
                //else
                //{
                //    drp_sd.Items.FindByText("请选择").Selected = true;
                //}
                txt_cname.Text = entity.PastName;
                txt_dz.Text = entity.Address;
                txt_jgdm.Text = entity.LawManCode;
                if (entity.Industry!=-2)
                {
                    drop_industry.SelectedValue = entity.Industry.ToString();
                    //drop_industry.Items.FindByValue(ds.Tables[0].Rows[0]["行业类别"].ToString()).Selected = true;
                }

                txt_frdb.Text = entity.LawMan;
                txt_czhm.Text = entity.FaxNumber;
                txt_email.Text = entity.Email;
                txt_tel1.Text = entity.Telphone1;
                txt_OrgCode.Text = entity.OrganizationCode;
                txt_Postcode.Text = entity.PostCode;
                txt_mobile1.Text = entity.Phone1;
                if (entity.SetUpDate.ToString() != "" && entity.SetUpDate.ToString() != "1900-01-01 00:00:00.000" && entity.SetUpDate.ToString() != "1900/1/1 0:00:00")
                    txt_createdate.Text = DateTime.Parse(entity.SetUpDate.ToString()).ToString("yyyy-MM-dd");

            }
        }
        #endregion

        #region 保存数据

        private void AllTxtreadOnly()//设置详细页面中所有的TextBox为只读
        {
            //txt_pwNO.Enabled = false;
            txt_qymc.Enabled = false;
            txt_cname.Enabled = false;
            txt_dz.Enabled = false;
            txt_jgdm.Enabled = false;
            drp_sd.Enabled = false;
            txt_frdb.Enabled = false;
            txt_tel1.Enabled = false;
            txt_mobile1.Enabled = false;
            // txt_zfw1.Enabled = false;
            // txt_zfw2.Enabled = false;


            txt_czhm.Enabled = false;
            txt_email.Enabled = false;
            //txt_cp.Enabled = false;
            //txt_other.Enabled = false;
            txt_createdate.Enabled = false;
            drop_industry.Enabled = false;
        }
        private string checkInput()
        {
            string msg = "";

            if (txt_qymc.Text.Trim() == "") msg += "请输入企业名称！";
            if (drp_sd.SelectedValue.ToString() == "3304") msg += "请选择企业所属区域！";
            //if (drop_status.SelectedValue.ToString() == "0") msg += "请选择企业生产状态！";
            //if (drop_industry.SelectedValue.ToString() == "0") msg += "请选择企业所属行业！";
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Enterprise where Name='" + txt_qymc.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该企业已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Enterprise where Name='" + txt_qymc.Text.Trim() + "' and EnterpriseID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "单位全称不能重复！";
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
                #region  保存Enterprise
                //保存Enterprise
                Entity.Enterprise enterprise = new Entity.Enterprise();
                enterprise.Name = txt_qymc.Text.Trim();
                enterprise.AreaCode = drp_sd.SelectedValue.ToString();
                enterprise.PastName = txt_cname.Text.Trim();
                enterprise.Address = txt_dz.Text.Trim();
                enterprise.LawManCode = txt_jgdm.Text.Trim();
                enterprise.LawMan = txt_frdb.Text.Trim();
                enterprise.Email = txt_email.Text.Trim();
                enterprise.Telphone1 = txt_tel1.Text.Trim();
                enterprise.Phone1 = txt_mobile1.Text.Trim();
                enterprise.OrganizationCode = txt_OrgCode.Text.Trim();
                enterprise.PostCode = txt_Postcode.Text.Trim();
                enterprise.Type = int.Parse(drop_type.SelectedValue.Trim());
                try
                {
                    enterprise.SetUpDate = DateTime.Parse(txt_createdate.Text.ToString());
                }
                catch
                { enterprise.SetUpDate = DateTime.Parse("1900-01-01 0:00:00"); }
                enterprise.FaxNumber = txt_czhm.Text.Trim();
                if (drop_industry.SelectedValue == null)
                {
                    enterprise.Industry = 0;
                }
                else
                {
                    enterprise.Industry = int.Parse(drop_industry.SelectedValue.ToString());
                }
                enterprise.CreateDate = DateTime.Now;
                enterprise.UpdateDate = DateTime.Now;
                enterprise.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                enterprise.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();

                int iReturn = 0;
                if (sGuid == string.Empty || sGuid == null)
                {
                    iReturn = DAL.Enterprise.AddEnterprise(enterprise);
                }
                else
                {

                    enterprise.EnterpriseID = int.Parse(sGuid);
                    iReturn = DAL.Enterprise.UpdateEnterprise(enterprise);
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
        #endregion
        }

        #endregion
    }
}
