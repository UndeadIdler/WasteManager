using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class MonitorItem
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMonitorItem()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [MonitorItem]", null);
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
        /// <param name="IsShow">    </param>
        /// <returns></returns>
        public static List<Entity.MonitorItem> GetMonitorItemList(int IsShow)
        {
            List<Entity.MonitorItem> list = new List<Entity.MonitorItem>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [MonitorItem] where IsShow='" + IsShow + "'", null);
                while (dataReader.Read())
                {
                    Entity.MonitorItem entity = new Entity.MonitorItem();
                    entity.ItemID = DataHelper.ParseToInt(dataReader["ItemID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.ItemName = dataReader["ItemName"].ToString();
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
                    entity.IsShow = DataHelper.ParseToInt(dataReader["IsShow"].ToString());
                    entity.Unit = dataReader["Unit"].ToString();
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
        public static DataTable QueryMonitorItem(string ItemCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [MonitorItem] where 1=1 ");
                if (!string.IsNullOrEmpty(ItemCode))
                {
                    sb.Append(" and ItemCode like '%" + ItemCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ItemName like '%" + Name + "%'");
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
        public static DataTable QueryMonitorItemEx(string ItemCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select ItemCode 监测项代码,ItemName 监测项名称,Unit 单位 from [MonitorItem] where 1=1 ");
                if (!string.IsNullOrEmpty(ItemCode))
                {
                    sb.Append(" and ItemCode like '%" + ItemCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ItemName like '%" + Name + "%'");
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
        /// <param name="ItemID">    </param>
        /// <returns></returns>
        public static Entity.MonitorItem GetMonitorItemByID(int ItemID)
        {
            Entity.MonitorItem entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [MonitorItem] where ItemID='" + ItemID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.MonitorItem();
                    entity.ItemID = DataHelper.ParseToInt(dataReader["ItemID"].ToString());
                    entity.ItemCode = dataReader["ItemCode"].ToString();
                    entity.ItemName = dataReader["ItemName"].ToString();
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
                    entity.IsShow = DataHelper.ParseToInt(dataReader["IsShow"].ToString());
                    entity.Unit = dataReader["Unit"].ToString();
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
        /// <param name="ItemID">    </param>
        /// <returns></returns>
        public static string GetItemCodeByName(string ItemName)
        {
            string ItemCode = "";
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select ItemCode from [MonitorItem] where ItemName='" + ItemName + "'", null);
                while (dataReader.Read())
                {
                    ItemCode = dataReader["ItemCode"].ToString();
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



        public static int AddMonitorItem(Entity.MonitorItem entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //IDbDataParameter[] prams = {
                //    dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
                //    dbFactory.MakeInParam("@ItemName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemName.GetType().ToString()),entity.ItemName,50),
                //    dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32),
                //    dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32)
                //};
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisItem_Add", prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [MonitorItem]([ItemCode],[ItemName],[OrderID],[IsShow],[Unit]) values ('" + entity.ItemCode + "','" + entity.ItemName + "','" + entity.OrderID + "','" + entity.IsShow + "','" + entity.Unit + "')", null);
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


        public static int UpdateMonitorItem(Entity.MonitorItem entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //IDbDataParameter[] prams = {
                //    dbFactory.MakeInParam("@ItemID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemID.GetType().ToString()),entity.ItemID,32),
                //    dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemCode.GetType().ToString()),entity.ItemCode,20),
                //    dbFactory.MakeInParam("@ItemName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ItemName.GetType().ToString()),entity.ItemName,50),
                //    dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32),
                //    dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32)
                //};
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisItem_Update", prams);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[MonitorItem] set ItemCode='" + entity.ItemCode + "',ItemName='" + entity.ItemName + "',OrderID='" + entity.OrderID + "',IsShow='" + entity.IsShow + "',Unit='" + entity.Unit + "'where ItemID='" + entity.ItemID + "'", null);
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
        /// <param name="ItemID">    </param>
        /// <returns></returns>
        public static int DeleteMonitorItem(int ItemID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [MonitorItem] where ItemID='" + ItemID + "'", null);
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

