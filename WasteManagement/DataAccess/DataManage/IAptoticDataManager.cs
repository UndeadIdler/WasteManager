using System;
using System.Collections ;
using System.Data ;

namespace DataAccess
{
	/// <summary>
	/// IAptoticDataManager ���ڹ���������ϵͳ���й����о���ʹ�õ��ֲ��׷����仯�����ݣ�ͨ����Щ���ݵ�����
	/// ���Ǵ�������ݿ��У����ÿ����Ҫʱ�ͷ������ݿ⣬�Ʊؽ���ϵͳ���������ܡ�
	/// (1)IAptoticDataManager һ����ϵͳ����ʱ�����벿�����ݣ�������һЩ�����ڵ�һ��ʹ��ʱ�����룬
	/// (2)IAptoticDataManager �����������еĹ���������������Aptotic���ݡ�
	/// (3)���ϵͳ�������ǳ�ʱ�����У���ô�����趨�Զ�����ʱ�䣬��ʹIAptoticDataManager�б�������������ݿ��е�ͬ����
	/// (4)�벻Ҫ������GetData���ض����UnAptotic���֡�
	/// (5)����������ݶ�Ӧ��TableҪ�������һ���ֶ�Ϊ"ID"��ʾΨһ���� �� 
	/// 
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2005.05.30
	/// </summary>
	public interface IAptoticDataManager :IDisposable
	{
		//aptDataInfos ����AptoticDataInformation����
		void Initialize(ArrayList aptDataInfos  ,DataBaseType dbType ,int updateMinutes) ; //updateMinutesΪ-1��ʾ������		
		void ClearAllData() ;
		void UpdateData() ;

		void     ClearData(Type dataClassType) ; //���Ŀ������޸��˲��ֲ��ױ����ݣ�Ӧ���ô˷���֪ͨIAptoticDataManager��
		object   GetData(Type dataClassType ,string ID) ;
		object[] GetAllData(Type dataClassType) ;
	}
	

	public class AptoticDataInformation
	{		
		public string ConnStr    ;
		public Type DataClassType ; //����ȫ�������������ռ䲿��
		public bool LoadNow ;
	}
}
