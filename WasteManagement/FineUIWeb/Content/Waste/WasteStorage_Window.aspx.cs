using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Waste
{
    public partial class WasteStorage_Window : PageBase
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
                //BindDropdownList();
                if (Request.QueryString["id"] != null)
                {
                    sGuid = Request.QueryString["id"].ToString().Trim();
                    LoadData(sGuid);
                }
            }
        }

        private void BindDropdownList(string WasteCode)
        {

            DataTable dt = DAL.Pond.QueryPond(WasteCode,"");
            drop_Pond.DataTextField = "Name";
            drop_Pond.DataValueField = "PondID";
            drop_Pond.DataSource = dt;
            drop_Pond.DataBind();
        }


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                hf_StoreID.Text = thisGuid;
                Entity.WasteStorage entity = DAL.WasteStorage.GetWasteStorage(int.Parse(thisGuid));
                txt_BillNumber.Text = entity.BillNumber;
                hf_PlanID.Text = entity.PlanID.ToString();
                tb_PlanNumber.Text = entity.PlanNumber;
                txt_enterprise.Text = entity.ProduceName;
                hf_EID.Text = entity.EnterpriseID.ToString();
                txt_Waste.Text = entity.WasteName;
                txt_CarNumber.Text = entity.CarNumber;
                hid_WasteCode.Text = entity.WasteCode;
                BindDropdownList(entity.WasteCode);
                drop_Pond.SelectedValue = entity.PondID.ToString();
                Date_Start.SelectedDate = entity.DateTime;
                NB_Amount.Text = entity.Amount.ToString();
                txt_Driver.Text = entity.RealName;
                hf_DriverID.Text = entity.DriverID.ToString();
                txt_Receiver.Text = entity.ReceiverName;
                hf_ReceiverID.Text = entity.ReceiverID.ToString();
                hf_CarID.Text = entity.CarID.ToString();
                if (entity.Status >= 2)
                { 
                //全部只读，而且不可保存
                    txt_BillNumber.Readonly = true;
                    tb_PlanNumber.Readonly = true;
                    txt_enterprise.Readonly = true;
                    txt_Waste.Readonly = true;
                    txt_CarNumber.Readonly = true;
                    //BindDropdownList(entity.WasteCode);
                    drop_Pond.Readonly = true;
                    Date_Start.Readonly = true;
                    NB_Amount.Readonly = true;
                    txt_Driver.Readonly = true;
                    txt_Receiver.Readonly = true;
                    Grid1.Enabled = false;
                    btn_save.Enabled = false;
                }
            }
        }
        #endregion

        #region BindGrid
        /// <summary>
        /// [ISingleGridPage]重新绑定表格
        /// </summary>
        public void BindGrid()
        {
            // 2.获取当前分页数据
            DataTable table = GetPagedDataTable();

            // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
            Grid1.RecordCount = RowNum;

            // 3.绑定到Grid
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 模拟数据库分页
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable()
        {
            int pageIndex = Grid1.PageIndex;
            int pageSize = Grid1.PageSize;

            string sortField = Grid1.SortField;
            string sortDirection = Grid1.SortDirection;

            //查询数据
            string selectStr = string.Empty;

            DataTable table2 = DAL.TransferPlan.QueryTransferPlan(txt_ContractNumber.Text.Trim(), txt_Name.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), txt_WasteName.Text.Trim(), 2);

            RowNum = table2.Rows.Count;

            DataView view2 = table2.DefaultView;
            //view2.Sort = String.Format("{0} {1}", sortField, sortDirection);

            DataTable table = view2.ToTable();

            DataTable paged = table.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > table.Rows.Count)
            {
                rowend = table.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(table.Rows[i]);
            }

            return paged;
        }

        #endregion

        #region 保存数据
        protected string checkInfo()
        {
            //判断各个ID是否存在，并赋值
            string ret = "";
            int PlanID = 0;
            if (!string.IsNullOrEmpty(tb_PlanNumber.Text.Trim()))
            {
                PlanID = DAL.TransferPlan.GetPlanIDByNumber(tb_PlanNumber.Text);
                if (PlanID == 0)
                {
                    ret += "请选择正确的合同编号！";
                }
                else
                {
                    hf_PlanID.Text = PlanID.ToString();
                }    
            }
            if (PlanID > 0)
            {
                Entity.TransferPlan plan = DAL.TransferPlan.GetTransferPlan(PlanID);
                if (Date_Start.SelectedDate < plan.StartDate)
                {
                    ret += "时间早于合同的起始时间！";
                }
                if (Date_Start.SelectedDate > plan.EndDate)
                {
                    ret += "时间晚于合同的结束时间！";
                }
                decimal amount = plan.PlanAmount;
                decimal Used = DAL.TransferPlan.GetPlanAmount(PlanID);
                decimal PlanAmount = decimal.Parse(NB_Amount.Text.Trim());
                decimal Old = 0;
                if (!string.IsNullOrEmpty(sGuid))
                {
                    Entity.WasteStorage ws= DAL.WasteStorage.GetWasteStorage(int.Parse(sGuid));
                    Old = ws.Amount;
                }
                Entity.Pond pond = DAL.Pond.GetPond(int.Parse(drop_Pond.SelectedValue.ToString()));
                decimal capacity = pond.Capacity;
                decimal used = pond.Used;
                if (used + PlanAmount - Old > capacity)
                {
                    ret += "罐池的剩余量不足,罐池的剩余量为" + (capacity - used).ToString() + "!";
                }
                if (Used + PlanAmount - Old > amount)
                {
                    ret += "计划的剩余量不足,计划的剩余量为" + (amount - Used).ToString() + "!";
                }
            }
            int DID=DAL.Driver.GetDriverID(txt_Driver.Text.Trim());
            if (DID == 0)
            {
                ret += "请选择正确的驾驶员！";
            }
            else
            {
                hf_DriverID.Text = DID.ToString();
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

            int RID = DAL.User.GetUserID(txt_Receiver.Text.Trim(), 2);
            if (RID == 0)
            {
                ret += "请选择正确的签收人！";
            }
            else
            {
                hf_ReceiverID.Text = RID.ToString();
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
                Entity.WasteStorage entity = new Entity.WasteStorage();
                entity.PlanID = int.Parse(hf_PlanID.Text);
                entity.BillNumber = txt_BillNumber.Text.Trim();
                entity.PlanNumber = tb_PlanNumber.Text.Trim();
                entity.EnterpriseID = int.Parse(hf_EID.Text.Trim());
                entity.DriverID = int.Parse(hf_DriverID.Text.Trim());
                entity.CarID = int.Parse(hf_CarID.Text.Trim());
                entity.PondID = int.Parse(drop_Pond.SelectedValue.ToString());
                entity.ReceiverID = int.Parse(hf_ReceiverID.Text.Trim());
                entity.WasteCode = hid_WasteCode.Text;
                entity.DateTime = Date_Start.SelectedDate;
                entity.Amount = decimal.Parse(NB_Amount.Text.Trim());
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.Status = 1;
                int iReturn = 0;
                if (sGuid == string.Empty || sGuid == null)
                {
                    iReturn = DAL.WasteStorage.AddWasteStorage(entity);
                }
                else
                {

                    entity.StorageID= int.Parse(sGuid);
                    iReturn = DAL.WasteStorage.UpdateWasteStorage(entity);
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



        #region 合同查询
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;

            BindGrid();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndex != -1)
            {
                object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
                hf_PlanID.Text = keys[0].ToString();
                //txt_source.Text = keys[2].ToString();
                Entity.TransferPlan plan = DAL.TransferPlan.GetTransferPlan(int.Parse(keys[0].ToString()));
                tb_PlanNumber.Text = plan.PlanNumber;
                txt_enterprise.Text = plan.ProduceName;
                hf_EID.Text = plan.ProduceID.ToString();
                txt_Waste.Text = plan.WasteName;
                hid_WasteCode.Text = plan.WasteCode;
                Date_Start.MinDate = plan.StartDate;
                Date_Start.MaxDate = plan.EndDate;
                Window_DepartQuery.Hidden = true;

                //罐池号的DropdownList变化
                BindDropdownList(plan.WasteCode);
            }
        }

        #endregion

        protected void txt_source_TriggerClick(object sender, EventArgs e)
        {
            Window_DepartQuery.Hidden = false;
        }





    }
}