
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EOASMSList : EBase
    {
        public EOASMSList()
            : base()
        {
            m_EntityType = EntityTypeEnum.EOASMSList;
        }

        public EOASMSList(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EOASMSList;
        }


        public EOASMSList(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EOASMSList;
        }

        public EOASMSList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EOASMSList;
        }

    }
}
