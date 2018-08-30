using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class Contract
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ContractID">    </param>
        /// <returns></returns>
        public static Entity.Contract GetContract(int ContractID)
        {
            Entity.Contract entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vContract] where ContractID='" + ContractID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.Contract();
                    entity.ContractID = DataHelper.ParseToInt(dataReader["ContractID"].ToString());
                    entity.ContractNumber = dataReader["ContractNumber"].ToString();
                    entity.SignDate = DataHelper.ParseToDate(dataReader["SignDate"].ToString());
                    entity.ProduceID = DataHelper.ParseToInt(dataReader["ProduceID"].ToString());
                    entity.ProduceArea = dataReader["ProduceArea"].ToString();			
                    entity.HandleID = DataHelper.ParseToInt(dataReader["HandleID"].ToString());
                    entity.StartDate = DataHelper.ParseToDate(dataReader["StartDate"].ToString());
                    entity.EndDate = DataHelper.ParseToDate(dataReader["EndDate"].ToString());
                    entity.Amount = decimal.Parse(dataReader["Amount"].ToString());
                    entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.Total = decimal.Parse(dataReader["Total"].ToString());
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


        //待修改的
        /// <summary>
        ///
        /// </summary>
        /// <param name="ContractNumber">    </param>
        /// <param name="SignDate">    </param>
        /// <param name="ProduceID">    </param>
        /// <param name="StartDate">    </param>
        /// <param name="EndDate">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="StatusID">    </param>
        /// <returns></returns>
        public static DataTable GetAllContract(string ContractNumber, DateTime SignDate, int ProduceID, DateTime StartDate, DateTime EndDate, string WasteCode, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Contract] where ContractNumber='" + ContractNumber + "' and SignDate='" + SignDate + "' and ProduceID='" + ProduceID + "' and StartDate='" + StartDate + "' and EndDate='" + EndDate + "' and WasteCode='" + WasteCode + "' and StatusID='" + StatusID + "'", null);
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
        /// <param name="SignDate">    </param>
        /// <param name="ProduceID">    </param>
        /// <param name="StartDate">    </param>
        /// <param name="EndDate">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="StatusID">    </param>
        /// <returns></returns>
        public static DataTable QueryContract(string ContractNumber, string ProduceName, string StartTime, string EndTime, string WasteName, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vContract] where 1=1 ");
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
                    sb.Append(" and StartDate>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and EndDate<='" + EndTime + "'");
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


        public static DataTable QueryContract2(string ContractNumber, string ProduceName, string StartTime, string EndTime, string WasteName, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vContract] where 1=1 ");
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
                    sb.Append(" and StartDate>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and EndDate<='" + EndTime + "'");
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


        /// <summary>
        ///
        /// </summary>
        /// <param name="ContractNumber">    </param>
        /// <returns></returns>
        public static int GetContractIDByNumber(string ContractNumber)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Contract] where ContractNumber='" + ContractNumber + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["ContractID"].ToString());
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
        /// <param name="ContractNumber">    </param>
        /// <param name="SignDate">    </param>
        /// <param name="ProduceID">    </param>
        /// <param name="StartDate">    </param>
        /// <param name="EndDate">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="StatusID">    </param>
        /// <returns></returns>
        public static DataTable QueryContractEx(string ContractNumber, string ProduceName, string StartTime, string EndTime, string WasteName, int StatusID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select ContractNumber as 合同编号,SignDate as 签约日期,ProduceName as 企业名称,ProduceArea 行政区域代码,WasteName as 废物名称,StartDate as 起始日期, EndDate as 结束日期,Amount as 合同数量,StatusName as 审核状态 from [vContract] where 1=1 ");
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
                    sb.Append(" and StartDate>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and EndDate<='" + EndTime + "'");
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


        /// <summary>
        ///
        /// </summary>
        /// <param name="ContractID">    </param>
        /// <returns></returns>
        public static decimal GetContractAmount(int ContractID)
        {
            decimal iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vContractAmount] where ContractID='" + ContractID + "'", null);
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



        public static int AddContract(Entity.Contract entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractNumber.GetType().ToString()),entity.ContractNumber,50),
					dbFactory.MakeInParam("@SignDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SignDate.GetType().ToString()),entity.SignDate,0),
					dbFactory.MakeInParam("@ProduceID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProduceID.GetType().ToString()),entity.ProduceID,32),
					dbFactory.MakeInParam("@ProduceArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProduceArea.GetType().ToString()),entity.ProduceArea,20),
					dbFactory.MakeInParam("@HandleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HandleID.GetType().ToString()),entity.HandleID,32),
					dbFactory.MakeInParam("@StartDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StartDate.GetType().ToString()),entity.StartDate,0),
					dbFactory.MakeInParam("@EndDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EndDate.GetType().ToString()),entity.EndDate,0),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,50),
					dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Total.GetType().ToString()),entity.Total,10),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Contract_Add", prams);
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


        public static int UpdateContract(Entity.Contract entity)
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
					dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractNumber.GetType().ToString()),entity.ContractNumber,50),
					dbFactory.MakeInParam("@SignDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SignDate.GetType().ToString()),entity.SignDate,0),
					dbFactory.MakeInParam("@ProduceID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProduceID.GetType().ToString()),entity.ProduceID,32),
					dbFactory.MakeInParam("@ProduceArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProduceArea.GetType().ToString()),entity.ProduceArea,20),
					dbFactory.MakeInParam("@HandleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HandleID.GetType().ToString()),entity.HandleID,32),
					dbFactory.MakeInParam("@StartDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StartDate.GetType().ToString()),entity.StartDate,0),
					dbFactory.MakeInParam("@EndDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EndDate.GetType().ToString()),entity.EndDate,0),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,50),
					dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Total.GetType().ToString()),entity.Total,10),
					dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
					dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
					dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Contract_Update", prams);
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


        public static int UpdateContractStatus(Entity.Contract entity)
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
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Contract_UpdateStatus", prams);
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
        /// <param name="ContractID">    </param>
        /// <returns></returns>
        public static int DeleteContract(int ContractID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [Contract] where StatusID<>2 and ContractID='" + ContractID + "'", null);
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
        ///// <param name="EnterpriseID">    </param>
        ///// <param name="ContractNumber">    </param>
        ///// <param name="TradeDate">    </param>
        ///// <param name="BuyID">    </param>
        ///// <param name="BuyArea">    </param>
        ///// <param name="TradeType">    </param>
        ///// <param name="ProjectName">    </param>
        ///// <param name="ProjectInfo">    </param>
        ///// <param name="SourceType">    </param>
        ///// <param name="SaleID">    </param>
        ///// <param name="SaleArea">    </param>
        ///// <param name="TransactionAmount">    </param>
        ///// <param name="Fee">    </param>
        ///// <param name="Total">    </param>
        ///// <param name="IsInstallment">    </param>
        ///// <param name="Paid">    </param>
        ///// <param name="Unpaid">    </param>
        ///// <param name="Remark">    </param>
        ///// <param name="CreateUser">    </param>
        ///// <param name="CreateDate">    </param>
        ///// <param name="StatusID">    </param>
        ///// <param name="CalculateAmount">    </param>
        ///// <param name="IYear">    </param>
        ///// <param name="Number">    </param>
        ///// <returns></returns>
        //public static int AddContract(int EnterpriseID, string ContractNumber, DateTime TradeDate, int BuyID, string BuyArea, int TradeType, string ProjectName, string ProjectInfo, int SourceType, int SaleID, string SaleArea, double TransactionAmount, double Fee, double Total, int IsInstallment, double Paid, double Unpaid, string Remark, string CreateUser, DateTime CreateDate, int StatusID, double CalculateAmount, int IYear, int Number)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnterpriseID.GetType().ToString()),EnterpriseID,32),
        //            dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractNumber.GetType().ToString()),ContractNumber,50),
        //            dbFactory.MakeInParam("@TradeDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(TradeDate.GetType().ToString()),TradeDate,0),
        //            dbFactory.MakeInParam("@BuyID",	DBTypeConverter.ConvertCsTypeToOriginDBType(BuyID.GetType().ToString()),BuyID,32),
        //            dbFactory.MakeInParam("@BuyArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(BuyArea.GetType().ToString()),BuyArea,20),
        //            dbFactory.MakeInParam("@TradeType",	DBTypeConverter.ConvertCsTypeToOriginDBType(TradeType.GetType().ToString()),TradeType,32),
        //            dbFactory.MakeInParam("@ProjectName",	DBTypeConverter.ConvertCsTypeToOriginDBType(ProjectName.GetType().ToString()),ProjectName,50),
        //            dbFactory.MakeInParam("@ProjectInfo",	DBTypeConverter.ConvertCsTypeToOriginDBType(ProjectInfo.GetType().ToString()),ProjectInfo,0),
        //            dbFactory.MakeInParam("@SourceType",	DBTypeConverter.ConvertCsTypeToOriginDBType(SourceType.GetType().ToString()),SourceType,32),
        //            dbFactory.MakeInParam("@SaleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SaleID.GetType().ToString()),SaleID,32),
        //            dbFactory.MakeInParam("@SaleArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(SaleArea.GetType().ToString()),SaleArea,20),
        //            dbFactory.MakeInParam("@TransactionAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(TransactionAmount.GetType().ToString()),TransactionAmount,64),
        //            dbFactory.MakeInParam("@Fee",	DBTypeConverter.ConvertCsTypeToOriginDBType(Fee.GetType().ToString()),Fee,64),
        //            dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(Total.GetType().ToString()),Total,64),
        //            dbFactory.MakeInParam("@IsInstallment",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsInstallment.GetType().ToString()),IsInstallment,32),
        //            dbFactory.MakeInParam("@Paid",	DBTypeConverter.ConvertCsTypeToOriginDBType(Paid.GetType().ToString()),Paid,64),
        //            dbFactory.MakeInParam("@Unpaid",	DBTypeConverter.ConvertCsTypeToOriginDBType(Unpaid.GetType().ToString()),Unpaid,64),
        //            dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(Remark.GetType().ToString()),Remark,0),
        //            dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateUser.GetType().ToString()),CreateUser,50),
        //            dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(CreateDate.GetType().ToString()),CreateDate,0),
        //            dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(StatusID.GetType().ToString()),StatusID,32),
        //            dbFactory.MakeInParam("@CalculateAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(CalculateAmount.GetType().ToString()),CalculateAmount,64),
        //            dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(IYear.GetType().ToString()),IYear,32),
        //            dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(Number.GetType().ToString()),Number,32),
        //            dbFactory.MakeOutReturnParam()
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Contract_Add", prams);
        //        iReturn = int.Parse(prams[24].Value.ToString());
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



        //public static int AddContract(Entity.Contract entity)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
        //    IDbTransaction trans = thelper.StartTransaction();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EnterpriseID.GetType().ToString()),entity.EnterpriseID,32),
        //            dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractNumber.GetType().ToString()),entity.ContractNumber,50),
        //            dbFactory.MakeInParam("@TradeDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TradeDate.GetType().ToString()),entity.TradeDate,0),
        //            dbFactory.MakeInParam("@BuyID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BuyID.GetType().ToString()),entity.BuyID,32),
        //            dbFactory.MakeInParam("@BuyArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BuyArea.GetType().ToString()),entity.BuyArea,20),
        //            dbFactory.MakeInParam("@TradeType",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TradeType.GetType().ToString()),entity.TradeType,32),
        //            dbFactory.MakeInParam("@ProjectName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProjectName.GetType().ToString()),entity.ProjectName,50),
        //            dbFactory.MakeInParam("@ProjectInfo",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProjectInfo.GetType().ToString()),entity.ProjectInfo,0),
        //            dbFactory.MakeInParam("@SourceType",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SourceType.GetType().ToString()),entity.SourceType,32),
        //            dbFactory.MakeInParam("@SaleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SaleID.GetType().ToString()),entity.SaleID,32),
        //            dbFactory.MakeInParam("@SaleArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SaleArea.GetType().ToString()),entity.SaleArea,20),
        //            dbFactory.MakeInParam("@TransactionAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TransactionAmount.GetType().ToString()),entity.TransactionAmount,10),
        //            dbFactory.MakeInParam("@Fee",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Fee.GetType().ToString()),entity.Fee,10),
        //            dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Total.GetType().ToString()),entity.Total,10),
        //            dbFactory.MakeInParam("@IsInstallment",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsInstallment.GetType().ToString()),entity.IsInstallment,32),
        //            dbFactory.MakeInParam("@Paid",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Paid.GetType().ToString()),entity.Paid,10),
        //            dbFactory.MakeInParam("@Unpaid",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unpaid.GetType().ToString()),entity.Unpaid,10),
        //            dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
        //            dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
        //            dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
        //            dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
        //            dbFactory.MakeInParam("@CalculateAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CalculateAmount.GetType().ToString()),entity.CalculateAmount,10),
        //            dbFactory.MakeInParam("@IYear",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IYear.GetType().ToString()),entity.IYear,32),
        //            dbFactory.MakeInParam("@Number",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Number.GetType().ToString()),entity.Number,32),
        //            dbFactory.MakeOutReturnParam()
        //        };
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Contract_Add", prams);
        //        iReturn = int.Parse(prams[24].Value.ToString());
        //        thelper.CommitTransaction(trans);
        //        iReturn = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        thelper.RollTransaction(trans);
        //        iReturn = 0;

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
        ///// <param name="ContractID">    </param>
        ///// <param name="EnterpriseID">    </param>
        ///// <param name="ContractNumber">    </param>
        ///// <param name="TradeDate">    </param>
        ///// <param name="BuyID">    </param>
        ///// <param name="BuyArea">    </param>
        ///// <param name="TradeType">    </param>
        ///// <param name="ProjectName">    </param>
        ///// <param name="ProjectInfo">    </param>
        ///// <param name="SourceType">    </param>
        ///// <param name="SaleID">    </param>
        ///// <param name="SaleArea">    </param>
        ///// <param name="TransactionAmount">    </param>
        ///// <param name="Fee">    </param>
        ///// <param name="Total">    </param>
        ///// <param name="IsInstallment">    </param>
        ///// <param name="Paid">    </param>
        ///// <param name="Unpaid">    </param>
        ///// <param name="Remark">    </param>
        ///// <param name="UpdateUser">    </param>
        ///// <param name="UpdateDate">    </param>
        ///// <param name="StatusID">    </param>
        ///// <param name="CalculateAmount">    </param>
        ///// <returns></returns>
        //public static int UpdateContract(int ContractID, int EnterpriseID, string ContractNumber, DateTime TradeDate, int BuyID, string BuyArea, int TradeType, string ProjectName, string ProjectInfo, int SourceType, int SaleID, string SaleArea, double TransactionAmount, double Fee, double Total, int IsInstallment, double Paid, double Unpaid, string Remark, string UpdateUser, DateTime UpdateDate, int StatusID, double CalculateAmount)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractID.GetType().ToString()),ContractID,32),
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(EnterpriseID.GetType().ToString()),EnterpriseID,32),
        //            dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractNumber.GetType().ToString()),ContractNumber,50),
        //            dbFactory.MakeInParam("@TradeDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(TradeDate.GetType().ToString()),TradeDate,0),
        //            dbFactory.MakeInParam("@BuyID",	DBTypeConverter.ConvertCsTypeToOriginDBType(BuyID.GetType().ToString()),BuyID,32),
        //            dbFactory.MakeInParam("@BuyArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(BuyArea.GetType().ToString()),BuyArea,20),
        //            dbFactory.MakeInParam("@TradeType",	DBTypeConverter.ConvertCsTypeToOriginDBType(TradeType.GetType().ToString()),TradeType,32),
        //            dbFactory.MakeInParam("@ProjectName",	DBTypeConverter.ConvertCsTypeToOriginDBType(ProjectName.GetType().ToString()),ProjectName,50),
        //            dbFactory.MakeInParam("@ProjectInfo",	DBTypeConverter.ConvertCsTypeToOriginDBType(ProjectInfo.GetType().ToString()),ProjectInfo,0),
        //            dbFactory.MakeInParam("@SourceType",	DBTypeConverter.ConvertCsTypeToOriginDBType(SourceType.GetType().ToString()),SourceType,32),
        //            dbFactory.MakeInParam("@SaleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(SaleID.GetType().ToString()),SaleID,32),
        //            dbFactory.MakeInParam("@SaleArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(SaleArea.GetType().ToString()),SaleArea,20),
        //            dbFactory.MakeInParam("@TransactionAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(TransactionAmount.GetType().ToString()),TransactionAmount,64),
        //            dbFactory.MakeInParam("@Fee",	DBTypeConverter.ConvertCsTypeToOriginDBType(Fee.GetType().ToString()),Fee,64),
        //            dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(Total.GetType().ToString()),Total,64),
        //            dbFactory.MakeInParam("@IsInstallment",	DBTypeConverter.ConvertCsTypeToOriginDBType(IsInstallment.GetType().ToString()),IsInstallment,32),
        //            dbFactory.MakeInParam("@Paid",	DBTypeConverter.ConvertCsTypeToOriginDBType(Paid.GetType().ToString()),Paid,64),
        //            dbFactory.MakeInParam("@Unpaid",	DBTypeConverter.ConvertCsTypeToOriginDBType(Unpaid.GetType().ToString()),Unpaid,64),
        //            dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(Remark.GetType().ToString()),Remark,0),
        //            dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
        //            dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
        //            dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(StatusID.GetType().ToString()),StatusID,32),
        //            dbFactory.MakeInParam("@CalculateAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(CalculateAmount.GetType().ToString()),CalculateAmount,64)
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Contract_Update", prams);
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


        //public static int UpdateContract(Entity.Contract entity)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
        //    IDbTransaction trans = thelper.StartTransaction();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractID.GetType().ToString()),entity.ContractID,32),
        //            dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EnterpriseID.GetType().ToString()),entity.EnterpriseID,32),
        //            dbFactory.MakeInParam("@ContractNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ContractNumber.GetType().ToString()),entity.ContractNumber,50),
        //            dbFactory.MakeInParam("@TradeDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TradeDate.GetType().ToString()),entity.TradeDate,0),
        //            dbFactory.MakeInParam("@BuyID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BuyID.GetType().ToString()),entity.BuyID,32),
        //            dbFactory.MakeInParam("@BuyArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BuyArea.GetType().ToString()),entity.BuyArea,20),
        //            dbFactory.MakeInParam("@TradeType",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TradeType.GetType().ToString()),entity.TradeType,32),
        //            dbFactory.MakeInParam("@ProjectName",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProjectName.GetType().ToString()),entity.ProjectName,50),
        //            dbFactory.MakeInParam("@ProjectInfo",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ProjectInfo.GetType().ToString()),entity.ProjectInfo,0),
        //            dbFactory.MakeInParam("@SourceType",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SourceType.GetType().ToString()),entity.SourceType,32),
        //            dbFactory.MakeInParam("@SaleID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SaleID.GetType().ToString()),entity.SaleID,32),
        //            dbFactory.MakeInParam("@SaleArea",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.SaleArea.GetType().ToString()),entity.SaleArea,20),
        //            dbFactory.MakeInParam("@TransactionAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.TransactionAmount.GetType().ToString()),entity.TransactionAmount,10),
        //            dbFactory.MakeInParam("@Fee",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Fee.GetType().ToString()),entity.Fee,10),
        //            dbFactory.MakeInParam("@Total",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Total.GetType().ToString()),entity.Total,10),
        //            dbFactory.MakeInParam("@IsInstallment",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.IsInstallment.GetType().ToString()),entity.IsInstallment,32),
        //            dbFactory.MakeInParam("@Paid",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Paid.GetType().ToString()),entity.Paid,10),
        //            dbFactory.MakeInParam("@Unpaid",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Unpaid.GetType().ToString()),entity.Unpaid,10),
        //            dbFactory.MakeInParam("@Remark",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Remark.GetType().ToString()),entity.Remark,0),
        //            dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
        //            dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
        //            dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StatusID.GetType().ToString()),entity.StatusID,32),
        //            dbFactory.MakeInParam("@CalculateAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CalculateAmount.GetType().ToString()),entity.CalculateAmount,10)
        //        };
        //        iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_Contract_Update", prams);
        //        thelper.CommitTransaction(trans);
        //        iReturn = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        thelper.RollTransaction(trans);
        //        iReturn = 0;

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
        ///// <param name="ContractID">    </param>
        ///// <param name="UpdateUser">    </param>
        ///// <param name="UpdateDate">    </param>
        ///// <param name="ExamineUser">    </param>
        ///// <param name="ExamineDate">    </param>
        ///// <param name="StatusID">    </param>
        ///// <returns></returns>
        //public static int UpdateContractStatus(int ContractID, string UpdateUser, DateTime UpdateDate, string ExamineUser, DateTime ExamineDate, int StatusID)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractID.GetType().ToString()),ContractID,32),
        //            dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateUser.GetType().ToString()),UpdateUser,50),
        //            dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(UpdateDate.GetType().ToString()),UpdateDate,0),
        //            dbFactory.MakeInParam("@ExamineUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(ExamineUser.GetType().ToString()),ExamineUser,50),
        //            dbFactory.MakeInParam("@ExamineDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(ExamineDate.GetType().ToString()),ExamineDate,0),
        //            dbFactory.MakeInParam("@StatusID",	DBTypeConverter.ConvertCsTypeToOriginDBType(StatusID.GetType().ToString()),StatusID,32)
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Contract_UpdateStatus", prams);
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
        ///// <param name="ContractID">    </param>
        ///// <returns></returns>
        //public static int DeleteContract(int ContractID)
        //{
        //    int iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractID.GetType().ToString()),ContractID,32)
        //        };
        //        iReturn = db.ExecuteNonQuery(dbFactory.GetConnection(Config.con), true, CommandType.StoredProcedure, "proc_Contract_Delete", prams);
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
        ///// <param name="ContractID">    </param>
        ///// <returns></returns>
        //public static Entity.Contract GetContract(int ContractID)
        //{
        //    Entity.Contract entity = null;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //            dbFactory.MakeInParam("@ContractID",	DBTypeConverter.ConvertCsTypeToOriginDBType(ContractID.GetType().ToString()),ContractID,32)
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Contract_GetByID", prams);
        //        while (dataReader.Read())
        //        {
        //            entity = new Entity.Contract();
        //            entity.ContractID = DataHelper.ParseToInt(dataReader["ContractID"].ToString());
        //            entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
        //            entity.ContractNumber = dataReader["ContractNumber"].ToString();
        //            entity.TradeDate = DataHelper.ParseToDate(dataReader["TradeDate"].ToString());
        //            entity.BuyID = DataHelper.ParseToInt(dataReader["BuyID"].ToString());
        //            entity.BuyArea = dataReader["BuyArea"].ToString();
        //            entity.TradeType = DataHelper.ParseToInt(dataReader["TradeType"].ToString());
        //            entity.ProjectName = dataReader["ProjectName"].ToString();
        //            entity.ProjectInfo = dataReader["ProjectInfo"].ToString();
        //            entity.SourceType = DataHelper.ParseToInt(dataReader["SourceType"].ToString());
        //            entity.SaleID = DataHelper.ParseToInt(dataReader["SaleID"].ToString());
        //            entity.SaleArea = dataReader["SaleArea"].ToString();
        //            entity.TransactionAmount = decimal.Parse(dataReader["TransactionAmount"].ToString());
        //            entity.Fee = decimal.Parse(dataReader["Fee"].ToString());
        //            entity.Total = decimal.Parse(dataReader["Total"].ToString());
        //            entity.IsInstallment = DataHelper.ParseToInt(dataReader["IsInstallment"].ToString());
        //            entity.Paid = decimal.Parse(dataReader["Paid"].ToString());
        //            entity.Unpaid = decimal.Parse(dataReader["Unpaid"].ToString());
        //            entity.Remark = dataReader["Remark"].ToString();
        //            entity.CreateUser = dataReader["CreateUser"].ToString();
        //            entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
        //            entity.UpdateUser = dataReader["UpdateUser"].ToString();
        //            entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
        //            entity.ExamineUser = dataReader["ExamineUser"].ToString();
        //            entity.ExamineDate = DataHelper.ParseToDate(dataReader["ExamineDate"].ToString());
        //            entity.StatusID = DataHelper.ParseToInt(dataReader["StatusID"].ToString());
        //            entity.CalculateAmount = decimal.Parse(dataReader["CalculateAmount"].ToString());
        //            entity.IYear = DataHelper.ParseToInt(dataReader["IYear"].ToString());
        //            entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
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
        ///// <returns></returns>
        //public static List<Entity.Contract> GetAllContract()
        //{
        //    List<Entity.Contract> list = new List<Entity.Contract>();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Contract_GetAll", prams);
        //        while (dataReader.Read())
        //        {
        //            Entity.Contract entity = new Entity.Contract();
        //            entity.ContractID = DataHelper.ParseToInt(dataReader["ContractID"].ToString());
        //            entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
        //            entity.ContractNumber = dataReader["ContractNumber"].ToString();
        //            entity.TradeDate = DataHelper.ParseToDate(dataReader["TradeDate"].ToString());
        //            entity.BuyID = DataHelper.ParseToInt(dataReader["BuyID"].ToString());
        //            entity.BuyArea = dataReader["BuyArea"].ToString();
        //            entity.TradeType = DataHelper.ParseToInt(dataReader["TradeType"].ToString());
        //            entity.ProjectName = dataReader["ProjectName"].ToString();
        //            entity.ProjectInfo = dataReader["ProjectInfo"].ToString();
        //            entity.SourceType = DataHelper.ParseToInt(dataReader["SourceType"].ToString());
        //            entity.SaleID = DataHelper.ParseToInt(dataReader["SaleID"].ToString());
        //            entity.SaleArea = dataReader["SaleArea"].ToString();
        //            entity.TransactionAmount = decimal.Parse(dataReader["TransactionAmount"].ToString());
        //            entity.Fee = decimal.Parse(dataReader["Fee"].ToString());
        //            entity.Total = decimal.Parse(dataReader["Total"].ToString());
        //            entity.IsInstallment = DataHelper.ParseToInt(dataReader["IsInstallment"].ToString());
        //            entity.Paid = decimal.Parse(dataReader["Paid"].ToString());
        //            entity.Unpaid = decimal.Parse(dataReader["Unpaid"].ToString());
        //            entity.Remark = dataReader["Remark"].ToString();
        //            entity.CreateUser = dataReader["CreateUser"].ToString();
        //            entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
        //            entity.UpdateUser = dataReader["UpdateUser"].ToString();
        //            entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
        //            entity.ExamineUser = dataReader["ExamineUser"].ToString();
        //            entity.ExamineDate = DataHelper.ParseToDate(dataReader["ExamineDate"].ToString());
        //            entity.StatusID = DataHelper.ParseToInt(dataReader["StatusID"].ToString());
        //            entity.CalculateAmount = decimal.Parse(dataReader["CalculateAmount"].ToString());
        //            entity.IYear = DataHelper.ParseToInt(dataReader["IYear"].ToString());
        //            entity.Number = DataHelper.ParseToInt(dataReader["Number"].ToString());
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
        //public static DataTable GetAllContractEx()
        //{
        //    DataTable dt = new DataTable();
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDbDataParameter[] prams = {
        //        };
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.StoredProcedure, "proc_Contract_GetAll", prams);
        //        if (dataReader.Read())
        //        {
        //            dt = dataReader.GetSchemaTable();
        //        }
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
        #endregion


    }
}
