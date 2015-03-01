using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;


namespace Approve.EntitySys
{
    [Serializable] 
    public class EsCode : EBase
    {
        public EsCode()
		{
            base.m_EntityType = EntityTypeEnum.EsCode;
		}

        public EsCode(IDictionary dict)
            : base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsCode;
		}

		public EsCode(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsCode;
		}

        public EsCode(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsCode;
		} 
       
    }
}
