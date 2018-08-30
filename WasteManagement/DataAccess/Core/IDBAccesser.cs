using System;
using System.Collections ;
using System.Data;

namespace DataAccess
{
	/// <summary>
	/// IDBAccesser ������ĵ����ݷ��ʽӿ�	
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public interface IDBAccesser :IDBAccesserQuery ,IDBAccesserOrder ,IDBAccesserRelation
	{
		//property
		string		 ConnectString{get ;}
		string		 DbTableName {get ;}
		DataBaseType DataBaseType{get ;}			

		//others
		IPaginationManager GetPaginationMgr(int page_size ,string whereStr ,string[] columns ) ;
	}

	/// <summary>
	/// IDBAccesserRelation Ϊִ��sql��ϵ�ͽӿ�
	/// </summary>
	public interface IDBAccesserRelation
	{
		//RelationAction
		int         GetRecordsCount() ;//�õ����м�¼������
		object      GetFieldValue  (string theID ,string fieldName) ;
		object		GetFieldValueEx(string whereStr ,string fieldName) ;
		bool        UpdateFieldValue(object theID ,string fieldName ,object newVal ,IDbTransaction trans) ;
		object      ExecuteScalar(string command) ;
		IDataReader GetReader(string select_str) ; //IDataReader�����Ҫ��ʱ�ر�
	}

	/// <summary>
	/// IDBAccesserQuery Ϊִ��sql��ѯ�ӿ�
	/// </summary>
	public interface IDBAccesserQuery
	{
		bool	 ReviseAObject(string where_str ,object target )  ;//ʹ�����ݿ����������µ�ǰ����
		object   GetAObject(string where_str) ;//if there is no condition clause ,please input ""
		object[] GetObjects(string where_str) ;		
		object[] GetObjectsWithoutBlob(string where_str) ; //��ȡ�Ķ����з���Blob�ֶζ�δ���
		bool	 FillBlobData(object obj) ;				   //���ĳ�����������Blob�ֶ�
		DataSet  GetDataSet(string select_str) ;	
	}

	/// <summary>
	/// IDBAccesserOrder Ϊִ��sql����ӿ�
	/// </summary>
	public interface IDBAccesserOrder
	{
		//�������Ҫ����trans��null����
		void Insert(object obj ,IDbTransaction trans) ;
		void Update(object obj ,IDbTransaction trans) ;		
		void Delete(object ID ,IDbTransaction trans) ; //IDһ��Ϊint��string����	
		void InsertBatch(ArrayList objs ,IDbTransaction trans) ;//������

		//������󲢷����Զ���ű�־
		object InsertReturnIdentity(object obj ,IDbTransaction trans) ;
	}	
}
