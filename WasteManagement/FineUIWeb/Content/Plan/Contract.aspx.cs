using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Plan
{
    public partial class Contract : PageBase, ISingleGridPage
    {
        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "合同登记";
                //Master.SetBtnDeleteVisible(false);
                BindDropdownList();
                BindGrid();
                //CheckUserRole();
            }
        }


        private void BindDropdownList()
        {

            DataTable dt = DAL.Status.GetAllStatus();
            DataRow dr = dt.NewRow();
            dr["Code"] = "-2";
            dr["Description"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            drop_Status.DataTextField = "Description";
            drop_Status.DataValueField = "Code";
            drop_Status.DataSource = dt;
            drop_Status.DataBind();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }



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

            DataTable table2 = DAL.Contract.QueryContract(txt_ContractNumber.Text.Trim(), txt_Name.Text.Trim(),DateStart.Text.Trim(),DateEnd.Text.Trim(), txt_WasteName.Text.Trim(),int.Parse(drop_Status.SelectedValue.Trim()));

            RowNum = table2.Rows.Count;

            DataView view2 = table2.DefaultView;
            if (table2.Rows.Count > 0)
            {
                view2.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }
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

        #region ISingleGridPage

        /// <summary>
        /// [ISingleGridPage]删除表格数据
        /// </summary>
        public void DeleteSelectedRows()
        {            
            int selectedCount = Grid1.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请至少选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            Entity.Contract entity = DAL.Contract.GetContract(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (entity.StatusID == 2)
            {
                Alert.ShowInTop("审核通过的不能删除！", MessageBoxIcon.Information);
                return;
            }
            else
            {
                int BSuccess = DAL.Contract.DeleteContract(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
                if (BSuccess == 1)
                {
                    Alert.ShowInTop(" 操作成功！", MessageBoxIcon.Information);
                    BindGrid();
                }
                else
                {
                    Alert.ShowInTop(" 操作失败！", MessageBoxIcon.Warning);
                }
            }
            

        }

        /// <summary>
        /// [ISingleGridPage]主表格实例
        /// </summary>
        public Grid Grid
        {
            get
            {
                return Grid1;
            }
        }

        /// <summary>
        /// [ISingleGridPage]获取新增地址
        /// </summary>
        /// <returns></returns>
        public string GetNewUrl()
        {
            //if (!beWrite) return "";
            return "Plan/Contract_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("Plan/Contract_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
        }

        #endregion


        #region Events
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion


        protected void btn_Pass_Click(object sender, EventArgs e)
        {
            int selectedCount = Grid1.SelectedRowIndexArray.Length;
            if (selectedCount == 0)
            {
                Alert.ShowInTop("请至少选择一项纪录！", MessageBoxIcon.Warning);
                return;
            }
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            int ContractID = int.Parse(HttpUtility.UrlEncode(keys[0].ToString()));
            //Entity.Contract entity = new Entity.Contract();
            //entity.ContractID = int.Parse(HttpUtility.UrlEncode(keys[0].ToString()));
            Entity.Contract entity = DAL.Contract.GetContract(ContractID);
            if (entity.StatusID == 2)
            {
                Alert.ShowInTop("该记录已经审核通过了！", MessageBoxIcon.Warning);
                return;
            }
            entity.StatusID = 2;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString(); 
            int BSuccess = DAL.Contract.UpdateContractStatus(entity);
            if (BSuccess == 1)
            {
                Alert.ShowInTop(" 操作成功！", MessageBoxIcon.Information);
                BindGrid();
            }
            else
            {
                Alert.ShowInTop(" 操作失败！", MessageBoxIcon.Warning);
            }
        }


        protected void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "合同表.xls";
                //Response.ContentType = "application/ms-excel";
                //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                //Response.Clear();
                DataTable table2 = DAL.Contract.QueryContractEx(txt_ContractNumber.Text.Trim(), txt_Name.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), txt_WasteName.Text.Trim(), int.Parse(drop_Status.SelectedValue.Trim()));
                DAL.NPOIHelper.ExportByWebEx(table2, "合同表", filename);
                //DAL.NPOIHelper.Export(table2, "驾驶员表").WriteTo(Response.OutputStream);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
}