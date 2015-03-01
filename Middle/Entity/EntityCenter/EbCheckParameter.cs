using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Approve.EntityBase;
using System.Data;

 
namespace Approve.EntityCenter
{
    [Serializable]
    public class EbCheckParameter : EEnterpriseBase
    {
        public EbCheckParameter()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbCheckParameter;
        }

        public EbCheckParameter(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbCheckParameter;
        }


        public EbCheckParameter(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbCheckParameter;
        }

        public EbCheckParameter(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbCheckParameter;
        }
    }
}

