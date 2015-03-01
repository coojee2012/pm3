using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EaSubFlow : EEnterpriseBase
    {
        public EaSubFlow()
            : base()
        {
            m_EntityType = EntityTypeEnum.EaSubFlow;
        }

        public EaSubFlow(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EaSubFlow;
        }


        public EaSubFlow(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EaSubFlow;
        }

        public EaSubFlow(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EaSubFlow;
        }
        public string FRoleId
        {
            get { return base.GetProperty("FRoleId").ToString(); }
            set { base.SetProperty("FRoleId", value); }
        }

        public string FName
        {
            get { return base.GetProperty("FName").ToString(); }
            set { base.SetProperty("FName", value); }
        }

        public int FDefineDay
        {
            get { return EConvert.ToInt(base.GetProperty("FDefineDay")); }
            set { base.SetProperty("FDefineDay", value); }
        }
        public int FOrder
        {
            get { return EConvert.ToInt(base.GetProperty("FOrder")); }
            set { base.SetProperty("FOrder", value); }
        }
        public int FIsSend
        {
            get { return EConvert.ToInt(base.GetProperty("FIsSend")); }
            set { base.SetProperty("FIsSend", value); }
        }

        public int FIsEnd
        {
            get { return EConvert.ToInt(base.GetProperty("FIsEnd")); }
            set { base.SetProperty("FIsEnd", value); }
        }
        public int FLevel
        {
            get { return EConvert.ToInt(base.GetProperty("FLevel")); }
            set { base.SetProperty("FLevel", value); }
        }
        public int FIsAppEnd
        {
            get { return EConvert.ToInt(base.GetProperty("FIsAppEnd")); }
            set { base.SetProperty("FIsAppEnd", value); }
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
        /// 修改时间:2009-04-28 15:04
        ///修改人:霍立海   
        /// </summary>
        public string FProcessId
        {
            get { return EConvert.ToString(base.GetProperty("FProcessId")); }
            set { base.SetProperty("FProcessId", value); }
        }

        
    }
}

