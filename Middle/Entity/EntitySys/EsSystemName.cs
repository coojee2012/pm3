using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsSystemName : EBase
    {
        public EsSystemName()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsSystemName;
        }

        public EsSystemName(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsSystemName;
        }


        public EsSystemName(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsSystemName;
        }

        public EsSystemName(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsSystemName;
        }

        public string FQurl
        {
            get { return EConvert.ToString(base.GetProperty("FQurl")); }
            set { base.SetProperty("FQurl", value); }
        }
    }
}
