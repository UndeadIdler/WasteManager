using System ;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IADOBase ����Ǽ򵥵����ݿ���������漰����ȣ�����ʹ�øýӿڡ�
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public interface IADOBase
	{
		void DoCommand(string commandStr) ;
		DataSet DoQuery(string queryStr) ;
		void CommitDataSet(DataSet ds) ;//DataSet �����Ǳ�IADOBase��DoQuery���ص�DataSet
	}

	

}
