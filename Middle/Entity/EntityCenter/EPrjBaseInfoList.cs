using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EPrjBaseInfo : EEnterpriseBase
    {
        public EPrjBaseInfo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfo;
        }

        public EPrjBaseInfo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfo;
        }


        public EPrjBaseInfo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfo;
        }

        public EPrjBaseInfo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfo;
        }
    }
}
