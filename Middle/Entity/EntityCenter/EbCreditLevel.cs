using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbCreditLevel : EEnterpriseBase
    {
        public EbCreditLevel()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbCreditLevel;
        }

        public EbCreditLevel(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbCreditLevel;
        }


        public EbCreditLevel(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbCreditLevel;
        }

        public EbCreditLevel(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbCreditLevel;
        }
    }
}
