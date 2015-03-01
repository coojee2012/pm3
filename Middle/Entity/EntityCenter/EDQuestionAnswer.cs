using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EDQuestionAnswer : EEnterpriseBase
    {
        public EDQuestionAnswer():base()
		{
			m_EntityType = EntityTypeEnum.EDQuestionAnswer;
		}

		public EDQuestionAnswer(IDictionary iDictionary):base(iDictionary)
		{
			m_EntityType = EntityTypeEnum.EDQuestionAnswer;
		}


		public EDQuestionAnswer(DataRow dr):base(dr)
		{
			m_EntityType = EntityTypeEnum.EDQuestionAnswer;
		}

        public EDQuestionAnswer(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EDQuestionAnswer;	
		}
    }
}
