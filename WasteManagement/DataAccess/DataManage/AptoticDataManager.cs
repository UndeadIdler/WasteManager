using System;
using System.Threading ;
using System.Collections ;
using System.Reflection  ;

namespace DataAccess
{
	/// <summary>
	/// AptoticDataManager ��AptoticDataManager��Ĭ��ʵ�֣�����SkyDataAccessЭ�顣
	/// ĿǰAptoticDataManager ֻ����Դ�����ʽ������Ŀ����Ŀ�У���Ϊ�����޷���Խ���򼯡�
	/// ������������Խ�Affix�е�XAptoticDataManager������Ŀ������м���
	/// 
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2005.05.26
	/// </summary>
	public class AptoticDataManager :IAptoticDataManager 
	{
		private IDBAccesserFactory dbAccesserFactory ;
		private ArrayList aptoticDataInfoList = null ;
		private Hashtable htableData = Hashtable.Synchronized(new Hashtable()) ;
		private DataBaseType curDbType = DataBaseType.SqlServer ;
		private int refreshMinute = -1 ;//-1��ʾ������
		private bool toDispose = false ;

		public AptoticDataManager()
		{			
		}

		#region IAptoticDataManager ��Ա

		#region Initialize
		public void Initialize(ArrayList aptDataInfos ,DataBaseType dbType, int updateMinutes)
		{
			this.aptoticDataInfoList = aptDataInfos ;
			this.curDbType = dbType ;
			this.dbAccesserFactory = new DBAccesserFactory() ;
			this.dbAccesserFactory.Initialize(dbType ,null ,false) ;

			this.LoadInitialData() ;
			if((updateMinutes != -1) && (updateMinutes > 0 ))
			{
				this.refreshMinute = updateMinutes ;
				CbSimple cback = new CbSimple(this.RefreshThread) ;
				cback.BeginInvoke(null ,null) ;
			}
		}

		private void RefreshThread()
		{
			while(! this.toDispose)
			{
				Thread.Sleep(this.refreshMinute * 60000) ;
				this.ClearAllData() ;
				this.LoadInitialData() ;
			}
		}
		#endregion

		#region LoadInitialData ,LoadOneOptoticData
		private void LoadInitialData()
		{
			foreach(AptoticDataInformation info in this.aptoticDataInfoList)
			{
				if(info.LoadNow)
				{
					this.LoadOneOptoticData(info) ;
				}
			}
		}

		private void LoadOneOptoticData(AptoticDataInformation info)
		{
			if(this.htableData[info.DataClassType] != null)
			{
				return ;
			}

			IDBAccesser accesser = this.dbAccesserFactory.CreateDBAccesser(this.curDbType ,info.ConnStr ,info.DataClassType ,null) ;
			if(accesser == null)
			{
				return ;
			}

			object[] objs = accesser.GetObjects("") ;
			if(objs != null)
			{
				this.htableData.Add(info.DataClassType ,objs) ;
			}
		}
		#endregion

		#region ClearAllData ,UpdateData
		public void ClearAllData()
		{		
			this.htableData.Clear() ;
		}

		public void ClearData(Type dataClassType)
		{
			if(this.htableData[dataClassType] != null)
			{
				this.htableData.Remove(dataClassType) ;
			}
		}

		public void UpdateData()
		{
			this.htableData.Clear() ;
			this.LoadInitialData() ;
		}
		#endregion

		#region GetAllData ,GetData
		public object[] GetAllData(Type dataClassType)
		{
			object[] objs = (object[])this.htableData[dataClassType] ;
			if(objs == null)
			{
				foreach(AptoticDataInformation info in this.aptoticDataInfoList)
				{
					if(info.DataClassType.Equals(dataClassType))
					{
						this.LoadOneOptoticData(info) ;
						objs = (object[])this.htableData[dataClassType] ;
						break ;
					}
				}
			}

			return objs ;
		}

		public object GetData(Type dataClassType, string ID)
		{
			object[] objs = this.GetAllData(dataClassType) ;
			if(objs == null)
			{
				return null ;
			}

			foreach(object tar in objs)
			{
				object proValue = dataClassType.InvokeMember("ID" ,BindingFlags.Default | BindingFlags.GetProperty ,null ,tar ,null) ;
				if(proValue.ToString() == ID)
				{
					return tar ;
				}
			}

			return null ;
		}
		#endregion

		#endregion

		#region IDisposable ��Ա

		public void Dispose()
		{
			this.toDispose = true ;
			this.htableData.Clear() ;
		}

		#endregion
	}	
}
