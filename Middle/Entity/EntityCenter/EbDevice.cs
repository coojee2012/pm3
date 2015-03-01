using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EbDevice : EEnterpriseBase
    {
        public EbDevice()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbDevice;
        }

        public EbDevice(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbDevice;
        }


        public EbDevice(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbDevice;
        }

        public EbDevice(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbDevice;
        }
    }
}
