using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityQuali
{
    [Serializable]
    public class EqPrjFile : EEnterpriseBase
    {
        public EqPrjFile():base()
		{
            m_EntityType = EntityTypeEnum.EqPrjFile;
		}

		public EqPrjFile(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EqPrjFile;
		}


		public EqPrjFile(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EqPrjFile;
		}

        public EqPrjFile(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EqPrjFile;	
		}
       
    }
}
