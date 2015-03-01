using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntityCenter
{
    [Serializable]
    public class EaComplaint : EEnterpriseBase
    {
       public EaComplaint()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaComplaint;
        }

        public EaComplaint(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaComplaint;
        }


        public EaComplaint(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaComplaint;
        }

        public EaComplaint(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaComplaint;
        }
		
    }
}
