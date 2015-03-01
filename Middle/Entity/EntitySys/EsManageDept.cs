using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
    [Serializable]
    public class EsManageDept : EBase
    {
        public EsManageDept():base()
		{
            m_EntityType = EntityTypeEnum.EsManageDept;
		}

		public EsManageDept(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsManageDept;
		}


		public EsManageDept(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsManageDept;
		}

        public EsManageDept(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EsManageDept;	
		}
        public string FNumber
        {
            get { return base.GetProperty("FNumber").ToString(); }
            set { base.SetProperty("FNumber", value); }
        }
        public string FParentId
        {
            get { return base.GetProperty("FParentId").ToString(); }
            set { base.SetProperty("FParentId", value); }
        }
        public string FName
        {
            get { return base.GetProperty("FName").ToString(); }
            set { base.SetProperty("FName", value); }
        }
        public int FLevel
        {
            get { return EConvert.ToInt(base.GetProperty("FLevel")); }
            set { base.SetProperty("FLevel", value); }
        }
        /// <summary>
        ///  «∑Ò¿©»®œÿ
        /// </summary>
        public string FIsTown
        {
            get { return EConvert.ToString(base.GetProperty("FIsTown")); }
            set { base.SetProperty("FIsTown", value); }
        }
    }
}
