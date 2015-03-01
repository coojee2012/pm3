using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EoAdvert : EEnterpriseBase
    {
        public EoAdvert()
            : base()
        {
            m_EntityType = EntityTypeEnum.EoAdvert;
        }

        public EoAdvert(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EoAdvert;
        }


        public EoAdvert(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EoAdvert;
        }

        public EoAdvert(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EoAdvert;
        }
    }
}
