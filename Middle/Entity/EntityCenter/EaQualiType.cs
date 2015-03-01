using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaQualiType : EEnterpriseBase
    {
        public EaQualiType()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaQualiType;
        }

        public EaQualiType(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaQualiType;
        }


        public EaQualiType(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaQualiType;
        }

        public EaQualiType(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaQualiType;
        }
    }
}
