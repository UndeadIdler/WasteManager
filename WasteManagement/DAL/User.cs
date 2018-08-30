using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class User
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="UserName">    </param>
        /// <param name="PassWord">    </param>
        /// <param name="IsStop">    </param>
        /// <returns></returns>
        public static string Login(string UserName, string PassWord)
        {
            string iReturn = "";
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserName",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserName.GetType().ToString()),UserName,50),
					dbFactory.MakeInParam("@PassWord",	DBTypeConverter.ConvertCsTypeToOriginDBType(PassWord.GetType().ToString()),PassWord,50)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_User_Login", prams);
                if (dataReader.Read())
                {
                    iReturn = dataReader["GUID"].ToString();
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
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static Entity.User GetUserByID(int ID)
        {
            Entity.User entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [User] where ID='" + ID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.User();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.UserName = dataReader["UserName"].ToString();
                    entity.PassWord = dataReader["PassWord"].ToString();
                    entity.GUID = dataReader["GUID"].ToString();
                    entity.Department = dataReader["Department"].ToString();
                    entity.RealName = dataReader["RealName"].ToString();
                    entity.PwdChgDate = DataHelper.ParseToDate(dataReader["PwdChgDate"].ToString());
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



        /// <summary>
        ///
        /// </summary>
        /// <param name="GUID">    </param>
        /// <returns></returns>
        public static Entity.User GetUser(string GUID)
        {
            Entity.User entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [User] where GUID='" + GUID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.User();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.UserName = dataReader["UserName"].ToString();
                    entity.PassWord = dataReader["PassWord"].ToString();
                    entity.GUID = dataReader["GUID"].ToString();
                    entity.Department = dataReader["Department"].ToString();
                    entity.RealName = dataReader["RealName"].ToString();
                    entity.PwdChgDate = DataHelper.ParseToDate(dataReader["PwdChgDate"].ToString());
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



        /// <summary>
        ///
        /// </summary>
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static DataTable QueryUser(string Name,int RoleID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vUserRole] where RoleID<>6 ");
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and RealName like '%" + Name + "%'");
                }
                if (RoleID != -2)
                {
                    sb.Append(" and RoleID='" + RoleID + "'");
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
        public static DataTable QueryUserEx(string Name, int RoleID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select RealName as 姓名,Description as 类型 from [vUserRole] where RoleID<>6 ");
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and RealName like '%" + Name + "%'");
                }
                if (RoleID != -2)
                {
                    sb.Append(" and RoleID='" + RoleID + "'");
                }
                sb.Append(" order by RoleName ");
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
        /// <returns></returns>
        public static List<Entity.User> GetAllUser()
        {
            List<Entity.User> list = new List<Entity.User>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_User_GetAll", prams);
                while (dataReader.Read())
                {
                    Entity.User entity = new Entity.User();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.UserName = dataReader["UserName"].ToString();
                    entity.PassWord = dataReader["PassWord"].ToString();
                    entity.GUID = dataReader["GUID"].ToString();
                    //entity.AreaCode = dataReader["AreaCode"].ToString();
                    //entity.AreaInCharge = dataReader["AreaInCharge"].ToString();
                    entity.RealName = dataReader["RealName"].ToString();
                    entity.PwdChgDate = DataHelper.ParseToDate(dataReader["PwdChgDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    entity.IsStop = DataHelper.ParseToBoolean(dataReader["IsStop"].ToString());
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


        public static List<string> GetUserNames(int RoleID)
        {
            List<string> list = new List<string>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select RealName from [vUserRole] where IsStop=0 and RoleID='" + RoleID + "'", null);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static DataTable GetUserNamesEx(int RoleID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select UserID,RealName from [vUserRole] where IsStop=0 and RoleID='" + RoleID + "'", null);
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
        public static DataTable GetUserNamesEx2(string RoleIDs)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select UserID,RealName from [vUserRole] where IsStop=0 and RoleID in (" + RoleIDs + ")", null);
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
        /// <param name="Name">    </param>
        /// <returns></returns>
        public static int GetUserID(string Name,int RoleID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select UserID from [vUserRole] where RoleID='"+RoleID+"' and RealName='" + Name + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["UserID"].ToString());
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
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static List<Entity.Role> GetUserRole(string GUID)
        {
            List<Entity.Role> list = new List<Entity.Role>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from vUserRole where GUID='" + GUID + "'", null);
                while (dataReader.Read())
                {
                    Entity.Role entity = new Entity.Role();
                    entity.ID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    entity.RoleName = dataReader["RoleName"].ToString();
                    entity.Description = dataReader["Description"].ToString();
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
        public static List<Entity.Menu> GetMenu(string GUID)
        {
            List<Entity.Menu> list = new List<Entity.Menu>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vUserMenu] where GUID='"+GUID+"'", null);
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



        /// <summary>
        ///
        /// </summary>
        /// <param name="ID">    </param>
        /// <returns></returns>
        public static DataTable GetMenuEx(string GUID)
        {
            DataTable list = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@GUID",	DBTypeConverter.ConvertCsTypeToOriginDBType(GUID.GetType().ToString()),GUID,100)
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_User_GetMenu", prams);
                if (dataReader.Read())
                {
                    list = dataReader.GetSchemaTable();
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
        /// <param name="UserName">    </param>
        /// <param name="PassWord">    </param>
        /// <param name="GUID">    </param>
        /// <param name="AreaCode">    </param>
        /// <param name="AreaInCharge">    </param>
        /// <param name="RealName">    </param>
        /// <param name="PwdChgDate">    </param>
        /// <param name="CreateUser">    </param>
        /// <param name="CreateDate">    </param>
        /// <param name="UpdateUser">    </param>
        /// <param name="UpdateDate">    </param>
        /// <param name="IsStop">    </param>
        /// <returns></returns>
        public static int AddUser(string UserName, string PassWord, string GUID, string AreaCode, string AreaInCharge, string RealName, DateTime PwdChgDate, string CreateUser, DateTime CreateDate, string UpdateUser, DateTime UpdateDate, bool IsStop)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserName",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserName.GetType().ToString()),UserName,50),
					dbFactory.MakeInParam("@PassWord",	DBTypeConverter.ConvertCsTypeToOriginDBType(PassWord.GetType().ToString()),PassWord,50),
					dbFactory.MakeInParam("@GUID",	DBTypeConverter.ConvertCsTypeToOriginDBType(GUID.GetType().ToString()),GUID,100),
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20),
					dbFactory.MakeInParam("@AreaInCharge",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaInCharge.GetType().ToString()),AreaInCharge,100),
					dbFactory.MakeInParam("@RealName",	DBTypeConverter.ConvertCsTypeToOriginDBType(RealName.GetType().ToString()),RealName,20),
					dbFactory.MakeInParam("@PwdChgDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(PwdChgDate.GetType().ToString()),PwdChgDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateUser.GetType().ToString()),CreateUser,50),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateDate.GetType().ToString()),CreateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
					dbFactory.MakeInParam("@IsStop",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsStop.GetType().ToString()),IsStop,4),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_User_Add", prams);
                iReturn = int.Parse(prams[12].Value.ToString());
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
        /// <param name="UserName">    </param>
        /// <param name="GUID">    </param>
        /// <param name="AreaCode">    </param>
        /// <param name="AreaInCharge">    </param>
        /// <param name="RealName">    </param>
        /// <param name="UpdateUser">    </param>
        /// <param name="UpdateDate">    </param>
        /// <param name="IsStop">    </param>
        /// <returns></returns>
        public static int UpdateUser(string UserName, string GUID, string AreaCode, string AreaInCharge, string RealName, string UpdateUser, DateTime UpdateDate, bool IsStop)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@UserName",	DBTypeConverter.ConvertCsTypeToOriginDBType(UserName.GetType().ToString()),UserName,50),
					dbFactory.MakeInParam("@GUID",	DBTypeConverter.ConvertCsTypeToOriginDBType(GUID.GetType().ToString()),GUID,100),
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20),
					dbFactory.MakeInParam("@AreaInCharge",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaInCharge.GetType().ToString()),AreaInCharge,100),
					dbFactory.MakeInParam("@RealName",	DBTypeConverter.ConvertCsTypeToOriginDBType(RealName.GetType().ToString()),RealName,20),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
					dbFactory.MakeInParam("@IsStop",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsStop.GetType().ToString()),IsStop,4)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_User_Update", prams);
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
        /// <param name="PassWord">    </param>
        /// <param name="GUID">    </param>
        /// <param name="PwdChgDate">    </param>
        /// <param name="UpdateUser">    </param>
        /// <param name="UpdateDate">    </param>
        /// <returns></returns>
        public static int UpdateUserPw(string PassWord, string GUID, DateTime PwdChgDate, string UpdateUser, DateTime UpdateDate)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@PassWord",	DBTypeConverter.ConvertCsTypeToOriginDBType(PassWord.GetType().ToString()),PassWord,50),
					dbFactory.MakeInParam("@GUID",	DBTypeConverter.ConvertCsTypeToOriginDBType(GUID.GetType().ToString()),GUID,100),
					dbFactory.MakeInParam("@PwdChgDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(PwdChgDate.GetType().ToString()),PwdChgDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_User_UpdatePw", prams);
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
        /// <param name="GUID">    </param>
        /// <returns></returns>
        public static int DeleteUser(string GUID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@GUID",	DBTypeConverter.ConvertCsTypeToOriginDBType(GUID.GetType().ToString()),GUID,100)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_User_Delete", prams);
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

