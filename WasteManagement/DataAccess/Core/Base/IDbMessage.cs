using System;

namespace DataAccess
{
	/// <summary>
	/// IDbMessage ����ȡ��IDataBaseInfoMgr ��
	/// </summary>
	public interface IDbMessage
	{
		DataBaseType DataBaseType{get ; set ;}
		string		 ConnectString{get ; set ;}

		void		 ActivateDbConnChangedEvent() ; //�ⲿ֪ͨIDataBaseInfoMgr���ݿ�������Ϣ�����˱仯		
		event		 EventHandler DbConfigChanged ; 
		bool         IsMultiDataBase{get ;set ;}
	}
}
