 
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaAppActionRecord : EEnterpriseBase
    {
        public EaAppActionRecord()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaAppActionRecord;
        }

        public EaAppActionRecord(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaAppActionRecord;
        }


        public EaAppActionRecord(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaAppActionRecord;
        }

        public EaAppActionRecord(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaAppActionRecord;
        }
    }
}
