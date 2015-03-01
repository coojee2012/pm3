using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using log4net;
using EgovaDAO;

namespace EgovaBLL
{
    public class WorkFlowApp
    {
        // private EgovaDB db = new EgovaDB();
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 验证是否可以撤销业务
        /// </summary>
        /// <param name="fAppId"></param>
        /// <returns></returns>
        public bool ValidateCanCancelApply(string fAppId)
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
                if(count>0)
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
                logger.Error("撤销业务过程中出现错误，错误信息：" + e.Message);
                return false;
            }
        }
        //撤销某一次业务申请
        public bool CancelApply(string fAppId)
        {
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
                logger.Error("撤销业务过程中出现错误，错误信息："+e.Message);
                return false;
            }
        }
        
        /// <summary>
        /// 接件操作
        /// </summary>
        /// <param name="fProcessRecordID"></param>
        /// <returns></returns>
        public bool Accept(string fProcessRecordID, string appUserID, int appUserLevel, string idea)
        {
            EgovaDB db = new EgovaDB();
            bool flag = true;
            try
            {
                CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == appUserID).FirstOrDefault();
                CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == fProcessRecordID).FirstOrDefault();
                CF_App_ProcessInstance ep = db.CF_App_ProcessInstance.Where(t => t.FID == er.FProcessinstanceId).FirstOrDefault();
                //更新CF_App_ProcessInstance
                ep.FSeeState = 1;
                ep.fseetime = DateTime.Now;
                ep.FPlanTime = null;
                //CF_App_ProcessRecord
                er.FAppPerson = user.FName;
                er.FAppTime = DateTime.Now;
                er.FCompany = user.FCompany;
                er.FFunction = user.FFunction;
                er.FIdea = idea;
                er.FLevel = appUserLevel;
                er.FMeasure = 5;
                er.FResult = "1";
                er.FUserId = appUserID;
                er.FWaiteTime = (DateTime.Now - (DateTime)ep.FReportDate).Days;

                db.SubmitChanges();
                //处理流程
                flag = this.Transit(fProcessRecordID);
            }
            catch (Exception e)
            {
                flag = false;
                logger.Error("接件过程中出现错误，错误信息："+e.Message);
            }


            return flag;

        }
        /// <summary>
        /// 批转流程
        /// 
        /// </summary>
        /// <param name="fProcessRecordID">CF_App_ProcessRecord表的主键</param>
        /// <returns></returns>
        public bool Transit(string fProcessRecordID)
        {
            EgovaDB db = new EgovaDB();
            bool sucess = true;
            try
            {
                CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == fProcessRecordID).FirstOrDefault();
                if (er == null)
                {
                    logger.Error("批转流程过程中获取不到CF_App_ProcessRecord信息");
                    return false;
                }
                CF_App_ProcessInstance ep = null; //流程实例
                CF_App_List el = null; //业务类型
                CF_App_SubFlow currSub = null; //当前子流程（阶段）
                CF_App_SubFlow nextSub = null; //下个子流程（阶段）
                bool isEnd = false; //该流程是否结束
                ep = db.CF_App_ProcessInstance.Where(t => t.FID == er.FProcessinstanceId).FirstOrDefault();
                if (ep == null)
                {
                    logger.Error("批转流程过程中获取不到CF_App_ProcessInstance信息");
                    return false;
                }

                //计时处理,表示预计的最后截止时间
                DateTime fPlanTime = SConvert.ToDateTime(ep.FPlanTime);//开始了的流程，这个字段会有值
                if (fPlanTime == null || fPlanTime == DateTime.MinValue)//如果为空，求出计划时间,这种情况可能是流程开始
                {
                    //对应的业务类型的计时
                    var query = db.ExecuteQuery<int>("select isnull(FDay,0) from cf_sys_ManageType where fnumber='" + ep.FManageTypeId + "' and fsystemId='" + ep.FSystemId + "'");
                    int dayTotal = query.FirstOrDefault<int>();
                    fPlanTime = this.GetEndTime(DateTime.Now, dayTotal);
                }
                //上报次数或步数
                int fReportCount = db.ExecuteQuery<int>("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + ep.FLinkId + "'").FirstOrDefault<int>();//最大步骤
                fReportCount++;

                //获取当前子流程定义
                currSub = db.CF_App_SubFlow.Where(t => t.FID == ep.FSubFlowId).FirstOrDefault();
                if (currSub == null)
                {
                    logger.Error("批转流程过程中获取不到当前子流程定义CF_App_SubFlow信息");
                    return false;
                }
                //if (currSub.FIsQuali == 1) //需要就位定级不能批量审核
                //{
                //    sb.Remove(0, sb.Length);
                //    sb.Append(" select FIsQuali from CF_App_ProcessRecord  ");
                //    sb.Append(" where FSubFLowId = '" + currSub.FId + "'");
                //    sb.Append(" and FProcessInstanceID='" + ep.FId + "'");

                //    string sIsQuali = rCenter.GetSignValue(sb.ToString());
                //    if (sIsQuali != "3")
                //    {
                //        continue;
                //    }
                //}
                //获取当前子流程定义
                nextSub = GetNextSubFlow(ep.FID);
                //如果扩权县需要跳转
                //if (appProcessRecord[i].Contains("FUpDeptLevel"))
                //{
                //    if (appProcessRecord[i]["FUpDeptLevel"].ToString() != "100")
                //    {
                //        nextSub = GetNextSubFlow(ep.FId, EConvert.ToInt(appProcessRecord[i]["FUpDeptLevel"]));
                //    }
                //}

                //if (appProcessRecord[i].Contains("FIsQuali"))
                //{
                //    //如果已经就位定级 直接办结
                //    if (appProcessRecord[i]["FIsQuali"].ToString() == "3")
                //    {
                //        currSub.FIsEnd = 1;
                //    }
                //}
                //没有下一个子流程了或者当前子流程的审批结束阶段，则表示流程在此结束
                if (nextSub == null || currSub.FIsEnd == 1)
                {
                    isEnd = true;
                }
                if (isEnd) //流程结束
                {
                    //更新CF_App_ProcessInstance表
                    ep.FAppState = 6;
                    //fState when 1 then '上报审核中' when 2 then '打回企业' when 3 then '打回下级' 
                    //when 5 then '未审核证书' when 6 then '审核完成'
                    ep.FState = 6;
                    ep.FPlanTime = fPlanTime;
                    ep.FResult = er.FResult;
                    //更新CF_App_ProcessRecord表


                    CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == ep.FLinkId).FirstOrDefault();
                    if (currSub.FIsAppEnd == 1) //是审核结束
                    {
                        ep.FFactTime = DateTime.Now;
                        //  CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == ep.FLinkId).FirstOrDefault();
                        if (ab != null)
                        {
                            ab.FIsAppEnd = 1;
                            ab.FAppEndTime = DateTime.Now;
                        }
                    }
                    if (er.FResult == "1")//该阶段同意通过，并且已经是审核结束了isEnd = true
                    {

                    }
                    //不是办结和退回企业的业务设置为办结状态
                    int count = db.CF_App_List.Where(t => t.FId == er.FLinkId && t.FState != 2 && t.FState != 6).Count();
                    if (count == 0)
                    {
                        el = db.CF_App_List.Where(t => t.FId == er.FLinkId).FirstOrDefault();
                        el.FState = 6;
                    }
                    if (ab != null)
                    {
                        ab.FIsEnd = 1;
                        ab.FState = 9; 
                        ab.FEndTime = DateTime.Now;
                    }

                }
                else
                {
                    //继续上报,流程往前走，更新CF_App_ProcessInstance状态
                    string currDept = string.Empty;
                    currDept = GetCurrDept(ep.FLinkId, ep.FBaseInfoID, nextSub.FID);
                    ep.FAppState = 0;
                    ep.FState = 1;
                    ep.FSubFlowId = nextSub.FID;
                    ep.FCurStepID = SConvert.ToInt(currDept);
                    ep.FRoleId = nextSub.FRoleId;
                    ep.FDefineDay = nextSub.FDefineDay;
                    ep.FPlanTime = fPlanTime; //终审时间
                    if (currSub.FIsAppEnd == 1) //是审核结束
                    {
                        if (ep.FFactTime == null
                            || ep.FFactTime == Convert.ToDateTime("1900-01-01 00:00:00.000"))
                        {
                            ep.FFactTime = DateTime.Now;  //设置实际结束时间
                            CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == ep.FLinkId).FirstOrDefault();
                            if (ab != null)
                            {
                                ab.FIsAppEnd = 1;
                                ab.FAppEndTime = DateTime.Now;
                            }
                        }
                        //-------- ??? 流程审核结束了，是否更新这个字段
                        ep.FResult = er.FResult;
                    }
                    //插入下一阶段数据
                    CF_App_ProcessRecord newER = new CF_App_ProcessRecord();
                    newER.FID = Guid.NewGuid().ToString();
                    newER.FProcessinstanceId = ep.FID;
                    newER.FLinkId = ep.FLinkId;
                    newER.FSubFlowId = nextSub.FID;
                    newER.FMeasure = 0;
                    newER.FRoleId = nextSub.FRoleId.ToString();
                    newER.FRoleDesc = nextSub.FName;
                    newER.FTypeId = nextSub.FTypeId;
                    newER.FIsPrint = nextSub.FIsPrint;
                    newER.FIsQuali = nextSub.FIsQuali;
                    newER.FOrder = nextSub.FOrder;
                    newER.FIsDeleted = 0;
                    newER.FReportTime = DateTime.Now;
                    newER.FDefineDay = nextSub.FDefineDay;
                    newER.FLevel = nextSub.FLevel;
                    newER.FReportCount = fReportCount;//审核步骤
                    db.CF_App_ProcessRecord.InsertOnSubmit(newER);

                    //继承上级审核结果
                    //if (appProcessRecord[i].Contains("FResult"))
                    //{
                    //    sl.Add("FResult", appProcessRecord[i]["FResult"].ToString());
                    //}
                    //if (appProcessRecord[i].Contains("FPersonnel"))
                    //{
                    //    sl.Add("FPersonnel", appProcessRecord[i]["FPersonnel"].ToString());
                    //}
                    //if (appProcessRecord[i].Contains("FPerformance"))
                    //{
                    //    sl.Add("FPerformance", appProcessRecord[i]["FPerformance"].ToString());
                    //}

                    //更新当前阶段数据，比如操作人等


                    //------------------------------------
                }

                db.SubmitChanges();
                EgovaDB db1 = new EgovaDB();
                //流程结束，备份
                if (isEnd)
                {
                    BackUpProcessInstance(ep.FID);
                }
            }
            catch (Exception e)
            {
                sucess = false;
                logger.Error("接件过程中出现错误，错误信息：" + e.Message);
            }
            return sucess;
        }
        /// <summary>
        /// 备份流程
        /// </summary>
        /// <param name="fProcessInstanceID"></param>
        /// <returns></returns>
        public bool BackUpProcessInstance(string fProcessInstanceID)
        {
            EgovaDB db = new EgovaDB();
            bool sucess = false;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(" begin ");
                sb.Append(" insert into CF_App_ProcessInstanceBackup select * from CF_App_ProcessInstance where   fid='" + fProcessInstanceID + "'");
                sb.Append(" insert into CF_App_ProcessRecordBackup select * from CF_App_ProcessRecord where   FProcessInstanceID='" + fProcessInstanceID + "'");
                sb.Append(" delete from CF_App_ProcessInstance where fid='" + fProcessInstanceID + "'");
                sb.Append(" delete from CF_App_ProcessRecord where FProcessInstanceID='" + fProcessInstanceID + "'");
                sb.Append(" end ");
                db.ExecuteCommand(sb.ToString());
                sucess = true;
            }catch(Exception e)
            {
                sucess = false;
                logger.Error("备份流程出现错误，错误信息："+e.Message);
            }
            
            return sucess;
        }
        /// <summary>
        /// 上报
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public bool Report(string fBaseInfoId, string fAppId, string fYear, string fMonth, string FSystemId, string FManageDeptId, string fnumber, string fUpDept)
        {
            EgovaDB db = new EgovaDB();
            bool sucess = false;
            StringBuilder sb = new StringBuilder();
            try 
            {
                if (fnumber == null || fnumber == "")
                {
                    logger.Error("上报过程中出现错误，参数fnumber不能为空");
                    return false;
                }
                CF_App_List app = (from a in db.CF_App_List
                                   where a.FId == fAppId
                                   select a).FirstOrDefault();
                if (app == null )
                {
                    logger.Error("上报过程中出现错误，获取不到对应的业务表");
                    return false;
                }
                CF_Sys_User user = (from a in db.CF_Sys_User
                                   join b in db.CF_Sys_UserRight
                                   on a.FID equals b.FUserId
                                   where b.FBaseinfoID == fBaseInfoId
                                        select a).FirstOrDefault();
                if(user == null)
                {
                    logger.Error("上报过程中出现错误，获取不到对应的上报部门");
                    return false;
                }
                //var t = (from a in db.CF_Ent_BaseInfo
                //         where app.FId == fBaseInfoId
                //         select a).FirstOrDefault();
                //if (t == null)
                //{
                //    logger.Error("上报过程出现错误，获取不到部门信息 ");
                //    return false;
                //}
                string sBarCode = GetBarCode(fnumber, FSystemId);
                if (sBarCode == null || sBarCode == "")
                {
                    logger.Error("上报过程中出现错误，获取不到对应的BarCode");
                    return false;
                }
                CF_Sys_ManageDept ManageDept = (from a in db.CF_Sys_ManageDept
                                                where a.FNumber == SConvert.ToInt(fnumber)
                                                select a).FirstOrDefault();
                CF_App_Process ep;
                //提取是否扩权县
                bool isTown = ManageDept.FIsTown;
                if (isTown)
                {
                    //扩权县判断
                    ep = this.GetProcess(FSystemId, SConvert.ToString(app.FManageTypeId), "1930100", "1930100", "700101");
                    if (ep == null)
                    {
                        ep = this.GetProcess(FSystemId, SConvert.ToString(app.FManageTypeId), "1930100", "1930100", FManageDeptId);
                        if (ep == null)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    ep = this.GetProcess(FSystemId, SConvert.ToString(app.FManageTypeId), "1930100", "1930100", FManageDeptId);
                }
                if (ep == null)
                {
                    logger.Error("上报过程中出现错误，获取不到对应的流程");
                    return false;
                }
                CF_App_SubFlow startSubFlow = this.GetStartSubFlow(ep.FID, fBaseInfoId, fUpDept);
                if (startSubFlow == null)
                {
                    logger.Error("上报过程中出现错误，获取不到对应的开始流程节点");
                    return false;
                }
                string fCurStepID = GetCurrDeptYYXM(fBaseInfoId, startSubFlow.FID, fAppId);
                if (fCurStepID == null || fCurStepID == "")
                {
                    logger.Error("上报过程中出现错误，获取不到对应的fCurStepID");
                    return false;
                }
                string fManageDeptId = fCurStepID;
                //新建CF_App_ProcessInstance
                CF_App_ProcessInstance cp = new CF_App_ProcessInstance();
                cp.FID = Guid.NewGuid().ToString();
                cp.FLinkId = fAppId;
                cp.FBaseInfoID = fBaseInfoId;
                cp.FEntName = user.FName;
                cp.FIsPrime = 0;
                cp.FListId = SConvert.ToInt("19301");
                cp.FLevelId = SConvert.ToInt("1930100");
                cp.FTypeId = SConvert.ToInt("1930100");
                cp.FEmpId = app.FLinkId;
                cp.FLeadId = "";
                cp.FLeadName = "";
                cp.FIsTemp = 0;
                //施工企业资质是否暂定
                //if (sDeatil[i]["FIsTemp"] != null)
                //{
                //    cp.FIsTemp = 0;
                //    sl[i].Add("FIsTemp", sDeatil[i]["FIsTemp"].ToString());
                //}
                cp.FIsNew = 0;
                cp.FManageTypeId = app.FManageTypeId;
                cp.FManageDeptId = SConvert.ToInt(fCurStepID);
                cp.FProcessId = ep.FID;
                cp.FSubFlowId = startSubFlow.FID;
                cp.FYear = SConvert.ToInt(fYear);
                cp.FMonth = SConvert.ToInt(fMonth);
                cp.FSubmitDate = DateTime.Now;
                cp.FReportDate = DateTime.Now;
                cp.FCurStepID = SConvert.ToInt(fCurStepID);
                cp.FRoleId = startSubFlow.FRoleId;
                cp.FBeginRoleId = startSubFlow.FRoleId;
                cp.FDefineDay = startSubFlow.FDefineDay;
                cp.FState = 1;
                cp.FAppState = 1;
                cp.FCreateTime = DateTime.Now;
                cp.FIsDeleted = 0;
                cp.FSystemId = SConvert.ToInt(FSystemId);
                cp.FResult = "未审批完成";
                cp.FBarCode = sBarCode;
                db.CF_App_ProcessInstance.InsertOnSubmit(cp);

                //新建CF_App_ProcessRecord
                CF_App_ProcessRecord cr = new CF_App_ProcessRecord();
                cr.FID = Guid.NewGuid().ToString();
                cr.FProcessinstanceId = cp.FID;
                cr.FLinkId = fAppId;
                cr.FRoleDesc = startSubFlow.FName;
                cr.FMeasure = 0;
                cr.FReportTime = DateTime.Now;
                cr.FRoleId = SConvert.ToString(startSubFlow.FRoleId);
                cr.FSubFlowId = startSubFlow.FID;
                cr.FIsQuali = startSubFlow.FIsQuali;
                cr.FIsPrint = startSubFlow.FIsPrint;
                cr.FManageDeptId = SConvert.ToInt(fCurStepID);
                cr.FDefineDay = startSubFlow.FDefineDay;
                cr.FTypeId = startSubFlow.FTypeId;
                cr.FOrder = startSubFlow.FOrder;
                cr.FLevel = startSubFlow.FLevel;
                cr.FIsDeleted = 0;
                db.CF_App_ProcessRecord.InsertOnSubmit(cr);

                //更新业务表
                app.FBaseName = user.FName;
                app.FReportDate = DateTime.Now;
                app.FYear = SConvert.ToInt(fYear);
                app.FState = 1;
                app.FTime = DateTime.Now;
                app.FUpDeptId = SConvert.ToInt(fnumber);
                

                //先删除以前可能遗留的流程数据
                sb.Remove(0, sb.Length);
                sb.Append("delete from CF_App_ProcessInstance where flinkid='" + fAppId + "';delete from CF_App_ProcessRecord where flinkid='" + fAppId + "'");
                sb.Append(";delete from CF_App_AcceptBook where FLinkId='" + fAppId + "'  ");
                db.ExecuteCommand(sb.ToString());
                //提交数据
             //   db.SubmitChanges();

                //启动审批流程
                //sb.Remove(0, sb.Length);
                //sb.Append(" delete from CF_App_AcceptBook where FLinkId='" + fAppId + "'  ");
                //sb.Append(" and FState<>11 ");
                //db.ExecuteCommand(sb.ToString());

                var t = (from a in db.CF_Ent_BaseInfo
                         where a.FId == fBaseInfoId
                         select a).FirstOrDefault();
                if (t == null)
                {
                    logger.Error("上报过程出现错误，获取不到部门信息 ");
                    return false;
                }

                CF_App_AcceptBook ab = new CF_App_AcceptBook();
                ab.FId = Guid.NewGuid().ToString();
                ab.FSystemId = SConvert.ToInt(FSystemId);
                ab.FBaseInfoId = fBaseInfoId;
                ab.FLinkId = fAppId;
                ab.FAppNo = sBarCode;
                ab.FApplyUserId = fBaseInfoId;
                ab.FAppMangeTypeId = app.FManageTypeId;
                ab.FApplyTime = app.FReportDate;
                ab.FState = 3;
                ab.FIsDeleted = 0;
                ab.FCreateTime = DateTime.Now;
                ab.FEntName = t.FName;
                ab.FEntPostCode = "";
                ab.FEntCode = t.FJuridcialCode;
                ab.FEntAddress = t.FRegistAddress;
                ab.FEntEmail = t.FEmail;
                ab.FEntLinkMan = t.FLinkMan;
                ab.FEntLinkTel = t.FTel;
                ab.FEntManager = t.FOTxt5;
                ab.FApplyAcceptUnit = t.FName;
                ab.FApplyAcceptPerson = t.FOTxt5;
                ab.FAppMangeTypeName = SConvert.ToString(
                            (from m in db.CF_Sys_ManageType
                               where m.FNumber == app.FManageTypeId
                               select new
                               {
                                   m.FName
                               }).FirstOrDefault()
                               );
                db.CF_App_AcceptBook.InsertOnSubmit(ab);
                db.SubmitChanges();              
                sucess = true;
            }catch(Exception  e)
            {
                sucess = false;
                logger.Error("上报过程发生错误，错误信息："+e.Message);
            }
            return sucess;

        }
        /// <summary>
        /// 回退（退件）
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public bool Rollback(string flistId, string appUserId, string roleId, string dfId, string backIdea)
        {
            EgovaDB db = new EgovaDB();
            StringBuilder sb = new StringBuilder();
            bool sucess = true;
            try
            {
                backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
                CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == appUserId).FirstOrDefault();
                CF_App_List app = db.CF_App_List.Where(t => t.FId == flistId).FirstOrDefault();
                if (app != null)
                {
                    string FLinkId = flistId;
                    string fbaseInfoId = app.FBaseinfoId;
                    //查询是否有办结的，如果没有，可以打回到企业
                    sb.Remove(0, sb.Length);
                    sb.Append("select count(*) from (");
                    sb.Append(" select fid from cf_App_ProcessInstanceBackup where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(" union ");
                    sb.Append(" select fid from cf_App_ProcessInstance where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(")tt");
                    int iCount = db.ExecuteQuery<int>(sb.ToString()).FirstOrDefault<int>();
                    if (iCount > 0)//如果有办结的流程，不可打回 |继续下一轮
                    {
                        logger.Error("业务已经办结，不可以打回");
                        return false;
                    }
                    //否则
                    var table = (from ep in db.CF_App_ProcessInstance
                                 join er in db.CF_App_ProcessRecord
                                 on ep.FID equals er.FProcessinstanceId
                                 where
                             ep.FSubFlowId == er.FSubFlowId
                             && er.FRoleId == roleId
                             && er.FManageDeptId.ToString().Contains(dfId)
                             && er.FTypeId == 1
                             && (er.FMeasure == null || er.FMeasure == 0 || er.FMeasure == 4)
                             && ep.FState != 6
                             && ep.FLinkId == flistId
                                 select new
                                 {
                                     epID = ep.FID,
                                     erID = er.FID
                                 }).ToList();
                    foreach (var item in table)
                    {
                        string epID = item.epID;
                        string erID = item.erID;
                        //更新CF_App_ProcessInstance
                        CF_App_ProcessInstance ep = db.CF_App_ProcessInstance.Where(t => t.FID == epID).FirstOrDefault();
                        ep.FState = 2;
                        ep.FBackIdea = backIdea;
                        ep.FResult = "2";
                        ep.FFactTime = DateTime.Now;

                        //更新
                        CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == erID).FirstOrDefault();
                        er.FUserId = appUserId;
                        er.FMeasure = 5; // 已经办理
                        er.FResult = "2";
                        er.FAppTime = DateTime.Now;
                        er.FAppPerson = user.FName;
                        er.FCompany = user.FCompany;
                        er.FFunction = user.FFunction;
                        er.FIdea = backIdea;
                    }
                    //更新CF_App_AcceptBook
                    CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == FLinkId).FirstOrDefault();
                    if (ab != null)
                    {
                        ab.FState = 11;
                    }
                    //更新到CF_App_Idea
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
                    idea.FReportCount = app.FReportCount;
                    idea.FResult = "打回";
                    idea.FResultInt = 2;
                    idea.FContent = backIdea;
                    idea.FAppTime = DateTime.Now;
                    idea.FUserId = appUserId;
                    idea.FTime = DateTime.Now;

                    //保存CF_App_List
                    app.FState = 2;
                    app.FResult = backIdea;

                    db.SubmitChanges();
                }

            }
            catch(Exception e)
            {
                sucess = false;
                logger.Error("回退出现错误，错误信息：" + e.Message);
            }
            

            return sucess;

        }
        /// <summary>
        /// 回退（退件）到企业
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public bool BackToEnt(string flistId, string appUserId, string roleId, string dfId, string backIdea)
        {
            EgovaDB db = new EgovaDB();
            StringBuilder sb = new StringBuilder();
            bool sucess = true;
            try
            {
                backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
                CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == appUserId).FirstOrDefault();
                CF_App_List app = db.CF_App_List.Where(t => t.FId == flistId).FirstOrDefault();
                if (app != null)
                {
                    string FLinkId = flistId;
                    string fbaseInfoId = app.FBaseinfoId;
                    //查询是否有办结的，如果没有，可以打回到企业
                    sb.Remove(0, sb.Length);
                    sb.Append("select count(*) from (");
                    sb.Append(" select fid from cf_App_ProcessInstanceBackup where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(" union ");
                    sb.Append(" select fid from cf_App_ProcessInstance where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                    sb.Append(")tt");
                    int iCount = db.ExecuteQuery<int>(sb.ToString()).FirstOrDefault<int>();
                    if (iCount > 0)//如果有办结的流程，不可打回 |继续下一轮
                    {
                        logger.Error("业务已经办结，不可以打回");
                        return false;
                    }
                    //否则
                    var table = (from ep in db.CF_App_ProcessInstance
                                 join er in db.CF_App_ProcessRecord
                                 on ep.FID equals er.FProcessinstanceId
                                 where
                             ep.FSubFlowId == er.FSubFlowId
                             && er.FRoleId == roleId
                             && er.FManageDeptId.ToString().Contains(dfId)
                             && er.FTypeId == 1
                             && (er.FMeasure == null || er.FMeasure == 0 || er.FMeasure == 4)
                             && ep.FState != 6
                             && ep.FLinkId == flistId
                                 select new
                                 {
                                     epID = ep.FID,
                                     erID = er.FID
                                 }).ToList();
                    foreach (var item in table)
                    {
                        string epID = item.epID;
                        string erID = item.erID;
                        //更新CF_App_ProcessInstance
                        CF_App_ProcessInstance ep = db.CF_App_ProcessInstance.Where(t => t.FID == epID).FirstOrDefault();
                        ep.FState = 2;
                        ep.FBackIdea = backIdea;
                        ep.FResult = "2";
                        ep.FFactTime = DateTime.Now;

                        //更新
                        CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == erID).FirstOrDefault();
                        er.FUserId = appUserId;
                        er.FMeasure = 5; // 已经办理
                        er.FResult = "2";
                        er.FAppTime = DateTime.Now;
                        er.FAppPerson = user.FName;
                        er.FCompany = user.FCompany;
                        er.FFunction = user.FFunction;
                        er.FIdea = backIdea;
                    }
                    //更新CF_App_AcceptBook
                    CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == FLinkId).FirstOrDefault();
                    if (ab != null)
                    {
                        ab.FState = 11;
                    }
                    //更新到CF_App_Idea
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
                    idea.FReportCount = app.FReportCount;
                    idea.FResult = "打回";
                    idea.FResultInt = 2;
                    idea.FContent = backIdea;
                    idea.FAppTime = DateTime.Now;
                    idea.FUserId = appUserId;
                    idea.FTime = DateTime.Now;

                    //保存CF_App_List
                    app.FState = 2;
                    app.FResult = backIdea;

                    db.SubmitChanges();
                }

            }
            catch (Exception e)
            {
                sucess = false;
                logger.Error("回退出现错误，错误信息：" + e.Message);
            }


            return sucess;

        }
        /// <summary>
        /// 不予受理并结案
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public bool BackAndEnd(string flistId, string appUserId, string backIdea)
        {
            EgovaDB db = new EgovaDB();
            StringBuilder sb = new StringBuilder();
            bool sucess = true;
            try
            {
                backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
                CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == appUserId).FirstOrDefault();
                CF_App_List app = db.CF_App_List.Where(t => t.FId == flistId).FirstOrDefault();
                if (app != null)
                {
                    //更新CF_App_ProcessInstance
                    CF_App_ProcessInstance ep = db.CF_App_ProcessInstance.Where(t => t.FLinkId == flistId).FirstOrDefault();
                    if (ep != null)
                    {
                        ep.FSeeState = 3;
                        ep.FState = 6;
                        ep.FBackIdea = backIdea;
                        ep.FResult = "3";
                        ep.FFactTime = DateTime.Now;
                        ep.fseetime = DateTime.Now;
                    }

                    //更新
                    CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FLinkId == flistId).FirstOrDefault();
                    if (er != null)
                    {
                        er.FUserId = appUserId;
                        er.FAppTime = DateTime.Now;
                        er.FAppPerson = user.FName;
                        er.FCompany = user.FCompany;
                        er.FFunction = user.FFunction;
                        er.FIdea = backIdea;
                        er.FMeasure = 1;
                    }
                    //更新CF_App_AcceptBook
                    CF_App_AcceptBook ab = db.CF_App_AcceptBook.Where(t => t.FId == flistId).FirstOrDefault();
                    if (ab != null)
                    {
                        ab.FState = 13;
                        ab.FEndTime = DateTime.Now;
                    }
                    //更新到CF_App_Idea
                    CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == flistId).FirstOrDefault();
                    if (idea == null)
                    {
                        idea = new CF_App_Idea()
                        {
                            FId = Guid.NewGuid().ToString(),
                            FCreateTime = DateTime.Now,
                            FLinkId = flistId,
                            FIsdeleted = 0,
                        };
                        db.CF_App_Idea.InsertOnSubmit(idea);
                    }
                    idea.FReportCount = app.FReportCount;
                    idea.FResult = "打回";
                    idea.FResultInt = 2;
                    idea.FContent = backIdea;
                    idea.FAppTime = DateTime.Now;
                    idea.FUserId = appUserId;
                    idea.FTime = DateTime.Now;

                    //保存CF_App_List
                    app.FState = 6;
                    app.FResult = backIdea;

                    db.SubmitChanges();
                }

            }
            catch (Exception e)
            {
                sucess = false;
                logger.Error("不予受理出现错误，错误信息：" + e.Message);
            }
            return sucess;

        }

        #region 得到当前流程的下一个子流程
        /// <summary>
        /// 得到当前流程的下一个子流程
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <returns>EaSubFlow 子流程对象</returns>
        public CF_App_SubFlow GetNextSubFlow(string fProcessInstanceId)
        {
            EgovaDB db = new EgovaDB();
            CF_App_ProcessInstance epI = db.CF_App_ProcessInstance.Where(t => t.FID == fProcessInstanceId).FirstOrDefault();
            if (epI == null)
            {
                return null;
            }
            CF_App_SubFlow currSub = db.CF_App_SubFlow.Where(t => t.FID == epI.FSubFlowId).FirstOrDefault();
            if (currSub == null || currSub.FIsEnd == 1)  //FIsEnd 1 流程结束
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("forder>" + currSub.FOrder + " and FProcessId='" + epI.FProcessId + "' order by forder");
            CF_App_SubFlow nextSub = (from t in db.CF_App_SubFlow
                                      where t.FProcessId == epI.FProcessId
                                        && t.FOrder > currSub.FOrder
                                      orderby t.FOrder ascending
                                      select t).FirstOrDefault();
            return nextSub;
        }

        /// <summary>
        /// 得到当前流程的符合审批等级的下一个子流程
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <param name="iLevel">审批等级</param>
        /// <returns>EaSubFlow 子流程对象</returns>
        public CF_App_SubFlow GetNextSubFlow(string fProcessInstanceId, int iLevel)
        {
            EgovaDB db = new EgovaDB();
            CF_App_ProcessInstance epI = db.CF_App_ProcessInstance.Where(t => t.FID == fProcessInstanceId).FirstOrDefault();
            if (epI == null)
            {
                return null;
            }
            CF_App_SubFlow currSub = db.CF_App_SubFlow.Where(t => t.FID == epI.FSubFlowId).FirstOrDefault();
            if (currSub == null || currSub.FIsEnd == 1)  //FIsEnd 1 流程结束
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("forder>" + currSub.FOrder + " and FProcessId='" + epI.FProcessId + "' order by forder");
            CF_App_SubFlow nextSub = (from t in db.CF_App_SubFlow
                                      where t.FProcessId == epI.FProcessId
                                        && t.FOrder > currSub.FOrder
                                        && t.FLevel == iLevel
                                      orderby t.FOrder ascending
                                      select t).FirstOrDefault();
            return nextSub;
        }
        #endregion
        /// <summary>
        /// 得到启始子流程
        /// </summary>
        /// <param name="fProcessId"></param>
        /// <param name="fBaseInfoId"></param>
        /// <param name="fUpDept"></param>
        /// <returns></returns>
        public CF_App_SubFlow GetStartSubFlow(string fProcessId, string fBaseInfoId, string fUpDept)
        {
            EgovaDB db = new EgovaDB();
            StringBuilder sb = new StringBuilder();
            var fLevel = (from t in db.CF_Sys_ManageDept
                          where t.FNumber == int.Parse(fUpDept)
                          select t.FLevel).FirstOrDefault();
            if (fLevel == null)
            {
                return null;
            }
            CF_App_SubFlow es = (from t in db.CF_App_SubFlow
                                 where t.FProcessId == fProcessId
                                 && t.FLevel <= fLevel
                                 orderby t.FOrder
                                 select t).FirstOrDefault();
            return es;


        }

        /// <summary>
        /// 得到某一流程的第一个子流程
        /// </summary>
        /// <param name="fProcessId"></param>
        /// <returns></returns>
        public CF_App_SubFlow GetFirstSubFlow(string fProcessId)
        {
            EgovaDB db = new EgovaDB();
            CF_App_SubFlow es = (from t in db.CF_App_SubFlow
                                 where t.FProcessId == fProcessId
                                 orderby t.FOrder
                                 select t).FirstOrDefault();
            return es;
        }

        /// <summary>
        /// 根据开始时间和天数求出结束日期
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dayCount"></param>
        /// <returns></returns>
        public DateTime GetEndTime(DateTime dateStart, int dayCount)
        {
            EgovaDB db = new EgovaDB();
            //如果开始计算的结束时间，在开始与结束中间有休息日，则计算出有几个休息日，然后把递归往后推几天
            DateTime dateEnd = dateStart.AddDays(dayCount);//结束时间
            string sql = "select count(*) from CF_Sys_Holidays where FDate>='" + dateStart.ToShortDateString() + "' and  FDate<'" + dateEnd.ToShortDateString() + "'";
            dayCount = db.ExecuteQuery<int>(sql).FirstOrDefault<int>();
            if (dayCount > 0)
            {
                return GetEndTime(dateEnd, dayCount);
            }
            else
            {
                return dateEnd;
            }
        }

        public string GetCurrDept(string FAppId, string fBaseInfoId, string fSubFlowId)
        {
            EgovaDB db = new EgovaDB();
            CF_App_SubFlow es = db.CF_App_SubFlow.Where(t => t.FID == fSubFlowId).FirstOrDefault();
            if (es == null)
            {
                return "";
            }
            string[] fUpDeptNumber = GetEntAllUpDeptYYXM(fBaseInfoId, FAppId).Split(',');
            if (fUpDeptNumber.Length == 0)
            {
                return "";
            }
            string tempStr = "";
            for (int i = 0; i < fUpDeptNumber.Length; i++)
            {
                if (i == 0)
                {
                    tempStr += "'" + fUpDeptNumber[i] + "'";
                }
                else
                {
                    tempStr += ",'" + fUpDeptNumber[i] + "'";
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("select top 1 fnumber from CF_Sys_ManageDept where fnumber in(");
            sb.Append(tempStr);
            sb.Append(")");
            sb.Append(" and fisdeleted=0 and flevel = " + es.FLevel + "  order by fnumber desc");
            var fnumber = db.ExecuteQuery<int?>(sb.ToString()).FirstOrDefault();
            if(fnumber!=null)
            {
                return fnumber.ToString();
            }
            else
            {
                return "";
            } 
        }

        public string GetEntAllUpDeptYYXM(string fBaseInfoId, string fappid, params string[] s)
        {
            EgovaDB db = new EgovaDB();
            string fReturnValue = "";
            int? fDeptNumber = db.ExecuteQuery<int?>("select FUpDeptId from cf_App_list where FBaseInfoId='" + fBaseInfoId + "' and FId='" + fappid + "'").FirstOrDefault();
            if (fDeptNumber == null )
            {
                return "";
            }
            fReturnValue = fReturnValue.Insert(0, fDeptNumber.ToString());
            var v = db.CF_Sys_ManageDept.Where(t => t.FNumber == fDeptNumber).FirstOrDefault();
            if (v != null && v.FParentId != null)
            {
                fDeptNumber = v.FParentId;
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }
            var v1 = db.CF_Sys_ManageDept.Where(t => t.FNumber == fDeptNumber).FirstOrDefault();
            if (v1 != null && v1.FParentId != null)
            {
                fDeptNumber = v1.FParentId;
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }
            return fReturnValue;
        }
        public string GetCurrDeptYYXM(string fBaseInfoId, string fSubFlowId, string fAppId)
        {
            EgovaDB db = new EgovaDB();
            string result = "";
            try
            {
                CF_App_SubFlow es = (from a in db.CF_App_SubFlow
                                     where a.FID == fSubFlowId
                                     select a).FirstOrDefault();
                if (es == null)
                {
                    logger.Error("GetCurrDeptYYXM 内的空值");
                    return "";
                }
                string[] fUpDeptNumber = GetEntAllUpDeptYYXM(fBaseInfoId, fAppId).Split(',');
                if (fUpDeptNumber.Length == 0)
                {
                    return "";
                }
                string tempStr = "";
                for (int i = 0; i < fUpDeptNumber.Length; i++)
                {
                    if (i == 0)
                    {
                        tempStr += "'" + fUpDeptNumber[i] + "'";
                    }
                    else
                    {
                        tempStr += ",'" + fUpDeptNumber[i] + "'";
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("select top 1 fnumber from CF_Sys_ManageDept where fnumber in(");
                sb.Append(tempStr);
                sb.Append(")");
                sb.Append(" and fisdeleted=0 and flevel = " + es.FLevel + "  order by fnumber desc");
                var r = db.ExecuteQuery<int>(sb.ToString()).FirstOrDefault();
                result = SConvert.ToString(r);
                return result;
            }
            catch(Exception e)
            {
                logger.Error("GetCurrDeptYYXM 内的异常,异常信息："+e.Message);
                return "";
            }
            
        }
        #region 获取申请编号
        public string GetBarCode(string fRegistDeptNumber, string fSystemId)
        {
            EgovaDB db = new EgovaDB();
            string sBarCode = "";
            try
            {
                //4位注册地区
                if (fRegistDeptNumber.Length > 4)
                {
                    fRegistDeptNumber = fRegistDeptNumber.Substring(0, 4);
                }
                else
                {
                    fRegistDeptNumber = fRegistDeptNumber.PadRight(4, '0');
                }
                sBarCode += fRegistDeptNumber;

                //四位年份
                sBarCode += DateTime.Now.Year.ToString();

                //两位月份
                sBarCode += DateTime.Now.Month.ToString().Length > 1 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString();

                //两位系统编号
                switch (fSystemId)
                {
                    case "101":
                        sBarCode += "01";
                        break;
                    case "120":
                        sBarCode += "02";
                        break;
                    case "125":
                        sBarCode += "03";
                        break;
                    case "130":
                        sBarCode += "12";
                        break;

                    case "135":
                        sBarCode += "04";
                        break;
                    case "140":
                        sBarCode += "05";
                        break;
                    case "145":
                        sBarCode += "06";
                        break;
                    case "150":
                        sBarCode += "07";
                        break;
                    case "155":
                        sBarCode += "14";
                        break;
                    case "160":
                        sBarCode += "08";
                        break;
                    case "165":
                        sBarCode += "09";
                        break;
                    case "175":
                        sBarCode += "10";
                        break;
                    case "180":
                        sBarCode += "11";
                        break;
                    case "185":
                        sBarCode += "12";
                        break;
                    case "186":
                        sBarCode += "13";
                        break;
                    case "187":
                        sBarCode += "14";
                        break;
                    case "193":
                        sBarCode += "15";
                        break;
                    case "196":
                        sBarCode += "16";
                        break;
                    case "199":
                        sBarCode += "17";
                        break;
                    case "202":
                        sBarCode += "18";
                        break;
                    case "210":
                        sBarCode += "19";
                        break;
                    case "167":
                        sBarCode += "20";
                        break;
                    default:
                        string sTemp = fSystemId.PadLeft(2, '0');
                        sBarCode += sTemp.Substring(sTemp.Length - 2);
                        break;
                }
                string fMaxBarCode = db.CF_App_No.Where(t => t.FNo.Contains(sBarCode)).Max(t => t.FNo);
                if (fMaxBarCode != null && fMaxBarCode != "")
                {
                    int iXh = SConvert.ToInt(fMaxBarCode.Substring(14, 6));
                    iXh++;
                    string sXh = "000000" + iXh.ToString();
                    sXh = sXh.Substring(sXh.Length - 6, 6);
                    sBarCode = sBarCode + sXh;
                }
                else
                {
                    sBarCode = sBarCode + "000001";
                }
                CF_App_No newItem = new CF_App_No();
                newItem.FId = Guid.NewGuid().ToString();
                newItem.FNo = sBarCode;
                newItem.FIsDeleted = 0;
                newItem.FCreateTime = DateTime.Now;
                newItem.FSystemId = SConvert.ToInt(fSystemId);
                db.CF_App_No.InsertOnSubmit(newItem);
                db.SubmitChanges();
                EgovaDB db1 = new EgovaDB();
                int count = SConvert.ToInt(db1.ExecuteQuery<int>("select count(*) from cf_App_ProcessInstance where FBarCode='" + sBarCode + "'"));
                if (count > 0)
                {
                    return this.GetBarCode(fRegistDeptNumber, fSystemId);
                }
                else
                {
                    return sBarCode;
                }
            }catch(Exception e)
            {
                logger.Error("获取编号出错,错误信息："+e.Message);
                return "";
            }

            
        }
        #endregion

        #region 得到一个流程
        /// <summary>
        ///得到一个流程
        /// </summary>
        /// <param name="fManageTypeId">流程包含的业务</param>
        /// <param name="fLevelId">流程包含的资质等级</param>
        /// <param name="fQualiTypeId">流程包含的专业</param>
        /// <returns>EaProcess对象</returns>
        public CF_App_Process GetProcess(string fSystemId, string fManageTypeId, string fLevelId, string fQualiTypeId, string FManageDeptId)
        {
            EgovaDB db = new EgovaDB();
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 cap.* from CF_App_Process cap,CF_App_QualiLevel caq,");
            sb.Append(" CF_App_ManageType cam,");
            sb.Append(" CF_App_QualiType caqt ");
            sb.Append(" where cap.fid = caq.FProcessId and cap.fid = cam.FProcessId ");
            sb.Append(" and cap.fid = caqt.FProcessId ");
            sb.Append(" and cap.fisdeleted=0 ");
            sb.Append(" and caq.fisdeleted=0 ");
            sb.Append(" and cam.fisdeleted=0 ");
            sb.Append(" and caqt.fisdeleted=0 ");
            sb.Append(" and cap.FManageDeptId='" + FManageDeptId + "' ");
            sb.Append(" and caq.FLevelId ='" + fLevelId + "'");
            sb.Append(" and caqt.FQualiTypeId ='" + fQualiTypeId + "'");
            sb.Append(" and cam.FManageTypeId='" + fManageTypeId + "' ");
            sb.Append(" and cap.FSystemId=" + fSystemId);
            if (fSystemId == "101" && fManageTypeId == "113")//如果是施工企业资质变更的话，就不能选择这条流程
            {
                sb.Append(" and cap.fnumber<>465");
            }
            if (fSystemId == "125" && fManageTypeId == "134")//如果是监理企业资质变更的话，就不能选择这条流程
            {
                sb.Append(" and cap.fnumber<>263");
            }
            CF_App_Process cp = db.ExecuteQuery<CF_App_Process>(sb.ToString()).FirstOrDefault();
            return cp;
        }
        #endregion
        /// <summary>
        /// 验证是否可以新建一个业务
        /// </summary>
        /// <param name="prjItemId">工程</param>
        /// <param name="manageTypeId">业务类型</param>
        /// <returns></returns>
        public bool ValidateNewBiz(string prjItemId, int manageTypeId)
        {
            bool flag = false;
            EgovaDB db = new EgovaDB();
            string sql = @"select count(*) from CF_App_List a,TC_QA_Record b 
                    where a.FId = b.FAppId and a.FManageTypeId={0} and b.FPrjItemId='{1}'";
            sql = string.Format(sql,manageTypeId,prjItemId);
            int count = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
            if (count > 0)
            {
                sql = @"select count(*) from CF_App_List a,TC_QA_Record b 
                    where a.FId = b.FAppId and a.FManageTypeId={0} and b.FPrjItemId='{1}' 
                    and a.FState = 6 and a.FResult = 3 ";
                sql = string.Format(sql, manageTypeId, prjItemId);
                int count1 = SConvert.ToInt(db.ExecuteQuery<int>(sql).FirstOrDefault());
                if (count1 > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                 flag = true;
            }
            return flag;
        }
        public bool ValidateReport(string fAppId)
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
    }
}
