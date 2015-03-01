using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbFinance : EEnterpriseBase
    {
        public EbFinance()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbFinance;
        }

        public EbFinance(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbFinance;
        }


        public EbFinance(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbFinance;
        }

        public EbFinance(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbFinance;
        }
    }
}
