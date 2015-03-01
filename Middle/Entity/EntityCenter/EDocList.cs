using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    /// <summary>
    /// EPNews 的摘要说明。
    [Serializable]
    public class EDocList : EEnterpriseBase
    {
        public EDocList()
            : base()
        {
            m_EntityType = EntityTypeEnum.EDocList;
        }

        public EDocList(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EDocList;
        }
        public EDocList(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EDocList;
        }

        public EDocList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EDocList;
        }

        public string FNo
        {
            get { return EConvert.ToString(base.GetProperty("FNo")); }
            set { base.SetProperty("FNo", value); }
        }
        /// </summary>
        public string FConUnit
        {
            get { return EConvert.ToString(base.GetProperty("FConUnit")); }
            set { base.SetProperty("FConUnit", value); }
        }
        public string FProName
        {
            get { return EConvert.ToString(base.GetProperty("FProName")); }
            set { base.SetProperty("FProName", value); }
        }

        public string FAddress
        {
            get { return EConvert.ToString(base.GetProperty("FAddress")); }
            set { base.SetProperty("FAddress", value); }
        }
        public string FPlace
        {
            get { return EConvert.ToString(base.GetProperty("FPlace")); }
            set { base.SetProperty("FPlace", value); }
        }
        public string FPlaceNum
        {
            get { return EConvert.ToString(base.GetProperty("FPlaceNum")); }
            set { base.SetProperty("FPlaceNum", value); }
        }
        public string FUserId
        {
            get { return EConvert.ToString(base.GetProperty("FUserId")); }
            set { base.SetProperty("FUserId", value); }
        }
        public string FBatchNo
        {
            get { return EConvert.ToString(base.GetProperty("FBatchNo")); }
            set { base.SetProperty("FBatchNo", value); }
        }

        public int FOrder
        {
            get { return EConvert.ToInt(base.GetProperty("FOrder")); }
            set { base.SetProperty("FOrder", value); }
        }
        public DateTime FCreateTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FCreateTime")); }
            set { base.SetProperty("FCreateTime", value); }
        }
      
    }
}
