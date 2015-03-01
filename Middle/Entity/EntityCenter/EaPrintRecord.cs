using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaPrintRecord : EEnterpriseBase
    {
        public EaPrintRecord()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaPrintRecord;
        }

        public EaPrintRecord(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaPrintRecord;
        }


        public EaPrintRecord(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaPrintRecord;
        }

        public EaPrintRecord(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaPrintRecord;
        }

        public string FManageDeptId
        {
            get { return EConvert.ToString(base.GetProperty("FManageDeptId")); }
            set { base.SetProperty("FManageDeptId", value); }
        }
    }
}
