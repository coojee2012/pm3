using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbPrjFile : EEnterpriseBase
    {
        public EbPrjFile()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbPrjFile;
        }

        public EbPrjFile(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbPrjFile;
        }


        public EbPrjFile(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbPrjFile;
        }

        public EbPrjFile(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbPrjFile;
        }

    }
}
