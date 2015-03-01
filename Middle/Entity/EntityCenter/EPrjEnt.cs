using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EPrjEnt : EEnterpriseBase
    {
        public EPrjEnt()
            : base()
        {
            m_EntityType = EntityTypeEnum.EPrjEnt;
        }

        public EPrjEnt(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EPrjEnt;
        }


        public EPrjEnt(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EPrjEnt;
        }

        public EPrjEnt(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EPrjEnt;
        }
    }
}
