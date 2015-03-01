using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaProcess : EEnterpriseBase
    {
        public EaProcess()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaProcess;
        }

        public EaProcess(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaProcess;
        }


        public EaProcess(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaProcess;
        }

        public EaProcess(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaProcess;
        }

        public string FManageDeptId
        {
            get { return EConvert.ToString(base.GetProperty("FManageDeptId")); }
            set { base.SetProperty("FManageDeptId", value); }
        }
    }
}
