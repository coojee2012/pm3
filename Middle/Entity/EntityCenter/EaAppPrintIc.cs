  
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppPrintIc : EEnterpriseBase
    {
        public EaAppPrintIc():base()
		{
            m_EntityType = EntityTypeEnum.EaAppPrintIc;
		}

        public EaAppPrintIc(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EaAppPrintIc;
		}


		public EaAppPrintIc(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EaAppPrintIc;
		}

		public EaAppPrintIc(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EaAppPrintIc;	
		}
		
    }
}