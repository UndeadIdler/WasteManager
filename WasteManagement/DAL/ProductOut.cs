using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class ProductOut
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="PondID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="ReceiverEnterpriseID">    </param>
        /// <param name="DriverID">    </param>
        /// <param name="ConsignorID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetAllProductOut(string PondName, string WasteName, string StartTime, string EndTime, string ReceiverEnterpriseName, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vProductOut] where 1=1 ");
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (!string.IsNullOrEmpty(ReceiverEnterpriseName))
                {
                    sb.Append(" and Name like '%" + ReceiverEnterpriseName + "%'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
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
        /// <param name="PondID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="ReceiverEnterpriseID">    </param>
        /// <param name="DriverID">    </param>
        /// <param name="ConsignorID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetAllProductOutEx(string PondName, string WasteName, string StartTime, string EndTime, string ReceiverEnterpriseName, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select DateTime as 日期,PondName as 罐池号,WasteName as 成品名称,Amount as 数量,Name as 收货单位,CarNumber 车牌号,RealName as 驾驶员,UserRealName as 发货人,StatusName as 状态 from [vProductOut] where 1=1 ");
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (!string.IsNullOrEmpty(ReceiverEnterpriseName))
                {
                    sb.Append(" and Name like '%" + ReceiverEnterpriseName + "%'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
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



        public static DataTable QueryProductOutEx2(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select DateTime 日期,PondName as 罐池号,WasteName as 废物名称,Amount as 数量,Name 收货单位,CarNumber 车牌号,RealName as 驾驶员,UserRealName as 发货人 from [vProductOut] where Status=2");
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
        /// <param name="OutID">    </param>
        /// <returns></returns>
        public static Entity.ProductOut GetProductOut(int OutID)
        {
            Entity.ProductOut entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vProductOut] where OutID='" + OutID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.ProductOut();
                    entity.OutID = DataHelper.ParseToInt(dataReader["OutID"].ToString());
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.WasteCode = dataReader["Stores"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    entity.Amount = decimal.Parse(dataReader["Amount"].ToString());
                    entity.ReceiverEnterpriseID = DataHelper.ParseToInt(dataReader["ReceiverEnterpriseID"].ToString());
                    entity.EnterpriseName = dataReader["Name"].ToString();
                    entity.DriverID = DataHelper.ParseToInt(dataReader["DriverID"].ToString());
                    entity.DriverName = dataReader["RealName"].ToString();
                    entity.CarID = DataHelper.ParseToInt(dataReader["CarID"].ToString());
                    entity.CarNumber = dataReader["CarNumber"].ToString();
                    entity.ConsignorID = DataHelper.ParseToInt(dataReader["ConsignorID"].ToString());
                    entity.ConsignorName = dataReader["UserRealName"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return entity;
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
        public static DataTable GetSum(string StartTime, string EndTime,int IsEnterprise)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select sum(Amount) as Total,WasteName ");
                if (IsEnterprise == 1)
                {
                    sb.Append(",Name");
                }
                sb.Append(" from vProductOut where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (IsEnterprise == 1)
                {
                    sb.Append(" group by Name,WasteName");
                    sb.Append(" order by Name");
                }
                else
                {
                    sb.Append(" group by WasteName");
                }

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
        public static DataTable GetSumEx(string StartTime, string EndTime, int IsEnterprise)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ");
                if (IsEnterprise == 1)
                {
                    sb.Append("Name 收货单位,");
                }
                sb.Append("WasteName 成品名称,sum(Amount) as 合计");
                sb.Append(" from vProductOut where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (IsEnterprise == 1)
                {
                    sb.Append(" group by Name,WasteName");
                    sb.Append(" order by 收货单位");
                }
                else
                {
                    sb.Append(" group by WasteName");
                }

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
        public static List<Entity.ProductOut> GetSumProductOut()
        {
            List<Entity.ProductOut> list = new List<Entity.ProductOut>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductOut where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    Entity.ProductOut entity = new Entity.ProductOut();
                    entity.WasteName = dataReader["WasteName"].ToString();
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
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumProductOut(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductOut where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' and WasteName='" + WasteName + "' group by WasteName", null);
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
        public static decimal GetPartSumProductOutEx(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vProductOut where Status=2 and DateTime='" + DateTime.Now.Date + "' and WasteName='" + WasteName + "' group by WasteName", null);
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


        public static int AddProductOut(Entity.ProductOut entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@ReceiverEnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverEnterpriseID.GetType().ToString()),entity.ReceiverEnterpriseID,32),
					dbFactory.MakeInParam("@DriverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DriverID.GetType().ToString()),entity.DriverID,32),
					dbFactory.MakeInParam("@ConsignorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ConsignorID.GetType().ToString()),entity.ConsignorID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CarID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarID.GetType().ToString()),entity.CarID,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductOut_Add", prams);
                iReturn = int.Parse(prams[13].Value.ToString());
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


        public static int UpdateProductOut(Entity.ProductOut entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@OutID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OutID.GetType().ToString()),entity.OutID,32),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@ReceiverEnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverEnterpriseID.GetType().ToString()),entity.ReceiverEnterpriseID,32),
					dbFactory.MakeInParam("@DriverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DriverID.GetType().ToString()),entity.DriverID,32),
					dbFactory.MakeInParam("@ConsignorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ConsignorID.GetType().ToString()),entity.ConsignorID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CarID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarID.GetType().ToString()),entity.CarID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductOut_Update", prams);
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
        /// <param name="OutID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int UpdateProductOutStatus(int OutID, int Status)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@OutID",	DBTypeConverter.ConvertCsTypeToOriginDBType(OutID.GetType().ToString()),OutID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(Status.GetType().ToString()),Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductOut_UpdateStatus", prams);
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
        /// <param name="OutID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int PassProductOut(Entity.ProductOut entity,decimal Used)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@OutID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OutID.GetType().ToString()),entity.OutID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductOut_UpdateStatus", prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.PondID + "','" + Used + "','4','成品出库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + Used + "'where PondID='" + entity.PondID + "'", null);
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
        /// <param name="OutID">    </param>
        /// <returns></returns>
        public static int DeleteProductOut(int OutID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [ProductOut] where Status<>2 and OutID='" + OutID + "'", null);
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
