using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EnComment : EEnterpriseBase
    {
       public EnComment()
		{
            base.m_EntityType = EntityTypeEnum.EnComment;
		}

		public EnComment(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EnComment;
		}

		public EnComment(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EnComment;
		}

        public EnComment(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EnComment;
		}

        public string FNewsId
        {
            get { return EConvert.ToString(base.GetProperty("FNewsId")); }
            set { base.SetProperty("FNewsId", value); }
        }
        public string FAuthor
        {
            get { return EConvert.ToString(base.GetProperty("FAuthor")); }
            set { base.SetProperty("FAuthor", value); }
        }

        public string FIp
        {
            get { return EConvert.ToString(base.GetProperty("FIp")); }
            set { base.SetProperty("FIp", value); }
        }

        public string FText
        {
            get { return EConvert.ToString(base.GetProperty("FText")); }
            set { base.SetProperty("FText", value); }
        }

        public int FState
        {
            get { return EConvert.ToInt(base.GetProperty("FState")); }
            set { base.SetProperty("FState", value); }
        }

        public int FOrder
        {
            get { return EConvert.ToInt(base.GetProperty("FOrder")); }
            set { base.SetProperty("FOrder", value); }
        }

        public DateTime FPubTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FPubTime")); }
            set { base.SetProperty("FPubTime", value); }
        }
        public string FContent
        {
            get { return EConvert.ToString(base.GetProperty("FContent")); }
            set { base.SetProperty("FContent", value); }
        }
    }
}