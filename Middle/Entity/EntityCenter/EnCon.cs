using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{

    [Serializable]
    public class EnCon : EEnterpriseBase
    {
        public EnCon()
        {
            base.m_EntityType = EntityTypeEnum.EnCon;
        }

        public EnCon(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EnCon;
        }

        public EnCon(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EnCon;
        }

        public EnCon(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EnCon;
        }
    }

}
