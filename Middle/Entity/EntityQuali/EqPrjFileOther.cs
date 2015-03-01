using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityQuali
{
    [Serializable]
    public class EqPrjFileOther : EEnterpriseBase
    {
        public EqPrjFileOther()
            : base()
        {
            m_EntityType = EntityTypeEnum.EqPrjFileOther;
        }

        public EqPrjFileOther(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EqPrjFileOther;
        }


        public EqPrjFileOther(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EqPrjFileOther;
        }

        public EqPrjFileOther(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EqPrjFileOther;
        }

    }
}
