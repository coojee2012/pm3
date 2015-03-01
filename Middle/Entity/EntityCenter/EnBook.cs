using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EnBook : EEnterpriseBase
    {
        public EnBook()
            : base()
        {
            m_EntityType = EntityTypeEnum.EnBook;
        }

        public EnBook(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EnBook;
        }


        public EnBook(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EnBook;
        }

        public EnBook(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EnBook;
        }
    }

}
