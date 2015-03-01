using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;
namespace Approve.EntitySys
{
    
    [Serializable] 
    public class EsBadActionCode : EBase
    {
        public EsBadActionCode()
		{
            base.m_EntityType = EntityTypeEnum.EsBadActionCode;
		}

        public EsBadActionCode(IDictionary dict)
            : base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsBadActionCode;
		}

		public EsBadActionCode(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsBadActionCode;
		}

        public EsBadActionCode(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsBadActionCode;
		} 
       
    }
}


