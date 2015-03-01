using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Approve.EntityBase;
namespace Approve.EntityCenter
{
    [Serializable]
   public class EplanDetail : EEnterpriseBase
    {
         public EplanDetail()
            : base()
        {
            m_EntityType = EntityTypeEnum.CF_OA_PlanDetail;
        }

        public EplanDetail(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.CF_OA_PlanDetail;
        }


        public EplanDetail(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.CF_OA_PlanDetail;
        }

        public EplanDetail(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.CF_OA_PlanDetail;
        }
    }
}

