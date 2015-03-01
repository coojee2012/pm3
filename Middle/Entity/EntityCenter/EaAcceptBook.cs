using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAcceptBook : EEnterpriseBase
    {
        public EaAcceptBook()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaAcceptBook;
        }

        public EaAcceptBook(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaAcceptBook;
        }


        public EaAcceptBook(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaAcceptBook;
        }

        public EaAcceptBook(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaAcceptBook;
        }

        public DateTime FEndTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FEndTime")); }
            set { base.SetProperty("FEndTime", value); }
        }
        public int FApproveState
        {
            get { return EConvert.ToInt(base.GetProperty("FApproveState")); }
            set { base.SetProperty("FApproveState", value); }
        }
        public int fisend
        {
            get { return EConvert.ToInt(base.GetProperty("fisend")); }
            set { base.SetProperty("fisend", value); }
        }
        public int fstate
        {
            get { return EConvert.ToInt(base.GetProperty("fstate")); }
            set { base.SetProperty("fstate", value); }
        }
    }
}

