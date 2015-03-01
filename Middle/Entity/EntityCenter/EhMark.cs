
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EhMark : EEnterpriseBase
    {
        public EhMark()
            : base()
        {
            m_EntityType = EntityTypeEnum.EhMark;
        }

        public EhMark(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EhMark;
        }


        public EhMark(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EhMark;
        }

        public EhMark(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EhMark;
        }
    }
}