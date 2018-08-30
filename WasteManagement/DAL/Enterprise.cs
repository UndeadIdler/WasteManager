using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class Enterprise
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="Name">    </param>
        /// <returns></returns>
        public static Entity.Enterprise GetEnterpriseInfo(string Name)
        {
            Entity.Enterprise entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Enterprise] where Name='" + Name + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Enterprise();
                    entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.AreaCode = dataReader["AreaCode"].ToString();
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
        /// <param name="EnterpriseID">    </param>
        /// <returns></returns>
        public static string GetEnterpriseName(int EnterpriseID)
        {
            string iReturn = string.Empty;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select Name from [Enterprise] where EnterpriseID='" + EnterpriseID + "'", null);
                while (dataReader.Read())
                {
                    iReturn = dataReader["Name"].ToString();
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
        /// <param name="Name">    </param>
        /// <returns></returns>
        public static int GetEnterpriseID(string Name)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Enterprise] where Name='" + Name + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
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
        /// <param name="Name">    </param>
        /// <param name="OrganizationCode">    </param>
        /// <param name="Type">    </param>
        /// <param name="AreaCode">    </param>
        /// <returns></returns>
        public static DataTable GetAllEnterprise(string LawManCode, string Name, string OrganizationCode, int Type, string AreaCode)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Enterprise] where 1=1 ");
                if (!string.IsNullOrEmpty(LawManCode))
                {
                    sb.Append(" and LawManCode like '%" + LawManCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( Name like '%" + Name + "%' or PastName like '%"+Name+"%')");
                }
                if (!string.IsNullOrEmpty(OrganizationCode))
                {
                    sb.Append(" and OrganizationCode like '%" + OrganizationCode + "%'");
                }
                if (Type!=-2)
                {
                    sb.Append(" and Type='" +Type + "'");
                }
                if (AreaCode!="3304")
                {
                    sb.Append(" and AreaCode ='" + AreaCode + "'");
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
        /// <param name="Name">    </param>
        /// <param name="OrganizationCode">    </param>
        /// <param name="Type">    </param>
        /// <param name="AreaCode">    </param>
        /// <returns></returns>
        public static DataTable GetAllEnterpriseEx(string LawManCode, string Name, string OrganizationCode, int Type, string AreaCode)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Enterprise] where 1=1 ");
                if (!string.IsNullOrEmpty(LawManCode))
                {
                    sb.Append(" and LawManCode like '%" + LawManCode + "%'");
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    sb.Append(" and ( Name like '%" + Name + "%' or PastName like '%" + Name + "%')");
                }
                if (!string.IsNullOrEmpty(OrganizationCode))
                {
                    sb.Append(" and OrganizationCode like '%" + OrganizationCode + "%'");
                }
                if (Type != -2)
                {
                    sb.Append(" and Type='" + Type + "'");
                }
                if (AreaCode != "3304")
                {
                    sb.Append(" and AreaCode ='" + AreaCode + "'");
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



        public static DataTable GetPartEnterprise(int Type)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [Enterprise] where 1=1 ");
                if (Type != -2)
                {
                    sb.Append(" and Type='" + Type + "'");
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
        /// <param name="Type">    </param>
        /// <returns></returns>
        public static List<string> GetEnterpriseNames(int Type)
        {
            List<string> list = new List<string>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select Name from [Enterprise] where Type='" + Type + "'", null);
                while (dataReader.Read())
                {
                    list.Add(dataReader["Name"].ToString());
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
        /// <param name="EnterpriseID">    </param>
        /// <returns></returns>
        public static Entity.Enterprise GetEnterprise(int EnterpriseID)
        {
            Entity.Enterprise entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Enterprise] where EnterpriseID='" + EnterpriseID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Enterprise();
                    entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
                    entity.Name = dataReader["Name"].ToString();
                    entity.LawManCode = dataReader["LawManCode"].ToString();
                    entity.OrganizationCode = dataReader["OrganizationCode"].ToString();
                    entity.PastName = dataReader["PastName"].ToString();
                    entity.SetUpDate = DataHelper.ParseToDate(dataReader["SetUpDate"].ToString());
                    entity.Type = DataHelper.ParseToInt(dataReader["Type"].ToString());
                    entity.FaxNumber = dataReader["FaxNumber"].ToString();
                    entity.Industry = DataHelper.ParseToInt(dataReader["Industry"].ToString());
                    entity.AreaCode = dataReader["AreaCode"].ToString();
                    entity.PostCode = dataReader["PostCode"].ToString();
                    entity.Address = dataReader["Address"].ToString();
                    entity.LawMan = dataReader["LawMan"].ToString();
                    entity.Email = dataReader["Email"].ToString();
                    entity.Phone1 = dataReader["Phone1"].ToString();
                    entity.Telphone1 = dataReader["Telphone1"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
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


        public static int AddEnterprise(Entity.Enterprise entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Name.GetType().ToString()),entity.Name,50),
					dbFactory.MakeInParam("@LawManCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LawManCode.GetType().ToString()),entity.LawManCode,20),
					dbFactory.MakeInParam("@OrganizationCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrganizationCode.GetType().ToString()),entity.OrganizationCode,20),
					dbFactory.MakeInParam("@PastName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PastName.GetType().ToString()),entity.PastName,50),
					dbFactory.MakeInParam("@SetUpDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SetUpDate.GetType().ToString()),entity.SetUpDate,0),
					dbFactory.MakeInParam("@Type",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Type.GetType().ToString()),entity.Type,32),
					dbFactory.MakeInParam("@FaxNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FaxNumber.GetType().ToString()),entity.FaxNumber,20),
					dbFactory.MakeInParam("@Industry",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Industry.GetType().ToString()),entity.Industry,32),
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AreaCode.GetType().ToString()),entity.AreaCode,20),
					dbFactory.MakeInParam("@PostCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PostCode.GetType().ToString()),entity.PostCode,20),
					dbFactory.MakeInParam("@Address",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Address.GetType().ToString()),entity.Address,80),
					dbFactory.MakeInParam("@LawMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LawMan.GetType().ToString()),entity.LawMan,10),
					dbFactory.MakeInParam("@Email",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Email.GetType().ToString()),entity.Email,50),
					dbFactory.MakeInParam("@Phone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Phone1.GetType().ToString()),entity.Phone1,20),
					dbFactory.MakeInParam("@Telphone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Telphone1.GetType().ToString()),entity.Telphone1,20),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Enterprise_Add", prams);
                iReturn = int.Parse(prams[19].Value.ToString());
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

        public static int UpdateEnterprise(Entity.Enterprise entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EnterpriseID.GetType().ToString()),entity.EnterpriseID,32),
					dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Name.GetType().ToString()),entity.Name,50),
					dbFactory.MakeInParam("@LawManCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LawManCode.GetType().ToString()),entity.LawManCode,20),
					dbFactory.MakeInParam("@OrganizationCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.OrganizationCode.GetType().ToString()),entity.OrganizationCode,20),
					dbFactory.MakeInParam("@PastName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PastName.GetType().ToString()),entity.PastName,50),
					dbFactory.MakeInParam("@SetUpDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SetUpDate.GetType().ToString()),entity.SetUpDate,0),
					dbFactory.MakeInParam("@Type",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Type.GetType().ToString()),entity.Type,32),
					dbFactory.MakeInParam("@FaxNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FaxNumber.GetType().ToString()),entity.FaxNumber,20),
					dbFactory.MakeInParam("@Industry",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Industry.GetType().ToString()),entity.Industry,32),
					dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.AreaCode.GetType().ToString()),entity.AreaCode,20),
					dbFactory.MakeInParam("@PostCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PostCode.GetType().ToString()),entity.PostCode,20),
					dbFactory.MakeInParam("@Address",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Address.GetType().ToString()),entity.Address,80),
					dbFactory.MakeInParam("@LawMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.LawMan.GetType().ToString()),entity.LawMan,10),
					dbFactory.MakeInParam("@Email",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Email.GetType().ToString()),entity.Email,50),
					dbFactory.MakeInParam("@Phone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Phone1.GetType().ToString()),entity.Phone1,20),
					dbFactory.MakeInParam("@Telphone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Telphone1.GetType().ToString()),entity.Telphone1,20),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Enterprise_Update", prams);
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
        /// <param name="EnterpriseID">    </param>
        /// <returns></returns>
        public static int DeleteEnterprise(int EnterpriseID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Enterprise] where EnterpriseID='" + EnterpriseID + "'", null);
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




        #region
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="Name">    </param>
        ///// <param name="LawManCode">    </param>
        ///// <param name="PastName">    </param>
        ///// <param name="SetUpDate">    </param>
        ///// <param name="FaxNumber">    </param>
        ///// <param name="Industry">    </param>
        ///// <param name="AreaCode">    </param>
        ///// <param name="PostCode">    </param>
        ///// <param name="Address">    </param>
        ///// <param name="LawMan">    </param>
        ///// <param name="Email">    </param>
        ///// <param name="Phone1">    </param>
        ///// <param name="Telphone1">    </param>
        ///// <param name="EnvironmentManage">    </param>
        ///// <param name="Phone2">    </param>
        ///// <param name="Telphone2">    </param>
        ///// <param name="EnvironmentalMan">    </param>
        ///// <param name="Phone3">    </param>
        ///// <param name="Telphone3">    </param>
        ///// <param name="Status">    </param>
        ///// <param name="IsWater">    </param>
        ///// <param name="WaterTime">    </param>
        ///// <param name="SewageID">    </param>
        ///// <param name="IsHeat">    </param>
        ///// <param name="HeatTime">    </param>
        ///// <param name="PowerID">    </param>
        ///// <param name="EmissionRightID">    </param>
        ///// <param name="CreateDate">    </param>
        ///// <param name="CreateUser">    </param>
        ///// <param name="UpdateDate">    </param>
        ///// <param name="UpdateUser">    </param>
        ///// <param name="IsArea">    </param>
        ///// <returns></returns>
        //public static int AddEnterprise(string Name, string LawManCode, string PastName, DateTime SetUpDate, string FaxNumber, int Industry, string AreaCode, string PostCode, string Address, string LawMan, string Email, string Phone1, string Telphone1, string EnvironmentManage, string Phone2, string Telphone2, string EnvironmentalMan, string Phone3, string Telphone3, int Status, int IsWater, DateTime WaterTime, int SewageID, int IsHeat, DateTime HeatTime, int PowerID, string SewageWarrantID, DateTime CreateDate, string CreateUser, DateTime UpdateDate, string UpdateUser, int IsArea)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(Name.GetType().ToString()),Name,50),
        //            dbFactory.MakeInParam("@LawManCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(LawManCode.GetType().ToString()),LawManCode,20),
        //            dbFactory.MakeInParam("@PastName",	DBTypeConverter.ConvertCsTypeToOriginDBType(PastName.GetType().ToString()),PastName,50),
        //            dbFactory.MakeInParam("@SetUpDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(SetUpDate.GetType().ToString()),SetUpDate,0),
        //            dbFactory.MakeInParam("@FaxNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(FaxNumber.GetType().ToString()),FaxNumber,20),
        //            dbFactory.MakeInParam("@Industry",	DBTypeConverter.ConvertCsTypeToOriginDBType(Industry.GetType().ToString()),Industry,32),
        //            dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20),
        //            dbFactory.MakeInParam("@PostCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(PostCode.GetType().ToString()),PostCode,20),
        //            dbFactory.MakeInParam("@Address",	DBTypeConverter.ConvertCsTypeToOriginDBType(Address.GetType().ToString()),Address,80),
        //            dbFactory.MakeInParam("@LawMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(LawMan.GetType().ToString()),LawMan,10),
        //            dbFactory.MakeInParam("@Email",	DBTypeConverter.ConvertCsTypeToOriginDBType(Email.GetType().ToString()),Email,50),
        //            dbFactory.MakeInParam("@Phone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone1.GetType().ToString()),Phone1,20),
        //            dbFactory.MakeInParam("@Telphone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone1.GetType().ToString()),Telphone1,20),
        //            dbFactory.MakeInParam("@EnvironmentManage",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnvironmentManage.GetType().ToString()),EnvironmentManage,10),
        //            dbFactory.MakeInParam("@Phone2",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone2.GetType().ToString()),Phone2,20),
        //            dbFactory.MakeInParam("@Telphone2",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone2.GetType().ToString()),Telphone2,20),
        //            dbFactory.MakeInParam("@EnvironmentalMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnvironmentalMan.GetType().ToString()),EnvironmentalMan,10),
        //            dbFactory.MakeInParam("@Phone3",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone3.GetType().ToString()),Phone3,20),
        //            dbFactory.MakeInParam("@Telphone3",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone3.GetType().ToString()),Telphone3,20),
        //            dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(Status.GetType().ToString()),Status,32),
        //            dbFactory.MakeInParam("@IsWater",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsWater.GetType().ToString()),IsWater,32),
        //            dbFactory.MakeInParam("@WaterTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(WaterTime.GetType().ToString()),WaterTime,0),
        //            dbFactory.MakeInParam("@SewageID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SewageID.GetType().ToString()),SewageID,32),
        //            dbFactory.MakeInParam("@IsHeat",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsHeat.GetType().ToString()),IsHeat,32),
        //            dbFactory.MakeInParam("@HeatTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(HeatTime.GetType().ToString()),HeatTime,0),
        //            dbFactory.MakeInParam("@PowerID",	DBTypeConverter.ConvertCsTypeToOriginDBType(PowerID.GetType().ToString()),PowerID,32),
        //            dbFactory.MakeInParam("@SewageWarrantID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SewageWarrantID.GetType().ToString()),SewageWarrantID,20),
        //            dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateDate.GetType().ToString()),CreateDate,0),
        //            dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateUser.GetType().ToString()),CreateUser,50),
        //            dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
        //            dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
        //            dbFactory.MakeInParam("@IsArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsArea.GetType().ToString()),IsArea,32),
        //            dbFactory.MakeOutReturnParam()
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Enterprise_AddEx", prams);
        //        iReturn = int.Parse(prams[32].Value.ToString());
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
        ///// <param name="EnterpriseID">    </param>
        ///// <returns></returns>
        //public static int DeleteEnterprise(int EnterpriseID)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnterpriseID.GetType().ToString()),EnterpriseID,32)
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Enterprise_Delete", prams);
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
        ///// <param name="EnterpriseID">    </param>
        ///// <param name="Name">    </param>
        ///// <param name="LawManCode">    </param>
        ///// <param name="PastName">    </param>
        ///// <param name="SetUpDate">    </param>
        ///// <param name="FaxNumber">    </param>
        ///// <param name="Industry">    </param>
        ///// <param name="AreaCode">    </param>
        ///// <param name="PostCode">    </param>
        ///// <param name="Address">    </param>
        ///// <param name="LawMan">    </param>
        ///// <param name="Email">    </param>
        ///// <param name="Phone1">    </param>
        ///// <param name="Telphone1">    </param>
        ///// <param name="EnvironmentManage">    </param>
        ///// <param name="Phone2">    </param>
        ///// <param name="Telphone2">    </param>
        ///// <param name="EnvironmentalMan">    </param>
        ///// <param name="Phone3">    </param>
        ///// <param name="Telphone3">    </param>
        ///// <param name="Status">    </param>
        ///// <param name="IsWater">    </param>
        ///// <param name="WaterTime">    </param>
        ///// <param name="SewageID">    </param>
        ///// <param name="IsHeat">    </param>
        ///// <param name="HeatTime">    </param>
        ///// <param name="PowerID">    </param>
        ///// <param name="SewageWarrantID">    </param>
        ///// <param name="UpdateDate">    </param>
        ///// <param name="UpdateUser">    </param>
        ///// <param name="IsArea">    </param>
        ///// <returns></returns>
        //public static int UpdateEnterprise(int EnterpriseID, string Name, string LawManCode, string PastName, DateTime SetUpDate, string FaxNumber, int Industry, string AreaCode, string PostCode, string Address, string LawMan, string Email, string Phone1, string Telphone1, string EnvironmentManage, string Phone2, string Telphone2, string EnvironmentalMan, string Phone3, string Telphone3, int Status, int IsWater, DateTime WaterTime, int SewageID, int IsHeat, DateTime HeatTime, int PowerID, string SewageWarrantID, DateTime UpdateDate, string UpdateUser, int IsArea)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnterpriseID.GetType().ToString()),EnterpriseID,32),
        //            dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(Name.GetType().ToString()),Name,50),
        //            dbFactory.MakeInParam("@LawManCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(LawManCode.GetType().ToString()),LawManCode,20),
        //            dbFactory.MakeInParam("@PastName",	DBTypeConverter.ConvertCsTypeToOriginDBType(PastName.GetType().ToString()),PastName,50),
        //            dbFactory.MakeInParam("@SetUpDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(SetUpDate.GetType().ToString()),SetUpDate,0),
        //            dbFactory.MakeInParam("@FaxNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(FaxNumber.GetType().ToString()),FaxNumber,20),
        //            dbFactory.MakeInParam("@Industry",	DBTypeConverter.ConvertCsTypeToOriginDBType(Industry.GetType().ToString()),Industry,32),
        //            dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20),
        //            dbFactory.MakeInParam("@PostCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(PostCode.GetType().ToString()),PostCode,20),
        //            dbFactory.MakeInParam("@Address",	DBTypeConverter.ConvertCsTypeToOriginDBType(Address.GetType().ToString()),Address,80),
        //            dbFactory.MakeInParam("@LawMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(LawMan.GetType().ToString()),LawMan,10),
        //            dbFactory.MakeInParam("@Email",	DBTypeConverter.ConvertCsTypeToOriginDBType(Email.GetType().ToString()),Email,50),
        //            dbFactory.MakeInParam("@Phone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone1.GetType().ToString()),Phone1,20),
        //            dbFactory.MakeInParam("@Telphone1",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone1.GetType().ToString()),Telphone1,20),
        //            dbFactory.MakeInParam("@EnvironmentManage",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnvironmentManage.GetType().ToString()),EnvironmentManage,10),
        //            dbFactory.MakeInParam("@Phone2",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone2.GetType().ToString()),Phone2,20),
        //            dbFactory.MakeInParam("@Telphone2",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone2.GetType().ToString()),Telphone2,20),
        //            dbFactory.MakeInParam("@EnvironmentalMan",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnvironmentalMan.GetType().ToString()),EnvironmentalMan,10),
        //            dbFactory.MakeInParam("@Phone3",	DBTypeConverter.ConvertCsTypeToOriginDBType(Phone3.GetType().ToString()),Phone3,20),
        //            dbFactory.MakeInParam("@Telphone3",	DBTypeConverter.ConvertCsTypeToOriginDBType(Telphone3.GetType().ToString()),Telphone3,20),
        //            dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(Status.GetType().ToString()),Status,32),
        //            dbFactory.MakeInParam("@IsWater",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsWater.GetType().ToString()),IsWater,32),
        //            dbFactory.MakeInParam("@WaterTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(WaterTime.GetType().ToString()),WaterTime,0),
        //            dbFactory.MakeInParam("@SewageID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SewageID.GetType().ToString()),SewageID,32),
        //            dbFactory.MakeInParam("@IsHeat",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsHeat.GetType().ToString()),IsHeat,32),
        //            dbFactory.MakeInParam("@HeatTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(HeatTime.GetType().ToString()),HeatTime,0),
        //            dbFactory.MakeInParam("@PowerID",	DBTypeConverter.ConvertCsTypeToOriginDBType(PowerID.GetType().ToString()),PowerID,32),
        //            dbFactory.MakeInParam("@SewageWarrantID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SewageWarrantID.GetType().ToString()),SewageWarrantID,20),
        //            dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
        //            dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
        //            dbFactory.MakeInParam("@IsArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsArea.GetType().ToString()),IsArea,32)
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Enterprise_Update", prams);
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
        ///// <param name="Name">    </param>
        ///// <param name="AreaCode">    </param>
        ///// <returns></returns>
        //public static int GetEnterprise(string Name, string AreaCode)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@Name",	DBTypeConverter.ConvertCsTypeToOriginDBType(Name.GetType().ToString()),Name,50),
        //            dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20)
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Enterprise_Get", prams);
        //        while (dataReader.Read())
        //        {
        //            iReturn = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
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
        ///// <param name="EnterpriseID">    </param>
        ///// <returns></returns>
        //public static Entity.Enterprise GetEnterpriseEx(int EnterpriseID)
        //{
        //    Entity.Enterprise entity = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            //dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnterpriseID.GetType().ToString()),EnterpriseID,32)
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vEnterprise] where EnterpriseID='" + EnterpriseID + "'", null);
        //        while (dataReader.Read())
        //        {
        //            entity = new Entity.Enterprise();
        //            entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
        //            entity.Name = dataReader["Name"].ToString();
        //            entity.LawManCode = dataReader["LawManCode"].ToString();
        //            entity.PastName = dataReader["PastName"].ToString();
        //            entity.SetUpDate = DataHelper.ParseToDate(dataReader["SetUpDate"].ToString());
        //            entity.FaxNumber = dataReader["FaxNumber"].ToString();
        //            entity.Industry = DataHelper.ParseToInt(dataReader["Industry"].ToString());
        //            //需要修改
        //            entity.IndustryName = dataReader["Industry"].ToString();
        //            entity.AreaCode = dataReader["AreaCode"].ToString();
        //            entity.AreaName = dataReader["AreaName"].ToString();
        //            entity.PostCode = dataReader["PostCode"].ToString();
        //            entity.Address = dataReader["Address"].ToString();
        //            entity.LawMan = dataReader["LawMan"].ToString();
        //            entity.Email = dataReader["Email"].ToString();
        //            entity.Phone1 = dataReader["Phone1"].ToString();
        //            entity.Telphone1 = dataReader["Telphone1"].ToString();
        //            entity.EnvironmentManage = dataReader["EnvironmentManage"].ToString();
        //            entity.Phone2 = dataReader["Phone2"].ToString();
        //            entity.Telphone2 = dataReader["Telphone2"].ToString();
        //            entity.EnvironmentalMan = dataReader["EnvironmentalMan"].ToString();
        //            entity.Phone3 = dataReader["Phone3"].ToString();
        //            entity.Telphone3 = dataReader["Telphone3"].ToString();
        //            entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
        //            entity.IsWater = DataHelper.ParseToInt(dataReader["IsWater"].ToString());
        //            entity.WaterTime = DataHelper.ParseToDate(dataReader["WaterTime"].ToString());
        //            entity.SewageID = DataHelper.ParseToInt(dataReader["SewageID"].ToString());
        //            entity.IsHeat = DataHelper.ParseToInt(dataReader["IsHeat"].ToString());
        //            entity.HeatTime = DataHelper.ParseToDate(dataReader["HeatTime"].ToString());
        //            entity.PowerID = DataHelper.ParseToInt(dataReader["PowerID"].ToString());
        //            entity.SewageWarrantID = dataReader["SewageWarrantID"].ToString();
        //            entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
        //            entity.CreateUser = dataReader["CreateUser"].ToString();
        //            entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
        //            entity.UpdateUser = dataReader["UpdateUser"].ToString();
        //            entity.IsArea = DataHelper.ParseToInt(dataReader["IsArea"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return entity;
        //}



        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="AreaCode">    </param>
        ///// <param name="IsArea">    </param>
        ///// <returns></returns>
        //public static Entity.Enterprise GetEnterpriseEx2(string AreaCode, int IsArea)
        //{
        //    Entity.Enterprise entity  = new Entity.Enterprise();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@AreaCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(AreaCode.GetType().ToString()),AreaCode,20),
        //            dbFactory.MakeInParam("@IsArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsArea.GetType().ToString()),IsArea,32)
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Enterprise_GetEx", prams);
        //        while (dataReader.Read())
        //        {                    
        //            entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
        //            entity.Name = dataReader["Name"].ToString();
        //            entity.LawManCode = dataReader["LawManCode"].ToString();
        //            entity.PastName = dataReader["PastName"].ToString();
        //            entity.SetUpDate = DataHelper.ParseToDate(dataReader["SetUpDate"].ToString());
        //            entity.FaxNumber = dataReader["FaxNumber"].ToString();
        //            entity.Industry = DataHelper.ParseToInt(dataReader["Industry"].ToString());
        //            entity.AreaCode = dataReader["AreaCode"].ToString();
        //            entity.PostCode = dataReader["PostCode"].ToString();
        //            entity.Address = dataReader["Address"].ToString();
        //            entity.LawMan = dataReader["LawMan"].ToString();
        //            entity.Email = dataReader["Email"].ToString();
        //            entity.Phone1 = dataReader["Phone1"].ToString();
        //            entity.Telphone1 = dataReader["Telphone1"].ToString();
        //            entity.EnvironmentManage = dataReader["EnvironmentManage"].ToString();
        //            entity.Phone2 = dataReader["Phone2"].ToString();
        //            entity.Telphone2 = dataReader["Telphone2"].ToString();
        //            entity.EnvironmentalMan = dataReader["EnvironmentalMan"].ToString();
        //            entity.Phone3 = dataReader["Phone3"].ToString();
        //            entity.Telphone3 = dataReader["Telphone3"].ToString();
        //            entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
        //            entity.IsWater = DataHelper.ParseToInt(dataReader["IsWater"].ToString());
        //            entity.WaterTime = DataHelper.ParseToDate(dataReader["WaterTime"].ToString());
        //            entity.SewageID = DataHelper.ParseToInt(dataReader["SewageID"].ToString());
        //            entity.IsHeat = DataHelper.ParseToInt(dataReader["IsHeat"].ToString());
        //            entity.HeatTime = DataHelper.ParseToDate(dataReader["HeatTime"].ToString());
        //            entity.PowerID = DataHelper.ParseToInt(dataReader["PowerID"].ToString());
        //            entity.SewageWarrantID = dataReader["SewageWarrantID"].ToString();
        //            entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
        //            entity.CreateUser = dataReader["CreateUser"].ToString();
        //            entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
        //            entity.UpdateUser = dataReader["UpdateUser"].ToString();
        //            entity.IsArea = DataHelper.ParseToInt(dataReader["IsArea"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        db.Conn.Close();
        //    }
        //    return entity;
        //}

        #endregion

    }
}
