using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class PondUsed
    {
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="PondID">    </param>
        ///// <returns></returns>
        //public static decimal GetPondUsedAmount(int PondID)
        //{
        //    decimal iReturn = 0;
        //    DBOperatorBase db = new DataBase();
        //    IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
        //    try
        //    {
        //        IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select top 1* from [PondUsed] where PondID='" + PondID + "' order by CreateDate desc", null);
        //        while (dataReader.Read())
        //        {
        //            iReturn = decimal.Parse(dataReader["Used"].ToString());
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


        public static string GetJson(int PondID)
        {
            string json = "";
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                //IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [PondUsed] where PondID='" + PondID + "'", null);
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [Pond] where PondID='" + PondID + "'", null);
                //json = JsonHelper.ToJson(dataReader);
                dt = DAL.DataBase.GetDataTableFromIDataReader(dataReader);
                json = JsonHelper.ToJson(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db.Conn.Close();
            }
            return json;
        
        
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="PondID">    </param>
        /// <returns></returns>
        public static DataTable GetPondUsed(int PondID)
        {
            DataTable dt = new DataTable();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [PondUsed] where PondID='" + PondID + "'", null);
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
        public static Entity.PondUsed GetAllPondUsed()
        {
            Entity.PondUsed entity = null;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [PondUsed]", null);
                while (dataReader.Read())
                {
                    entity = new Entity.PondUsed();
                    entity.ID = DataHelper.ParseToInt(dataReader["ID"].ToString());
                    entity.PondID = DataHelper.ParseToInt(dataReader["PondID"].ToString());
                    entity.Used = decimal.Parse(dataReader["Used"].ToString());
                    entity.SourceType = DataHelper.ParseToInt(dataReader["SourceType"].ToString());
                    entity.TypeName = dataReader["TypeName"].ToString();
                    //entity.CreateUser = dataReader["CreateUser"].ToString();
                    //entity.CreateDate = DataHelper.ParseToDate(dataReader["CreateDate"].ToString());
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


        public static int AddPondUsed(Entity.PondUsed entity)
        {
            int iReturn = 0;
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            SqlTransactionHelper thelper = new SqlTransactionHelper(DAL.Config.con);
            IDbTransaction trans = thelper.StartTransaction();
            try
            {
                iReturn = db.ExecuteNonQueryTrans(trans, CommandType.Text, "Insert into [PondUsed]([PondID],[Used],[SourceType],[TypeName],[CreateUser],[CreateDate]) values ('" + entity.PondID + "','" + entity.Used + "','" + entity.SourceType + "','" + entity.TypeName + "','" + entity.CreateUser + "','" + entity.CreateDate + "')", null);
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
