using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
 
    public class EjCol : EEnterpriseBase
    {
        public EjCol()
            : base()
        {
            m_EntityType = EntityTypeEnum.EjCol;
        }

        public EjCol(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EjCol;
        }


        public EjCol(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EjCol;
        }

        public EjCol(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EjCol;
        }
    }


}
