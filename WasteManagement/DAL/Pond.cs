using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Pond
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllPond()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Pond]", null);
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
        /// <returns></returns>
        public static DataTable GetAllPond2()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond]", null);
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
        /// <returns></returns>
        public static DataTable GetPartPond(int WasteType)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond] where Type='"+WasteType+"'", null);
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
        /// <returns></returns>
        public static List<Entity.Pond> GetAllPondEx()
        {
            List<Entity.Pond> list = new List<Entity.Pond>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond]", null);
                while (dataReader.Read())
                {
                    Entity.Pond entity = new Entity.Pond();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.Capacity = decimal.Parse(dataReader["Capacity"].ToString());
                    entity.Used = decimal.Parse(dataReader["Used"].ToString());
                    entity.Stores = dataReader["Stores"].ToString();
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
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
        /// <returns></returns>
        public static List<Entity.Pond> GetAllPondEx2(int WasteType)
        {
            List<Entity.Pond> list = new List<Entity.Pond>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond] where Type='" + WasteType + "'", null);
                while (dataReader.Read())
                {
                    Entity.Pond entity = new Entity.Pond();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.Capacity = decimal.Parse(dataReader["Capacity"].ToString());
                    entity.Used = decimal.Parse(dataReader["Used"].ToString());
                    entity.Stores = dataReader["Stores"].ToString();
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
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
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable QueryPond(string WasteCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vPond] where 1=1 ");
                if (!string.IsNullOrEmpty(WasteCode))
                {
                    sb.Append(" and Stores like '%" + WasteCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and WasteName like '%" + Name + "%'");
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
        public static DataTable QueryPondEx(string WasteCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select Name 罐池号, Capacity 容量,WasteName 贮存品,Description 贮存品种类,Number 编号,Status 状态 from [vPond] where 1=1 ");
                if (!string.IsNullOrEmpty(WasteCode))
                {
                    sb.Append(" and Stores like '%" + WasteCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and WasteName like '%" + Name + "%'");
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
        /// <param name="PondID">    </param>
        /// <returns></returns>
        public static Entity.Pond GetPond(int PondID)
        {
            Entity.Pond entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond] where PondID='" + PondID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Pond();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.Capacity = decimal.Parse(dataReader["Capacity"].ToString());
                    entity.Stores = dataReader["Stores"].ToString();
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
                    entity.Used = decimal.Parse(dataReader["Used"].ToString());
                    //entity.Remain = decimal.Parse(dataReader["Remain"].ToString());
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.Status = dataReader["Status"].ToString();
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
        /// <param name="PondID">    </param>
        /// <returns></returns>
        public static Entity.Pond GetPondByName(string Name)
        {
            Entity.Pond entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPond] where Name='" + Name + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Pond();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.Capacity = decimal.Parse(dataReader["Capacity"].ToString());
                    entity.Stores = dataReader["Stores"].ToString();
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
                    entity.Used = decimal.Parse(dataReader["Used"].ToString());
                    //entity.Remain = decimal.Parse(dataReader["Remain"].ToString());
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.Status = dataReader["Status"].ToString();
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


        public static int AddPond(Entity.Pond entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Name.GetType().ToString()),entity.Name,20),
					dbFactory.MakeInParam("@Capacity",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Capacity.GetType().ToString()),entity.Capacity,10),
					dbFactory.MakeInParam("@Stores",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Stores.GetType().ToString()),entity.Stores,20),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32),
					dbFactory.MakeInParam("@IsDelete",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsDelete.GetType().ToString()),entity.IsDelete,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Pond_Add", prams);
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


        public static int UpdatePond(Entity.Pond entity)
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
					dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Name.GetType().ToString()),entity.Name,20),
					dbFactory.MakeInParam("@Capacity",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Capacity.GetType().ToString()),entity.Capacity,10),
					dbFactory.MakeInParam("@Stores",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Stores.GetType().ToString()),entity.Stores,20),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32),
                    //dbFactory.MakeInParam("@Remain",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remain.GetType().ToString()),entity.Remain,10),
					dbFactory.MakeInParam("@IsDelete",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsDelete.GetType().ToString()),entity.IsDelete,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Pond_Update", prams);
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


        //public static int UpdatePondAmount(Entity.Pond entity)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
        //    IDbTransaction trans = thelper.StartTransaction();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
        //            dbFactory.MakeInParam("@Used",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Used.GetType().ToString()),entity.Used,10),
        //            dbFactory.MakeInParam("@Remain",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remain.GetType().ToString()),entity.Remain,10)
        //        };
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Pond_UpdateAmount", prams);
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
        /// <param name="PondID">    </param>
        /// <returns></returns>
        public static int DeletePond(int PondID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Pond] where PondID='" + PondID + "'", null);
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
