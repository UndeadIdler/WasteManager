using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class WasteStorage
    {
        public static DataTable QueryWasteStorage(string PondName, string WasteName, string StartTime, string EndTime, string ProduceName, int Status, string BillNumber, string PlanNumber)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vWasteStorage] where 1=1 ");
                if (!string.IsNullOrEmpty(BillNumber))
                {
                    sb.Append(" and BillNumber like '%" + BillNumber + "%'");
                }
                if (!string.IsNullOrEmpty(PlanNumber))
                {
                    sb.Append(" and PlanNumber like '%" + PlanNumber + "%'");
                }
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (!string.IsNullOrEmpty(ProduceName))
                {
                    sb.Append(" and ProduceName like '%" + ProduceName + "%'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
                }
                sb.Append(" order by DateTime desc");
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


        public static DataTable QueryWasteStorageEx(string PondName, string WasteName, string StartTime, string EndTime, string ProduceName, int Status, string BillNumber, string PlanNumber)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select BillNumber as 联单号,PlanNumber as 计划编号,DateTime 日期,ProduceName as 企业名称,ProduceArea as 行政区域代码,WasteName as 废物名称,Amount as 数量,CarNumber 车牌号,RealName as 驾驶员,PondName as 罐池号,ReceiverName as 签收人,StatusName as 状态 from [vWasteStorage] where 1=1 ");
                if (!string.IsNullOrEmpty(BillNumber))
                {
                    sb.Append(" and BillNumber like '%" + BillNumber + "%'");
                }
                if (!string.IsNullOrEmpty(PlanNumber))
                {
                    sb.Append(" and PlanNumber like '%" + PlanNumber + "%'");
                }
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and WasteName like '%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (!string.IsNullOrEmpty(ProduceName))
                {
                    sb.Append(" and ProduceName like '%" + ProduceName + "%'");
                }
                if (Status != -2)
                {
                    sb.Append(" and Status='" + Status + "'");
                }
                sb.Append(" order by DateTime desc");
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


        public static DataTable QueryWasteStorageEx2(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select BillNumber as 联单号,PlanNumber as 计划编号,DateTime 日期,ProduceName as 企业名称,ProduceArea as 行政区域代码,WasteName as 废物名称,Amount as 数量,CarNumber 车牌号,RealName as 驾驶员,PondName as 罐池号,ReceiverName as 签收人,StatusName as 状态 from [vWasteStorage] where Status>=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }                
                //sb.Append(" order by DateTime desc");
                sb.Append(" order by DateTime");
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
        public static int GetWSIDByNumber(string BillNumber)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [WasteStorage] where BillNumber='" + BillNumber + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["StorageID"].ToString());
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
        /// <param name="PlanID">    </param>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetAllWasteStorage(int PlanID, string BillNumber, DateTime DateTime, int EnterpriseID, string WasteCode, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [WasteStorage] where PlanID='" + PlanID + "' and BillNumber='" + BillNumber + "' and DateTime='" + DateTime + "' and EnterpriseID='" + EnterpriseID + "' and WasteCode='" + WasteCode + "' and Status='" + Status + "'", null);
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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetSum(string StartTime, string EndTime, int IsByProduceName)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select sum(Amount) as Total,WasteName ");
                if (IsByProduceName == 1)
                {
                    sb.Append(",ProduceName");
                }
                sb.Append(" from vWasteStorage where Status>=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by ");


                if (IsByProduceName ==1)
                {
                    sb.Append(" ProduceName,WasteName");
                    sb.Append(" order by ProduceName");
                }
                else
                {
                    sb.Append(" WasteName");
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
        /// <param name="PlanID">    </param>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetSumEx(string StartTime, string EndTime, int IsByProduceName)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ");
                if (IsByProduceName == 1)
                {
                    sb.Append(" ProduceName 产生单位,");
                }
                sb.Append("WasteName 废物种类,sum(Amount) as 合计 ");
                sb.Append(" from vWasteStorage where Status>=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by ");


                if (IsByProduceName == 1)
                {
                    sb.Append(" ProduceName,WasteName");
                    sb.Append(" order by 产生单位");
                }
                else
                {
                    sb.Append(" WasteName");
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
        /// <param name="StorageID">    </param>
        /// <returns></returns>
        public static Entity.WasteStorage GetWasteStorage(int StorageID)
        {
            Entity.WasteStorage entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [vWasteStorage] where StorageID='" + StorageID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.WasteStorage();
                    entity.StorageID = DataHelper.ParseToInt(dataReader["StorageID"].ToString());
                    entity.PlanNumber = dataReader["PlanNumber"].ToString();
                    entity.PlanID = DataHelper.ParseToInt(dataReader["PlanID"].ToString());
                    entity.BillNumber = dataReader["BillNumber"].ToString();
                    entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
                    entity.ProduceName = dataReader["ProduceName"].ToString();
                    entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.Amount = decimal.Parse(dataReader["Amount"].ToString());
                    entity.DriverID = DataHelper.ParseToInt(dataReader["DriverID"].ToString());
                    entity.RealName= dataReader["RealName"].ToString();
                    entity.CarID = DataHelper.ParseToInt(dataReader["CarID"].ToString());
                    entity.CarNumber = dataReader["CarNumber"].ToString();
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.PondName = dataReader["PondName"].ToString();
                    entity.ReceiverID = DataHelper.ParseToInt(dataReader["ReceiverID"].ToString());
                    entity.ReceiverName = dataReader["ReceiverName"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
                    entity.StatusName = dataReader["StatusName"].ToString();
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
        /// <param name="StorageID">    </param>
        /// <returns></returns>
        public static int GetWasteStorageIDByBillNumber(string BillNumber)
        {
            int iReturn=0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [WasteStorage] where BillNumber='" + BillNumber + "'", null);
                while (dataReader.Read())
                {
                    iReturn = DataHelper.ParseToInt(dataReader["StorageID"].ToString());
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
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static List<Entity.WasteStorage> GetSumWasteStorage()
        {
            List<Entity.WasteStorage> list = new List<Entity.WasteStorage>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vWasteStorage where Status>=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    Entity.WasteStorage entity = new Entity.WasteStorage();
                    //entity.StorageID = DataHelper.ParseToInt(dataReader["StorageID"].ToString());
                    //entity.PlanID = DataHelper.ParseToInt(dataReader["PlanID"].ToString());
                    //entity.BillNumber = dataReader["BillNumber"].ToString();
                    //entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    //entity.EnterpriseID = DataHelper.ParseToInt(dataReader["EnterpriseID"].ToString());
                    //entity.WasteCode = dataReader["WasteCode"].ToString();
                    entity.WasteName = dataReader["WasteName"].ToString();
                    entity.Amount = decimal.Parse(dataReader["Total"].ToString());
                    //entity.DriverID = DataHelper.ParseToInt(dataReader["DriverID"].ToString());
                    //entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    //entity.ReceiverID = DataHelper.ParseToInt(dataReader["ReceiverID"].ToString());
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    //entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
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
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumWasteStorage(string WasteName)
        {
            decimal sum=0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vWasteStorage where Status>=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' and WasteName='"+WasteName+"' group by WasteName", null);
                while (dataReader.Read())
                {
                    sum = decimal.Parse(dataReader["Total"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return sum;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumWasteStorageEx(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                //string Start = string.Format("{0}-01-01", DateTime.Now.Date);
                //string End = string.Format("{0}-12-31", DateTime.Now.Date);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(Amount) as Total,WasteName from vWasteStorage where Status>=2 and DateTime='" + DateTime.Now.Date + "' and WasteName='" + WasteName + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    sum = decimal.Parse(dataReader["Total"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return sum;
        }

        public static int AddWasteStorage(Entity.WasteStorage entity)
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
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EnterpriseID.GetType().ToString()),entity.EnterpriseID,32),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@DriverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DriverID.GetType().ToString()),entity.DriverID,32),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CarID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarID.GetType().ToString()),entity.CarID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteStorage_Add", prams);
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

        public static int UpdateWasteStorage(Entity.WasteStorage entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@StorageID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.StorageID.GetType().ToString()),entity.StorageID,32),
					dbFactory.MakeInParam("@PlanID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PlanID.GetType().ToString()),entity.PlanID,32),
					dbFactory.MakeInParam("@BillNumber",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.BillNumber.GetType().ToString()),entity.BillNumber,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@EnterpriseID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.EnterpriseID.GetType().ToString()),entity.EnterpriseID,32),
					dbFactory.MakeInParam("@WasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.WasteCode.GetType().ToString()),entity.WasteCode,20),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Amount.GetType().ToString()),entity.Amount,10),
					dbFactory.MakeInParam("@DriverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DriverID.GetType().ToString()),entity.DriverID,32),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.PondID.GetType().ToString()),entity.PondID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@CarID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CarID.GetType().ToString()),entity.CarID,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteStorage_Update", prams);
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


        public static int PassWasteStorage(Entity.WasteStorage entity, decimal Used)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[WasteStorage] set UpdateDate='" + entity.UpdateDate + "',UpdateUser='" + entity.UpdateUser + "',Status='" + entity.Status + "'where StorageID='" + entity.StorageID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.PondID + "','" + Used + "','1','废酸入库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + Used + "'where PondID='" + entity.PondID + "'", null);
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
        /// <param name="StorageID">    </param>
        /// <param name="UpdateDate">    </param>
        /// <param name="UpdateUser">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int UpdateWasteStorageStatus(int StorageID, DateTime UpdateDate, string UpdateUser, int Status)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[WasteStorage] set UpdateDate='" + UpdateDate + "',UpdateUser='" + UpdateUser + "',Status='" + Status + "'where StorageID='" + StorageID + "'", null);
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


        public static int DeleteWasteStorage(int StorageID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [WasteStorage] where Status<>2 and StorageID='" + StorageID + "'", null);
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
