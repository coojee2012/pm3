using System;
using System.Collections;
using Approve.EntityBase;
using System.Data; 
namespace Approve.EntityCenter
{
    [Serializable]
    public class EnRecUnit : EEnterpriseBase
    {
        public EnRecUnit()
            : base()
        {
            m_EntityType = EntityTypeEnum.EnRecUnit;
        }

        public EnRecUnit(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EnRecUnit;
        }


        public EnRecUnit(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EnRecUnit;
        }

        public EnRecUnit(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EnRecUnit;
        }
    }
}
