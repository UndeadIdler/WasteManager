using System;

namespace DataAccess
{
	#region SqlADOBase
	/// <summary>
	/// SqlADOBase ADOBase��Sql���ݿ���ʵ�֡�
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public class SqlADOBase : ADOBase
	{
		public SqlADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new SqlDBTypeElementFactory() ;
		}
	}
	#endregion

	#region OracleADOBase
	/// <summary>
	/// OracleADOBase ADOBase��Oracle���ݿ���ʵ�֡�
	/// </summary>
	public class OracleADOBase  : ADOBase
	{
		public OracleADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new OracleDBTypeElementFactory() ;
		}
	}
	#endregion

	#region OleADOBase
	/// <summary>
	/// OleADOBase ADOBase��Ole���ݿ���ʵ�֡���
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public class OleADOBase : ADOBase
	{
		public OleADOBase(string connectStr) : base(connectStr)
		{
		}

		public override IDBTypeElementFactory GetDBTypeElementFactory()
		{
			return new OleDBTypeElementFactory() ;
		}
	}
	#endregion
}
