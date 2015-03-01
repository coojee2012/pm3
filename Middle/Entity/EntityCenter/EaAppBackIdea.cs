using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppBackIdea : EEnterpriseBase
    {
        public EaAppBackIdea()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaAppBackIdea;
        }

        public EaAppBackIdea(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaAppBackIdea;
        }


        public EaAppBackIdea(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaAppBackIdea;
        }

        public EaAppBackIdea(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaAppBackIdea;
        }
    }
}
