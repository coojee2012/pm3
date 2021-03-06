
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
   
    [Serializable]
    public class EbookOrder : EEnterpriseBase
    {
        public EbookOrder()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbookOrder;
        }

        public EbookOrder(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbookOrder;
        }


        public EbookOrder(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbookOrder;
        }

        public EbookOrder(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbookOrder;
        }
    }
}
