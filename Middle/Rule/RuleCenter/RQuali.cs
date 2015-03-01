using System;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using Approve.PersistEnterprise;
using Approve.PersistBase;
using Approve.RuleBase;
using System.EnterpriseServices;
using Approve.EntityBase;
using Approve.EntityCenter;


namespace Approve.RuleCenter
{

    public class RQuali : RBase
    {
        private PEnt m_pes;

        public RQuali()
        {
            m_pes = null;
            this.pDBName = "dbQuali";
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
        }
        /// <summary>
        /// dbType:重构函数，int类型，0：表示back库           
        /// </summary>
        /// <param name="dbType"></param>
        public RQuali(int dbType)
        {
            m_pes = null;
            this.pDBName = "dbQualiBackup";
            BaseTools baseTools = new BaseTools();
            this.baseTools = baseTools;
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

        public void inputBaseInfo(string FBaseInfoId, string FAppId)
        {
            RCenter rc = new RCenter();

            //企业所属系统编号
            string fSystemId = rc.getEntSystemId(FBaseInfoId);
            if (fSystemId == null || fSystemId == "")
            {
                return;
            }


            SaveOptionEnum so = SaveOptionEnum.Insert;
            string FId = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" fbaseinfoid='" + FBaseInfoId + "' and FAPPId='" + FAppId + "'");
            DataTable dt = this.GetTable(EntityTypeEnum.EqEbBaseInfo, "fid", sb.ToString());
            if (dt.Rows.Count > 0)
            {
                FId = dt.Rows[0]["FID"].ToString();
                so = SaveOptionEnum.Update;
            }

            sb.Remove(0, sb.Length);
            sb.Append("FId='" + FBaseInfoId + "'");
            dt = rc.GetTable(EntityTypeEnum.EbBaseInfo, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            string sSystemId = dt.Rows[0]["FSystemId"].ToString();

            SortedList sl = new SortedList();
            string sKey = "FID";
            EntityTypeEnum en = EntityTypeEnum.EqEbBaseInfo;

            sl.Add("FID", FId);
            sl.Add("FBaseInfoId", FBaseInfoId);
            sl.Add("FAPPId", FAppId);
            sl.Add("FJuridcialCode", dt.Rows[0]["FJuridcialCode"].ToString());
            sl.Add("FEntCode", dt.Rows[0]["FEntCode"].ToString());
            sl.Add("FName", dt.Rows[0]["FName"].ToString());
            sl.Add("FOtherName", dt.Rows[0]["FOtherName"].ToString());
            sl.Add("FTradeId", dt.Rows[0]["FTradeId"].ToString());
            sl.Add("FSubjectionId", dt.Rows[0]["FSubjectionId"].ToString());
            sl.Add("FManageDeptName", dt.Rows[0]["FManageDeptName"].ToString());
            sl.Add("FUpDeptId", dt.Rows[0]["FUpDeptId"].ToString());
            sl.Add("FBankName", dt.Rows[0]["FBankName"].ToString());
            sl.Add("FBankAccountNo", dt.Rows[0]["FBankAccountNo"].ToString());
            sl.Add("FLicence", dt.Rows[0]["FLicence"].ToString());
            sl.Add("FRegistFund", dt.Rows[0]["FRegistFund"].ToString());
            sl.Add("FRegistFundUnitId", dt.Rows[0]["FRegistFundUnitId"].ToString());
            sl.Add("FEntCreateFileNo", dt.Rows[0]["FEntCreateFileNo"].ToString());
            sl.Add("FEntTypeId", dt.Rows[0]["FEntTypeId"].ToString());
            sl.Add("FRegistDeptId", dt.Rows[0]["FRegistDeptId"].ToString());
            sl.Add("FRegistAddress", dt.Rows[0]["FRegistAddress"].ToString());
            sl.Add("FRegistPostcode", dt.Rows[0]["FRegistPostcode"].ToString());
            sl.Add("FAddress", dt.Rows[0]["FAddress"].ToString());
            sl.Add("FPostcode", dt.Rows[0]["FPostcode"].ToString());
            sl.Add("FLinkMan", dt.Rows[0]["FLinkMan"].ToString());
            sl.Add("FMobile", dt.Rows[0]["FMobile"].ToString());
            sl.Add("FTel", dt.Rows[0]["FTel"].ToString());
            sl.Add("FFax", dt.Rows[0]["FFax"].ToString());
            sl.Add("FUrl", dt.Rows[0]["FUrl"].ToString());
            sl.Add("FEmail", dt.Rows[0]["FEmail"].ToString());
            sl.Add("FEntCreateTime", dt.Rows[0]["FEntCreateTime"].ToString());
            sl.Add("FRegistDate", dt.Rows[0]["FRegistDate"].ToString());
            sl.Add("FApproveDate", dt.Rows[0]["FApproveDate"].ToString());
            sl.Add("FEntAppDeptName", dt.Rows[0]["FEntAppDeptName"].ToString());
            sl.Add("FCreditLevel", dt.Rows[0]["FCreditLevel"].ToString());
            sl.Add("FCreditScore", dt.Rows[0]["FCreditScore"].ToString());
            sl.Add("FSafeNo", dt.Rows[0]["FSafeNo"].ToString());
            //sl.Add("FOTxt1", dt.Rows[0]["FOTxt1"].ToString());
            //sl.Add("FOTxt2", dt.Rows[0]["FOTxt2"].ToString());

            sl.Add("FInt1", dt.Rows[0]["FInt1"].ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FSystemId", dt.Rows[0]["FSystemId"].ToString());
            sl.Add("FIsSafe", dt.Rows[0]["FIsSafe"].ToString());




            //sb.Append(" select top 6 FTypeName,FLevelName,FAppTime,FIsBase From CF_Ent_QualiCertiTrade where fbaseinfoid='" + FBaseInfoId + "' ");
            //sb.Append(" and FState=1 order by FIsBase desc,FAppTime desc ");

            if (sSystemId != "175")
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select  FTypeName,FLevelName,FAppTime,FIsBase From CF_Ent_QualiCertiTrade where fbaseinfoid='" + FBaseInfoId + "' ");
                sb.Append(" and FState=1 order by FIsBase desc,FAppTime desc ");
                dt = rc.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] row = dt.Select(" FIsBase=1");
                    if (row != null && row.Length > 0)
                    {
                        sl.Add("FPTxt", row[0]["FTypeName"].ToString().Trim() + row[0]["FLevelName"].ToString().Trim() + "  " + rc.StrToDate(row[0]["FAppTime"].ToString().Trim()));
                        dt.Rows.Remove(row[0]);
                    }
                    int iRowCount = dt.Rows.Count;
                    //if (iRowCount < 5)
                    //{
                    //    for (int i = 0; i < 5 - iRowCount; i++)
                    //    {
                    //        dt.Rows.Add(dt.NewRow());
                    //    }
                    //}


                    string sQuali = "";
                    for (int i = 0; i < iRowCount; i++)
                    {
                        sQuali += dt.Rows[i]["FTypeName"].ToString().Trim() + dt.Rows[i]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[i]["FAppTime"].ToString().Trim()) + "\r\n";
                    }
                    if (sQuali.Length > 100)
                    {
                        sQuali = sQuali.Substring(0, 100) + "...";
                    }
                    sl.Add("FOTxt1", sQuali);

                    //sl.Add("FOTxt1", dt.Rows[0]["FTypeName"].ToString().Trim() + dt.Rows[0]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[0]["FAppTime"].ToString().Trim()));
                    //sl.Add("FOTxt2", dt.Rows[1]["FTypeName"].ToString().Trim() + dt.Rows[1]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[1]["FAppTime"].ToString().Trim()));
                    //sl.Add("FOTxt3", dt.Rows[2]["FTypeName"].ToString().Trim() + dt.Rows[2]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[2]["FAppTime"].ToString().Trim()));
                    //sl.Add("FOTxt4", dt.Rows[3]["FTypeName"].ToString().Trim() + dt.Rows[3]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[3]["FAppTime"].ToString().Trim()));
                    //sl.Add("FOTxt5", dt.Rows[4]["FTypeName"].ToString().Trim() + dt.Rows[4]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[4]["FAppTime"].ToString().Trim()));


                }
            }
            else
            {

                sl.Add("FOTxt1", dt.Rows[0]["FOTxt1"].ToString().Trim());
                sl.Add("FOTxt2", dt.Rows[0]["FOTxt2"].ToString().Trim());
                sl.Add("FOTxt3", dt.Rows[0]["FOTxt3"].ToString().Trim());
                sl.Add("FOTxt4", dt.Rows[0]["FOTxt4"].ToString().Trim());
                sl.Add("FOTxt5", dt.Rows[0]["FOTxt5"].ToString().Trim());
            }

            sb.Remove(0, sb.Length);
            sb.Append("select distinct(FCertiNo) from CF_Ent_QualiCerti where fbaseinfoid='" + FBaseInfoId + "' and FIsValid=1");
            dt = rc.GetTable(sb.ToString());
            string FOCertiNo2 = "";//勘察设计用
            if (dt != null && dt.Rows.Count > 0)
            {
                sl.Add("FOCertiNo", dt.Rows[0]["FCertiNo"].ToString().Trim());
                if (dt.Rows.Count > 1)
                {
                    FOCertiNo2 = dt.Rows[1]["FCertiNo"].ToString();
                }
            }
            if (fSystemId == "155" && FOCertiNo2 != "")
            {
                sl["FSafeNo"] = FOCertiNo2;
            }






            this.SaveEBase(EntityTypeEnum.EqEbBaseInfo, sl, "FID", so);

            dt = rc.GetTable(EntityTypeEnum.EPText, "", "flinkid='" + FBaseInfoId + "'");

            sb.Remove(0, sb.Length);
            if (dt != null && dt.Rows.Count > 0)
            {

                SortedList[] sls = new SortedList[dt.Rows.Count];
                EntityTypeEnum[] ens = new EntityTypeEnum[dt.Rows.Count];
                SaveOptionEnum[] sos = new SaveOptionEnum[dt.Rows.Count];
                string[] fkeys = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sls[i] = new SortedList();
                    sls[i].Add("FID", Guid.NewGuid().ToString());
                    sls[i].Add("FAppId", FAppId);
                    sls[i].Add("FContent", dt.Rows[i]["FContent"].ToString());
                    sls[i].Add("FTIME", DateTime.Now);
                    sls[i].Add("FISDELETED", 0);
                    sls[i].Add("FCreateTime", DateTime.Now);
                    sls[i].Add("FLinkId", FBaseInfoId);
                    sls[i].Add("FType", dt.Rows[i]["FType"].ToString());

                    ens[i] = EntityTypeEnum.EqPText;
                    sos[i] = SaveOptionEnum.Insert;
                    fkeys[i] = "FID";

                }
                SaveEBaseM(ens, sls, fkeys, sos);
            }

            //导入企业负责人
            setLeader(FAppId, FBaseInfoId);

            //导入企业其他信息表
            SInPutBaseInfoOther(FBaseInfoId, FAppId);

            //导入企业财务表
            SInPutEntFinance(FBaseInfoId, FAppId);


            DataTable dtTemp = null;
            #region 数据导入速度慢（先注释掉）
            ////导入企业人员表
            //sb.Remove(0, sb.Length);
            //sb.Append(" select * from cf_emp_baseinfo where fbaseinfoid='" + FBaseInfoId + "'");
            //dtTemp = rc.GetTable(sb.ToString());
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    SInPutEmpInfo(FAppId, FBaseInfoId, dtTemp);
            //}

            ////导入企业人员表（安全生产） 
            //sb.Remove(0, sb.Length);
            //sb.Append(" select * from cf_emp_safebaseinfo where fbaseinfoid='" + FBaseInfoId + "'");
            //dtTemp = rc.GetTable(sb.ToString());
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    SInPutEmpInfo(FAppId, FBaseInfoId, dtTemp,EntityTypeEnum.EqEeSafeBaseinfo);
            //}

            ////导入企业工程
            //sb.Remove(0, sb.Length);
            //sb.Append(" select * from cf_ent_project where fbaseinfoid='" + FBaseInfoId + "'");
            //dtTemp = rc.GetTable(sb.ToString());
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    //房地产企业工程的导入
            //    if (fSystemId == "130")
            //    {
            //        SInPutHourseProjectInfo(FAppId, FBaseInfoId, dtTemp);

            //    }
            //    else
            //    {
            //        SInPutProjectInfo(FAppId, FBaseInfoId, dtTemp);
            //    }

            //}

            ////导入企业设备
            //sb.Remove(0, sb.Length);
            //sb.Append(" select * from CF_Ent_Device where fbaseinfoid='" + FBaseInfoId + "'");
            //dtTemp = rc.GetTable(sb.ToString());
            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    SInPutDeviceInfo(FAppId, FBaseInfoId, dtTemp);

            //}
            #endregion

            //导入企业良好行为
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_GoodAction where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                this.SInPutEntGoodInfo(FAppId, FBaseInfoId, dtTemp);

            }


            //导入企业不良行为
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_BadAction where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutEntBadInfo(FAppId, FBaseInfoId, dtTemp);
            }


            //导入企业经营情况(房地产企业)
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_ZCFZ where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutHourseEntWorkInfo(FAppId, FBaseInfoId, dtTemp);
            }

            //导入股东出资信息(房地产企业)
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_InvestPerson where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutInvestPerson(FAppId, FBaseInfoId, dtTemp);
            }

            //导入帐号信息(房地产企业)
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_BankAccount where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutBankAccount(FAppId, FBaseInfoId, dtTemp);
            }

            //导入银行信用等级(房地产企业)
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_CreditLevel where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutCreditLevel(FAppId, FBaseInfoId, dtTemp);
            }

            //导入质量认证信息(房地产企业)
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_Quality where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutQuality(FAppId, FBaseInfoId, dtTemp);
            }

            //导入业务成果
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_ENT_GoodAchieve where fbaseinfoid='" + FBaseInfoId + "'");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                SInPutGoodAchieve(FAppId, FBaseInfoId, dtTemp);
            }

            //导入证书信息
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_Ent_QualiCerti where fbaseinfoid='" + FBaseInfoId + "'");
            sb.Append(" and FIsValid=1 and ftype<>1 ");
            dtTemp = rc.GetTable(sb.ToString());
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                this.SInPutQualiInfo(FAppId, FBaseInfoId, dtTemp);
            }

        }

        private void setLeader(string FAppId, string FBaseInfoId)
        {
            RCenter rc = new RCenter();
            RQuali rq = new RQuali();
            StringBuilder sb = new StringBuilder();
            sb.Append("FBaseInfoId='" + FBaseInfoId + "' ");//and FPersonTypeId="+FType);
            DataTable dt = rc.GetTable(EntityTypeEnum.EbLeader, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            int iCount = dt.Rows.Count;
            if (iCount <= 0) return;

            SortedList[] sl = new SortedList[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            string[] sKey = new string[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string FId = Guid.NewGuid().ToString();
                sl[i] = new SortedList();
                sl[i].Add("FID", Guid.NewGuid().ToString());
                sl[i].Add("FEmpId", dr["FId"].ToString());
                sl[i].Add("FAppId", FAppId);
                sl[i].Add("FBaseInfoId", dr["FBaseInfoId"].ToString());
                sl[i].Add("FPersonTypeId", dr["FPersonTypeId"].ToString());
                sl[i].Add("FName", dr["FName"].ToString());
                sl[i].Add("FSex", dr["FSex"].ToString());
                sl[i].Add("FBirthDay", dr["FBirthDay"].ToString());
                sl[i].Add("FFunction", dr["FFunction"].ToString());
                sl[i].Add("FTechId", dr["FTechId"].ToString());
                sl[i].Add("FDegreeId", dr["FDegreeId"].ToString());
                sl[i].Add("FGraduateSchool", dr["FGraduateSchool"].ToString());
                sl[i].Add("FSpecialtyId", dr["FSpecialtyId"].ToString());
                sl[i].Add("FGraduateTime", dr["FGraduateTime"].ToString());
                sl[i].Add("FWorkYear", dr["FWorkYear"].ToString());
                sl[i].Add("FTel", dr["FTel"].ToString());
                sl[i].Add("FCall", dr["FCall"].ToString());
                sl[i].Add("FAward", dr["FAward"].ToString());
                sl[i].Add("FPrint", dr["FPrint"].ToString());
                sl[i].Add("FBad", dt.Rows[i]["FBad"].ToString());
                sl[i].Add("FIDCardTypeId", dt.Rows[i]["FIDCardTypeId"].ToString());
                sl[i].Add("FCertiNo", dt.Rows[i]["FCertiNo"].ToString());
                sl[i].Add("FGetTime", dt.Rows[i]["FGetTime"].ToString());
                sl[i].Add("FEndTime", dt.Rows[i]["FEndTime"].ToString());
                sl[i].Add("FPubDeptName", dt.Rows[i]["FPubDeptName"].ToString());
                sl[i].Add("FPostcode", dt.Rows[i]["FPostcode"].ToString());
                sl[i].Add("FIdCard", dt.Rows[i]["FIdCard"].ToString());

                sl[i].Add("FAge", dt.Rows[i]["FAge"].ToString());
                sl[i].Add("FEmail", dt.Rows[i]["FEmail"].ToString());
                sl[i].Add("FIsSafety", dt.Rows[i]["FIsSafety"].ToString());
                sl[i].Add("FSafetyNo", dt.Rows[i]["FSafetyNo"].ToString());


                //sl[i].Add("FPhotoUrl", dr["FPhotoUrl"].ToString());
                sl[i].Add("FTxt1", dr["FTxt1"].ToString());
                sl[i].Add("FTxt2", dr["FTxt2"].ToString());
                sl[i].Add("FTxt3", dr["FTxt3"].ToString());
                sl[i].Add("FTxt4", dr["FTxt4"].ToString());
                sl[i].Add("FTxt5", dr["FTxt5"].ToString());
                sl[i].Add("FTxt6", dr["FTxt6"].ToString());
                sl[i].Add("FTxt7", dr["FTxt7"].ToString());
                sl[i].Add("FTxt8", dr["FTxt8"].ToString());
                sl[i].Add("FTxt9", dr["FTxt9"].ToString());
                sl[i].Add("FTxt10", dr["FTxt10"].ToString());
                sl[i].Add("FIsDeleted", 0);
                sl[i].Add("FCreateTime", DateTime.Now);

                so[i] = SaveOptionEnum.Insert;
                sKey[i] = "FID";
                en[i] = EntityTypeEnum.EqEbLeader;
            }


            sb.Remove(0, sb.Length);
            sb.Append(" fisdeleted=0 and  FEmpId in (select fid from CF_Ent_Leader where  FBaseInfoId='" + FBaseInfoId + "') ");
            dt = rc.GetTable(EntityTypeEnum.EeResume, "", sb.ToString());
            SortedList[] sl1 = null;
            EntityTypeEnum[] en1 = null;
            string[] sKey1 = null;
            SaveOptionEnum[] so1 = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                //rq.PExcute("Delete From CF_AppEmp_Resume Where FEmpId='" + sl["FEmpId"].ToString() + "' and FAppId='" + this.Session["FAppId"].ToString() + "'");
                sl1 = new SortedList[dt.Rows.Count];
                en1 = new EntityTypeEnum[dt.Rows.Count];

                sKey1 = new string[dt.Rows.Count];
                so1 = new SaveOptionEnum[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sl1[i] = new SortedList();
                    sl1[i].Add("FID", Guid.NewGuid().ToString());
                    sl1[i].Add("FAppId", FAppId);
                    sl1[i].Add("FEmpId", dt.Rows[i]["FEmpId"].ToString());
                    sl1[i].Add("FBaseInfoName", dt.Rows[i]["FBaseInfoName"].ToString());
                    sl1[i].Add("FBaseInfoId", dt.Rows[i]["FBaseInfoId"].ToString());
                    sl1[i].Add("FWorkContent", dt.Rows[i]["FWorkContent"].ToString());
                    sl1[i].Add("FApprovePerson", dt.Rows[i]["FApprovePerson"].ToString());
                    sl1[i].Add("FApproveTel", dt.Rows[i]["FApproveTel"].ToString());
                    sl1[i].Add("FRemark", dt.Rows[i]["FRemark"].ToString());
                    sl1[i].Add("FBeginTime", dt.Rows[i]["FBeginTime"].ToString());
                    sl1[i].Add("FEndTime", dt.Rows[i]["FEndTime"].ToString());
                    sl1[i].Add("FIsDeleted", 0);
                    sl1[i].Add("FDuty", dt.Rows[i]["FDuty"].ToString());
                    en1[i] = EntityTypeEnum.EqEeResume;
                    sKey1[i] = "FID";
                    so1[i] = SaveOptionEnum.Insert;
                }
            }



            //人员照片
            sb.Remove(0, sb.Length);
            sb.Append(" fisdeleted=0  and  FLinkId in (select fid from CF_Ent_Leader where  FBaseInfoId='" + FBaseInfoId + "') ");
            dt = rc.GetTable(EntityTypeEnum.EPText, "", sb.ToString());
            SortedList[] sl2 = null;
            EntityTypeEnum[] en2 = null;
            string[] sKey2 = null;
            SaveOptionEnum[] so2 = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                //rq.PExcute("Delete From CF_AppEmp_Resume Where FEmpId='" + sl["FEmpId"].ToString() + "' and FAppId='" + this.Session["FAppId"].ToString() + "'");
                sl2 = new SortedList[dt.Rows.Count];
                en2 = new EntityTypeEnum[dt.Rows.Count];

                sKey2 = new string[dt.Rows.Count];
                so2 = new SaveOptionEnum[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sl2[i] = new SortedList();
                    sl2[i].Add("FID", Guid.NewGuid().ToString());
                    sl2[i].Add("FAppId", FAppId);
                    //sl2[i].Add("FEmpId", dt.Rows[i]["FID"].ToString());
                    sl2[i].Add("FContent", dt.Rows[i]["FContent"].ToString());
                    sl2[i].Add("FLinkId", dt.Rows[i]["FLinkId"].ToString());
                    sl2[i].Add("FType", dt.Rows[i]["FType"].ToString());
                    //sl2[i].Add("FApprovePerson", dt.Rows[i]["FApprovePerson"].ToString());
                    //sl2[i].Add("FApproveTel", dt.Rows[i]["FApproveTel"].ToString());
                    //sl2[i].Add("FRemark", dt.Rows[i]["FRemark"].ToString());
                    //sl2[i].Add("FBeginTime", dt.Rows[i]["FBeginTime"].ToString());
                    //sl2[i].Add("FEndTime", dt.Rows[i]["FEndTime"].ToString());
                    sl2[i].Add("FIsDeleted", 0);
                    en2[i] = EntityTypeEnum.EqPText;
                    sKey2[i] = "FID";
                    so2[i] = SaveOptionEnum.Insert;
                }
            }

            if (rq.SaveEBaseM(en, sl, sKey, so))
            {
                rq.SaveEBaseM(en1, sl1, sKey1, so1);
                rq.SaveEBaseM(en2, sl2, sKey2, so2);
            }
        }

        //判断一个企业上报的业务是否都审批完毕
        public bool IsAllAppEnd(string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from cf_app_list ");
            sb.Append(" where fstate<>6 ");
            sb.Append(" and fisdeleted=0 ");
            sb.Append(" and fbaseinfoid='" + fBaseInfoId + "'");
            int iCount = GetSQLCount(sb.ToString());
            if (iCount <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //得到某一次业务申请除主项外的最大资质等级
        public int GetNoPMaxReportLevel(string fAppId, string fBaseInfoId)
        {
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();
            sb.Append(" select flevelid from CF_App_Detail ");
            sb.Append(" where fappid ='" + fAppId + "'");
            sb.Append(" and fbaseinfoid ='" + fBaseInfoId + "' ");
            sb.Append(" and FIsPrime<>1 ");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return int.MaxValue;
            }
            int iCount = dt.Rows.Count;
            sb.Remove(0, sb.Length);
            sb.Append(" select top 1 flevel from CF_Sys_QualiLevel where fnumber in (");
            for (int i = 0; i < iCount; i++)
            {
                if (i == 0)
                {
                    sb.Append("'" + dt.Rows[i][0].ToString() + "'");
                }
                else
                {
                    sb.Append(",'" + dt.Rows[i][0].ToString() + "'");
                }
            }
            sb.Append(")  order by flevel");

            string fMaxLevel = rc.GetSignValue(sb.ToString());
            try
            {

                if (fMaxLevel == null || fMaxLevel == "")
                {
                    return int.MaxValue;
                }
                return EConvert.ToInt(fMaxLevel);
            }
            catch (Exception ex)
            {
                return int.MaxValue;
            }
        }

        //得到某一次业务申请除主项的资质等级
        public int GetPReportLevel(string fAppId, string fBaseInfoId)
        {
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder(0);
            sb.Append(" select fLevelId from CF_App_Detail ");
            sb.Append(" where fAppId='" + fAppId + "' ");
            sb.Append(" and fBaseInfoId='" + fBaseInfoId + "' ");
            sb.Append(" and FIsPrime=1 ");
            string PReportLevelId = this.GetSignValue(sb.ToString());
            return rc.GetQualiLevelByNumber(PReportLevelId);
        }

        //得到一个企业的主项资质
        public int GetEnPLevle(string fBaseInfoId)
        {
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder(0);
            sb.Append(" select fLevelId from CF_Ent_QualiCertiTrade ");
            sb.Append(" where fBaseInfoId='" + fBaseInfoId + "' ");
            sb.Append(" and FIsBase=1 and FState=1");
            string PReportLevelId = rc.GetSignValue(sb.ToString());
            return rc.GetQualiLevelByNumber(PReportLevelId);
        }

        //得到一个企业除主项外的最大资质等级
        public int GetEntNoPLevel(string fBaseInfoId)
        {
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder(0);
            sb.Append(" select top 1 flevel from CF_Sys_QualiLevel where fnumber in (");
            sb.Append(" select fLevelId from CF_Ent_QualiCertiTrade ");
            sb.Append(" where fBaseInfoId='" + fBaseInfoId + "' ");
            sb.Append(" and FIsBase<>1 and FState=1)  order by flevel");
            string fMaxLevel = rc.GetSignValue(sb.ToString());
            try
            {
                if (fMaxLevel == null || fMaxLevel == "")
                {
                    return int.MaxValue;
                }
                return EConvert.ToInt(fMaxLevel);
            }
            catch (Exception ex)
            {
                return int.MaxValue;
            }
        }

        //是否有企业基本信息数据
        public bool IsHasEntData(string fAppId, string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fid from cf_appent_baseinfo ");
            sb.Append(" where fbaseinfoid ='" + fBaseInfoId + "'");
            sb.Append(" and fappid='" + fAppId + "' and fisdeleted=0 ");
            string fId = GetSignValue(sb.ToString());
            if (fId == null || fId == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //删除某一次业务申请
        public bool DelApply(string fAppId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" begin ");
            sb.Append(" delete from CF_AppEnt_BaseInfo where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_FINANCE where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_App_Detail where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_App_List where FId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEmp_Baseinfo where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEmp_Resume where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_BadAction where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_Device where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_GoodAction where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_Leader where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_Project where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppPub_Text where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_ZCFZ where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_Quality where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_ProjectManager where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_ProjBackUp where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_InvestPerson where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_BankAccount where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEnt_CreditLevel where FAPPId='" + fAppId + "' ");
            sb.Append(" end ");
            try
            {
                this.PExcute(sb.ToString());
                sb.Remove(0, sb.Length);
                sb.Append(" begin ");
                sb.Append(" delete from CF_App_ProcessRecord where flinkid ='" + fAppId + "' ");
                sb.Append(" delete from CF_App_ProcessInstance where flinkid ='" + fAppId + "' ");
                sb.Append(" end ");
                RCenter rc = new RCenter();
                rc.PExcute(sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region 删除某一次业务申请.施工许可证专用
        public bool DelApply_SGXK(string fAppId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" begin ");
            sb.Append("delete from CQ_Prj_BaseInfo where FAPPId='" + fAppId + "';");
            sb.Append("delete from CQ_PrjGC_ItemInfo where FAPPId='" + fAppId + "';");
            sb.Append(" delete from CF_AppPrj_Data where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppPrj_BaseInfo where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_App_Detail where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_App_List where FId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppEmp_Baseinfo where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppPrj_Ent where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppPrj_File where FAPPId='" + fAppId + "' ");
            sb.Append(" delete from CF_AppPrj_FileOther where FAPPId='" + fAppId + "' ");

            sb.Append(" end ");
            try
            {
                this.PExcute(sb.ToString());
                sb.Remove(0, sb.Length);
                sb.Append(" begin ");
                sb.Append(" delete from CF_App_ProcessRecord where flinkid ='" + fAppId + "' ");
                sb.Append(" delete from CF_App_ProcessInstance where flinkid ='" + fAppId + "' ");
                sb.Append(" end ");
                RCenter rc = new RCenter();
                rc.PExcute(sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        //撤销某一次业务申请
        public bool CancelApply(string fAppId)
        {
            StringBuilder sb = new StringBuilder();
       
         
                //this.PExcute(sb.ToString());
                sb.Remove(0, sb.Length);
                sb.Append(" begin ");  
                sb.Append("update CF_App_List set fstate=0 where FId='" + fAppId + "' ");
                sb.Append("delete from CF_App_ProcessRecord ");
                sb.Append("where fprocessInstanceId in (");
                sb.Append("select fid from CF_App_ProcessInstance where flinkid='" + fAppId + "' and isnull(FState,0) not in (2,6)");
                sb.Append(");");
                sb.Append("delete from CF_App_ProcessInstance where flinkid='" + fAppId + "' and isnull(FState,0) not in (2,6)");
                sb.Append(" end ");
                RCenter rc = new RCenter();
                rc.PExcute(sb.ToString());
                return true;
          
        }

        //导入企业基本信息数据
        public void SInPutBaseInfo(string fBaseInfoId, string fAppId)
        {
            RCenter rc = new RCenter();

            SaveOptionEnum so = SaveOptionEnum.Insert;
            string FId = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" fbaseinfoid='" + fBaseInfoId + "' and FAPPId='" + fAppId + "'");
            DataTable dt = this.GetTable(EntityTypeEnum.EqEbBaseInfo, "fid", sb.ToString());
            if (dt.Rows.Count > 0)
            {
                FId = dt.Rows[0]["FID"].ToString();
                so = SaveOptionEnum.Update;
            }

            sb.Remove(0, sb.Length);
            sb.Append("FId='" + fBaseInfoId + "'");
            dt = rc.GetTable(EntityTypeEnum.EbBaseInfo, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            string fSystemId = dt.Rows[0]["FSystemId"].ToString();

            SortedList sl = new SortedList();
            string sKey = "FID";
            EntityTypeEnum en = EntityTypeEnum.EqEbBaseInfo;

            sl.Add("FID", FId);
            sl.Add("FBaseInfoId", fBaseInfoId);
            sl.Add("FAPPId", fAppId);
            sl.Add("FJuridcialCode", dt.Rows[0]["FJuridcialCode"].ToString());
            sl.Add("FEntCode", dt.Rows[0]["FEntCode"].ToString());
            sl.Add("FName", dt.Rows[0]["FName"].ToString());
            sl.Add("FOtherName", dt.Rows[0]["FOtherName"].ToString());
            sl.Add("FTradeId", dt.Rows[0]["FTradeId"].ToString());
            sl.Add("FSubjectionId", dt.Rows[0]["FSubjectionId"].ToString());
            sl.Add("FManageDeptName", dt.Rows[0]["FManageDeptName"].ToString());
            sl.Add("FUpDeptId", dt.Rows[0]["FUpDeptId"].ToString());
            sl.Add("FBankName", dt.Rows[0]["FBankName"].ToString());
            sl.Add("FBankAccountNo", dt.Rows[0]["FBankAccountNo"].ToString());
            sl.Add("FLicence", dt.Rows[0]["FLicence"].ToString());
            sl.Add("FRegistFund", dt.Rows[0]["FRegistFund"].ToString());
            sl.Add("FRegistFundUnitId", dt.Rows[0]["FRegistFundUnitId"].ToString());
            sl.Add("FEntCreateFileNo", dt.Rows[0]["FEntCreateFileNo"].ToString());
            sl.Add("FEntTypeId", dt.Rows[0]["FEntTypeId"].ToString());
            sl.Add("FRegistDeptId", dt.Rows[0]["FRegistDeptId"].ToString());
            sl.Add("FRegistAddress", dt.Rows[0]["FRegistAddress"].ToString());
            sl.Add("FRegistPostcode", dt.Rows[0]["FRegistPostcode"].ToString());
            sl.Add("FAddress", dt.Rows[0]["FAddress"].ToString());
            sl.Add("FPostcode", dt.Rows[0]["FPostcode"].ToString());
            sl.Add("FLinkMan", dt.Rows[0]["FLinkMan"].ToString());
            sl.Add("FMobile", dt.Rows[0]["FMobile"].ToString());
            sl.Add("FTel", dt.Rows[0]["FTel"].ToString());
            sl.Add("FFax", dt.Rows[0]["FFax"].ToString());
            sl.Add("FUrl", dt.Rows[0]["FUrl"].ToString());
            sl.Add("FEmail", dt.Rows[0]["FEmail"].ToString());
            sl.Add("FEntCreateTime", dt.Rows[0]["FEntCreateTime"].ToString());
            sl.Add("FRegistDate", dt.Rows[0]["FRegistDate"].ToString());
            sl.Add("FApproveDate", dt.Rows[0]["FApproveDate"].ToString());
            sl.Add("FEntAppDeptName", dt.Rows[0]["FEntAppDeptName"].ToString());
            sl.Add("FCreditLevel", dt.Rows[0]["FCreditLevel"].ToString());
            sl.Add("FCreditScore", dt.Rows[0]["FCreditScore"].ToString());
            sl.Add("FInt1", dt.Rows[0]["FInt1"].ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);

            if (fSystemId == "180")//不是外来施工的企业
            {
                sl.Add("FOCertiNo", dt.Rows[0]["FOCertiNo"].ToString());
                sl.Add("FPTxt", dt.Rows[0]["FPTxt"].ToString());
                sl.Add("FOTxt1", dt.Rows[0]["FOTxt1"].ToString().Trim());
                sl.Add("FOTxt2", dt.Rows[0]["FOTxt2"].ToString().Trim());
                sl.Add("FOTxt3", dt.Rows[0]["FOTxt3"].ToString().Trim());
                sl.Add("FOTxt4", dt.Rows[0]["FOTxt4"].ToString().Trim());
                sl.Add("FOTxt5", dt.Rows[0]["FOTxt5"].ToString().Trim());
            }
            else
            {
                if (fSystemId == "175")
                {
                    sl.Add("FOTxt1", dt.Rows[0]["FOTxt1"].ToString().Trim());
                    sl.Add("FOTxt2", dt.Rows[0]["FOTxt2"].ToString().Trim());
                    sl.Add("FOTxt3", dt.Rows[0]["FOTxt3"].ToString().Trim());
                    sl.Add("FOTxt4", dt.Rows[0]["FOTxt4"].ToString().Trim());
                    sl.Add("FOTxt5", dt.Rows[0]["FOTxt5"].ToString().Trim());
                }
                else
                {
                    sb.Remove(0, sb.Length);
                    sb.Append(" select top 6 FTypeName,FLevelName,FAppTime,FIsBase From CF_Ent_QualiCertiTrade where fbaseinfoid='" + fBaseInfoId + "' ");
                    sb.Append(" and FState=1 order by FIsBase desc,FAppTime desc ");
                    dt = rc.GetTable(sb.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] row = dt.Select(" FIsBase=1");
                        if (row != null && row.Length > 0)
                        {
                            sl.Add("FPTxt", row[0]["FTypeName"].ToString().Trim() + row[0]["FLevelName"].ToString().Trim() + "  " + rc.StrToDate(row[0]["FAppTime"].ToString().Trim()));
                            dt.Rows.Remove(row[0]);
                        }
                        int iRowCount = dt.Rows.Count;
                        if (iRowCount < 5)
                        {
                            for (int i = 0; i < 5 - iRowCount; i++)
                            {
                                dt.Rows.Add(dt.NewRow());
                            }
                        }

                        sl.Add("FOTxt1", dt.Rows[0]["FTypeName"].ToString().Trim() + dt.Rows[0]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[0]["FAppTime"].ToString().Trim()));
                        sl.Add("FOTxt2", dt.Rows[1]["FTypeName"].ToString().Trim() + dt.Rows[1]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[1]["FAppTime"].ToString().Trim()));
                        sl.Add("FOTxt3", dt.Rows[2]["FTypeName"].ToString().Trim() + dt.Rows[2]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[2]["FAppTime"].ToString().Trim()));
                        sl.Add("FOTxt4", dt.Rows[3]["FTypeName"].ToString().Trim() + dt.Rows[3]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[3]["FAppTime"].ToString().Trim()));
                        sl.Add("FOTxt5", dt.Rows[4]["FTypeName"].ToString().Trim() + dt.Rows[4]["FLevelName"].ToString().Trim() + "   " + rc.StrToDate(dt.Rows[4]["FAppTime"].ToString().Trim()));
                    }
                }

                sb.Remove(0, sb.Length);
                sb.Append("select distinct(FCertiNo) from CF_Ent_QualiCerti where fbaseinfoid='" + fBaseInfoId + "' and FIsValid=1");
                dt = rc.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    sl.Add("FOCertiNo", dt.Rows[0]["FCertiNo"].ToString());
                }
            }

            this.SaveEBase(EntityTypeEnum.EqEbBaseInfo, sl, "FID", so);
        }

        //导入企业其他信息数据
        public void SInPutBaseInfoOther(string fBaseInfoId, string fAppId)
        {
            RCenter rc = new RCenter();

            SaveOptionEnum so = SaveOptionEnum.Insert;
            string FId = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" fbaseinfoid='" + fBaseInfoId + "' and FAPPId='" + fAppId + "'");
            DataTable dt = this.GetTable(EntityTypeEnum.EqEbBaseInfoOther, "fid", sb.ToString());
            if (dt.Rows.Count > 0)
            {
                FId = dt.Rows[0]["FID"].ToString();
                so = SaveOptionEnum.Update;
            }

            sb.Remove(0, sb.Length);
            sb.Append("fBaseInfoId='" + fBaseInfoId + "'");
            dt = rc.GetTable(EntityTypeEnum.EbBaseInfoOther, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            SortedList sl = new SortedList();
            string sKey = "FID";
            EntityTypeEnum en = EntityTypeEnum.EqEbBaseInfoOther;

            sl.Add("FID", FId);
            sl.Add("FBaseInfoId", fBaseInfoId);
            sl.Add("FAppId", fAppId);
            sl.Add("FIsDeleted", 0);
            for (int i = 1; i <= 68; i++)
            {
                sl.Add("FNb" + i, dt.Rows[0]["FNb" + i].ToString());
            }
            for (int j = 1; j <= 56; j++)
            {
                sl.Add("FFL" + j, dt.Rows[0]["FFL" + j].ToString());
            }
            for (int k = 1; k <= 12; k++)
            {
                sl.Add("FTx" + k, dt.Rows[0]["FTx" + k].ToString());
            }
            this.SaveEBase(EntityTypeEnum.EqEbBaseInfoOther, sl, "FID", so);
        }

        //导入企业财务表
        public void SInPutEntFinance(string fBaseInfoId, string fAppId)
        {
            RCenter rc = new RCenter();

            SaveOptionEnum so = SaveOptionEnum.Insert;
            string FId = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" fbaseinfoid='" + fBaseInfoId + "' and FAPPId='" + fAppId + "'");
            DataTable dt = this.GetTable(EntityTypeEnum.EqEbFinance, "fid", sb.ToString());
            if (dt.Rows.Count > 0)
            {
                FId = dt.Rows[0]["FID"].ToString();
                so = SaveOptionEnum.Update;
            }

            sb.Remove(0, sb.Length);
            sb.Append("fBaseInfoId='" + fBaseInfoId + "'");
            dt = rc.GetTable(EntityTypeEnum.EbFinance, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            SortedList sl = new SortedList();
            string sKey = "FID";
            EntityTypeEnum en = EntityTypeEnum.EqEbFinance;

            sl.Add("FID", FId);
            sl.Add("FBaseInfoId", fBaseInfoId);
            sl.Add("FAppId", fAppId);
            sl.Add("FIsDeleted", 0);
            for (int i = 1; i <= 41; i++)
            {
                sl.Add("FInt" + i, dt.Rows[0]["FInt" + i].ToString());
            }
            for (int j = 1; j <= 43; j++)
            {
                sl.Add("FLt" + j, dt.Rows[0]["FLt" + j].ToString());
            }
            for (int k = 1; k <= 1; k++)
            {
                sl.Add("FTxt" + k, dt.Rows[0]["FTxt" + k].ToString());
            }
            this.SaveEBase(EntityTypeEnum.EqEbFinance, sl, "FID", so);
        }

        //导入企业负责人信息数据
        public void SInputEntLeaderInfo(string fBaseInfoId, string fAppId, string fPersonTypeId)
        {
            RCenter rc = new RCenter();
            RQuali rq = new RQuali();
            StringBuilder sb = new StringBuilder();
            sb.Append("FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId);//and FPersonTypeId="+FType);
            DataTable dt = rc.GetTable(EntityTypeEnum.EbLeader, "", sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            int iCount = dt.Rows.Count;
            if (iCount <= 0) return;

            SortedList[] sl = new SortedList[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            string[] sKey = new string[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string FId = rq.GetSignValue(EntityTypeEnum.EqEbLeader, "FId", "FAppId='" + fAppId + "' and FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId);
                if (FId != null && FId != "")
                {
                    so[i] = SaveOptionEnum.Update;
                }
                else
                {
                    FId = Guid.NewGuid().ToString();
                    so[i] = SaveOptionEnum.Insert;
                }

                sl[i] = new SortedList();
                sl[i].Add("FID", FId);
                sl[i].Add("FEmpId", dr["FId"].ToString());
                sl[i].Add("FAppId", fAppId);
                sl[i].Add("FBaseInfoId", dr["FBaseInfoId"].ToString());
                sl[i].Add("FPersonTypeId", dr["FPersonTypeId"].ToString());
                sl[i].Add("FName", dr["FName"].ToString());
                sl[i].Add("FSex", dr["FSex"].ToString());
                sl[i].Add("FBirthDay", dr["FBirthDay"].ToString());
                sl[i].Add("FFunction", dr["FFunction"].ToString());
                sl[i].Add("FTechId", dr["FTechId"].ToString());
                sl[i].Add("FDegreeId", dr["FDegreeId"].ToString());
                sl[i].Add("FGraduateSchool", dr["FGraduateSchool"].ToString());
                sl[i].Add("FSpecialtyId", dr["FSpecialtyId"].ToString());
                sl[i].Add("FGraduateTime", dr["FGraduateTime"].ToString());
                sl[i].Add("FWorkYear", dr["FWorkYear"].ToString());
                sl[i].Add("FTel", dr["FTel"].ToString());
                sl[i].Add("FCall", dr["FCall"].ToString());
                sl[i].Add("FAward", dr["FAward"].ToString());
                sl[i].Add("FPrint", dr["FPrint"].ToString());
                sl[i].Add("FBad", dt.Rows[i]["FBad"].ToString());
                sl[i].Add("FCertiNo", dt.Rows[i]["FCertiNo"].ToString());
                sl[i].Add("FGetTime", dt.Rows[i]["FGetTime"].ToString());
                sl[i].Add("FEndTime", dt.Rows[i]["FEndTime"].ToString());
                sl[i].Add("FPubDeptName", dt.Rows[i]["FPubDeptName"].ToString());
                sl[i].Add("FIdCard", dt.Rows[i]["FIdCard"].ToString());
                sl[i].Add("FAge", dt.Rows[i]["FAge"].ToString());
                sl[i].Add("FEmail", dt.Rows[i]["FEmail"].ToString());
                sl[i].Add("FIsSafety", dt.Rows[i]["FIsSafety"].ToString());
                sl[i].Add("FSafetyNo", dt.Rows[i]["FSafetyNo"].ToString());
                sl[i].Add("FIDCardTypeId", dt.Rows[i]["FIDCardTypeId"].ToString());
                sl[i].Add("fpostcode", dt.Rows[i]["fpostcode"].ToString());

                sl[i].Add("FTxt1", dr["FTxt1"].ToString());
                sl[i].Add("FTxt2", dr["FTxt2"].ToString());
                sl[i].Add("FTxt3", dr["FTxt3"].ToString());
                sl[i].Add("FTxt4", dr["FTxt4"].ToString());
                sl[i].Add("FTxt5", dr["FTxt5"].ToString());
                sl[i].Add("FTxt6", dr["FTxt6"].ToString());
                sl[i].Add("FTxt7", dr["FTxt7"].ToString());
                sl[i].Add("FTxt8", dr["FTxt8"].ToString());
                sl[i].Add("FTxt9", dr["FTxt9"].ToString());
                sl[i].Add("FTxt10", dr["FTxt10"].ToString());
                sl[i].Add("FIsDeleted", 0);
                sl[i].Add("FCreateTime", DateTime.Now);
                sl[i].Add("FRemark", dr["FRemark"].ToString());
                sKey[i] = "FID";
                en[i] = EntityTypeEnum.EqEbLeader;
            }


            sb.Remove(0, sb.Length);
            sb.Append(" delete from CF_AppEmp_Resume");
            sb.Append(" where fisdeleted=0 and  FEmpId in (select FEmpId from CF_AppEnt_Leader where  FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId);
            sb.Append(" and FAppId='" + fAppId + "') ");
            rq.PExcute(sb.ToString());

            sb.Remove(0, sb.Length);
            sb.Append(" fisdeleted=0 and  FEmpId in (select fid from CF_Ent_Leader where  FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId + ") ");
            dt = rc.GetTable(EntityTypeEnum.EeResume, "", sb.ToString());
            SortedList[] sl1 = null;
            EntityTypeEnum[] en1 = null;
            string[] sKey1 = null;
            SaveOptionEnum[] so1 = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                //rq.PExcute("Delete From CF_AppEmp_Resume Where FEmpId='" + sl["FEmpId"].ToString() + "' and FAppId='" + this.Session["FAppId"].ToString() + "'");
                sl1 = new SortedList[dt.Rows.Count];
                en1 = new EntityTypeEnum[dt.Rows.Count];

                sKey1 = new string[dt.Rows.Count];
                so1 = new SaveOptionEnum[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sl1[i] = new SortedList();
                    sl1[i].Add("FID", Guid.NewGuid().ToString());
                    sl1[i].Add("FAppId", fAppId);
                    sl1[i].Add("FEmpId", dt.Rows[i]["FEmpId"].ToString());
                    sl1[i].Add("FBaseInfoName", dt.Rows[i]["FBaseInfoName"].ToString());
                    sl1[i].Add("FBaseInfoId", dt.Rows[i]["FBaseInfoId"].ToString());
                    sl1[i].Add("FWorkContent", dt.Rows[i]["FWorkContent"].ToString());
                    sl1[i].Add("FApprovePerson", dt.Rows[i]["FApprovePerson"].ToString());
                    sl1[i].Add("FApproveTel", dt.Rows[i]["FApproveTel"].ToString());
                    sl1[i].Add("FRemark", dt.Rows[i]["FRemark"].ToString());
                    sl1[i].Add("FBeginTime", dt.Rows[i]["FBeginTime"].ToString());
                    sl1[i].Add("FEndTime", dt.Rows[i]["FEndTime"].ToString());
                    sl1[i].Add("FIsDeleted", 0);
                    sl1[i].Add("FDuty", dt.Rows[i]["FDuty"].ToString());
                    en1[i] = EntityTypeEnum.EqEeResume;
                    sKey1[i] = "FID";
                    so1[i] = SaveOptionEnum.Insert;
                }
            }



            //人员照片
            sb.Remove(0, sb.Length);
            sb.Append(" delete from CF_AppPub_Text");
            sb.Append(" where fisdeleted=0 and  FLinkId in (select FEmpId from CF_AppEnt_Leader where  FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId);
            sb.Append(" and FAppId='" + fAppId + "') ");
            rq.PExcute(sb.ToString());

            sb.Remove(0, sb.Length);
            sb.Append(" fisdeleted=0 and    FLinkId in (select fid from CF_Ent_Leader where  FBaseInfoId='" + fBaseInfoId + "' and FPersonTypeId=" + fPersonTypeId + ") ");
            dt = rc.GetTable(EntityTypeEnum.EPText, "", sb.ToString());
            SortedList[] sl2 = null;
            EntityTypeEnum[] en2 = null;
            string[] sKey2 = null;
            SaveOptionEnum[] so2 = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                sl2 = new SortedList[dt.Rows.Count];
                en2 = new EntityTypeEnum[dt.Rows.Count];
                sKey2 = new string[dt.Rows.Count];
                so2 = new SaveOptionEnum[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sl2[i] = new SortedList();
                    sl2[i].Add("FID", Guid.NewGuid().ToString());
                    sl2[i].Add("FAppId", fAppId);
                    sl2[i].Add("FContent", dt.Rows[i]["FContent"].ToString());
                    sl2[i].Add("FLinkId", dt.Rows[i]["FLinkId"].ToString());
                    sl2[i].Add("FType", dt.Rows[i]["FType"].ToString());
                    sl2[i].Add("FIsDeleted", 0);
                    en2[i] = EntityTypeEnum.EqPText;
                    sKey2[i] = "FID";
                    so2[i] = SaveOptionEnum.Insert;
                }
            }

            if (rq.SaveEBaseM(en, sl, sKey, so))
            {
                rq.SaveEBaseM(en1, sl1, sKey1, so1);
                rq.SaveEBaseM(en2, sl2, sKey2, so2);
            }
        }





        //导入企业文本信息数据(简历,规章制度,框架图等)
        public void SInPutTextInfo(string fBaseInfoId, string fAppId, string fType)
        {
            RQuali rq = new RQuali();
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();

            sb.Append(" select * from CF_Pub_Text where FlinkId='" + fBaseInfoId + "'");
            sb.Append(" and ftype='" + fType + "' ");
            DataTable dt = rc.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            sb.Remove(0, sb.Length);
            sb.Append(" delete from CF_AppPub_Text where FAppId='" + fAppId + "' ");
            sb.Append(" and FlinkId='" + fBaseInfoId + "' and FType='" + fType + "' ");
            rq.PExcute(sb.ToString());

            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Insert;
            string fkey = "FID";
            EntityTypeEnum en = EntityTypeEnum.EqPText;
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FAppId", fAppId);
            sl.Add("FContent", dt.Rows[0]["FContent"].ToString());
            sl.Add("FISDELETED", 0);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FLinkId", fBaseInfoId);
            sl.Add("FType", dt.Rows[0]["FType"].ToString());
            rq.SaveEBase(en, sl, fkey, so);
        }



        //导入企业人员信息数据
        public void SInPutEmpInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            RCenter rc = new RCenter();

            StringBuilder sb = new StringBuilder();

            ArrayList arraySl = new ArrayList();
            ArrayList arrayEn = new ArrayList();
            ArrayList arrayFKey = new ArrayList();
            ArrayList arraySo = new ArrayList();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                SortedList sl = new SortedList();
                EntityTypeEnum en = EntityTypeEnum.EqEeBaseinfo;
                string feky = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;


                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", fAppId);
                sl.Add("FEmpId", dt.Rows[j]["FId"].ToString());
                sl.Add("FBaseInfoID", fBaseInfoId);
                sl.Add("FName", dt.Rows[j]["FName"].ToString());
                sl.Add("FSex", dt.Rows[j]["FSex"].ToString());
                sl.Add("FBirthDay", dt.Rows[j]["FBirthDay"].ToString());
                sl.Add("FFunction", dt.Rows[j]["FFunction"].ToString());
                sl.Add("FTechId", dt.Rows[j]["FTechId"].ToString());
                sl.Add("FGraduateTime", dt.Rows[j]["FGraduateTime"].ToString());
                sl.Add("FGraduateSchool", dt.Rows[j]["FGraduateSchool"].ToString());
                sl.Add("FSpecialtyId", dt.Rows[j]["FSpecialtyId"].ToString());
                sl.Add("FIdCard", dt.Rows[j]["FIdCard"].ToString());
                sl.Add("FCall", dt.Rows[j]["FCall"].ToString());
                sl.Add("FTEL", dt.Rows[j]["FTEL"].ToString());
                sl.Add("FWorkState", dt.Rows[j]["FWorkState"].ToString());
                sl.Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl.Add("FIDCardTypeId", dt.Rows[j]["FIDCardTypeId"].ToString());
                sl.Add("FDegreeId", dt.Rows[j]["FDegreeId"].ToString());
                sl.Add("FWorkYear", dt.Rows[j]["FWorkYear"].ToString());
                sl.Add("FAge", dt.Rows[j]["FAge"].ToString());
                sl.Add("FPersonTypeId", dt.Rows[j]["FPersonTypeId"].ToString());
                sl.Add("FTypeId", dt.Rows[j]["FTypeId"].ToString());
                sl.Add("FRegistSpecialId", dt.Rows[j]["FRegistSpecialId"].ToString());
                sl.Add("FRegistSpecialId2", dt.Rows[j]["FRegistSpecialId2"].ToString());
                sl.Add("FCertiNo", dt.Rows[j]["FCertiNo"].ToString());
                sl.Add("FPrintNo", dt.Rows[j]["FPrintNo"].ToString());
                sl.Add("FLevelId", dt.Rows[j]["FLevelId"].ToString());
                sl.Add("FGetTime", dt.Rows[j]["FGetTime"].ToString());
                sl.Add("FGetTypeId", dt.Rows[j]["FGetTypeId"].ToString());
                sl.Add("FEndTime", dt.Rows[j]["FEndTime"].ToString());
                sl.Add("FPubDeptName", dt.Rows[j]["FPubDeptName"].ToString());
                sl.Add("FEmail", dt.Rows[j]["FEmail"].ToString());
                sl.Add("FBBeginTime", dt.Rows[j]["FBBeginTime"].ToString());
                sl.Add("FBEndTime", dt.Rows[j]["FBEndTime"].ToString());
                sl.Add("FNation", dt.Rows[j]["FNation"].ToString());
                sl.Add("FAddress", dt.Rows[j]["FAddress"].ToString());
                sl.Add("FPostCode", dt.Rows[j]["FPostCode"].ToString());
                sl.Add("FResume", dt.Rows[j]["FResume"].ToString());
                sl.Add("FRegisfNation", dt.Rows[j]["FRegisfNation"].ToString());
                sl.Add("FUnit", dt.Rows[j]["FUnit"].ToString());
                sl.Add("FWorkKind", dt.Rows[j]["FWorkKind"].ToString());
                sl.Add("FWorkSpecialId", dt.Rows[j]["FWorkSpecialId"].ToString());
                sl.Add("FIsSafety", dt.Rows[j]["FIsSafety"].ToString());
                sl.Add("FSafetyNo", dt.Rows[j]["FSafetyNo"].ToString());

                sl.Add("FRegistDate", dt.Rows[j]["FRegistDate"].ToString());
                sl.Add("FSealNo", dt.Rows[j]["FSealNo"].ToString());
                sl.Add("FInsuranceNo", dt.Rows[j]["FInsuranceNo"].ToString());
                sl.Add("FInsuranceTel", dt.Rows[j]["FInsuranceTel"].ToString());
                sl.Add("FInsurancePerson", dt.Rows[j]["FInsurancePerson"].ToString());
                sl.Add("FInsuranceRemark", dt.Rows[j]["FInsuranceRemark"].ToString());

                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);
                sl.Add("FRState", dt.Rows[j]["FRState"].ToString());
                sl.Add("FGood", dt.Rows[j]["FGood"].ToString());

                arrayEn.Add(en);
                arrayFKey.Add(feky);
                arraySl.Add(sl);
                arraySo.Add(so);


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppPub_Text where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Pub_Text  where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fisdeleted=0");
                DataTable dt1 = rc.GetTable(sb.ToString());
                if (dt != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int p = 0; p < iCount; p++)
                    {
                        en = EntityTypeEnum.EqPText;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FLinkId", dt1.Rows[p]["FLinkId"].ToString());
                        sl.Add("FType", dt1.Rows[p]["FType"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FContent", dt1.Rows[p]["FContent"].ToString());
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkAchievement");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkAchievement;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProvePeople", dt1.Rows[y]["FProvePeople"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FProjectName", dt1.Rows[y]["FProjectName"].ToString());
                        sl.Add("FProjectLevel", dt1.Rows[y]["FProjectLevel"].ToString());
                        sl.Add("FProjectUnit", dt1.Rows[y]["FProjectUnit"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FResult", dt1.Rows[y]["FResult"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkExperience");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkExperience;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProjectId", dt1.Rows[y]["FProjectId"].ToString());
                        sl.Add("FProjectTypeId", dt1.Rows[y]["FProjectTypeId"].ToString());
                        sl.Add("FCost", dt1.Rows[y]["FCost"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FBaseinfoName", dt1.Rows[y]["FBaseinfoName"].ToString());
                        sl.Add("FAppPerson", dt1.Rows[y]["FAppPerson"].ToString());
                        sl.Add("FCompany", dt1.Rows[y]["FCompany"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FAward", dt1.Rows[y]["FAward"].ToString());
                        sl.Add("FBad", dt1.Rows[y]["FBad"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBusiness", dt1.Rows[y]["FBusiness"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FDesc", dt1.Rows[y]["FDesc"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_Resume where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_Resume");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeResume;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FBaseInfoName", dt1.Rows[y]["FBaseInfoName"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FApprovePerson", dt1.Rows[y]["FApprovePerson"].ToString());
                        sl.Add("FApproveTel", dt1.Rows[y]["FApproveTel"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FDesc", dt1.Rows[y]["FDesc"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }

            }
            int iCount1 = arraySl.Count;
            if (iCount1 != 0)
            {
                EntityTypeEnum[] ens = new EntityTypeEnum[iCount1];
                string[] fkeys = new string[iCount1];
                SortedList[] sls = new SortedList[iCount1];
                SaveOptionEnum[] sos = new SaveOptionEnum[iCount1];

                for (int k = 0; k < iCount1; k++)
                {
                    ens[k] = (EntityTypeEnum)(arrayEn[k]);
                    fkeys[k] = (String)(arrayFKey[k]);
                    sls[k] = (SortedList)(arraySl[k]);
                    sos[k] = (SaveOptionEnum)(arraySo[k]);

                }
                this.SaveEBaseM(ens, sls, fkeys, sos);
            }

        }



        //导入企业负责人信息数据
        public void SInputEntLeaderInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            RCenter rc = new RCenter();
            RQuali rq = new RQuali();
            StringBuilder sb = new StringBuilder();
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            int iCount = dt.Rows.Count;
            if (iCount <= 0) return;


            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();

            SortedList sl = new SortedList();
            SaveOptionEnum so = new SaveOptionEnum();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string FId = rq.GetSignValue(EntityTypeEnum.EqEbLeader, "FId", "FAppId='" + fAppId + "' and FBaseInfoId='" + fBaseInfoId + "' and FEmpId='" + dt.Rows[i]["FId"].ToString() + "'");
                if (FId != null && FId != "")
                {
                    so = SaveOptionEnum.Update;
                }
                else
                {
                    FId = Guid.NewGuid().ToString();
                    so = SaveOptionEnum.Insert;
                }

                sl = new SortedList();
                sl.Add("FID", FId);
                sl.Add("FEmpId", dr["FId"].ToString());
                sl.Add("FAppId", fAppId);
                sl.Add("FBaseInfoId", dr["FBaseInfoId"].ToString());
                sl.Add("FPersonTypeId", dr["FPersonTypeId"].ToString());
                sl.Add("FName", dr["FName"].ToString());
                sl.Add("FSex", dr["FSex"].ToString());
                sl.Add("FBirthDay", dr["FBirthDay"].ToString());
                sl.Add("FFunction", dr["FFunction"].ToString());
                sl.Add("FTechId", dr["FTechId"].ToString());
                sl.Add("FDegreeId", dr["FDegreeId"].ToString());
                sl.Add("FGraduateSchool", dr["FGraduateSchool"].ToString());
                sl.Add("FSpecialtyId", dr["FSpecialtyId"].ToString());
                sl.Add("FGraduateTime", dr["FGraduateTime"].ToString());
                sl.Add("FWorkYear", dr["FWorkYear"].ToString());
                sl.Add("FTel", dr["FTel"].ToString());
                sl.Add("FCall", dr["FCall"].ToString());
                sl.Add("FAward", dr["FAward"].ToString());
                sl.Add("FPrint", dr["FPrint"].ToString());
                sl.Add("FBad", dr["FBad"].ToString());
                sl.Add("FCertiNo", dr["FCertiNo"].ToString());
                sl.Add("FGetTime", dr["FGetTime"].ToString());
                sl.Add("FEndTime", dr["FEndTime"].ToString());
                sl.Add("FPubDeptName", dr["FPubDeptName"].ToString());
                sl.Add("FIdCard", dr["FIdCard"].ToString());
                sl.Add("FAge", dr["FAge"].ToString());
                sl.Add("FEmail", dr["FEmail"].ToString());
                sl.Add("FIsSafety", dr["FIsSafety"].ToString());
                sl.Add("FSafetyNo", dr["FSafetyNo"].ToString());
                sl.Add("FIDCardTypeId", dr["FIDCardTypeId"].ToString());
                sl.Add("fpostcode", dr["fpostcode"].ToString());


                sl.Add("FTxt1", dr["FTxt1"].ToString());
                sl.Add("FTxt2", dr["FTxt2"].ToString());
                sl.Add("FTxt3", dr["FTxt3"].ToString());
                sl.Add("FTxt4", dr["FTxt4"].ToString());
                sl.Add("FTxt5", dr["FTxt5"].ToString());
                sl.Add("FTxt6", dr["FTxt6"].ToString());
                sl.Add("FTxt7", dr["FTxt7"].ToString());
                sl.Add("FTxt8", dr["FTxt8"].ToString());
                sl.Add("FTxt9", dr["FTxt9"].ToString());
                sl.Add("FTxt10", dr["FTxt10"].ToString());
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);

                arrEn.Add(EntityTypeEnum.EqEbLeader);
                arrSl.Add(sl);
                arrKey.Add("FID");
                arrSo.Add(so);


                //人员简历
                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_Resume");
                sb.Append(" where fisdeleted=0 and  FEmpId='" + dr["FID"].ToString() + "'");
                sb.Append(" and FAppId='" + fAppId + "'");
                rq.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" fisdeleted=0 and  FEmpId ='" + dr["FID"].ToString() + "' ");
                DataTable dtTemp = rc.GetTable(EntityTypeEnum.EeResume, "", sb.ToString());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    //rq.PExcute("Delete From CF_AppEmp_Resume Where FEmpId='" + sl["FEmpId"].ToString() + "' and FAppId='" + this.Session["FAppId"].ToString() + "'");

                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FEmpId", dtTemp.Rows[k]["FEmpId"].ToString());
                        sl.Add("FBaseInfoName", dtTemp.Rows[k]["FBaseInfoName"].ToString());
                        sl.Add("FBaseInfoId", dtTemp.Rows[k]["FBaseInfoId"].ToString());
                        sl.Add("FWorkContent", dtTemp.Rows[k]["FWorkContent"].ToString());
                        sl.Add("FApprovePerson", dtTemp.Rows[k]["FApprovePerson"].ToString());
                        sl.Add("FApproveTel", dtTemp.Rows[k]["FApproveTel"].ToString());
                        sl.Add("FRemark", dtTemp.Rows[k]["FRemark"].ToString());
                        sl.Add("FBeginTime", dtTemp.Rows[k]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dtTemp.Rows[k]["FEndTime"].ToString());
                        sl.Add("FIsDeleted", 0);
                        sl.Add("FDuty", dtTemp.Rows[k]["FDuty"].ToString());

                        arrEn.Add(EntityTypeEnum.EqEeResume);
                        arrSl.Add(sl);
                        arrKey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }


                //人员照片
                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppPub_Text");
                sb.Append(" where fisdeleted=0 and  FLinkId ='" + dr["FID"].ToString() + "'");
                sb.Append(" and FAppId='" + fAppId + "' ");
                rq.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" fisdeleted=0 and   FLinkId ='" + dr["FID"].ToString() + "'");
                dtTemp = rc.GetTable(EntityTypeEnum.EPText, "", sb.ToString());

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        sl = new SortedList();


                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FContent", dtTemp.Rows[k]["FContent"].ToString());
                        sl.Add("FLinkId", dtTemp.Rows[k]["FLinkId"].ToString());
                        sl.Add("FType", dtTemp.Rows[k]["FType"].ToString());
                        sl.Add("FIsDeleted", 0);


                        arrEn.Add(EntityTypeEnum.EqPText);
                        arrSl.Add(sl);
                        arrKey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }
            }

            if (arrEn.Count == 0)
            {
                return;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
            SortedList[] sls = new SortedList[arrEn.Count];
            string[] keys = new string[arrEn.Count];
            SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];


            for (int i = 0; i < arrEn.Count; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = new SortedList();
                sls[i] = (SortedList)arrSl[i];
                keys[i] = (string)arrKey[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
            }

            rq.SaveEBaseM(ens, sls, keys, sos);
        }



        //enTo要倒入的实体
        public void SInPutEmpInfo(string fAppId, string fBaseInfoId, DataTable dt, EntityTypeEnum enTo)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            RCenter rc = new RCenter();

            StringBuilder sb = new StringBuilder();

            ArrayList arraySl = new ArrayList();
            ArrayList arrayEn = new ArrayList();
            ArrayList arrayFKey = new ArrayList();
            ArrayList arraySo = new ArrayList();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                SortedList sl = new SortedList();
                EntityTypeEnum en = enTo;
                string feky = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;


                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", fAppId);
                sl.Add("FEmpId", dt.Rows[j]["FId"].ToString());
                sl.Add("FBaseInfoID", fBaseInfoId);
                sl.Add("FName", dt.Rows[j]["FName"].ToString());
                sl.Add("FSex", dt.Rows[j]["FSex"].ToString());
                sl.Add("FBirthDay", dt.Rows[j]["FBirthDay"].ToString());
                sl.Add("FFunction", dt.Rows[j]["FFunction"].ToString());
                sl.Add("FTechId", dt.Rows[j]["FTechId"].ToString());
                sl.Add("FGraduateTime", dt.Rows[j]["FGraduateTime"].ToString());
                sl.Add("FGraduateSchool", dt.Rows[j]["FGraduateSchool"].ToString());
                sl.Add("FSpecialtyId", dt.Rows[j]["FSpecialtyId"].ToString());
                sl.Add("FIdCard", dt.Rows[j]["FIdCard"].ToString());
                sl.Add("FCall", dt.Rows[j]["FCall"].ToString());
                sl.Add("FTEL", dt.Rows[j]["FTEL"].ToString());
                sl.Add("FWorkState", dt.Rows[j]["FWorkState"].ToString());
                sl.Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl.Add("FIDCardTypeId", dt.Rows[j]["FIDCardTypeId"].ToString());
                sl.Add("FDegreeId", dt.Rows[j]["FDegreeId"].ToString());
                sl.Add("FWorkYear", dt.Rows[j]["FWorkYear"].ToString());
                sl.Add("FAge", dt.Rows[j]["FAge"].ToString());
                sl.Add("FPersonTypeId", dt.Rows[j]["FPersonTypeId"].ToString());
                sl.Add("FTypeId", dt.Rows[j]["FTypeId"].ToString());
                sl.Add("FRegistSpecialId", dt.Rows[j]["FRegistSpecialId"].ToString());
                sl.Add("FRegistSpecialId2", dt.Rows[j]["FRegistSpecialId2"].ToString());
                sl.Add("FCertiNo", dt.Rows[j]["FCertiNo"].ToString());
                sl.Add("FPrintNo", dt.Rows[j]["FPrintNo"].ToString());
                sl.Add("FLevelId", dt.Rows[j]["FLevelId"].ToString());
                sl.Add("FGetTime", dt.Rows[j]["FGetTime"].ToString());
                sl.Add("FGetTypeId", dt.Rows[j]["FGetTypeId"].ToString());
                sl.Add("FEndTime", dt.Rows[j]["FEndTime"].ToString());
                sl.Add("FPubDeptName", dt.Rows[j]["FPubDeptName"].ToString());
                sl.Add("FEmail", dt.Rows[j]["FEmail"].ToString());
                sl.Add("FBBeginTime", dt.Rows[j]["FBBeginTime"].ToString());
                sl.Add("FBEndTime", dt.Rows[j]["FBEndTime"].ToString());
                sl.Add("FNation", dt.Rows[j]["FNation"].ToString());
                sl.Add("FAddress", dt.Rows[j]["FAddress"].ToString());
                sl.Add("FPostCode", dt.Rows[j]["FPostCode"].ToString());
                sl.Add("FResume", dt.Rows[j]["FResume"].ToString());
                sl.Add("FRegisfNation", dt.Rows[j]["FRegisfNation"].ToString());
                sl.Add("FUnit", dt.Rows[j]["FUnit"].ToString());
                sl.Add("FWorkKind", dt.Rows[j]["FWorkKind"].ToString());
                sl.Add("FWorkSpecialId", dt.Rows[j]["FWorkSpecialId"].ToString());
                sl.Add("FIsSafety", dt.Rows[j]["FIsSafety"].ToString());
                sl.Add("FSafetyNo", dt.Rows[j]["FSafetyNo"].ToString());

                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);
                sl.Add("FRState", dt.Rows[j]["FRState"].ToString());
                sl.Add("FGood", dt.Rows[j]["FGood"].ToString());

                arrayEn.Add(en);
                arrayFKey.Add(feky);
                arraySl.Add(sl);
                arraySo.Add(so);


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppPub_Text where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Pub_Text  where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fisdeleted=0");
                DataTable dt1 = rc.GetTable(sb.ToString());
                if (dt != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int p = 0; p < iCount; p++)
                    {
                        en = EntityTypeEnum.EqPText;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FLinkId", dt1.Rows[p]["FLinkId"].ToString());
                        sl.Add("FType", dt1.Rows[p]["FType"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FContent", dt1.Rows[p]["FContent"].ToString());
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkAchievement");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkAchievement;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProvePeople", dt1.Rows[y]["FProvePeople"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FProjectName", dt1.Rows[y]["FProjectName"].ToString());
                        sl.Add("FProjectLevel", dt1.Rows[y]["FProjectLevel"].ToString());
                        sl.Add("FProjectUnit", dt1.Rows[y]["FProjectUnit"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FResult", dt1.Rows[y]["FResult"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkExperience");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkExperience;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProjectId", dt1.Rows[y]["FProjectId"].ToString());
                        sl.Add("FProjectTypeId", dt1.Rows[y]["FProjectTypeId"].ToString());
                        sl.Add("FCost", dt1.Rows[y]["FCost"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FBaseinfoName", dt1.Rows[y]["FBaseinfoName"].ToString());
                        sl.Add("FAppPerson", dt1.Rows[y]["FAppPerson"].ToString());
                        sl.Add("FCompany", dt1.Rows[y]["FCompany"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FAward", dt1.Rows[y]["FAward"].ToString());
                        sl.Add("FBad", dt1.Rows[y]["FBad"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBusiness", dt1.Rows[y]["FBusiness"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FDesc", dt1.Rows[y]["FDesc"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_Resume where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_Resume");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeResume;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FBaseInfoName", dt1.Rows[y]["FBaseInfoName"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FApprovePerson", dt1.Rows[y]["FApprovePerson"].ToString());
                        sl.Add("FApproveTel", dt1.Rows[y]["FApproveTel"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FDesc", dt1.Rows[y]["FDesc"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }

            }
            int iCount1 = arraySl.Count;
            if (iCount1 != 0)
            {
                EntityTypeEnum[] ens = new EntityTypeEnum[iCount1];
                string[] fkeys = new string[iCount1];
                SortedList[] sls = new SortedList[iCount1];
                SaveOptionEnum[] sos = new SaveOptionEnum[iCount1];

                for (int k = 0; k < iCount1; k++)
                {
                    ens[k] = (EntityTypeEnum)(arrayEn[k]);
                    fkeys[k] = (String)(arrayFKey[k]);
                    sls[k] = (SortedList)(arraySl[k]);
                    sos[k] = (SaveOptionEnum)(arraySo[k]);

                }
                this.SaveEBaseM(ens, sls, fkeys, sos);
            }

        }

        //导入企业工程信息数据
        public void SInPutProjectInfo(string fAppId, string fBaseInfoId, DataTable pData)
        {
            if (pData == null || pData.Rows.Count == 0)
            {
                return;
            }
            RCenter rc = new RCenter();
            int ipCount = pData.Rows.Count;


            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrFkey = new ArrayList();
            ArrayList arrSo = new ArrayList();

            for (int i = 0; i < ipCount; i++)
            {
                EntityTypeEnum en = EntityTypeEnum.EqEbProject;
                SortedList sl = new SortedList();
                string fKey = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;



                string fProId = Guid.NewGuid().ToString();

                sl.Add("FID", fProId);
                sl.Add("FAppId", fAppId);
                sl.Add("FProjectId", pData.Rows[i]["FId"].ToString());
                sl.Add("FName", pData.Rows[i]["FName"].ToString());
                sl.Add("FProjectNo", pData.Rows[i]["FProjectNo"].ToString());
                sl.Add("FTechId", pData.Rows[i]["FTechId"].ToString());
                sl.Add("FArea", pData.Rows[i]["FArea"].ToString());
                sl.Add("FBargainNo", pData.Rows[i]["FBargainNo"].ToString());
                sl.Add("FBargainPrice", pData.Rows[i]["FBargainPrice"].ToString());
                sl.Add("FFactPrice", pData.Rows[i]["FFactPrice"].ToString());
                sl.Add("FBeginDate", pData.Rows[i]["FBeginDate"].ToString());
                sl.Add("FEndDate", pData.Rows[i]["FEndDate"].ToString());
                sl.Add("FPlanDate", pData.Rows[i]["FPlanDate"].ToString());
                sl.Add("FFactDate", pData.Rows[i]["FFactDate"].ToString());
                sl.Add("FReason", pData.Rows[i]["FReason"].ToString());
                sl.Add("FQualiTypeId", pData.Rows[i]["FQualiTypeId"].ToString());
                sl.Add("FProjectTypeId", pData.Rows[i]["FProjectTypeId"].ToString());
                sl.Add("FUnitId", pData.Rows[i]["FUnitId"].ToString());
                sl.Add("FCount", pData.Rows[i]["FCount"].ToString());
                sl.Add("FQResult", pData.Rows[i]["FQResult"].ToString());
                sl.Add("FAllMoney", pData.Rows[i]["FAllMoney"].ToString());
                sl.Add("FIdea", pData.Rows[i]["FIdea"].ToString());
                sl.Add("FType", pData.Rows[i]["FType"].ToString());
                sl.Add("FPerson", pData.Rows[i]["FPerson"].ToString());
                sl.Add("FReportPerson", pData.Rows[i]["FReportPerson"].ToString());
                sl.Add("FRecivePerson", pData.Rows[i]["FRecivePerson"].ToString());
                sl.Add("FSize", pData.Rows[i]["FSize"].ToString());


                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);






                arrEn.Add(en);
                arrFkey.Add(fKey);
                arrSl.Add(sl);
                arrSo.Add(so);


                StringBuilder sb = new StringBuilder();
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Ent_ProjectOther where fid='" + pData.Rows[i]["FId"].ToString() + "'");
                DataTable dt = rc.GetTable(sb.ToString());

                if (dt != null && dt.Rows.Count > 0)
                {
                    en = EntityTypeEnum.EqEbProjectOther;
                    sl = new SortedList();

                    sl.Add("FID", fProId);
                    sl.Add("FAppId", fAppId);
                    sl.Add("FProjectAddress", dt.Rows[0]["FProjectAddress"].ToString());
                    sl.Add("FOCertiNo", dt.Rows[0]["FOCertiNo"].ToString());
                    sl.Add("FJobTypeId", dt.Rows[0]["FJobTypeId"].ToString());
                    sl.Add("FJobDesc", dt.Rows[0]["FJobDesc"].ToString());
                    sl.Add("FIsPart", dt.Rows[0]["FIsPart"].ToString());
                    sl.Add("FPartContent", dt.Rows[0]["FPartContent"].ToString());
                    sl.Add("FFormTypeId", dt.Rows[0]["FFormTypeId"].ToString());
                    sl.Add("FManagerName", dt.Rows[0]["FManagerName"].ToString());
                    sl.Add("FManagerId", dt.Rows[0]["FManagerId"].ToString());
                    sl.Add("FIntroduce", dt.Rows[0]["FIntroduce"].ToString());
                    sl.Add("FCityId", dt.Rows[0]["FCityId"].ToString());
                    sl.Add("FSResult", dt.Rows[0]["FSResult"].ToString());
                    sl.Add("FAward", dt.Rows[0]["FAward"].ToString());
                    sl.Add("FWorkAddress", dt.Rows[0]["FWorkAddress"].ToString());
                    sl.Add("FWorkTel", dt.Rows[0]["FWorkTel"].ToString());
                    sl.Add("FConUnit", dt.Rows[0]["FConUnit"].ToString());
                    sl.Add("FConUnitPerson", dt.Rows[0]["FConUnitPerson"].ToString());
                    sl.Add("FConUnitTel", dt.Rows[0]["FConUnitTel"].ToString());
                    sl.Add("FTestUnit", dt.Rows[0]["FTestUnit"].ToString());
                    sl.Add("FTestUnitPerson", dt.Rows[0]["FTestUnitPerson"].ToString());
                    sl.Add("FTestUnitTel", dt.Rows[0]["FTestUnitTel"].ToString());
                    sl.Add("FRemark", dt.Rows[0]["FRemark"].ToString());

                    for (int k = 1; k <= 30; k++)
                    {
                        sl.Add("FFl" + k, dt.Rows[0]["FFl" + k].ToString());
                    }
                    for (int j = 1; j <= 8; j++)
                    {
                        sl.Add("FTx" + j, dt.Rows[0]["FTx" + j].ToString());
                    }

                    for (int l = 1; l <= 4; l++)
                    {
                        sl.Add("FInt" + l, dt.Rows[0]["FInt" + l].ToString());
                    }

                    arrEn.Add(en);
                    arrFkey.Add(fKey);
                    arrSl.Add(sl);
                    arrSo.Add(so);
                }


                SInitCheckParameter(pData.Rows[i]["FId"].ToString(), fAppId, ref arrEn, ref arrSl, ref arrFkey, ref arrSo);

            }

            ipCount = arrEn.Count;
            if (ipCount <= 0)
            {
                return;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[ipCount];
            string[] keys = new string[ipCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[ipCount];
            SortedList[] sls = new SortedList[ipCount];
            for (int i = 0; i < ipCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = (SortedList)arrSl[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
                keys[i] = (string)arrFkey[i];
            }
            this.SaveEBaseM(ens, sls, keys, sos);

        }

        //导入企业工程信息数据
        public void SInPutProjectInfo(string fAppId, string fBaseInfoId, DataTable pData, DataTable pOtherData)
        {
            if (pData == null || pData.Rows.Count == 0 || pOtherData == null || pOtherData.Rows.Count == 0)
            {
                return;
            }
            int ipCount = pData.Rows.Count;
            int ipOtherCount = pOtherData.Rows.Count;

            //EntityTypeEnum[] en = new EntityTypeEnum[2 * ipCount];
            //SortedList[] sl = new SortedList[2 * ipCount];
            //string[] fkey = new string[2 * ipCount];
            //SaveOptionEnum[] so = new SaveOptionEnum[2 * ipCount];

            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrFkey = new ArrayList();
            ArrayList arrSo = new ArrayList();

            for (int i = 0; i < ipCount; i++)
            {
                EntityTypeEnum en = EntityTypeEnum.EqEbProject;
                SortedList sl = new SortedList();
                string fKey = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;



                string fProId = Guid.NewGuid().ToString();

                sl.Add("FID", fProId);
                sl.Add("FAppId", fAppId);
                sl.Add("FProjectId", pData.Rows[i]["FId"].ToString());
                sl.Add("FName", pData.Rows[i]["FName"].ToString());
                sl.Add("FProjectNo", pData.Rows[i]["FProjectNo"].ToString());
                sl.Add("FTechId", pData.Rows[i]["FTechId"].ToString());
                sl.Add("FArea", pData.Rows[i]["FArea"].ToString());
                sl.Add("FBargainNo", pData.Rows[i]["FBargainNo"].ToString());
                sl.Add("FBargainPrice", pData.Rows[i]["FBargainPrice"].ToString());
                sl.Add("FFactPrice", pData.Rows[i]["FFactPrice"].ToString());
                sl.Add("FBeginDate", pData.Rows[i]["FBeginDate"].ToString());
                sl.Add("FEndDate", pData.Rows[i]["FEndDate"].ToString());
                sl.Add("FPlanDate", pData.Rows[i]["FPlanDate"].ToString());
                sl.Add("FFactDate", pData.Rows[i]["FFactDate"].ToString());
                sl.Add("FReason", pData.Rows[i]["FReason"].ToString());
                sl.Add("FQualiTypeId", pData.Rows[i]["FQualiTypeId"].ToString());
                sl.Add("FProjectTypeId", pData.Rows[i]["FProjectTypeId"].ToString());
                sl.Add("FUnitId", pData.Rows[i]["FUnitId"].ToString());
                sl.Add("FCount", pData.Rows[i]["FCount"].ToString());
                sl.Add("FQResult", pData.Rows[i]["FQResult"].ToString());
                sl.Add("FAllMoney", pData.Rows[i]["FAllMoney"].ToString());
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);
                sl.Add("FType", pData.Rows[i]["FType"].ToString());



                arrEn.Add(en);
                arrFkey.Add(fKey);
                arrSl.Add(sl);
                arrSo.Add(so);


                en = EntityTypeEnum.EqEbProjectOther;
                sl = new SortedList();

                sl.Add("FID", fProId);
                sl.Add("FAppId", fAppId);
                sl.Add("FProjectAddress", pOtherData.Rows[i]["FProjectAddress"].ToString());
                sl.Add("FOCertiNo", pOtherData.Rows[i]["FOCertiNo"].ToString());
                sl.Add("FJobTypeId", pOtherData.Rows[i]["FJobTypeId"].ToString());
                sl.Add("FJobDesc", pOtherData.Rows[i]["FJobDesc"].ToString());
                sl.Add("FIsPart", pOtherData.Rows[i]["FIsPart"].ToString());
                sl.Add("FPartContent", pOtherData.Rows[i]["FPartContent"].ToString());
                sl.Add("FFormTypeId", pOtherData.Rows[i]["FFormTypeId"].ToString());
                sl.Add("FManagerName", pOtherData.Rows[i]["FManagerName"].ToString());
                sl.Add("FManagerId", pOtherData.Rows[i]["FManagerId"].ToString());
                sl.Add("FIntroduce", pOtherData.Rows[i]["FIntroduce"].ToString());
                sl.Add("FCityId", pOtherData.Rows[i]["FCityId"].ToString());
                sl.Add("FSResult", pOtherData.Rows[i]["FSResult"].ToString());
                sl.Add("FAward", pOtherData.Rows[i]["FAward"].ToString());
                sl.Add("FWorkAddress", pOtherData.Rows[i]["FWorkAddress"].ToString());
                sl.Add("FWorkTel", pOtherData.Rows[i]["FWorkTel"].ToString());
                sl.Add("FConUnit", pOtherData.Rows[i]["FConUnit"].ToString());
                sl.Add("FConUnitPerson", pOtherData.Rows[i]["FConUnitPerson"].ToString());
                sl.Add("FConUnitTel", pOtherData.Rows[i]["FConUnitTel"].ToString());
                sl.Add("FTestUnit", pOtherData.Rows[i]["FTestUnit"].ToString());
                sl.Add("FTestUnitPerson", pOtherData.Rows[i]["FTestUnitPerson"].ToString());
                sl.Add("FTestUnitTel", pOtherData.Rows[i]["FTestUnitTel"].ToString());
                sl.Add("FRemark", pOtherData.Rows[i]["FRemark"].ToString());

                for (int k = 1; k <= 30; k++)
                {
                    sl.Add("FFl" + k, pOtherData.Rows[i]["FFl" + k].ToString());
                }
                for (int j = 1; j <= 8; j++)
                {
                    sl.Add("FTx" + j, pOtherData.Rows[i]["FTx" + j].ToString());
                }

                for (int l = 1; l <= 4; l++)
                {
                    sl.Add("FInt" + l, pOtherData.Rows[i]["FInt" + l].ToString());
                }

                arrEn.Add(en);
                arrFkey.Add(fKey);
                arrSl.Add(sl);
                arrSo.Add(so);


                SInitCheckParameter(pData.Rows[i]["FId"].ToString(), fAppId, ref arrEn, ref arrSl, ref arrFkey, ref arrSo);

            }

            ipCount = arrEn.Count;
            if (ipCount <= 0)
            {
                return;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[ipCount];
            string[] keys = new string[ipCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[ipCount];
            SortedList[] sls = new SortedList[ipCount];
            for (int i = 0; i < ipCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = (SortedList)arrSl[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
                keys[i] = (string)arrFkey[i];
            }
            this.SaveEBaseM(ens, sls, keys, sos);

        }


        //导入房地产企业工程数据
        public void SInPutHourseProjectInfo(string fAppId, string fBaseInfoId, DataTable pData)
        {
            if (pData == null || pData.Rows.Count == 0)
            {
                return;
            }
            RCenter rc = new RCenter();

            StringBuilder sb = new StringBuilder();
            int ipCount = pData.Rows.Count;





            ArrayList arrSort = new ArrayList();
            ArrayList arrEn = new ArrayList();
            ArrayList arrFKey = new ArrayList();
            ArrayList arrSo = new ArrayList();


            for (int i = 0; i < ipCount; i++)
            {
                string fProjectId = Guid.NewGuid().ToString();

                EntityTypeEnum en = EntityTypeEnum.EqEbProject;
                SortedList sl = new SortedList();
                string fkey = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;

                sl.Add("FID", fProjectId);
                sl.Add("FAppId", fAppId);
                sl.Add("FProjectId", pData.Rows[i]["FId"].ToString());
                sl.Add("FName", pData.Rows[i]["FName"].ToString());
                sl.Add("FProjectNo", pData.Rows[i]["FProjectNo"].ToString());
                sl.Add("FTechId", pData.Rows[i]["FTechId"].ToString());
                sl.Add("FArea", pData.Rows[i]["FArea"].ToString());
                sl.Add("FBargainNo", pData.Rows[i]["FBargainNo"].ToString());
                sl.Add("FBargainPrice", pData.Rows[i]["FBargainPrice"].ToString());
                sl.Add("FFactPrice", pData.Rows[i]["FFactPrice"].ToString());
                sl.Add("FBeginDate", pData.Rows[i]["FBeginDate"].ToString());
                sl.Add("FEndDate", pData.Rows[i]["FEndDate"].ToString());
                sl.Add("FPlanDate", pData.Rows[i]["FPlanDate"].ToString());
                sl.Add("FFactDate", pData.Rows[i]["FFactDate"].ToString());
                sl.Add("FReason", pData.Rows[i]["FReason"].ToString());
                sl.Add("FQualiTypeId", pData.Rows[i]["FQualiTypeId"].ToString());
                sl.Add("FProjectTypeId", pData.Rows[i]["FProjectTypeId"].ToString());
                sl.Add("FUnitId", pData.Rows[i]["FUnitId"].ToString());
                sl.Add("FCount", pData.Rows[i]["FCount"].ToString());
                sl.Add("FQResult", pData.Rows[i]["FQResult"].ToString());
                sl.Add("FAllMoney", pData.Rows[i]["FAllMoney"].ToString());
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);

                arrEn.Add(en);
                arrSort.Add(sl);
                arrFKey.Add(fkey);
                arrSo.Add(so);



                en = EntityTypeEnum.EqEbProjectOther;
                sl = new SortedList();
                fkey = "FID";
                so = SaveOptionEnum.Insert;

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Ent_ProjectOther where fid='" + pData.Rows[i]["FId"].ToString() + "'");
                DataTable dt = rc.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    sl.Add("FID", fProjectId);
                    sl.Add("FAppId", fAppId);
                    sl.Add("FProjectAddress", dt.Rows[0]["FProjectAddress"].ToString());
                    sl.Add("FOCertiNo", dt.Rows[0]["FOCertiNo"].ToString());
                    sl.Add("FJobTypeId", dt.Rows[0]["FJobTypeId"].ToString());
                    sl.Add("FJobDesc", dt.Rows[0]["FJobDesc"].ToString());
                    sl.Add("FIsPart", dt.Rows[0]["FIsPart"].ToString());
                    sl.Add("FPartContent", dt.Rows[0]["FPartContent"].ToString());
                    sl.Add("FFormTypeId", dt.Rows[0]["FFormTypeId"].ToString());
                    sl.Add("FManagerName", dt.Rows[0]["FManagerName"].ToString());
                    sl.Add("FManagerId", dt.Rows[0]["FManagerId"].ToString());
                    sl.Add("FIntroduce", dt.Rows[0]["FIntroduce"].ToString());
                    sl.Add("FCityId", dt.Rows[0]["FCityId"].ToString());
                    sl.Add("FSResult", dt.Rows[0]["FSResult"].ToString());
                    sl.Add("FAward", dt.Rows[0]["FAward"].ToString());
                    sl.Add("FWorkAddress", dt.Rows[0]["FWorkAddress"].ToString());
                    sl.Add("FWorkTel", dt.Rows[0]["FWorkTel"].ToString());
                    sl.Add("FConUnit", dt.Rows[0]["FConUnit"].ToString());
                    sl.Add("FConUnitPerson", dt.Rows[0]["FConUnitPerson"].ToString());
                    sl.Add("FConUnitTel", dt.Rows[0]["FConUnitTel"].ToString());
                    sl.Add("FTestUnit", dt.Rows[0]["FTestUnit"].ToString());
                    sl.Add("FTestUnitPerson", dt.Rows[0]["FTestUnitPerson"].ToString());
                    sl.Add("FTestUnitTel", dt.Rows[0]["FTestUnitTel"].ToString());
                    sl.Add("FRemark", dt.Rows[0]["FRemark"].ToString());
                    for (int k = 1; k <= 30; k++)
                    {
                        sl.Add("FFl" + k, dt.Rows[0]["FFl" + k].ToString());
                    }
                    for (int j = 1; j <= 8; j++)
                    {
                        sl.Add("FTx" + j, dt.Rows[0]["FTx" + j].ToString());
                    }

                    for (int l = 1; l <= 4; l++)
                    {
                        sl.Add("FInt" + l, dt.Rows[0]["FInt" + l].ToString());
                    }
                    arrEn.Add(en);
                    arrSort.Add(sl);
                    arrFKey.Add(fkey);
                    arrSo.Add(so);
                }





                sb.Remove(0, sb.Length);
                sb.Append(" FProgectId='" + pData.Rows[i]["FId"].ToString() + "' ");
                dt = rc.GetTable(EntityTypeEnum.EbProjectManager, "", sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iCount = dt.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEbProjectManager;
                        sl = new SortedList();
                        fkey = "FID";
                        so = SaveOptionEnum.Insert;

                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FProgectId", pData.Rows[i]["FId"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FEmployeeId", dt.Rows[y]["FEmployeeId"].ToString());
                        sl.Add("FName", dt.Rows[y]["FName"].ToString());
                        sl.Add("FDuty", dt.Rows[y]["FDuty"].ToString());
                        sl.Add("FBeginDate", dt.Rows[y]["FBeginDate"].ToString());
                        sl.Add("FEndDate", dt.Rows[y]["FEndDate"].ToString());
                        sl.Add("FIsDeleted", 0);

                        arrEn.Add(en);
                        arrSort.Add(sl);
                        arrFKey.Add(fkey);
                        arrSo.Add(so);
                    }
                }






                sb.Remove(0, sb.Length);
                sb.Append(" FProgectId='" + pData.Rows[i]["FId"].ToString() + "' ");
                dt = rc.GetTable(EntityTypeEnum.EbProjectCerti, "", sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iCount = dt.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEbProjectCerti;
                        sl = new SortedList();
                        fkey = "FID";
                        so = SaveOptionEnum.Insert;

                        string fId = Guid.NewGuid().ToString();
                        sl.Add("FID", fId);
                        sl.Add("FProgectId", pData.Rows[i]["FId"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCertiType", dt.Rows[y]["FCertiType"].ToString());
                        sl.Add("FCertiNumber", dt.Rows[y]["FCertiNumber"].ToString());
                        sl.Add("FCertiAwardDepart", dt.Rows[y]["FCertiAwardDepart"].ToString());
                        sl.Add("FProjectArea", dt.Rows[y]["FProjectArea"].ToString());
                        sl.Add("FAllowProjectBuild", dt.Rows[y]["FAllowProjectBuild"].ToString());
                        sl.Add("FCubageEffic", dt.Rows[y]["FCubageEffic"].ToString());
                        sl.Add("FBuildDensity", dt.Rows[y]["FBuildDensity"].ToString());
                        sl.Add("FGrassEffic", dt.Rows[y]["FGrassEffic"].ToString());
                        sl.Add("FPhotoId", dt.Rows[y]["FPhotoId"].ToString());
                        sl.Add("FRemark", dt.Rows[y]["FRemark"].ToString());

                        sl.Add("FCertiDate", dt.Rows[y]["FCertiDate"].ToString());


                        sl.Add("FGroundUseBTime", dt.Rows[y]["FGroundUseBTime"].ToString());
                        sl.Add("FGroundUseETime", dt.Rows[y]["FGroundUseETime"].ToString());
                        sl.Add("FGroundDegree", dt.Rows[y]["FGroundDegree"].ToString());
                        sl.Add("FGroundType", dt.Rows[y]["FGroundType"].ToString());
                        sl.Add("FGroundSum", dt.Rows[y]["FGroundSum"].ToString());
                        sl.Add("FGroundBussArea", dt.Rows[y]["FGroundBussArea"].ToString());
                        sl.Add("FGroundFamilyArea", dt.Rows[y]["FGroundFamilyArea"].ToString());
                        sl.Add("FGroundIndustryAre", dt.Rows[y]["FGroundIndustryAre"].ToString());
                        sl.Add("FGroundOtherAre", dt.Rows[y]["FGroundOtherAre"].ToString());

                        sl.Add("FXSProXKZ", dt.Rows[y]["FXSProXKZ"].ToString());
                        sl.Add("FXSGroundXKZ", dt.Rows[y]["FXSGroundXKZ"].ToString());
                        sl.Add("FXSGroundUXKZ", dt.Rows[y]["FXSGroundUXKZ"].ToString());
                        sl.Add("FXSOtherXKZ", dt.Rows[y]["FXSOtherXKZ"].ToString());
                        sl.Add("fiscurr", dt.Rows[y]["fiscurr"].ToString());
                        sl.Add("Fyear", dt.Rows[y]["Fyear"].ToString());
                        sl.Add("FIsDeleted", 0);

                        arrEn.Add(en);
                        arrSort.Add(sl);
                        arrFKey.Add(fkey);
                        arrSo.Add(so);


                        sb.Remove(0, sb.Length);
                        sb.Append(" FLinkId='" + dt.Rows[y]["FId"].ToString() + "'");
                        DataTable dtt = rc.GetTable(EntityTypeEnum.EPText, "", sb.ToString());
                        if (dtt != null && dtt.Rows.Count > 0)
                        {
                            {
                                int iCount1 = dtt.Rows.Count;
                                for (int z = 0; z < iCount1; z++)
                                {
                                    en = EntityTypeEnum.EqPText;
                                    sl = new SortedList();
                                    fkey = "FID";
                                    so = SaveOptionEnum.Insert;
                                    sl.Add("FID", Guid.NewGuid().ToString());
                                    sl.Add("FLinkId", fId);
                                    sl.Add("FAppId", fAppId);
                                    sl.Add("FContent", dtt.Rows[z]["FContent"].ToString());
                                    sl.Add("FType", dtt.Rows[z]["FType"].ToString());
                                    sl.Add("FCreateTime", DateTime.Now);
                                    sl.Add("FIsDeleted", 0);

                                    arrEn.Add(en);
                                    arrSort.Add(sl);
                                    arrFKey.Add(fkey);
                                    arrSo.Add(so);
                                }
                            }
                        }
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" FProgectId='" + pData.Rows[i]["FId"].ToString() + "' ");
                dt = rc.GetTable(EntityTypeEnum.EbProjBackUp, "", sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iCount = dt.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEbProjBackUp;
                        sl = new SortedList();
                        fkey = "FID";
                        so = SaveOptionEnum.Insert;
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FProgectId", pData.Rows[i]["FId"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FBackupCode", dt.Rows[y]["FBackupCode"].ToString());
                        sl.Add("FCertiAwardDepart", dt.Rows[y]["FCertiAwardDepart"].ToString());
                        sl.Add("FBackUpDate", dt.Rows[y]["FBackUpDate"].ToString());
                        sl.Add("FProjectArea", dt.Rows[y]["FProjectArea"].ToString());
                        sl.Add("fiscurr", dt.Rows[y]["fiscurr"].ToString());
                        sl.Add("FIsDeleted", y);

                        arrEn.Add(en);
                        arrSort.Add(sl);
                        arrFKey.Add(fkey);
                        arrSo.Add(so);
                    }
                }



            }


            int arrayCount = arrEn.Count;
            if (arrayCount == 0)
            {
                return;
            }
            else
            {
                EntityTypeEnum[] ens = new EntityTypeEnum[arrayCount];
                SortedList[] sls = new SortedList[arrayCount];
                string[] fekys = new string[arrayCount];
                SaveOptionEnum[] sos = new SaveOptionEnum[arrayCount];

                for (int t = 0; t < arrayCount; t++)
                {
                    ens[t] = (EntityTypeEnum)(arrEn[t]);
                    sls[t] = (SortedList)(arrSort[t]);
                    fekys[t] = (String)(arrFKey[t]);
                    sos[t] = (SaveOptionEnum)(arrSo[t]);
                }
                this.SaveEBaseM(ens, sls, fekys, sos);
            }
        }

        //导入企业设备数据
        public void SInPutDeviceInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();
            int iCount = dt.Rows.Count;


            ArrayList arrSl = new ArrayList();
            ArrayList arrEn = new ArrayList();
            ArrayList arrFkey = new ArrayList();
            ArrayList arrSo = new ArrayList();




            SortedList sl = new SortedList();
            EntityTypeEnum en = EntityTypeEnum.EqEbDevice;
            string fKey = "FID";
            SaveOptionEnum so = SaveOptionEnum.Insert;
            for (int j = 0; j < iCount; j++)
            {
                sl = new SortedList();
                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", fAppId);
                sl.Add("FDeviceId", dt.Rows[j]["FId"].ToString());
                sl.Add("FName", dt.Rows[j]["FName"].ToString());
                sl.Add("FModel", dt.Rows[j]["FModel"].ToString());
                sl.Add("FTypeId", dt.Rows[j]["FTypeId"].ToString());
                sl.Add("FTech", dt.Rows[j]["FTech"].ToString());
                sl.Add("FPower", dt.Rows[j]["FPower"].ToString());
                sl.Add("FCount", dt.Rows[j]["FCount"].ToString());
                sl.Add("FUnit", dt.Rows[j]["FUnit"].ToString());
                sl.Add("FOldValue", dt.Rows[j]["FOldValue"].ToString());
                sl.Add("FNowValue", dt.Rows[j]["FNowValue"].ToString());
                sl.Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl.Add("FIsSelf", dt.Rows[j]["FIsSelf"].ToString());
                sl.Add("FState", 2);
                sl.Add("FRState", dt.Rows[j]["FRState"].ToString());
                sl.Add("FTxt1", dt.Rows[j]["FTxt1"].ToString());
                sl.Add("FTxt2", dt.Rows[j]["FTxt2"].ToString());
                sl.Add("FTxt3", dt.Rows[j]["FTxt3"].ToString());
                sl.Add("FTxt4", dt.Rows[j]["FTxt4"].ToString());
                sl.Add("FTxt5", dt.Rows[j]["FTxt5"].ToString());
                sl.Add("FDate1", dt.Rows[j]["FDate1"].ToString());
                sl.Add("FOrder", dt.Rows[j]["FOrder"].ToString());
                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);


                arrEn.Add(en);
                arrFkey.Add(fKey);
                arrSl.Add(sl);
                arrSo.Add(so);

                SInitCheckParameter(dt.Rows[j]["FId"].ToString(), fAppId, ref arrEn, ref arrSl, ref arrFkey, ref arrSo);

            }

            iCount = arrEn.Count;
            if (iCount <= 0)
            {
                return;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            string[] keys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            SortedList[] sls = new SortedList[iCount];
            for (int i = 0; i < iCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = (SortedList)arrSl[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
                keys[i] = (string)arrFkey[i];
            }
            this.SaveEBaseM(ens, sls, keys, sos);
        }

        //导入企业良好行为
        public void SInPutEntGoodInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbGoodAction;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;


                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FGoodId", dt.Rows[j]["FId"].ToString());
                sl[j].Add("FProjectId", dt.Rows[j]["FProjectId"].ToString());
                sl[j].Add("FProjectName", dt.Rows[j]["FProjectName"].ToString());
                sl[j].Add("FAddress", dt.Rows[j]["FAddress"].ToString());
                sl[j].Add("FFileNo", dt.Rows[j]["FFileNo"].ToString());
                sl[j].Add("FGetDate", dt.Rows[j]["FGetDate"].ToString());
                sl[j].Add("FTypeId", dt.Rows[j]["FTypeId"].ToString());
                sl[j].Add("FDesc", dt.Rows[j]["FDesc"].ToString());
                sl[j].Add("FScore", dt.Rows[j]["FScore"].ToString());
                sl[j].Add("FWDeptId", dt.Rows[j]["FWDeptId"].ToString());
                sl[j].Add("FPDeptId", dt.Rows[j]["FPDeptId"].ToString());
                sl[j].Add("FBeginTime", dt.Rows[j]["FBeginTime"].ToString());
                sl[j].Add("FEndTime", dt.Rows[j]["FEndTime"].ToString());
                sl[j].Add("FDeptIdName", dt.Rows[j]["FDeptIdName"].ToString());
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FState", 2);

            }
            this.SaveEBaseM(en, sl, fkey, so);
        }

        //导入企业不良行为
        public void SInPutEntBadInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbBadAction;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBadId", dt.Rows[j]["FId"].ToString());
                sl[j].Add("FProjectId", dt.Rows[j]["FProjectId"].ToString());
                sl[j].Add("FProjectName", dt.Rows[j]["FProjectName"].ToString());
                sl[j].Add("FRegionId", dt.Rows[j]["FRegionId"].ToString());
                sl[j].Add("FAddress", dt.Rows[j]["FAddress"].ToString());
                sl[j].Add("FScore", dt.Rows[j]["FScore"].ToString());
                sl[j].Add("FFact", dt.Rows[j]["FFact"].ToString());
                sl[j].Add("FRule", dt.Rows[j]["FRule"].ToString());
                sl[j].Add("FWay", dt.Rows[j]["FWay"].ToString());
                sl[j].Add("FDeptId", dt.Rows[j]["FDeptId"].ToString());
                sl[j].Add("FHTime", dt.Rows[j]["FHTime"].ToString());
                sl[j].Add("FDTime", dt.Rows[j]["FDTime"].ToString());
                sl[j].Add("FAppDeptId", dt.Rows[j]["FAppDeptId"].ToString());
                sl[j].Add("FBeginTime", dt.Rows[j]["FBeginTime"].ToString());
                sl[j].Add("FEndTime", dt.Rows[j]["FEndTime"].ToString());
                sl[j].Add("FBuidUnit", dt.Rows[j]["FBuidUnit"].ToString());
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);
        }

        //导入企业经营情况
        public void SInPutHourseEntWorkInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbZCFZ;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FZCFZId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FType", dt.Rows[j]["FType"].ToString());
                sl[j].Add("FYear", dt.Rows[j]["FYear"].ToString());
                sl[j].Add("FDepartPrincipal", dt.Rows[j]["FDepartPrincipal"].ToString());
                sl[j].Add("FReportPrincipal", dt.Rows[j]["FReportPrincipal"].ToString());
                sl[j].Add("FReportTime", dt.Rows[j]["FReportTime"].ToString());
                sl[j].Add("Fiscurr", dt.Rows[j]["fiscurr"].ToString());
                for (int i = 1; i <= 117; i++)
                {
                    sl[j].Add("Ffl" + i, dt.Rows[j]["Ffl" + i].ToString());
                }
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);
        }

        //导入股东出资信息
        public void SInPutInvestPerson(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbInvestPerson;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FInvestPersonId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FName", dt.Rows[j]["FName"].ToString());
                sl[j].Add("FType", dt.Rows[j]["FType"].ToString());
                sl[j].Add("FMoney", dt.Rows[j]["FMoney"].ToString());
                sl[j].Add("FMoneySale", dt.Rows[j]["FMoneySale"].ToString());
                sl[j].Add("FMoneyType", dt.Rows[j]["FMoneyType"].ToString());
                sl[j].Add("FIdCard", dt.Rows[j]["FIdCard"].ToString());
                sl[j].Add("FManagerName", dt.Rows[j]["FManagerName"].ToString());
                sl[j].Add("FValidBegin", dt.Rows[j]["FValidBegin"].ToString());
                sl[j].Add("FValidEnd", dt.Rows[j]["FValidEnd"].ToString());
                sl[j].Add("FBirthDay", dt.Rows[j]["FBirthDay"].ToString());
                sl[j].Add("FCertiNo", dt.Rows[j]["FCertiNo"].ToString());
                sl[j].Add("FCertiNoOther", dt.Rows[j]["FCertiNoOther"].ToString());
                sl[j].Add("Fiscurr", dt.Rows[j]["fiscurr"].ToString());
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);

        }

        //导入帐号信息
        public void SInPutBankAccount(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbBankAccount;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FBankAccountId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FBank", dt.Rows[j]["FBank"].ToString());
                sl[j].Add("FAcountNo", dt.Rows[j]["FAcountNo"].ToString());
                sl[j].Add("FIsMain", dt.Rows[j]["FIsMain"].ToString());

                sl[j].Add("FValidBegin", dt.Rows[j]["FValidBegin"].ToString());
                sl[j].Add("FValidEnd", dt.Rows[j]["FValidEnd"].ToString());
                sl[j].Add("Fiscurr", dt.Rows[j]["fiscurr"].ToString());
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);

        }

        //导入银行信用等级
        public void SInPutCreditLevel(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbCreditLevel;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FCreditLevelId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FCreditLevel", dt.Rows[j]["FCreditLevel"].ToString());
                sl[j].Add("FCreditLevelNo", dt.Rows[j]["FCreditLevelNo"].ToString());
                sl[j].Add("FCreditOrgan", dt.Rows[j]["FCreditOrgan"].ToString());
                sl[j].Add("FCreditBeginTime", dt.Rows[j]["FCreditBeginTime"].ToString());
                sl[j].Add("FCreditEndTime", dt.Rows[j]["FCreditEndTime"].ToString());
                sl[j].Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl[j].Add("FValidBegin", dt.Rows[j]["FValidBegin"].ToString());
                sl[j].Add("FValidEnd", dt.Rows[j]["FValidEnd"].ToString());
                sl[j].Add("Fiscurr", dt.Rows[j]["fiscurr"].ToString());
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);

        }

        //导入质量认证信息
        public void SInPutQuality(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbQuality;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FQualityId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FQualityName", dt.Rows[j]["FQualityName"].ToString());
                sl[j].Add("FQualityNum", dt.Rows[j]["FQualityNum"].ToString());
                sl[j].Add("FPassDept", dt.Rows[j]["FPassDept"].ToString());
                sl[j].Add("FPassDate", dt.Rows[j]["FPassDate"].ToString());
                sl[j].Add("FEffectStartDate", dt.Rows[j]["FEffectStartDate"].ToString());
                sl[j].Add("FEffectEndDate", dt.Rows[j]["FEffectEndDate"].ToString());
                sl[j].Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl[j].Add("FValidBegin", dt.Rows[j]["FValidBegin"].ToString());
                sl[j].Add("FValidEnd", dt.Rows[j]["FValidEnd"].ToString());
                sl[j].Add("Fiscurr", dt.Rows[j]["fiscurr"].ToString());
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);

        }

        //导入业务成果
        public void SInPutGoodAchieve(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int iCount = dt.Rows.Count;
            SortedList[] sl = new SortedList[iCount];
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];
            for (int j = 0; j < iCount; j++)
            {
                en[j] = EntityTypeEnum.EqEbGoodAchieve;
                sl[j] = new SortedList();
                fkey[j] = "FID";
                so[j] = SaveOptionEnum.Insert;

                sl[j].Add("FID", Guid.NewGuid().ToString());
                sl[j].Add("FAppId", fAppId);
                sl[j].Add("FBaseInfoId", fBaseInfoId);
                sl[j].Add("FGoodAchieveId", dt.Rows[j]["FID"].ToString());
                sl[j].Add("FName", dt.Rows[j]["FName"].ToString());
                sl[j].Add("FLevel", dt.Rows[j]["FLevel"].ToString());
                sl[j].Add("FFINISHTIME", dt.Rows[j]["FFINISHTIME"].ToString());
                sl[j].Add("FMemo", dt.Rows[j]["FMemo"].ToString());
                sl[j].Add("FValidbegin", dt.Rows[j]["FValidbegin"].ToString());
                sl[j].Add("FValidEnd", dt.Rows[j]["FValidEnd"].ToString());
                sl[j].Add("FType", dt.Rows[j]["FType"].ToString());
                sl[j].Add("FOperDeptName", dt.Rows[j]["FOperDeptName"].ToString());
                sl[j].Add("FCreateTime", DateTime.Now);
                sl[j].Add("FIsDeleted", 0);
                sl[j].Add("FState", 2);
            }
            this.SaveEBaseM(en, sl, fkey, so);
        }

        //导入企业证书资质
        public void SInPutQualiInfo(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            RCenter rc = new RCenter();
            int iCount = dt.Rows.Count;

            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList arrKey = new ArrayList();



            for (int j = 0; j < iCount; j++)
            {
                SortedList sl = new SortedList();

                string sCertiId = Guid.NewGuid().ToString();
                DataRow row = dt.Rows[j];
                sl.Add("FID", sCertiId);
                sl.Add("FAppId", fAppId);
                sl.Add("FBaseInfoId", fBaseInfoId);
                sl.Add("FCertiType", row["FCertiType"].ToString());
                sl.Add("FCertiNo", row["FCertiNo"].ToString());
                sl.Add("FAppDeptId", row["FAppDeptId"].ToString());
                sl.Add("FAppDeptName", row["FAppDeptName"].ToString());
                sl.Add("FBeginTime", row["FBeginTime"].ToString());
                sl.Add("FEndTime", row["FEndTime"].ToString());
                sl.Add("FAppTime", row["FAppTime"].ToString());
                sl.Add("FLevelName", row["FLevelName"].ToString());
                sl.Add("FLevelId", row["FLevelId"].ToString());
                sl.Add("FLevel", row["FLevel"].ToString());
                sl.Add("FContent", row["FContent"].ToString());
                sl.Add("FIsValid", row["FIsValid"].ToString());
                sl.Add("FIsTemp", row["FIsTemp"].ToString());
                sl.Add("FPCount", row["FPCount"].ToString());
                sl.Add("FOCount", row["FOCount"].ToString());
                sl.Add("FEntName", row["FEntName"].ToString());
                sl.Add("FEntAddress", row["FEntAddress"].ToString());
                sl.Add("FEntCreateTime", row["FEntCreateTime"].ToString());
                sl.Add("FEntRegistFund", row["FEntRegistFund"].ToString());
                sl.Add("FEntLicence", row["FEntLicence"].ToString());
                sl.Add("FEntTypeId", row["FEntTypeId"].ToString());
                sl.Add("FEntJuridical", row["FEntJuridical"].ToString());
                sl.Add("FEntJuridicalFunction", row["FEntJuridicalFunction"].ToString());
                sl.Add("FEntJuridicalTechId", row["FEntJuridicalTechId"].ToString());
                sl.Add("FEntManager", row["FEntManager"].ToString());
                sl.Add("FEntManagerFunction", row["FEntManagerFunction"].ToString());
                sl.Add("FEntManagerTechId", row["FEntManagerTechId"].ToString());
                sl.Add("FEntTechnic", row["FEntTechnic"].ToString());
                sl.Add("FEntTechnicFunction", row["FEntTechnicFunction"].ToString());
                sl.Add("FEntTechnicTechId", row["FEntTechnicTechId"].ToString());
                sl.Add("FIsDeleted", 0);

                arrEn.Add(EntityTypeEnum.EqEbQualiCerti);
                arrKey.Add("FID");
                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Insert);


                StringBuilder sb = new StringBuilder();
                sb.Append(" select * from CF_Ent_QualiCertiTrade  ");
                sb.Append(" where FCertiId='" + row["FID"].ToString() + "'");
                DataTable dtTemp = rc.GetTable(sb.ToString());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int w = 0; w < dtTemp.Rows.Count; w++)
                    {
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FCertiId", sCertiId);
                        sl.Add("FBaseInfoId", fBaseInfoId);
                        sl.Add("FAppId", fAppId);
                        sl.Add("FListId", dtTemp.Rows[w]["FListId"].ToString());
                        sl.Add("FListName", dtTemp.Rows[w]["FListName"].ToString());
                        sl.Add("FTypeId", dtTemp.Rows[w]["FTypeId"].ToString());
                        sl.Add("FTypeName", dtTemp.Rows[w]["FTypeName"].ToString());
                        sl.Add("FLevelId", dtTemp.Rows[w]["FLevelId"].ToString());
                        sl.Add("FLevelName", dtTemp.Rows[w]["FLevelName"].ToString());
                        sl.Add("FLeadId", dtTemp.Rows[w]["FLeadId"].ToString());
                        sl.Add("FLeadName", dtTemp.Rows[w]["FLeadName"].ToString());
                        sl.Add("FState", dtTemp.Rows[w]["FState"].ToString());
                        sl.Add("FOrder", dtTemp.Rows[w]["FOrder"].ToString());
                        sl.Add("FIsBase", dtTemp.Rows[w]["FIsBase"].ToString());
                        sl.Add("FAppDeptId", dtTemp.Rows[w]["FAppDeptId"].ToString());
                        sl.Add("FAppDeptName", dtTemp.Rows[w]["FAppDeptName"].ToString());
                        sl.Add("FAppTime", dtTemp.Rows[w]["FAppTime"].ToString());
                        sl.Add("FReason", dtTemp.Rows[w]["FReason"].ToString());
                        sl.Add("FCancelDate", dtTemp.Rows[w]["FCancelDate"].ToString());
                        sl.Add("FIsTemp", dtTemp.Rows[w]["FIsTemp"].ToString());
                        sl.Add("FIsDeleted", 0);
                        sl.Add("FCreateTime", DateTime.Now);

                        arrEn.Add(EntityTypeEnum.EqEbQualiCertiTrade);
                        arrKey.Add("FID");
                        arrSl.Add(sl);
                        arrSo.Add(SaveOptionEnum.Insert);

                    }
                }

            }

            iCount = arrEn.Count;

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SortedList[] sls = new SortedList[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];

            for (int r = 0; r < iCount; r++)
            {
                ens[r] = (EntityTypeEnum)arrEn[r];
                sls[r] = (SortedList)arrSl[r];
                fkeys[r] = (string)arrKey[r];
                sos[r] = (SaveOptionEnum)arrSo[r];

            }
            this.SaveEBaseM(ens, sls, fkeys, sos);
        }

        //导入企业监理工程师，建造师
        public void SInputspecialEmp(string fAppId, string fBaseInfoId, DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();
            ArrayList arraySl = new ArrayList();
            ArrayList arrayEn = new ArrayList();
            ArrayList arrayFKey = new ArrayList();
            ArrayList arraySo = new ArrayList();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                SortedList sl = new SortedList();
                EntityTypeEnum en = EntityTypeEnum.EqEeBaseinfo;
                string feky = "FID";
                SaveOptionEnum so = SaveOptionEnum.Insert;


                sl.Add("FID", Guid.NewGuid().ToString());
                sl.Add("FAppId", fAppId);
                sl.Add("FEmpId", dt.Rows[j]["FId"].ToString());
                sl.Add("FBaseInfoID", fBaseInfoId);
                sl.Add("FName", dt.Rows[j]["FName"].ToString());
                sl.Add("FSex", dt.Rows[j]["FSex"].ToString());
                sl.Add("FBirthDay", dt.Rows[j]["FBirthDay"].ToString());
                sl.Add("FFunction", dt.Rows[j]["FFunction"].ToString());
                sl.Add("FTechId", dt.Rows[j]["FTechId"].ToString());
                sl.Add("FGraduateTime", dt.Rows[j]["FGraduateTime"].ToString());
                sl.Add("FGraduateSchool", dt.Rows[j]["FGraduateSchool"].ToString());
                sl.Add("FSpecialtyId", dt.Rows[j]["FSpecialtyId"].ToString());
                sl.Add("FIdCard", dt.Rows[j]["FIdCard"].ToString());
                sl.Add("FCall", dt.Rows[j]["FCall"].ToString());
                sl.Add("FTEL", dt.Rows[j]["FTEL"].ToString());
                sl.Add("FWorkState", dt.Rows[j]["FWorkState"].ToString());
                sl.Add("FRemark", dt.Rows[j]["FRemark"].ToString());
                sl.Add("FIDCardTypeId", dt.Rows[j]["FIDCardTypeId"].ToString());
                sl.Add("FDegreeId", dt.Rows[j]["FDegreeId"].ToString());
                sl.Add("FWorkYear", dt.Rows[j]["FWorkYear"].ToString());
                sl.Add("FAge", dt.Rows[j]["FAge"].ToString());
                sl.Add("FPersonTypeId", dt.Rows[j]["FPersonTypeId"].ToString());
                sl.Add("FTypeId", dt.Rows[j]["FTypeId"].ToString());
                sl.Add("FRegistSpecialId", dt.Rows[j]["FRegistSpecialId"].ToString());
                sl.Add("FRegistSpecialId2", dt.Rows[j]["FRegistSpecialId2"].ToString());
                sl.Add("FCertiNo", dt.Rows[j]["FCertiNo"].ToString());
                sl.Add("FPrintNo", dt.Rows[j]["FPrintNo"].ToString());
                sl.Add("FLevelId", dt.Rows[j]["FLevelId"].ToString());
                sl.Add("FGetTime", dt.Rows[j]["FGetTime"].ToString());
                sl.Add("FGetTypeId", dt.Rows[j]["FGetTypeId"].ToString());
                sl.Add("FEndTime", dt.Rows[j]["FEndTime"].ToString());
                sl.Add("FPubDeptName", dt.Rows[j]["FPubDeptName"].ToString());
                sl.Add("FEmail", dt.Rows[j]["FEmail"].ToString());
                sl.Add("FBBeginTime", dt.Rows[j]["FBBeginTime"].ToString());
                sl.Add("FBEndTime", dt.Rows[j]["FBEndTime"].ToString());
                sl.Add("FNation", dt.Rows[j]["FNation"].ToString());
                sl.Add("FAddress", dt.Rows[j]["FAddress"].ToString());
                sl.Add("FPostCode", dt.Rows[j]["FPostCode"].ToString());
                sl.Add("FResume", dt.Rows[j]["FResume"].ToString());
                sl.Add("FUnit", dt.Rows[j]["FUnit"].ToString());
                sl.Add("FRegisfNation", dt.Rows[j]["FRegisfNation"].ToString());



                sl.Add("FIsDeleted", 0);
                sl.Add("FCreateTime", DateTime.Now);
                sl.Add("FState", 2);
                sl.Add("FRState", dt.Rows[j]["FRState"].ToString());
                sl.Add("FGood", dt.Rows[j]["FGood"].ToString());

                arrayEn.Add(en);
                arrayFKey.Add(feky);
                arraySl.Add(sl);
                arraySo.Add(so);


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppPub_Text where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Pub_Text  where flinkid='" + dt.Rows[j]["FId"].ToString() + "' and fisdeleted=0");
                DataTable dt1 = rc.GetTable(sb.ToString());
                if (dt != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int p = 0; p < iCount; p++)
                    {
                        en = EntityTypeEnum.EqPText;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FLinkId", dt1.Rows[p]["FLinkId"].ToString());
                        sl.Add("FType", dt1.Rows[p]["FType"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FContent", dt1.Rows[p]["FContent"].ToString());
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }


                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkAchievement where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkAchievement");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkAchievement;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProvePeople", dt1.Rows[y]["FProvePeople"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FProjectName", dt1.Rows[y]["FProjectName"].ToString());
                        sl.Add("FProjectLevel", dt1.Rows[y]["FProjectLevel"].ToString());
                        sl.Add("FProjectUnit", dt1.Rows[y]["FProjectUnit"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FResult", dt1.Rows[y]["FResult"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_WorkExperience where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_WorkExperience");
                sb.Append(" where FEmpId='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeWorkExperience;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FProjectId", dt1.Rows[y]["FProjectId"].ToString());
                        sl.Add("FProjectTypeId", dt1.Rows[y]["FProjectTypeId"].ToString());
                        sl.Add("FCost", dt1.Rows[y]["FCost"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FBaseinfoName", dt1.Rows[y]["FBaseinfoName"].ToString());
                        sl.Add("FAppPerson", dt1.Rows[y]["FAppPerson"].ToString());
                        sl.Add("FCompany", dt1.Rows[y]["FCompany"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FAward", dt1.Rows[y]["FAward"].ToString());
                        sl.Add("FBad", dt1.Rows[y]["FBad"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBusiness", dt1.Rows[y]["FBusiness"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FDesc", dt1.Rows[y]["FDesc"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);

                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }

                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_RegistSpecial where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_RegistSpecial where fempid='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeRegistSpecial;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FSpecialTypeId", dt1.Rows[y]["FSpecialTypeId"].ToString());
                        sl.Add("FSpecialNo", dt1.Rows[y]["FSpecialNo"].ToString());
                        sl.Add("FGetTypeId", dt1.Rows[y]["FGetTypeId"].ToString());
                        sl.Add("FGetDate", dt1.Rows[y]["FGetDate"].ToString());
                        sl.Add("FAppSepcialId", dt1.Rows[y]["FAppSepcialId"].ToString());
                        sl.Add("FAppLevelId", dt1.Rows[y]["FAppLevelId"].ToString());
                        sl.Add("FStudy1", dt1.Rows[y]["FStudy1"].ToString());
                        sl.Add("FStudy2", dt1.Rows[y]["FStudy2"].ToString());
                        sl.Add("FRegistTypeId", dt1.Rows[y]["FRegistTypeId"].ToString());
                        sl.Add("FReMark", dt1.Rows[y]["FReMark"].ToString());
                        sl.Add("FIsPrime", dt1.Rows[y]["FIsPrime"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);
                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_RegistCerti where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_RegistCerti where fempid='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeRegistCerti;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FCertiTypeId", dt1.Rows[y]["FCertiTypeId"].ToString());
                        sl.Add("FPubDeptName", dt1.Rows[y]["FPubDeptName"].ToString());
                        sl.Add("FCertiNo", dt1.Rows[y]["FCertiNo"].ToString());
                        sl.Add("FBeginDate", dt1.Rows[y]["FBeginDate"].ToString());
                        sl.Add("FEndDate", dt1.Rows[y]["FEndDate"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);
                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }



                sb.Remove(0, sb.Length);
                sb.Append(" delete from CF_AppEmp_Resume where fempid='" + dt.Rows[j]["FId"].ToString() + "' and fappid='" + fAppId + "'");
                this.PExcute(sb.ToString());

                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_Emp_Resume where fempid='" + dt.Rows[j]["FId"].ToString() + "'");
                dt1 = rc.GetTable(sb.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    int iCount = dt1.Rows.Count;
                    for (int y = 0; y < iCount; y++)
                    {
                        en = EntityTypeEnum.EqEeResume;
                        feky = "FID";
                        so = SaveOptionEnum.Insert;
                        sl = new SortedList();
                        sl.Add("FID", Guid.NewGuid().ToString());
                        sl.Add("FEmpId", dt1.Rows[y]["FEmpId"].ToString());
                        sl.Add("FBaseInfoName", dt1.Rows[y]["FBaseInfoName"].ToString());
                        sl.Add("FBaseInfoId", dt1.Rows[y]["FBaseInfoId"].ToString());
                        sl.Add("FDuty", dt1.Rows[y]["FDuty"].ToString());
                        sl.Add("FWorkContent", dt1.Rows[y]["FWorkContent"].ToString());
                        sl.Add("FApprovePerson", dt1.Rows[y]["FApprovePerson"].ToString());
                        sl.Add("FApproveTel", dt1.Rows[y]["FApproveTel"].ToString());
                        sl.Add("FRemark", dt1.Rows[y]["FRemark"].ToString());
                        sl.Add("FBeginTime", dt1.Rows[y]["FBeginTime"].ToString());
                        sl.Add("FEndTime", dt1.Rows[y]["FEndTime"].ToString());
                        sl.Add("FAppId", fAppId);
                        sl.Add("FCreateTime", DateTime.Now);
                        sl.Add("FIsDeleted", 0);
                        arrayEn.Add(en);
                        arrayFKey.Add(feky);
                        arraySl.Add(sl);
                        arraySo.Add(so);
                    }
                }

            }

            int iCount1 = arraySl.Count;
            if (iCount1 != 0)
            {
                EntityTypeEnum[] ens = new EntityTypeEnum[iCount1];
                string[] fkeys = new string[iCount1];
                SortedList[] sls = new SortedList[iCount1];
                SaveOptionEnum[] sos = new SaveOptionEnum[iCount1];

                for (int k = 0; k < iCount1; k++)
                {
                    ens[k] = (EntityTypeEnum)(arrayEn[k]);
                    fkeys[k] = (String)(arrayFKey[k]);
                    sls[k] = (SortedList)(arraySl[k]);
                    sos[k] = (SaveOptionEnum)(arraySo[k]);

                }
                this.SaveEBaseM(ens, sls, fkeys, sos);
            }
        }

        //企业预审情况
        public void SInitCheckParameter(string fRelationId, string fAppId, ref ArrayList arrEn, ref ArrayList arrSl, ref ArrayList arrFKey, ref ArrayList arrSo)
        {
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from CF_Ent_CheckParameter ");
            sb.Append(" where FRELATIONID='" + fRelationId + "'");

            DataTable dt = rc.GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SortedList sl = new SortedList();
                    EntityTypeEnum en = EntityTypeEnum.EqEbCheckParameter;
                    string fKey = "FID";
                    SaveOptionEnum so = SaveOptionEnum.Insert;


                    sl.Add("FID", Guid.NewGuid());
                    sl.Add("FAppId", fAppId);
                    sl.Add("FSTANDARDTYPEID", dt.Rows[i]["FSTANDARDTYPEID"].ToString());
                    sl.Add("FRELATIONID", dt.Rows[i]["FRELATIONID"].ToString());
                    sl.Add("FNAME", dt.Rows[i]["FNAME"].ToString());
                    sl.Add("FUNIT", dt.Rows[i]["FUNIT"].ToString());
                    sl.Add("FVALUE", dt.Rows[i]["FVALUE"].ToString());
                    sl.Add("FCONDITION", dt.Rows[i]["FCONDITION"].ToString());
                    sl.Add("FISDELETED", 0);
                    sl.Add("FIsOk", dt.Rows[i]["FIsOk"].ToString());
                    sl.Add("FCheckParameterId", dt.Rows[i]["FID"].ToString());
                    arrEn.Add(en);
                    arrFKey.Add(fKey);
                    arrSl.Add(sl);
                    arrSo.Add(so);
                }
            }
        }

        //产生编号
        public string CreateAppDetailNo()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }

        //判断某一个业务类型的申请,是否审批完成
        public bool IsAppEnd(string fManageTypeId, string fBaseInfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from cf_app_list ");
            sb.Append(" where fmanagetypeid='" + fManageTypeId + "' ");
            sb.Append(" and fbaseinfoid='" + fBaseInfoId + "'");
            sb.Append(" and fstate<>6 ");

            int iCount = this.GetSQLCount(sb.ToString());
            if (iCount > 0)
            {
                return false;
            }
            else
            {
                return
                    true;
            }

        }

        //把某一次申请填写数据导到本次申请中
        public void SInitDataFRomOtherApp(string fBaseInfoId, string fAppid, string fNewAppId)
        {
            DelApply(fNewAppId);

            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrFkey = new ArrayList();
            ArrayList arrSo = new ArrayList();

            DataTable dt = null;
            SortedList sl = new SortedList();
            int iCount = 0;

            StringBuilder sb = new StringBuilder();

            //导入企业基本信息
            sb.Append(" select * from cf_appent_baseinfo ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                sl = CreateData(dt.Rows[0]);

                if (sl.Contains("FID"))
                {
                    sl.Remove("FID");
                }
                sl.Add("FID", Guid.NewGuid().ToString());

                if (sl.Contains("FAPPID"))
                {
                    sl.Remove("FAPPID");
                }
                sl.Add("FAPPID", fNewAppId);

                arrEn.Add(EntityTypeEnum.EqEbBaseInfo);
                arrSl.Add(sl);
                arrFkey.Add("FID");
                arrSo.Add(SaveOptionEnum.Insert);
            }


            //导入企业财务信息
            sb.Append(" select * from CF_AppEnt_FINANCE ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                sl = CreateData(dt.Rows[0]);

                if (sl.Contains("FID"))
                {
                    sl.Remove("FID");
                }
                sl.Add("FID", Guid.NewGuid().ToString());

                if (sl.Contains("FAPPID"))
                {
                    sl.Remove("FAPPID");
                }
                sl.Add("FAPPID", fNewAppId);

                arrEn.Add(EntityTypeEnum.EqEbFinance);
                arrSl.Add(sl);
                arrFkey.Add("FID");
                arrSo.Add(SaveOptionEnum.Insert);
            }


            //导入企业其它信息
            sb.Append(" select * from CF_AppEnt_BaseInfoOther ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                sl = CreateData(dt.Rows[0]);

                if (sl.Contains("FID"))
                {
                    sl.Remove("FID");
                }
                sl.Add("FID", Guid.NewGuid().ToString());

                if (sl.Contains("FAPPID"))
                {
                    sl.Remove("FAPPID");
                }
                sl.Add("FAPPID", fNewAppId);

                arrEn.Add(EntityTypeEnum.EqEbBaseInfoOther);
                arrSl.Add(sl);
                arrFkey.Add("FID");
                arrSo.Add(SaveOptionEnum.Insert);
            }


            //导入文本图片信息
            sb.Append(" select * from CF_AppPub_Text ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                iCount = dt.Rows.Count;

                for (int a = 0; a < iCount; a++)
                {
                    sl = CreateData(dt.Rows[a]);
                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);

                    arrEn.Add(EntityTypeEnum.EqPText);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //导入设备
            sb.Append(" select * from CF_AppEnt_Device ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                iCount = dt.Rows.Count;

                for (int b = 0; b < iCount; b++)
                {
                    sl = CreateData(dt.Rows[b]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);

                    arrEn.Add(EntityTypeEnum.EqEbDevice);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //导入工程
            #region
            sb.Append(" select * from CF_AppEnt_Project ");
            sb.Append(" where fappid='" + fAppid + "' ");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                iCount = dt.Rows.Count;
                for (int c = 0; c < iCount; c++)
                {
                    sl = CreateData(dt.Rows[c]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }

                    string fNewPId = Guid.NewGuid().ToString();

                    string fProId = dt.Rows[c]["FProjectId"].ToString();

                    sl.Add("FID", fNewPId);

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);

                    arrEn.Add(EntityTypeEnum.EqEbProject);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);


                }
            }

            //工程附表
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ProjectOther ");
            sb.Append(" and fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {

                iCount = dt.Rows.Count;
                for (int u = 0; u < iCount; u++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[u]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }

                    sl.Add("FID", Guid.NewGuid().ToString());


                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbProjectOther);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //CF_AppEnt_PorjectProcessIdear
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_PorjectProcessIdear ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int d = 0; d < dt.Rows.Count; d++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[d]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());


                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqProjectProcessIdear);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ProjBackUp
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ProjBackUp ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int e = 0; e < dt.Rows.Count; e++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[e]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());


                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);

                    arrEn.Add(EntityTypeEnum.EqEbProjBackUp);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //CF_AppEnt_ProjectAdd
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ProjectAdd ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int f = 0; f < dt.Rows.Count; f++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[f]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());


                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqProjectAdd);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }

                //CF_AppEnt_ProjectAppEmployee
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_AppEnt_ProjectAppEmployee ");
                sb.Append(" where fappid='" + fAppid + "'");
                dt = GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int g = 0; g < dt.Rows.Count; g++)
                    {
                        sl = new SortedList();
                        sl = CreateData(dt.Rows[g]);

                        if (sl.Contains("FID"))
                        {
                            sl.Remove("FID");
                        }
                        sl.Add("FID", Guid.NewGuid().ToString());


                        if (sl.Contains("FAPPID"))
                        {
                            sl.Remove("FAPPID");
                        }
                        sl.Add("FAPPID", fNewAppId);


                        arrEn.Add(EntityTypeEnum.EqProjectAppEmployee);
                        arrSl.Add(sl);
                        arrFkey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }

                //CF_AppEnt_ProjectCerti
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_AppEnt_ProjectCerti ");
                sb.Append(" where fappid='" + fAppid + "'");
                dt = GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int h = 0; h < dt.Rows.Count; h++)
                    {
                        sl = new SortedList();
                        sl = CreateData(dt.Rows[h]);

                        if (sl.Contains("FID"))
                        {
                            sl.Remove("FID");
                        }
                        sl.Add("FID", Guid.NewGuid().ToString());


                        if (sl.Contains("FAPPID"))
                        {
                            sl.Remove("FAPPID");
                        }
                        sl.Add("FAPPID", fNewAppId);


                        arrEn.Add(EntityTypeEnum.EqEbProjectCerti);
                        arrSl.Add(sl);
                        arrFkey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }

                //CF_AppEnt_projectDetail
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_AppEnt_projectDetail ");
                sb.Append(" where fappid='" + fAppid + "'");
                dt = GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        sl = new SortedList();
                        sl = CreateData(dt.Rows[j]);

                        if (sl.Contains("FID"))
                        {
                            sl.Remove("FID");
                        }
                        sl.Add("FID", Guid.NewGuid().ToString());


                        if (sl.Contains("FAPPID"))
                        {
                            sl.Remove("FAPPID");
                        }
                        sl.Add("FAPPID", fNewAppId);


                        arrEn.Add(EntityTypeEnum.EqProjectDetail);
                        arrSl.Add(sl);
                        arrFkey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }


                //CF_AppEnt_ProjectManager
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_AppEnt_ProjectManager ");
                sb.Append(" where fappid='" + fAppid + "'");
                dt = GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        sl = new SortedList();
                        sl = CreateData(dt.Rows[k]);

                        if (sl.Contains("FID"))
                        {
                            sl.Remove("FID");
                        }
                        sl.Add("FID", Guid.NewGuid().ToString());


                        if (sl.Contains("FAPPID"))
                        {
                            sl.Remove("FAPPID");
                        }
                        sl.Add("FAPPID", fNewAppId);


                        arrEn.Add(EntityTypeEnum.EqEbProjectManager);
                        arrSl.Add(sl);
                        arrFkey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }



                //CF_AppEnt_ProjectResult
                sb.Remove(0, sb.Length);
                sb.Append(" select * from CF_AppEnt_ProjectResult ");
                sb.Append(" where fappid='" + fAppid + "'");
                dt = GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int l = 0; l < dt.Rows.Count; l++)
                    {
                        sl = new SortedList();
                        sl = CreateData(dt.Rows[l]);

                        if (sl.Contains("FID"))
                        {
                            sl.Remove("FID");
                        }
                        sl.Add("FID", Guid.NewGuid().ToString());

                        if (sl.Contains("FAPPID"))
                        {
                            sl.Remove("FAPPID");
                        }
                        sl.Add("FAPPID", fNewAppId);


                        arrEn.Add(EntityTypeEnum.EqProjectResult);
                        arrSl.Add(sl);
                        arrFkey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Insert);
                    }
                }


            }

            #endregion

            //导入企业负责人
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_Leader ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[m]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid().ToString());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbLeader);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //导入企业人员
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_Baseinfo ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[n]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeBaseinfo);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //导入人员简历
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_Resume ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int q = 0; q < dt.Rows.Count; q++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[q]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeResume);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEmp_WorkAchievement
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_WorkAchievement ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int w = 0; w < dt.Rows.Count; w++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[w]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeWorkAchievement);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEmp_WorkExperience
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_WorkExperience ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[r]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeWorkExperience);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEmp_RegistCerti
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_RegistCerti ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int t = 0; t < dt.Rows.Count; t++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[t]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeRegistCerti);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }



            //CF_AppEmp_RegistCerti
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEmp_RegistSpecial ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int z = 0; z < dt.Rows.Count; z++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[z]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEeRegistSpecial);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //CF_AppEnt_ApplyAchieve
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplyAchieve ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[x]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplyAchieve);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ApplyDevice
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplyDevice ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int v = 0; v < dt.Rows.Count; v++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[v]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplyDevice);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ApplyDevice
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplyEmployee ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int aa = 0; aa < dt.Rows.Count; aa++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[aa]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplyEmployee);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ApplyDevice
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplyProject ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ab = 0; ab < dt.Rows.Count; ab++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ab]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplyProject);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ApplySpecial
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplySpecial ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ac = 0; ac < dt.Rows.Count; ac++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ac]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplySpecial);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_ApplyTrade
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ApplyTrade ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ad = 0; ad < dt.Rows.Count; ad++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ad]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbApplyTrade);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_BadAction
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_BadAction ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ae = 0; ae < dt.Rows.Count; ae++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ae]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbBadAction);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_BankAccount
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_BankAccount ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int af = 0; af < dt.Rows.Count; af++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[af]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbBankAccount);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //CF_AppEnt_CheckParameter
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_CheckParameter ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ag = 0; ag < dt.Rows.Count; ag++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ag]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbCheckParameter);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_CreditLevel
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_CreditLevel ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ah = 0; ah < dt.Rows.Count; ah++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ah]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbCreditLevel);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_GoodAchieve
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_GoodAchieve ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int ai = 0; ai < dt.Rows.Count; ai++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[ai]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbGoodAchieve);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }


            //CF_AppEnt_GoodAction
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_GoodAction ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int al = 0; al < dt.Rows.Count; al++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[al]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbGoodAction);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            //CF_AppEnt_GoodAction
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_InvestPerson ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int am = 0; am < dt.Rows.Count; am++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[am]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbInvestPerson);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }



            //CF_AppEnt_ZCFZ
            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_AppEnt_ZCFZ ");
            sb.Append(" where fappid='" + fAppid + "'");
            dt = GetTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int an = 0; an < dt.Rows.Count; an++)
                {
                    sl = new SortedList();
                    sl = CreateData(dt.Rows[an]);

                    if (sl.Contains("FID"))
                    {
                        sl.Remove("FID");
                    }
                    sl.Add("FID", Guid.NewGuid());

                    if (sl.Contains("FAPPID"))
                    {
                        sl.Remove("FAPPID");
                    }
                    sl.Add("FAPPID", fNewAppId);


                    arrEn.Add(EntityTypeEnum.EqEbZCFZ);
                    arrSl.Add(sl);
                    arrFkey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);
                }
            }

            iCount = arrEn.Count;
            if (iCount <= 0)
            {
                return;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SortedList[] sls = new SortedList[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];

            for (int xy = 0; xy < iCount; xy++)
            {
                ens[xy] = (EntityTypeEnum)arrEn[xy];
                sls[xy] = new SortedList();
                sls[xy] = (SortedList)arrSl[xy];
                fkeys[xy] = (string)arrFkey[xy];
                sos[xy] = (SaveOptionEnum)arrSo[xy];

            }

            SaveEBaseM(ens, sls, fkeys, sos);
        }


        //数据转化 把dr转换成SortedList
        public SortedList CreateData(DataRow dr)
        {
            if (dr == null)
            {
                return null;
            }
            SortedList sl = new SortedList();

            DataTable dt = dr.Table;

            int iCount = dt.Columns.Count;

            for (int i = 0; i < iCount; i++)
            {
                sl.Add(dt.Columns[i].ColumnName.Trim().ToUpper(), dr[i].ToString());
            }

            return sl;
        }

        //判断某一类企业负责人的信息是否填写
        public bool IsHasEntLeaderData(string fAppId, string fPersonTypeId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select count(1) from cf_appent_leader ");
            sb.Append(" where fappid='" + fAppId + "' and fpersontypeid='" + fPersonTypeId + "'");
            int iCount = GetSQLCount(sb.ToString());
            if (iCount <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}