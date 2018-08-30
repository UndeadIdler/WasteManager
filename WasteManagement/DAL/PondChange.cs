using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class PondChange
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllPondChange()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [PondChange]", null);
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
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable QueryPondChange(string PondName, string WasteName)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vPondChange] where 1=1 ");
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and Name like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and WasteName like '%" + WasteName + "%'");
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



        public static int AddPondChange(Entity.PondChange entity,decimal Capacity)
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
					dbFactory.MakeInParam("@OldAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OldAmount.GetType().ToString()),entity.OldAmount,10),
					dbFactory.MakeInParam("@NewAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.NewAmount.GetType().ToString()),entity.NewAmount,10),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_PondChange_Add", prams);
                //
                decimal Used = entity.NewAmount;
                decimal Remain = Capacity - entity.NewAmount;
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + Used + "',Remain='" + Remain + "' where PondID='" + entity.PondID + "'", null);
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


        public static int AddPondChangeEx(Entity.PondChange entity)
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
					dbFactory.MakeInParam("@OldAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OldAmount.GetType().ToString()),entity.OldAmount,10),
					dbFactory.MakeInParam("@NewAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.NewAmount.GetType().ToString()),entity.NewAmount,10),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_PondChange_Add", prams);
                //
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.PondID + "','" + entity.NewAmount + "','6','修正','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + entity.NewAmount + "'  where PondID='" + entity.PondID + "'", null);
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
