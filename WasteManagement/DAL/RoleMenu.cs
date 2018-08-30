using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class RoleMenu
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static List<Entity.RoleMenu> GetAllRoleMenu()
        {
            List<Entity.RoleMenu> list = new List<Entity.RoleMenu>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_RoleMenu_GetAll", prams);
                while (dataReader.Read())
                {
                    Entity.RoleMenu entity = new Entity.RoleMenu();
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    //entity.MenuID = DataHelper.ParseToInt(dataReader["MenuID"].ToString());
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
        public static Entity.RoleMenu GetRoleMenu(string RoleID)
        {
            Entity.RoleMenu roleMenu = new Entity.RoleMenu();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
				};
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from vRoleMenu where RoleID='" + RoleID + "'", null);
                while (dataReader.Read())
                {
                    roleMenu.role.RoleName = dataReader["RoleName"].ToString();
                    roleMenu.role.Description = dataReader["Description"].ToString();
                    //roleMenu.role.IsAudit = bool.Parse(dataReader["IsAudit"].ToString());
                    Entity.Menu entity = new Entity.Menu();
                    //entity.RoleID = DataHelper.ParseToInt(dataReader["RoleID"].ToString());
                    entity.ID = DataHelper.ParseToInt(dataReader["MenuID"].ToString());
                    entity.MenuName = dataReader["MenuName"].ToString();
                    entity.MenuUrl = dataReader["MenuUrl"].ToString();
                    roleMenu.menuList.Add(entity);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return roleMenu;
        }




        /// <summary>
        ///
        /// </summary>
        /// <param name="RoleID">    </param>
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static int AddRoleMenu(Entity.RoleMenu roleMenu)
        {
            int iReturn = 0;
            int i = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleName",	DBTypeConverter.ConvertCsTypeToOriginDBType(roleMenu.role.RoleName.GetType().ToString()),roleMenu.role.RoleName,20),
					dbFactory.MakeInParam("@Description",	DBTypeConverter.ConvertCsTypeToOriginDBType(roleMenu.role.Description.GetType().ToString()),roleMenu.role.Description,0),
                    //dbFactory.MakeInParam("@IsAudit",	DBTypeConverter.ConvertCsTypeToOriginDBType(roleMenu.role.IsAudit.GetType().ToString()),roleMenu.role.IsAudit,4),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Role_Add", prams);
                i = int.Parse(prams[2].Value.ToString());

                if (i > 0)
                {
                    roleMenu.role.ID = i;
                }
                if (i == 0)
                {
                    return 0;
                }
                for (int s = 0; s < roleMenu.menuList.Count; s++)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [RoleMenu]([RoleID],[MenuID]) values ('" + roleMenu.role.ID + "','" + roleMenu.menuList[s].ID + "')", null);
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
        /// <param name="RoleID">    </param>
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static int UpdateRoleMenu(Entity.RoleMenu roleMenu)
        {
            int iReturn = 0;
            int i = 0;
            DBOperatorBase db = new DataBase();

            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {

                //iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Role] set RoleName='" + roleMenu.role.RoleName + "',Description='" + roleMenu.role.Description + "',IsAudit='" + roleMenu.role.IsAudit + "'where ID='" + roleMenu.role.ID + "'", null);

                for (int s = 0; s < roleMenu.NewAdd.Count; s++)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [RoleMenu]([RoleID],[MenuID]) values ('" + roleMenu.role.ID + "','" + roleMenu.NewAdd[s].ToString() + "')", null);
                }
                for (int t = 0; t < roleMenu.Delete.Count; t++)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [RoleMenu] where RoleID='" + roleMenu.role.ID + "' and MenuID='" + roleMenu.Delete[t].ToString() + "'", null);
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
        /// <param name="RoleID">    </param>
        /// <param name="MenuID">    </param>
        /// <returns></returns>
        public static int DeleteRoleMenu(int RoleID, int MenuID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@RoleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(RoleID.GetType().ToString()),RoleID,32),
					dbFactory.MakeInParam("@MenuID",	DBTypeConverter.ConvertCsTypeToOriginDBType(MenuID.GetType().ToString()),MenuID,32)
				};
                iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_RoleMenu_Delete", prams);
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
