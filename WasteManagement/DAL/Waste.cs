using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class Waste
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllWaste()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Waste]", null);
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
        public static DataTable GetUseWaste()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select distinct Stores,WasteName from [vPond]", null);
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


        public static DataTable GetPartWaste(int Type)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Waste] where Type='" + Type+"'", null);
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
        /// <param name="Type">    </param>
        /// <returns></returns>
        public static List<Entity.Waste> GetPartWasteEx(int Type)
        {
            List<Entity.Waste> list = new List<Entity.Waste>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Waste] where Type='" + Type + "'", null);
                while (dataReader.Read())
                {
                    Entity.Waste entity = new Entity.Waste();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.List = dataReader["List"].ToString();
                    entity.Type = DataHelper.ParseToInt(dataReader["Type"].ToString());
                    entity.Unit = dataReader["Unit"].ToString();
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
                    entity.IsShow = DataHelper.ParseToInt(dataReader["IsShow"].ToString());
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
        /// <param name="ItemID">    </param>
        /// <returns></returns>
        public static string GetWasteCodeByName(string WasteName)
        {
            string ItemCode = "";
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select WasteCode from [Waste] where WasteName='" + WasteName + "'", null);
                while (dataReader.Read())
                {
                    ItemCode = dataReader["WasteCode"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return ItemCode;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable QueryWaste(string WasteCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Waste] where 1=1 ");
                if (!string.IsNullOrEmpty(WasteCode))
                {
                    sb.Append(" and WasteCode like '%" + WasteCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( WasteName like '%" + Name + "%' or List like '%" + Name + "%')");
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
        public static DataTable QueryWasteEx(string WasteCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select WasteName as 废物名称,OrderID as 编号,WasteCode as 废物代码 from [Waste] where 1=1 ");
                if (!string.IsNullOrEmpty(WasteCode))
                {
                    sb.Append(" and WasteCode like '%" + WasteCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( WasteName like '%" + Name + "%' or List like '%" + Name + "%')");
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
        public static Entity.Waste GetWaste(int ID)
        {
            Entity.Waste entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Waste] where ID='" + ID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Waste();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.List = dataReader["List"].ToString();
                    entity.Type = DataHelper.ParseToInt(dataReader["Type"].ToString());
                    entity.Unit = dataReader["Unit"].ToString();
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
                    entity.IsShow = DataHelper.ParseToInt(dataReader["IsShow"].ToString());
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


        public static int AddWaste(Entity.Waste entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@WasteName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteName.GetType().ToString()),entity.WasteName,50),
					dbFactory.MakeInParam("@List",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.List.GetType().ToString()),entity.List,50),
					dbFactory.MakeInParam("@Type",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Type.GetType().ToString()),entity.Type,32),
					dbFactory.MakeInParam("@Unit",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unit.GetType().ToString()),entity.Unit,20),
					dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32),
					dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Waste_Add", prams);
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


        public static int UpdateWaste(Entity.Waste entity)
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
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@WasteName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteName.GetType().ToString()),entity.WasteName,50),
					dbFactory.MakeInParam("@List",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.List.GetType().ToString()),entity.List,50),
					dbFactory.MakeInParam("@Type",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Type.GetType().ToString()),entity.Type,32),
					dbFactory.MakeInParam("@Unit",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unit.GetType().ToString()),entity.Unit,20),
					dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32),
					dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Waste_Update", prams);
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
        public static int DeleteWaste(int ID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Waste] where ID='" + ID + "'", null);
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
