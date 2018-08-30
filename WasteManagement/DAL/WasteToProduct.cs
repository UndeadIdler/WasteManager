using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public static class WasteToProduct
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="FromPondID">    </param>
        /// <param name="FromWasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetAllWasteToProduct(int FromPondID, string FromWasteCode, int Status)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [WasteToProduct] where FromPondID='" + FromPondID + "' and FromWasteCode='" + FromWasteCode + "' and Status='" + Status + "'", null);
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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable QueryWasteToProduct(string PondName, string WasteName, string StartTime, string EndTime, int HandleManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select * from [vWasteToProduct] where 1=1 ");
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and  PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(WasteName))
                {
                    sb.Append(" and  WasteName like'%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (HandleManID != -2)
                {
                    sb.Append(" and HandleManID='" + HandleManID + "'");
                }
                //IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                //DataSet ds = new DataSet();
                //dataAdapter.Fill(ds);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
                List<Entity.Waste> items = DAL.Waste.GetPartWasteEx(2);
                foreach (Entity.Waste item in items)
                {
                    DataColumn dc = new DataColumn(item.WasteCode);
                    DataColumn dc2 = new DataColumn(item.WasteCode+"Pond");
                    dt.Columns.Add(dc);
                    dt.Columns.Add(dc2);
                }
                //List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
                //foreach (Entity.Waste item in itemss)
                //{
                //    DataColumn dc = new DataColumn(item.WasteCode);
                //    dt.Columns.Add(dc);
                //}

                //List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                //foreach (Entity.AnalysisItem item in items)
                //{
                //    DataColumn dc = new DataColumn(item.ItemName);
                //    dt.Columns.Add(dc);
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.ProductDetail> list = DAL.ProductDetail.GetProductDetailEx(int.Parse(dr["DealID"].ToString()));
                    if (list.Count > 0)
                    {
                        foreach (Entity.ProductDetail entity in list)
                        {
                            dr[entity.ItemCode] = entity.Amount.ToString();
                            dr[entity.ItemCode + "Pond"] = entity.Name;
                        }
                    }
                    //List<Entity.FinalWaste> list2 = DAL.FinalWaste.GetFinalWaste(int.Parse(dr["DealID"].ToString()));
                    //if (list2.Count > 0)
                    //{
                    //    foreach (Entity.FinalWaste entity in list2)
                    //    {
                    //        dr[entity.ItemCode] = entity.Result.ToString();
                    //    }
                    //}
                }

                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                //dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
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
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="AnalysisManID">    </param>
        /// <returns></returns>
        public static DataTable QueryWasteToProductEx(string PondName, string WasteName, string StartTime, string EndTime, int HandleManID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select DealID, DateTime 日期,PondName as 罐池号,WasteName as 废物名称,FromAmount as 数量,HandleManName as 处置人,ReceiverName as 签收人 from [vWasteToProduct] where 1=1 ");
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and  PondName like '%" + PondName + "%'");
                }
                if (!string.IsNullOrEmpty(PondName))
                {
                    sb.Append(" and  WasteName like'%" + WasteName + "%'");
                }
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                if (HandleManID != -2)
                {
                    sb.Append(" and HandleManID='" + HandleManID + "'");
                }
                //IDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), Config.con);
                //DataSet ds = new DataSet();
                //dataAdapter.Fill(ds);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
                List<Entity.Waste> items = DAL.Waste.GetPartWasteEx(2);
                foreach (Entity.Waste item in items)
                {
                    DataColumn dc = new DataColumn(item.WasteCode);
                    DataColumn dc2 = new DataColumn(item.WasteCode + "Pond");
                    dt.Columns.Add(dc);
                    dt.Columns.Add(dc2);
                }
                //List<Entity.Waste> itemss = DAL.Waste.GetPartWasteEx(3);
                //foreach (Entity.Waste item in itemss)
                //{
                //    DataColumn dc = new DataColumn(item.WasteCode);
                //    dt.Columns.Add(dc);
                //}

                //List<Entity.AnalysisItem> items = DAL.AnalysisItem.GetAnalysisItemList(1);
                //foreach (Entity.AnalysisItem item in items)
                //{
                //    DataColumn dc = new DataColumn(item.ItemName);
                //    dt.Columns.Add(dc);
                //}
                foreach (DataRow dr in dt.Rows)
                {
                    List<Entity.ProductDetail> list = DAL.ProductDetail.GetProductDetailEx(int.Parse(dr["DealID"].ToString()));
                    if (list.Count > 0)
                    {
                        foreach (Entity.ProductDetail entity in list)
                        {
                            dr[entity.ItemCode] = entity.Amount.ToString();
                            dr[entity.ItemCode + "Pond"] = entity.Name;
                        }
                    }
                    //List<Entity.FinalWaste> list2 = DAL.FinalWaste.GetFinalWaste(int.Parse(dr["DealID"].ToString()));
                    //if (list2.Count > 0)
                    //{
                    //    foreach (Entity.FinalWaste entity in list2)
                    //    {
                    //        dr[entity.ItemCode] = entity.Result.ToString();
                    //    }
                    //}
                }

                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, sb.ToString(), null);
                //dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
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



        public static DataTable QueryWasteToProductEx2(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Select DateTime 日期,PondName as 罐池号,WasteName as 废物名称,FromAmount as 数量,HandleManName as 处置人,ReceiverName as 签收人 from [vWasteToProduct] where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
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



        /// <summary>
        ///
        /// </summary>
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static Entity.WasteToProduct GetWasteToProduct(int DealID)
        {
            Entity.WasteToProduct entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [WasteToProduct] where DealID='" + DealID + "'", null);
                while (dataReader.Read())
                {
                    entity = new Entity.WasteToProduct();
                    entity.DealID = DataHelper.ParseToInt(dataReader["DealID"].ToString());
                    entity.FromPondID = DataHelper.ParseToInt(dataReader["FromPondID"].ToString());
                    entity.FromWasteCode = dataReader["FromWasteCode"].ToString();
                    entity.DateTime = DataHelper.ParseToDate(dataReader["DateTime"].ToString());
                    entity.FromAmount = decimal.Parse(dataReader["FromAmount"].ToString());
                    entity.HanderManID = DataHelper.ParseToInt(dataReader["HanderManID"].ToString());
                    entity.ReceiverID = DataHelper.ParseToInt(dataReader["ReceiverID"].ToString());
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.UpdateDate = DataHelper.ParseToDate(dataReader["UpdateDate"].ToString());
                    //entity.UpdateUser = dataReader["UpdateUser"].ToString();
                    entity.Status = DataHelper.ParseToInt(dataReader["Status"].ToString());
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
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static List<Entity.WasteToProduct> GetSumWasteToProduct()
        {
            List<Entity.WasteToProduct> list = new List<Entity.WasteToProduct>();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vWasteToProduct where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' group by WasteName", null);
                while (dataReader.Read())
                {
                    Entity.WasteToProduct entity = new Entity.WasteToProduct();
                    entity.FromWasteCode = dataReader["WasteName"].ToString();
                    entity.FromAmount = decimal.Parse(dataReader["Total"].ToString());
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
        public static decimal GetPartSumWasteToProduct(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                string Start = string.Format("{0}-01-01", DateTime.Now.Year);
                string End = string.Format("{0}-12-31", DateTime.Now.Year);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vWasteToProduct where Status=2 and DateTime>='" + Start + "' and DateTime<='" + End + "' and WasteName='" + WasteName + "' group by WasteName", null);
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
        /// <param name="PlanID">    </param>
        /// <param name="BillNumber">    </param>
        /// <param name="DateTime">    </param>
        /// <param name="EnterpriseID">    </param>
        /// <param name="WasteCode">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static DataTable GetSum(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select sum(FromAmount) as Total,WasteName from vWasteToProduct where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by WasteName");
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
        public static DataTable GetSumEx(string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select WasteName 废物名称,sum(FromAmount) as 合计 from vWasteToProduct where Status=2");
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sb.Append(" and DateTime>='" + StartTime + "'");
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sb.Append(" and DateTime<='" + EndTime + "'");
                }
                sb.Append(" group by WasteName");
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
        /// <param name="DateTime">    </param>
        /// <returns></returns>
        public static decimal GetPartSumWasteToProductEx(string WasteName)
        {
            decimal sum = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "select sum(FromAmount) as Total,WasteName from vWasteToProduct where Status=2 and DateTime='" + DateTime.Now.Date + "' and  WasteName='" + WasteName + "' group by WasteName", null);
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



        public static int AddWasteToProduct(Entity.WasteToProduct entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@FromPondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromPondID.GetType().ToString()),entity.FromPondID,32),
					dbFactory.MakeInParam("@FromWasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromWasteCode.GetType().ToString()),entity.FromWasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@FromAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromAmount.GetType().ToString()),entity.FromAmount,10),
					dbFactory.MakeInParam("@HanderManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HanderManID.GetType().ToString()),entity.HanderManID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteToProduct_Add", prams);
                iReturn = int.Parse(prams[11].Value.ToString());
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


        public static int AddWasteToProductEntity(Entity.WasteToProduct entity,List<Entity.ProductDetail> products)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@FromPondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromPondID.GetType().ToString()),entity.FromPondID,32),
					dbFactory.MakeInParam("@FromWasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromWasteCode.GetType().ToString()),entity.FromWasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@FromAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromAmount.GetType().ToString()),entity.FromAmount,10),
					dbFactory.MakeInParam("@HanderManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HanderManID.GetType().ToString()),entity.HanderManID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@CreateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateDate.GetType().ToString()),entity.CreateDate,0),
					dbFactory.MakeInParam("@CreateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.CreateUser.GetType().ToString()),entity.CreateUser,50),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeOutReturnParam()
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteToProduct_Add", prams);
                iReturn = int.Parse(prams[11].Value.ToString());
                int DealID = int.Parse(prams[11].Value.ToString());
                foreach (Entity.ProductDetail product in products)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@WTPID",	DBTypeConverter.ConvertCsTypeToOriginDBType(DealID.GetType().ToString()),DealID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(product.ItemCode.GetType().ToString()),product.ItemCode,20),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(product.PondID.GetType().ToString()),product.PondID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(product.Amount.GetType().ToString()),product.Amount,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductDetail_Add", pramse);
                }
                //foreach (Entity.FinalWaste waste in wastes)
                //{
                //    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [FinalWaste]([DealID],[ItemCode],[Result],[Status]) values ('" + DealID + "','" + waste.ItemCode + "','" + waste.Result + "','" + entity.Status + "')", null);
                //}
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


        public static int UpdateWasteToProduct(Entity.WasteToProduct entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@DealID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DealID.GetType().ToString()),entity.DealID,32),
					dbFactory.MakeInParam("@FromPondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromPondID.GetType().ToString()),entity.FromPondID,32),
					dbFactory.MakeInParam("@FromWasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromWasteCode.GetType().ToString()),entity.FromWasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@FromAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromAmount.GetType().ToString()),entity.FromAmount,10),
					dbFactory.MakeInParam("@HanderManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HanderManID.GetType().ToString()),entity.HanderManID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteToProduct_Update", prams);
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


        public static int UpdateWasteToProductEntity(Entity.WasteToProduct entity,List<Entity.ProductDetail> Adds,List<Entity.ProductDetail> Updates,List<Entity.ProductDetail> Deletes)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                IDbDataParameter[] prams = {
					dbFactory.MakeInParam("@DealID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DealID.GetType().ToString()),entity.DealID,32),
					dbFactory.MakeInParam("@FromPondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromPondID.GetType().ToString()),entity.FromPondID,32),
					dbFactory.MakeInParam("@FromWasteCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromWasteCode.GetType().ToString()),entity.FromWasteCode,20),
					dbFactory.MakeInParam("@DateTime",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DateTime.GetType().ToString()),entity.DateTime,0),
					dbFactory.MakeInParam("@FromAmount",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.FromAmount.GetType().ToString()),entity.FromAmount,10),
					dbFactory.MakeInParam("@HanderManID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.HanderManID.GetType().ToString()),entity.HanderManID,32),
					dbFactory.MakeInParam("@ReceiverID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.ReceiverID.GetType().ToString()),entity.ReceiverID,32),
					dbFactory.MakeInParam("@UpdateDate",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateDate.GetType().ToString()),entity.UpdateDate,0),
					dbFactory.MakeInParam("@UpdateUser",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.UpdateUser.GetType().ToString()),entity.UpdateUser,50),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.Status.GetType().ToString()),entity.Status,32)
				};
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_WasteToProduct_Update", prams);
                foreach (Entity.ProductDetail add in Adds)
                {
                    IDbDataParameter[] pramse = {
					dbFactory.MakeInParam("@WTPID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DealID.GetType().ToString()),entity.DealID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.ItemCode.GetType().ToString()),add.ItemCode,20),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.PondID.GetType().ToString()),add.PondID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.Status.GetType().ToString()),add.Status,32),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(add.Amount.GetType().ToString()),add.Amount,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductDetail_Add", pramse); 
                }
                foreach (Entity.ProductDetail update in Updates)
                {
                    IDbDataParameter[] pramsq = {
					dbFactory.MakeInParam("@DetailID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.DetailID.GetType().ToString()),update.DetailID,32),
					dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ItemCode.GetType().ToString()),update.ItemCode,20),
					dbFactory.MakeInParam("@PondID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.PondID.GetType().ToString()),update.PondID,32),
					dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Status.GetType().ToString()),update.Status,32),
					dbFactory.MakeInParam("@Amount",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Amount.GetType().ToString()),update.Amount,10)
				    };
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_ProductDetail_Update", pramsq);
                }
                foreach (Entity.ProductDetail delete in Deletes)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [ProductDetail] where DetailID='" + delete.DetailID + "'", null);
                }
                //foreach (Entity.FinalWaste add in Adds2)
                //{
                //   iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [FinalWaste]([DealID],[ItemCode],[Result],[Status]) values ('" + entity.DealID + "','" + add.ItemCode + "','" + add.Result + "','" + add.Status + "')", null);
                //}
                //foreach (Entity.FinalWaste update in Update2)
                //{
                //    IDbDataParameter[] pramsf = {
                //    dbFactory.MakeInParam("@FWID",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.FWID.GetType().ToString()),update.FWID,32),
                //    dbFactory.MakeInParam("@DealID",	DBTypeConverter.ConvertCsTypeToOriginDBType(entity.DealID.GetType().ToString()),entity.DealID,32),
                //    dbFactory.MakeInParam("@ItemCode",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.ItemCode.GetType().ToString()),update.ItemCode,20),
                //    dbFactory.MakeInParam("@Result",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Result.GetType().ToString()),update.Result,10),
                //    dbFactory.MakeInParam("@Status",	DBTypeConverter.ConvertCsTypeToOriginDBType(update.Status.GetType().ToString()),update.Status,32)
                //    };
                //    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.StoredProcedure, "proc_FinalWaste_Update", pramsf);
                //}
                //foreach (Entity.FinalWaste delete in Delete2)
                //{
                //    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWaste] where FWID='" + delete.FWID + "'", null);
                //}
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
        /// <param name="DealID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int UpdateWasteToProduct(int DealID, int Status)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[WasteToProduct] set Status='" + Status + "'  where DealID='" + DealID + "'", null);
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
        /// <param name="DealID">    </param>
        /// <param name="Status">    </param>
        /// <returns></returns>
        public static int PassWasteToProduct(Entity.WasteToProduct entity,decimal Used,List<Entity.ProductDetail> lists)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                //lists中的成品Amount存罐池使用量
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[WasteToProduct] set Status='" + entity.Status + "'  where DealID='" + entity.DealID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.FromPondID + "','" + Used + "','2','废酸出库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
                foreach (Entity.ProductDetail list in lists)
                {
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[ProductDetail] set Status=2 where DetailID='" + list.DetailID + "'", null);
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + list.PondID + "','" + list.Amount + "','3','成品入库','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
                    iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Update	[Pond] set Used='" + list.Amount + "'where PondID='" + list.PondID + "'", null);
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
        /// <param name="DealID">    </param>
        /// <returns></returns>
        public static int DeleteWasteToProduct(int DealID)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [WasteToProduct] where DealID='" + DealID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [ProductDetail] where WTPID='" + DealID + "'", null);
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Delete from [FinalWaste] where DealID='" + DealID + "'", null);
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
