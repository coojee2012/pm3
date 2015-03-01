using System;
using System.Data;
using System.Collections;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
	/// <summary>
	/// Function:  用基类的不同构造企业实体基类
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

		// 企业记录版本的起始时间
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

		// 企业记录版本的结束时间
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
