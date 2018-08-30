using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Waste
{
    public partial class FinalWaste : PageBase, ISingleGridPage
    {
        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "新危废产生记录表";
                //Master.SetBtnDeleteVisible(false);
                BindDropdownList();
                BindGrid();
                //CheckUserRole();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
            foreach (Entity.Waste item in itemss)
            {
                FineUI.BoundField bf = new FineUI.BoundField();
                if (string.IsNullOrEmpty(item.Unit))
                {
                    bf.HeaderText = item.WasteName;
                }
                else
                {
                    bf.HeaderText = item.WasteName + "(" + item.Unit + "）";
                }
                //bf.HeaderText = item.WasteName;
                bf.DataField = item.WasteCode;
                //bf.SortField = item.WasteCode;
                bf.Width = 120;
                //bf.HeaderText = item.ItemName;

                bf.TextAlign = FineUI.TextAlign.Center;
                GF2.Columns.Add(bf);
            }

        }


        private void BindDropdownList()
        {
            //处置人+分析人
            DataTable dt = DAL.User.GetUserNamesEx2("1,4");
            DataRow dr = dt.NewRow();
            dr["UserID"] = "-2";
            dr["RealName"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            drop_Handle.DataTextField = "RealName";
            drop_Handle.DataValueField = "UserID";
            drop_Handle.DataSource = dt;
            drop_Handle.DataBind();

            DataTable dt2 = DAL.Status.GetAllStatus();
            DataRow dr2 = dt2.NewRow();
            dr2["Code"] = "-2";
            dr2["Description"] = "全部";
            dt2.Rows.InsertAt(dr2, 0);
            drop_Status.DataTextField = "Description";
            drop_Status.DataValueField = "Code";
            drop_Status.DataSource = dt2;
            drop_Status.DataBind();
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

            DataTable table2 = DAL.FinalWasteLog.QueryFinalWasteLog(DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Handle.SelectedValue.Trim()),int.Parse(drop_Status.SelectedValue.Trim()));

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
            Entity.FinalWasteLog entity = DAL.FinalWasteLog.GetFinalWasteLog(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (entity.Status == 2)
            {
                Alert.ShowInTop("审核通过的不能删除！", MessageBoxIcon.Information);
                return;
            }
            else
            {
                int BSuccess = DAL.FinalWasteLog.DeleteFinalWasteLog(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
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
            return "Waste/FinalWaste_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("Waste/FinalWaste_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
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
            int LogID = int.Parse(HttpUtility.UrlEncode(keys[0].ToString()));
            Entity.FinalWasteLog entity = DAL.FinalWasteLog.GetFinalWasteLog(LogID);
            if (entity.Status == 2)
            {
                Alert.ShowInTop("该记录已经审核通过了！", MessageBoxIcon.Warning);
                return;
            }

            //entity.CreateDate = DateTime.Now;
            //entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
            entity.Status = 2;
            //先判断是否可行

            int BSuccess = DAL.FinalWasteLog.UpdateFinalWasteLogEx(entity);
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
                string filename = "新危废记录表.xls";
                DataTable table2 = DAL.FinalWasteLog.QueryFinalWasteLogEx(DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Handle.SelectedValue.Trim()), int.Parse(drop_Status.SelectedValue.Trim()));
                //DAL.NPOIHelper.ExportByWebExForAnalysis(table2, "入厂特性分析表", filename);
                DAL.NPOIHelper.ExportByWebExForWTP(table2, "新危废记录表", filename);
                //btn_Export.EnableAjax = true;
            }
            catch (Exception ex)
            {

            }
        }

    }
}