using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class UserRole
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserRoles()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vUserRole] order by UserID", null);
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
        public static List<Entity.UserRole> GetAllUserRole()
        {
            List<Entity.UserRole> list = new List<Entity.UserRole>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_UserRole_GetAll", prams);
                while (dataReader.Read())
                {
                    Entity.UserRole entity = new Entity.UserRole();
                    //entity.UserID = DataHelper.ParseToInt(dataReader["UserID"].ToString());
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
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
        /// <param name="UserID">    </param>
        /// <returns></returns>
        public static Entity.UserRole GetUserRoleByGUID(string GUID)
        {
            Entity.UserRole entity = new Entity.UserRole();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                List<Entity.Role> Roles = new List<Entity.Role>();
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select * from vUserRole where GUID='" + GUID + "'", null);
                if (dataReader.Read())
                {
                    //Entity.UserRole entity = new Entity.UserRole();
                    //entity.UserID = DataHelper.ParseToInt(dataReader["UserID"].ToString());
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    //list.Add(entity);
                    entity.user.ID = DataHelper.ParseToInt(dataReader["UserID"].ToString());
                    entity.user.UserName = dataReader["UserName"].ToString();
                    entity.user.PassWord = dataReader["PassWord"].ToString();
                    entity.user.GUID = dataReader["GUID"].ToString();
                    entity.user.RealName = dataReader["RealName"].ToString();
                    entity.user.IsStop = DataHelper.ParseToBoolean(dataReader["IsStop"].ToString());
                    

                    //entity.role.ID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    //entity.role.RoleName = dataReader["RoleName"].ToString();
                    //entity.role.Description = dataReader["Description"].ToString();
                    //entity.role.IsAudit = DataHelper.ParseToBoolean(dataReader["IsAudit"].ToString());
                }
                dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select * from vUserRole where GUID='" + GUID + "'", null);
                while(dataReader.Read())
                {
                    Entity.Role role = new Entity.Role(); 
                    role.ID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    role.RoleName = dataReader["RoleName"].ToString();
                    role.Description = dataReader["Description"].ToString();
                    Roles.Add(role);
                    //entity.role.IsAudit = DataHelper.ParseToBoolean(dataReader["IsAudit"].ToString());
                }
                entity.role = Roles;
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
        /// <param name="UserID">    </param>
        /// <returns></returns>
        public static List<Entity.UserRole> GetUserRoleByUserID(int UserID)
        {
            List<Entity.UserRole> list = new List<Entity.UserRole>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserID.GetType().ToString()),UserID,32)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_UserRole_GetByUserID", prams);
                while (dataReader.Read())
                {
                    Entity.UserRole entity = new Entity.UserRole();
                    //entity.UserID = DataHelper.ParseToInt(dataReader["UserID"].ToString());
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
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
        /// <param name="UserID">    </param>
        /// <param name="RoleID">    </param>
        /// <returns></returns>
        public static int AddUserRole(Entity.UserRole userRole)
        {
            int iReturn = 0;
            int i = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [User]([UserName],[PassWord],[GUID],[RealName],[PwdChgDate],[CreateUser],[CreateDate],[UpdateUser],[UpdateDate],[IsStop]) values ('" + userRole.user.UserName + "','" + userRole.user.PassWord + "','" + userRole.user.GUID + "','" + userRole.user.RealName + "','" + userRole.user.PwdChgDate + "','" + userRole.user.CreateUser + "','" + userRole.user.CreateDate + "','" + userRole.user.UpdateUser + "','" + userRole.user.UpdateDate + "','" + userRole.user.IsStop + "')", null);
                foreach(Entity.Role role in userRole.role)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [UserRole]([UGuid],[RoleID]) values ('" + userRole.user.GUID + "','" + role.ID + "')", null);
                }
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



        public static int UpdateUserRole(Entity.UserRole userRole)
        {
            int iReturn = 0;
            int i = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[User] set UserName='" + userRole.user.UserName + "',RealName='" + userRole.user.RealName + "',UpdateUser='" + userRole.user.UpdateUser + "',UpdateDate='" + userRole.user.UpdateDate + "',IsStop='" + userRole.user.IsStop + "' where GUID='" + userRole.user.GUID + "'", null);
                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[UserRole] set RoleID='" + userRole.role.ID + "'where UGuid='" + userRole.user.GUID + "'", null);
                foreach (int a in userRole.Add)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [UserRole]([UGuid],[RoleID]) values ('" + userRole.user.GUID + "','" + a + "')", null);
                }
                foreach (int b in userRole.Delete)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [UserRole] where UGuid='" + userRole.user.GUID + "' and RoleID='" + b + "'", null);
                }
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
        /// <param name="UserID">    </param>
        /// <param name="RoleID">    </param>
        /// <returns></returns>
        public static int DeleteUserRole(int UserID, int RoleID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserID",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserID.GetType().ToString()),UserID,32),
					dbFactory.MakeInParam("@RoleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleID.GetType().ToString()),RoleID,32)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_UserRole_Delete", prams);
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


    }
}

