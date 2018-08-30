using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Basic
{
    public partial class Area : PageBase, ISingleGridPage
    {

        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "区域信息";
                BindGrid();
                CheckUserRole();
            }
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }

        private void CheckUserRole()
        {
            string pageName = this.Request.Url.Segments[this.Request.Url.Segments.Length - 1].ToString();
            //if (!blPageWrite(pageName, Request.Cookies["Cookies"].Values["UserGuid"].ToString()))
            //{
            //    beWrite = false;
            //}
            //else
            //{
            //    beWrite = true;
            //}
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

            DataTable table2 = DAL.Area.QueryArea(ser_vDirectiveNumber.Text.Trim(), ser_vSaveNumber.Text.Trim());
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
            //if (!beWrite) return;

            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            int BSuccess = DAL.Area.DeleteArea(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (BSuccess == 1)
            {
                Alert.ShowInTop(" 删除成功！", MessageBoxIcon.Information);
                BindGrid();
            }
            else
            {
                Alert.ShowInTop(" 删除失败！", MessageBoxIcon.Warning);
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
            return "Basic/Area_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("Basic/Area_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
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


        protected void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "行政区域.xls";
                DataTable table2 = DAL.Area.QueryAreaEx(ser_vDirectiveNumber.Text.Trim(), ser_vSaveNumber.Text.Trim());
                DAL.NPOIHelper.ExportByWebEx(table2, "行政区域表", filename);
                //btn_Export.EnableAjax = true;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
