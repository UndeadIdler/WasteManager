using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class PlanDetail
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="PlanID">    </param>
        /// <param name="Code">    </param>
        /// <returns></returns>
        public static Entity.PlanDetail GetPlanDetail(int PlanID, string Code)
        {
            Entity.PlanDetail entity = new Entity.PlanDetail();
            DBOperatorBase db = new DataBase();
            IDBTypeElementFactory dbFactory = db.GetDBTypeElementFactory();
            try
            {
                IDataReader dataReader = db.ExecuteReader(Config.con, CommandType.Text, "Select * from [PlanDetail] where PlanID='" + PlanID + "' and Code='" + Code + "'", null);
                while (dataReader.Read())
                {
                    
                    entity.ItemID = DataHelper.ParseToInt(dataReader["ItemID"].ToString());
                    entity.PlanID = DataHelper.ParseToInt(dataReader["PlanID"].ToString());
                    entity.Code = dataReader["Code"].ToString();
                    entity.Amount = decimal.Parse(dataReader["Amount"].ToString());
                    entity.IsDelete = DataHelper.ParseToInt(dataReader["IsDelete"].ToString());

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
    }
}
