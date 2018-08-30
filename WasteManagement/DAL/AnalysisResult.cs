using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class AnalysisResult
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="BillNumber">    </param>
        /// <returns></returns>
        public static DataTable GetAnalysisResult(string BillNumber)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vAnalysisResult] where BillNumber='" + BillNumber + "'", null);
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
        public static List<Entity.AnalysisResult> GetAnalysisResultEx(string BillNumber)
        {
            List<Entity.AnalysisResult> list = new List<Entity.AnalysisResult>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vAnalysisResult] where BillNumber='" + BillNumber + "'", null);
                while (dataReader.Read())
                {
                    Entity.AnalysisResult entity = new Entity.AnalysisResult();
                    entity.ResultID = DataHelper.ParseToInt(dataReader["ResultID"].ToString());
                    entity.BillNumber = dataReader["BillNumber"].ToString();
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
        public static Entity.AnalysisResult GetAnalysisResultByID(int ResultID)
        {
            Entity.AnalysisResult entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [AnalysisResult] where ResultID='" + ResultID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.AnalysisResult();
                    entity.ResultID = DataHelper.ParseToInt(dataReader["ResultID"].ToString());
                    entity.BillNumber = dataReader["BillNumber"].ToString();
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


        public static int AddAnalysisResult(Entity.AnalysisResult entity)
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
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Add", prams);
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


        public static int UpdateAnalysisResult(Entity.AnalysisResult entity)
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
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Update", prams);
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
        public static int DeleteAnalysisResult(int ResultID)
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
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisResult_Delete", prams);
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
