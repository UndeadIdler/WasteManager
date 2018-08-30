using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Area
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="IsDelete">    </param>
        /// <returns></returns>
        public static DataTable GetAllArea(int IsDelete)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Area] where IsDelete='" + IsDelete + "'", null);
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
        public static DataTable QueryArea(string AreaCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Area] where 1=1 ");
                if (!string.IsNullOrEmpty(AreaCode))
                {
                    sb.Append(" and AreaCode like '%" + AreaCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( FullName like '%" + Name + "%' or ShortName like '%" + Name + "%')");
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
        /// <param name="AreaCode">    </param>
        /// <returns></returns>
        public static string GetAreaLetter(string AreaCode)
        {
            string iReturn = string.Empty;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Area] where AreaCode='" + AreaCode + "'", null);
                while (dataReader.Read())
                {
                    iReturn = dataReader["LetterCode"].ToString();
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
        public static DataTable QueryAreaEx(string AreaCode, string Name)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select AreaCode 行政区域代码,FullName 全称,ShortName 简称,LetterCode 代号 from [Area] where 1=1 ");
                if (!string.IsNullOrEmpty(AreaCode))
                {
                    sb.Append(" and AreaCode like '%" + AreaCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( FullName like '%" + Name + "%' or ShortName like '%" + Name + "%')");
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
        public static DataTable GetAllAreaEx2(int IsDelete)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select distinct AreaCode,ShortName from [Area] where IsDelete='" + IsDelete + "'", null);
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
        public static List<Entity.Area> GetAllAreaEx(int IsDelete)
        {
            List<Entity.Area> list = new List<Entity.Area>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Area] where IsDelete='" + IsDelete + "'", null);
                while (dataReader.Read())
                {
                    Entity.Area entity = new Entity.Area();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.AreaCode = DataHelper.ParseToInt(dataReader["AreaCode"].ToString());
                    entity.FullName = dataReader["FullName"].ToString();
                    entity.LetterCode = dataReader["LetterCode"].ToString();
                    entity.ShortName = dataReader["ShortName"].ToString();
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
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
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static Entity.Area GetAreaByID(int ID)
        {
            Entity.Area entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Area] where ID='" + ID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Area();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.AreaCode = DataHelper.ParseToInt(dataReader["AreaCode"].ToString());
                    entity.FullName = dataReader["FullName"].ToString();
                    entity.LetterCode = dataReader["LetterCode"].ToString();
                    entity.ShortName = dataReader["ShortName"].ToString();
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());
                    entity.OrderID = DataHelper.ParseToInt(dataReader["OrderID"].ToString());
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


        public static int AddArea(Entity.Area entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AreaCode.GetType().ToString()),entity.AreaCode,32),
					dbFactory.MakeInParam("@FullName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FullName.GetType().ToString()),entity.FullName,20),
					dbFactory.MakeInParam("@LetterCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LetterCode.GetType().ToString()),entity.LetterCode,10),
					dbFactory.MakeInParam("@ShortName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ShortName.GetType().ToString()),entity.ShortName,20),
					dbFactory.MakeInParam("@IsDelete",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsDelete.GetType().ToString()),entity.IsDelete,32),
					dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Area_Add", prams);
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


        public static int UpdateArea(Entity.Area entity)
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
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AreaCode.GetType().ToString()),entity.AreaCode,32),
					dbFactory.MakeInParam("@FullName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FullName.GetType().ToString()),entity.FullName,20),
					dbFactory.MakeInParam("@LetterCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LetterCode.GetType().ToString()),entity.LetterCode,10),
					dbFactory.MakeInParam("@ShortName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ShortName.GetType().ToString()),entity.ShortName,20),
					dbFactory.MakeInParam("@IsDelete",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsDelete.GetType().ToString()),entity.IsDelete,32),
					dbFactory.MakeInParam("@OrderID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrderID.GetType().ToString()),entity.OrderID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Area_Update", prams);
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
        public static int DeleteArea(int ID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Area] where ID='" + ID + "'", null);
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


        #region 作废的
        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static List<Entity.Area> GetAllT_AreaInfo()
        //{
        //    List<Entity.Area> list = new List<Entity.Area>();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_T_AreaInfo_GetAll", prams);
        //        while (dataReader.Read())
        //        {
        //            Entity.Area entity = new Entity.Area();
        //            entity.Id = DataHelper.ParseToInt(dataReader["id"].ToString());
        //            entity.Area_id = DataHelper.ParseToInt(dataReader["area_id"].ToString());
        //            entity.Area_name = dataReader["area_name"].ToString();
        //            entity.Area_bm = dataReader["area_bm"].ToString();
        //            entity.Area_jc = dataReader["area_jc"].ToString();
        //            entity.Flag = DataHelper.ParseToInt(dataReader["flag"].ToString());
        //            entity.Orderid = DataHelper.ParseToInt(dataReader["orderid"].ToString());
        //            list.Add(entity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return list;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static DataTable GetAllArea()
        //{
        //    DataTable dt = new DataTable();
        //    //List<Entity.Area> list = new List<Entity.Area>();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_T_AreaInfo_GetAll", prams);
        //        if (dataReader.Read())
        //        {
        //            dt = dataReader.GetSchemaTable();
        //        }
        //        //while (dataReader.Read())
        //        //{
        //        //    Entity.Area entity = new Entity.Area();
        //        //    entity.Id = DataHelper.ParseToInt(dataReader["id"].ToString());
        //        //    entity.Area_id = DataHelper.ParseToInt(dataReader["area_id"].ToString());
        //        //    entity.Area_name = dataReader["area_name"].ToString();
        //        //    entity.Area_bm = dataReader["area_bm"].ToString();
        //        //    entity.Area_jc = dataReader["area_jc"].ToString();
        //        //    entity.Flag = DataHelper.ParseToInt(dataReader["flag"].ToString());
        //        //    entity.Orderid = DataHelper.ParseToInt(dataReader["orderid"].ToString());
        //        //    list.Add(entity);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return dt;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static int GetMaxID()
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select MAX(id) as ID from [T_AreaInfo]", null);
        //        while (dataReader.Read())
        //        {
        //            iReturn = DataHelper.ParseToInt(dataReader["ID"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return iReturn;
        //}


        ///// <summary>
        /////
        ///// </summary>
        ///// <returns></returns>
        //public static string GetAreaString(string AreaInCharge)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string[] areas = AreaInCharge.Split('，');
        //    foreach (string area in areas)
        //    {
        //        sb.Append("'" + area + "',");
        //    }
        //    string s = sb.ToString();
        //    return s.Substring(0, s.Length - 1);
        //}

        #endregion

    }
}

