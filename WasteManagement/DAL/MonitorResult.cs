using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class MonitorResult
    {/// <summary>
        ///
        /// </summary>
        /// <param name="BillNumber">    </param>
        /// <returns></returns>
        public static DataTable GetMonitorResult(int MonitorID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vMonitorResult] where MonitorID='" + MonitorID + "'", null);
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
        /// <returns></returns>
        public static List<Entity.MonitorResult> GetMonitorResultEx(int MonitorID)
        {
            List<Entity.MonitorResult> list = new List<Entity.MonitorResult>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vMonitorResult] where MonitorID='" + MonitorID + "'", null);
                while (dataReader.Read())
                {
                    Entity.MonitorResult entity = new Entity.MonitorResult();
                    entity.ResultID = DataHelper.ParseToInt(dataReader["ResultID"].ToString());
                    entity.MonitorID = DataHelper.ParseToInt(dataReader["MonitorID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.ItemName = dataReader["ItemName"].ToString();
                    entity.Result = decimal.Parse(dataReader["Result"].ToString());
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
        /// <param name="ResultID">    </param>
        /// <returns></returns>
        public static Entity.MonitorResult GetMonitorResultByID(int ResultID)
        {
            Entity.MonitorResult entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [MonitorResult] where ResultID='" + ResultID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.MonitorResult();
                    entity.ResultID = DataHelper.ParseToInt(dataReader["ResultID"].ToString());
                    entity.MonitorID = DataHelper.ParseToInt(dataReader["MonitorID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.Result = decimal.Parse(dataReader["Result"].ToString());
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


        public static int AddMonitorResult(Entity.MonitorResult entity)
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
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Add", prams);
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


        public static int UpdateMonitorResult(Entity.MonitorResult entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ResultID.GetType().ToString()),entity.ResultID,32),
					dbFactory.MakeInParam("@MonitorID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MonitorID.GetType().ToString()),entity.MonitorID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Update", prams);
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
        /// <param name="ResultID">    </param>
        /// <returns></returns>
        public static int DeleteMonitorResult(int ResultID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ResultID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ResultID.GetType().ToString()),ResultID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_MonitorResult_Delete", prams);
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

