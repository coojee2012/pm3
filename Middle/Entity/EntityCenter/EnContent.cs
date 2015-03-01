using System;
using System.Collections;
using System.Data;
using Approve.EntityBase;

namespace Approve.EntityCenter
{
	/// <summary>
	/// EText ��ժҪ˵����
	/// </summary>
	/// 
    [Serializable]
	public class EnContent:EEnterpriseBase
	{
		public EnContent()
		{
            base.m_EntityType = EntityTypeEnum.EnContent;
		}

		public EnContent(IDictionary dict):base(dict)
		{
            base.m_EntityType = EntityTypeEnum.EnContent;
		}

		public EnContent(DataRow dr):base(dr)
		{
            base.m_EntityType = EntityTypeEnum.EnContent;
		}

		public EnContent(DataRow dr,IDictionary rel):base(dr,rel)
		{
            base.m_EntityType = EntityTypeEnum.EnContent;
		}
		
		public string Ftype 
		{
			get{return EConvert.ToString(base.GetProperty("Ftype"));}
			set{base.SetProperty("Ftype",value);}
		}
		public string Ftext 
		{
			get{return EConvert.ToString(base.GetProperty("Ftext"));}
			set{base.SetProperty("Ftext",value);}
		}
		
		#region ��ҵ��Ƭ����
		

		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string FContent
		{
			get{return (string)GetProperty("FContent");}
			set{SetProperty("FContent",value);}
		}
		#endregion
	}
}
