using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbIntroduce : EEnterpriseBase
    {
        public EbIntroduce():base()
		{
            m_EntityType = EntityTypeEnum.EbIntroduce;
		}

		public EbIntroduce(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EbIntroduce;
		}


		public EbIntroduce(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EbIntroduce;
		}

        public EbIntroduce(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EbIntroduce;	
		}
        public string FContent
        {
            get { return EConvert.ToString(base.GetProperty("FContent")); }
            set { base.SetProperty("FContent", value); }
        }
    }
}
