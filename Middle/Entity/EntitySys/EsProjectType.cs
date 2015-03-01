using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsProjectType : EBase
    {
        public EsProjectType()
		{
            base.m_EntityType = EntityTypeEnum.EsProjectType;
		}

		public EsProjectType(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsProjectType;
		}

		public EsProjectType(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsProjectType;
		}

        public EsProjectType(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsProjectType;
		}
 

    }
}
