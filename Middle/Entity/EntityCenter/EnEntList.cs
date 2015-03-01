using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EnEntList : EEnterpriseBase
    {
        public EnEntList()
        {
            base.m_EntityType = EntityTypeEnum.EnEntList;
        }

        public EnEntList(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EnEntList;
        }

        public EnEntList(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EnEntList;
        }

        public EnEntList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EnEntList;
        }
    }
}
