using System;
using System.Collections.Generic;
using System.Text;
using MyXls;
using System.Data;
using org.in2bits.MyXls;
using System.IO;
using System.Web;


namespace DAL
{
    public static class MyxlsHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        public static void ExportExcelForPercent(string sheetName, string xlsname, DataTable table)
        {

            //DataTable table = GetDataTableForPercent(areaid, dt);

            if (table == null || table.Rows.Count == 0) { return; }
            XlsDocument xls = new XlsDocument();
            Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);

            //填充表头  
            foreach (DataColumn col in table.Columns)
            {
                sheet.Cells.Add(1, col.Ordinal + 1, col.ColumnName);
            }

            //填充内容  
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    sheet.Cells.Add(i + 2, j + 1, table.Rows[i][j].ToString());
                }
            }

            //保存  
            xls.FileName = xlsname;
            xls.Save();
            xls = null;
        }



        /// <summary>
        /// Myxls导出Excel
        /// </summary>
        public static void ExportExcelForPercentForWeb(string sheetName, string xlsname, DataTable table)
        {

            XlsDocument xls = new XlsDocument();
            Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);
            try
            {
                //DataTable table = GetDataTableForPercent(areaid, curdate);

                if (table == null || table.Rows.Count == 0) { return; }
                //XlsDocument xls = new XlsDocument();
                //Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);

                //填充表头  
                foreach (DataColumn col in table.Columns)
                {
                    sheet.Cells.Add(1, col.Ordinal + 1, col.ColumnName);
                }

                //填充内容  
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        sheet.Cells.Add(i + 2, j + 1, table.Rows[i][j].ToString());
                    }
                }

                //保存  
                //xls.FileName = xlsname;
                //xls.Save();

                #region 客户端保存
                using (MemoryStream ms = new MemoryStream())
                {
                    xls.Save(ms);
                    ms.Flush();
                    ms.Position = 0;
                    sheet = null;
                    xls = null;
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();

                    response.Charset = "UTF-8";
                    response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + xlsname));
                    //System.Web.HttpContext.Current.Response.WriteFile(fi.FullName);
                    byte[] data = ms.ToArray();
                    System.Web.HttpContext.Current.Response.BinaryWrite(data);

                }

                #endregion
                //xls = null;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sheet = null;
                xls = null;
            }

        }

    }
}
