using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EPrjCerti : EEnterpriseBase
    {
        public EPrjCerti()
            : base()
        {
            m_EntityType = EntityTypeEnum.EPrjCerti;
        }

        public EPrjCerti(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EPrjCerti;
        }


        public EPrjCerti(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EPrjCerti;
        }

        public EPrjCerti(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EPrjCerti;
        }
    }
}
