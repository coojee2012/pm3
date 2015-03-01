using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{
	/// <summary>
	/// EVersion 的摘要说明。
	[Serializable]
	public class EDQuestion:EEnterpriseBase
	{
		public EDQuestion():base()
		{
            m_EntityType = EntityTypeEnum.EDQuestion;
		}

		public EDQuestion(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EDQuestion;
		}

		public EDQuestion(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EDQuestion;
		}
        public EDQuestion(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EDQuestion;	
		}
	}
}
