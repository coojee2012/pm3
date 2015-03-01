using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
	/// <summary>
	/// EDicClass 的摘要说明。
	/// </summary>
	[Serializable]
	public class EsDicClass: EBase
	{
				
		public EsDicClass():base()
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}

		public EsDicClass(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}


		public EsDicClass(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;
		}

		public EsDicClass(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EsDicClass;	
		}
	
		public string FName 
		{
			get{return EConvert.ToString(base.GetProperty("FName"));}
			set{base.SetProperty("FName",value);}
		}
        public string FSystemId 
		{
            get { return EConvert.ToString(base.GetProperty("FSystemId")); }
            set { base.SetProperty("FSystemId", value); }
		}
		public string FNumber 
		{
			get{return EConvert.ToString(base.GetProperty("FNumber"));}
			set{base.SetProperty("FNumber",value);}
		}
		public string FCNumber 
		{
			get{return EConvert.ToString(base.GetProperty("FCNumber"));}
			set{base.SetProperty("FCNumber",value);}
		}

		
	}
}
