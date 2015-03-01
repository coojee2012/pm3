using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
	/// <summary>
	/// EDic 的摘要说明。
	/// </summary>
	[Serializable]
	public class EsDic: EBase
	{
		public EsDic():base()
		{
            m_EntityType = EntityTypeEnum.EsDic;
		}

		public EsDic(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsDic;
		}


		public EsDic(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsDic;
		}

		public EsDic(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EsDic;	
		}
		
		public string FName 
		{
			get{return EConvert.ToString(base.GetProperty("FName"));}
			set{base.SetProperty("FName",value);}
		}
		public string FClassId 
		{
			get{return EConvert.ToString(base.GetProperty("FClassId"));}
			set{base.SetProperty("FClassId",value);}
		}
		public int FOrder 
		{
			get{return EConvert.ToInt(base.GetProperty("FOrder"));}
			set{base.SetProperty("FOrder",value);}
		}
		public int FLevel 
		{
			get{return EConvert.ToInt(base.GetProperty("FLevel"));}
			set{base.SetProperty("FLevel",value);}
		}
		 
		public string FNumber 
		{
			get{return EConvert.ToString(base.GetProperty("FNumber"));}
			set{base.SetProperty("FNumber",value);}
		}
        public string FSystemId 
		{
            get { return EConvert.ToString(base.GetProperty("FSystemId")); }
            set { base.SetProperty("FSystemId", value); }
		}
		public string FCNumber 
		{
			get{return EConvert.ToString(base.GetProperty("FCNumber"));}
			set{base.SetProperty("FCNumber",value);}
		}
		public string FParentId 
		{
			get{return EConvert.ToString(base.GetProperty("FParentId"));}
			set{base.SetProperty("FParentId",value);}
		}
        public string FParent
        {
            get { return EConvert.ToString(base.GetProperty("FParent")); }
            set { base.SetProperty("FParent", value); }
        }
        public string FRemark 
		{
            get { return EConvert.ToString(base.GetProperty("FRemark")); }
            set { base.SetProperty("FRemark", value); }
		}


		
	}
}
