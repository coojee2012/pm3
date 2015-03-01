using System;
using System.Collections;
using Approve.EntityBase;
using System.Data; 

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaAppAcceptBook : EEnterpriseBase
    {
        public EaAppAcceptBook()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaAppAcceptBook;
        }

        public EaAppAcceptBook(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaAppAcceptBook;
        }


        public EaAppAcceptBook(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaAppAcceptBook;
        }

        public EaAppAcceptBook(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaAppAcceptBook;
        }
    }
}
