using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EeBaseinfo : EEnterpriseBase
    {
        public EeBaseinfo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EeBaseinfo;
        }

        public EeBaseinfo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EeBaseinfo;
        }


        public EeBaseinfo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EeBaseinfo;
        }

        public EeBaseinfo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EeBaseinfo;
        }
    }
}
