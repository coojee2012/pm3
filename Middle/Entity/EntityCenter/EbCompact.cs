using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EbCompact : EEnterpriseBase
    {
        public EbCompact()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbCompact;
        }

        public EbCompact(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbCompact;
        }


        public EbCompact(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbCompact;
        }

        public EbCompact(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbCompact;
        }
    }
}
