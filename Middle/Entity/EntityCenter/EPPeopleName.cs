using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EPPeopleName : EEnterpriseBase
    {
        public EPPeopleName()
		{
            base.m_EntityType = EntityTypeEnum.EPPeopleName;
		}

		public EPPeopleName(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EPPeopleName;
		}

		public EPPeopleName(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EPPeopleName;
		}

        public EPPeopleName(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EPPeopleName;
		}
        public string FAppId
        {
            get { return EConvert.ToString(base.GetProperty("FAppId")); }
            set { base.SetProperty("FAppId", value); }
        }
    }
}
