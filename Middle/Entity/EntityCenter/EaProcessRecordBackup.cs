using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaProcessRecordBackup : EEnterpriseBase
    {
        public EaProcessRecordBackup()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaProcessRecordBackup;
        }

        public EaProcessRecordBackup(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecordBackup;
        }


        public EaProcessRecordBackup(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecordBackup;
        }

        public EaProcessRecordBackup(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecordBackup;
        }
    }
}

