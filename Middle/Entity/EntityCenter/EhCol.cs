using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
  
    [Serializable]
    public class EhCol : EEnterpriseBase
    {
        public EhCol()
            : base()
        {
            m_EntityType = EntityTypeEnum.EhCol;
        }

        public EhCol(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EhCol;
        }


        public EhCol(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EhCol;
        }

        public EhCol(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EhCol;
        }
    }
}
