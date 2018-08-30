using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class FinalWasteLog
    {
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="FromPondID">    </param>
        ///// <param name="FromWasteCode">    </param>
        ///// <param name="Status">    </param>
        ///// <returns></returns>
        //public static DataTable GetAllFinalWasteLog(int FromPondID, string FromWasteCode, int Status)
        //{
        //    DataTable dt = new DataTable();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [FinalWasteLog] where FromPondID='" + FromPondID + "' and FromWasteCode='" + FromWasteCode + "' and Status='" + Status + "'", null);
        //        dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return dt;
        //}


        /// <summary>
        ///
        /// </summary>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable QueryFinalWasteLog(string StartTime, string EndTime, int UserID, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vFinalWasteLog] where 1=1 ");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (UserID != -2)
                {
                    sb.Append(" and UserID='" + UserID + "'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
                }
                //IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                //DataSet ds = new DataSet();
                //dataAdapter.Fill(ds);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
                List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
                foreach (Entity.Waste item in itemss)
                {
                    DataColumn dc = new DataColumn(item.WasteCode);
                    dt.Columns.Add(dc);
                }

                //List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                //foreach (Entity.AnalysisItem item in items)
                //{
                //    DataColumn dc = new DataColumn(item.ItemName);
                //    dt.Columns.Add(dc);
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.FinalWaste> list2 = DAL.FinalWaste.GetFinalWaste(int.Parse(dr["LogID"].ToString()));
                    if (list2.Count > 0)
                    {
                        foreach (Entity.FinalWaste entity in list2)
                        {
                            dr[entity.ItemCode] = entity.Result.ToString();
                        }
                    }
                }

                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                //dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable QueryFinalWasteLogEx(string StartTime, string EndTime, int UserID, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select LogID, DateTime 日期,LogNumber as 记录编号,RealName as 填写人,StatusName as 状态 from [vFinalWasteLog] where 1=1 ");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (UserID != -2)
                {
                    sb.Append(" and UserID='" + UserID + "'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
                }
                //IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                //DataSet ds = new DataSet();
                //dataAdapter.Fill(ds);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
                List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
                foreach (Entity.Waste item in itemss)
                {
                    DataColumn dc = new DataColumn(item.WasteCode);
                    dt.Columns.Add(dc);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.FinalWaste> list2 = DAL.FinalWaste.GetFinalWaste(int.Parse(dr["LogID"].ToString()));
                    if (list2.Count > 0)
                    {
                        foreach (Entity.FinalWaste entity in list2)
                        {
                            dr[entity.ItemCode] = entity.Result.ToString();
                        }
                    }
                }

                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                //dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
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



        //public static DataTable QueryFinalWasteLogEx2(string StartTime, string EndTime)
        //{
        //    DataTable dt = new DataTable();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("Select DateTime 日期,PondName as 罐池号,WasteName as 废物名称,FromAmount as 数量,HandleManName as 处置人,ReceiverName as 签收人 from [vFinalWasteLog] where Status=2");
        //        if (!string.IsNullOrEmpty(StartTime))
        //        {
        //            sb.Append(" and DateTime>='" + StartTime + "'");
        //        }
        //        if (!string.IsNullOrEmpty(EndTime))
        //        {
        //            sb.Append(" and DateTime<='" + EndTime + "'");
        //        }
        //        sb.Append(" order by DateTime desc");
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
        //        dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return dt;
        //}



        /// <summary>
        ///
        /// </summary>
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static Entity.FinalWasteLog GetFinalWasteLog(int LogID)
        {
            Entity.FinalWasteLog entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vFinalWasteLog] where LogID='" + LogID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.FinalWasteLog();
                    entity.LogID = DataHelper.ParseToInt(dataReader["LogID"].ToString());
                    entity.LogNumber = DataHelper.ParseToInt(dataReader["LogNumber"].ToString());
                    entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    entity.IYear = DataHelper.ParseToInt(dataReader["IYear"].ToString());
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
                    entity.UserID = DataHelper.ParseToInt(dataReader["UserID"].ToString());
                    entity.RealName = dataReader["RealName"].ToString();
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


        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="DateTime">    </param>
        ///// <returns></returns>
        //public static List<Entity.FinalWasteLog> GetSumFinalWasteLog()
        //{
        //    List<Entity.FinalWasteLog> list = new List<Entity.FinalWasteLog>();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        string Start = string.Format("{0}-01-01", DateTime.Now.Year);
        //        string End = string.Format("{0}-12-31", DateTime.Now.Year);
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vFinalWasteLog where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' group by WasteName", null);
        //        while (dataReader.Read())
        //        {
        //            Entity.FinalWasteLog entity = new Entity.FinalWasteLog();
        //            entity.FromWasteCode = dataReader["WasteName"].ToString();
        //            entity.FromAmount = decimal.Parse(dataReader["Total"].ToString());
        //            list.Add(entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="DateTime">    </param>
        ///// <returns></returns>
        //public static decimal GetPartSumFinalWasteLog(string WasteName)
        //{
        //    decimal sum = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        string Start = string.Format("{0}-01-01", DateTime.Now.Year);
        //        string End = string.Format("{0}-12-31", DateTime.Now.Year);
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vFinalWasteLog where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' and WasteName='" + WasteName + "' group by WasteName", null);
        //        while (dataReader.Read())
        //        {
        //            sum = decimal.Parse(dataReader["Total"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return sum;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="PlanID">    </param>
        ///// <param name="BillNumber">    </param>
        ///// <param name="DateTime">    </param>
        ///// <param name="EnterpriseID">    </param>
        ///// <param name="WasteCode">    </param>
        ///// <param name="Status">    </param>
        ///// <returns></returns>
        //public static DataTable GetSum(string StartTime, string EndTime)
        //{
        //    DataTable dt = new DataTable();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("select sum(FromAmount) as Total,WasteName from vFinalWasteLog where Status=2");
        //        if (!string.IsNullOrEmpty(StartTime))
        //        {
        //            sb.Append(" and DateTime>='" + StartTime + "'");
        //        }
        //        if (!string.IsNullOrEmpty(EndTime))
        //        {
        //            sb.Append(" and DateTime<='" + EndTime + "'");
        //        }
        //        sb.Append(" group by WasteName");
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
        //        dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return dt;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="PlanID">    </param>
        ///// <param name="BillNumber">    </param>
        ///// <param name="DateTime">    </param>
        ///// <param name="EnterpriseID">    </param>
        ///// <param name="WasteCode">    </param>
        ///// <param name="Status">    </param>
        ///// <returns></returns>
        //public static DataTable GetSumEx(string StartTime, string EndTime)
        //{
        //    DataTable dt = new DataTable();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("select WasteName 废物名称,sum(FromAmount) as 合计 from vFinalWasteLog where Status=2");
        //        if (!string.IsNullOrEmpty(StartTime))
        //        {
        //            sb.Append(" and DateTime>='" + StartTime + "'");
        //        }
        //        if (!string.IsNullOrEmpty(EndTime))
        //        {
        //            sb.Append(" and DateTime<='" + EndTime + "'");
        //        }
        //        sb.Append(" group by WasteName");
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
        //        dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return dt;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="DateTime">    </param>
        ///// <returns></returns>
        //public static decimal GetPartSumFinalWasteLogEx(string WasteName)
        //{
        //    decimal sum = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vFinalWasteLog where Status=2 and DateTime='" + DateTime.Now.Date + "' and  WasteName='" + WasteName + "' group by WasteName", null);
        //        while (dataReader.Read())
        //        {
        //            sum = decimal.Parse(dataReader["Total"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return sum;
        //}



        public static int AddFinalWasteLog(Entity.FinalWasteLog entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@LogNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogNumber.GetType().ToString()),entity.LogNumber,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32),
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UserID.GetType().ToString()),entity.UserID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWasteLog_Add", prams);
                iReturn = int.Parse(prams[10].Value.ToString());
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


        public static int AddFinalWasteLogEntity(Entity.FinalWasteLog entity,List<Entity.FinalWaste> wastes)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@LogNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogNumber.GetType().ToString()),entity.LogNumber,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32),
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UserID.GetType().ToString()),entity.UserID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWasteLog_Add", prams);
                iReturn = int.Parse(prams[10].Value.ToString());
                int LogID = int.Parse(prams[10].Value.ToString());               
                foreach (Entity.FinalWaste waste in wastes)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [FinalWaste]([LogID],[ItemCode],[Result],[Status]) values ('" + LogID + "','" + waste.ItemCode + "','" + waste.Result + "','" + entity.Status + "')", null);
                }
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


        public static int UpdateFinalWasteLog(Entity.FinalWasteLog entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@LogID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogID.GetType().ToString()),entity.LogID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UserID.GetType().ToString()),entity.UserID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWasteLog_Update", prams);
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


        public static int UpdateFinalWasteLogEntity(Entity.FinalWasteLog entity,List<Entity.FinalWaste> Adds2, List<Entity.FinalWaste> Update2, List<Entity.FinalWaste> Delete2)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@LogID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogID.GetType().ToString()),entity.LogID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UserID.GetType().ToString()),entity.UserID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWasteLog_Update", prams);                
                foreach (Entity.FinalWaste add in Adds2)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [FinalWaste]([LogID],[ItemCode],[Result],[Status]) values ('" + entity.LogID + "','" + add.ItemCode + "','" + add.Result + "','" + add.Status + "')", null);
                }
                foreach (Entity.FinalWaste update in Update2)
                {
                    IDbDataParameter[] pramsf = {
					dbFactory.MakeInParam("@FWID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.FWID.GetType().ToString()),update.FWID,32),
					dbFactory.MakeInParam("@LogID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogID.GetType().ToString()),entity.LogID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ItemCode.GetType().ToString()),update.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Result.GetType().ToString()),update.Result,10),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Status.GetType().ToString()),update.Status,32)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWaste_Update", pramsf);
                }
                foreach (Entity.FinalWaste delete in Delete2)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWaste] where FWID='" + delete.FWID + "'", null);
                }
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
        /// <param name="DealID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int UpdateFinalWasteLog(int LogID, int Status)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[FinalWasteLog] set Status='" + Status + "'  where LogID='" + LogID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[FinalWaste] set Status='" + Status + "'  where LogID='" + LogID + "'", null);
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
        /// <param name="DealID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int UpdateFinalWasteLogEx(Entity.FinalWasteLog entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[FinalWasteLog] set UpdateDate='" + entity.UpdateDate + "',UpdateUser='" + entity.UpdateUser + "',Status='" + entity.Status + "'where LogID='" + entity.LogID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[FinalWaste] set Status='" + entity.Status + "'  where LogID='" + entity.LogID + "'", null);
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
        /// <param name="PlanNumber">    </param>
        /// <param name="IYear">    </param>
        /// <returns></returns>
        public static int GetMaxNumber(int IYear)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select MAX(Number) as Number from [vFinalWasteLog] where  IYear='" + IYear + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["Number"].ToString());
                }
            }
            catch (Exception ex)
            {

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
        /// <param name="PlanNumber">    </param>
        /// <param name="IYear">    </param>
        /// <returns></returns>
        public static int GetCount(DateTime? Date)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select MAX(Number) as Number from [vFinalWasteLog] where  DateTime='" + Date + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["Number"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return iReturn;
        }


        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="DealID">    </param>
        ///// <param name="Status">    </param>
        ///// <returns></returns>
        //public static int PassFinalWasteLog(Entity.FinalWasteLog entity, decimal Used, List<Entity.ProductDetail> lists)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
        //    IDbTransaction trans = thelper.StartTransaction();
        //    try
        //    {
        //        //lists中的成品Amount存罐池使用量
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[FinalWasteLog] set Status='" + entity.Status + "'  where DealID='" + entity.DealID + "'", null);
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.FromPondID + "','" + Used + "','2','废酸出库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
        //        foreach (Entity.ProductDetail list in lists)
        //        {
        //            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[ProductDetail] set Status=2 where DetailID='" + list.DetailID + "'", null);
        //            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + list.PondID + "','" + list.Amount + "','3','成品入库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
        //            iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + list.Amount + "'where PondID='" + list.PondID + "'", null);
        //        }
        //        thelper.CommitTransaction(trans);
        //        iReturn = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        thelper.RollTransaction(trans);
        //        iReturn = 0;

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn;
        //}



        /// <summary>
        ///
        /// </summary>
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static int DeleteFinalWasteLog(int LogID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWasteLog] where LogID='" + LogID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWaste] where LogID='" + LogID + "'", null);
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

