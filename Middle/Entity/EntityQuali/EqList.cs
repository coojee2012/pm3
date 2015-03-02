using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityQuali
{
    [Serializable]
    public class EqList : EEnterpriseBase
    {
        public EqList()
        {
            base.m_EntityType = EntityTypeEnum.EqList;
        }

        public EqList(IDictionary dict)
            : base(dict)
        {
            base.m_EntityType = EntityTypeEnum.EqList;
        }

        public EqList(DataRow dr)
            : base(dr)
        {
            base.m_EntityType = EntityTypeEnum.EqList;
        }

        public EqList(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            base.m_EntityType = EntityTypeEnum.EqList;
        }

        public string FId
        {
            get { return EConvert.ToString(base.GetProperty("FID")); }
            set { base.SetProperty("FId", value); }
        }

        public string FName
        {
            get { return EConvert.ToString(base.GetProperty("FName")); }
            set { base.SetProperty("FName", value); }
        }

        public string FBaseInfoId
        {
            get { return EConvert.ToString(base.GetProperty("FBaseInfoId")); }
            set { base.SetProperty("FBaseInfoId", value); }
        }

        public string FManageTypeId
        {
            get { return EConvert.ToString(base.GetProperty("FManageTypeId")); }
            set { base.SetProperty("FManageTypeId", value); }
        }

        public DateTime FwriteDate
        {
            get { return EConvert.ToDateTime(base.GetProperty("FwriteDate")); }
            set { base.SetProperty("FwriteDate", value); }
        }

        public DateTime FReportDate
        {
            get { return EConvert.ToDateTime(base.GetProperty("FReportDate")); }
            set { base.SetProperty("FReportDate", value); }
        }
        public int FIsSign
        {
            get { return EConvert.ToInt(base.GetProperty("FIsSign")); }
            set { base.SetProperty("FIsSign", value); }
        }
        public int FState
        {
            get { return EConvert.ToInt(base.GetProperty("FState")); }
            set { base.SetProperty("FState", value); }
        }
        public string FResult
        {
            get { return EConvert.ToString(base.GetProperty("FResult")); }
            set { base.SetProperty("FResult", value); }
        }
        public int FYear
        {
            get { return EConvert.ToInt(base.GetProperty("FYear")); }
            set { base.SetProperty("FYear", value); }
        }
        public string FLinkId
        {
            get { return EConvert.ToString(base.GetProperty("FLinkId")); }
            set { base.SetProperty("FLinkId", value); }
        }
        public string FBaseName
        {
            get { return EConvert.ToString(base.GetProperty("FBaseName")); }
            set { base.SetProperty("FBaseName", value); }
        }
        public string FIDCard
        {
            get { return EConvert.ToString(base.GetProperty("FIDCard")); }
            set { base.SetProperty("FIDCard", value); }
        }
        public int FUpDeptId
        {
            get { return EConvert.ToInt(base.GetProperty("FUpDeptId")); }
            set { base.SetProperty("FUpDeptId", value); }
        }
        public string FRemark
        {
            get { return EConvert.ToString(base.GetProperty("FRemark")); }
            set { base.SetProperty("FRemark", value); }
        }   
        public string FPrjId
        {
            get { return EConvert.ToString(base.GetProperty("FPrjId")); }
            set { base.SetProperty("FPrjId", value); }
        }
        public int FCount
        {
            get { return EConvert.ToInt(base.GetProperty("FCount")); }
            set { base.SetProperty("FCount", value); }
        }
        
    }
}
