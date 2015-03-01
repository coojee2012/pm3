
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EsUserInfo : EBase
    { 
        public EsUserInfo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsUserInfo;
        }

        public EsUserInfo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsUserInfo;
        }


        public EsUserInfo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsUserInfo;
        }

        public EsUserInfo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsUserInfo;
        }

    }
}
