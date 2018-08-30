using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WasteManagement
{
    public class MyDataOp
    {
        private string strSql;//存放Sql语句
        private static string strConn;//存放连接数据库的参数
        private SqlConnection sqlConn;
        static MyDataOp()
        {
            strConn = ConfigurationManager.ConnectionStrings["connString"].ToString();
        }
        /// <summary>
        /// 执行查询，将查询结果以DataReader的形式返回
        /// </summary>
        /// <returns>DataReader</returns>
        public SqlDataReader CreateReader()
        {
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            try
            {
                sqlConn.Open();
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                return sqlReader;
            }
            catch (Exception e)
            {
                sqlConn.Close();
                return null;

            }
            finally
            {

            }
        }
        /// <summary>
        /// 执行查询，将查询结果以DataReader的形式返回
        /// </summary>
        /// <returns>DataReader</returns>
        public SqlDataReader CreateReader(string sql)
        {
            strSql = sql;
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            try
            {
                sqlConn.Open();
                SqlDataReader sqlReader = sqlComm.ExecuteReader();
                return sqlReader;
            }
            catch (Exception e)
            {
                sqlConn.Close();
                return null;

            }
            finally
            {

            }
        }

        /// <summary>
        /// 执行查询操做，将结果以DataSet的形式返回
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet CreateDataSet()
        {
            sqlConn = new SqlConnection(strConn);
            try
            {
                SqlDataAdapter sqlAdpt = new SqlDataAdapter(strSql, sqlConn);
                DataSet ds = new DataSet();
                sqlAdpt.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            { sqlConn.Close(); }

        }

        /// <summary>
        /// 执行查询操做，将结果以DataSet的形式返回
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet CreateDataSet(string sql)
        {
            strSql = sql;
            sqlConn = new SqlConnection(strConn);
            try
            {
                SqlDataAdapter sqlAdpt = new SqlDataAdapter(strSql, sqlConn);
                DataSet ds = new DataSet();
                sqlAdpt.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            { sqlConn.Close(); }

        }

        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public bool ExecuteCommand()
        {
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            sqlConn.Open();
            try
            {
                sqlComm.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                sqlConn.Close();
            }
        }
        /// <summary>
        /// 执行添加、删除、修改等操作
        /// </summary>
        /// <returns>操作是否成功,成功则返回true</returns>
        public bool ExecuteCommand(string sql)
        {
            strSql = sql;
            sqlConn = new SqlConnection(strConn);
            SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
            sqlConn.Open();
            try
            {
                if (sqlComm.ExecuteNonQuery() > 0)
                {
                    sqlConn.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        /// <summary>
        /// 执行事务处理
        /// </summary>
        /// <param name="n">同时操作的任务数量</param>
        /// <param name="arr_strSql">存放用于操作的所有Sql语句</param>
        /// <returns>操作是否成功，成功则返回true</returns>
        public bool DoTran(int n, string[] arr_strSql)
        {
            bool blSuccess = false;
            //建立连接并打开
            sqlConn = new SqlConnection(strConn);
            sqlConn.Open();

            SqlCommand sqlComm = new SqlCommand();

            //SqlTransaction sqlTran=new SqlTransaction();
            //注意，SqlTransaction类无公开的构造函数
            SqlTransaction sqlTran;
            //创建一个事务
            sqlTran = sqlConn.BeginTransaction();
            //从此开始，基于该连接的数据操作都被认为是事务的一部分

            try
            {
                //下面绑定连接和事务对象
                sqlComm.Connection = sqlConn;
                sqlComm.Transaction = sqlTran;

                //在每次事务执行之前都检查其有效性显得代价太高——绝大多数的情况下这种耗时的检查是不必要的。
                //事务存储点提供了一种机制，用于回滚部分事务。因此，我们可以不必在更新之前检查更新的有效性，
                //而是预设一个存储点，在更新之后，如果没有出现错误，就继续执行，否则回滚到更新之前的存储点。
                //存储点的作用就在于此。要注意的是，更新和回滚代价很大，只有在遇到错误的可能性很小，
                //而且预先检查更新的有效性的代价相对很高的情况下，使用存储点才会非常有效。

                //设定存储点
                sqlTran.Save("NoUpdate");

                //更新数据
                for (int i = 0; i < n; i++)
                {
                    sqlComm.CommandText = arr_strSql[i];
                    sqlComm.ExecuteNonQuery();
                }

                //提交事务
                sqlTran.Commit();
                blSuccess = true;
            }
            catch (Exception err)
            {
                //不使用存储点
                //sqlTran.Rollback(); 
                //更新错误，回滚到指定存储点
                sqlTran.Rollback("NoUpdate");

                //blSuccess = false;
            }
            finally
            {
                sqlConn.Close();
            }
            return blSuccess;
        }


        public int DoPro(string proName, SqlParameter[] prams, object[] values)
        {
            int returnRow = 0;
            sqlConn = new SqlConnection(strConn);
            SqlCommand myCom = new SqlCommand(proName, sqlConn);
            myCom.CommandType = CommandType.StoredProcedure;
            try
            {
                myCom.Connection.Open();
                for (int i = 0; i < prams.Length; i++)
                {
                    prams[i].Value = values[i].ToString();
                }

                foreach (SqlParameter parameter in prams)
                {
                    myCom.Parameters.Add(parameter);
                }
                returnRow = myCom.ExecuteNonQuery();
            }
            catch
            {
                myCom.Connection.Close();
            }
            finally
            {
                myCom.Connection.Close();
            }
            return returnRow;
        }
    }
}

