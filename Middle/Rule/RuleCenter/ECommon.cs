//using System;
//using System.Data;
//using System.Text;
//using System.Collections;
//using Ylgl.RuleBase;
//using Ylgl.EntityBase;
//using Ylgl.EntityEnterprise;
//using Ylgl.PersistBase;
//using Ylgl.PersistEnterprise;
//
//namespace Ylgl.RuleEnterprise
//{
//	/// <summary>
//	/// Commmon ��ժҪ˵����
//	/// </summary>
//	public class ECommon:RBase
//	{
//		private PEnt m_pent;
//
//		public PEnt pent
//		{
//			get
//			{
//				if(m_pent==null)
//					m_pent = new PEnt();
//				return m_pent;
//			}
//		}
////		private IConnection Cn;
//		
//		//=========================================SELECT===========================================
//		#region PEntSelect
//		//��ҵҵ����
//		#region Achievement
//		public EProjectAchievement GetProjectAchievement(string ReduceOption,string condition)
//		{
////			
////
////			
////			try
////			{
//				return pent.GetProjectAchievement(null, ReduceOption,condition);
////			}
////			catch(Exception ex)
////			{
////				throw ex ; 
////			}
////			finally
////			{
////						
////			}	
//		}
//		public DataTable GetProjectALL(string EFId,string Condition)
//		{
////			
////			
////			try
////			{
//				return pent.GetProjectAchievementAll(null, EFId,Condition);
////			}
////			catch(Exception ex)
////			{
////				throw ex ; 
////			}
////			finally
////			{
////						
////			}	
//		}
//		#endregion
//		//��ȫ���֤
//		#region GetSafetyNo
//		public ESafetyNo GetSafetyNo(string ReduceOption,string condition)
//		{
////			
////			try
////			{
////				
//
//				return pent.GetSafetyNo(null,ReduceOption,condition);
////			}
////			catch(Exception ex)
////			{
////				throw ex ; 
////			}
////			finally
////			{
////						
////			}	
//		}
//
//		public DataTable GetAllSafetyNo( string strReduce, string strCond, int nPageSize, int nCurrPage )
//		{
//			DataTable dt = null;
//
//			dt = this.pent.GetPageTable( null, EntityTypeEnum.ESafetyNo, strReduce, strCond, nPageSize, nCurrPage );
//
//			return dt;
//		}
//
//		public int CountAllSafetyNo( string strCond )
//		{
//			int nCount = 0;
//
//			nCount = this.pent.Count( null, EntityTypeEnum.ESafetyNo, strCond );
//
//			return nCount;
//		}
//
//		public string GetCurrYearMaxNumber( string strYear )
//		{
//			StringBuilder sb = new StringBuilder();
//
//			sb.Append( "select max(SUBSTRING(FNumber, LEN(FNumber) - 5, 6)) as fnumber from Sf_Ent_SafetyNo where year(fcreatetime) = '"+strYear+"'" );
//
//			string strRtn = "";
//
//			object obj = this.pent.ExecuteSql( null, sb.ToString(), SqlResultEnum.Have );
//			
//			if( obj == null || obj.ToString() == "" )
//			{
//				strRtn = "000001";
//			}
//			else
//			{
//				int nNo = EConvert.ToInt( obj )+1;
//
//				switch( nNo.ToString().Length )
//				{
//					case 1:
//						strRtn = "00000"+nNo.ToString();
//						break;
//					case 2:
//						strRtn = "0000"+nNo.ToString();
//						break;
//					case 3:
//						strRtn = "000"+nNo.ToString();
//						break;
//					case 4:
//						strRtn = "00"+nNo.ToString();
//						break;
//					case 5:
//						strRtn = "0"+nNo.ToString();
//						break;
//				}
//			}
//
//			return strRtn;
//		}
//		
//		#endregion
//		//��ȫ���������������ϱ�
//		#region GetSafetyNoApply
//		public ESafetyNoApply GetSafetyNoApply(string ReduceOption,string condition)
//		{
//			
//			try
//			{
//				
//
//				return pent.GetSafetyNoApply(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ���������
//		#region BaseInfo
//		public EBaseInfo GetBaseInfo(string ReduceOption,string condition)
//		{
//			
//			try
//			{
//				
//
//				return pent.GetBaseInfo(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool IsBaseInfoExist(string FBaseInfoId)
//		{
//			
//			try
//			{
//				
//
//				return pent.IsBaseInfoExist(null,FBaseInfoId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//���ŵ�����ȡ��
//		//		public string GetManageDeptNameById(string ManageDeptId)
//		//		{
//		//			
//		//
//		//			
//		//			
//		//			StringBuilder sb = new StringBuilder();
//		//			sb.Append("select fname from gov_managedept where fid='");
//		//			sb.Append(ManageDeptId);
//		//			sb.Append("' and fisdeleted<>1");
//		//			try
//		//			{
//		//				Object obj = Cn.Execute(sb.ToString(),SqlResultEnum.Have);
//		//				if(obj!=null)
//		//					return (string)obj;
//		//				return "";
//		//			}
//		//			catch(Exception ex)
//		//			{
//		//				throw ex ; 
//		//			}
//		//			finally
//		//			{
//		//						
//		//			}	
//		//		}
//
//		//		public string GetManageDeptNameByNum(string Num)
//		//		{
//		//			
//		//
//		//			
//		//
//		//			StringBuilder sb = new StringBuilder();
//		//			sb.Append("select fname from gov_managedept where fnumber='");
//		//			sb.Append(Num);
//		//			sb.Append("' and fisdeleted<>1");
//		//			try
//		//			{
//		//				Object obj = Cn.Execute(sb.ToString(),SqlResultEnum.Have);
//		//				if(obj!=null)
//		//					return (string)obj;
//		//				return "";
//		//			}
//		//			catch(Exception ex)
//		//			{
//		//				throw ex ; 
//		//			}
//		//			finally
//		//			{
//		//						
//		//			}	
//		//		}
//		//		public EManageDept[] GetProvince(string Root)
//		//		{
//		//			return pent.GetProvince(Root);
//		//		}
//
//		public DataTable GetArea(string fnumber,int flevel)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetArea(null,fnumber,flevel);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public string GetManageDeptNameByNum(string fnumber)
//		{
//			try
//			{
//				return pent.GetManageDeptNameByNum(null,fnumber);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//		public bool IsCity(string FManageDeptId,string FManageDeptNames)
//		//		{
//		//			return pent.IsCity(FManageDeptId,FManageDeptNames);
//		//		}
//		//
//		//		public EManageDept[] GetCity(string Province)
//		//		{
//		//			return pent.GetCity(Province);
//		//		}
//
//
//	
//		//�õ����ı�����
//		public ELongText GetEntLongText(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetEntLongText( null,ReduceOption,condition);	
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//�õ���Ƭ
//		public EEntPhoto GetEntPhoto(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetEntPhoto(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public object GetPic(string FId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetPic(null,FId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��Ա֤���
//		#region Certificate
//		public ECertificate GetCertificate(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetCertificate(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//�ϱ����ı�����
//		#region ESafetyAchievement
//		public ESafetyAchievement GetSafetyAchievement(string ReduceOption,string condition)
//		{
//			try
//			{
//				return pent.GetSafetyAchievement(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ��е�豸��
//		#region Device
//		public EDevice GetDevice(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetDevice(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public DataTable GetDeviceAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetDeviceAll(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ��Ա��
//		#region Employee
//		public Safety.EntityEnterprise.EEmployee GetEmployee(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetEmployee(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public DataTable GetEmployeeAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetEmployeeAll(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public DataTable GetEmployeeOne(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetEmployeeOne(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		//��Ա������
//		#region resume
//		public EResume GetResume(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetResume(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public DataTable GetResumeAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetResumeAll(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//�μӹ�����Ա��
//		#region attentprojectmumber
//		public EAttentProjectMumber GetAttentProjectMumber(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAttentProjectMumber(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		
//		//ͨ�������ʾ����ѯ��ҵ����Ա
//		public DataTable GetProjectMumberAll(string EFId,string FBaseInfoId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetProjectMumberAll(null,EFId,FBaseInfoId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		/// ��ù����豸�б�
//		#region attentprojectDevice
//		public EProjectDevice GetAttentProjectDevice(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAttentProjectDevice(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public DataTable GetProjectDeviceAll(string EFId,string FBaseInfoId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetProjectDeviceAll(null, EFId,FBaseInfoId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����֤���
//		#region CertificateQualification
//		public ECertificateQualification GetCertificateQualification(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetCertificateQualification(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		//�����ҵ���е�����
//		public DataTable GetCertiQualiAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetCertiQualiAll(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		//�������ر�
//		#region CivilizationPlace
//		public ECivilizationPlace GetCivilizationPlace(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetCivilizationPlace(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//�����ҵ���е���������
//		public DataTable GetCivilPlaceAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetCivilPlaceAll(null,EFId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ѵ�����
//		#region TrainMumber
//		public ETrainMumber GetTrainMumber(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetTrainMumber(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//
//		//������Ա����
//		#region ETrainApply
//		public ETrainApply GetTrainApply(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetTrainApply(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ�¹ʱ�
//		#region Accident
//		public EAccident GetAccident(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAccident(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//�����ҵ���е��¼�
//		public DataTable GetAccidentAll(string EFId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAccidentAll(null,EFId); 
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//�������Ա��
//		#region AccidentpentearchMumber
//		public EAccidentResearchMumber GetAccidentResearchMumber(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAccidentResearchMumber(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//�����ҵ���еĵ�����Ա
//		public DataTable GetAccidResearchAll(string EFId,string EProjectId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAccidResearchAll(null,EFId,EProjectId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		
//		//������Ա��
//		#region DeathMumber
//		public EDeathMumber GetDeathMumber(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetDeathMumber(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//�����ҵ���еĵ�����Ա
//		public DataTable GetDeathMumberAll(string EFId,string EAccidentId)
//		{
//			
//
//			
//			try
//			{
//				return pent. GetDeathMumberAll(null,EFId,EAccidentId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��½�û���Ϣ��
//		#region getuser
//		public EUser GetUser(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetUser(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//�õ���������
//		#region getProcessApp
//		public DataTable GetProcessInstance(string FLevel,string FManageDeptId)
//		{
//			
//
//			
//			try
//			{
//				return pent. GetProcessInstance(null,FLevel,FManageDeptId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}		
//		#endregion
//		//ESafetyBizLevel
//		#region ESafetyBizLevel
//		public ESafetyBizLevel GetSafetyBizLevel(string ReduceOption,string Condi)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetSafetyBizLevel(null,ReduceOption,Condi);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}		
//		#endregion
//		//�õ���������ID
//		#region GetProcessInsByOper
//		public string GetProcessInsByOper(string OperId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetProcessInsByOper(null,OperId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}		
//		#endregion
//		//�õ�������˼�¼
//		#region GetProceRecord
//		public EProcRecord GetAppRecord(string ReduceOption,string condition)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetAppRecord(null,ReduceOption,condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}		
//		#endregion
//
//		#endregion
//		//=========================================����,����,ɾ��===========================================
//		#region update
//		//��ҵ������Ϣ��
//		#region BaseInfo
//		public bool SaveBaseInfo(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveBaseInfo(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}	
//		#endregion
//		//��ȫ���֤
//		#region SaveSafetyNo
//		public bool SaveSafetyNo(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveSafetyNo(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool SaveSafetyNo(ESafetyNo eSN, SaveOptionEnum soe)
//		{
//			return pent.Save(null,eSN, "fid,fvalidbegin,fvalidend", soe);				
//		}
//
//		#endregion
//		//��ȫ������������������
//		#region SaveSafetyNoApply
//		public bool SaveSafetyNoApply(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveSafetyNoApply(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//���ı�
//		#region EntLongText
//		public bool SaveLongText(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveLongText(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//ͼƬ
//		#region EntPhto
//		public bool SaveEntPhoto(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveEntPhoto(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ����
//		#region Achievement 
//		public bool SaveProjectAchievement(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveProjectAchievement(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DelProjectAchievement(string condition,bool IsFact)
//		{
//			
//
//						
//			try
//			{
//				return pent.DelProjectAchievement(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		// ����μӹ�����Ա
//		#region attentproject
//		public bool SaveAttenProjectMumber(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//						
//			try
//			{
//				return pent.SaveAttenProjectMumber(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteAttenProjectMumber(string condition,bool IsFact)
//		{
//			
//
//						
//			try
//			{
//				return pent.DeleteAttenProjectMumber(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		// ����μӹ����豸
//		#region longtext
//		public bool SaveAttenProjectDevice(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveAttenProjectDevice(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteAttenProjectDevice(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteAttenProjectDevice(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		// ������ҵ����
//		#region CertificationQualification
//		public bool SaveCertifiQuali(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveCertifiQuali(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteCertifiQuali(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteCertifiQuali(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		// ������ҵ�¼�
//		#region accident
//		public bool SaveAccident(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveAccident(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteAccident(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteAccident(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		// ����������Ա
//		#region accidentResearchMumber
//		public bool SaveAccidentResearchMumber(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveAccidentResearchMumber(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteAccidentResearchMumber(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteAccidentResearchMumber(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		// ����������Ա
//		#region DeathMumber
//		public bool SaveDeathMumber(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveDeathMumber(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteDeathMumber(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteDeathMumber(null,condition,IsFact);
//			}
//		
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		//��Ա֤���
//		#region Certificate
//		public bool SaveCertificate(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveCertificate(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool DelCertificate(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DelCertificate(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//�ϱ�����
//		#region SaveSafetyAchievement
//		public bool SaveSafetyAchievement(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveSafetyAchievement(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool DelSafetyAchievement(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DelSafetyAchievement(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ��е�豸��
//		#region Device
//		public bool SaveDevice(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveDevice(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool DelDevice(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DelDevice(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��ҵ��Ա��
//		#region Employee
//		public bool SaveEmployee(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveEmployee(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool SaveEmployee(IDictionary dict1,IDictionary dict2,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveEmployee(null,dict1,dict2,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		public bool DelEmployee(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DelEmployee(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//��Ա������
//		#region pesume
//		public bool SaveResume(IDictionary dict,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveResume(null,dict,"FID",soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		//ɾ��һ����Ա������Ϣ
//		public bool DeleteResume(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteResume(null,condition,IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����user
//		#region user
//		public bool SaveUser(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveUser(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//
//		// ������������
//		#region CivilizationPlace
//		public bool SaveCivilizationPlace(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveCivilizationPlace(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteCivilizationPlace(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteCivilizationPlace(null,condition,IsFact);
//			}
//		
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//
//		// ��ѵ����
//		#region TrainMumber
//		public bool SaveTrainMumber(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveTrainMumber(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteTrainMumber(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteTrainMumber(null,condition,IsFact);
//			}
//		
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//
//		// ������Ա����
//		#region TrainApply
//		public bool SaveTrainApply(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveTrainApply(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		public bool DeleteTrainApply(string condition,bool IsFact)
//		{
//			
//
//			
//			try
//			{
//				return pent.DeleteTrainApply(null,condition,IsFact);
//			}
//		
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
//		#endregion
//		// ������Ա����
//		#region Resume
//		public bool SaveResume(IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//
//			
//			try
//			{
//				return pent.SaveResume(null,dict,keyField,soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//
////		public bool DeleteResume(string condition,bool IsFact)
////		{
////			
////
////			
////			try
////			{
////				return pent.DeleteResume(null,condition,IsFact);
////			}
////		
////			catch(Exception ex)
////			{
////				throw ex ; 
////			}
////			finally
////			{
////						
////			}	
////		}
//
//		#endregion
//		//ɾ����˷������
//		#region user DelProcessRecord
//		public bool DelProcessRecord(string OperId)
//		{
//			
//
//			
//			try
//			{
//				return pent.DelProcessRecord(null,OperId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		#endregion
//		//=========================================ͨ�÷���===========================================
//		#region common
//		//ִ��SQL
//		#region ExcuteSql
//		public bool ExcuteSql(string sql)
//		{
//			
//			try
//			{
//				
//				return pent.ExcuteSql(null,sql);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//���ݿ�ʼ�ͽ����У����ر�
//		#region GetTable
//		public DataTable GetTable(string sql,int startrow,int endrow)
//		{
//			
//			try
//			{
//				
//				return pent.GetTable(null,sql,startrow,endrow);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//���ؼ�¼��
//		#region GetCount
//		public int GetCount(string sql)
//		{
//			
//			try
//			{
//				
//				return pent.GetCount(null,sql);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//
//		}
//		#endregion
//		//����SQL�����ر�
//		#region GetTable
//		public DataTable GetTable(string sql)
//		{
//			
//			try
//			{
//				
//				return pent.GetTable(null,sql);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//
//		#region GetListString
//		public string GetListString(string ReduceOption,EntityTypeEnum EntityType ,string condition)
//		{
//			return pent.GetListString(ReduceOption,EntityType ,condition);
//		}
//		#endregion
//		//����������
//		#region SaveMuliTable
//		public bool SaveMuliTable(IDictionary dict1,IDictionary dict2,EntityTypeEnum EntityType1,EntityTypeEnum EntityType2,string keyField,SaveOptionEnum soe1,SaveOptionEnum soe2)
//		{
//			
//			try
//			{
//				
//				return pent.SaveMuliTable(null, dict1, dict2, EntityType1, EntityType2, keyField, soe1,soe2);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����������
//		#region SaveMuliTable
//		public bool SaveMuliTable(IDictionary dict1,IDictionary dict2,IDictionary dict3,EntityTypeEnum EntityType1,EntityTypeEnum EntityType2,EntityTypeEnum EntityType3,string keyField,SaveOptionEnum soe1,SaveOptionEnum soe2,SaveOptionEnum soe3)
//		{
//			
//			try
//			{
//				
//				return pent.SaveMuliTable(null, dict1, dict2,dict3, EntityType1, EntityType2,EntityType3, keyField, soe1,soe2,soe3);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����ID�����ع��������ֱ���
//		#region GetManageDeptNumber
//		public string GetManageDeptNumber(string ManageDeptId)
//		{
//			
//			try
//			{
//				
//				return pent.GetGovManageDeptNumber(null,ManageDeptId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����ID�����ع�������Ϣ
//		#region GetManageDept
//		public EManageDept GetManageDept(string ReduceOption,string Condi)
//		{
//			
//			try
//			{
//				
//				return pent.GetManageDept(null,ReduceOption,Condi);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����ID�����ع���������
//		#region GetManageDeptNameById
//		public string GetManageDeptNameById(string ManageDeptId)
//		{
//			
//			try
//			{
//				
//				return pent.GetManageDeptNameById(null,ManageDeptId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����ID�����ع�����ȫ��
//		#region GetManageDeptFullNameById
//		public string GetManageDeptFullNameById(string ManageDeptId)
//		{
//			
//			try
//			{
//				
//				return pent.GetManageDeptFullNameById(null,ManageDeptId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		#region GetGovManageDeptSpecialtyId
//		public DataTable GetGovManageDeptSpecialtyId(string FLevel,string FManageDeptId,string FSpecialtyDeptTypeId)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetGovManageDeptSpecialtyId(null,  FLevel, FManageDeptId, FSpecialtyDeptTypeId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		#region GetGovManageDeptData
//		public string GetGovManageDeptData(string ReduceOption,string Condi)
//		{
//			
//
//			
//			try
//			{
//				return pent.GetGovManageDeptData(null, ReduceOption,Condi);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//IsCertificateApply
//		#region IsCertificateApply
//		public bool IsCertificateApply(string FId)
//		{
//			
//
//			
//			try
//			{
//				return pent.IsCertificateApply(null, FId);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//select����
//		#region getbase��ͨ�÷���
//		public IEBase GetEBase(string ReduceOption,string condition,EntityTypeEnum EntityType,bool Version,string EndTime)
//		{
//			
//
//			
//			try
//			{
//				if (Version)
//				{
//					if (EndTime.Trim()!="")
//						if (condition.Trim()!="")
//							condition+=" and FValidEnd='"+EndTime.Trim()+"'";
//						else
//							condition="FValidEnd='"+EndTime.Trim()+"'";
//					else
//					{
//						if (condition.Trim()!="")
//							condition+=" and FValidEnd='9999-12-31 23:59:59'";
//						else
//							condition="FValidEnd='9999-12-31 23:59:59'";
//					}
//				}
//				return pent.GetEBase(null, ReduceOption,condition,EntityType);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//
//		#region getbase��ͨ�÷���
//		public IEBase GetEBase(string ReduceOption,string condition,EntityTypeEnum EntityType,bool Version,string EndTime,string BeginTime)
//		{
//			
//
//			
//			try
//			{
//				if (Version)
//				{
//					if (EndTime.Trim()!="")
//						if (condition.Trim()!="")
//							condition+=" and FValidEnd>='"+EndTime.Trim()+"' and fvalidbegin<='"+BeginTime+"'";
//						else
//							condition="FValidEnd>='"+EndTime.Trim()+"' and fvalidbegin<='"+BeginTime+"'";
//					
//				}
//				return pent.GetEBase(null, ReduceOption,condition,EntityType);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//
//		//��������
//		#region SaveWithVersion
//		public bool SaveWithVersion(IDictionary dic,string FID,EntityTypeEnum EntityType,string keyField,SaveOptionEnum soe)
//		{
//			
//			try
//			{
//				
//				return pent.SaveWithVersion(null, dic, FID, EntityType, keyField, soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//ɾ������
//		#region DeleteWithVersion
//		public bool DeleteWithVersion(string FID,EntityTypeEnum EntityType,string EFID,string IsFact)
//		{
//			
//			
//			try
//			{
//				
//				return pent.DeleteWithVersion(null, FID, EntityType, EFID, IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}		
//		}
//		#endregion
//		
//		//�����汾�ı���ͨ�÷���
//		#region SaveEBase
//		public bool SaveEBase(EntityTypeEnum EntityType,IDictionary dict,string keyField,SaveOptionEnum soe)
//		{
//			
//			try
//			{
//				
//				return pent.SaveEBase(null,EntityType, dict, keyField, soe);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}		
//		}	
//		#endregion
//
//		#region delEbase
//		public bool DelEBase(IConnection Cn,EntityTypeEnum EntityType,string condition,bool IsFact)
//		{
//			
//			try
//			{
//				
//				return pent.DelEBase(null,EntityType, condition, IsFact);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}		
//		}
//		#endregion
//
//		//��ȡ��ҵ���°汾
//		#region GetLastVersion
//		public EVersion GetLastVersion(IConnection Cn,string FID,int FIsDeleted,string EndTime)
//		{
//			
//
//			try
//			{
//				
//				return pent.GetLastVersion(null,FID, FIsDeleted, EndTime);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}				
//		}
//		
//		#endregion
//
//		//�γ��°汾
//		public void CreateNewVersion( string FBaseinfoID )
//		{
//			try
//			{
//				this.pent.CreateNewVersion( null, FBaseinfoID );
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//		}
//
//		//��ȡ���°汾��ǰһ�汾
//		public EVersion GetPreLastVersion( string FBaseinfoID )
//		{
//			try
//			{
//				return this.pent.GetPreLastVersion( null, FBaseinfoID );
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//		}
//
//		//ִ��һ��SQL,����string
//		#region GetString
//		public string GetString( EntityTypeEnum EntityType,string ReduceOption,string condition)
//		{
//			
//			
//			
//			try
//			{
//				return pent.GetString(null, EntityType, ReduceOption, condition);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//
//		//ִ��һ��SQL,����string
//		#region GetString
//		public string GetString( string SQL)
//		{
//			
//			
//			
//			try
//			{
//				return pent.GetString(null, SQL);
//			}
//			catch(Exception ex)
//			{
//				throw ex ; 
//			}
//			finally
//			{
//						
//			}	
//		}
//		#endregion
//		//����һ�����������ϼ�����
//		//���������ϼ������б�
//		#region GetReportDept
//		public SortedList GetReportDept(string FDeptId)
//		{
//			string FNumberTemp="";
//			SortedList sl=new SortedList();
//			sl.Add("","");
//			//ECommon Ebi=new ECommon();
//			string FNumber=this.GetManageDeptNumber(FDeptId);
//			string[] DeptIdA=FNumber.Split(".".ToCharArray());
//			if (DeptIdA.Length>1)
//			{
//				
//				for (int i=1;i<DeptIdA.Length;i++)
//				{
//					//�õ�ÿһ�����ű���
//					FNumberTemp="";
//					for (int j=0;j<=i;j++)
//					{
//						if (FNumberTemp=="")
//							FNumberTemp=DeptIdA[j];
//						else
//							FNumberTemp+="."+DeptIdA[j];
//					}
//					EManageDept Em=this.GetManageDept("FID,FNAME","FNUMBER='"+FNumberTemp+"' AND fisdeleted=0");
//					sl.Add(FNumberTemp,Em.FName);//����NUMBER
//				}
//				return sl;
//			}
//			else
//			{
//				return null;
//			}
//		}
//		#endregion
//		#endregion
//	}
//}
