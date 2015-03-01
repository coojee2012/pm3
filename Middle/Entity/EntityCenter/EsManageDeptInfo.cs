using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    public class EsManageDeptInfo : EEnterpriseBase
    {
        
        public EsManageDeptInfo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsManageDeptInfo;
        }

        public EsManageDeptInfo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsManageDeptInfo;
        }


        public EsManageDeptInfo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsManageDeptInfo;
        }

        public EsManageDeptInfo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsManageDeptInfo;
        }

    }
}
