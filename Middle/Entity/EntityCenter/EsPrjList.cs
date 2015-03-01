using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EsPrjList : EEnterpriseBase
    {
        public EsPrjList()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsPrjList;
        }

        public EsPrjList(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsPrjList;
        }


        public EsPrjList(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsPrjList;
        }

        public EsPrjList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsPrjList;
        }
    }
}
