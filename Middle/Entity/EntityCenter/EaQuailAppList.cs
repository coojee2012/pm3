using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaQuailAppList : EEnterpriseBase
    {
        public EaQuailAppList()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaQuailAppList;
        }

        public EaQuailAppList(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaQuailAppList;
        }


        public EaQuailAppList(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaQuailAppList;
        }

        public EaQuailAppList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaQuailAppList;
        }
    }
}
