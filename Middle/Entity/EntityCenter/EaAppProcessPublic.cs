
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppProcessPublic : EEnterpriseBase
    {
        public EaAppProcessPublic():base()
		{
            m_EntityType = EntityTypeEnum.EaAppProcessPublic;
		}

        public EaAppProcessPublic(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessPublic;
		}


		public EaAppProcessPublic(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessPublic;
		}

		public EaAppProcessPublic(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessPublic;	
		}
		
    }
}
