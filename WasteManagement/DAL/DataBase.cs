using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess;

namespace DAL
{
    public enum DataBaseType
    {
        AccessClient,
        OracleClient,
        SqlClient
    }

    public enum BaseRecordType
    {
        Division = 0
    }




    public class DataBase : DBOperatorBase
    {
        public DataBase()
        {

        }

        public override IDBTypeElementFactory GetDBTypeElementFactory()
        {
            if (DataBaseType.SqlClient == Config.dataBaseType)
            {
                return new SqlDBTypeElementFactory();
            }
            else if (DataBaseType.OracleClient == Config.dataBaseType)
            {
                return new OracleDBTypeElementFactory();
            }

            return new OleDBTypeElementFactory();
        }

        public static DataTable ConvertDataReaderToDataTable(IDataReader dataReader)
        {
            DataTable datatable = new DataTable("DataTable");
            DataTable schemaTable = dataReader.GetSchemaTable();
            //动态添加列
            try
            {

                foreach (DataRow myRow in schemaTable.Rows)
                {
                    DataColumn myDataColumn = new DataColumn();
                    myDataColumn.DataType = myRow["DataTypeName"].GetType();
                    myDataColumn.ColumnName = myRow[0].ToString();
                    datatable.Columns.Add(myDataColumn);
                }
                //添加数据
                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }
                    datatable.Rows.Add(myDataRow);
                    myDataRow = null;
                }
                schemaTable = null;
                dataReader.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                //Error.Log(ex.ToString());
                throw new Exception("转换出错出错!", ex);
            }

        }


        public static DataTable GetDataTableFromIDataReader(IDataReader reader)
        {
            DataTable dt = new DataTable();
            bool init = false;
            dt.BeginLoadData();
            object[] vals = new object[0];
            while (reader.Read())
            {
                if (!init)
                {
                    init = true;
                    int fieldCount = reader.FieldCount;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                    }
                    vals = new object[fieldCount];
                }
                reader.GetValues(vals);
                dt.LoadDataRow(vals, true);
            }
            reader.Close();
            dt.EndLoadData();
            return dt;
        } 
    }
}

