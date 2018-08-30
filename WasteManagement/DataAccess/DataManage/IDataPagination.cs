using System;
using System.Data ;
using System.Collections ;

namespace DataAccess
{
	/// <summary>
	/// IDataPaginationManager ����ʵ�����ݲ�ѯ�ķ�ҳ������
	/// �����е����ݼ�¼�ܶ�ʱ����Apdaterһ�ζ������е����ݼ��ķ�ʱ�����˷��ڴ棬��ʱ��Ҫ�õ���ҳ�ˡ�
	/// DataPaginationManagerÿ�δ����ݿ��ж�ȡָ����һҳ�����Ұ���ʷҳ������Stack�У�����������ٴη�����ʷҳ��
	/// �Ͳ����ٷ������ݿ��ˣ�ֱ�Ӵ�Stack��ȡ�����ɡ�
	/// 
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 
	/// �Ѿ���IPaginationManagerȡ������������������������������������
	/// </summary>
	public interface IDataPaginationManager
	{
		//complexIDName ��"ID"��"sta.ID"�����ڸ��ϲ�ѯ��
		//selectStr �в��������Group By �� Order By ���ֶ�
		//void Initialize(IDBAccesser accesser ,string selectStr ,string complexID_Name) ; 

		int       ItemCount{get ;}
		int		  PageCount {get ;}
		int		  CurrentPageIndex{get ;}
		DataTable StartPage() ;
		DataTable NextPage() ;
		DataTable PrePage()  ;
		DataTable CurrentPage {get ;}
		DataTable GetPage(int index) ; //ֻ���������������ȡ����ҳ

		event CbSimpleInt CurrentPageIndexChanged ;
	}

	/// <summary>
	/// DataPagination ��IDataPagination��Ĭ��ʵ�֡�����SkyDataAccessЭ�飭����һ����ʾΨһ�������ֶ�"ID"
	/// </summary>
	public class DataPaginationManager : IDataPaginationManager
	{
		private PaginationParas curParas = null ;
		private IADOBase adoBase         = null ;
		private int pageCount            = 0 ;	
		private int itemCount            = 0 ;

		private DataTable currentPage    = null ;
		private int curPageIndex         = 0 ;					

		//��Stack���洢��ʷҳ����ʵ��ǰһҳ����
		private Stack statusStackForward  = new Stack() ;
		private Stack statusStackBackWard = new Stack() ;
		private bool preForward = true ;//��һ��������

		public  event CbSimpleInt CurrentPageIndexChanged ;		

		#region IDataPagination ��Ա
		public DataPaginationManager(IDBAccesser accesser ,string selectStr ,int page_size ,bool ascending)
		{			
			this.curParas = new PaginationParas() ;			
			this.curParas.ComplexIDName = "ID" ;			
			this.curParas.PageSize = page_size ;
			this.curParas.SelectString = this.CheckSelectString(selectStr) ;	
			this.curParas.ConnectString = accesser.ConnectString ;
			this.curParas.DbType        = accesser.DataBaseType ;
			this.curParas.DBTableName   = accesser.DbTableName  ;
			this.curParas.Ascending     = ascending ;

			this.InitializeAdoBase(accesser.DataBaseType ,accesser.ConnectString) ;			
			this.pageCount = this.GetPageCount() ;
		}

		public DataPaginationManager(PaginationParas paras)
		{
			this.curParas = paras ;
			this.InitializeAdoBase(this.curParas.DbType ,this.curParas.ConnectString) ;			
			this.pageCount = this.GetPageCount() ;
		}

		#region private
		private void InitializeAdoBase(DataBaseType dbType ,string connStr)
		{
			switch(dbType)
			{
				case DataBaseType.SqlServer :
				{
					this.adoBase = new SqlADOBase(connStr) ;
					break ;
				}
				case DataBaseType.Ole :
				{
					this.adoBase = new OleADOBase(connStr) ;
					break ;
				}
				case DataBaseType.Oracle :
				{
					this.adoBase = new OracleADOBase(connStr) ;
					break ;
				}
				default:
				{
					throw new Exception("The target DataBaseType is not implemented !")  ;
				}
			}
		}

		private string CheckSelectString(string selectStr)
		{
			if((selectStr == null) || (selectStr == ""))
			{
				throw new Exception("SelectStr is invalid !")  ;
			}			

			string str = selectStr.ToLower() ;
			if((str.IndexOf("order by") != -1) || (str.IndexOf("group by") != -1))
			{
				throw new Exception("SelectStr Can't contain 'order by' or 'group by' !")  ;
			}	

			selectStr = selectStr.ToLower() ;
			string ss = string.Format("select top {0} " ,this.curParas.PageSize) ;
			return selectStr.Replace("select" ,ss );
		}

		private string ConstructSelectString(bool first ,bool forward ,PageStatus curSta)
		{
			if(this.curParas.Ascending)
			{
				if(first)
				{				
					return this.curParas.SelectString ;
				}


				string comp = " >= " ;
				string curIDValue = curSta.preIDValueHead ;
				if(forward)
				{
					comp = " > " ;
					curIDValue = curSta.curIDValueEnd ;
				}			

				if(-1 == this.curParas.SelectString.IndexOf("where"))
				{
					return this.curParas.SelectString + string.Format(" where {0} {1} '{2}'" ,this.curParas.ComplexIDName ,comp ,curIDValue) ;
				}
			
				return this.curParas.SelectString + string.Format(" and {0} {1} '{2}'" ,this.curParas.ComplexIDName ,comp ,curIDValue) ;			
			}
			else
			{
				if(first)
				{				
					return string.Format(this.curParas.SelectString + " order by {0} desc " ,this.curParas.ComplexIDName);
				}


				string comp = " <= " ;
				string curIDValue = curSta.preIDValueHead ;
				if(forward)
				{
					comp = " < " ;
					curIDValue = curSta.curIDValueEnd ;
				}			

				if(-1 == this.curParas.SelectString.IndexOf("where"))
				{
					return this.curParas.SelectString + string.Format(" where {0} {1} '{2}' order by {0} desc" ,this.curParas.ComplexIDName ,comp ,curIDValue) ;
				}
			
				return this.curParas.SelectString + string.Format(" and {0} {1} '{2}' order by {0} desc" ,this.curParas.ComplexIDName ,comp ,curIDValue) ;			
			}
		}

		private int GetPageCount()
		{
			string str = null ;
			int index = this.curParas.SelectString.IndexOf("where") ;
			if(-1 == index)
			{
				str = string.Format("Select Count(*) from {0}" ,this.curParas.DBTableName) ;
			}
			else
			{
				string whereStr = this.curParas.SelectString.Substring(index) ;
				str = string.Format("Select Count(*) from {0} {1}" ,this.curParas.DBTableName ,whereStr) ;					
			}

			DataSet ds = this.adoBase.DoQuery(str) ;
			
			if(ds.Tables[0].Rows.Count != 0)
			{
				int num = int.Parse(ds.Tables[0].Rows[0][0].ToString()) ;
				this.itemCount = num ;
				int pageCount = num/this.curParas.PageSize ;
				if(num%this.curParas.PageSize > 0)
				{
					pageCount += 1 ;
				}

				return pageCount ;
			}

			this.itemCount = 0 ;
			return 0 ;
		}
		#endregion		

		#region PageCount ,CurrentPageIndex ,CurrentPage
		public int PageCount
		{
			get
			{
				return this.pageCount ;
			}
		}

		public int ItemCount
		{
			get
			{
				return this.itemCount ;
			}
		}

		public int CurrentPageIndex
		{
			get
			{
				return this.curPageIndex ;
			}
		}

		public DataTable CurrentPage
		{
			get
			{
				return this.currentPage ;
			}
		}
		#endregion

		#region StartPage
		public DataTable StartPage()
		{
			if(this.pageCount == 0)
			{
				return null ;
			}

			this.statusStackBackWard.Clear() ;
			this.statusStackForward.Clear() ;

			string select = this.ConstructSelectString(true ,true ,null) ;
			DataSet ds = this.adoBase.DoQuery(select) ;

			PageStatus sta = new PageStatus() ;
			sta.curIDValueEnd  = ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1]["ID"].ToString() ;
			sta.preIDValueHead = ds.Tables[0].Rows[0]["ID"].ToString() ;
			sta.curTable = ds.Tables[0] ;
			this.statusStackForward.Push(sta) ;

			this.curPageIndex   = 0 ;
			this.currentPage    = sta.curTable ;
			this.ActivePageIndexChanged(this.curPageIndex) ;

			return this.currentPage ;
		}
		#endregion

		#region NextPage
		public DataTable NextPage()
		{
			if(this.curPageIndex >= this.pageCount-1)
			{
				return null ;
			}

			if(this.statusStackBackWard.Count >0)
			{				
				PageStatus staRes = (PageStatus)this.statusStackBackWard.Pop() ;
				this.statusStackForward.Push(staRes) ;
				if(! this.preForward)
				{
					if(this.statusStackBackWard.Count > 0)
					{
						staRes = (PageStatus)this.statusStackBackWard.Pop() ;
						this.statusStackForward.Push(staRes) ;
					}
				}
				
				return this.ReturnCurrentPage(staRes.curTable ,true) ;
			}

			PageStatus curSta = (PageStatus)this.statusStackForward.Peek() ;	
			string select = this.ConstructSelectString(false ,true ,curSta) ;
			DataSet ds = this.adoBase.DoQuery(select) ;			

			PageStatus sta = new PageStatus() ;
			sta.curIDValueEnd  = ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1]["ID"].ToString() ;
			sta.preIDValueHead = curSta.curTable.Rows[0]["ID"].ToString() ;
			sta.curTable = ds.Tables[0] ;
			this.statusStackForward.Push(sta) ;			

			return this.ReturnCurrentPage(sta.curTable ,true) ;
		}
		#endregion

		#region PrePage
		public DataTable PrePage()
		{
			if(this.curPageIndex < 1)
			{
				return null ;
			}

			PageStatus oldSta = (PageStatus)this.statusStackForward.Pop() ;	
			this.statusStackBackWard.Push(oldSta) ;

			if(this.preForward)
			{
				if(this.statusStackForward.Count > 0)
				{
					oldSta = (PageStatus)this.statusStackForward.Pop() ;	
					this.statusStackBackWard.Push(oldSta) ;
				}
			}			

			return this.ReturnCurrentPage(oldSta.curTable ,false) ;
		}
		#endregion

		#region ReturnCurrentPage
		private DataTable ReturnCurrentPage(DataTable curPage ,bool foward)
		{
			if(curPage == null)
			{
				return null ;
			}

			if(foward)
			{
				++ this.curPageIndex ;			
			}
			else
			{
				-- this.curPageIndex ;	
			}

			this.preForward = foward ;
			this.currentPage = curPage ;
			this.ActivePageIndexChanged(this.curPageIndex) ;

			return this.currentPage ;
		}
		#endregion

		#region GetPage
		//�����ʷ��¼���ж�Ӧ��page���򷵻��������򷵻�null
		public DataTable GetPage(int index)
		{
			if(index > (this.statusStackBackWard.Count + this.statusStackForward.Count -1) || index < 0)
			{
				return null ;
			}

			int distance = index - this.curPageIndex ;

			if(distance == 0)
			{
				return this.currentPage ;
			}			
			else if(distance > 0)
			{
				for(int i=0 ;i<distance ;i++)
				{
					this.NextPage() ;
				}

				return this.currentPage ;
			}
			else
			{
				for(int i=distance ;i<0 ;i++)
				{
					this.PrePage() ;
				}

				return this.currentPage ;
			}						
		}
		#endregion

		#region ActivePageIndexChanged
		private void ActivePageIndexChanged(int index)
		{
			if(this.CurrentPageIndexChanged != null)
			{
				this.CurrentPageIndexChanged(index) ;
			}
		}
		#endregion

		#endregion
	}

	public class PageStatus
	{
		public string     curIDValueEnd    = "" ; //��ҳ���һ����¼ID
		public string     preIDValueHead   = "" ; //��ҳ��һ����¼ID
		public DataTable  curTable = null ;
	}

	public class PaginationParas
	{
		public string ConnectString = null ;
		public string SelectString  = null ;
		public string ComplexIDName = null ;	
		public string DBTableName   = null ;
		public int    PageSize      = 0 ;
		public bool   Ascending     = true ;
		public DataBaseType  DbType = DataBaseType.SqlServer ;
	}

}
