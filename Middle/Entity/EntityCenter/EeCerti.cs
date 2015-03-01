using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EeCerti : EEnterpriseBase
    {
        public EeCerti()
            : base()
        {
            m_EntityType = EntityTypeEnum.EeCerti;
        }

        public EeCerti(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EeCerti;
        }


        public EeCerti(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EeCerti;
        }

        public EeCerti(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EeCerti;
        }
    }
}
