using System;
using System.Data ;
using System.Data.SqlClient ;

namespace DataAccess.Extend
{
	#region ESqlCommand
	/// <summary>
	/// ESqlCommand �����л���SqlCommand��System.Data.SqlClient.SqlCommand�ǲ������л���
	/// </summary>
	[Serializable]
	public class ESqlCommand
	{
		private ESqlParameter[] esParas ;
		private string          cmdText ;
		private CommandType     cmdType ;

		public ESqlCommand(SqlCommand command)
		{
			this.cmdText = command.CommandText ;
			this.cmdType = command.CommandType ;

			this.esParas = new ESqlParameter[command.Parameters.Count] ;
			int index = 0 ;
			foreach(SqlParameter para in command.Parameters)
			{
				this.esParas[index] = new ESqlParameter(para) ;
				index++ ;
			}
		}

		public SqlCommand ToSqlCommand(string connStr)
		{
			SqlCommand command = new SqlCommand(this.cmdText ,new SqlConnection(connStr) ,null) ;
		
			for(int i=0 ;i<this.esParas.Length ;i++)
			{
				command.Parameters.Add(this.esParas[i].ToSqlParameter()) ;
			}		
			
			command.CommandType = this.cmdType ;
			return command ;			
		}
	}

	[Serializable]
	public class ESqlParameter
	{
		public ESqlParameter(SqlParameter sPara)
		{
			this.paraName = sPara.ParameterName ;
			this.paraLen  = sPara.Size ;
			this.paraVal  = sPara.Value ;
			this.sqlDbType= sPara.SqlDbType ;		
		}

		public SqlParameter ToSqlParameter()
		{
			SqlParameter para = new SqlParameter(this.paraName ,this.sqlDbType ,this.paraLen) ;
			para.Value = this.paraVal ;

			return para ;
		}


		#region ParaName
		private string paraName = "" ; 
		public string ParaName
		{
			get
			{
				return this.paraName ;
			}
			set
			{
				this.paraName = value ;
			}
		}
		#endregion
	
		#region ParaLen
		private int paraLen = 0 ; 
		public int ParaLen
		{
			get
			{
				return this.paraLen ;
			}
			set
			{
				this.paraLen = value ;
			}
		}
		#endregion
	
		#region ParaVal
		private object paraVal = null ; 
		public object ParaVal
		{
			get
			{
				return this.paraVal ;
			}
			set
			{
				this.paraVal = value ;
			}
		}
		#endregion		

		#region SqlDbType
		private SqlDbType sqlDbType = SqlDbType.NVarChar ; 
		public SqlDbType SqlDbType
		{
			get
			{
				return this.sqlDbType ;
			}
			set
			{
				this.sqlDbType = value ;
			}
		}
		#endregion

	}
	#endregion
}
