using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using System.IO;

namespace WasteManagement.Content.Basic
{
    //public partial class Driver : System.Web.UI.Page
    //{
    public partial class Driver : PageBase, ISingleGridPage
    {

        private int RowNum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnSelectColumns.OnClientClick = Window2.GetShowReference("../grid/grid_excel_selectcolumns_iframe_window.aspx");

                if (Request.Cookies["Cookies"] == null) Response.Redirect("../../Login.aspx");

                (Master.FindControl("Panel1") as FineUI.Panel).BodyPadding = "5px";
                (Master.FindControl("Panel1") as FineUI.Panel).Title = "驾驶员信息";
                BindGrid();
                CheckUserRole();
            }
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

            DataTable table2 = DAL.Driver.QueryDriver(ser_vSaveNumber.Text.Trim());
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
            int BSuccess = DAL.Driver.DeleteDriver(int.Parse(HttpUtility.UrlEncode(keys[0].ToString())));
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
            return "Basic/Driver_Window.aspx";
        }

        /// <summary>
        /// [ISingleGridPage]获取编辑地址
        /// </summary>
        /// <returns></returns>
        public string GetEditUrl()
        {
            //if (!beWrite) return "";
            object[] keys = Grid1.DataKeys[Grid1.SelectedRowIndex];
            return String.Format("Basic/Driver_Window.aspx?id={0}", HttpUtility.UrlEncode(keys[0].ToString()));
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

        #region 导出表格
        //protected void btn_Export_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        string filename = "test2.xls";
        //        Response.ContentType = "application/ms-excel";
        //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        //        Response.Clear();
        //        DataTable table2 = DAL.Driver.QueryDriver(ser_vSaveNumber.Text.Trim());
        //        DAL.NPOIHelper.Export(table2, "驾驶员表").WriteTo(Response.OutputStream);
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        //Response.End();
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }

        //}


        //protected void btn_Export2_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        //string filename = "test22.xls";

        //        //Response.Clear();
        //        //Response.ContentType = "application/vnd.ms-excel";
        //        //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        //        //Response.Buffer = true;

        //        //Response.Charset = "GB2312";
        //        //Response.ContentEncoding = System.Text.Encoding.UTF8;


        //        string filename = "test.xls";
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        //        Response.Clear();

        //        InitializeWorkbook();
        //        GenerateData();
        //        GetExcelStream().WriteTo(Response.OutputStream);
        //        Response.End();



        //        //InitializeWorkbook();
        //        //GenerateData();
        //        ////Response.Output.Write(oStringWriter.ToString());//Output the stream.
        //        ////Response.Flush();
        //        ////Response.End();

        //        //GetExcelStream().WriteTo(Response.OutputStream);
        //        //HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        ////Response.End();
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }
        //}

        //HSSFWorkbook hssfworkbook;
        //MemoryStream GetExcelStream()
        //{
        //    //Write the stream data of workbook to the root directory
        //    MemoryStream file = new MemoryStream();
        //    hssfworkbook.Write(file);
        //    return file;
        //}

        //void GenerateData()
        //{
        //    ISheet sheet1 = hssfworkbook.CreateSheet("Sheet1");

        //    sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
        //    int x = 1;
        //    for (int i = 1; i <= 15; i++)
        //    {
        //        IRow row = sheet1.CreateRow(i);
        //        for (int j = 0; j < 15; j++)
        //        {
        //            row.CreateCell(j).SetCellValue(x++);
        //        }
        //    }
        //}

        //void InitializeWorkbook()
        //{
        //    hssfworkbook = new HSSFWorkbook();

        //    ////create a entry of DocumentSummaryInformation
        //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        //    dsi.Company = "NPOI Team";
        //    hssfworkbook.DocumentSummaryInformation = dsi;

        //    ////create a entry of SummaryInformation
        //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        //    si.Subject = "NPOI SDK Example";
        //    hssfworkbook.SummaryInformation = si;
        //}

        //protected void btn_Export3_Click(object sender, EventArgs e)
        //{
        //    Export();
        //}
        //public void Export()
        //{
        //    string filename = "111";

        //    Response.Clear();
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename + ".xlsx"));

        //    //NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
        //    //NPOI.SS.UserModel.ISheet sheet1 = workbook.CreateSheet("BOM详情");
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    //HSSFSheet sheet1 = workbook.CreateSheet("BOM详情");
        //    ////给sheet1添加第一行的头部标题
        //    ////HSSFRow
        //    //NPOI.HSSF.UserModel.HSSFRow row1 = sheet1.CreateRow(0);
        //    //row1.CreateCell(0).SetCellValue("物料编码");
        //    //row1.CreateCell(1).SetCellValue("物料名称");
        //    //row1.CreateCell(2).SetCellValue("规格型号");
        //    //row1.CreateCell(3).SetCellValue("物料用量");
        //    //row1.CreateCell(4).SetCellValue("用量单位");
        //    //row1.CreateCell(5).SetCellValue("备注");
        //    //将数据逐步写入sheet1各个行
        //    //List<AkBom> pageResult = _akBomRepository.GetPageList(0, 10000, Request["searchString"], "");
        //    //for (int i = 0; i < pageResult.Count; i++)
        //    //{
        //    //    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
        //    //    rowtemp.CreateCell(0).SetCellValue(pageResult[i].ChildNumber);
        //    //    rowtemp.CreateCell(1).SetCellValue(pageResult[i].ChildName);
        //    //    rowtemp.CreateCell(2).SetCellValue(pageResult[i].Spec);
        //    //    rowtemp.CreateCell(3).SetCellValue(double.Parse(pageResult[i].MaterialSum.ToString()));
        //    //    rowtemp.CreateCell(4).SetCellValue(pageResult[i].Unit);
        //    //    rowtemp.CreateCell(5).SetCellValue(pageResult[i].Remark);
        //    //}
        //    //写入到客户端 
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    workbook.Write(ms);
        //    Response.BinaryWrite(ms.ToArray());

        //    Response.Flush();
        //    Response.End();
        //}

        //protected void btn_Export4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string filename = "test2.xls";
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename));
        //        Response.AddHeader("Content-Transfer-Encoding", "binary");
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

        //        //Response.ContentType = "application/ms-excel";
        //        //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        //        Response.Clear();
        //        MemoryStream file = new MemoryStream();
        //        HSSFWorkbook workbook = new HSSFWorkbook();
        //        //HSSFSheet sheet1 = workbook.CreateSheet("BOM详情");
        //        ////给sheet1添加第一行的头部标题
        //        //NPOI.HSSF.UserModel.HSSFRow row1 = sheet1.CreateRow(0);
        //        //row1.CreateCell(0).SetCellValue("物料编码");
        //        //row1.CreateCell(1).SetCellValue("物料名称");
        //        //row1.CreateCell(2).SetCellValue("规格型号");
        //        //row1.CreateCell(3).SetCellValue("物料用量");
        //        //row1.CreateCell(4).SetCellValue("用量单位");
        //        //row1.CreateCell(5).SetCellValue("备注");
        //        //workbook.Write(file);
        //        //Response.BinaryWrite(file.GetBuffer());
        //        //Response.End();

        //        //DataTable table2 = DAL.Driver.QueryDriver(ser_vSaveNumber.Text.Trim());
        //        //DAL.NPOIHelper.Export(table2, "驾驶员表").WriteTo(Response.OutputStream);
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        //Response.End();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = "驾驶员表.xls";
                //Response.ContentType = "application/ms-excel";
                //Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
                //Response.Clear();
                DataTable table2 = DAL.Driver.QueryDriverEx(ser_vSaveNumber.Text.Trim());
                DAL.NPOIHelper.ExportByWebEx(table2, "驾驶员表", filename);
                //DAL.NPOIHelper.Export(table2, "驾驶员表").WriteTo(Response.OutputStream);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            catch (Exception ex)
            {

            }
        }


        //protected void btn_Export6_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable table = DAL.Driver.QueryDriver(ser_vSaveNumber.Text.Trim());
        //        string fileName = string.Empty;
        //        fileName =  "Test.xls";
        //        DAL.MyxlsHelper.ExportExcelForPercentForWeb("11", fileName, table);
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }
        //}


        //protected void btn_Export7_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable table = DAL.Driver.QueryDriver(ser_vSaveNumber.Text.Trim());
        //        string fileName = string.Empty;
        //        fileName = "Test.xls";
        //        DAL.MyxlsHelper.ExportExcelForPercent("11", fileName, table);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        #endregion
    }
}
