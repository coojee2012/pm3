using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    public class EDQuestionClass : EEnterpriseBase
    {
        public EDQuestionClass()
            : base()
        {
            m_EntityType = EntityTypeEnum.EDQuestionClass;
        }

        public EDQuestionClass(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EDQuestionClass;
        }

        public EDQuestionClass(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EDQuestionClass;
        }
        public EDQuestionClass(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EDQuestionClass;
        }

    }
}
