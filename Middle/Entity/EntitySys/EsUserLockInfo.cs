using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsUserLockInfo : EBase
    {          
        public EsUserLockInfo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsUserLockInfo;
        }

        public EsUserLockInfo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsUserLockInfo;
        }


        public EsUserLockInfo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsUserLockInfo;
        }

        public EsUserLockInfo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsUserLockInfo;
        }
    }
}
