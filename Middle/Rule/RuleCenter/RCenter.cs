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
        //fEntId企业编码
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

        //得到不同类型企业的不良行为标准编号
        public string GetBadByEntType(string fEntTypeId)
        {
            switch (fEntTypeId)
            {
                case "101": //施工企业
                    return "'D1'";
                case "120": //招标代理企业企业
                    return "'F1'";
                case "125": //工程监理企业
                    return "'E1'";
                case "130": //房地产企业
                    return "''";
                case "135": //园林绿化企业
                    return "''";

                case "140": //外来勘察设计企业
                    return "''";

                case "145": //施工图审查机构
                    return "'I1'";

                case "155": //勘察设计企业
                    return "'B1','C1'";

                case "175": //质量检测机构
                    return "'H1'";

                case "180": //外省进川施工企业
                    return "'D1'";

                case "185": //造价咨询企业
                    return "'G1'";


                default:
                    return "''";
            }
        }

        public string Test(string str1, string str2)
        {
            return str1 + str2;
        }

        //根据资质编号得到资质等级
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

        //判断申请的资质是否是新资质
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

        //判断企业是否有主项
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

        //判断企业是否有增项
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

        //判断企业是否有资质证书
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

        //是否有安全生产许可证书
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

        //判断企业是否有建造师
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

        //注销企业
        public bool EntLogout(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();

            //ArrayList arrEn = new ArrayList();
            //ArrayList arrSl = new ArrayList();
            //ArrayList arrFKey = new ArrayList();
            //ArrayList arrSo = new ArrayList();


            //企业基本信息
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

            ////人员信息
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


            ////工程信息
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

            ////设备信息
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

        

        //园林绿化企业证书编号生成
        public string CreateYlCertiNo(string fLevelId)
        {
            string fDeptNumber = System.Configuration.ConfigurationManager.AppSettings["DefaultDept"].ToString();

            string sCertiNo = "";

            //城市园林绿化企业资质（简称“城园绿证”）的拼音缩写；
            sCertiNo += "CYLZ";
            sCertiNo += "·";

            //各省、市、自治区汉字简称
            if (fDeptNumber == "51")
            {
                sCertiNo += "川";
            }
            if (fDeptNumber == "36")
            {
                sCertiNo += "赣";
            }
            if (fDeptNumber == "15")
            {
                sCertiNo += "蒙";
            }
            sCertiNo += "·";

            //资质等级（分别以壹、贰、叁大写汉字表示）。 
            string sCertiLevel = "";

            StringBuilder sb = new StringBuilder();
            sb.Append(" fnumber='" + fLevelId + "'");

            string sLevel = GetSignValue(EntityTypeEnum.EsQualiLevel, "FLevel", sb.ToString());
            switch (sLevel)
            {
                case "1":
                    sCertiLevel = "·壹";
                    break;
                case "2":
                    sCertiLevel = "·贰";
                    break;
                case "3":
                    sCertiLevel = "·叁";
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
        /// 房地产、物业、评估 机构生产证号
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
                case "130"://房产
                    
                    break;
                case "186"://评估
                   
                    break;
                case "187"://物业  ---待修改
                 
                    break;
            }
            return fCertiNo;
        }
 

        //判断一个企业的资质是否都和证书表关联上了(主要用于施工和勘察设计)
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


        ////删除一个企业的所有数据(dbcenter和dbqual)
        //public bool DelEnt(string fBaseInfoId)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //删除dbCenter中的数据
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

        //更新提供给外面系统的接口，sdata是传递过来的数据，itype是更新类型
        public void upDataUserInfo(SortedList sData)
        {
            string iType = sData["iType"].ToString();
            if (iType == "1")//更新企业所有信息
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
            //更新企业法人信息
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


        //更新企业登录用户名和密码
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

            //如果原来的用户名小于8位 新用户名前面直接加上产生的随机数 
            if (sOldName.Length < 8)
            {
                sNewName = (new Random()).Next(10).ToString() + sOldName;
            }
            else
            {
                return true;
            }

            //新密码前面补齐0 
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


            //得到一个不重复的用户名和密码
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


        //判断是否重复 
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

        //判断一个审批角色在一个流程里是否有办结权限
        /// <summary>
        /// 判断一个审批角色在一个流程里是否有办结权限
        /// Author:霍立海
        /// Time:2009-04-28 14:00
        /// </summary> 
        /// <param name="fRoleId">审批角色Id</param>
        /// <param name="fProcessId">流程实例Id</param>
        /// <returns>是否可以办结 true(可以) false(不可以)</returns>
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





        //判断一个审批角色的子流程在是否是这个审批角色的最后一个子流程
        /// <summary>
        /// 判断一个审批角色的子流程在是否是这个审批角色的最后一个子流程
        /// Author:霍立海
        /// Time:2009-04-28 14:59
        /// </summary> 
        /// <param name="fRoleId">审批角色Id</param>
        /// <param name="fSubFlowId">子流程Id</param>
        /// <returns>true(是) false(不是)</returns>
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

        //获取一个流程最大审批等级
        /// <summary>
        /// 获取一个流程最大审批等级
        /// Author:霍立海
        /// Time:2009-04-28 15:39
        /// </summary>
        /// <param name="fProcessId">流程实例Id</param>
        /// <returns>流程最大审批等级</returns>
        public int GetMaxAppLevel(string fProcessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select max(FLevel) from CF_App_SubFlow where ");
            sb.Append(" FProcessId='" + fProcessId + "'");
            int iLevel = EConvert.ToInt(GetSignValue(sb.ToString()));
            return iLevel;
        }


        //判断一个流程是否包含该审批等级的子流程
        /// <summary>
        /// 获取一个流程最大审批等级
        /// Author:霍立海
        /// Time:2009-04-28 15:45
        /// </summary>
        /// <param name="iLevel">审批等级</param>
        /// <param name="fProcessId">流程实例Id</param>
        /// <returns>true(是) false(不是)</returns>
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



 

        //是否有证书，如果没有证书，必须先就位主项资质
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
        /// 获得管区划
        /// </summary>
        /// <param name="ID">FNumber或FID</param>
        /// <param name="type">
        /// 1：通过FNumber得到FName
        /// 2：通过FID得到FName
        /// 3：通过FNumber得到FFullName
        /// 4：通过FID得到FFullName
        /// 5：通过FNumber得到FID
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
        /// 备份企业的证书信息
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
            sb.Append("select fname ,fnumber,fparentid,fistown from cf_sys_ManageDept where fisdeleted=0 and fname not like '%市辖区%' ");

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
                            drc["fname"] = "　　" + drt[m]["fname"].ToString();
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
                            drc["fname"] = "　　" + drt[m]["fname"].ToString();
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