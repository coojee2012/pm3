using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{  
    [Serializable]
    public class EaManageType : EEnterpriseBase
    {
        public EaManageType()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaManageType;
        }

        public EaManageType(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaManageType;
        }


        public EaManageType(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaManageType;
        }

        public EaManageType(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaManageType;
        }

    }
}
