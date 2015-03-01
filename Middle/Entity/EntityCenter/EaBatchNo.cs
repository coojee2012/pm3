 
using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaBatchNo : EEnterpriseBase
    {
        public EaBatchNo()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaBatchNo;
        }

        public EaBatchNo(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaBatchNo;
        }


        public EaBatchNo(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaBatchNo;
        }

        public EaBatchNo(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaBatchNo;
        }

        public string FNo
        {
            get { return EConvert.ToString(base.GetProperty("FNo")); }
            set { base.SetProperty("FNo", value); }
        }
        public string FTtile
        {
            get { return EConvert.ToString(base.GetProperty("FTtile")); }
            set { base.SetProperty("FTtile", value); }
        }

      
        public int FDFId
        {
            get { return EConvert.ToInt(base.GetProperty("FDFId")); }
            set { base.SetProperty("FDFId", value); }
        }

        public int FState
        {
            get { return EConvert.ToInt(base.GetProperty("FState")); }
            set { base.SetProperty("FState", value); }
        }

        public int FSystemID
        {
            get { return EConvert.ToInt(base.GetProperty("FSystemID")); }
            set { base.SetProperty("FSystemID", value); }
        }

    }
}
