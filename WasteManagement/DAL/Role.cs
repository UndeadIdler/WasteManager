using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Role
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="RoleName">    </param>
        /// <param name="RoleDescription">    </param>
        /// <returns></returns>
        public static int AddRole(string RoleName, string RoleDescription)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleName",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleName.GetType().ToString()),RoleName,50),
					dbFactory.MakeInParam("@RoleDescription",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleDescription.GetType().ToString()),RoleDescription,100)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Role_Add", prams);
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
        /// <param name="RoleID">    </param>
        /// <param name="RoleName">    </param>
        /// <param name="RoleDescription">    </param>
        /// <returns></returns>
        public static int UpdateRole(int RoleID, string RoleName, string RoleDescription)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleID.GetType().ToString()),RoleID,32),
					dbFactory.MakeInParam("@RoleName",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleName.GetType().ToString()),RoleName,50),
					dbFactory.MakeInParam("@RoleDescription",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleDescription.GetType().ToString()),RoleDescription,100)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Role_Update", prams);
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
        /// <param name="RoleID">    </param>
        /// <returns></returns>
        public static int DeleteRole(int RoleID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleID.GetType().ToString()),RoleID,32)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Role_Delete", prams);
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
        /// <param name="RoleID">    </param>
        /// <returns></returns>
        public static Entity.Role GetRole(int RoleID)
        {
            Entity.Role entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleID.GetType().ToString()),RoleID,32)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Role_Get", prams);
                while (dataReader.Read())
                {
                    entity = new Entity.Role();
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    entity.RoleName = dataReader["RoleName"].ToString();
                    //entity.RoleDescription = dataReader["RoleDescription"].ToString();
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
        public static List<Entity.Role> GetAllRole()
        {
            List<Entity.Role> list = new List<Entity.Role>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select * from Role", prams);
                while (dataReader.Read())
                {
                    Entity.Role entity = new Entity.Role();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.RoleName = dataReader["RoleName"].ToString();
                    entity.Description = dataReader["Description"].ToString();
                    //entity.IsAudit = DataHelper.ParseToBoolean(dataReader["IsAudit"].ToString());
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
        public static DataTable GetAllRoles()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Role]", null);
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

    }
}