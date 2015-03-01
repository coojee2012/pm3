using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
    [Serializable]
    public class EData : EEnterpriseBase
    {
        		public EData()
		{
            base.m_EntityType = EntityTypeEnum.EData;
		}

		public EData(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EData;
		}

		public EData(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EData;
		}

        public EData(DataRow dr, IDictionary rel)
            : base(dr, rel)
		{
            base.m_EntityType = EntityTypeEnum.EData;
		}
		
		public string FTxt1 
		{
            get { return EConvert.ToString(base.GetProperty("FTxt1")); }
            set { base.SetProperty("FTxt1", value); }
		}
        public string FTxt2
        {
            get { return EConvert.ToString(base.GetProperty("FTxt2")); }
            set { base.SetProperty("FTxt2", value); }
        }
        public string FTxt3
        {
            get { return EConvert.ToString(base.GetProperty("FTxt3")); }
            set { base.SetProperty("FTxt3", value); }
        }
        public string FTxt4
        {
            get { return EConvert.ToString(base.GetProperty("FTxt4")); }
            set { base.SetProperty("FTxt4", value); }
        }
        public string FTxt5
        {
            get { return EConvert.ToString(base.GetProperty("FTxt5")); }
            set { base.SetProperty("FTxt5", value); }
        }
        public string FTxt6
        {
            get { return EConvert.ToString(base.GetProperty("FTxt6")); }
            set { base.SetProperty("FTxt6", value); }
        }
        public string FTxt7
        {
            get { return EConvert.ToString(base.GetProperty("FTxt7")); }
            set { base.SetProperty("FTxt7", value); }
        }

        public int FInt1
        {
            get { return EConvert.ToInt(base.GetProperty("FInt1")); }
            set { base.SetProperty("FInt1", value); }
        }
        public int FInt2
        {
            get { return EConvert.ToInt(base.GetProperty("FInt2")); }
            set { base.SetProperty("FInt2", value); }
        }
        public int FInt3
        {
            get { return EConvert.ToInt(base.GetProperty("FInt3")); }
            set { base.SetProperty("FInt3", value); }
        }
        public int FInt4
        {
            get { return EConvert.ToInt(base.GetProperty("FInt4")); }
            set { base.SetProperty("FInt4", value); }
        }
        public int FInt5
        {
            get { return EConvert.ToInt(base.GetProperty("FInt5")); }
            set { base.SetProperty("FInt5", value); }
        }
    }
}
