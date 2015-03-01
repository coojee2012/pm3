using System;
using System.Collections;
using Approve.EntityBase;
using System.Data; 

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaCheck : EEnterpriseBase
    {
        public EaCheck()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaCheck;
        }

        public EaCheck(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaCheck;
        }


        public EaCheck(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaCheck;
        }

        public EaCheck(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaCheck;
        }
    }
}
