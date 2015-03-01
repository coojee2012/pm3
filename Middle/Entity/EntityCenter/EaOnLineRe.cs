using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{
    [Serializable]
   public class EaOnLineRe : EEnterpriseBase
    {
        public EaOnLineRe()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaOnLineRe;
        }

        public EaOnLineRe(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaOnLineRe;
        }


        public EaOnLineRe(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaOnLineRe;
        }

        public EaOnLineRe(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaOnLineRe;
        }
    }
}
