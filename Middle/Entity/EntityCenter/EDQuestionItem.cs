using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EDQuestionItem : EEnterpriseBase
    {
        public EDQuestionItem():base()
		{
			m_EntityType = EntityTypeEnum.EDQuestionItem;
		}

		public EDQuestionItem(IDictionary iDictionary):base(iDictionary)
		{
			m_EntityType = EntityTypeEnum.EDQuestionItem;
		}


		public EDQuestionItem(DataRow dr):base(dr)
		{
			m_EntityType = EntityTypeEnum.EDQuestionItem;
		}

        public EDQuestionItem(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EDQuestionItem;	
		}
    }
}
