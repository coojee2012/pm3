using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

 



namespace Approve.EntityCenter
{
    [Serializable]
    public class EaQualiConditionOther : EEnterpriseBase
    {
        public EaQualiConditionOther()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionOther;
        }

        public EaQualiConditionOther(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionOther;
        }


        public EaQualiConditionOther(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionOther;
        }

        public EaQualiConditionOther(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionOther;
        }
    }
}
