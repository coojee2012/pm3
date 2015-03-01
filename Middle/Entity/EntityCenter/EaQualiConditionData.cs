 
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaQualiConditionData : EEnterpriseBase
    {
        public EaQualiConditionData()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionData;
        }

        public EaQualiConditionData(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionData;
        }


        public EaQualiConditionData(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionData;
        }

        public EaQualiConditionData(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaQualiConditionData;
        }
    }
}
