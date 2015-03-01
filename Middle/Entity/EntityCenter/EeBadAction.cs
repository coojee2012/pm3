using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EeBadAction : EEnterpriseBase
    {
        public EeBadAction()
            : base()
        {
            m_EntityType = EntityTypeEnum.EeBadAction;
        }

        public EeBadAction(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EeBadAction;
        }


        public EeBadAction(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EeBadAction;
        }

        public EeBadAction(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EeBadAction;
        }
    }
}
