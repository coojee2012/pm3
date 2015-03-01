using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;
namespace Approve.EntitySys
{   
    [Serializable]
    public class EsQualiLevel : EBase
    {
        public EsQualiLevel()
        {
            base.m_EntityType = EntityTypeEnum.EsQualiLevel;
        }

        public EsQualiLevel(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EsQualiLevel;
        }

        public EsQualiLevel(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EsQualiLevel;
        }

        public EsQualiLevel(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EsQualiLevel;
        }


    }
}
