using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaQualiCondition : EEnterpriseBase
    {
        public EaQualiCondition():base()
		{
            m_EntityType = EntityTypeEnum.EaQualiCondition;
		}

        public EaQualiCondition(IDictionary iDictionary)
            : base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EaQualiCondition;
		}


		public EaQualiCondition(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EaQualiCondition;
		}

		public EaQualiCondition(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EaQualiCondition;	
		}
		
    }
}
