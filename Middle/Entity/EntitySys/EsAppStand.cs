
using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsAppStand : EBase
    {
        public EsAppStand()
		{
            base.m_EntityType = EntityTypeEnum.EsAppStand;
		}

		public EsAppStand(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsAppStand;
		}

		public EsAppStand(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsAppStand;
		}

        public EsAppStand(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsAppStand;
		}
 

    }
}
