using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbBaseInfoOther : EEnterpriseBase
    {
        public EbBaseInfoOther()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbBaseInfoOther;
        }

        public EbBaseInfoOther(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbBaseInfoOther;
        }


        public EbBaseInfoOther(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbBaseInfoOther;
        }

        public EbBaseInfoOther(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbBaseInfoOther;
        }
    }
}
