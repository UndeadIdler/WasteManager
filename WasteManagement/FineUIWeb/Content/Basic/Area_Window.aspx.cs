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
    public partial class Area_Window : PageBase
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
                Entity.Area entity = DAL.Area.GetAreaByID(int.Parse(sId));
                txt_name.Text = entity.FullName;
                //txt_pwNO.Text = ds.Tables[0].Rows[0]["排污权证号"].ToString();
                txt_jc.Text = entity.ShortName;
                txt_areacode.Text = entity.AreaCode.ToString();
                txt_bm.Text = entity.LetterCode;

                CheckStop.SelectedValue = entity.IsDelete.ToString();
                Orderid.Text = entity.OrderID.ToString();
            }

        }


        #endregion

        #region 保存数据

        private string checkInput()
        {
            string msg = "";

            if (txt_name.Text.Trim() == "") msg += "请输入区域名称！";

            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Area where FullName='" + txt_name.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该区域名称已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Area where FullName='" + txt_name.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "区域名称不能重复！";
                    }

            }
            if (txt_jc.Text.Trim() == "") msg += "请输入区域简称！";
            //int i = 99;
            //if (!int.TryParse(txt_areacode.Text.ToString().Trim(),out i))
            //{
            //    msg += "区域数字编码必须均为数字！";
            //}
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Area where ShortName='" + txt_jc.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该区域简称已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Area where ShortName='" + txt_jc.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "区域简称不能重复！";
                    }

            }
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from Area where AreaCode='" + txt_areacode.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "该区域编码已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from Area where AreaCode='" + txt_areacode.Text.Trim() + "' and ID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        msg += "区域编码不能重复！";
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
                Entity.Area entity = new Entity.Area();
                entity.FullName = txt_name.Text.Trim();// = ds.Tables[0].Rows[0]["单位全称"].ToString();
                entity.ShortName = txt_jc.Text.Trim();// = ds.Tables[0].Rows[0]["单位曾用名全称"].ToString();
                entity.LetterCode = txt_bm.Text.Trim();
                entity.OrderID = int.Parse(Orderid.Text.ToString());
                entity.AreaCode = int.Parse(txt_areacode.Text.Trim());// = ds.Tables[0].Rows[0]["单位法人代码"].ToString();
                entity.IsDelete = int.Parse(CheckStop.SelectedValue.ToString());
                if (string.IsNullOrEmpty(sGuid))
                {
                    //Add
                    int success = DAL.Area.AddArea(entity);
                    if (success==1)
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

                    int success = DAL.Area.UpdateArea(entity);
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