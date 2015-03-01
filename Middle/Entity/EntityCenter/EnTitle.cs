using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
	/// <summary>
	/// ENews 的摘要说明。
	/// </summary>
	[Serializable]
	public class EnTitle:EEnterpriseBase
	{
		public EnTitle()
		{
            base.m_EntityType = EntityTypeEnum.EnTitle;
		}

		public EnTitle(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EnTitle;
		}

		public EnTitle(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EnTitle;
		}

		public EnTitle(DataRow dr,IDictionary rel):base(dr,rel)
		{
            base.m_EntityType = EntityTypeEnum.EnTitle;
		}
		
		public string FTitle 
		{
			get{return EConvert.ToString(base.GetProperty("FTitle"));}
			set{base.SetProperty("FTitle",value);}
		}
		public int FClassID 
		{
			get{return EConvert.ToInt(base.GetProperty("FClassID"));}
			set{base.SetProperty("FClassID",value);}
		}
		public string FFileNote 
		{
			get{return EConvert.ToString(base.GetProperty("FFileNote"));}
			set{base.SetProperty("FFileNote",value);}
		}

		public int FIsDirect 
		{
			get{return EConvert.ToInt(base.GetProperty("FIsDirect"));}
			set{base.SetProperty("FIsDirect",value);}
		}

        public DateTime FPubTime 
		{
            get { return EConvert.ToDateTime(base.GetProperty("FPubTime")); }
            set { base.SetProperty("FPubTime", value); }
		}
        public DateTime FCreateTime
        {
            get { return EConvert.ToDateTime(base.GetProperty("FCreateTime")); }
            set { base.SetProperty("FCreateTime", value); }
        }
		public int FState 
		{
			get{return EConvert.ToInt(base.GetProperty("FState"));}
			set{base.SetProperty("FState",value);}
		}

		public string FDeptID 
		{
			get{return EConvert.ToString(base.GetProperty("FDeptID"));}
			set{base.SetProperty("FDeptID",value);}
		}
		public string FDeptName 
		{
			get{return EConvert.ToString(base.GetProperty("FDeptName"));}
			set{base.SetProperty("FDeptName",value);}
		}
		public string FWebId 
		{
			get{return EConvert.ToString(base.GetProperty("FWebId"));}
			set{base.SetProperty("FWebId",value);}
		}

		public int FOrder 
		{
			get{return EConvert.ToInt(base.GetProperty("FOrder"));}
			set{base.SetProperty("FOrder",value);}
		}
        public string FKey
        {
            get { return EConvert.ToString(base.GetProperty("FKey")); }
            set { base.SetProperty("FKey", value); }
        }
        public string FName
        {
            get { return EConvert.ToString(base.GetProperty("FName")); }
            set { base.SetProperty("FName", value); }
        }
        public int FCount
        {
            get { return EConvert.ToInt(base.GetProperty("FCount")); }
            set { base.SetProperty("FCount", value); }
        }
        public string FColor
        {
            get { return EConvert.ToString(base.GetProperty("FColor")); }
            set { base.SetProperty("FColor", value); }
        }
        public string FPubDepart
        {
            get { return EConvert.ToString(base.GetProperty("FPubDepart")); }
            set { base.SetProperty("FPubDepart", value); }
        }
	}
}
