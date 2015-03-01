using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EpVideo : EEnterpriseBase
    {
        public EpVideo()
		{
            base.m_EntityType = EntityTypeEnum.EpVideo;
		}

		public EpVideo(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EpVideo;
		}

		public EpVideo(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EpVideo;
		}

        public EpVideo(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EpVideo;
		}
        public string FWebUrl
        {
            get { return EConvert.ToString(base.GetProperty("FWebUrl")); }
            set { base.SetProperty("FWebUrl", value); }
        }
    }
}
