using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Menu
    {
        public static int AddMenu(Entity.Menu entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@FatherID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FatherID.GetType().ToString()),entity.FatherID,32),
					dbFactory.MakeInParam("@MenuName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuName.GetType().ToString()),entity.MenuName,50),
					dbFactory.MakeInParam("@ImageUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ImageUrl.GetType().ToString()),entity.ImageUrl,0),
					dbFactory.MakeInParam("@MenuUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuUrl.GetType().ToString()),entity.MenuUrl,50),
					dbFactory.MakeInParam("@MenuOrder",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuOrder.GetType().ToString()),entity.MenuOrder,32),
					dbFactory.MakeInParam("@MenuFile",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuFile.GetType().ToString()),entity.MenuFile,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Menu_Add", prams);
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

        public static int UpdateMenu(Entity.Menu entity)
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
					dbFactory.MakeInParam("@FatherID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FatherID.GetType().ToString()),entity.FatherID,32),
					dbFactory.MakeInParam("@MenuName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuName.GetType().ToString()),entity.MenuName,50),
					dbFactory.MakeInParam("@ImageUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ImageUrl.GetType().ToString()),entity.ImageUrl,0),
					dbFactory.MakeInParam("@MenuUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuUrl.GetType().ToString()),entity.MenuUrl,50),
					dbFactory.MakeInParam("@MenuOrder",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuOrder.GetType().ToString()),entity.MenuOrder,32),
					dbFactory.MakeInParam("@MenuFile",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.MenuFile.GetType().ToString()),entity.MenuFile,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Menu_Update", prams);
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


        #region  旧的
        /// <summary>
        ///
        /// </summary>
        /// <param name="MenuName">    </param>
        /// <param name="MenuUrl">    </param>
        /// <param name="MenuDescription">    </param>
        /// <param name="ParentID">    </param>
        /// <returns></returns>
        public static int AddMenu(string MenuName, string MenuUrl, string MenuDescription, int ParentID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MenuName",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuName.GetType().ToString()),MenuName,50),
					dbFactory.MakeInParam("@MenuUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuUrl.GetType().ToString()),MenuUrl,0),
					dbFactory.MakeInParam("@MenuDescription",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuDescription.GetType().ToString()),MenuDescription,50),
					dbFactory.MakeInParam("@ParentID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ParentID.GetType().ToString()),ParentID,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Menu_Add", prams);
                iReturn = int.Parse(prams[4].Value.ToString());
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
        /// <param name="MenuID">    </param>
        /// <param name="MenuName">    </param>
        /// <param name="MenuUrl">    </param>
        /// <param name="MenuDescription">    </param>
        /// <param name="ParentID">    </param>
        /// <returns></returns>
        public static int UpdateMenu(int MenuID, string MenuName, string MenuUrl, string MenuDescription, int ParentID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MenuID",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuID.GetType().ToString()),MenuID,32),
					dbFactory.MakeInParam("@MenuName",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuName.GetType().ToString()),MenuName,50),
					dbFactory.MakeInParam("@MenuUrl",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuUrl.GetType().ToString()),MenuUrl,0),
					dbFactory.MakeInParam("@MenuDescription",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuDescription.GetType().ToString()),MenuDescription,50),
					dbFactory.MakeInParam("@ParentID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ParentID.GetType().ToString()),ParentID,32)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Menu_Update", prams);
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
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static int DeleteMenu(int MenuID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MenuID",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuID.GetType().ToString()),MenuID,32)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Menu_Delete", prams);
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
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static Entity.Menu GetMenu(int MenuID)
        {
            Entity.Menu entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@MenuID",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuID.GetType().ToString()),MenuID,32)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Menu_Get", prams);
                while (dataReader.Read())
                {
                    entity = new Entity.Menu();
                    //entity.MenuID = DataHelper.ParseToInt(dataReader["MenuID"].ToString());
                    entity.MenuName = dataReader["MenuName"].ToString();
                    entity.MenuUrl = dataReader["MenuUrl"].ToString();
                    //entity.MenuDescription = dataReader["MenuDescription"].ToString();
                    //entity.ParentID = DataHelper.ParseToInt(dataReader["ParentID"].ToString());
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
        /// <returns></returns>
        public static List<Entity.Menu> GetAllMenu()
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Menu_GetAll", prams);
                while (dataReader.Read())
                {
                    Entity.Menu entity = new Entity.Menu();
                    //entity.MenuID = DataHelper.ParseToInt(dataReader["MenuID"].ToString());
                    entity.MenuName = dataReader["MenuName"].ToString();
                    entity.MenuUrl = dataReader["MenuUrl"].ToString();
                    //entity.MenuDescription = dataReader["MenuDescription"].ToString();
                    //entity.ParentID = DataHelper.ParseToInt(dataReader["ParentID"].ToString());
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
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static List<Entity.Menu> GetMenuByUser(int UserID)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserID.GetType().ToString()),UserID,32)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Menu_GetByUser", prams);
                while (dataReader.Read())
                {
                    Entity.Menu entity = new Entity.Menu();
                    //entity.MenuID = DataHelper.ParseToInt(dataReader["MenuID"].ToString());
                    entity.MenuName = dataReader["MenuName"].ToString();
                    entity.MenuUrl = dataReader["MenuUrl"].ToString();
                    //entity.MenuDescription = dataReader["MenuDescription"].ToString();
                    //entity.ParentID = DataHelper.ParseToInt(dataReader["ParentID"].ToString());
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
        public static List<Entity.Menu> GetMenuByUserEx(int UserID)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vUserMenu] where UserID='" + UserID + "'", null);
                while (dataReader.Read())
                {
                    Entity.Menu entity = new Entity.Menu();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.FatherID = DataHelper.ParseToInt(dataReader["FatherID"].ToString());
                    entity.MenuName = dataReader["MenuName"].ToString();
                    entity.ImageUrl = dataReader["ImageUrl"].ToString();
                    entity.MenuUrl = dataReader["MenuUrl"].ToString();
                    entity.MenuOrder = DataHelper.ParseToInt(dataReader["MenuOrder"].ToString());
                    entity.MenuFile = dataReader["MenuFile"].ToString();
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
        #endregion
    }
}

