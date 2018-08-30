using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient ;

namespace DataAccess
{
	/// <summary>
	/// ITransactionHelper ��Ҫ���ڼ�ADO.NET�����̣������Դ���ͬһ���ݿ��ڲ�������ͨ��������IDBAccesser�������ʹ�� ��
	/// ����ǿ����ݿ������������Ҫʹ��COM+����
	/// 
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2004.04.18
	/// </summary>
	public interface ITransactionHelper
	{
		//�����ݿ����ӣ�����������
		IDbTransaction StartTransaction() ; //���صĽ������ΪIDBAccesser��֧������ķ�������Insert���Ĳ���

		//�ύ���񣬲��ر����ݿ�����
		void CommitTransaction(IDbTransaction trans) ;

		//�ع����񣬲��ر����ݿ�����
		void RollTransaction(IDbTransaction trans) ;
	}

	#region BaseTransactionHelper
	// ͬһ��BaseTransactionHelper ���󲻿ɴ��������������У������һ��BaseTransactionHelper �����Ӧһ������
	public abstract class BaseTransactionHelper :ITransactionHelper
	{
		protected string connectStr ;
		protected IDbConnection connection ;

		public BaseTransactionHelper(string conStr)
		{
			this.connectStr = conStr ;
			this.InitializeConnectionPool() ;
		}

		protected abstract void InitializeConnectionPool() ;

		#region ITransactionHelper ��Ա

		public IDbTransaction StartTransaction()
		{
			if(this.connection == null)
			{
				return null ;
			}

			this.connection.Open() ;
			return this.connection.BeginTransaction() ;
		}

		public void CommitTransaction(IDbTransaction trans)
		{
			if((this.connection == null) ||(trans == null))
			{
				return ;
			}

			trans.Commit() ;
			this.connection.Close() ;
		}

		public void RollTransaction(IDbTransaction trans)
		{
			if((this.connection == null) ||(trans == null))
			{
				return ;
			}

			trans.Rollback() ;
			this.connection.Close() ;
		}

		#endregion
	}
	#endregion

	#region SqlTransactionHelper
	//SQLServer���ݿ��TransactionHelper
	public class SqlTransactionHelper : BaseTransactionHelper
	{	
		public SqlTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new SqlConnection(this.connectStr) ;
		}
		
	}
	#endregion

	#region OleTransactionHelper
	//OLE Access���ݿ��TransactionHelper
	public class OleTransactionHelper : BaseTransactionHelper
	{	
		public OleTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new OleDbConnection(this.connectStr) ;
		}		
	}
	#endregion

	#region OracleTransactionHelper
	public class OracleTransactionHelper : BaseTransactionHelper
	{	
		public OracleTransactionHelper(string conStr) : base(conStr)
		{						
		}

		protected override void InitializeConnectionPool()
		{
			this.connection = new OracleConnection(this.connectStr) ;
		}		
	}
	#endregion
}
