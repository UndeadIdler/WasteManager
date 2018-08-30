using System;
using System.EnterpriseServices ;

namespace DataAccess.Distributed
{
	/// <summary>
	/// EsTransactionScope ����֧�ֲַ�ʽ���񣨿ɿ����ݿ⣩��
	/// ͨ�� using( EsTransactionScope ts = new EsTransactionScope())ʹ��EsTransactionScope�ࡣ
	/// ע�����ַ�������Florin Lazar��http://blogs.msdn.com/florinlazar/archive/2004/07/24/194199.aspx
	/// </summary>
	public class EsTransactionScope : IDisposable
	{
		//�ύ����ʱ������Ϊtrue		
		private bool consistent = false;

		#region ctor
		public EsTransactionScope()
		{  
			this.EnterTxContext(TransactionOption.Required);
		} 

		public EsTransactionScope(TransactionOption txOption)
		{
			this.EnterTxContext(txOption);
		}

		//��������������
		private void EnterTxContext(TransactionOption txOption)
		{
			ServiceConfig config = new ServiceConfig(); 
			config.Transaction = txOption; 
			ServiceDomain.Enter(config);   
		} 
		#endregion

		#region Dispose ȡ��������������
		public void Dispose()
		{     
			if(!this.consistent)
			{
				//ȡ������
				ContextUtil.SetAbort();
			}

			ServiceDomain.Leave();
		}
		#endregion
 
		#region Complete �ύ����
		//�����񷽷�ִ�к󣬱�����ô˷������ύ���񣬷��򽫻���Ϊ�����������в������ع���
		public void Complete()
		{
			this.consistent = true;
		} 
		#endregion
	}

	#region Example
	/*
	 using( EsTransactionScope ts = new EsTransactionScope())
	 {
		DistributedMethod() ;
		ts.Complete() ;
	 }
	 
	 �������ʧ�ܣ��������ع������׳��쳣��
	*/
	#endregion
}

