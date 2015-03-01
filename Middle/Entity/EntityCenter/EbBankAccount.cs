using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbBankAccount : EEnterpriseBase
    {
        public EbBankAccount()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbBankAccount;
        }

        public EbBankAccount(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbBankAccount;
        }


        public EbBankAccount(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbBankAccount;
        }

        public EbBankAccount(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbBankAccount;
        }
    }
}
