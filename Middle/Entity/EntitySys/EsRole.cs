using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsRole : EBase
    {
        public EsRole()
		{
            base.m_EntityType = EntityTypeEnum.EsRole;
		}

		public EsRole(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EsRole;
		}

		public EsRole(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EsRole;
		}

        public EsRole(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EsRole;
		}

        public string FName
        {
            get { return EConvert.ToString(base.GetProperty("FName")); }
            set { base.SetProperty("FName", value); }
        }
        public int FOrder
        {
            get { return EConvert.ToInt(base.GetProperty("FOrder")); }
            set { base.SetProperty("FOrder", value); }
        }

    }
}
