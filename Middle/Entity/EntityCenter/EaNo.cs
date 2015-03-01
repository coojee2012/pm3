 

using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaNo : EEnterpriseBase
    {
        public EaNo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaNo;
        }

        public EaNo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaNo;
        }


        public EaNo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaNo;
        }

        public EaNo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaNo;
        }
    }
}
