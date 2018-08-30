using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class FinalWaste
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static List<Entity.FinalWaste> GetAllFinalWaste(int LogID)
        {
            List<Entity.FinalWaste> list = new List<Entity.FinalWaste>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [FinalWaste] where LogID='" + LogID + "'", null);
                while (dataReader.Read())
                {
                    Entity.FinalWaste entity = new Entity.FinalWaste();
                    entity.FWID = DataHelper.ParseToInt(dataReader["FWID"].ToString());
                    entity.LogID = DataHelper.ParseToInt(dataReader["LogID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.Result = decimal.Parse(dataReader["Result"].ToString());
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
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
        public static DataTable GetFinalWasteEx(int LogID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vFinalWaste] where LogID='" + LogID + "'", null);
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
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static List<Entity.FinalWaste> GetFinalWaste(int LogID)
        {
            List<Entity.FinalWaste> list = new List<Entity.FinalWaste>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [FinalWaste] where LogID='" + LogID + "'", null);
                while (dataReader.Read())
                {
                    Entity.FinalWaste entity = new Entity.FinalWaste();
                    entity.FWID = DataHelper.ParseToInt(dataReader["FWID"].ToString());
                    entity.LogID = DataHelper.ParseToInt(dataReader["LogID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.Result = decimal.Parse(dataReader["Result"].ToString());
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
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

        public static int AddFinalWaste(Entity.FinalWaste entity)
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
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWaste_Add", prams);
                iReturn = int.Parse(prams[4].Value.ToString());
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


        public static int UpdateFinalWaste(Entity.FinalWaste entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@FWID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FWID.GetType().ToString()),entity.FWID,32),
					dbFactory.MakeInParam("@LogID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LogID.GetType().ToString()),entity.LogID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
					dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Result.GetType().ToString()),entity.Result,10),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWaste_Update", prams);
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
        /// <param name="FWID">    </param>
        /// <returns></returns>
        public static int DeleteFinalWaste(int FWID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWaste] where FWID='" + FWID + "'", null);
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
