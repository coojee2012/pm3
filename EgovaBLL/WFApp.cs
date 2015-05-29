using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

using EgovaDAO;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;

namespace EgovaBLL
{
   public class WFApp
    {
        /// <summary>
        /// 验证是否可以进行接件、审核、回退等操作（其中一种操作后，不能操作另外一种用）
        /// </summary>
        /// <param name="fAppId"></param>
        /// <param name="fProcessRecordID"></param>
        /// <returns></returns>
       public static bool ValidateCanDo(string fProcessRecordID)
        {
            bool flag = false;
            EgovaDB db = new EgovaDB();
            string sql = @" select count(*) from CF_App_ProcessRecord r,CF_App_ProcessInstance p
                            where r.FProcessinstanceId=p.FID
                            and r.FID='{0}' and r.FMeasure=0 and p.FState <> 2";
            sql = string.Format(sql, fProcessRecordID);
            int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
            if (count > 0)
            {
                
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 验证是否可以新建一个业务
        /// </summary>
        /// <param name="prjItemId">工程</param>
        /// <param name="manageTypeId">业务类型</param>
        /// <returns></returns>
       public static bool ValidateNewBiz(string prjItemId, int manageTypeId)
        {
            bool flag = false;
            EgovaDB db = new EgovaDB();
            string sql = @"select count(*) from CF_App_List 
                    where FManageTypeId={0} and FLinkId='{1}'";
            sql = string.Format(sql, manageTypeId, prjItemId);
            int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
            if (count > 0)
            {
                sql = @"select count(*) from CF_App_List 
                    where FManageTypeId={0} and FLinkId='{1}' 
                    and FState != 6  and  isnull(fisdeleted,0) ! =1";// and FResult = 3 ";
                sql = string.Format(sql, manageTypeId, prjItemId);
                int count1 = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
                if (count1 > 0)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }
        public static bool ValidateReport(string fAppId)
        {
            bool flag = false;
            EgovaDB db = new EgovaDB();
            string sql = @" select COUNT(*) from CF_App_List a,CF_App_ProcessInstance cp
                            where a.FId = cp.FLinkId 
                            and a.FState not in (0,2)
                            and a.FId='{0}'";
            sql = string.Format(sql, fAppId);
            int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
            if (count > 0)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }
       /// <summary>
       /// 企业端上报
       /// </summary>
       /// <param name="fAppId"></param>
       /// <param name="fSystemId"></param>
       /// <param name="fDeptNumber"></param>
       /// <param name="fNumber"></param>
       /// <returns></returns>
        public static bool Report(string fAppId, string fSystemId, string fDeptNumber, string fNumber)
       {
           EgovaDB db = new EgovaDB();
           RApp ra = new RApp();
           string dept = ComFunction.GetDefaultDept();
           if (fDeptNumber == null || fDeptNumber == "")
           {              
               return false;
           }
           if (!ValidateReport(fAppId))
           {
               return false;
           }
           DateTime dTime = DateTime.Now;
           //设计端业务
           CF_App_List app = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
           SortedList[] sl = new SortedList[1];
           if (app != null)
           {
               
               sl[0] = new SortedList();
               sl[0].Add("FID", app.FId);
               sl[0].Add("FAppId", app.FId);
               sl[0].Add("FBaseInfoId", app.FBaseinfoId);
               sl[0].Add("FManageTypeId", app.FManageTypeId);
               sl[0].Add("FListId", "19301");
               sl[0].Add("FTypeId", "1930100");
               sl[0].Add("FLevelId", "1930100");
               sl[0].Add("FIsPrime", 0);

               //sl.Add("FAppDeptId", row["FAppDeptId"].ToString());
               //sl.Add("FAppDeptName", row["FAppDeptName"].ToString());
               sl[0].Add("FAppTime", DateTime.Now);
               sl[0].Add("FIsNew", 0);
               sl[0].Add("FIsBase", 0);
               sl[0].Add("FIsTemp", 0);
               //  sl[0].Add("FUpDept", p_FManageDeptId.SelectedValue);
               sl[0].Add("FUpDept", fNumber);
               //sl[0].Add("FEmpId", app.FLinkId);//CF_Prj_Data.FID
               //sl[0].Add("FEmpName", app.FPrjName);
               //sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
               //sl[0].Add("FLeadName", app.FTxt1);
               sl[0].Add("FEmpId", app.FLinkId);//CF_Prj_Data.FID
               sl[0].Add("FEmpName", "");
               sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
               sl[0].Add("FLeadName", "");
               StringBuilder sb = new StringBuilder();

               sb.Append("update CF_App_List set FUpDeptId=" + fNumber + ",");
               sb.Append("ftime=getdate() where fid = '" + fAppId + "'");
          //     rc.PExcute(sb.ToString());
               db.ExecuteCommand(sb.ToString());

               if (ra.EntStartProcessKCSJ(app.FBaseinfoId, fAppId, app.FYear.ToString(), DateTime.Now.Month.ToString(), fSystemId, fDeptNumber, fNumber, sl))
               {
                   return true;
               }
               else
               {
                   return false;
               }


           }

           return true;
       }
        /// <summary>
        /// 验证是否可以撤销业务
        /// </summary>
        /// <param name="fAppId"></param>
        /// <returns></returns>
       public static bool ValidateCanCancelApply(string fAppId)
        {
            string sql = @"select COUNT(*) from 
                        (
                        select top 1 cr.* from
                         CF_App_ProcessRecord cr,CF_App_ProcessInstance  cp
                         where 
                         cr.FLinkId = '{0}'
                         and cr.FLinkId = cp.FLinkId
                         and cp.FState not in (2,6)
                         order by cr.FOrder
                         ) t
                         where t.FMeasure = 0 ";
            try
            {
                sql = string.Format(sql, fAppId);
                EgovaDB db = new EgovaDB();
                int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
               // logger.Error("撤销业务过程中出现错误，错误信息：" + e.Message);
                return false;
            }
        }
        //撤销某一次业务申请
       public static bool CancelApply(string fAppId)
        {
            if(!ValidateCanCancelApply(fAppId))
            {
                return false;
            }
            StringBuilder sb = new StringBuilder();
            try
            {
                EgovaDB db = new EgovaDB();
                sb.Remove(0, sb.Length);
                sb.Append(" begin ");
                sb.Append("update CF_App_List set fstate=0 where FId='" + fAppId + "' ");
                sb.Append("delete from CF_App_ProcessRecord ");
                sb.Append("where fprocessInstanceId in (");
                sb.Append("select fid from CF_App_ProcessInstance where flinkid='" + fAppId + "' and isnull(FState,0) not in (2,6)");
                sb.Append(");");
                sb.Append("delete from CF_App_ProcessInstance where flinkid='" + fAppId + "' and isnull(FState,0) not in (2,6)");
                sb.Append(" end ");
                db.ExecuteCommand(sb.ToString());
                return true;

            }
            catch (Exception e)
            {
              //  logger.Error("撤销业务过程中出现错误，错误信息：" + e.Message);
                return false;
            }
        }
       /// <summary>
       /// 办理业务，即保存意见，操作人等
       /// </summary>
       /// <param name="fProcessRecordID"></param>
       /// <param name="sIdea"></param>
       /// <param name="sResult"></param>
       /// <param name="sAppPerson"></param>
       /// <param name="sCompany"></param>
       /// <param name="sFunction"></param>
       /// <param name="sAppTime"></param>
       /// <returns></returns>
       public static bool Assign(string fProcessRecordID,string sIdea,string sResult,string sAppPerson,string sCompany,string sFunction,string sAppTime)
       {
           bool success = false;
           RCenter rc = new RCenter(); 
           SortedList osort = new SortedList();
           osort.Add("FID", fProcessRecordID);
           osort.Add("FAppPerson", sAppPerson);
           osort.Add("FCompany", sCompany);
           osort.Add("FFunction", sFunction);
           osort.Add("FAppTime", sAppTime);
           osort.Add("FIdea", sIdea);
           osort.Add("FResult", sResult);
           success = rc.SaveEBase(EntityTypeEnum.EaProcessRecord, osort, "FID", SaveOptionEnum.Update);
           return success;
       }
       /// <summary>
       /// 上报流程，接件、初审、复审、办结等操作调用(审批过程中使用)
       /// </summary>
       /// <param name="fAppId"></param>
       /// <param name="fProcessInstanceID"></param>
       /// <param name="fProcessRecordID"></param>
       /// <param name="dfUserId"></param>
       /// <param name="sIdea"></param>
       /// <param name="sResult"></param>
       /// <param name="sAppPerson"></param>
       /// <param name="sCompany"></param>
       /// <param name="sFunction"></param>
       /// <param name="sAppTime"></param>
       /// <returns></returns>
       public static bool ReportProcess(string fAppId, string fProcessInstanceID, string fProcessRecordID,
           string dfUserId,string sIdea,string sResult,string sAppPerson,string sCompany,string sFunction,string sAppTime)
       {
           bool success = false;
           RCenter rc = new RCenter(); 
           RApp ra = new RApp();
           //先办理（保存意见等）
           success = Assign(fProcessRecordID,sIdea,sResult,sAppPerson,sCompany,sFunction,sAppTime);
           string FLinkId = fAppId;
           EaProcessRecord Er = (EaProcessRecord)rc.GetEBase(EntityTypeEnum.EaProcessRecord, "FResult", "FID='" + fProcessRecordID + "'");
           SortedList[] sl = new SortedList[1];
           sl[0] = new SortedList();
           sl[0].Add("FID", fProcessRecordID);
           sl[0].Add("FProcessInstanceID", fProcessInstanceID);
           sl[0].Add("FLinkId", FLinkId);
           sl[0].Add("FMeasure", 5);
           if (Er != null && !string.IsNullOrEmpty(Er.FResult))
               sl[0].Add("FResult", Er.FResult);
           else sl[0].Add("FResult", sResult);

           sl[0].Add("FUserId", dfUserId);
           EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + fProcessInstanceID + "'");
           DateTime fReportTime = epi.FReportDate;
           DateTime nowTime = DateTime.Now;
           TimeSpan spanTime = nowTime - fReportTime;
           sl[0].Add("FWaiteTime", spanTime.Days);
           success = ra.GovReportProcessKCSJ(sl, "");
           return success;
       }

       /// <summary>
       /// 打回到上一级
       /// </summary>
       /// <param name="fAppId">业务ID</param>
       /// <param name="sTypeId">子流程编码</param>
       /// <param name="dfRoleId">操作人角色</param>
       /// <param name="opPerson">操作人</param>
       /// <returns></returns>
       public static bool BackToPre(string fAppId, string sTypeId, string dfRoleId, string opPerson)
       {
           bool success = false;
           RCenter rc = new RCenter();            
           string FLinkId = fAppId;
           int fReportCount = EConvert.ToInt(rc.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + FLinkId + "'"));//最大步骤
           fReportCount++;
           DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessInstance, "FID", "flinkid='" + FLinkId + "' AND fstate=1");
           int iCount = dt.Rows.Count;
           if (iCount > 0)
           {
               ArrayList arrEn = new ArrayList();
               ArrayList arrSl = new ArrayList();
               ArrayList arrKey = new ArrayList();
               ArrayList arrSo = new ArrayList();
               for (int i = 0; i < iCount; i++)
               {
                   string fProcessInstanceId = dt.Rows[i]["FID"].ToString();
                   SortedList sl = new SortedList();
                   SortedList sl1 = new SortedList();
                   SortedList sl2 = new SortedList();
                   DataTable dtTemp = rc.GetTable("select * from cf_App_ProcessRecord where FProcessinstanceId='" + fProcessInstanceId + "' ");
                   string fSubFlowId = string.Empty;
                   string fRoleId = string.Empty;
                   if (dtTemp != null && dtTemp.Rows.Count > 0)//如果有这一步
                   {
                       sl1.Clear();
                       DataRow drTemp = dtTemp.Rows[0];
                       sl1.Add("FID", System.Guid.NewGuid().ToString());
                       sl1.Add("FProcessInstanceID", fProcessInstanceId);
                       sl1.Add("FLinkId", drTemp["FLinkId"]);
                       sl1.Add("FRoleDesc", drTemp["FRoleDesc"]);
                       sl1.Add("FMeasure", 4);//被打回状态
                       sl1.Add("FReportTime", DateTime.Now);
                       sl1.Add("FRoleId", drTemp["FRoleId"]);
                       sl1.Add("FSubFlowId", drTemp["FSubFlowId"]);
                       fRoleId = drTemp["FRoleId"].ToString();//角色
                       fSubFlowId = drTemp["FSubFlowId"].ToString();//步骤
                       sl1.Add("FIsQuali", drTemp["FIsQuali"]);
                       sl1.Add("FIsPrint", drTemp["FIsPrint"]);
                       sl1.Add("FManageDeptId", drTemp["FManageDeptId"]);
                       sl1.Add("FDefineDay", drTemp["FDefineDay"]);
                       sl1.Add("FTypeId", drTemp["FTypeId"]);
                       sl1.Add("FOrder", drTemp["FOrder"]);
                       sl1.Add("FLevel", drTemp["FLevel"]);
                       sl1.Add("FIsDeleted", 0);
                       sl1.Add("FReportCount", fReportCount);//审核步骤
                       arrEn.Add(EntityTypeEnum.EaProcessRecord);
                       arrSl.Add(sl1);
                       arrKey.Add("FID");
                       arrSo.Add(SaveOptionEnum.Insert);//新插入一条 

                       string fnewId = rc.GetSignValue("select fid from cf_App_ProcessRecord where fprocessInstanceId='" + fProcessInstanceId
                           + "' and ftypeId='" + sTypeId + "' and froleId in(" + dfRoleId + ") order by FReportCount desc");
                       if (!string.IsNullOrEmpty(fnewId))
                       {
                           sl2.Clear();
                           sl2.Add("FID", fnewId);
                           sl2.Add("FMeasure", 3);//标识为打回到上一步状态
                           sl2.Add("FAppPerson", opPerson);
                           sl2.Add("FIdea", "打回");
                           arrEn.Add(EntityTypeEnum.EaProcessRecord);
                           arrSl.Add(sl2);
                           arrKey.Add("FID");
                           arrSo.Add(SaveOptionEnum.Update);
                       }

                   }
                   sl.Clear();
                   sl.Add("FID", fProcessInstanceId);
                   if (!string.IsNullOrEmpty(fSubFlowId))
                       sl.Add("FSubFlowId", fSubFlowId);
                   if (!string.IsNullOrEmpty(fRoleId))
                       sl.Add("FRoleId", fRoleId);
                   arrEn.Add(EntityTypeEnum.EaProcessInstance);
                   arrSl.Add(sl);
                   arrKey.Add("FID");
                   arrSo.Add(SaveOptionEnum.Update);
               }
               StringBuilder sb = new StringBuilder();
               iCount = arrSo.Count;
               EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
               SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
               string[] fkeys = new string[iCount];
               SortedList[] sls = new SortedList[iCount];

               for (int t = 0; t < iCount; t++)
               {
                   ens[t] = (EntityTypeEnum)arrEn[t];
                   sos[t] = (SaveOptionEnum)arrSo[t];
                   fkeys[t] = (string)arrKey[t];

                   sls[t] = new SortedList();
                   sls[t] = (SortedList)arrSl[t];
               }
               success = rc.SaveEBaseM(ens, sls, fkeys, sos);
           }

           return success;
       }
       /// <summary>
       /// 打回到企业
       /// </summary>
       /// <param name="fProcessInstanceID"></param>
       /// <param name="dfUserId"></param>
       /// <param name="fLevel"></param>
       /// <param name="sIdea"></param>
       /// <param name="sResult"></param>
       /// <param name="sAppPerson"></param>
       /// <param name="sCompany"></param>
       /// <param name="sFunction"></param>
       /// <param name="sAppTime"></param>
       /// <returns></returns>
       public static bool BackToEnt(string fProcessInstanceID, string dfUserId, string fLevel, string sIdea, string sResult,
           string sAppPerson, string sCompany, string sFunction, string sAppTime)
       {
           bool success = false;
           RCenter rc = new RCenter();
           RApp ra = new RApp();
           if (fProcessInstanceID == null || fProcessInstanceID == "")
           {
               return false;
           }
           EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceID + "'");
           if (epi == null)
           {
               return false;
           }

           EaProcessRecord eap = ra.GetProcessRecord(fProcessInstanceID, epi.FSubFlowId);
           if (eap == null)
           {
               return false;
           }
           eap.FAppPerson = sAppPerson;
           eap.FAppTime = EConvert.ToDateTime(sAppTime);
           eap.FCompany = sCompany;
           eap.FFunction = sFunction;
           eap.FIdea = sIdea;
           eap.FLevel = EConvert.ToInt(fLevel);
           eap.FLinkId = epi.FLinkId;
           eap.FMeasure = 0;
           eap.FOrder = 5000;
           eap.FUserId = dfUserId;
           DateTime fReportTime = epi.FReportDate;
           DateTime nowTime = DateTime.Now;
           TimeSpan spanTime = nowTime - fReportTime;
           eap.FWaiteTime = spanTime.Days;
           success = ra.BackProcessToEnt(fProcessInstanceID, eap);
           //保存CF_App_List,CF_App_Idea的状态
           EgovaDB db = new EgovaDB();
           CF_App_List app = db.CF_App_List.Where(t => t.FId == epi.FLinkId).FirstOrDefault();          
           if (app != null)
           {
               app.FState = 2;
               app.FResult = sIdea;
               //更新到CF_App_Idea
               CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == epi.FLinkId).FirstOrDefault();
               if (idea == null)
               {
                   idea = new CF_App_Idea()
                   {
                       FId = Guid.NewGuid().ToString(),
                       FCreateTime = DateTime.Now,
                       FLinkId = epi.FLinkId,
                       FIsdeleted = 0,
                   };
                   db.CF_App_Idea.InsertOnSubmit(idea);
               }
               idea.FReportCount = app.FReportCount;
               idea.FResult = "打回";
               idea.FResultInt = 2;
               idea.FContent = sIdea;
               idea.FAppTime = DateTime.Now;
               idea.FUserId = dfUserId;
               idea.FTime = DateTime.Now;
               db.SubmitChanges();
           }           
           return success;
       }
       /// <summary>
       /// 不予受理，直接结案
       /// </summary>
       /// <param name="fProcessInstanceID"></param>
       /// <param name="fProcessRecordID"></param>
       /// <param name="sIdea"></param>
       /// <param name="dfUserId"></param>
       /// <returns></returns>
       public static bool EndApp(string fProcessInstanceID, string fProcessRecordID, string dfUserId, string sIdea)
       {
            bool success = false;
            RCenter rc = new RCenter();
            RApp ra = new RApp();
            EgovaDB db = new EgovaDB();

            string backIdea = sIdea;
            backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;

            StringBuilder sb = new StringBuilder();
            string erIds = string.Empty;
            //string s = ReturnString(out erIds);
            string s = fProcessInstanceID;
            erIds = fProcessRecordID;
            if (s.Trim() == "")
            {
                return false;
            }
            string[] strs = s.Trim().Split(',');
            string[] strs1 = erIds.Trim().Split(',');
            if (strs.Length <= 0)
            {
                return false;
            }

            int iCount = strs.Length;

            ArrayList arrSl = new ArrayList();
            ArrayList arrEn = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList array = new ArrayList();
            ArrayList arrLink = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {
                array.Add(strs[i]);

                SortedList sl = new SortedList();
                sl.Add("FID", strs[i]);
                sl.Add("FSeeState", 3);
                sl.Add("FState", 6);
                sl.Add("FSeeTime", DateTime.Now);

                arrSl.Add(sl);
                arrEn.Add(EntityTypeEnum.EaProcessInstance);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
                if (strs1.Length > i)
                {
                    sl = new SortedList();
                    sl.Add("FID", strs1[i]);
                    sl.Add("FUserId", dfUserId);
                    sl.Add("FResult", 3);
                    sl.Add("FMeasure", 5);
                    arrSl.Add(sl);
                    arrEn.Add(EntityTypeEnum.EaProcessRecord);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);
                }
                sb.Remove(0, sb.Length);
                sb.Append(" select flinkid from CF_App_ProcessInstance  ");
                sb.Append(" where fid='" + strs[i] + "'");

                string fLinkId = rc.GetSignValue(sb.ToString());
                if (!arrLink.Contains(fLinkId))
                {
                    arrLink.Add(fLinkId);
                }
                //龚成龙【2010-03-16】
                rc.PExcute("update CF_App_ProcessRecord set FAppTime=getdate() where fprocessInstanceId='" + strs[i] + "' and FTypeId=1");//修改退件的时间

            }
            iCount = arrLink.Count;
            for (int j = 0; j < iCount; j++)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select fid from CF_App_AcceptBook ");
                sb.Append(" where flinkid='" + arrLink[j].ToString() + "'");

                string fId = rc.GetSignValue(sb.ToString());
                if (fId != null && fId != "")
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", fId);
                    sl.Add("FState", 13);
                    arrSl.Add(sl);
                    arrEn.Add(EntityTypeEnum.EaAcceptBook);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);
                }

                string FLinkId = arrLink[j].ToString();
                //保存到CF_App_Idea
                CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
                if (idea == null)
                {
                    idea = new CF_App_Idea()
                    {
                        FId = Guid.NewGuid().ToString(),
                        FCreateTime = DateTime.Now,
                        FLinkId = FLinkId,
                        FIsdeleted = 0,
                    };
                    db.CF_App_Idea.InsertOnSubmit(idea);
                }
                idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FReportCount).FirstOrDefault();
                idea.FResult = "打回";
                idea.FResultInt = 2;
                idea.FContent = sIdea;
                idea.FAppTime = DateTime.Now;
                idea.FUserId = EConvert.ToString(dfUserId);
                idea.FTime = DateTime.Now;
            }
            db.SubmitChanges();

            iCount = arrSo.Count;

            SortedList[] sls = new SortedList[iCount];
            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            for (int k = 0; k < iCount; k++)
            {
                sls[k] = new SortedList();
                sls[k] = (SortedList)arrSl[k];
                ens[k] = (EntityTypeEnum)arrEn[k];
                fkeys[k] = (string)arrKey[k];
                sos[k] = (SaveOptionEnum)arrSo[k];
            }
            success = rc.SaveEBaseM(ens, sls, fkeys, sos);
            ra.BatchEnd(array, backIdea, "3");
            return success;
       }
   }
}
