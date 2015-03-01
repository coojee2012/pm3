using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EbBaseInfo : EEnterpriseBase
    {
        public EbBaseInfo():base()
		{
            m_EntityType = EntityTypeEnum.EbBaseInfo;
		}

        public EbBaseInfo(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EbBaseInfo;
		}


		public EbBaseInfo(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EbBaseInfo;
		}

		public EbBaseInfo(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EbBaseInfo;	
		}
		
    }
}
