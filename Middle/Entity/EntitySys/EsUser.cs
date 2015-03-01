using System;
using System.Collections;
using Approve.EntityBase;
using System.Data;


namespace Approve.EntitySys
{
	/// <summary>
	/// UserEntity 的摘要说明。
	/// </summary>
	[Serializable]
	public class EsUser: EBase
	{
		
		#region 公共属性

		public string FIdentityCard
		{
			get{return EConvert.ToString(GetProperty("FIdentityCard"));}
			set{SetProperty("FIdentityCard",value);}
		}


		public string FEmail
		{
			get{return EConvert.ToString(GetProperty("FEmail"));}
			set{SetProperty("FEmail",value);}
		}


		public string FTEL
		{
			get{return EConvert.ToString(GetProperty("FTEL"));}
			set{SetProperty("FTEL",value);}
		}


		public string FLockNumber
		{
			get{return EConvert.ToString(GetProperty("FLockNumber"));}
			set{SetProperty("FLockNumber",value);}
		}

		public string FName
		{
			get{return EConvert.ToString(GetProperty("FName"));}
			set{SetProperty("FName",value);}
		}

        public string FPWD
		{
            get { return EConvert.ToString(GetProperty("FPWD")); }
            set { SetProperty("FPWD", value); }
		}

		public string FManageDeptId
		{
			get{return EConvert.ToString(GetProperty("FManageDeptId"));}
			set{SetProperty("FManageDeptId",value);}
		}
        public int FCount
        {
            get { return EConvert.ToInt(GetProperty("FCount")); }
            set { SetProperty("FCount", value); }
        }
		public int FState
		{
			get{return EConvert.ToInt(GetProperty("FState"));}
			set{SetProperty("FState",value);}
		}

        public string FBaseId
		{
            get { return EConvert.ToString(GetProperty("FBaseId")); }
            set { SetProperty("FBaseId", value); }
		}

		public string FCreateTime
		{
			get{return EConvert.ToString(GetProperty("FCreateTime"));}
			set{SetProperty("FCreateTime",value);}
		}

		public string FEmployeeId
		{
			get{return EConvert.ToString(GetProperty("FEmployeeId"));}
			set{base.SetProperty("FEmployeeId",value);}
		}
		public int FType
		{
			get{return EConvert.ToInt(GetProperty("FType"));}
			set{SetProperty("FType",value);}
		}

		public int FFirst
		{
			get{return EConvert.ToInt(GetProperty("FFirst"));}
			set{SetProperty("FFirst",value);}
		}

		public string FDocumentOrder
		{
			get{return EConvert.ToString(GetProperty("FDocumentOrder"));}
			set{SetProperty("FDocumentOrder",value);}
		}

		public string FUserGroupID
		{
			get{return EConvert.ToString(GetProperty("FUserGroupID"));}
			set{SetProperty("FUserGroupID",value);}
		}

		public string FLockLabelNumber
		{
			get{return EConvert.ToString(GetProperty("FLockLabelNumber"));}
			set{SetProperty("FLockLabelNumber",value);}
		}

		public string FRoleId
		{
			get{return EConvert.ToString(GetProperty("FRoleId"));}
			set{SetProperty("FRoleId",value);}
		}

		public string FSystemCode
		{
			get{return EConvert.ToString(GetProperty("FSystemCode"));}
			set{SetProperty("FSystemCode",value);}
		}

		public string FRegion
		{
			get{return EConvert.ToString(GetProperty("FRegion"));}
			set{SetProperty("FRegion",value);}
		}

		public string FSystemReght
		{
			get{return EConvert.ToString(GetProperty("FSystemReght"));}
			set{SetProperty("FSystemReght",value);}
		}
		public string FProblemReght
		{
			get{return EConvert.ToString(GetProperty("FProblemReght"));}
			set{SetProperty("FProblemReght",value);}
		}

		public string FCompany
		{
			get{return EConvert.ToString(GetProperty("FCompany"));}
			set{SetProperty("FCompany",value);}
		} 
	 
		public DateTime FAvailabilityDate
		{
			get{return EConvert.ToDateTime(GetProperty("FAvailabilityDate"));}
			set{SetProperty("FAvailabilityDate",value);}
        }
       
		
		
		#endregion

		public EsUser():base()
		{
            m_EntityType = EntityTypeEnum.EsUser;
		}

		public EsUser(IDictionary iDictionary):base(iDictionary)
		{
            m_EntityType = EntityTypeEnum.EsUser;
		}

		public EsUser(DataRow dr):base(dr)
		{
            m_EntityType = EntityTypeEnum.EsUser;
		}
		public EsUser(DataRow dr,IDictionary rel):base(dr,rel)
		{
            m_EntityType = EntityTypeEnum.EsUser;	
		}

    

        public string FPassWord
        {
            get { return EConvert.ToString(base.GetProperty("FPassWord")); }
            set { base.SetProperty("FPassWord", value); }
        }

     
        public string FFunction
        {
            get { return EConvert.ToString(base.GetProperty("FFunction")); }
            set { base.SetProperty("FFunction", value); }
        }

        public string FLinkMan
        {
            get { return EConvert.ToString(base.GetProperty("FLinkMan")); }
            set { base.SetProperty("FLinkMan", value); }
        }

        public string FDepartmentID
        {
            get { return EConvert.ToString(base.GetProperty("FDepartmentID")); }
            set { base.SetProperty("FDepartmentID", value); }
        }

        

       
	 
	}
}
