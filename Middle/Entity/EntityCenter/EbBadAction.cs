using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{

    [Serializable]
    public class EbBadAction : EEnterpriseBase
    {
        public EbBadAction()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbBadAction;
        }

        public EbBadAction(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbBadAction;
        }


        public EbBadAction(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbBadAction;
        }

        public EbBadAction(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbBadAction;
        }
    }
}
