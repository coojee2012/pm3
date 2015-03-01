using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EbFindPerson : EEnterpriseBase
    {
        public EbFindPerson():base()
		{
            m_EntityType = EntityTypeEnum.EbFindPerson;
		}

		public EbFindPerson(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EbFindPerson;
		}


		public EbFindPerson(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EbFindPerson;
		}

        public EbFindPerson(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            m_EntityType = EntityTypeEnum.EbFindPerson;	
		}
        public string FBaseInfoId
        {
            get { return EConvert.ToString(base.GetProperty("FBaseInfoId")); }
            set { base.SetProperty("FBaseInfoId", value); }
        }
        public string FPositionDesc
        {
            get { return EConvert.ToString(base.GetProperty("FPositionDesc")); }
            set { base.SetProperty("FPositionDesc", value); }
        }
        public string FPosition
        {
            get { return EConvert.ToString(base.GetProperty("FPosition")); }
            set { base.SetProperty("FPosition", value); }
        }
        public string FDepartment
        {
            get { return EConvert.ToString(base.GetProperty("FDepartment")); }
            set { base.SetProperty("FDepartment", value); }
        }
        public string FAddress
        {
            get { return EConvert.ToString(base.GetProperty("FAddress")); }
            set { base.SetProperty("FAddress", value); }
        }
        public int FCount
        {
            get { return EConvert.ToInt(base.GetProperty("FCount")); }
            set { base.SetProperty("FCount", value); }
        }
        public int FWorkYear
        {
            get { return EConvert.ToInt(base.GetProperty("FWorkYear")); }
            set { base.SetProperty("FWorkYear", value); }
        }
        public string FDegree
        {
            get { return EConvert.ToString(base.GetProperty("FDegree")); }
            set { base.SetProperty("FDegree", value); }
        }
        public string FAward
        {
            get { return EConvert.ToString(base.GetProperty("FAward")); }
            set { base.SetProperty("FAward", value); }
        }
        public string FPubDate 
        {
            get { return EConvert.ToString(base.GetProperty("FPubDate")); }
            set { base.SetProperty("FPubDate", value); }
        }
        public string FAppRequire
        {
            get { return EConvert.ToString(base.GetProperty("FAppRequire")); }
            set { base.SetProperty("FAppRequire", value); }
        }
    }
}
