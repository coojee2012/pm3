using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbDesignReport : EEnterpriseBase
    {
        public EbDesignReport()
            : base()
        {
            m_EntityType = EntityTypeEnum.EbDesignReport;
        }

        public EbDesignReport(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EbDesignReport;
        }


        public EbDesignReport(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EbDesignReport;
        }

        public EbDesignReport(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EbDesignReport;
        }

        public int FYear
        {
            get { return EConvert.ToInt(base.GetProperty("FYear")); }
            set { base.SetProperty("FYear", value); }
        }
        public int FMonth
        {
            get { return EConvert.ToInt(base.GetProperty("FMonth")); }
            set { base.SetProperty("FMonth", value); }
        }
        public int FType
        {
            get { return EConvert.ToInt(base.GetProperty("FType")); }
            set { base.SetProperty("FType", value); }
        }
    }
}
