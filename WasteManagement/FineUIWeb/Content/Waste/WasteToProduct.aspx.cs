using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WasteManagement.Content.Waste
{
    public partial class WasteToProduct : PageBase, ISingleGridPage
    {
        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "处置为成品表";
                //Master.SetBtnDeleteVisible(false);
                BindDropdownList();
                BindGrid();
                //CheckUserRole();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Entity.Waste> items = DAL.Waste.GetPartWasteEx(2);
            foreach (Entity.Waste item in items)
            {
                FineUI.GroupField gf = new GroupField();
                gf.HeaderText = item.WasteName;
                FineUI.BoundField bf = new FineUI.BoundField();
                if (string.IsNullOrEmpty(item.Unit))
                {
                    bf.HeaderText = "数量";
                }
                else
                {
                    bf.HeaderText = "数量("+item.Unit+"）";
                }
                
                bf.DataField = item.WasteCode;
                //bf.SortField = item.WasteCode;
                bf.Width = 70;
                //bf.HeaderText = item.ItemName;

                bf.TextAlign = FineUI.TextAlign.Center;

                FineUI.BoundField bf2 = new FineUI.BoundField();
                bf2.HeaderText = "罐池号";
                bf2.DataField = item.WasteCode+"Pond";
                //bf.SortField = item.WasteCode;
                bf2.Width = 70;
                //bf.HeaderText = item.ItemName;

                bf.TextAlign = FineUI.TextAlign.Center;
                bf2.TextAlign = FineUI.TextAlign.Center;
                gf.Columns.Add(bf);
                gf.Columns.Add(bf2);
                GF1.Columns.Add(gf);
            }

            //List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
            //foreach (Entity.Waste item in itemss)
            //{
            //    FineUI.BoundField bf = new FineUI.BoundField();
            //    bf.HeaderText = item.WasteName;
            //    bf.DataField = item.WasteCode;
            //    //bf.SortField = item.WasteCode;
            //    bf.Width = 50;
            //    //bf.HeaderText = item.ItemName;

            //    bf.TextAlign = FineUI.TextAlign.Center;
            //    GF2.Columns.Add(bf);
            //}

        }


        private void BindDropdownList()
        {

            DataTable dt = DAL.User.GetUserNamesEx(4);
            DataRow dr = dt.NewRow();
            dr["UserID"] = "-2";
            dr["RealName"] = "全部";
            dt.Rows.InsertAt(dr, 0);
            drop_Handle.DataTextField = "RealName";
            drop_Handle.DataValueField = "UserID";
            drop_Handle.DataSource = dt;
            drop_Handle.DataBind();
        }


        //protected void Grid1_Sort(object sender, GridSortEventArgs e)
        //{
        //    BindGrid();
        //}

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

            DataTable table2 = DAL.WasteToProduct.QueryWasteToProduct(txt_PondName.Text.Trim(), txt_WasteName.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Handle.SelectedValue.Trim()));

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
            Entity.WasteToProduct entity = DAL.WasteToProduct.GetWasteToProduct(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
            if (entity.Status == 2)
            {
                Alert.ShowInTop("审核通过的不能删除！", MessageBoxIcon.Information);
                return;
            }
            else
            {
                int BSuccess = DAL.WasteToProduct.DeleteWasteToProduct(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
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
            return "Waste/WasteToProduct_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("Waste/WasteToProduct_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
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
            int DealID = int.Parse(HttpUtility.UrlEncode(keys[0].ToString()));
            Entity.WasteToProduct entity = DAL.WasteToProduct.GetWasteToProduct(DealID);
            if (entity.Status == 2)
            {
                Alert.ShowInTop("该记录已经审核通过了！", MessageBoxIcon.Warning);
                return;
            }

            entity.CreateDate = DateTime.Now;
            entity.CreateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = Request.Cookies["Cookies"].Values["UserName"].ToString();
            entity.Status = 2;
            //先判断是否可行
            int PondID = entity.FromPondID;
            Entity.Pond pond2 = DAL.Pond.GetPond(PondID);
            //decimal Used = DAL.PondUsed.GetPondUsedAmount(PondID);
            decimal Used = pond2.Used;
            decimal Now = Used - entity.FromAmount;
            if (Now<0)
            {
                string message = "出库罐池" + pond2.Name + "库存不足！";
                Alert.ShowInTop(message, MessageBoxIcon.Warning);
                return;       
            }
            List<Entity.ProductDetail> lists = DAL.ProductDetail.GetProductDetailEx(DealID);
            bool IsOK = true;
            foreach (Entity.ProductDetail list in lists)
            {
                int PondID2 = list.PondID;
                Entity.Pond pond=DAL.Pond.GetPond(PondID2);
                decimal Capacity = pond.Capacity;
                //decimal Used2 = DAL.PondUsed.GetPondUsedAmount(PondID2);
                decimal Used2 = pond.Used;
                decimal Now2 = Used2 + list.Amount;
                if (Capacity < Now2)
                {
                    string message = "入库罐池"+pond.Name + "余量不足！";
                    IsOK = false;
                    Alert.ShowInTop(message, MessageBoxIcon.Warning);
                    return;                   
                }
                list.Amount = Now2;
            }
            if (IsOK == false)
            {
                return;
            }
            int BSuccess = DAL.WasteToProduct.PassWasteToProduct(entity,Now,lists);
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
                string filename = "废酸转成品表.xls";
                DataTable table2 = DAL.WasteToProduct.QueryWasteToProductEx(txt_PondName.Text.Trim(), txt_WasteName.Text.Trim(), DateStart.Text.Trim(), DateEnd.Text.Trim(), int.Parse(drop_Handle.SelectedValue.Trim()));
                //DAL.NPOIHelper.ExportByWebExForAnalysis(table2, "入厂特性分析表", filename);
                DAL.NPOIHelper.ExportByWebExForWTP(table2, "废酸转成品表", filename);
                //btn_Export.EnableAjax = true;
            }
            catch (Exception ex)
            {

            }
        }

    }
}