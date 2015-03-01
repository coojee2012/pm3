using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EProjectContent : EEnterpriseBase
    {
        public EProjectContent():base()
		{
            m_EntityType = EntityTypeEnum.EProjectContent;
		}

		public EProjectContent(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EProjectContent;
		}


		public EProjectContent(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EProjectContent;
		}

        public EProjectContent(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EProjectContent;	
		}

        public string FProjectId
        {
            get { return EConvert.ToString(base.GetProperty("FProjectId")); }
            set { base.SetProperty("FProjectId", value); }
        }

        public string FContent
        {
            get { return EConvert.ToString(base.GetProperty("FContent")); }
            set { base.SetProperty("FContent", value); }
        }

 
    }
}
