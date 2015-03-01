using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EsBadActionCode : EEnterpriseBase
    {
        public EsBadActionCode()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsBadActionCode;
        }

        public EsBadActionCode(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsBadActionCode;
        }


        public EsBadActionCode(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsBadActionCode;
        }

        public EsBadActionCode(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsBadActionCode;
        }

    }
}
