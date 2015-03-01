using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    
    public class EjMeterail : EEnterpriseBase
    {
        public EjMeterail()
            : base()
        {
            m_EntityType = EntityTypeEnum.EjMeterail;
        }

        public EjMeterail(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EjMeterail;
        }


        public EjMeterail(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EjMeterail;
        }

        public EjMeterail(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EjMeterail;
        }
    }
}
