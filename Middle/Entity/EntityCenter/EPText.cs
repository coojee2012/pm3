using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntityCenter
{
    /// <summary>
    /// EVersion 的摘要说明。
    [Serializable]
    public class EPText : EEnterpriseBase
    {
        public EPText()
            : base()
        {
            m_EntityType = EntityTypeEnum.EPText;
        }

        public EPText(IDictionary iDictionary)
            : base(iDictionary)
        {
            m_EntityType = EntityTypeEnum.EPText;
        }

        public EPText(DataRow dr)
            : base(dr)
        {
            m_EntityType = EntityTypeEnum.EPText;
        }
        public EPText(DataRow dr, IDictionary rel)
            : base(dr, rel)
        {
            m_EntityType = EntityTypeEnum.EPText;
        }

        public string FKey
        {
            get { return EConvert.ToString(base.GetProperty("FKey")); }
            set { base.SetProperty("FKey", value); }
        }
        /// </summary>
        public int FType
        {
            get { return EConvert.ToInt(base.GetProperty("FType")); }
            set { base.SetProperty("FType", value); }
        }

        public string FText1
        {
            get { return EConvert.ToString(base.GetProperty("FText1")); }
            set { base.SetProperty("FText1", value); }
        }
        /// </summary>
        public string FText2
        {
            get { return EConvert.ToString(base.GetProperty("FText2")); }
            set { base.SetProperty("FText2", value); }
        }


    }
}
