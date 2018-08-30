using System;
using System.Reflection ;
using System.Collections;

namespace DataAccess
{
	/// <summary>
	/// IDBAccesserFactory ���ڲ�����Բ�ͬ���������ݿ⣩�ķ�����Accesser ��	
	/// Լ����
	/// (1)���ݿ������DataClass��ͬ����
	/// (2)���ݿ����������������� DataClassName + "SqlDealer" ,��StudentSqlDealer ��ʾ���ʶ�Ӧ��SQL���ݿ��е�Student��	
	/// 
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2005.05.25
	/// </summary>
	public interface IDBAccesserFactory
	{		
		//���connStr�����仯���ɴ���null �������Ole����connStrΪaccess�ļ���·����cachAccesser��ʾ�Ƿ񻺴洴����ʵ����
		void Initialize(DataBaseType dbType ,string connStr ,bool cachAccesser) ;
		
		//����dataClassType��Ϊ������Ϊ���ڱ���ʱ�������Ͳ����ڵĴ���
		IDBAccesser CreateDBAccesser(Type dataClassType) ;			
		IDBAccesser CreateDBAccesser(DataBaseType dbType , string connStr , Type dataClassType ,string assemblyName);//assemblyName�Ƿ����������ĳ�������	
	}
	
	public class DBAccesserFactory :IDBAccesserFactory
	{
		private DataBaseType curDbType      = DataBaseType.SqlServer ;	
		private string       curConnStr     = null ;
		private bool         accesserCached = false ;
		private Hashtable    htableCached   = null ;
		
		#region IDBAccesserFactory ��Ա
		#region Initialize
		public void Initialize(DataBaseType dbType ,string connStr ,bool cachAccesser)
		{
			this.curDbType      = dbType ;			
			this.curConnStr     = connStr ;
			this.accesserCached = cachAccesser ;
			if(cachAccesser)
			{
				this.htableCached = Hashtable.Synchronized(new Hashtable()) ;
			}
		}
		#endregion

		#region public CreateDBAccesser
		public IDBAccesser CreateDBAccesser(Type dataClassType )
		{
			if(this.curConnStr == null)
			{
				throw new Exception("Paras Information is not enough to Create instance !") ;
			}			

			return this.CreateDBAccesser(this.curDbType ,this.curConnStr ,dataClassType ,null) ;			
		}

		public IDBAccesser CreateDBAccesser(DataBaseType dbType , string connStr , Type dataClassType ,string assemblyName)
		{
			string tarTypeName = dataClassType.FullName + DBAccesserFactory.GetAppendixOfDBType(dbType) ;

			return this.CreateDBAccesser(tarTypeName ,connStr ,dataClassType ,assemblyName) ;						
		}	
		#endregion

		#region private CreateDBAccesser
		private IDBAccesser CreateDBAccesser(string dealerTypeName , string connStr , Type dataClassType ,string assemblyName)
		{
			if(this.accesserCached)
			{
				if(this.htableCached[dataClassType] != null)
				{
					return (IDBAccesser)this.htableCached[dataClassType] ;
				}
			}
			
			Type tarType = AdvancedFunction.GetType(dealerTypeName ,assemblyName) ;

			if(tarType == null)
			{
				throw new Exception("The target type is not exit !") ;
			}

			Type supType = typeof(DataAccess.IDBAccesser)  ;

			// ������IsSubclassOf��IsSubclassOf ��ⲻ�˽ӿ����͡�
			if(! supType.IsAssignableFrom(tarType))
			{
				throw new Exception("The target DbDealer Type is not subclass of IDBAccesser !") ;
			}

			object[] args = {connStr} ;			
			IDBAccesser accesser = (IDBAccesser)Activator.CreateInstance(tarType ,args) ;

			if(this.accesserCached)
			{
				if(accesser != null)
				{
					this.htableCached.Add(dataClassType ,accesser) ;
				}
			}

			return accesser ;
		}
		#endregion

		#endregion

		#region GetAppendixOfDBType
		private static string GetAppendixOfDBType(DataBaseType dbType)
		{
			switch(dbType)
			{
				case DataBaseType.SqlServer :
				{
					return "SqlDealer" ;
				}
				case DataBaseType.Ole :
				{
					return "OleDealer" ;
				}
				case DataBaseType.Oracle :
				{
					return "OracleDealer" ;
				}
				default:
				{
					throw new Exception("Error DataBase Type !") ;
				}
			}
		}
		#endregion
	}

	public enum DataBaseType
	{
		SqlServer ,Ole ,Oracle
	}
}
