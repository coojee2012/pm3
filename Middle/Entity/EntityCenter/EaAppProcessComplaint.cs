
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppProcessComplaint : EEnterpriseBase
    {
        public EaAppProcessComplaint():base()
		{
            m_EntityType = EntityTypeEnum.EaAppProcessComplaint;
		}

        public EaAppProcessComplaint(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessComplaint;
		}


		public EaAppProcessComplaint(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessComplaint;
		}

		public EaAppProcessComplaint(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EaAppProcessComplaint;	
		}
		
    }
}
