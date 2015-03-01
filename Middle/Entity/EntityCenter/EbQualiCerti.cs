using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbQualiCerti : EEnterpriseBase
    {
        public EbQualiCerti()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbQualiCerti;
        }

        public EbQualiCerti(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbQualiCerti;
        }


        public EbQualiCerti(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbQualiCerti;
        }

        public EbQualiCerti(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbQualiCerti;
        }
    }
}
