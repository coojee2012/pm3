using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;
namespace Approve.EntitySys
{
    [Serializable]
    public class EsObject : EBase
    {
        public EsObject()
        {
            base.m_EntityType = EntityTypeEnum.EsObject;
        }

        public EsObject(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EsObject;
        }

        public EsObject(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EsObject;
        }

        public EsObject(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EsObject;
        }


    }
}
