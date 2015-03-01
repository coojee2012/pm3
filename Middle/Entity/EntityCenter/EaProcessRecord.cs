using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{ 
    [Serializable]
    public class EaProcessRecord : EEnterpriseBase
    {
        public EaProcessRecord()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaProcessRecord;
        }

        public EaProcessRecord(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecord;
        }


        public EaProcessRecord(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecord;
        }

        public EaProcessRecord(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaProcessRecord;
        }

     
 


        public string FProcessInstanceId
        {
            get { return EConvert.ToString(base.GetProperty("FProcessInstanceId")); }
            set { base.SetProperty("FProcessInstanceId", value); }
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
        public int FMeasure
        {
            get { return EConvert.ToInt(base.GetProperty("FMeasure")); }
            set { base.SetProperty("FMeasure", value); }
        }
        public string FResult
        {
            get { return EConvert.ToString(base.GetProperty("FResult")); }
            set { base.SetProperty("FResult", value); }
        }
        public string FIdea
        {
            get { return EConvert.ToString(base.GetProperty("FIdea")); }
            set { base.SetProperty("FIdea", value); }
        }
        public string FAppPerson
        {
            get { return EConvert.ToString(base.GetProperty("FAppPerson")); }
            set { base.SetProperty("FAppPerson", value); }
        }
        public string FCompany
        {
            get { return EConvert.ToString(base.GetProperty("FCompany")); }
            set { base.SetProperty("FCompany", value); }
        }
        public string FFunction
        {
            get { return EConvert.ToString(base.GetProperty("FFunction")); }
            set { base.SetProperty("FFunction", value); }
        }
        public string FManageDeptId
        {
            get { return EConvert.ToString(base.GetProperty("FManageDeptId")); }
            set { base.SetProperty("FManageDeptId", value); }
        }
        public DateTime FAppTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FAppTime")); }
            set { base.SetProperty("FAppTime", value); }
        }
        public DateTime FReportTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FReportTime")); }
            set { base.SetProperty("FReportTime", value); }
        }
        public string FUserId
        {
            get { return EConvert.ToString(base.GetProperty("FUserId")); }
            set { base.SetProperty("FUserId", value); }
        }
        public string FRoleId
        {
            get { return EConvert.ToString(base.GetProperty("FRoleId")); }
            set { base.SetProperty("FRoleId", value); }
        }

        public string FRoleDesc
        {
            get { return EConvert.ToString(base.GetProperty("FRoleDesc")); }
            set { base.SetProperty("FRoleDesc", value); }
        }

        public int FWaiteTime
        {
            get { return EConvert.ToInt(base.GetProperty("FWaiteTime")); }
            set { base.SetProperty("FWaiteTime", value); }
        }
        public int FDefineDay
        {
            get { return EConvert.ToInt(base.GetProperty("FDefineDay")); }
            set { base.SetProperty("FDefineDay", value); }
        }
        public int FLevel
        {
            get { return EConvert.ToInt(base.GetProperty("FLevel")); }
            set { base.SetProperty("FLevel", value); }
        }
        public int FOrder
        {
            get { return EConvert.ToInt(base.GetProperty("FOrder")); }
            set { base.SetProperty("FOrder", value); }
        }
        public int FTypeId
        {
            get { return EConvert.ToInt(base.GetProperty("FTypeId")); }
            set { base.SetProperty("FTypeId", value); }
        }

        public int FIsQuali
        {
            get { return EConvert.ToInt(base.GetProperty("FIsQuali")); }
            set { base.SetProperty("FIsQuali", value); }
        }
        public int FIsPrint
        {
            get { return EConvert.ToInt(base.GetProperty("FIsPrint")); }
            set { base.SetProperty("FIsPrint", value); }
        }

        /// <summary>
        /// 修改时间:2009-04-28 15:23
        /// 修改人:霍立海
        /// </summary>
        public int FUpDeptLevel
        {
            get { return EConvert.ToInt(base.GetProperty("FUpDeptLevel")); }
            set { base.SetProperty("FUpDeptLevel", value); }
        }

        
    }
}
