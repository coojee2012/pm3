using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;
namespace Approve.EntitySys
{
    public class EsManageType : EBase
    {
        public EsManageType()
            : base()
        {
            m_EntityType = EntityTypeEnum.EsManageType;
        }

        public EsManageType(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EsManageType;
        }


        public EsManageType(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EsManageType;
        }

        public EsManageType(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EsManageType;
        }

        public string FQurl
        {
            get { return EConvert.ToString(base.GetProperty("FQurl")); }
            set { base.SetProperty("FQurl", value); }
        }
        public string FAUrl
        {
            get { return EConvert.ToString(base.GetProperty("FAUrl")); }
            set { base.SetProperty("FAUrl", value); }
        }

        public string FName
        {
            get { return EConvert.ToString(base.GetProperty("FName")); }
            set { base.SetProperty("FName", value); }
        }

        public int FIsPrint
        {
            get { return EConvert.ToInt(base.GetProperty("FIsPrint")); }
            set { base.SetProperty("FIsPrint", value); }
        }
    }
}
