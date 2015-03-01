using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{
	/// <summary>
	/// EVersion 的摘要说明。
	[Serializable]
	public class EPWorkList:EEnterpriseBase
	{
		public EPWorkList():base()
		{
            m_EntityType = EntityTypeEnum.EPWorkList;
		}

		public EPWorkList(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EPWorkList;
		}

		public EPWorkList(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EPWorkList;
		}
        public EPWorkList(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EPWorkList;	
		}

        public string FAppId
        {
            get { return EConvert.ToString(base.GetProperty("FAppId")); }
            set { base.SetProperty("FAppId", value); }
        }

	}
}
