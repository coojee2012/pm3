using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppBatchNo : EEnterpriseBase
    {
        public EaAppBatchNo():base()
		{
            m_EntityType = EntityTypeEnum.EaAppBatchNo;
		}

		public EaAppBatchNo(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EaAppBatchNo;
		}


		public EaAppBatchNo(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EaAppBatchNo;
		}

        public EaAppBatchNo(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EaAppBatchNo;	
		}
        public string FBatchNoId
        {
            get { return EConvert.ToString(base.GetProperty("FBatchNoId")); }
            set { base.SetProperty("FBatchNoId", value); }
        }
        public string FAppId
        {
            get { return EConvert.ToString(base.GetProperty("FAppId")); }
            set { base.SetProperty("FAppId", value); }
        }

        public int FDFId
        {
            get { return EConvert.ToInt(base.GetProperty("FDFId")); }
            set { base.SetProperty("FDFId", value); }
        }
    }
}
