using System;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IPaginationManager ����ʵ�����ݲ�ѯ�ķ�ҳ������
	/// �����е����ݼ�¼�ܶ�ʱ����Apdaterһ�ζ������е����ݼ��ķ�ʱ�����˷��ڴ棬��ʱ��Ҫ�õ���ҳ�ˡ�
	/// IPaginationManagerÿ�δ����ݿ��ж�ȡָ����һҳ�����ҿ��Ի���ָ��������page��
	/// ����ȡ��IDataPaginationManager�ӿ� 
	/// ���ߣ���ΰ sky.zhuwei@163.com  
	/// 2005.12.04
	/// </summary>
	public interface IPaginationManager
	{
		void      Initialize(DataPaginationParas paras) ;
		void      Initialize(IDBAccesser accesser ,int page_Size ,string whereStr ,string[] fields) ;//���ѡ�������У�fields�ɴ�null
		
		DataTable GetPage(int index) ;  //ȡ����indexҳ
		DataTable CurrentPage() ;
		DataTable PrePage() ;
		DataTable NextPage() ;

		int		  PageCount{get ;}
		int       CacherSize{get; set; }
	}

	public class PaginationManager :IPaginationManager
	{
		private DataPaginationParas   theParas ;
		private IADOBase			  adoBase ;			
		private DataTable   curPage      = null ;
		private int         itemCount    = 0 ;
		private int         pageCount    = -1 ;		
		private int         curPageIndex = -1 ;
		
		private FixCacher   fixCacher    = null ;
		private string      fieldStrs    = "" ;

		/// <summary>
		/// cacheSize С�ڵ���0 ���� ��ʾ������ ��Int.MaxValue ���� ��������
		/// </summary>		
		public PaginationManager(int cacheSize)
		{
			if(cacheSize == int.MaxValue)
			{
				this.fixCacher = new FixCacher() ;
			}
			else if(cacheSize >0)
			{
				this.fixCacher = new FixCacher(cacheSize) ;
			}
			else
			{
				this.fixCacher = null ;
			}
		}	

		public PaginationManager()
		{
		}

		#region IDataPaginationManager ��Ա
		public int CacherSize
		{
			get
			{
				if(this.fixCacher == null)
				{
					return 0 ;
				}

				return this.fixCacher.Size ;
			}
			set
			{
				if(this.fixCacher == null)
				{
					this.fixCacher = new FixCacher(value) ;
				}
				else
				{
					this.fixCacher.Size = value ;
				}
			}
		}
		public int PageCount
		{
			get
			{
				if(this.pageCount == -1)
				{
					string selCountStr = string.Format("Select count(*) from {0} {1}" ,this.theParas.TableName ,this.theParas.WhereStr) ;
					DataSet ds = this.adoBase.DoQuery(selCountStr) ;
					this.itemCount = int.Parse(ds.Tables[0].Rows[0][0].ToString()) ;
					this.pageCount = this.itemCount/this.theParas.PageSize ;
					if((this.itemCount%this.theParas.PageSize > 0))
					{
						++ this.pageCount ;
					}
				}

				return this.pageCount ;
			}
		}

		/// <summary>
		/// GetPage ȡ��ָ����һҳ
		/// </summary>		
		public DataTable GetPage(int index)
		{
			if(index == this.curPageIndex)
			{
				return this.curPage ;
			}

			if((index < 0) || (index > (this.PageCount-1)))
			{
				return null;
			}

			DataTable dt = this.GetCachedObject(index) ;

			if(dt == null)
			{
				string selectStr = this.ConstrutSelectStr(index) ;
				DataSet ds = this.adoBase.DoQuery(selectStr) ;
				dt = ds.Tables[0] ;

				this.CacheObject(index ,dt) ;
			}

			this.curPage	  = dt ;
			this.curPageIndex = index ;
			return this.curPage ;
		}

		private DataTable GetCachedObject(int index)
		{
			if(this.fixCacher == null)
			{
				return null ;
			}

			return (DataTable)this.fixCacher[index] ;
		}

		private void CacheObject(int index ,DataTable page)
		{
			if(this.fixCacher != null)
			{
				this.fixCacher.PutIn(index ,page) ;
			}
		}

		public DataTable CurrentPage()
		{
			return this.curPage ;
		}

		public DataTable PrePage()
		{
			return this.GetPage((this.curPageIndex-1)) ;
		}

		public DataTable NextPage()
		{
			return this.GetPage((this.curPageIndex + 1)) ;
		}	
	
		private string ConstrutSelectStr(int pageIndex)
		{
			if(pageIndex == 0)
			{
				return string.Format("Select top {0} {1} from {2} {3} ORDER BY ID" ,this.theParas.PageSize ,this.fieldStrs ,this.theParas.TableName ,this.theParas.WhereStr) ;
			}

			int innerCount     = this.itemCount - this.theParas.PageSize*pageIndex ;
			string innerSelStr = string.Format("Select top {0} {1} from {2} {3} ORDER BY ID DESC " ,innerCount , this.fieldStrs ,this.theParas.TableName ,this.theParas.WhereStr) ;
			string outerSelStr = string.Format("Select top {0} * from ({1}) DERIVEDTBL ORDER BY ID" ,this.theParas.PageSize ,innerSelStr) ;

			return outerSelStr ;
		}

		#region Initialize
		public void Initialize(IDBAccesser accesser, int page_Size, string whereStr, string[] fields)
		{
			this.theParas = new DataPaginationParas(accesser.ConnectString ,accesser.DbTableName ,whereStr) ;
			this.theParas.Fields = fields ;
			this.theParas.PageSize = page_Size ;
		
			this.fieldStrs = this.theParas.GetFiedString() ;	
			this.adoBase = new SqlADOBase(this.theParas.ConnectString) ;
		}	
		
		public void Initialize(DataPaginationParas paras)
		{
			this.theParas = paras ;
			this.fieldStrs = this.theParas.GetFiedString() ;	
			this.adoBase = new SqlADOBase(this.theParas.ConnectString) ;
		}

		#endregion

		#endregion
	}

	public class DataPaginationParas
	{
		public int      PageSize = 10 ;		
		public string[] Fields = {"*"}; //Ҫ���������У�"*"��ʾ������

		public string   ConnectString ;
		public string   TableName ; 
		public string   WhereStr ;      //����������where�־�

		public DataPaginationParas(string connStr ,string tableName ,string whereStr)
		{
			this.ConnectString = connStr ;
			this.TableName	   = tableName ;
			this.WhereStr      = whereStr ;
		}		

		#region GetFiedString
		public string GetFiedString()
		{
			if(this.Fields == null) 
			{
				this.Fields = new string[] {"*"} ;
			}

			string fieldStrs = "" ;

			for(int i=0 ;i<this.Fields.Length ;i++)
			{
				fieldStrs += " " + this.Fields[i] ;
				if(i != (this.Fields.Length -1))
				{
					fieldStrs += " , " ;
				}
				else
				{
					fieldStrs += " " ;
				}
			}

			return fieldStrs ;
		}
		#endregion

	}

}
