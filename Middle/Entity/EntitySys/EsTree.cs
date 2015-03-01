using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;

namespace Approve.EntitySys
{
	/// <summary>
	/// EPTree 的摘要说明。
	[Serializable]
	public class EsTree: EBase
	{
		public EsTree():base()
		{
            m_EntityType = EntityTypeEnum.EsTree;
		}

		public EsTree(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsTree;
		}


		public EsTree(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsTree;
		}

		public EsTree(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EsTree;	
		}
		public string FDerived 
		{
			get{return EConvert.ToString(base.GetProperty("FDerived"));}
			set{base.SetProperty("FDerived",value);}
		}

        public string FRoleId
        {
            get { return EConvert.ToString(base.GetProperty("FRoleId")); }
            set { base.SetProperty("FRoleId", value); }
        }

		/// <summary>
		///
		/// </summary>
		public string FName 
		{
			get{return EConvert.ToString(base.GetProperty("FName"));}
			set{base.SetProperty("FName",value);}
		}
		/// <summary>
		///
		/// </summary>
		public string FPicName 
		{
			get{return EConvert.ToString(base.GetProperty("FPicName"));}
			set{base.SetProperty("FPicName",value);}
		}

        public string FSelcePicName
        {
            get { return EConvert.ToString(base.GetProperty("FSelcePicName")); }
            set { base.SetProperty("FSelcePicName", value); }
        }

        public string FExpPicName
        {
            get { return EConvert.ToString(base.GetProperty("FExpPicName")); }
            set { base.SetProperty("FExpPicName", value); }
        }
		/// <summary>
		///
		/// </summary>
		public string FParentName 
		{
			get{return EConvert.ToString(base.GetProperty("FParentName"));}
			set{base.SetProperty("FParentName",value);}
		}
		/// <summary>
		///
		/// </summary>
		public string FParent
		{
            get { return EConvert.ToString(base.GetProperty("FParent")); }
            set { base.SetProperty("FParent", value); }
		}
		/// <summary>
		///
		/// </summary>
		public int FLevel 
		{
			get{return EConvert.ToInt(base.GetProperty("FLevel"));}
			set{base.SetProperty("FLevel",value);}
		}
		/// <summary>
		///
		/// </summary>
		public int FOrder 
		{
			get{return EConvert.ToInt(base.GetProperty("FOrder"));}
			set{base.SetProperty("FOrder",value);}
		}

        public string FWebUrl 
		{
            get { return EConvert.ToString(base.GetProperty("FWebUrl")); }
            set { base.SetProperty("FWebUrl", value); }
		}

		public string FAdminUrl
		{
			get{return EConvert.ToString(base.GetProperty("FAdminUrl"));}
			set{base.SetProperty("FAdminUrl",value);}
		}

        public int FNumber
		{
            get { return EConvert.ToInt(base.GetProperty("FNumber")); }
            set { base.SetProperty("FNumber", value); }
		}
	}
}
