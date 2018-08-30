using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class ProductDetail
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllProductDetail()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [ProductDetail]", null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return dt;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="WTPID">    </param>
        /// <returns></returns>
        public static List<Entity.ProductDetail> GetProductDetailEx(int WTPID)
        {
            List<Entity.ProductDetail> list = new List<Entity.ProductDetail>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vProductDetail] where WTPID='" + WTPID + "'", null);
                while (dataReader.Read())
                {
                    Entity.ProductDetail entity = new Entity.ProductDetail();
                    entity.DetailID = DataHelper.ParseToInt(dataReader["DetailID"].ToString());
                    entity.WTPID = DataHelper.ParseToInt(dataReader["WTPID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
                    entity.Amount = decimal.Parse(dataReader["Amount"].ToString());
                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="WTPID">    </param>
        /// <returns></returns>
        public static DataTable GetProductDetail(int WTPID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vProductDetail] where WTPID='" + WTPID + "'", null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return dt;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="WTPID">    </param>
        /// <returns></returns>
        public static DataTable QueryProductDetail(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select DateTime 日期,WasteName as 废物名称,Name as 罐池号,Amount as 数量,HandleManName as 处置人,ReceiverName as 签收人,StatusName as 状态 from [vProductDetail] where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" order by DateTime desc");
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return dt;
        }




        /// <summary>
        ///
        /// </summary>
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static List<Entity.ProductDetail> GetSumProductDetail()
        {
            List<Entity.ProductDetail> list = new List<Entity.ProductDetail>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductDetail where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    Entity.ProductDetail entity = new Entity.ProductDetail();
                    entity.Name = dataReader["WasteName"].ToString();
                    entity.Amount = decimal.Parse(dataReader["Total"].ToString());
                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return list;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="PlanID">    </param>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetSum(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select sum(Amount) as Total,WasteName from vProductDetail where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by WasteName");
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return dt;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="PlanID">    </param>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetSumEx(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select WasteName 产品种类,sum(Amount) as 合计 from vProductDetail where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by WasteName");
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return dt;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumProductDetail(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductDetail where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' and WasteName='" + WasteName + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    sum = decimal.Parse(dataReader["Total"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return sum;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumProductDetailEx(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductDetail where Status=2 and DateTime='" + DateTime.Now.Date + "' and WasteName='" + WasteName + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    sum = decimal.Parse(dataReader["Total"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return sum;
        }


        public static int AddProductDetail(Entity.ProductDetail entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@WTPID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WTPID.GetType().ToString()),entity.WTPID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductDetail_Add", prams);
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
                iReturn = 0;

            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

        public static int UpdateProductDetail(Entity.ProductDetail entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@DetailID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DetailID.GetType().ToString()),entity.DetailID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductDetail_Update", prams);
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
                iReturn = 0;

            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DetailID">    </param>
        /// <returns></returns>
        public static int DeleteProductDetail(int DetailID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [ProductDetail] where DetailID='" + DetailID + "'", null);
                thelper.CommitTransaction(trans);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                thelper.RollTransaction(trans);
                iReturn = 0;

            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }

    }
}
