using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EtBind : EEnterpriseBase
    {
         public EtBind()
            : base()
        {
            m_EntityType = EntityTypeEnum.EtBind;
        }
        public EtBind(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EtBind;
        }
        public EtBind(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EtBind;
        }
        public EtBind(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EtBind;
        }
    }
}
