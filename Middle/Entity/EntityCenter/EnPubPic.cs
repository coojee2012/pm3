using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
  
    [Serializable]

    public class EnPubPic : EEnterpriseBase
    {
        public EnPubPic()
            : base()
        {
            m_EntityType = EntityTypeEnum.EnPubPic;
        }

        public EnPubPic(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EnPubPic;
        }


        public EnPubPic(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EnPubPic;
        }

        public EnPubPic(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EnPubPic;
        }
    }
}
