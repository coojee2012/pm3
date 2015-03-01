using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsUserRight : EBase
    { 
        public EsUserRight():base()
		{
            m_EntityType = EntityTypeEnum.EsUserRight;
		}

		public EsUserRight(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsUserRight;
		}


		public EsUserRight(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsUserRight;
		}

        public EsUserRight(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EsUserRight;	
		}
    }
}
