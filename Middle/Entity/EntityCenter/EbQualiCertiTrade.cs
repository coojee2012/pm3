using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbQualiCertiTrade : EEnterpriseBase
    {
        public EbQualiCertiTrade()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbQualiCertiTrade;
        }

        public EbQualiCertiTrade(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbQualiCertiTrade;
        }


        public EbQualiCertiTrade(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbQualiCertiTrade;
        }

        public EbQualiCertiTrade(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbQualiCertiTrade;
        }
    }
}
