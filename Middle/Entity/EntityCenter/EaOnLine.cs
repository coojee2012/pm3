using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
  public  class EaOnLine : EEnterpriseBase
    { 
        public EaOnLine()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaOnLine;
        }

        public EaOnLine(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaOnLine;
        }


        public EaOnLine(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaOnLine;
        }

        public EaOnLine(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaOnLine;
        }
      
    }
}
