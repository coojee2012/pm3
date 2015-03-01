using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EeGoodAction : EEnterpriseBase
    {
        public EeGoodAction()
            : base()
        {
            m_EntityType = EntityTypeEnum.EeGoodAction;
        }

        public EeGoodAction(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EeGoodAction;
        }


        public EeGoodAction(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EeGoodAction;
        }

        public EeGoodAction(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EeGoodAction;
        }
    }
}
