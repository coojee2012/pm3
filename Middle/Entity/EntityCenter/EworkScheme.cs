using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Approve.EntityBase;
namespace Approve.EntityCenter
{
    [Serializable]
    class EworkScheme:EEnterpriseBase
    {
         public EworkScheme()
            : base()
        {
            m_EntityType = EntityTypeEnum.CF_OA_WorkScheme;
        }

        public EworkScheme(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.CF_OA_WorkScheme;
        }


        public EworkScheme(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.CF_OA_WorkScheme;
        }

        public EworkScheme(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.CF_OA_WorkScheme;
        }
    }
}

