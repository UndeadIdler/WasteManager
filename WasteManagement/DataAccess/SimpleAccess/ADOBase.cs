using System;
using System.Data ;
using System.Data.SqlClient ;


namespace DataAccess
{
	/// <summary>
	/// ����Ǽ򵥵����ݿ���������漰����ȣ�����ʹ���������
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public abstract class ADOBase : IADOBase  ,IDBTypeElementFactoryGetter
	{
		private IDBTypeElementFactory dbElementFactory ;
		private IDbCommand command ;
		private IDbConnection connection ;
		private IDbDataAdapter adapter ;		

		public ADOBase(string connectStr)
		{
			this.dbElementFactory = this.GetDBTypeElementFactory() ;

			this.connection			= this.dbElementFactory.GetConnection(connectStr) ;
			this.command			= this.dbElementFactory.GetCommand() ;
			this.command.Connection = this.connection ;
			this.adapter			= this.dbElementFactory.GetDataAdapter() ;			
		}

		#region IADOBase ��Ա

		public void DoCommand(string commandStr)
		{
			this.command.CommandText = commandStr ;
			try
			{				
				this.connection.Open() ;
				this.command.ExecuteNonQuery() ;
			}
			finally
			{
				this.connection.Close() ;
			}
		}

		public DataSet DoQuery(string queryStr)
		{
			this.command.CommandText = queryStr ;
			this.adapter.SelectCommand = this.command ;
			DataSet data_set_result = new DataSet() ;
			
			this.adapter.Fill(data_set_result)  ;
			return data_set_result ;
		}

		//ע�⣺this.ds_groupInfo.Tables[0].Rows[row_num].Delete();��佫��row_num�б��Ϊ��delete�� ��
		//   �� this.ds_groupInfo.Tables[0].Rows.RemoveAt(row_num) ;�����״�DataTable��ɾ���С�����
		//   �� adapter.Update(ds)ʱ�Ͳ�������ݿ��еĶ�Ӧ����delete���� 
		//   �������Ҫʹ��adapter�������ݿ⣬�ڶ�DataTable�еļ�¼����ɾ������ʱ�������Rows[row_num].Delete()��
		//   ������Rows.RemoveAt(row_num)
		public void CommitDataSet(DataSet ds)
		{
			//����adapter��select����Զ���������sql���
			this.dbElementFactory.BuildCommandForAdapter(this.adapter) ;			
			this.adapter.Update(ds) ;
			ds.AcceptChanges() ;	
		}
		#endregion

		#region IDBTypeAssistantGetter ��Ա

		public abstract IDBTypeElementFactory GetDBTypeElementFactory() ;

		#endregion
	}

}
