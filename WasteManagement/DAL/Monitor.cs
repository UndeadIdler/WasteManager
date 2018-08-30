using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Monitor
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMonitor()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Monitor]", prams);
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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable GetMonitor(int PositionID, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vMonitor] where 1=1 ");
                if (PositionID != -2)
                {
                    sb.Append(" and PositionID='" + PositionID + "'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (AnalysisManID != -2)
                {
                    sb.Append(" and AnalysisManID='" + AnalysisManID + "'");
                }
                //IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(),);

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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable GetMonitorEx(int PositionID, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vMonitor] where 1=1 ");
                if (PositionID != -2)
                {
                    sb.Append(" and PositionID='" + PositionID + "'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (AnalysisManID != -2)
                {
                    sb.Append(" and AnalysisManID='" + AnalysisManID + "'");
                }
                IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                dt = ds.Tables[0];
                List<Entity.MonitorItem> items = DAL.MonitorItem.GetMonitorItemList(1);
                foreach (Entity.MonitorItem item in items)
                {
                    //DataColumn dc = new DataColumn(item.ItemName);
                    DataColumn dc = new DataColumn(item.ItemCode);
                    dt.Columns.Add(dc);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.MonitorResult> list = DAL.MonitorResult.GetMonitorResultEx(int.Parse(dr["MonitorID"].ToString()));
                    if (list.Count > 0)
                    {
                        foreach (Entity.MonitorResult entity in list)
                        {

                            dr[entity.ItemCode] = entity.Result.ToString();
                            //dr[entity.ItemName] = entity.Result.ToString();
                        }
                    }

                    //DataTable dt2 = DAL.AnalysisResult.GetAnalysisResult(dr["BillNumber"].ToString());
                    //if (dt2.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr2 in dt2.Rows)
                    //    {

                    //    }

                    //}
                }

                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                //dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    db.Conn.Close();
            //}
            return dt;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable GetMonitorEx2(int PositionID, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select MonitorID 记录ID, PositionName 监测位置,DateTime 日期,RealName 分析人 from [vMonitor] where 1=1 ");
                if (PositionID != -2)
                {
                    sb.Append(" and PositionID='" + PositionID + "'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (AnalysisManID != -2)
                {
                    sb.Append(" and AnalysisManID='" + AnalysisManID + "'");
                }
                IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                dt = ds.Tables[0];
                List<Entity.MonitorItem> items = DAL.MonitorItem.GetMonitorItemList(1);
                foreach (Entity.MonitorItem item in items)
                {
                    DataColumn dc = new DataColumn(item.ItemName);
                    //DataColumn dc = new DataColumn(item.ItemCode);
                    dt.Columns.Add(dc);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.MonitorResult> list = DAL.MonitorResult.GetMonitorResultEx(int.Parse(dr["记录ID"].ToString()));
                    if (list.Count > 0)
                    {
                        foreach (Entity.MonitorResult entity in list)
                        {

                            //dr[entity.ItemCode] = entity.Result.ToString();
                            dr[entity.ItemName] = entity.Result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    db.Conn.Close();
            //}
            return dt;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="AnalysisID">    </param>
        /// <returns></returns>
        public static Entity.Monitor GetMonitorByID(int MonitorID)
        {
            Entity.Monitor entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vMonitor] where MonitorID='" + MonitorID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Monitor();
                    entity.MonitorID = DataHelper.ParseToInt(dataReader["MonitorID"].ToString());
                    entity.PositionName = dataReader["PositionName"].ToString();
                    entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    entity.AnalysisManID = DataHelper.ParseToInt(dataReader["AnalysisManID"].ToString());
                    entity.RealName = dataReader["RealName"].ToString();
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
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



        public static int AddMonitorEntity(Entity.Monitor entity, List<Entity.MonitorResult> results)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@PositionID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PositionID.GetType().ToString()),entity.PositionID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", prams);
                iReturn = int.Parse(prams[8].Value.ToString());
                int MonitorID = int.Parse(prams[8].Value.ToString());
                foreach (Entity.MonitorResult result in results)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(MonitorID.GetType().ToString()),MonitorID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(result.ItemCode.GetType().ToString()),result.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(result.Result.GetType().ToString()),result.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Add", pramse);
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




        public static int AddMonitor(Entity.Monitor entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@PositionID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PositionID.GetType().ToString()),entity.PositionID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Add", prams);
                iReturn = int.Parse(prams[7].Value.ToString());
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


        public static int UpdateMonitor(Entity.Monitor entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MonitorID.GetType().ToString()),entity.MonitorID,32),
					dbFactory.MakeInParam("@PositionID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PositionID.GetType().ToString()),entity.PositionID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Update", prams);
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


        public static int UpdateMonitorEntity(Entity.Monitor entity, List<Entity.MonitorResult> Adds, List<Entity.MonitorResult> Deletes, List<Entity.MonitorResult> Updates)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MonitorID.GetType().ToString()),entity.MonitorID,32),
					dbFactory.MakeInParam("@PositionID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PositionID.GetType().ToString()),entity.PositionID,32),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Monitor_Update", prams);
                foreach (Entity.MonitorResult add in Adds)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MonitorID.GetType().ToString()),entity.MonitorID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.ItemCode.GetType().ToString()),add.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.Result.GetType().ToString()),add.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Add", pramse);
                }
                foreach (Entity.MonitorResult update in Updates)
                {
                    IDbDataParameter[] pramsq = {
					dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ResultID.GetType().ToString()),update.ResultID,32),
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MonitorID.GetType().ToString()),entity.MonitorID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ItemCode.GetType().ToString()),update.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Result.GetType().ToString()),update.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Update", pramsq);
                }
                foreach (Entity.MonitorResult delete in Deletes)
                {
                    //IDbDataParameter[] pramsw = {
                    //dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(delete.ResultID.GetType().ToString()),delete.ResultID,32)
                    //};
                    //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Delete", pramsw);
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [MonitorResult] where ResultID='" + delete.ResultID + "'", null);
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



        public static int PassMonitor(Entity.Monitor entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //IDbDataParameter[] prams = {
                //    dbFactory.MakeInParam("@AnalysisID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisID.GetType().ToString()),entity.AnalysisID,32),
                //    dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
                //};
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_UpdateStatus", prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Monitor] set Status='" + entity.Status + "',UpdateDate='" + entity.UpdateDate + "',UpdateUser='" + entity.UpdateUser + "'where MonitorID='" + entity.MonitorID + "'", null);
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
        /// <param name="AnalysisID">    </param>
        /// <returns></returns>
        public static int DeleteMonitor(int MonitorID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Monitor] where MonitorID='" + MonitorID + "'", null);
                //string BillNumber = DAL.Analysis.GetAnalysisByID(AnalysisID).BillNumber;
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [MonitorResult] where MonitorID='" + MonitorID + "'", null);
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

