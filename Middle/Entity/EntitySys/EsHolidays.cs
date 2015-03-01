using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsHolidays : EBase
	{
				
		public EsHolidays():base()
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}

		public EsHolidays(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}


		public EsHolidays(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}

        public EsHolidays(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;	
		}

		
	}
}
