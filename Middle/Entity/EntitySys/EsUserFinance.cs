using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntitySys
{
    [Serializable]
    public class EsUserFinance : EBase
    {
        public EsUserFinance():base()
		{
            m_EntityType = EntityTypeEnum.EsUserFinance;
		}

        public EsUserFinance(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsUserFinance;
		}


		public EsUserFinance(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsUserFinance;
		}

		public EsUserFinance(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EsUserFinance;	
		}
	
    }
}
