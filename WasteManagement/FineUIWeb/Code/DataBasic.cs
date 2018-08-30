using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace WasteManagement
{
    public class DataBasic
    {
        public DataBasic()
        {

        }

        #region 用户权限

        public string GetUserGuid(string username, string password)
        {
            string guid = string.Empty;
            //string sSql = "select id,UserID,t_R_UserInfo.RoleID,Name,PWDModifyTime,LevelID,ReadRight,WriteRight,RefreshRight,id from t_R_UserInfo inner join t_R_Role on t_R_Role.RoleID=t_R_UserInfo.RoleID where UserID='" + txt_UserName.Text.Trim() + "' and PWD='" + txt_Pwd.Text.Trim() + "'";

            string sSql = string.Format("select GUID from [User] where UserName = '{0}' and PassWord = '{1}'", username, password);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            if (sDs.Tables[0].Rows.Count == 1)
            {
                guid = sDs.Tables[0].Rows[0][0].ToString();
            }
            return guid;
        }

        //public DataSet GetUserTree(string userguid)
        //{
        //    string sSql = string.Format("select * from View_Ac_UserMenu where btUserMenuShow = 'True' and vUserGuid = '{0}' order by vMenuOrder", userguid);
        //    DataSet sDs = new MyDataOp().CreateDataSet(sSql);

        //    return sDs;
        //}

        public DataSet GetUserTree(string userguid)
        {
            string sSql = string.Format("select * from vUserMenu where  Guid = '{0}' order by MenuOrder", userguid);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);

            return sDs;
        }

        public int iGetUserTree(string userguid)
        {
            int num = 0;
            string sSql = string.Format("select count(*) from View_Ac_UserMenu where vUserGuid = '{0}'", userguid);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            if (sDs != null && sDs.Tables[0].Rows.Count > 0)
            {
                num = int.Parse(sDs.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public int iGetUserRole(string userguid)
        {
            int num = 0;
            string sSql = string.Format("select count(*) from View_Ac_UserRole where vUserGuid = '{0}'", userguid);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            if (sDs != null && sDs.Tables[0].Rows.Count > 0)
            {
                num = int.Parse(sDs.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public DataSet GetUserRole(string userguid)
        {
            string sSql = string.Format("select * from View_Ac_UserRole where vUserGuid = '{0}' order by vMenuOrder", userguid);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);

            return sDs;
        }

        public bool blBeWrite(string pagename, string userguid)
        {
            bool blSuccess = false;
            string sSql = string.Format("select btUserMenuWrite from View_Ac_UserRole where vUserGuid = '{0}' and vMenuFile like '%{1}%'", userguid, pagename);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            if (sDs != null && sDs.Tables[0].Rows.Count > 0)
            {
                if (sDs.Tables[0].Rows[0][0].ToString() == "True") blSuccess = true;
            }
            return blSuccess;
        }

        public DataSet GetMenuTree()
        {
            string sSql = string.Format("select * from Menu");
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);

            return sDs;
        }

        public bool ChangeUserPassword(string sUserGuid, string sNewPwd)
        {
            bool blSuccess = false;
            MyDataOp mdo = new MyDataOp();
            string sql = string.Format("update [User] set PassWord = '{0}',PwdChgDate=GetDate() where GUID = '{1}'", sUserGuid, sNewPwd);
            blSuccess = mdo.ExecuteCommand(sql);

            return blSuccess;
        }

        public bool Insert_Ac_Users(object[] values)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("insert Ac_Users(vUserName, vPassWord, vUserGuid, vUserRole, dPwdChgDate) values('{0}','{1}','{2}','{3}','{4}')", values[0], values[1], values[2], values[3], values[4]);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool Delete_Ac_Users(string vUserGuid)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("delete from User where Guid = '{0}'", vUserGuid);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool CreateUserTree(string vUserGuid)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("insert into Ac_UserMenu(vUserGuid,iMenuId) select '{0}',id from Ac_Menus", vUserGuid);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool UnAllCheckUserTree(string vUserGuid)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("update Ac_UserMenu set btShow = 'False' where vUserGuid = '{0}'", vUserGuid);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool SaveCheckedUserTree(string vUserGuid, string idstr)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("update Ac_UserMenu set btShow = 'True' where vUserGuid = '{0}' and iMenuId in ({1})", vUserGuid, idstr);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }


        public bool CreateUserRole(string vUserGuid)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("insert into Ac_UserRole(vUserGuid,iMenuId) select '{0}',id from Ac_Menus", vUserGuid);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool UnAllCheckUserRole(string vUserGuid)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("update Ac_UserRole set btWrite = 'False' where vUserGuid = '{0}'", vUserGuid);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        public bool SaveCheckedUserRole(string vUserGuid, string idstr)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sSql = string.Format("update Ac_UserRole set btWrite = 'True' where vUserGuid = '{0}' and iMenuId in ({1})", vUserGuid, idstr);
            success = mdo.ExecuteCommand(sSql);

            return success;
        }

        #endregion

        #region 获取数据
        public DataTable GetDataTable(string[] Element)
        {
            DataTable newDT = new DataTable("DataTable");

            string sSql = string.Format("select {3} * from {0} where 1=1 {1} {2}", Element[0], Element[1], Element[2], Element[3]);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            if (sDs.Tables[0].Rows.Count > 0) newDT = sDs.Tables[0];

            return newDT;
        }

        public DataSet GetDataSet(string[] Element)
        {
            string sSql = string.Format("select {3} * from {0} where 1=1 {1} {2}", Element[0], Element[1], Element[2], Element[3]);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            return sDs;
        }

        public DataSet GetDistinctDataSet(string[] Element)
        {

            string sSql = string.Format("select  distinct {1} from {0} order by {1}", Element[0], Element[1]);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            return sDs;
        }


        public DataSet GetDistinctAreaDataSet(string AreaInCharge)
        {

            string sSql = string.Format("select  distinct area_id,area_jc from T_AreaInfo where  area_jc in ({0}) order by area_id,area_jc", AreaInCharge);
            DataSet sDs = new MyDataOp().CreateDataSet(sSql);
            return sDs;
        }
        #endregion

        #region 删除数据
        public bool Delete_Data_Mul(object[] values)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sql = string.Format("delete from {0} where {1} in ({2})", values[0], values[1], values[2]);
            success = mdo.ExecuteCommand(sql);

            return success;
        }

        public bool Delete_Data(object[] values)
        {
            bool success = false;
            MyDataOp mdo = new MyDataOp();
            string sql = string.Format("delete from {0} where {1} = '{2}'", values[0], values[1], values[2]);
            success = mdo.ExecuteCommand(sql);

            return success;
        }

        #endregion

    }
}