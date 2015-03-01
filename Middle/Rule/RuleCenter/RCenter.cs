using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Approve.PersistEnterprise;
using Approve.PersistBase;
using Approve.RuleBase;
using Approve.EntityBase;
using Approve.EntityCenter;
using System.EnterpriseServices;

namespace Approve.RuleCenter
{

    public class RCenter : RBase
    {

        private PEnt m_pes;

        public RCenter()
        {
            m_pes = null;
            this.pDBName = "dbCenter";
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
        }
        public RCenter(string pDBName)
        {
            m_pes = null;
            if (pDBName == "")
            {
                this.pDBName = "dbCenter";
            }
            else
            {
                this.pDBName = pDBName;
            }
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
        }
        public RCenter(int iType)
        {
            this.pDBName = "dbCenter";
        }

      
        private PEnt pes
        {
            get
            {
                if (m_pes == null)
                    m_pes = new PEnt();
                return m_pes;
            }
        }

        public bool isHaveRight(string FUserId, string FSystemId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_Sys_UserRight where fuserid='" + FUserId + "' and FSystemId='" + FSystemId + "'");
            int iCount = this.GetSQLCount(sb.ToString());
            if (iCount <= 0) return false;
            return true;
        }
        //fEntId��ҵ����
        public string GetBadCodeType(string fBaseInfoId)
        {
            string fUserId = this.GetSignValue(EntityTypeEnum.EsUser, "FId", "FBaseInfoId='" + fBaseInfoId + "'");
            if (fUserId == null || fUserId == "")
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("select FSystemId from CF_Sys_UserRight where FUserId='" + fUserId + "'");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }
            sb.Remove(0, sb.Length);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append(GetBadByEntType(dt.Rows[i]["FSystemId"].ToString()));
                }
                else
                {
                    sb.Append("," + GetBadByEntType(dt.Rows[i]["FSystemId"].ToString()));
                }
            }
            return sb.ToString();
        }

        //�õ���ͬ������ҵ�Ĳ�����Ϊ��׼���
        public string GetBadByEntType(string fEntTypeId)
        {
            switch (fEntTypeId)
            {
                case "101": //ʩ����ҵ
                    return "'D1'";
                case "120": //�б������ҵ��ҵ
                    return "'F1'";
                case "125": //���̼�����ҵ
                    return "'E1'";
                case "130": //���ز���ҵ
                    return "''";
                case "135": //԰���̻���ҵ
                    return "''";

                case "140": //�������������ҵ
                    return "''";

                case "145": //ʩ��ͼ������
                    return "'I1'";

                case "155": //���������ҵ
                    return "'B1','C1'";

                case "175": //����������
                    return "'H1'";

                case "180": //��ʡ����ʩ����ҵ
                    return "'D1'";

                case "185": //�����ѯ��ҵ
                    return "'G1'";


                default:
                    return "''";
            }
        }

        public string Test(string str1, string str2)
        {
            return str1 + str2;
        }

        //�������ʱ�ŵõ����ʵȼ�
        public int GetQualiLevelByNumber(string fLevelNumber)
        {
            if (fLevelNumber == null || fLevelNumber == "")
            {
                return -1;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(" select flevel from CF_Sys_QualiLevel");
            sb.Append(" where fnumber =");
            sb.Append(fLevelNumber);
            string fQualiLevel = this.GetSignValue(sb.ToString());
            try
            {
                return EConvert.ToInt(fQualiLevel);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        //�ж�����������Ƿ���������
        public bool IsNewQuali(string fListId, string fTypeId, string fLevelId, string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" fListId='" + fListId + "' ");
            sb.Append(" and fTypeId='" + fTypeId + "'");
            sb.Append(" and fLevelId='" + fLevelId + "'");
            sb.Append(" and fBaseInfoId='" + fBaseInfoId + "' ");
            sb.Append(" and fstate=1 ");
            string fid = this.GetSignValue(EntityTypeEnum.EbQualiCertiTrade, "FId", sb.ToString());
            if (fid != null && fid != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //�ж���ҵ�Ƿ�������
        public bool IsHasPQuali(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fid from CF_Ent_QualiCertiTrade where fbaseinfoid='" + fBaseInfoId + "'");
            sb.Append(" and FIsBase=1 and FState=1");
            string fId = this.GetSignValue(sb.ToString());
            if (fId != null && fId != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //�ж���ҵ�Ƿ�������
        public bool IsHasOQuali(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fid from CF_Ent_QualiCertiTrade where fbaseinfoid='" + fBaseInfoId + "'");
            sb.Append(" and FIsBase<>1 and FState=1");
            DataTable dt = GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //�ж���ҵ�Ƿ�������֤��
        public bool IsHasQualiCerti(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_Ent_QualiCerti ");
            sb.Append(" where fbaseinfoid='" + fBaseInfoId + "' ");
            sb.Append(" and FIsValid=1 ");

            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //�Ƿ��а�ȫ�������֤��
        public bool isHaveSafeCerti(string FBaseinfoId)
        {
            string FCertiNo = this.GetSignValue(EntityTypeEnum.EbSafetyCerti, "fid", "fbaseinfoid='" + FBaseinfoId + "' and FIsDeleted=0 and FIsValid=1 ");
            if (FCertiNo != null && FCertiNo != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //�ж���ҵ�Ƿ��н���ʦ
        public bool IsHaveConstructPerson(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_Construct_UserInfo ");
            sb.Append(" where fbaseinfoid='" + fBaseInfoId + "' ");
            sb.Append(" and fisdeleted=0 ");

            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //ע����ҵ
        public bool EntLogout(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();

            //ArrayList arrEn = new ArrayList();
            //ArrayList arrSl = new ArrayList();
            //ArrayList arrFKey = new ArrayList();
            //ArrayList arrSo = new ArrayList();


            //��ҵ������Ϣ
            SortedList[] sl = new SortedList[2];
            EntityTypeEnum[] en = new EntityTypeEnum[2];
            string[] fKey = new string[2];
            SaveOptionEnum[] so = new SaveOptionEnum[2];

            sl[0] = new SortedList();
            sl[0].Add("FID", fBaseInfoId);
            sl[0].Add("FIsDeleted", 0);

            sl[1] = new SortedList();
            sl[1].Add("FBaseInfoId", fBaseInfoId);

            en[0] = EntityTypeEnum.EbBaseInfo;
            en[1] = EntityTypeEnum.EsUser;


            fKey[0] = "FID";
            fKey[1] = "FBaseInfoId";


            so[0] = SaveOptionEnum.Update;
            so[1] = SaveOptionEnum.Update;


            //arrEn.Add(en);
            //arrSl.Add(sl);
            //arrFKey.Add(fKey);
            //arrSo.Add(so);

            ////��Ա��Ϣ
            //sl = new SortedList();
            //en = EntityTypeEnum.EeBaseinfo;
            //string fKey = "FBaseInfoId";
            //so = SaveOptionEnum.Update;
            //sl.Add("FBaseInfoId", fBaseInfoId);
            //sl.Add("FIsDeleted", 0);

            //arrEn.Add(en);
            //arrSl.Add(sl);
            //arrFKey.Add(fKey);
            //arrSo.Add(so);


            ////������Ϣ
            //sl = new SortedList();
            //en = EntityTypeEnum.EbProject;
            //string fKey = "FBaseInfoId";
            //so = SaveOptionEnum.Update;
            //sl.Add("FBaseInfoId", fBaseInfoId);
            //sl.Add("FIsDeleted", 0);

            //arrEn.Add(en);
            //arrSl.Add(sl);
            //arrFKey.Add(fKey);
            //arrSo.Add(so);

            ////�豸��Ϣ
            //sl = new SortedList();
            //en = EntityTypeEnum.EbProject;
            //string fKey = "FBaseInfoId";
            //so = SaveOptionEnum.Update;
            //sl.Add("FBaseInfoId", fBaseInfoId);
            //sl.Add("FIsDeleted", 0);

            //arrEn.Add(en);
            //arrSl.Add(sl);
            //arrFKey.Add(fKey);
            //arrSo.Add(so);
            return SaveEBaseM(en, sl, fKey, so);
        }

        public DataTable GetMangeDept(string fPNumber)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" select fname,fnumber from CF_Sys_ManageDept ");
            sb.Append(" where (fnumber like '" + fPNumber + "%' or fnumber='00') ");
            sb.Append(" and flevel<=2 ");
            sb.Append(" and fclassnumber='102009' ");
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" order by fnumber ");
            return GetTable(sb.ToString());
        }

        public DataTable GetPageTable(DataTable dt, int startRow, int pageSize)
        {
            int iCount = dt.Rows.Count;
            if (iCount < startRow)
            {
                return null;
            }
            DataTable tempDt = dt.Clone();
            iCount = pageSize > dt.Rows.Count ? dt.Rows.Count : pageSize;
            for (int i = startRow; i < iCount; i++)
            {
                DataRow row = tempDt.NewRow();
                row = dt.Rows[i];
                tempDt.Rows.Add(row.ItemArray);
            }
            return tempDt;
        }

        

        //԰���̻���ҵ֤��������
        public string CreateYlCertiNo(string fLevelId)
        {
            string fDeptNumber = System.Configuration.ConfigurationManager.AppSettings["DefaultDept"].ToString();

            string sCertiNo = "";

            //����԰���̻���ҵ���ʣ���ơ���԰��֤������ƴ����д��
            sCertiNo += "CYLZ";
            sCertiNo += "��";

            //��ʡ���С����������ּ��
            if (fDeptNumber == "51")
            {
                sCertiNo += "��";
            }
            if (fDeptNumber == "36")
            {
                sCertiNo += "��";
            }
            if (fDeptNumber == "15")
            {
                sCertiNo += "��";
            }
            sCertiNo += "��";

            //���ʵȼ����ֱ���Ҽ����������д���ֱ�ʾ���� 
            string sCertiLevel = "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" fnumber='" + fLevelId + "'");

            string sLevel = GetSignValue(EntityTypeEnum.EsQualiLevel, "FLevel", sb.ToString());
            switch (sLevel)
            {
                case "1":
                    sCertiLevel = "��Ҽ";
                    break;
                case "2":
                    sCertiLevel = "����";
                    break;
                case "3":
                    sCertiLevel = "����";
                    break;
            }

            sb.Remove(0, sb.Length);
            sb.Append("select max(FCertiNo) from CF_Ent_QualiCerti where FCertiNo like '%" + sCertiNo + "[0-9][0-9][0-9][0-9]" + sCertiLevel + "%'");
            string sMaxNo = GetSignValue(sb.ToString());
            if (sMaxNo == null || sMaxNo == "")
            {
                sCertiNo += "0001";
                sCertiNo += sCertiLevel;
                return sCertiNo;
            }
            else
            {
                int sIndex = 0;
                try
                {
                    sIndex = EConvert.ToInt(sMaxNo.Substring(7, 4));
                }
                catch
                {
                    return "";
                }
                sIndex += 1;
                if (sIndex.ToString().Length < 4)
                {
                    for (int i = 0; i < 4 - sIndex.ToString().Length; i++)
                    {
                        sCertiNo += "0";
                    }
                }
                sCertiNo += sIndex;
                sCertiNo += sCertiLevel;
                return sCertiNo;
            }


        }

        /// <summary>
        /// ���ز�����ҵ������ ��������֤��
        /// </summary>
        /// <param name="fsysId"></param>
        /// <returns></returns>
        public string CreateFKCertiNo(string fsysId)
        {
            string fCertiNo = string.Empty;
            int fNo = 0;
            StringBuilder sb = new StringBuilder();
            switch (fsysId)
            {
                case "130"://����
                    
                    break;
                case "186"://����
                   
                    break;
                case "187"://��ҵ  ---���޸�
                 
                    break;
            }
            return fCertiNo;
        }
 

        //�ж�һ����ҵ�������Ƿ񶼺�֤����������(��Ҫ����ʩ���Ϳ������)
        public bool IsAllQualiConnect(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_Ent_QualiCertiTrade");
            sb.Append(" where fbaseinfoid='" + fBaseInfoId + "' ");
            sb.Append(" and (FCertiId is null or FCertiId='')");
            sb.Append(" and fstate=1 ");
            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return false;
            }
            return true;
        }


        ////ɾ��һ����ҵ����������(dbcenter��dbqual)
        //public bool DelEnt(string fBaseInfoId)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //ɾ��dbCenter�е�����
        //    sb.Append(" begin ");
        //    sb.Append(" delete from CF_App_AcceptBook where fbaseinfoid = '"+fBaseInfoId+"';");
        //    sb.Append(" delete from CF_App_ProcessRecord where FProcessInstanceID in ");
        //    sb.Append(" (select fid from CF_App_ProcessInstance where fbaseinfoid='"+fBaseInfoId+"' );");
        //    sb.Append(" delete from CF_App_ProcessInstance where fbaseinfoid = '" + fBaseInfoId + "';"); 
        //    sb.Append(" delete from CF_Pub_Text where FLinkId  in ");
        //    sb.Append(" (select fid from cf_emp_baseinfo where fbaseinfoid='"+fBaseInfoId+"')");
        //    sb.Append(" delete from CF_Emp_Resume where fempid  in ");
        //    sb.Append(" (select fid from cf_emp_baseinfo where fbaseinfoid='" + fBaseInfoId + "')");
        //    sb.Append(" delete from CF_Emp_Baseinfo where fbaseinfoid = '" + fBaseInfoId + "';");






        //} 

        //�����ṩ������ϵͳ�Ľӿڣ�sdata�Ǵ��ݹ��������ݣ�itype�Ǹ�������
        public void upDataUserInfo(SortedList sData)
        {
            string iType = sData["iType"].ToString();
            if (iType == "1")//������ҵ������Ϣ
            {
                if (sData.IndexOfKey("FUpDeptId") >= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" update cf_sys_user set FManageDeptId='" + sData["FUpDeptId"].ToString() + "' ");
                    sb.Append(" where fid='" + sData["FUserId"].ToString() + "'");

                    this.PExcute(sb.ToString());
                }

                DataTable dt = this.GetTable(EntityTypeEnum.EsUser, "", "fid='" + sData["FUserId"].ToString() + "'");
                if (dt.Rows.Count > 0 || sData["FState"].ToString() == "1")
                {
                    SortedList sbaseinfo = new SortedList();
                    sbaseinfo.Add("FID", dt.Rows[0]["fid"].ToString());
                    if (sData.IndexOfKey("FLinkMan") >= 0)
                    {
                        if (!string.IsNullOrEmpty(sData["FLinkMan"].ToString()))
                        {
                            sbaseinfo.Add("FLinkMan", sData["FLinkMan"].ToString());
                        }
                    }
                    if (sData.IndexOfKey("FAddress") >= 0)
                    {
                        sbaseinfo.Add("FAddress", sData["FAddress"].ToString());
                    }

                    if (sData.IndexOfKey("FName1") >= 0)
                    {
                        sbaseinfo.Add("FName", sData["FName1"].ToString());
                    }

                    if (sData.IndexOfKey("FPassWord1") >= 0)
                    {
                        sbaseinfo.Add("FPassWord", sData["FPassWord1"].ToString());
                    }

                    if (sData.IndexOfKey("FName2") >= 0)
                    {
                        sbaseinfo.Add("FName1", sData["FName2"].ToString());
                    }

                    if (sData.IndexOfKey("FPassWord2") >= 0)
                    {
                        sbaseinfo.Add("FPassWord1", sData["FPassWord2"].ToString());
                    }
                    if (sData.IndexOfKey("FTel") >= 0)
                    {
                        sbaseinfo.Add("FTel", sData["FTel"].ToString());
                    }
                    this.SaveEBase(EntityTypeEnum.EsUser, sbaseinfo, "FID", SaveOptionEnum.Update);

                    DataTable dtr = this.GetTable(EntityTypeEnum.EsUserRight, "", "fuserid='" + sData["FUserId"].ToString() + "' and fsystemid='" + sData["FSystemId"].ToString() + "'");
                    if (dtr.Rows.Count > 0)
                    {
                        SortedList sUserRight = new SortedList();
                        sUserRight.Add("FID", dtr.Rows[0]["fid"].ToString());
                        if (sData.IndexOfKey("FEntTypeId") >= 0)
                        {
                            sUserRight.Add("FEntTypeId", sData["FEntTypeId"].ToString());
                        }

                        if (sData.IndexOfKey("FRegistAddress") >= 0)
                        {
                            sUserRight.Add("FRegistAddress", sData["FRegistAddress"].ToString());
                        }

                        if (sData.IndexOfKey("FManagerName") >= 0)
                        {
                            sUserRight.Add("FManagerName", sData["FManagerName"].ToString());
                        }

                        if (sData.IndexOfKey("FJuridcialCode") >= 0)
                        {
                            sUserRight.Add("FJuridcialCode", sData["FJuridcialCode"].ToString());
                        }

                        if (sData.IndexOfKey("FLicence") >= 0)
                        {
                            sUserRight.Add("FLicence", sData["FLicence"].ToString());
                        }

                        //if (sData.IndexOfKey("FName1") >= 0)
                        //{
                        //    sUserRight.Add("FName", dt.Rows[0]["FName1"].ToString());
                        //}

                        //if (sData.IndexOfKey("FPassWord") >= 0)
                        //{
                        //    sUserRight.Add("FPassWord", dt.Rows[0]["FPassWord"].ToString());
                        //}

                        if (sData.IndexOfKey("FRegistPostCode") >= 0)
                        {
                            sUserRight.Add("FRegistPostCode", sData["FRegistPostCode"].ToString());
                        }

                        if (sData.IndexOfKey("FTel") >= 0)
                        {
                            sUserRight.Add("FTel", sData["FTel"].ToString());
                        }

                        this.SaveEBase(EntityTypeEnum.EsUserRight, sUserRight, "FID", SaveOptionEnum.Update);
                    }
                }
            }
            //������ҵ������Ϣ
            if (iType == "2")
            {
                DataTable dtr = this.GetTable(EntityTypeEnum.EsUserRight, "", "fuserid='" + sData["FUserId"].ToString() + "' and fsystemid='" + sData["FSystemId"].ToString() + "'");
                if (dtr.Rows.Count > 0)
                {
                    SortedList sUserRight = new SortedList();
                    sUserRight.Add("FID", dtr.Rows[0]["fid"].ToString());
                    sUserRight.Add("FManagerName", sData["FManagerName"].ToString());
                    this.SaveEBase(EntityTypeEnum.EsUserRight, sUserRight, "FID", SaveOptionEnum.Update);
                }
            }
        }


        //������ҵ��¼�û���������
        public bool UpdateEntUNameAndPwd(string fBaseInfoId, string fSystemId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select FId,FName,FPassWord from cf_sys_user where FbaseInfoId='" + fBaseInfoId + "'");
            DataTable UserData = GetTable(sb.ToString());
            if (UserData == null || UserData.Rows.Count == 0)
            {
                return true;
            }

            string sOldName = UserData.Rows[0]["FName"].ToString();
            string sOldPwd = UserData.Rows[0]["FPassWord"].ToString();

            string sNewName = "";
            string sNewPwd = "";

            //���ԭ�����û���С��8λ ���û���ǰ��ֱ�Ӽ��ϲ���������� 
            if (sOldName.Length < 8)
            {
                sNewName = (new Random()).Next(10).ToString() + sOldName;
            }
            else
            {
                return true;
            }

            //������ǰ�油��0 
            if (sNewName.Length < 6)
            {
                sNewPwd = "000000" + sNewName;
                sNewPwd = sNewPwd.Substring(sNewPwd.Length - 6, 6);
            }
            else
            {
                //if ((sOldName.Length - 6) > 0)
                //{

                sNewPwd = sNewName.Substring(sNewName.Length - 6, 6);
                //}
                //else
                //{
                //    sNewPwd = sOldName;
                //}
            }


            //�õ�һ�����ظ����û���������
            GetNameAndPwd(ref sNewName, ref sNewPwd);



            SortedList sl = new SortedList();
            sl.Add("FID", UserData.Rows[0]["FID"].ToString());
            sl.Add("FName", sNewName);
            sl.Add("FPassWord", sNewPwd);

            SaveEBase(EntityTypeEnum.EsUser, sl, "FID", SaveOptionEnum.Update);

            sb.Remove(0, sb.Length);
            sb.Append(" select fid from cf_sys_userright where  fuserid='" + UserData.Rows[0]["FID"].ToString() + "'");
            sb.Append(" and fsystemid='" + fSystemId + "'");
            string sUserRightId = GetSignValue(sb.ToString());
            if (string.IsNullOrEmpty(sUserRightId))
            {
                return true;
            }

            sl = new SortedList();
            sl.Add("FID", sUserRightId);
            sl.Add("FName", sOldName);
            sl.Add("FPassWord", sOldPwd);
            SaveEBase(EntityTypeEnum.EsUserRight, sl, "FID", SaveOptionEnum.Update);
            return true;
        }


        //�ж��Ƿ��ظ� 
        public void GetNameAndPwd(ref string fName, ref string fPWd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(1) from cf_sys_user where fname='" + fName + "' and fpassword='" + fPWd + "'");
            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                fName = (new Random()).Next(10).ToString() + fName.Substring(1, fName.Length - 1);
                if (fName.Length < 6)
                {
                    fPWd = "000000" + fName;
                    fPWd = fPWd.Substring(fPWd.Length - 6, 6);
                }
                else
                {
                    fPWd = fName.Substring(fName.Length - 6, 6);
                }
                GetNameAndPwd(ref fName, ref fPWd);
            }
        }

        //�ж�һ��������ɫ��һ���������Ƿ��а��Ȩ��
        /// <summary>
        /// �ж�һ��������ɫ��һ���������Ƿ��а��Ȩ��
        /// Author:������
        /// Time:2009-04-28 14:00
        /// </summary> 
        /// <param name="fRoleId">������ɫId</param>
        /// <param name="fProcessId">����ʵ��Id</param>
        /// <returns>�Ƿ���԰�� true(����) false(������)</returns>
        public bool IsCanAppEnd(string fRoleId, string fProcessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_App_SubFlow where ");
            sb.Append(" FRoleId='" + fRoleId + "' and FProcessId='" + fProcessId + "'");
            sb.Append(" and FIsEnd=1 ");
            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return true;
            }
            return false;
        }





        //�ж�һ��������ɫ�����������Ƿ������������ɫ�����һ��������
        /// <summary>
        /// �ж�һ��������ɫ�����������Ƿ������������ɫ�����һ��������
        /// Author:������
        /// Time:2009-04-28 14:59
        /// </summary> 
        /// <param name="fRoleId">������ɫId</param>
        /// <param name="fSubFlowId">������Id</param>
        /// <returns>true(��) false(����)</returns>
        public bool IslastSubFlow(string fRoleId, string fSubFlowId)
        {
            EaSubFlow es = (EaSubFlow)GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + fSubFlowId + "'");
            if (es == null)
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_App_SubFlow where ");
            sb.Append(" FRoleId='" + fRoleId + "' and FProcessId='" + es.FProcessId + "'");
            sb.Append(" and FOrder>" + es.FOrder);
            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return false;
            }
            return true;
        }

        //��ȡһ��������������ȼ�
        /// <summary>
        /// ��ȡһ��������������ȼ�
        /// Author:������
        /// Time:2009-04-28 15:39
        /// </summary>
        /// <param name="fProcessId">����ʵ��Id</param>
        /// <returns>������������ȼ�</returns>
        public int GetMaxAppLevel(string fProcessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select max(FLevel) from CF_App_SubFlow where ");
            sb.Append(" FProcessId='" + fProcessId + "'");
            int iLevel = EConvert.ToInt(GetSignValue(sb.ToString()));
            return iLevel;
        }


        //�ж�һ�������Ƿ�����������ȼ���������
        /// <summary>
        /// ��ȡһ��������������ȼ�
        /// Author:������
        /// Time:2009-04-28 15:45
        /// </summary>
        /// <param name="iLevel">�����ȼ�</param>
        /// <param name="fProcessId">����ʵ��Id</param>
        /// <returns>true(��) false(����)</returns>
        public bool IsHasSubFlow(int iLevel, string fProcessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from CF_App_SubFlow where ");
            sb.Append(" FProcessId='" + fProcessId + "'");
            sb.Append(" and FLevel=" + iLevel);
            int iCount = GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return true;
            }
            return false;
        }



 

        //�Ƿ���֤�飬���û��֤�飬�����Ⱦ�λ��������
        public string isHaveCerti(string fBaseInfoId)
        {
            EbQualiCerti ec = (EbQualiCerti)GetEBase(EntityTypeEnum.EbQualiCerti, "fid", "fbaseinfoid='" + fBaseInfoId + "' and fisdeleted=0 and fisvalid=1 ");
            if (ec != null)
            {
                return ec.FId;
            }
            else
            {
                return "";
            }
        }


 

        /// <summary>
        /// ��ù�����
        /// </summary>
        /// <param name="ID">FNumber��FID</param>
        /// <param name="type">
        /// 1��ͨ��FNumber�õ�FName
        /// 2��ͨ��FID�õ�FName
        /// 3��ͨ��FNumber�õ�FFullName
        /// 4��ͨ��FID�õ�FFullName
        /// 5��ͨ��FNumber�õ�FID
        /// </param>
        /// <returns></returns>
        public string getDept(string ID, int type)
        {
            string str = "";
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case 1:
                    sb.Append("select FName from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
                case 2:
                    sb.Append("select FName from CF_Sys_ManageDept where FID='" + ID + "'");
                    break;
                case 3:
                    sb.Append("select FFullName from CF_Sys_ManageDept where fnumber in (" + ID + ")");
                    break;
                case 4:
                    sb.Append("select FFullName from CF_Sys_ManageDept where FID='" + ID + "'");
                    break;
                case 5:
                    sb.Append("select FID from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
                default:
                    sb.Append("select FName from CF_Sys_ManageDept where fnumber='" + ID + "'");
                    break;
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                str = this.GetSignValue(sb.ToString());
            }
            return str;
        }

 
        /// <summary>
        /// ������ҵ��֤����Ϣ
        /// </summary>
        /// <param name="fid"></param>
        public void BackEntCerti(string fid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into CF_HIS_QualiCerti (");
            sb.Append("FMId, FID, FBaseInfoId, FCertiType, FCertiNo, FAppDeptId, FAppDeptName, FBeginTime, FEndTime, FAppTime, FLevelName, FLevelId, FLevel, FContent, FIsValid, FIsDeleted, FTime, FIsTemp, FPCount, FOCount, FEntName, FEntAddress, FEntCreateTime, FEntRegistFund, FEntLicence, FEntTypeId, FEntJuridical, FEntJuridicalFunction, FEntJuridicalTechId, FEntManager, FEntManagerFunction, FEntManagerTechId, FEntTechnic, FEntTechnicFunction, FEntTechnicTechId, FState, FIsApp, FIsPrint, FType, FAppId, FRemark, fsystemid, FBackCertiNo, FRegistAddress, FUserId, FAddOCount, FAddEDOCount, FIsCheck, FBZ, FIsEnddt, FJuridicalNo, FStateType, FCZRemark, FIsReceive, FReceivePerson, FReceiveTime, FInTime");
            sb.Append(") ");
            sb.Append("select newId() FMId, FID, FBaseInfoId, FCertiType, FCertiNo, FAppDeptId, FAppDeptName, FBeginTime, FEndTime, FAppTime, FLevelName, FLevelId, FLevel, FContent, FIsValid, FIsDeleted, FTime, FIsTemp, FPCount, FOCount, FEntName, FEntAddress, FEntCreateTime, FEntRegistFund, FEntLicence, FEntTypeId, FEntJuridical, FEntJuridicalFunction, FEntJuridicalTechId, FEntManager, FEntManagerFunction, FEntManagerTechId, FEntTechnic, FEntTechnicFunction, FEntTechnicTechId, FState, FIsApp, FIsPrint, FType, FAppId, FRemark, fsystemid, FBackCertiNo, FRegistAddress, FUserId, FAddOCount, FAddEDOCount, FIsCheck, FBZ, FIsEnddt, FJuridicalNo, FStateType, FCZRemark, FIsReceive, FReceivePerson, FReceiveTime,getdate() FInTime ");
            sb.Append("from CF_Ent_QualiCerti where fid='" + fid + "'");
            this.PExcute(sb.ToString());
        }

        public DataTable getAllupDeptId(string FProvice, int isHaveTown, int isCity)
        {
            DataTable dtr = new DataTable();
            DataColumn dc1 = new DataColumn("fnumber");
            dtr.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("fname");
            dtr.Columns.Add(dc2);

            StringBuilder sb = new StringBuilder();
            sb.Append("select fname ,fnumber,fparentid,fistown from cf_sys_ManageDept where fisdeleted=0 and fname not like '%��Ͻ��%' ");

            if (FProvice != "")
            {
                sb.Append(" and fnumber like '" + FProvice + "%' ");
            } 

            if (isCity == 1)
            {
                sb.Append(" and flevel<=2 ");
            }

            sb.Append(" order by fnumber ");
            DataTable dt = this.GetTable(sb.ToString());
            int iLevle = 3;
            int iCount1 = 0;
            if (FProvice.Length == 2)
            {
                iLevle = 1;
                iCount1 = 1;
            }
            if (FProvice.Length == 4)
            {
                iLevle = 2;
                iCount1 = 1;
            }

            if (FProvice.Length == 6)
            {
                iLevle = 3;
                iCount1 = 1;
            }

            Hashtable sl = new Hashtable();
            for (int i = 0; i < iCount1; i++)
            {
                DataRow[] dr = dt.Select("fnumber='" + FProvice + "'", "fnumber");

                DataRow drc = dtr.NewRow();
                drc["fname"] = dr[0]["fname"].ToString();
                drc["fnumber"] = dr[0]["fnumber"].ToString();
                dtr.Rows.Add(drc);

                dr = dt.Select("fparentid='" + FProvice + "'", "fnumber");
                int iCount2 = dr.Length;
                for (int j = 0; j < iCount2; j++)
                {
                    drc = dtr.NewRow();
                    drc["fname"] = dr[j]["fname"].ToString();
                    drc["fnumber"] = dr[j]["fnumber"].ToString();
                    dtr.Rows.Add(drc);

                    if (isHaveTown == 1)
                    {
                        DataRow[] drt = dt.Select("fparentid='" + dr[j]["fnumber"].ToString() + "' and fistown=1 ", "fnumber");
                        int iCount3 = drt.Length;
                        for (int m = 0; m < iCount3; m++)
                        {
                            drc = dtr.NewRow();
                            drc["fname"] = "����" + drt[m]["fname"].ToString();
                            drc["fnumber"] = drt[m]["fnumber"].ToString();
                            dtr.Rows.Add(drc);
                        }
                    }
                    else
                    {
                        DataRow[] drt = dt.Select("fparentid='" + dr[j]["fnumber"].ToString() + "'", "fnumber");
                        int iCount3 = drt.Length;
                        for (int m = 0; m < iCount3; m++)
                        {
                            drc = dtr.NewRow();
                            drc["fname"] = "����" + drt[m]["fname"].ToString();
                            drc["fnumber"] = drt[m]["fnumber"].ToString();
                            dtr.Rows.Add(drc);
                        }
                    }
                }
            }
            return dtr;
        }

        public bool PubNews(string fid, string fColNumber)
        {
            if (fColNumber == null || fColNumber == "")
            {
                return false;
            }
            EnTitle et = (EnTitle)this.GetEBase(EntityTypeEnum.EnTitle, "", "fid='" + fid + "'");
            if (et == null)
            {
                return false;
            }
            string[] cols = fColNumber.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int colCount = cols.Length;
            SortedList[] sl = new SortedList[colCount];
            string[] fkey = new string[colCount];
            SaveOptionEnum[] so = new SaveOptionEnum[colCount];
            EntityTypeEnum[] en = new EntityTypeEnum[colCount];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < colCount; i++)
            {
                sl[i] = new SortedList();

                string fColId = this.GetSignValue(EntityTypeEnum.EnCol, "FId", "FNewsId='" + fid + "' and fcolnumber='" + cols[i] + "'");
                if (fColId != null && fColId != "")
                {
                    sl[i].Add("FID", fColId);
                    sl[i].Add("FState", et.FState);
                    sl[i].Add("FCreateTime", DateTime.Now.ToString());
                    sl[i].Add("FOrder", et.FOrder);
                    so[i] = SaveOptionEnum.Update;
                }
                else
                {
                    sl[i].Add("FID", Guid.NewGuid().ToString());
                    sl[i].Add("FIsDeleted", 0);
                    sl[i].Add("FState", et.FState);
                    sl[i].Add("FOrder", et.FOrder);
                    sl[i].Add("FPubTime", DateTime.Now.ToString());
                    sl[i].Add("FColor", et.FColor);
                    sl[i].Add("FColNumber", cols[i]);
                    sl[i].Add("FNewsId", fid);
                    sl[i].Add("FCreateTime", DateTime.Now.ToString());
                    so[i] = SaveOptionEnum.Insert;
                }
                if (i == 0)
                {
                    sb.Append("'" + cols[i] + "'");
                }
                else
                {
                    sb.Append(",'" + cols[i] + "'");
                }
                fkey[i] = "FID";
                en[i] = EntityTypeEnum.EnCol;
            }
            if (sb.Length > 0)
            {
                this.PExcute(" delete from cf_news_Col where fnewsid='" + fid + "' and  FColNumber not in(" + sb.ToString() + ")");
            }
            return this.SaveEBaseM(en, sl, fkey, so);
        }
    }
}