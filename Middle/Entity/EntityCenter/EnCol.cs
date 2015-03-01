using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntityCenter
{
	/// <summary>
	/// EPNews 的摘要说明。
	[Serializable]
	public class EnCol:EEnterpriseBase
	{
		public EnCol():base()
		{
            m_EntityType = EntityTypeEnum.EnCol;
		}

		public EnCol(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EnCol;
		}


		public EnCol(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EnCol;
		}

		public EnCol(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EnCol;	
		}
		public string FNewsId 
		{
			get{return EConvert.ToString(base.GetProperty("FNewsId"));}
			set{base.SetProperty("FNewsId",value);}
		}
		/// <summary>
		///
		/// </summary>
		public string FClassId
		{
			get{return EConvert.ToString(base.GetProperty("FClassId"));}
			set{base.SetProperty("FClassId",value);}
		}
		public string FDeptName
		{
			get{return EConvert.ToString(base.GetProperty("FDeptName"));}
			set{base.SetProperty("FDeptName",value);}
		}
		
		/// <summary>
		///
		/// </summary>
		public int FOrder 
		{
			get{return EConvert.ToInt(base.GetProperty("FOrder"));}
			set{base.SetProperty("FOrder",value);}
		}
		/// <summary>
		///
		/// </summary>
		public int FType 
		{
			get{return EConvert.ToInt(base.GetProperty("FType"));}
			set{base.SetProperty("FType",value);}
		}
	}
}
