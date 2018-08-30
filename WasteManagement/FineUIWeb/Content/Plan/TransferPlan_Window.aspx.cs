using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using System.Text;

namespace WasteManagement.Content.Plan
{
    public partial class TransferPlan_Window : PageBase
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

            //DataTable dt = DAL.Status.GetAllStatus();
            //DataRow dr = dt.NewRow();
            //dr["Code"] = "-2";
            //dr["Description"] = "全部";
            //dt.Rows.InsertAt(dr, 0);
            //drop_Status.DataTextField = "Description";
            //drop_Status.DataValueField = "Code";
            //drop_Status.DataSource = dt;
            //drop_Status.DataBind();
        }


        #region 加载数据
        private void LoadData(string thisGuid)
        {
            if (thisGuid != string.Empty)
            {
                Entity.TransferPlan entity = DAL.TransferPlan.GetTransferPlan(int.Parse(thisGuid));
                txt_PlanNumber.Text = entity.PlanNumber;
                hid_htid.Text = entity.ContractID.ToString();
                Entity.Contract con = DAL.Contract.GetContract(entity.ContractID);
                //txt_source.Text = DAL.Enterprise.GetEnterpriseName(con.ProduceID);
                txt_source.Text = con.ContractNumber;
                txt_Waste.Text = con.WasteName;
                hid_WasteCode.Text = con.WasteCode;
                Date_Start.SelectedDate = entity.StartDate;
                Date_End.SelectedDate = entity.EndDate;
                Date_Sign.SelectedDate = entity.ApprovalDate;
                NB_Amount.Text = entity.PlanAmount.ToString();
                txt_Remark.Text = entity.Remark;
                if (entity.StatusID == 2)
                {
                    txt_ContractNumber.Readonly = true;
                    txt_PlanNumber.Readonly = true;
                    txt_source.Readonly = true;
                    txt_Waste.Readonly = true;
                    Date_Start.Readonly = true;
                    Date_End.Readonly = true;
                    Date_Sign.Readonly = true;
                    txt_Remark.Readonly = true;
                    NB_Amount.Readonly = true;
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

            //string Start = "";
            //string End = "";
            //if (string.IsNullOrEmpty(DateStart.Text.Trim()) && string.IsNullOrEmpty(DateEnd.Text.Trim()))
            //{
            //    Start = DateTime.Now.Date.ToString();
            //    End = DateTime.Now.Date.ToString();

            //}
            DataTable table2 = DAL.Contract.QueryContract(txt_ContractNumber.Text.Trim(), txt_Name.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), txt_WasteName.Text.Trim(), 2);

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
            if (sGuid == string.Empty || sGuid == null)
            {
                string checkstr = "select * from TransferPlan where PlanNumber='" + txt_PlanNumber.Text.Trim() + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ret += "该计划编号已存在！";
                    }
            }
            else
            {
                string checkstr = "select * from TransferPlan where PlanNumber='" + txt_PlanNumber.Text.Trim() + "' and PlanID!='" + sGuid + "'";
                DataSet dscheck = new MyDataOp().CreateDataSet(checkstr);
                if (dscheck != null)
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ret += "计划编号不能重复！";
                    }

            }
            int ContractID = 0;
            if (!string.IsNullOrEmpty(txt_source.Text))
            {
                ContractID = DAL.Contract.GetContractIDByNumber(txt_source.Text);
                if (ContractID == 0)
                {
                    ret += "请选择正确的合同编号！";
                }
                else
                {
                    hid_htid.Text = ContractID.ToString();
                }
            }
            if (ContractID > 0)
            {
                Entity.Contract contract = DAL.Contract.GetContract(ContractID);
                if (Date_Start.SelectedDate < contract.StartDate)
                {
                    ret += "开始时间早于合同的起始时间！";
                }
                if (Date_End.SelectedDate > contract.EndDate)
                {
                    ret += "结束时间晚于合同的结束时间！";
                }
                if (Date_Start.SelectedDate > Date_End.SelectedDate)
                {
                    ret += "结束时间小于开始时间！";
                }
                decimal amount = contract.Amount;
                decimal Used = DAL.Contract.GetContractAmount(ContractID);
                decimal PlanAmount = decimal.Parse(NB_Amount.Text.Trim());
                decimal Old = 0;
                if (!string.IsNullOrEmpty(sGuid))
                {
                    Entity.TransferPlan plan = DAL.TransferPlan.GetTransferPlan(int.Parse(sGuid));
                    Old = plan.PlanAmount;
                }
                if (Used + PlanAmount-Old > amount)
                {
                    ret += "合同的剩余量不足,合同的剩余量为"+(amount-Used).ToString()+"!";
                }
            }
            
            //判定计划数量之和是不是<合同的数量
            return ret;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string str = checkInfo();
            if (str != "")
            { Alert.ShowInTop(str, MessageBoxIcon.Warning); }
            else
            {
                Entity.TransferPlan entity = new Entity.TransferPlan();
                entity.ContractID = int.Parse(hid_htid.Text);
                //entity.PlanNumber = txt_PlanNumber.Text.Trim();
                entity.WasteCode = hid_WasteCode.Text;
                entity.StartDate = Date_Start.SelectedDate;
                entity.EndDate = Date_End.SelectedDate;
                entity.ApprovalDate = Date_Sign.SelectedDate;
                entity.PlanAmount = decimal.Parse(NB_Amount.Text.Trim());
                entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
                entity.CreateDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.StatusID = 1;
                entity.Remark = txt_Remark.Text.Trim();
                entity.IYear = Date_Sign.SelectedDate.Value.Year;
                Entity.Contract contract=DAL.Contract.GetContract(entity.ContractID);
                string AreaCode=contract.ProduceArea;
                entity.Number = DAL.TransferPlan.GetMaxNumber(AreaCode, entity.IYear)+1;
                entity.PlanNumber = txt_PlanNumber.Text.Trim();
                //entity.PlanNumber = GeneratePlanNumber(AreaCode, entity.IYear, entity.Number.ToString());
                int iReturn = 0;
                if (sGuid == string.Empty || sGuid == null)
                {
                    iReturn = DAL.TransferPlan.AddTransferPlan(entity);
                }
                else
                {

                    entity.PlanID = int.Parse(sGuid);
                    iReturn = DAL.TransferPlan.UpdateTransferPlan(entity);
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
                hid_htid.Text = keys[0].ToString();
                txt_source.Text = keys[1].ToString();
                Entity.Contract con = DAL.Contract.GetContract(int.Parse(keys[0].ToString()));
                txt_Waste.Text = con.WasteName;
                hid_WasteCode.Text = con.WasteCode;
                Window_DepartQuery.Hidden = true;
                Date_End.MaxDate = con.EndDate;
                Date_Start.MinDate = con.StartDate;


                //int Number = DAL.TransferPlan.GetMaxNumber(con.ProduceArea, DateTime.Now.Year) + 1;
                //txt_PlanNumber.Text=GeneratePlanNumber(con.ProduceArea,DateTime.Now.Year,Number.ToString());
            }
        }

        #endregion

        protected void txt_source_TriggerClick(object sender, EventArgs e)
        {
            Window_DepartQuery.Hidden = false;
        }


        private string GeneratePlanNumber(string AreaCode,int Year,string Number)
        {
            StringBuilder sb = new StringBuilder();
            string LetterCode = DAL.Area.GetAreaLetter(AreaCode);
            sb.Append(LetterCode.Trim());
            sb.Append(Year);
            for (int i = 0; i < 4 - Number.Length; i++)
            {
                sb.Append(0);
            }
            sb.Append(Number);
            return sb.ToString();
        }


    }
}