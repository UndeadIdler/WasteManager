using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class TransferPlan
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ContractID">    </param>
        /// <param name="PlanNumber">    </param>
        /// <param name="StartDate">    </param>
        /// <param name="EndDate">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="StatusID">    </param>
        /// <returns></returns>
        public static DataTable GetAllTransferPlan(int ContractID, string PlanNumber, DateTime StartDate, DateTime EndDate, string WasteCode, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [TransferPlan] where ContractID='" + ContractID + "' and PlanNumber='" + PlanNumber + "' and StartDate='" + StartDate + "' and EndDate='" + EndDate + "' and WasteCode='" + WasteCode + "' and StatusID='" + StatusID + "'", null);
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
        /// <param name="ContractNumber">    </param>
        /// <returns></returns>
        public static int GetPlanIDByNumber(string PlanNumber)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [TransferPlan] where PlanNumber='" + PlanNumber + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["PlanID"].ToString());
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
        /// <param name="ContractID">    </param>
        /// <returns></returns>
        public static decimal GetPlanAmount(int PlanID)
        {
            decimal iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vPlanAmount] where PlanID='" + PlanID + "'", null);
                while (dataReader.Read())
                {
                    iReturn = decimal.Parse(dataReader["Used"].ToString());
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



        public static DataTable QueryTransferPlan(string ContractNumber, string ProduceName, string StartTime, string EndTime, string WasteName, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vTransferPlan] where 1=1 ");
                if (!string.IsNullOrEmpty(ContractNumber))
                {
                    sb.Append(" and  ContractNumber like '%" + ContractNumber + "%'");
                }
                if (!string.IsNullOrEmpty(ProduceName))
                {
                    sb.Append(" and  ProduceName like '%" + ProduceName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and  WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and EndDate<='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and StartDate>='" + EndTime + "'");
                }
                if (StatusID != -2)
                {
                    sb.Append(" and StatusID='" + StatusID + "'");
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


        public static DataTable QueryTransferPlanAlarm()
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vPlanAmount] where ( EndDate>='" + DateTime.Now.Date + "' and EndDate<='" + DateTime.Now.Date.AddDays(30) + "' ) or Used > 0.9*PlanAmount");                
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


        public static DataTable QueryTransferPlanEx(string ContractNumber, string ProduceName, string StartTime, string EndTime, string WasteName, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select ContractNumber as 合同编号,PlanNumber as 计划编号,ProduceName as 企业名称,WasteName as 废物名称,StartDate as 起始日期,EndDate as 结束日期,PlanAmount as 计划数量,ApprovalDate as 批准日期,Status as 审核状态 from [vTransferPlan] where 1=1 ");
                if (!string.IsNullOrEmpty(ContractNumber))
                {
                    sb.Append(" and  ContractNumber like '%" + ContractNumber + "%'");
                }
                if (!string.IsNullOrEmpty(ProduceName))
                {
                    sb.Append(" and  ProduceName like '%" + ProduceName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and  WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and EndDate<='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and StartDate>='" + EndTime + "'");
                }
                if (StatusID != -2)
                {
                    sb.Append(" and StatusID='" + StatusID + "'");
                }
                sb.Append(" order by PlanNumber");
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
        /// <param name="PlanID">    </param>
        /// <returns></returns>
        public static Entity.TransferPlan GetTransferPlan(int PlanID)
        {
            Entity.TransferPlan entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vTransferPlan] where PlanID='" + PlanID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.TransferPlan();
                    entity.PlanID = DataHelper.ParseToInt(dataReader["PlanID"].ToString());
                    entity.ContractID = DataHelper.ParseToInt(dataReader["ContractID"].ToString());
                    entity.PlanNumber = dataReader["PlanNumber"].ToString();
                    entity.ApprovalDate = DataHelper.ParseToDate(dataReader["ApprovalDate"].ToString());
                    entity.StartDate = DataHelper.ParseToDate(dataReader["StartDate"].ToString());
                    entity.EndDate = DataHelper.ParseToDate(dataReader["EndDate"].ToString());
                    entity.PlanAmount = decimal.Parse(dataReader["PlanAmount"].ToString());
                    entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.ProduceID = DataHelper.ParseToInt(dataReader["ProduceID"].ToString());
                    entity.ProduceName = dataReader["ProduceName"].ToString();
                    entity.Remark = dataReader["Remark"].ToString();
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    entity.StatusID = DataHelper.ParseToInt(dataReader["StatusID"].ToString());
                    entity.IYear = DataHelper.ParseToInt(dataReader["IYear"].ToString());
                    entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
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
        /// <param name="PlanNumber">    </param>
        /// <param name="IYear">    </param>
        /// <returns></returns>
        public static int GetMaxNumber(string ProduceArea, int IYear)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select MAX(Number) as Number from [vTransferPlan] where ProduceArea='" + ProduceArea + "' and IYear='" + IYear + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["Number"].ToString());
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


        public static int AddTransferPlan(Entity.TransferPlan entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractID.GetType().ToString()),entity.ContractID,32),
					dbFactory.MakeInParam("@PlanNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanNumber.GetType().ToString()),entity.PlanNumber,50),
					dbFactory.MakeInParam("@ApprovalDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ApprovalDate.GetType().ToString()),entity.ApprovalDate,0),
					dbFactory.MakeInParam("@StartDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StartDate.GetType().ToString()),entity.StartDate,0),
					dbFactory.MakeInParam("@EndDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EndDate.GetType().ToString()),entity.EndDate,0),
					dbFactory.MakeInParam("@PlanAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanAmount.GetType().ToString()),entity.PlanAmount,10),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,50),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_TransferPlan_Add", prams);
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

        public static int UpdateTransferPlan(Entity.TransferPlan entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@PlanID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanID.GetType().ToString()),entity.PlanID,32),
					dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractID.GetType().ToString()),entity.ContractID,32),
					dbFactory.MakeInParam("@PlanNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanNumber.GetType().ToString()),entity.PlanNumber,50),
					dbFactory.MakeInParam("@ApprovalDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ApprovalDate.GetType().ToString()),entity.ApprovalDate,0),
					dbFactory.MakeInParam("@StartDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StartDate.GetType().ToString()),entity.StartDate,0),
					dbFactory.MakeInParam("@EndDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EndDate.GetType().ToString()),entity.EndDate,0),
					dbFactory.MakeInParam("@PlanAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanAmount.GetType().ToString()),entity.PlanAmount,10),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,50),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_TransferPlan_Update", prams);
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
        /// <param name="PlanID">    </param>
        /// <param name="StatusID">    </param>
        /// <returns></returns>
        public static int UpdateTransferPlanStatus(int PlanID, int StatusID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[TransferPlan] set StatusID='" + StatusID + "' where PlanID='" + PlanID + "'", null);
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
        /// <param name="PlanID">    </param>
        /// <returns></returns>
        public static int DeleteTransferPlan(int PlanID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [TransferPlan] where StatusID<>2 and PlanID='" + PlanID + "'", null);
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
