using System;
using System.Collections;
using Approve.EntityBase;
using System.Data; 

namespace Approve.EntityCenter
{
    public class EwLink: EEnterpriseBase
    {
        public EwLink()
            : base()
        {
            m_EntityType = EntityTypeEnum.EwLink;
        }

        public EwLink(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EwLink;
        }


        public EwLink(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EwLink;
        }

        public EwLink(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EwLink;
        }
    }
}

