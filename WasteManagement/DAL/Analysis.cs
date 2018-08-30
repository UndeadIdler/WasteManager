using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Analysis
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAnalysis()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Analysis_GetAll", prams);
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
        public static DataTable GetAnalysis(string BillNumber, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vAnalysis] where 1=1 ");
                if (!string.IsNullOrEmpty(BillNumber))
                {
                    sb.Append(" and  BillNumber='" + BillNumber + "'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (AnalysisManID!=-2)
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
        public static DataTable GetAnalysisEx(string BillNumber, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vAnalysis] where 1=1 ");
                if (!string.IsNullOrEmpty(BillNumber))
                {
                    sb.Append(" and  BillNumber like '%" + BillNumber + "%'");
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
                IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(),Config.con);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);

                dt = ds.Tables[0];
                List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                foreach (Entity.AnalysisItem item in items)
                {
                    //DataColumn dc = new DataColumn(item.ItemName);
                    DataColumn dc = new DataColumn(item.ItemCode);
                    dt.Columns.Add(dc);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.AnalysisResult> list = DAL.AnalysisResult.GetAnalysisResultEx(dr["BillNumber"].ToString());
                    if (list.Count > 0)
                    {
                        foreach (Entity.AnalysisResult entity in list)
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
        public static DataTable GetAnalysisEx2(string BillNumber, string StartTime, string EndTime, int AnalysisManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select BillNumber 联单号,DateTime 日期,RealName 分析人 from [vAnalysis] where 1=1 ");
                if (!string.IsNullOrEmpty(BillNumber))
                {
                    sb.Append(" and  BillNumber like '%" + BillNumber + "%'");
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
                List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                foreach (Entity.AnalysisItem item in items)
                {
                    DataColumn dc = new DataColumn(item.ItemName);
                    //DataColumn dc = new DataColumn(item.ItemCode);
                    dt.Columns.Add(dc);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.AnalysisResult> list = DAL.AnalysisResult.GetAnalysisResultEx(dr["联单号"].ToString());
                    if (list.Count > 0)
                    {
                        foreach (Entity.AnalysisResult entity in list)
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
        public static Entity.Analysis GetAnalysisByID(int AnalysisID)
        {
            Entity.Analysis entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vAnalysis] where AnalysisID='" + AnalysisID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Analysis();
                    entity.AnalysisID = DataHelper.ParseToInt(dataReader["AnalysisID"].ToString());
                    entity.BillNumber = dataReader["BillNumber"].ToString();
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


        public static List<string> GetBillNumbers()
        {
            List<string> list = new List<string>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select distinct BillNumber from [vWasteStorage] where Status=3 and EndDate>='" + DateTime.Now + "'", null);
                while (dataReader.Read())
                {
                    list.Add(dataReader["BillNumber"].ToString());
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


        public static int AddAnalysisEntity(Entity.Analysis entity,List<Entity.AnalysisResult> results)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
                    dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_Add", prams);
                iReturn = int.Parse(prams[8].Value.ToString());
                int AnalysisID = int.Parse(prams[8].Value.ToString());
                foreach (Entity.AnalysisResult result in results)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(result.ItemCode.GetType().ToString()),result.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(result.Result.GetType().ToString()),result.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Add", pramse);
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




        public static int AddAnalysis(Entity.Analysis entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
                    dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_Add", prams);
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


        public static int UpdateAnalysis(Entity.Analysis entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@AnalysisID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisID.GetType().ToString()),entity.AnalysisID,32),
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_Update", prams);
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


        public static int UpdateAnalysisEntity(Entity.Analysis entity, List<Entity.AnalysisResult> Adds,List<Entity.AnalysisResult> Deletes,List<Entity.AnalysisResult> Updates)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@AnalysisID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisID.GetType().ToString()),entity.AnalysisID,32),
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@AnalysisManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisManID.GetType().ToString()),entity.AnalysisManID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_Update", prams);
                foreach (Entity.AnalysisResult add in Adds)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.ItemCode.GetType().ToString()),add.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.Result.GetType().ToString()),add.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Add", pramse);
                }
                foreach (Entity.AnalysisResult update in Updates)
                {
                    IDbDataParameter[] pramsq = {
					dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ResultID.GetType().ToString()),update.ResultID,32),
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ItemCode.GetType().ToString()),update.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Result.GetType().ToString()),update.Result,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Update", pramsq);
                }
                foreach (Entity.AnalysisResult delete in Deletes)
                {
                    //IDbDataParameter[] pramsw = {
                    //dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(delete.ResultID.GetType().ToString()),delete.ResultID,32)
                    //};
                    //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Delete", pramsw);
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [AnalysisResult] where ResultID='" + delete.ResultID + "'", null);
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



        public static int UpdateAnalysisStatus(Entity.Analysis entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@AnalysisID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisID.GetType().ToString()),entity.AnalysisID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_UpdateStatus", prams);
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


        public static int PassAnalysis(Entity.Analysis entity,int StorageID)
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
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Analysis] set Status='" + entity.Status + "',UpdateDate='" + entity.UpdateDate + "',UpdateUser='" + entity.UpdateUser + "'where AnalysisID='" + entity.AnalysisID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[WasteStorage] set Status='4' where StorageID='" + StorageID + "'", null);
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


        //public static int DeleteAnalysis(Entity.Analysis entity)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
        //    IDbTransaction trans = thelper.StartTransaction();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@AnalysisID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AnalysisID.GetType().ToString()),entity.AnalysisID,32)
        //        };
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Analysis_Delete", prams);
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
        /// <param name="AnalysisID">    </param>
        /// <returns></returns>
        public static int DeleteAnalysis(int AnalysisID, string BillNumber)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //IDbDataParameter[] prams = {
                //};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Analysis] where AnalysisID='" + AnalysisID + "'", null);
                //string BillNumber = DAL.Analysis.GetAnalysisByID(AnalysisID).BillNumber;
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [AnalysisResult] where BillNumber='" + BillNumber + "'", null);
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
