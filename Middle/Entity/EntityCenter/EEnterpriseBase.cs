using System;
using System.Data;
using System.Collections;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
	/// <summary>
	/// Function:  �û���Ĳ�ͬ������ҵʵ�����
	/// Author:    ????
	/// Time:      2004-11-16
	/// </summary>
	[Serializable]
	public class EEnterpriseBase:EBase
	{
		public EEnterpriseBase()
		{
			base.m_EntityType = EntityTypeEnum.EEnterpriseBase;
		}

		public EEnterpriseBase(IDictionary dict):base(dict)
		{
			base.m_EntityType = EntityTypeEnum.EEnterpriseBase;
		}

		public EEnterpriseBase(DataRow dr):base(dr)
		{
			base.m_EntityType = EntityTypeEnum.EEnterpriseBase;
		}

		public EEnterpriseBase(DataRow dr,IDictionary rel):base(dr,rel)
		{
			base.m_EntityType = EntityTypeEnum.EEnterpriseBase;
		}

		// ��ҵ��¼�汾����ʼʱ��
		public string FValidBegin
		{
			set
			{
				SetProperty("FValidBegin",value);
			}
			get
			{
				return EConvert.ToString(GetProperty("FValidBegin"));
			}
		}	

		// ��ҵ��¼�汾�Ľ���ʱ��
		public string FValidEnd
		{
			set
			{
				SetProperty("FValidEnd",value);
			}
			get
			{
				return EConvert.ToString(GetProperty("FValidEnd"));
			}
		}	
	}
}
