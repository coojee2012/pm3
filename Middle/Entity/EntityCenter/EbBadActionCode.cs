using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EbBadActionCode : EEnterpriseBase
    {
        public EbBadActionCode()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbBadActionCode;
        }

        public EbBadActionCode(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbBadActionCode;
        }


        public EbBadActionCode(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbBadActionCode;
        }

        public EbBadActionCode(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbBadActionCode;
        }
    }
}

