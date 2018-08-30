using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Report
{
    public partial class Storage : PageBase, ISingleGridPage
    {
        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "入库统计";
                Master.SetBtnDeleteVisible(false);
                Master.SetBtnEditVisible(false); 
                Master.SetBtnInsertVisible(false);
                BindGrid();
            }
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
            if (drop_Type.SelectedIndex==0&&cb_Enterprise.Checked)
            {
                Grid1.Hidden = true;
                Grid2.Hidden = false;
                Grid2.RecordCount = RowNum;

                // 3.绑定到Grid
                Grid2.DataSource = table;
                Grid2.DataBind();
            }
            else
            {
                Grid1.Hidden = false;
                Grid2.Hidden = true;
                Grid1.RecordCount = RowNum;

                // 3.绑定到Grid
                Grid1.DataSource = table;
                Grid1.DataBind();
            }

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
            if (drop_Type.SelectedIndex == 0 && cb_Enterprise.Checked == true)
            {
                //废物,并按企业分类
                int pageIndex = Grid2.PageIndex;
                int pageSize = Grid2.PageSize;

                string sortField = Grid2.SortField;
                string sortDirection = Grid2.SortDirection;

                //查询数据
                string selectStr = string.Empty;

                DataTable table2 = DAL.WasteStorage.GetSum(DateStart.SelectedDate.ToString(), DateEnd.SelectedDate.ToString(), 1);
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
            else
            {
                //成品和废物
                int pageIndex = Grid1.PageIndex;
                int pageSize = Grid1.PageSize;

                string sortField = Grid1.SortField;
                string sortDirection = Grid1.SortDirection;

                //查询数据
                string selectStr = string.Empty;
                DataTable table2 = new DataTable();
                if (drop_Type.SelectedIndex==0)
                {
                    table2 = DAL.WasteStorage.GetSum(DateStart.SelectedDate.ToString(), DateEnd.SelectedDate.ToString(), 0);
                }
                else
                {
                    table2 = DAL.ProductDetail.GetSum(DateStart.SelectedDate.ToString(), DateEnd.SelectedDate.ToString());
                }
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

        }

        #endregion

        #region ISingleGridPage

        /// <summary>
        /// [ISingleGridPage]删除表格数据
        /// </summary>
        public void DeleteSelectedRows()
        {

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


        protected void drop_TypeChanged(object sender, EventArgs e)
        {
            if (drop_Type.SelectedIndex == 0)
            {
                cb_Enterprise.Hidden = false;
            }
            else
            {
                cb_Enterprise.Hidden = true;
            }
        }


        protected void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {

                if (drop_Type.SelectedIndex == 0)
                {

                    //string filename = "废酸入库明细表.xls";
                    //DataTable table2 = DAL.WasteStorage.QueryWasteStorageEx2(DateStart.Text.Trim(), DateEnd.Text.Trim());
                    //DAL.NPOIHelper.ExportByWebEx(table2, "废酸入库明细表", filename);
                    string Start = DateStart.Text.Trim();
                    string End = DateEnd.Text.Trim();
                    string filename = Start + "到" + End + "废物入库统计表.xls";
                    DataTable table2 = new DataTable();
                    if (cb_Enterprise.Checked)
                    {
                        table2 = DAL.WasteStorage.GetSumEx(DateStart.Text.Trim(), DateEnd.Text.Trim(),1);
                    }
                    else
                    {
                        table2 = DAL.WasteStorage.GetSumEx(DateStart.Text.Trim(), DateEnd.Text.Trim(),0);
                    }
                    string name = Start + "到" + End + "废物入库统计表";
                    DAL.NPOIHelper.ExportByWebEx(table2, name, filename);
                    //DAL.NPOIHelper.ExportByWebEx(table2, "废物入库统计表", filename);
                }
                else
                {

                    //string filename = "成品入库明细表.xls";
                    //DataTable table2 = DAL.ProductDetail.QueryProductDetail(DateStart.Text.Trim(), DateEnd.Text.Trim());
                    //DAL.NPOIHelper.ExportByWebEx(table2, "成品入库明细表", filename);        
                    string Start = DateStart.Text.Trim();
                    string End = DateEnd.Text.Trim();
                    string filename = Start + "到" + End + "成品入库统计表.xls";
                    DataTable table2 = DAL.ProductDetail.GetSumEx(DateStart.Text.Trim(), DateEnd.Text.Trim());
                    string name = Start + "到" + End + "成品入库统计表";
                    DAL.NPOIHelper.ExportByWebEx(table2, name, filename);

                    //DAL.NPOIHelper.ExportByWebEx(table2, "成品入库统计表", filename);
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}