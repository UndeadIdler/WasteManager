using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class AnalysisItem
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAnalysisItem()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [AnalysisItem]", null);
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
        public static List<Entity.AnalysisItem> GetAnalysisItemList(int IsShow)
        {
            List<Entity.AnalysisItem> list = new List<Entity.AnalysisItem>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [AnalysisItem] where IsShow='" + IsShow + "'", null);
                while (dataReader.Read())
                {
                    Entity.AnalysisItem entity = new Entity.AnalysisItem();
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
        public static DataTable QueryAnalysisItem(string ItemCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [AnalysisItem] where 1=1 ");
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
        public static DataTable QueryAnalysisItemEx(string ItemCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select ItemCode 分析项代码,ItemName 分析项名称,Unit 单位 from [AnalysisItem] where 1=1 ");
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
        public static Entity.AnalysisItem GetAnalysisItemByID(int ItemID)
        {
            Entity.AnalysisItem entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [AnalysisItem] where ItemID='" + ItemID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.AnalysisItem();
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
            string ItemCode="";
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select ItemCode from [AnalysisItem] where ItemName='" + ItemName + "'", null);
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



        public static int AddAnalysisItem(Entity.AnalysisItem entity)
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
                //    dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32),
                //    dbFactory.MakeInParam("@Unit",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unit.GetType().ToString()),entity.Unit,20)
                //};
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisItem_Add", prams);

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [AnalysisItem]([ItemCode],[ItemName],[OrderID],[IsShow],[Unit]) values ('" + entity.ItemCode + "','" + entity.ItemName + "','" + entity.OrderID + "','" + entity.IsShow + "','" + entity.Unit + "')", null);
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


        public static int UpdateAnalysisItem(Entity.AnalysisItem entity)
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
                //    dbFactory.MakeInParam("@IsShow",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsShow.GetType().ToString()),entity.IsShow,32),
                //    dbFactory.MakeInParam("@Unit",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unit.GetType().ToString()),entity.Unit,20)
                //};
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_AnalysisItem_Update", prams);

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[AnalysisItem] set ItemCode='" + entity.ItemCode + "',ItemName='" + entity.ItemName + "',OrderID='" + entity.OrderID + "',IsShow='" + entity.IsShow + "',Unit='" + entity.Unit + "'where ItemID='" + entity.ItemID + "'", null);
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
        public static int DeleteAnalysisItem(int ItemID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [AnalysisItem] where ItemID='" + ItemID + "'", null);
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
