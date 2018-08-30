using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.State
{
    public partial class Monitor : PageBase, ISingleGridPage
    {
        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "废水排放监测表";
                //Master.SetBtnDeleteVisible(false);
                BindDropdownList();
                BindGrid();
                //CheckUserRole();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.MonitorItem> items = DAL.MonitorItem.GetMonitorItemList(1);
            foreach (Entity.MonitorItem item in items)
            {
                FineUI.BoundField bf = new FineUI.BoundField();
                if (string.IsNullOrEmpty(item.Unit))
                {
                    bf.HeaderText = item.ItemName;
                }
                else
                {
                    bf.HeaderText = item.ItemName + "(" + item.Unit + "）";
                }
                //bf.HeaderText = item.ItemName;
                bf.DataField = item.ItemCode;
                bf.SortField = item.ItemCode;
                bf.Width = 120;
                //bf.HeaderText = item.ItemName;

                bf.TextAlign = FineUI.TextAlign.Center;
                GF1.Columns.Add(bf);
            }

        }


        private void BindDropdownList()
        {

            DataTable dt = DAL.User.GetUserNamesEx(1);
            DataRow dr = dt.NewRow();
            dr["UserID"] = "-2";
            dr["RealName"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            drop_Analysis.DataTextField = "RealName";
            drop_Analysis.DataValueField = "UserID";
            drop_Analysis.DataSource = dt;
            drop_Analysis.DataBind();


            DataTable dt2 = DAL.Position.GetAllPositionEx();
            DataRow dr2 = dt2.NewRow();
            dr2["ID"] = "-2";
            dr2["Name"] = "全部";
            dt2.Rows.InsertAt(dr2, 0);
            drop_Position.DataTextField = "Name";
            drop_Position.DataValueField = "ID";
            drop_Position.DataSource = dt2;
            drop_Position.DataBind();
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

            //DataTable table2 = DAL.Analysis.GetAnalysis(txt_BillNumber.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Analysis.SelectedValue.Trim()));
            DataTable table2 = DAL.Monitor.GetMonitorEx(int.Parse(drop_Position.SelectedValue.Trim()), DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Analysis.SelectedValue.Trim()));


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
            Entity.Monitor entity = DAL.Monitor.GetMonitorByID(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (entity.Status == 2)
            {
                Alert.ShowInTop("审核通过的不能删除！", MessageBoxIcon.Information);
                return;
            }
            else
            {
                int BSuccess = DAL.Monitor.DeleteMonitor(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
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
            return "State/Monitor_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("State/Monitor_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
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
            int MonitorID = int.Parse(HttpUtility.UrlEncode(keys[0].ToString()));
            Entity.Monitor entity = DAL.Monitor.GetMonitorByID(MonitorID);
            if (entity.Status == 2)
            {
                Alert.ShowInTop("该记录已经审核通过了！", MessageBoxIcon.Warning);
                return;
            }

            entity.Status = 2;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
            int BSuccess = DAL.Monitor.PassMonitor(entity);
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
                string filename = "废水排放监测表.xls";
                DataTable table2 = DAL.Monitor.GetMonitorEx2(int.Parse(drop_Position.SelectedValue.Trim()), DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Analysis.SelectedValue.Trim()));
                //DAL.NPOIHelper.ExportByWebExForAnalysis(table2, "入厂特性分析表", filename);
                DAL.NPOIHelper.ExportByWebExForWTP(table2, "废水排放监测表", filename);
                //btn_Export.EnableAjax = true;
            }
            catch (Exception ex)
            {

            }
        }


    }
}