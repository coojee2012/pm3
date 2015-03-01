using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EPrjBaseInfoList : EEnterpriseBase
    {
        public EPrjBaseInfoList()
            : base()
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfoList;
        }

        public EPrjBaseInfoList(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfoList;
        }


        public EPrjBaseInfoList(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfoList;
        }

        public EPrjBaseInfoList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EPrjBaseInfoList;
        }
    }
}
