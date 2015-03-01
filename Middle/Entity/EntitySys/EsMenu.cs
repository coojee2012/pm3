using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsMenu : EBase
    {
        public EsMenu()
		{
            base.m_EntityType = EntityTypeEnum.EsMenu;
		}

        public EsMenu(IDictionary dict)
            : base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsMenu;
		}

		public EsMenu(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsMenu;
		}

        public EsMenu(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsMenu;
		}

        
    }
}
