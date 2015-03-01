using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;
namespace Approve.EntitySys
{
    [Serializable]
    public class EsRoleRight : EBase
    {
        public EsRoleRight()
        {
            base.m_EntityType = EntityTypeEnum.EsRoleRight;
        }

        public EsRoleRight(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EsRoleRight;
        }

        public EsRoleRight(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EsRoleRight;
        }

        public EsRoleRight(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EsRoleRight;
        } 

    }
}
