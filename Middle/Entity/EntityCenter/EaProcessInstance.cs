using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaProcessInstance : EEnterpriseBase
    {
        public EaProcessInstance()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaProcessInstance;
        }

        public EaProcessInstance(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaProcessInstance;
        }


        public EaProcessInstance(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaProcessInstance;
        }

        public EaProcessInstance(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaProcessInstance;
        }
        public string FBaseInfoId
        {

            get { return EConvert.ToString(base.GetProperty("FBaseInfoId")); }
            set { base.SetProperty("FBaseInfoId", value); }
        }

        

        public string FEntName
        {
            get { return EConvert.ToString(base.GetProperty("FEntName")); }
            set { base.SetProperty("FEntName", value); }
        }


        public string FEmpId
        {
            get { return EConvert.ToString(base.GetProperty("FEmpId")); }
            set { base.SetProperty("FEmpId", value); }
        }
        public string FEmpName
        {
            get { return EConvert.ToString(base.GetProperty("FEmpName")); }
            set { base.SetProperty("FEmpName", value); }
        }


        public string FProcessId
        {
            get { return EConvert.ToString(base.GetProperty("FProcessId")); }
            set { base.SetProperty("FProcessId", value); }
        }
        public string FLinkId
        {
            get { return EConvert.ToString(base.GetProperty("FLinkId")); }
            set { base.SetProperty("FLinkId", value); }
        }
        public string FSubFlowId
        {
            get { return EConvert.ToString(base.GetProperty("FSubFlowId")); }
            set { base.SetProperty("FSubFlowId", value); }
        }
        public string FManageTypeId
        {
            get { return EConvert.ToString(base.GetProperty("FManageTypeId")); }
            set { base.SetProperty("FManageTypeId", value); }
        }
        public string FCurStepID
        {
            get { return EConvert.ToString(base.GetProperty("FCurStepID")); }
            set { base.SetProperty("FCurStepID", value); }
        }
        public string FRoleId
        {
            get { return EConvert.ToString(base.GetProperty("FRoleId")); }
            set { base.SetProperty("FRoleId", value); }
        }

        public string FListId
        {
            get { return EConvert.ToString(base.GetProperty("FListId")); }
            set { base.SetProperty("FListId", value); }
        }

        public string FTypeId
        {
            get { return EConvert.ToString(base.GetProperty("FTypeId")); }
            set { base.SetProperty("FTypeId", value); }
        }

        public string FLeadId
        {
            get { return EConvert.ToString(base.GetProperty("FLeadId")); }
            set { base.SetProperty("FLeadId", value); }
        }

        public string FLevelId
        {
            get { return EConvert.ToString(base.GetProperty("FLevelId")); }
            set { base.SetProperty("FLevelId", value); }
        }

        public string FSystemId
        {
            get { return EConvert.ToString(base.GetProperty("FSystemId")); }
            set { base.SetProperty("FSystemId", value); }
        }

        public string FResult
        {
            get { return EConvert.ToString(base.GetProperty("FResult")); }
            set { base.SetProperty("FResult", value); }
        }

        public string FBarCode
        {
            get { return EConvert.ToString(base.GetProperty("FBarCode")); }
            set { base.SetProperty("FBarCode", value); }
        }

        public int FManageDeptId
        {
            get { return EConvert.ToInt(base.GetProperty("FManageDeptId")); }
            set { base.SetProperty("FManageDeptId", value); }
        }

        public int FState
        {
            get { return EConvert.ToInt(base.GetProperty("FState")); }
            set { base.SetProperty("FState", value); }
        }

        public int FAppState
        {
            get { return EConvert.ToInt(base.GetProperty("FAppState")); }
            set { base.SetProperty("FAppState", value); }
        }
        public DateTime FReportDate
        {
            get { return EConvert.ToDateTime(base.GetProperty("FReportDate")); }
            set { base.SetProperty("FReportDate", value); }
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
        public int FIsNew
        {
            get { return EConvert.ToInt(base.GetProperty("FIsNew")); }
            set { base.SetProperty("FIsNew", value); }
        }

        public DateTime FSubmitDate
        {
            get { return EConvert.ToDateTime(base.GetProperty("FSubmitDate")); }
            set { base.SetProperty("FSubmitDate", value); }
        }

        public DateTime FFactTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FFactTime")); }
            set { base.SetProperty("FFactTime", value); }
        }

        public DateTime FPlanTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FPlanTime")); }
            set { base.SetProperty("FPlanTime", value); }
        }

        public int FIsBase
        {
            get { return EConvert.ToInt(base.GetProperty("FIsBase")); }
            set { base.SetProperty("FIsBase", value); } 
        }
        public int FIsPrime
        {
            get { return EConvert.ToInt(base.GetProperty("FIsBase")); }
            set { base.SetProperty("FIsBase", value); } 
        }
        public int FIsTemp
        {
            get { return EConvert.ToInt(base.GetProperty("FIsTemp")); }
            set { base.SetProperty("FIsTemp", value); } 
        } 
        
    }
}
