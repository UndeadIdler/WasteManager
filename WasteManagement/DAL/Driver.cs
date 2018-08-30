using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class Driver
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="IsStop">    </param>
        /// <returns></returns>
        public static DataTable GetAllDriver(bool IsStop)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Driver] where IsStop='" + IsStop + "'", null);
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
        /// <param name="IsStop">    </param>
        /// <returns></returns>
        public static DataTable GetAllDriverEx()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Driver] ", null);
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


        public static List<string> GetDriverNames()
        {
            List<string> list = new List<string>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select  RealName from [Driver] where IsStop=0", null);
                while (dataReader.Read())
                {
                    list.Add(dataReader["RealName"].ToString());
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


        public static List<string> GetCarNumbers()
        {
            List<string> list = new List<string>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select distinct CarNumber from [Driver] where IsStop=0", null);
                while (dataReader.Read())
                {
                    list.Add(dataReader["CarNumber"].ToString());
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
        /// <param name="Name">    </param>
        /// <returns></returns>
        public static int GetDriverID(string Name)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select ID from [Driver] where RealName='" + Name + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["ID"].ToString());
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
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable QueryDriver(string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Driver] where 1=1 ");
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and RealName like '%" + Name + "%'");
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
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable QueryDriverEx(string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select RealName as 姓名,CarNumber as 常用车牌 from [Driver] where 1=1 ");
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and RealName like '%" + Name + "%'");
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
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static Entity.Driver GetDriverByID(int ID)
        {
            Entity.Driver entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Driver] where ID='" + ID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Driver();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.RealName = dataReader["RealName"].ToString();
                    entity.CarNumber = dataReader["CarNumber"].ToString();
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    entity.IsStop = DataHelper.ParseToBoolean(dataReader["IsStop"].ToString());
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


        public static int AddDriver(Entity.Driver entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RealName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.RealName.GetType().ToString()),entity.RealName,20),
					dbFactory.MakeInParam("@CarNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarNumber.GetType().ToString()),entity.CarNumber,20),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@IsStop",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsStop.GetType().ToString()),entity.IsStop,4)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Driver_Add", prams);
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

        public static int UpdateDriver(Entity.Driver entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ID.GetType().ToString()),entity.ID,32),
					dbFactory.MakeInParam("@RealName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.RealName.GetType().ToString()),entity.RealName,20),
					dbFactory.MakeInParam("@CarNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarNumber.GetType().ToString()),entity.CarNumber,20),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@IsStop",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsStop.GetType().ToString()),entity.IsStop,4)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Driver_Update", prams);
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



        public static int UpdateDriverStatus(Entity.Driver entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ID.GetType().ToString()),entity.ID,32),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@IsStop",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsStop.GetType().ToString()),entity.IsStop,4)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Driver_UpdateStatus", prams);
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
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static int DeleteDriver(int ID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Driver] where ID='" + ID + "'", null);
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
