using System.Text;
using System.Collections;
using System.Data;
using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using Approve.RuleBase;
using Approve.EntityCenter;
using Approve.PersistBase;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntitySys;
using System.Xml;
using System.Web;
using System.IO;
using System.Collections.Generic;
using Approve.EntityQuali;
namespace Approve.RuleApp
{
    public class RApp : RBase
    {
        private static Hashtable hashNumber = new Hashtable();
        private PApp _pApp;
        private RCenter _rCenter;

        private string strCenter = "";
        public RApp()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCenter">RCenter库名</param>
        public RApp(string strCenter)
        {
            this.strCenter = strCenter;
        }
        private PApp pApp
        {
            get
            {
                if (this._pApp == null)
                {
                    _pApp = new PApp();
                }
                return _pApp;
            }
        }
        private RCenter rCenter
        {
            get
            {
                if (this._rCenter == null)
                {
                    this._rCenter = new RCenter(strCenter);
                }
                return _rCenter;
            }
        }





        #region 得到一个企业的所有上报部门
        /// <summary>
        /// 得到一个企业的所有上报部门
        /// </summary>
        /// <param name="fBaseInfoId">企业Id</param>
        /// <returns>企业的所有上级部门以,为分隔符</returns>
        public string GetEntAllUpDept(string fBaseInfoId)
        {
            string fReturnValue = "";
            string fDeptNumber = this.rCenter.GetSignValue(EntityTypeEnum.EsUser, "FManageDeptId", "FBaseInfoId='" + fBaseInfoId + "'");
            if (fDeptNumber == null || fDeptNumber == "")
            {
                return "";
            }
            fReturnValue = fReturnValue.Insert(0, fDeptNumber);

            fDeptNumber = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + fDeptNumber + "'");
            if (fDeptNumber != null || fDeptNumber != "")
            {
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }

            fDeptNumber = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + fDeptNumber + "'");
            if (fDeptNumber != null || fDeptNumber != "")
            {
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }
            return fReturnValue;
        }
        #endregion

        #region 得到当前管理部门

        public string GetCurrDept(string fBaseInfoId, string fSubFlowId)
        {
            EaSubFlow es = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + fSubFlowId + "'");
            if (es == null)
            {
                return "";
            }
            string[] fUpDeptNumber = GetEntAllUpDept(fBaseInfoId).Split(',');
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
            return rCenter.GetSignValue(sb.ToString());
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
        public EaProcess GetProcess(string fSystemId, string fManageTypeId, string fLevelId, string fQualiTypeId, string FManageDeptId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 cap.fid from CF_App_Process cap,CF_App_QualiLevel caq,");
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
            return (EaProcess)this.rCenter.GetEBase(EntityTypeEnum.EaProcess, "", "fid in (" + sb.ToString() + ")");
        }

        /// <summary>
        ///得到一个流程
        /// </summary>
        /// <param name="fManageTypeId">流程包含的业务</param>
        /// <param name="fLevelId">流程包含的资质等级</param>
        /// <param name="fQualiTypeId">流程包含的专业</param>
        /// <returns>EaProcess对象</returns>
        public EaProcess GetProcessByEntId(string fSystemId, string fManageTypeId, string fLevelId, string fQualiTypeId, string FManageDeptId, string FBaseId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 cap.fid from CF_App_Process cap,CF_App_QualiLevel caq,");
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

            return (EaProcess)this.rCenter.GetEBase(EntityTypeEnum.EaProcess, "", "fid in (" + sb.ToString() + ")");
        }


        //运营项目
        public string GetEntAllUpDeptYYXM(string fBaseInfoId, string fappid, params string[] s)
        {
            string fReturnValue = "";
            string fDeptNumber = rCenter.GetSignValue("select FUpDeptId from cf_App_list where FBaseInfoId='" + fBaseInfoId + "' and FId='" + fappid + "'");
            if (fDeptNumber == null || fDeptNumber == "")
            {
                return "";
            }
            fReturnValue = fReturnValue.Insert(0, fDeptNumber);
            fDeptNumber = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + fDeptNumber + "'");
            if (fDeptNumber != null || fDeptNumber != "")
            {
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }

            fDeptNumber = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + fDeptNumber + "'");
            if (fDeptNumber != null || fDeptNumber != "")
            {
                fReturnValue = fReturnValue.Insert(0, fDeptNumber + ",");
            }
            return fReturnValue;
        }

        #region 运营项目专用
        public string GetCurrDeptYYXM(string fBaseInfoId, string fSubFlowId, string fAppId)
        {
            EaSubFlow es = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + fSubFlowId + "'");
            if (es == null)
            {
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
            return rCenter.GetSignValue(sb.ToString());
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
        public EaProcess GetProcess(string fManageTypeId, string fLevelId, string fQualiTypeId, string FManageDeptId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top 1 cap.fid from CF_App_Process cap,CF_App_QualiLevel caq,");
            sb.Append("CF_App_ManageType cam,");
            sb.Append("CF_App_QualiType caqt ");
            sb.Append(" where cap.fid = caq.FProcessId and cap.fid = cam.FProcessId and cap.fid = caqt.FProcessId ");
            sb.Append(" and cap.fisdeleted=0 ");
            sb.Append(" and caq.fisdeleted=0 ");
            sb.Append(" and cam.fisdeleted=0 ");
            sb.Append(" and caqt.fisdeleted=0 ");
            sb.Append(" and cap.FManageDeptId='" + FManageDeptId + "' ");
            sb.Append(" and caq.FLevelId ='" + fLevelId + "'");
            sb.Append(" and caqt.FQualiTypeId ='" + fQualiTypeId + "'");
            sb.Append(" and cam.FManageTypeId='" + fManageTypeId + "' ");

            return (EaProcess)this.rCenter.GetEBase(EntityTypeEnum.EaProcess, "", "fid in (" + sb.ToString() + ")");
        }
        #endregion
        /// <summary>
        /// 可以根据从省或市或县开始的流程
        /// </summary>
        /// <param name="fSystemId">类系统类型</param>
        /// <param name="fManageTypeId">业务类型</param>
        /// <param name="fLevelId">等级</param>
        /// <param name="fQualiTypeId">序列</param>
        /// <param name="FManageDeptId">系统部门</param>
        /// <param name="DeptLevel">1省，2市，3县，开始</param>
        /// <returns></returns>
        public EaProcess GetProcess(string fSystemId, string fManageTypeId, string fLevelId, string fQualiTypeId, string FManageDeptId, int StartLevel, int EndLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select cap.fid from CF_App_Process cap,CF_App_QualiLevel caq,");
            sb.AppendLine(" CF_App_ManageType cam,");
            sb.AppendLine(" CF_App_QualiType caqt ");
            sb.AppendLine(" where cap.fid = caq.FProcessId and cap.fid = cam.FProcessId and cap.fid = caqt.FProcessId ");
            sb.AppendLine(" and cap.fisdeleted=0 ");
            sb.AppendLine(" and caq.fisdeleted=0 ");
            sb.AppendLine(" and cam.fisdeleted=0 ");
            sb.AppendLine(" and caqt.fisdeleted=0 ");
            sb.AppendLine(" and cap.FManageDeptId='" + FManageDeptId + "' ");
            sb.AppendLine(" and caq.FLevelId ='" + fLevelId + "'");
            sb.AppendLine(" and caqt.FQualiTypeId ='" + fQualiTypeId + "'");
            sb.AppendLine(" and cam.FManageTypeId='" + fManageTypeId + "' ");
            sb.AppendLine(" and cap.FSystemId=" + fSystemId);

            sb.AppendLine(@" and  cap.FId in (
             select t1.FProcessId from (
            select FProcessId,min(FOrder) FOrder,max(FLevel) FLevel from CF_App_SubFlow   group by FProcessId ) t1
            inner join 
            (
            select FProcessId,max(FOrder)FOrder,max(FLevel) FLevel from CF_App_SubFlow group by FProcessId ) t2
            on t1.FProcessId=t2.FProcessId where t1.FLevel= " + StartLevel + " and t2.FLevel=" + EndLevel + @" ) ");
            return (EaProcess)this.rCenter.GetEBase(EntityTypeEnum.EaProcess, "", "fid in (" + sb.ToString() + ")");
        }
        #endregion


        #region 得到启始子流程
        public EaSubFlow GetStartSubFlow(string fProcessId, string fBaseInfoId, string fUpDept)
        {


            StringBuilder sb = new StringBuilder();
            sb.Append(" fnumber='" + fUpDept + "'");
            string fLevel = rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FLevel", sb.ToString());
            if (fLevel == null || fLevel == "")
            {
                return null;
            }
            sb.Remove(0, sb.Length);
            sb.Append("FProcessId='" + fProcessId + "' and FLevel<=" + fLevel + " order by forder");
            EaSubFlow es = (EaSubFlow)this.rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "  * ", sb.ToString());
            return es;


        }
        #endregion

        #region 得到某一流程的第一个子流程
        /// <summary>
        /// 得到某一流程的第一个子流程
        /// </summary>
        /// <param name="fProcessId">主流程id</param>
        /// <returns>EaSubFlow对象</returns>
        public EaSubFlow GetFirstSubFlow(string fProcessId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FProcessId='" + fProcessId + "' order by forder");
            return (EaSubFlow)this.rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "  * ", sb.ToString());
        }
        #endregion

        #region 得到当前流程的上一个子流程
        /// <summary>
        /// 得到当前流程的上一个子流程
        /// </summary>
        /// <param name="fProcessId">流程实例Id</param>
        /// <returns></returns>
        public EaSubFlow GetPreviousFlow(string fProcessInstanceId)
        {
            EaProcessInstance epI = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epI == null)
            {
                return null;
            }
            EaSubFlow currSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "FOrder", "FId='" + epI.FSubFlowId + "'");
            if (currSub == null)  //FIsEnd 1 流程结束
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("forder<" + currSub.FOrder + " and FProcessId='" + epI.FProcessId + "' order by forder desc");
            EaSubFlow previousSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", sb.ToString());
            return previousSub;
        }
        #endregion

        #region 得到当前流程的下一个子流程
        /// <summary>
        /// 得到当前流程的下一个子流程
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <returns>EaSubFlow 子流程对象</returns>
        public EaSubFlow GetNextSubFlow(string fProcessInstanceId)
        {
            EaProcessInstance epI = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epI == null)
            {
                return null;
            }
            EaSubFlow currSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "FOrder", "FId='" + epI.FSubFlowId + "'");
            if (currSub == null || currSub.FIsEnd == 1)  //FIsEnd 1 流程结束
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("forder>" + currSub.FOrder + " and FProcessId='" + epI.FProcessId + "' order by forder");
            EaSubFlow nextSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", sb.ToString());
            return nextSub;
        }

        /// <summary>
        /// 得到当前流程的符合审批等级的下一个子流程
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <param name="iLevel">审批等级</param>
        /// <returns>EaSubFlow 子流程对象</returns>
        public EaSubFlow GetNextSubFlow(string fProcessInstanceId, int iLevel)
        {
            EaProcessInstance epI = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epI == null)
            {
                return null;
            }
            EaSubFlow currSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "FOrder", "FId='" + epI.FSubFlowId + "'");
            if (currSub == null || currSub.FIsEnd == 1)  //FIsEnd 1 流程结束
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("forder>" + currSub.FOrder + " and FProcessId='" + epI.FProcessId + "' and Flevel=" + iLevel + " order by forder");
            EaSubFlow nextSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", sb.ToString());
            return nextSub;
        }
        #endregion

        #region 得到当前流程的最后一个子流程
        /// <summary>
        /// 得到当前流程的最后一个子流程
        /// </summary>
        /// <param name="fProcessId">流程实例Id</param>
        /// <returns>EaSubFlow 子流程对象</returns>
        public EaSubFlow GetLastSubFlow(string fProcessInstanceId)
        {
            EaProcessInstance epI = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epI == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(" FProcessId='" + epI.FProcessId + "' order by forder desc");
            EaSubFlow nextSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", sb.ToString());
            return nextSub;
        }
        #endregion




        #region 勘察设计
        public bool EntStartProcessKCSJ(string fBaseInfoId, string fAppId, string fYear, string fMonth, string FSystemId, string FManageDeptId, string fnumber, SortedList[] sDeatil)
        {
            StringBuilder sb = new StringBuilder();

            if (fnumber == null || fnumber == "") 
            {
                return false;
            }

            sb.Remove(0, sb.Length);
            sb.Append("select FCompany FName from cf_Sys_User u inner join CF_Sys_UserRight r on u.FId=r.FUserId where ");
            sb.Append(" r.fbaseInfoid='" + fBaseInfoId + "'");
            DataTable dtemp1 = rCenter.GetTable(sb.ToString());
            if (dtemp1.Rows.Count <= 0)
                return false;

            string sBarCode = GetBarCode(fnumber, FSystemId);
            if (sBarCode == null || sBarCode == "")
            {
                return false;
            }

            EsManageDept ManageDept = rCenter.GetEBase(EntityTypeEnum.EsManageDept, "fistown,FLevel", "fnumber='" + fnumber + "'") as EsManageDept;
            //提取是否扩权县
            string isTown = ManageDept.FIsTown;

            int iCount = sDeatil.Length;
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            SortedList[] sl = new SortedList[iCount];
            string[] fkey = new string[iCount];
            SaveOptionEnum[] so = new SaveOptionEnum[iCount];


            EntityTypeEnum[] en1 = new EntityTypeEnum[iCount];
            SortedList[] sl1 = new SortedList[iCount];
            string[] fkey1 = new string[iCount];
            SaveOptionEnum[] so1 = new SaveOptionEnum[iCount];

            string fEntName = dtemp1.Rows[0]["FName"].ToString();
            DataRow row = null;
            EaProcess ep = null;
            EaSubFlow es = null;
            for (int i = 0; i < iCount; i++)
            {
                en[i] = EntityTypeEnum.EaProcessInstance;
                sl[i] = new SortedList();
                fkey[i] = "FID";
                so[i] = SaveOptionEnum.Insert;


                en1[i] = EntityTypeEnum.EaProcessRecord;
                sl1[i] = new SortedList();
                fkey1[i] = "FID";
                so1[i] = SaveOptionEnum.Insert;

                if (isTown == "1")
                {
                    //扩权县判断
                    ep = this.GetProcess(FSystemId, sDeatil[i]["FManageTypeId"].ToString(), sDeatil[i]["FLevelId"].ToString(), sDeatil[i]["FTypeId"].ToString(), "700101");
                    if (ep == null)
                    {
                        ep = this.GetProcess(FSystemId, sDeatil[i]["FManageTypeId"].ToString(), sDeatil[i]["FLevelId"].ToString(), sDeatil[i]["FTypeId"].ToString(), FManageDeptId);
                        if (ep == null)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    ep = this.GetProcess(FSystemId, sDeatil[i]["FManageTypeId"].ToString(), sDeatil[i]["FLevelId"].ToString(), sDeatil[i]["FTypeId"].ToString(), FManageDeptId);
                }

                if (ep == null)
                {
                    return false;
                }

                es = this.GetStartSubFlow(ep.FId, fBaseInfoId, sDeatil[i]["FUpDept"].ToString());
                if (es == null)
                {
                    return false;
                }
                string fCurStepID = GetCurrDeptYYXM(fBaseInfoId, es.FId, fAppId);
                if (fCurStepID == null || fCurStepID == "")
                {
                    return false;
                }
                string fManageDeptId = fCurStepID;

                sl[i].Add("FID", System.Guid.NewGuid().ToString());
                sl[i].Add("FBaseInfoId", fBaseInfoId);
                sl[i].Add("FLinkId", fAppId);
                sl[i].Add("FEntName", fEntName);
                sl[i].Add("FIsPrime", sDeatil[i]["FIsPrime"].ToString());
                sl[i].Add("FListId", sDeatil[i]["FListId"].ToString());
                sl[i].Add("FLevelId", sDeatil[i]["FLevelId"].ToString());
                sl[i].Add("FTypeId", sDeatil[i]["FTypeId"].ToString());
                sl[i].Add("FEmpName", sDeatil[i]["FEmpName"].ToString());
                sl[i].Add("FLeadId", sDeatil[i]["FLeadId"].ToString());
                sl[i].Add("FEmpId", sDeatil[i]["FEmpId"].ToString());
                sl[i].Add("FLeadName", sDeatil[i]["FLeadName"].ToString());



                //施工企业资质是否暂定
                if (sDeatil[i]["FIsTemp"] != null)
                {
                    sl[i].Add("FIsTemp", sDeatil[i]["FIsTemp"].ToString());
                }

                sl[i].Add("FIsNew", sDeatil[i]["FIsNew"].ToString());
                sl[i].Add("FManageTypeId", sDeatil[i]["FManageTypeId"].ToString());
                sl[i].Add("FManageDeptId", fCurStepID);
                sl[i].Add("FProcessId", ep.FId);
                sl[i].Add("FSubFlowId", es.FId);
                sl[i].Add("FYear", fYear);
                sl[i].Add("FMonth", fMonth);
                sl[i].Add("FSubmitDate", DateTime.Now);
                sl[i].Add("FReportDate", DateTime.Now);
                sl[i].Add("FCurStepID", fCurStepID);
                sl[i].Add("FRoleId", es.FRoleId);
                sl[i].Add("FBeginRoleId", es.FRoleId);

                sl[i].Add("FDefineDay", es.FDefineDay);
                sl[i].Add("FState", 1);
                sl[i].Add("FAppState", 1);
                sl[i].Add("FCreateTime", DateTime.Now);
                sl[i].Add("FIsDeleted", 0);
                sl[i].Add("FSystemId", FSystemId);
                sl[i].Add("FResult", "未审批完成"); //终审结果
                sl[i].Add("FBarCode", sBarCode);

                sl1[i].Add("FID", System.Guid.NewGuid().ToString());
                sl1[i].Add("FProcessInstanceID", sl[i]["FID"].ToString());
                sl1[i].Add("FLinkId", fAppId);
                sl1[i].Add("FRoleDesc", es.FName);
                sl1[i].Add("FMeasure", 0);
                sl1[i].Add("FReportTime", DateTime.Now);
                sl1[i].Add("FRoleId", es.FRoleId);
                sl1[i].Add("FSubFlowId", es.FId);
                sl1[i].Add("FIsQuali", es.FIsQuali);
                sl1[i].Add("FIsPrint", es.FIsPrint);
                //sl[i].Add("FRoleDesc", es.FName);
                sl1[i].Add("FManageDeptId", fCurStepID);
                sl1[i].Add("FDefineDay", es.FDefineDay);
                sl1[i].Add("FTypeId", es.FTypeId);
                sl1[i].Add("FOrder", es.FOrder);
                sl1[i].Add("FLevel", es.FLevel);
                sl1[i].Add("FIsDeleted", 0);
            }
            try
            {
                sb.Remove(0, sb.Length);
                sb.Append("delete from CF_App_ProcessInstance where flinkid='" + fAppId + "';delete from CF_App_ProcessRecord where flinkid='" + fAppId + "'");
                rCenter.PExcute(sb.ToString(), true);

                if (rCenter.SaveEBaseM(en, sl, fkey, so))
                {
                    if (rCenter.SaveEBaseM(en1, sl1, fkey1, so1))
                    {
                        sb.Remove(0, sb.Length);

                        EntityTypeEnum qEn = EntityTypeEnum.EqList;
                        SortedList qSl = new SortedList();
                        string qFKey = "FID";
                        SaveOptionEnum qSo = SaveOptionEnum.Update;

                        qSl = new SortedList();
                        qSl.Add("FID", fAppId);
                        qSl.Add("FBaseName", fEntName);
                        qSl.Add("FReportDate", DateTime.Now);
                        qSl.Add("FYear", fYear);
                        qSl.Add("FState", 1);

                        rCenter.SaveEBase(qEn, qSl, qFKey, qSo);

                        StartKCSJAppDetail(sBarCode, fBaseInfoId, fAppId, FSystemId);

                        return true;
                    }
                }


            }
            catch (Exception ex)
            {
                for (int j = 0; j < sl.Length; j++)
                {
                    rCenter.DelEBase(EntityTypeEnum.EaProcessInstance, "FId='" + sl[j]["FID"].ToString() + "'", true);
                    rCenter.DelEBase(EntityTypeEnum.EaProcessRecord, "FId='" + sl1[j]["FID"].ToString() + "'", true);
                }
                return false;
            }
            return false;

        }
        #endregion


        /// <summary>
        /// 判断当前实例和当前步骤的子流程id是否能对应上
        /// 能对应，才能进行审批
        /// </summary>
        /// <param name="fprocessSub"></param>
        /// <param name="frecordSub"></param>
        /// <returns></returns>
        bool IsProcessIsTB(string fprocessSub, string frecordId)
        {
            string frecordSub = rCenter.GetSignValue("select FSubFlowId from cf_App_ProcessRecord where fid='" + frecordId + "'");
            return fprocessSub == frecordSub;
        }




        #region 打回到企业
        /// <summary>
        /// 打回到企业
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <param name="epr">审批记录对象</param>
        /// <returns></returns> 
        public bool BackProcessToEnt(string fProcessInstanceId, EaProcessRecord epr)
        {
            if (epr == null)
            {
                return false;
            }
            EaProcessInstance epi = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epi == null)
            {
                return false;
            }

            EntityTypeEnum[] en = null;
            string[] fkey = null;
            SortedList[] sl = null;
            SaveOptionEnum[] so = null;

            en = new EntityTypeEnum[2];
            fkey = new string[2];
            sl = new SortedList[2];
            so = new SaveOptionEnum[2];

            en[0] = EntityTypeEnum.EaProcessInstance;
            en[1] = EntityTypeEnum.EaProcessRecord;

            fkey[0] = "FID";
            fkey[1] = "FID";

            so[0] = SaveOptionEnum.Update;
            so[1] = SaveOptionEnum.Update;

            sl[0] = new SortedList();
            sl[0].Add("FID", fProcessInstanceId);
            sl[0].Add("FAppState", 2);
            sl[0].Add("FState", 2);
            sl[0].Add("FResult", "打回企业");

            sl[1] = new SortedList();
            sl[1].Add("FID", epr.FId);
            sl[1].Add("FMeasure", epr.FMeasure);
            sl[1].Add("FResult", epr.FResult);
            sl[1].Add("FIdea", epr.FIdea);
            sl[1].Add("FAppPerson", epr.FAppPerson);
            sl[1].Add("FCompany", epr.FCompany);
            sl[1].Add("FFunction", epr.FFunction);
            sl[1].Add("FManageDeptId", epr.FManageDeptId);
            sl[1].Add("FAppTime", epr.FAppTime);
            sl[1].Add("FUserId", epr.FUserId);
            sl[1].Add("FWaiteTime", epr.FWaiteTime);
            sl[1].Add("FLevel", epr.FLevel);

            return rCenter.SaveEBaseM(en, sl, fkey, so);
        }
        #endregion

        #region 打回到下一级
        /// <summary>
        /// 打回到下一级
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <param name="epr">审批记录对象</param>
        /// <returns></returns> 
        public bool BackProcessToSubGov(string fProcessInstanceId, EaProcessRecord epr)
        {
            if (epr == null)
            {
                return false;
            }
            EaProcessInstance epi = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epi == null)
            {
                return false;
            }
            EaSubFlow previousFlow = GetPreviousFlow(fProcessInstanceId);
            if (previousFlow == null)
            {
                return false;
            }

            EntityTypeEnum[] en = null;
            string[] fkey = null;
            SortedList[] sl = null;
            SaveOptionEnum[] so = null;

            en = new EntityTypeEnum[2];
            fkey = new string[2];
            sl = new SortedList[2];
            so = new SaveOptionEnum[2];

            en[0] = EntityTypeEnum.EaProcessInstance;
            en[1] = EntityTypeEnum.EaProcessRecord;

            fkey[0] = "FID";
            fkey[1] = "FID";

            so[0] = SaveOptionEnum.Update;
            so[1] = SaveOptionEnum.Update;

            string currDept = GetCurrDept(epi.FBaseInfoId, previousFlow.FId);
            sl[0] = new SortedList();
            sl[0].Add("FID", fProcessInstanceId);
            sl[0].Add("FAppState", 3);
            sl[0].Add("FState", 3);
            sl[0].Add("FCurStepID", currDept);
            sl[0].Add("FRoleId", previousFlow.FRoleId);
            sl[0].Add("FDefineDay", previousFlow.FDefineDay);
            sl[0].Add("FSubFlowId", previousFlow.FId);
            sl[0].Add("FBackIdea", epr.FIdea);



            sl[1] = new SortedList();
            sl[1].Add("FID", epr.FId);
            sl[1].Add("FMeasure", epr.FMeasure);
            sl[1].Add("FResult", epr.FResult);
            sl[1].Add("FIdea", epr.FIdea);
            sl[1].Add("FAppPerson", epr.FAppPerson);
            sl[1].Add("FCompany", epr.FCompany);
            sl[1].Add("FFunction", epr.FFunction);
            sl[1].Add("FManageDeptId", epr.FManageDeptId);
            sl[1].Add("FAppTime", epr.FAppTime);
            sl[1].Add("FUserId", epr.FUserId);
            sl[1].Add("FWaiteTime", epr.FWaiteTime);
            sl[1].Add("FLevel", epr.FLevel);
            return rCenter.SaveEBaseM(en, sl, fkey, so);
        }
        #endregion

        #region 结束一个流程
        /// <summary>
        /// 结束一个流程
        /// </summary>
        /// <param name="fProcessInstanceId">流程实例Id</param>
        /// <param name="epr">审批记录对象</param>
        /// <returns></returns>
        public bool EndProcess(string fProcessInstanceId, EaProcessRecord epr)
        {
            if (epr == null)
            {
                return false;
            }
            EaProcessInstance epi = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + fProcessInstanceId + "'");
            if (epi == null)
            {
                return false;
            }


            EntityTypeEnum[] en = null;
            string[] fkey = null;
            SortedList[] sl = null;
            SaveOptionEnum[] so = null;

            en = new EntityTypeEnum[2];
            fkey = new string[2];
            sl = new SortedList[2];
            so = new SaveOptionEnum[2];

            en[0] = EntityTypeEnum.EaProcessInstance;
            en[1] = EntityTypeEnum.EaProcessRecord;

            fkey[0] = "FID";
            fkey[1] = "FID";

            so[0] = SaveOptionEnum.Update;
            so[1] = SaveOptionEnum.Update;

            sl[0] = new SortedList();
            sl[0].Add("FID", fProcessInstanceId);
            sl[0].Add("FAppState", 6);
            sl[0].Add("FState", 6);

            string fResult = "";
            switch (epr.FMeasure)
            {
                case 0:
                    fResult = "未审批";
                    break;
                case 2:
                    fResult = "同意，审批完成";
                    break;
                case 4:
                    fResult = "不同意，审批完成";
                    break;
                default:
                    fResult = "系统出错！！";
                    break;

            }
            sl[0].Add("FResult", fResult);

            sl[1] = new SortedList();
            sl[1].Add("FID", epr.FId);
            sl[1].Add("FMeasure", epr.FMeasure);
            sl[1].Add("FResult", epr.FResult);
            sl[1].Add("FIdea", epr.FIdea);
            sl[1].Add("FAppPerson", epr.FAppPerson);
            sl[1].Add("FCompany", epr.FCompany);
            sl[1].Add("FFunction", epr.FFunction);
            sl[1].Add("FAppTime", epr.FAppTime);
            sl[1].Add("FManageDeptId", epr.FManageDeptId);
            sl[1].Add("FUserId", epr.FUserId);
            sl[1].Add("FWaiteTime", epr.FWaiteTime);
            sl[1].Add("FLevel", epr.FLevel);
            return rCenter.SaveEBaseM(en, sl, fkey, so);
        }
        #endregion

        #region 得到一个流程实例的某一个字流程的审批记录
        /// <summary>
        /// 得到一个流程实例的某一个字流程的审批记录
        /// </summary>
        /// <param name="fProcessInstanceId"></param>
        /// <param name="fSubFlowId"></param>
        /// <returns></returns>
        public EaProcessRecord GetProcessRecord(string fProcessInstanceId, string fSubFlowId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("fProcessInstanceId='" + fProcessInstanceId + "' and fSubFlowId='" + fSubFlowId + "'");
            EaProcessRecord epr = (EaProcessRecord)rCenter.GetEBase(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
            return epr;
        }
        #endregion

        #region 得到所有审批结果
        public string getAppResult(string PFId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FProcessInstanceID='" + PFId + "'and fmeasure<>0 order by FAppTime ");
            DataTable dt = rCenter.GetTable(EntityTypeEnum.EaProcessRecord, "FId,FRoleDesc,FResult", sb.ToString());
            sb.Remove(0, sb.Length);

            if (dt.Rows.Count <= 0)
            {
                sb.Remove(0, sb.Length);
                sb.Append("FProcessInstanceID='" + PFId + "'and fmeasure<>0 order by FAppTime ");
                dt = rCenter.GetTable(EntityTypeEnum.EaProcessRecordBackup, "FId,FRoleDesc,FResult", sb.ToString());
                sb.Remove(0, sb.Length);
            }

            if (dt.Rows.Count > 0)
            {

                sb.Append("<table width='100%' height='100%' cellpadding='0' cellspacing='0' border='0' style='border:none 0px;'>");

                int iCount = dt.Rows.Count;
                for (int i = 0; i < iCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        sb.Append("<tr>");
                    }
                    sb.Append("<td style='border:none 0px;' nowrap=nowrap>");
                    sb.Append("<a href='../../Government/AppQualiInfo/ShowAppIdea.aspx?frid=" + dt.Rows[i]["FID"].ToString() + "'");
                    sb.Append("target='_blank' class='link5' title='查看审批意见'>");
                    sb.Append(dt.Rows[i]["FRoleDesc"].ToString() + "：");
                    switch (dt.Rows[i]["FResult"].ToString())
                    {
                        case "1":
                            sb.Append("同意");
                            break;
                        case "3":
                            sb.Append("不同意");
                            break;

                    }
                    sb.Append("</a>");
                    sb.Append("</td>");
                    if ((i + 1) % 2 == 0)
                    {
                        sb.Append("</tr>");
                    }
                    if ((i == dt.Rows.Count - 1) && i % 2 == 0)
                    {
                        sb.Append("</tr>");

                    }
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        #region 更新dbCenter
        public void UpdateEntBase(DataTable dt, ref EntityTypeEnum[] en, ref SortedList[] sl, ref string[] fkey, ref SaveOptionEnum[] so)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int rowLength = dt.Rows.Count;
            sl = new SortedList[rowLength];
            en = new EntityTypeEnum[rowLength];
            fkey = new string[rowLength];
            so = new SaveOptionEnum[rowLength];

            for (int i = 0; i < rowLength; i++)
            {
                en[i] = EntityTypeEnum.EbBaseInfo;
                fkey[i] = "FID";
                so[i] = SaveOptionEnum.Insert;
                sl[i] = new SortedList();

                string fBaseInfoId = rCenter.GetSignValue(EntityTypeEnum.EbBaseInfo, "FID", "FId='" + dt.Rows[i]["FId"].ToString().Trim() + "'");
                if (fBaseInfoId != null && fBaseInfoId != "")
                {
                    so[i] = SaveOptionEnum.Update;
                }
                else
                {

                    sl[i].Add("FCreateTime", DateTime.Now);
                }
                sl[i].Add("FIsDeleted", 0);

                int columnCount = dt.Columns.Count;
                for (int j = 0; j < columnCount; j++)
                {
                    sl[i].Add(dt.Columns[j].ColumnName.ToUpper(), dt.Rows[i][j].ToString());
                }
            }

        }

        public void UpdateEntOtherInfo(DataTable dt, ref EntityTypeEnum[] en, ref SortedList[] sl, ref string[] fkey, ref SaveOptionEnum[] so)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            int rowLength = dt.Rows.Count;

            StringBuilder sb = new StringBuilder();
            sb.Append("delete from CF_Ent_BaseInfoOther where FBaseInfoId in (");
            for (int j = 0; j < rowLength; j++)
            {
                if (j == 0)
                {
                    sb.Append("'" + dt.Rows[j]["FBaseInfoId"].ToString() + "'");
                }
                else
                {
                    sb.Append(",'" + dt.Rows[j]["FBaseInfoId"].ToString() + "'");
                }
            }
            sb.Append(")");
            rCenter.PExcute(sb.ToString());

            sl = new SortedList[rowLength];
            en = new EntityTypeEnum[rowLength];
            fkey = new string[rowLength];
            so = new SaveOptionEnum[rowLength];

            for (int i = 0; i < rowLength; i++)
            {
                en[i] = EntityTypeEnum.EbBaseInfoOther;
                fkey[i] = "FID";
                so[i] = SaveOptionEnum.Insert;
                sl[i] = new SortedList();
                sl[i].Add("FIsDeleted", 0);
                int columnCount = dt.Columns.Count;
                for (int j = 0; j < columnCount; j++)
                {
                    sl[i].Add(dt.Columns[j].ColumnName.ToUpper(), dt.Rows[i][j].ToString());
                }
                //sl[i].Add("FValidEnd", DateTime.MaxValue);
                //sl[i].Add("FValidBegin", DateTime.MinValue); 
            }
        }
        #endregion

        public void UpdateEntQualiCertiTrade(DataTable dt, ref EntityTypeEnum[] en, ref SortedList[] sl, ref string[] fkey, ref SaveOptionEnum[] so)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();

            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_Ent_QualiCerti where fbaseinfoid='" + dt.Rows[0]["fbaseinfoid"].ToString() + "'");
            sb.Append(" and FIsValid=1");

            string sCerti = rCenter.GetSignValue(sb.ToString());

            SortedList sl1 = new SortedList();
            EntityTypeEnum en1 = EntityTypeEnum.EbQualiCerti;
            SaveOptionEnum so1 = SaveOptionEnum.Update;

            if (sCerti == null || sCerti == "")
            {
                so1 = SaveOptionEnum.Insert;
                sCerti = Guid.NewGuid().ToString();
                sl1.Add("FID", sCerti);
                sl1.Add("FBaseInfoId", dt.Rows[0]["FBaseInfoId"].ToString());
                sl1.Add("FAppDeptId", "51");
                sl1.Add("FAppDeptName", "四川省");
                sl1.Add("FBeginTime", DateTime.Now);
                sl1.Add("FEndTime", DateTime.Now.AddYears(1));
                sl1.Add("FAppTime", DateTime.Now);
                sl1.Add("FIsValid", 1);
                sl1.Add("FIsDeleted", 0);

            }
            else
            {
                sl1.Add("FID", sCerti);
            }
            DataRow[] row = dt.Select(" FIsBase=1 ");
            if (row != null && row.Length > 0)
            {
                sl1.Add("FLevelId", row[0]["FLevelId"].ToString().Trim());
                sl1.Add("FLevelName", rCenter.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber='" + row[0]["FLevelId"].ToString().Trim() + "'"));

            }
            rCenter.SaveEBase(en1, sl1, "FID", so1);


            int rowLength = dt.Rows.Count;

            bool isUpdateOther = false;

            sl = new SortedList[rowLength];
            en = new EntityTypeEnum[rowLength];
            fkey = new string[rowLength];
            so = new SaveOptionEnum[rowLength];

            for (int i = 0; i < rowLength; i++)
            {

                en[i] = EntityTypeEnum.EbQualiCertiTrade;
                fkey[i] = "FID";
                so[i] = SaveOptionEnum.Insert;
                sl[i] = new SortedList();

                sb.Remove(0, sb.Length);
                sb.Append(" select fId from CF_Ent_QualiCertiTrade where ");
                sb.Append(" fbaseinfoid='" + dt.Rows[i]["fbaseinfoid"].ToString() + "' ");
                sb.Append(" and flistid='" + dt.Rows[i]["flistid"].ToString() + "' ");
                sb.Append(" and ftypeid='" + dt.Rows[i]["ftypeid"].ToString() + "' ");
                sb.Append(" and flevelid='" + dt.Rows[i]["flevelid"].ToString() + "' ");
                sb.Append(" and fstate=" + dt.Rows[i]["fstate"].ToString());

                string fId = rCenter.GetSignValue(sb.ToString());
                if (fId != null)
                {
                    so[i] = SaveOptionEnum.Update;
                    dt.Rows[i]["FID"] = fId;

                }

                if (dt.Rows[i]["FIsBase"].ToString() == "1")
                {
                    isUpdateOther = true;
                }
                dt.Rows[i]["FCertiId"] = sCerti;

                int columnCount = dt.Columns.Count;
                for (int j = 0; j < columnCount; j++)
                {

                    sl[i].Add(dt.Columns[j].ColumnName.ToUpper(), dt.Rows[i][j].ToString());

                }

            }

            if (isUpdateOther)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" update CF_Ent_QualiCertiTrade set FIsBase=0 ");
                sb.Append(" where fbaseinfoid='" + dt.Rows[0]["fbaseinfoid"].ToString() + "'");
                rCenter.PExcute(sb.ToString());
            }
        }



        /// <summary>
        /// 判断注册地区和经济类型
        /// 如果发生了变化，证号也得发生变化
        /// </summary>
        /// <param name="fRegistDeptId"></param>
        /// <param name="fEntTypeId"></param>
        string CreateNo(string fRegistDeptId, string fEntTypeId, string fCertiNo)
        {
            if (fCertiNo.Length < 14)
            {
                return "-1";
            }
            else
            {
                string fNewNo = fCertiNo;
                //企业类型
                StringBuilder sb = new StringBuilder();
                sb.Append("select FCertiNo from cf_sys_dic where ");
                sb.Append("fparentid=110000 and fnumber='" + fEntTypeId + "'");
                string strTemp = rCenter.GetSignValue(sb.ToString());
                if (!string.IsNullOrEmpty(strTemp))
                    fNewNo = fNewNo.Replace(fNewNo[4].ToString(), strTemp);
                //主管部门 2012-12-03
                if (!string.IsNullOrEmpty(fRegistDeptId))
                {
                    fRegistDeptId = fRegistDeptId.PadRight(6, '0');
                    fNewNo = fNewNo.Substring(0, 6) + fRegistDeptId + fNewNo.Substring(12);
                }
                else
                {
                    return "-1";
                }
                if (!(fCertiNo.Equals(fNewNo)))
                //如果发生了变化，则要生成新的证书号码
                {
                    fNewNo = fNewNo.Substring(0, 12);
                    sb.Remove(0, sb.Length);
                    sb.Append("select max(substring(FCertiNo,13,2)) from ");
                    sb.Append("CF_Ent_QualiCerti where fCertiNo ");
                    sb.Append("like '" + fNewNo + "%'");
                    string FMaxNo = rCenter.GetSignValue(sb.ToString());
                    if (string.IsNullOrEmpty(FMaxNo))
                        FMaxNo = "01";
                    else
                        FMaxNo = (EConvert.ToInt(FMaxNo) + 1).ToString().PadLeft(2, '0');
                    if (FMaxNo.Length > 2)
                    {
                        return "-1";
                    }
                    else
                        return fNewNo + FMaxNo;
                }
            }
            return string.Empty;
        }

        //是否有证书，如果没有证书，必须先就位主项资质
        private string isHaveCerti(string FBaseInfoId)
        {
            EbQualiCerti ec = (EbQualiCerti)rCenter.GetEBase(EntityTypeEnum.EbQualiCerti, "fid", "fbaseinfoid='" + FBaseInfoId + "' and fisdeleted=0 and FLevelId<>'105001'");
            if (ec != null)
                return ec.FId;
            else
                return "";
        }
        //是否有证书，如果没有证书，查询甲级证书
        private string isHaveCerti(string flevelId, string FBaseinfoId)
        {
            EbQualiCerti ec = (EbQualiCerti)rCenter.GetEBase(EntityTypeEnum.EbQualiCerti, "fid", "fbaseinfoid='" + FBaseinfoId + "' and fisdeleted=0 and FLevelId='" + flevelId + "'");
            if (ec != null)
                return ec.FId;
            else
                return "";
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="appProcessRecord"></param>
        public void BatchAppKCSJ(SortedList[] appProcessRecord)
        {
            if (appProcessRecord == null || appProcessRecord.Length == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            int iCount = appProcessRecord.Length;
            ArrayList aData = new ArrayList();
            ArrayList aKey = new ArrayList();
            ArrayList aEntity = new ArrayList();
            ArrayList aSaveOption = new ArrayList();
            ArrayList aLinkId = new ArrayList(); //存储需要更新数据的FlinkId 
            ArrayList aAppEndId = new ArrayList(); //存储有流程结束的FlinkId 
            EaProcessInstance ep = null; //流程实例
            EaSubFlow currSub = null; //当前子流程
            EaSubFlow nextSub = null; //当前流程的下一个子流程
            bool isEnd = false; //该流程是否结束
            for (int i = 0; i < iCount; i++)
            {
                isEnd = false;
                //获取对应的EaProcessInstance
                sb.Remove(0, sb.Length);
                sb.Append("fid='" + appProcessRecord[i]["FProcessInstanceId"].ToString() + "'");
                ep = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", sb.ToString());
                if (ep == null)
                {
                    continue;
                }
                DateTime fPlanTime = ep.FPlanTime;
                if (fPlanTime == null || fPlanTime == DateTime.MinValue)//如果为空，求出计划时间
                {
                    int dayTotal = EConvert.ToInt(rCenter.GetSignValue("select isnull(FDay,0) from cf_sys_ManageType where fnumber='" + ep.FManageTypeId + "' and fsystemId='" + ep.FSystemId + "'"));
                    fPlanTime = new RCommon().GetEndTime(DateTime.Now, dayTotal);
                }
                int fReportCount = EConvert.ToInt(rCenter.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + ep.FLinkId + "'"));//最大步骤
                fReportCount++;
                //当前子流程
                currSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + ep.FSubFlowId + "' ");
                if (currSub == null)
                {
                    continue;
                }

                if (currSub.FIsQuali == 1) //需要就位定级不能批量审核
                {
                    sb.Remove(0, sb.Length);
                    sb.Append(" select FIsQuali from CF_App_ProcessRecord  ");
                    sb.Append(" where FSubFLowId = '" + currSub.FId + "'");
                    sb.Append(" and FProcessInstanceID='" + ep.FId + "'");

                    string sIsQuali = rCenter.GetSignValue(sb.ToString());
                    if (sIsQuali != "3")
                    {
                        continue;
                    }
                }
                nextSub = GetNextSubFlow(ep.FId);
                //如果扩权县需要跳转
                if (appProcessRecord[i].Contains("FUpDeptLevel"))
                {
                    if (appProcessRecord[i]["FUpDeptLevel"].ToString() != "100")
                    {
                        nextSub = GetNextSubFlow(ep.FId, EConvert.ToInt(appProcessRecord[i]["FUpDeptLevel"]));
                    }
                }

                if (appProcessRecord[i].Contains("FIsQuali"))
                {
                    //如果已经就位定级 直接办结
                    if (appProcessRecord[i]["FIsQuali"].ToString() == "3")
                    {
                        currSub.FIsEnd = 1;
                    }
                }

                if (nextSub == null || currSub.FIsEnd == 1)
                {
                    isEnd = true;
                }

                if (isEnd) //流程结束
                {
                    EntityTypeEnum en = EntityTypeEnum.EaProcessInstance;
                    SortedList sl = new SortedList();
                    string fkey = "FID";
                    SaveOptionEnum so = SaveOptionEnum.Update;

                    sl.Add("FID", ep.FId);
                    sl.Add("FAppState", 6);
                    sl.Add("FState", 6);
                    sl.Add("FPlanTime", fPlanTime);//应该终审时间
                    //if (currSub.FTypeId == 5) //是审核结束
                    //{
                    sl.Add("FResult", appProcessRecord[i]["FResult"].ToString());
                    //}

                    if (currSub.FIsAppEnd == 1) //是审核结束
                    {
                        if (ep.FFactTime == null
                            || ep.FFactTime == EConvert.ToDateTime("1900-01-01 00:00:00.000")
                            || EConvert.ToDateTime(ep.FFactTime) == DateTime.MinValue)
                        {
                            sl.Add("FFactTime", DateTime.Now.ToShortDateString());

                            SortedList tempList = new SortedList();
                            tempList.Add("FID", ep.FLinkId);
                            tempList.Add("FIsAppEnd", 1);
                            tempList.Add("FAppEndTime", DateTime.Now.ToShortDateString());


                            aData.Add(tempList);
                            aKey.Add("FID");
                            aEntity.Add(EntityTypeEnum.EaAcceptBook);
                            aSaveOption.Add(SaveOptionEnum.Update);
                        }
                    }

                    aData.Add(sl);
                    aKey.Add(fkey);
                    aEntity.Add(en);
                    aSaveOption.Add(so);
                    en = EntityTypeEnum.EaProcessRecord;
                    sl = appProcessRecord[i];
                    //sl.Add("FTypeId", currSub.FTypeId);
                    //sl.Add("");
                    fkey = "FID";
                    so = SaveOptionEnum.Update;

                    aData.Add(sl);
                    aKey.Add(fkey);
                    aEntity.Add(en);
                    aSaveOption.Add(so);
                    if (appProcessRecord[i]["FResult"].ToString() == "1") //审核结束并且同意
                    {
                        if (!aLinkId.Contains(ep.FLinkId))
                        {
                            aLinkId.Add(ep.FLinkId);
                        }
                    }
                    if (!aAppEndId.Contains(ep.FLinkId))
                    {
                        aAppEndId.Add(ep.FLinkId);
                    }
                }
                else
                {
                    //继续上报
                    EntityTypeEnum en = EntityTypeEnum.EaProcessInstance;
                    SortedList sl = new SortedList();
                    string fkey = "FID";
                    SaveOptionEnum so = SaveOptionEnum.Update;

                    string currDept = string.Empty;
                    currDept = GetCurrDept(ep.FLinkId, ep.FBaseInfoId, nextSub.FId);
                    sl.Add("FID", ep.FId);
                    sl.Add("FAppState", 0); //上报;
                    sl.Add("FState", 1);
                    sl.Add("FSubFlowId", nextSub.FId);
                    //sl.Add("FReportDate", DateTime.Now);
                    sl.Add("FCurStepID", currDept);
                    sl.Add("FRoleId", nextSub.FRoleId);
                    sl.Add("FDefineDay", nextSub.FDefineDay);
                    sl.Add("FPlanTime", fPlanTime);//应该终审时间
                    if (currSub.FIsAppEnd == 1) //是审核结束
                    {
                        if (ep.FFactTime == null
                            || ep.FFactTime == EConvert.ToDateTime("1900-01-01 00:00:00.000"))
                        {
                            sl.Add("FFactTime", DateTime.Now.ToShortDateString());

                            SortedList tempList = new SortedList();
                            tempList.Add("FID", ep.FLinkId);
                            tempList.Add("FIsAppEnd", 1);
                            tempList.Add("FAppEndTime", DateTime.Now.ToShortDateString());

                            aData.Add(tempList);
                            aKey.Add("FID");
                            aEntity.Add(EntityTypeEnum.EaAcceptBook);
                            aSaveOption.Add(SaveOptionEnum.Update);
                        }
                    }
                    //if (currSub.FTypeId == 5) //是审核结束
                    //{
                    sl.Add("FResult", appProcessRecord[i]["FResult"].ToString());
                    //} 
                    aData.Add(sl);
                    aKey.Add(fkey);
                    aEntity.Add(en);
                    aSaveOption.Add(so);

                    en = EntityTypeEnum.EaProcessRecord;
                    sl = new SortedList();
                    fkey = "FID";
                    so = SaveOptionEnum.Insert;
                    sl.Add("FID", Guid.NewGuid().ToString());
                    sl.Add("FProcessInstanceId", ep.FId);
                    sl.Add("FManageDeptId", currDept);
                    sl.Add("FLinkId", ep.FLinkId);
                    sl.Add("FSubFlowId", nextSub.FId);
                    sl.Add("FMeasure", 0);
                    sl.Add("FRoleId", nextSub.FRoleId);
                    sl.Add("FRoleDesc", nextSub.FName);
                    sl.Add("FTypeId", nextSub.FTypeId);
                    sl.Add("FIsPrint", nextSub.FIsPrint);
                    sl.Add("FIsQuali", nextSub.FIsQuali);
                    sl.Add("FOrder", nextSub.FOrder);
                    sl.Add("FIsdeleted", 0);
                    sl.Add("FReporttime", DateTime.Now);
                    sl.Add("FDefineDay", nextSub.FDefineDay);
                    sl.Add("FLevel", nextSub.FLevel);
                    sl.Add("FReportCount", fReportCount);//审核步骤

                    //继承上级审核结果
                    if (appProcessRecord[i].Contains("FResult"))
                    {
                        sl.Add("FResult", appProcessRecord[i]["FResult"].ToString());
                    }
                    if (appProcessRecord[i].Contains("FPersonnel"))
                    {
                        sl.Add("FPersonnel", appProcessRecord[i]["FPersonnel"].ToString());
                    }
                    if (appProcessRecord[i].Contains("FPerformance"))
                    {
                        sl.Add("FPerformance", appProcessRecord[i]["FPerformance"].ToString());
                    }

                    aData.Add(sl);
                    aKey.Add(fkey);
                    aEntity.Add(en);
                    aSaveOption.Add(so);

                    en = EntityTypeEnum.EaProcessRecord;
                    sl = appProcessRecord[i];
                    fkey = "FID";
                    so = SaveOptionEnum.Update;

                    aData.Add(sl);
                    aKey.Add(fkey);
                    aEntity.Add(en);
                    aSaveOption.Add(so);
                }
            }

            int iSaveCount = aKey.Count;
            EntityTypeEnum[] ens = new EntityTypeEnum[iSaveCount];
            string[] skeys = new string[iSaveCount];
            SortedList[] slups = new SortedList[iSaveCount];
            SaveOptionEnum[] sus = new SaveOptionEnum[iSaveCount];
            for (int i = 0; i < iSaveCount; i++)
            {
                ens[i] = (EntityTypeEnum)aEntity[i];
                skeys[i] = aKey[i].ToString();
                slups[i] = (SortedList)aData[i];
                sus[i] = (SaveOptionEnum)aSaveOption[i];
            }
            rCenter.SaveEBaseM(ens, slups, skeys, sus);

            iCount = appProcessRecord.Length;
            for (int i = 0; i < iCount; i++)
            {
                sb.Remove(0, sb.Length);
                sb.Append("fid='" + appProcessRecord[i]["FProcessInstanceId"].ToString() + "'");

                EaProcessInstance pi = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "FState,FId", sb.ToString());
                if (pi.FState == 6)
                {
                    BackUpProcessInstance(pi.FId);
                }
            }

            aEntity.Clear();
            aData.Clear();
            aKey.Clear();
            aSaveOption.Clear();

            aEntity.Clear();
            aData.Clear();
            aKey.Clear();
            aSaveOption.Clear();

            ArrayList aEntity1 = new ArrayList();
            ArrayList aData1 = new ArrayList();
            ArrayList aKey1 = new ArrayList();
            ArrayList aSaveOption1 = new ArrayList();

            iCount = aAppEndId.Count;
            for (int t = 0; t < iCount; t++)
            {
                string fbaseInfoId = string.Empty;
                sb.Remove(0, sb.Length);
                sb.Append(" select count(fstate) from CF_App_ProcessInstance ");
                sb.Append(" where flinkid='" + aAppEndId[t].ToString() + "'");
                sb.Append(" and fstate<>6 and fstate<>2 ");

                if (rCenter.GetSQLCount(sb.ToString()) == 0)
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", aAppEndId[t].ToString());
                    sl.Add("FState", 6);


                    aEntity.Add(EntityTypeEnum.EqList);
                    aKey.Add("FID");
                    aSaveOption.Add(SaveOptionEnum.Update);
                    aData.Add(sl);

                    sl = new SortedList();
                    sb.Remove(0, sb.Length);
                    sb.Append("FLinkId='" + aAppEndId[t].ToString() + "'");
                    EaAcceptBook eaBook = (EaAcceptBook)rCenter.GetEBase(EntityTypeEnum.EaAcceptBook, "FID", sb.ToString());

                    if (eaBook != null)
                    {
                        sl.Add("FID", eaBook.FId);
                        sl.Add("FIsEnd", 1);
                        sl.Add("FState", 9);
                        sl.Add("FEndTime", DateTime.Now);

                        aEntity1.Add(EntityTypeEnum.EaAcceptBook);
                        aKey1.Add("FID");
                        aSaveOption1.Add(SaveOptionEnum.Update);
                        aData1.Add(sl);
                    }
                }
                iSaveCount = aKey.Count;
                ens = new EntityTypeEnum[iSaveCount];
                skeys = new string[iSaveCount];
                slups = new SortedList[iSaveCount];
                sus = new SaveOptionEnum[iSaveCount];
                for (int i = 0; i < iSaveCount; i++)
                {
                    ens[i] = (EntityTypeEnum)aEntity[i];
                    skeys[i] = aKey[i].ToString();
                    slups[i] = (SortedList)aData[i];
                    sus[i] = (SaveOptionEnum)aSaveOption[i];
                }
                rCenter.SaveEBaseM(ens, slups, skeys, sus);


                iSaveCount = aKey1.Count;
                ens = new EntityTypeEnum[iSaveCount];
                skeys = new string[iSaveCount];
                slups = new SortedList[iSaveCount];
                sus = new SaveOptionEnum[iSaveCount];
                for (int i = 0; i < iSaveCount; i++)
                {
                    ens[i] = (EntityTypeEnum)aEntity1[i];
                    skeys[i] = aKey1[i].ToString();
                    slups[i] = (SortedList)aData1[i];
                    sus[i] = (SaveOptionEnum)aSaveOption1[i];
                }
                if (rCenter.SaveEBaseM(ens, slups, skeys, sus))
                {
                }
            }
        }
        //#region 企业批量打回
        //public void BatchBack(ArrayList fProcessInstanceID, string fIdea)
        //{
        //    fIdea = fIdea.Length > 500 ? fIdea.Substring(0, 500) : fIdea;
        //    StringBuilder sb = new StringBuilder();
        //    int iCount = fProcessInstanceID.Count;

        //    ArrayList arrEn = new ArrayList();
        //    ArrayList arrSo = new ArrayList();
        //    ArrayList arrKey = new ArrayList();
        //    ArrayList arrSl = new ArrayList();

        //    ArrayList aLinkId = new ArrayList();

        //    for (int i = 0; i < iCount; i++)
        //    {
        //        sb.Remove(0, sb.Length);
        //        sb.Append(" select flinkid from CF_App_ProcessInstance ");
        //        sb.Append(" where fid='" + fProcessInstanceID[i].ToString() + "'");

        //        string fLinkId = rCenter.GetSignValue(sb.ToString());
        //        if (!aLinkId.Contains(fLinkId))
        //        {
        //            aLinkId.Add(fLinkId);
        //        }
        //    }

        //    iCount = aLinkId.Count;

        //    sb.Remove(0, sb.Length);
        //    sb.Append(" select fid from CF_App_ProcessInstance ");
        //    sb.Append(" where fstate<>6 ");
        //    sb.Append(" and  flinkid in (");
        //    for (int j = 0; j < iCount; j++)
        //    {
        //        if (j == 0)
        //        {
        //            sb.Append("'" + aLinkId[j].ToString() + "'");
        //        }
        //        else
        //        {
        //            sb.Append(",'" + aLinkId[j].ToString() + "'");
        //        }

        //    }
        //    sb.Append(") ");

        //    DataTable dt = rCenter.GetTable(sb.ToString());

        //    iCount = dt.Rows.Count;

        //    for (int r = 0; r < iCount; r++)
        //    {
        //        SortedList sl = new SortedList();

        //        sl.Add("FID", dt.Rows[r]["FID"].ToString());
        //        sl.Add("FState", 2);
        //        sl.Add("FAppState", 2);
        //        sl.Add("FResult", "2");
        //        sl.Add("FBackIdea", fIdea);
        //        sl.Add("FFactTime", DateTime.Now.ToShortDateString());

        //        arrSl.Add(sl);
        //        arrSo.Add(SaveOptionEnum.Update);
        //        arrKey.Add("FID");
        //        arrEn.Add(EntityTypeEnum.EaProcessInstance);

        //    }

        //    iCount = aLinkId.Count;
        //    for (int w = 0; w < iCount; w++)
        //    {
        //        sb.Remove(0, sb.Length);
        //        sb.Append(" select fid from CF_App_AcceptBook ");
        //        sb.Append(" where flinkid='" + aLinkId[w].ToString() + "'");

        //        string fId = rCenter.GetSignValue(sb.ToString());
        //        if (fId != null && fId != "")
        //        {
        //            SortedList sl = new SortedList();
        //            sl.Add("FID", fId);
        //            sl.Add("FState", 11);
        //            sl.Add("FEndTime", DateTime.Now);


        //            arrSl.Add(sl);
        //            arrSo.Add(SaveOptionEnum.Update);
        //            arrKey.Add("FID");
        //            arrEn.Add(EntityTypeEnum.EaAcceptBook);
        //        }
        //    }

        //    iCount = arrSo.Count;

        //    EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
        //    SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
        //    string[] fkeys = new string[iCount];
        //    SortedList[] sls = new SortedList[iCount];

        //    for (int q = 0; q < iCount; q++)
        //    {
        //        ens[q] = (EntityTypeEnum)arrEn[q];
        //        sos[q] = (SaveOptionEnum)arrSo[q];
        //        fkeys[q] = (string)arrKey[q];
        //        sls[q] = new SortedList();
        //        sls[q] = (SortedList)arrSl[q];

        //    }
        //    rCenter.SaveEBaseM(ens, sls, fkeys, sos);


        //    iCount = aLinkId.Count;
        //    ens = new EntityTypeEnum[iCount];
        //    fkeys = new string[iCount];
        //    sls = new SortedList[iCount];
        //    sos = new SaveOptionEnum[iCount];

        //    for (int t = 0; t < iCount; t++)
        //    {
        //        ens[t] = EntityTypeEnum.EqList;
        //        fkeys[t] = "FID";
        //        sos[t] = SaveOptionEnum.Update;
        //        sls[t] = new SortedList();
        //        sls[t].Add("FID", aLinkId[t].ToString());
        //        sls[t].Add("FState", 2);
        //        sls[t].Add("fresult", fIdea);



        //    }
        //    rQuali.SaveEBaseM(ens, sls, fkeys, sos);
        //    try
        //    {
        //        for (int i = 0; i < fProcessInstanceID.Count; i++)
        //        {
        //            EaProcessInstance ep = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "*", "FId='" + fProcessInstanceID[i] + "'");
        //            if (ep != null)
        //            {
        //                string R_FManageTypeId = ep.FManageTypeId;
        //                string FBaseInfoId = ep.FBaseInfoId;
        //                string R_FAppId = ep.FId;
        //                string R_FTime = DateTime.Now.ToString();
        //                string R_BZ = "";

        //                string R_Matter = "办结";
        //                string R_Back_Matter = EConvert.ToString(fIdea); ;
        //                string R_Result = "1";
        //                string R_Qcfid = "";
        //                //上报电子监察
        //                string sValue = EConvert.ToString(System.Configuration.ConfigurationSettings.AppSettings["StartJC"]);
        //                string fDeptNumber = EConvert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DefaultDept"]);
        //                string DFId = EConvert.ToString(HttpContext.Current.Session["DFId"]);
        //                if (sValue == "1")
        //                {
        //                    if (DFId == fDeptNumber)//省上的才接进
        //                    {
        //                        if (R_Result == "0")
        //                        {
        //                            sb.Remove(0, sb.Length);
        //                            sb.Append(" select top 1 FCertiNo ");
        //                            sb.Append(" from CF_Ent_QualiCerti");
        //                            sb.Append(" where FBaseInfoId='" + FBaseInfoId + "' order by fapptime desc  ");
        //                        }
        //                        RCenter rc = new RCenter();
        //                        R_Qcfid = rc.GetSignValue(sb.ToString());
        //                        DataSubmit dst = new DataSubmit();

        //                        if (!dst.ToEnd(R_FManageTypeId, R_FAppId, R_FTime, R_BZ, R_Result, R_Matter, R_Back_Matter, R_Qcfid))
        //                        {
        //                            //tool.showMessage(dsb.ErrorString);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DataLog.Write(LogType.Error, LogSort.System, "发送电子监察失败", ex.ToString());
        //    }
        //}
        //#endregion
        #region 打回企业 龚成龙 2011-02-11
        public bool BatchBack(DataTable dt, string FLinkId, string backIdea)
        {
            backIdea = backIdea.Length > 500 ? backIdea.Substring(0, 500) : backIdea;
            int iCount = dt.Rows.Count;
            if (iCount > 0)
            {
                ArrayList arrEn = new ArrayList();
                ArrayList arrSl = new ArrayList();
                ArrayList arrKey = new ArrayList();
                ArrayList arrSo = new ArrayList();
                for (int i = 0; i < iCount; i++)
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", dt.Rows[i]["FID"].ToString());
                    sl.Add("FState", 2);
                    sl.Add("FBackIdea", backIdea);
                    sl.Add("FResult", "2");
                    sl.Add("FFactTime", DateTime.Now.ToShortDateString());
                    //打回类型 1：补正上报 2：补正打回
                    if (dt.Columns.Contains("FAppLevel"))
                        sl.Add("FAppLevel", dt.Rows[i]["FAppLevel"].ToString());
                    arrEn.Add(EntityTypeEnum.EaProcessInstance);
                    arrSl.Add(sl);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);

                    if (dt.Columns.Contains("erFId"))
                    {
                        sl = new SortedList();
                        sl.Add("FID", dt.Rows[i]["erFId"].ToString());
                        sl.Add("FUserId", dt.Rows[i]["FUserId"].ToString());
                        sl.Add("FMeasure", 5);//已经办理
                        if (dt.Columns.Contains("FAppPerson"))
                            sl.Add("FAppPerson", dt.Rows[i]["FAppPerson"].ToString());
                        if (dt.Columns.Contains("FAppPersonUnit"))
                            sl.Add("FCompany", dt.Rows[i]["FAppPersonUnit"].ToString());
                        if (dt.Columns.Contains("FAppPersonJob"))
                            sl.Add("FFunction", dt.Rows[i]["FAppPersonJob"].ToString());
                        if (dt.Columns.Contains("FAppTime"))
                            sl.Add("FAppTime", dt.Rows[i]["FAppTime"].ToString());
                        sl.Add("FResult", "2");
                        sl.Add("FAppTime", DateTime.Now);
                        sl.Add("FIdea", backIdea);

                        arrEn.Add(EntityTypeEnum.EaProcessRecord);
                        arrSl.Add(sl);
                        arrKey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Update);
                    }
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(" select fid from CF_App_AcceptBook ");
                sb.Append(" where flinkid = '" + FLinkId + "'");
                string fAcceptBookId = this.rCenter.GetSignValue(sb.ToString());
                if (fAcceptBookId != null && fAcceptBookId != "")
                {
                    SortedList sl = new SortedList();
                    sl.Add("FID", fAcceptBookId);
                    sl.Add("FState", 11);
                    arrEn.Add(EntityTypeEnum.EaAcceptBook);
                    arrSl.Add(sl);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Update);
                }

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
                this.rCenter.SaveEBaseM(ens, sls, fkeys, sos);


                SortedList slp = new SortedList();
                slp.Add("FID", FLinkId);
                slp.Add("FState", 2);
                slp.Add("fresult", backIdea);
                return rCenter.SaveEBase(EntityTypeEnum.EqList, slp, "FID", SaveOptionEnum.Update);
            }
            return false;
        }
        #endregion
        #region 批量结束
        public void BatchEnd(ArrayList fProcessInstanceID, string fAppIdea, string fAppResult)
        {

            int iCount = fProcessInstanceID.Count;
            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList arrKey = new ArrayList();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < iCount; i++)
            {

                SortedList sl = new SortedList();
                sl.Add("FID", fProcessInstanceID[i].ToString());
                sl.Add("FState", 6);
                sl.Add("FAppState", 6);
                sl.Add("FResult", fAppResult);
                sl.Add("FBackIdea", fAppIdea);
                sl.Add("FFactTime", DateTime.Now.ToShortDateString());

                arrEn.Add(EntityTypeEnum.EaProcessInstance);
                arrKey.Add("FID");
                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Update);

                if (i == 0)
                {
                    sb.Append("'" + fProcessInstanceID[i].ToString() + "'");
                }
                else
                {
                    sb.Append(",'" + fProcessInstanceID[i].ToString() + "'");
                }
            }
            if (sb.Length <= 0)
            {
                return;
            }
            DataTable dt = rCenter.GetTable(EntityTypeEnum.EaProcessInstance, "distinct(FLinkId),FManageTypeId", " fid in(" + sb.ToString() + ")");
            if (dt != null && dt.Rows.Count > 0)
            {
                int rowCount = dt.Rows.Count;
                EntityTypeEnum[] en1 = new EntityTypeEnum[rowCount];
                string[] fkey1 = new string[rowCount];
                SortedList[] sl1 = new SortedList[rowCount];
                SaveOptionEnum[] so1 = new SaveOptionEnum[rowCount];

                for (int j = 0; j < rowCount; j++)
                {
                    en1[j] = EntityTypeEnum.EqList;
                    fkey1[j] = "FID";
                    so1[j] = SaveOptionEnum.Update;
                    sl1[j] = new SortedList();
                    sl1[j].Add("FID", dt.Rows[j][0].ToString());
                    sl1[j].Add("FState", 6);
                    sl1[j].Add("fresult", fAppIdea);


                    string sId = rCenter.GetSignValue(EntityTypeEnum.EaAcceptBook, "FID", "FLinkId='" + dt.Rows[j][0].ToString() + "'");
                    if (!string.IsNullOrEmpty(sId))
                    {
                        SortedList sl = new SortedList();
                        sl.Add("FID", sId);
                        sl.Add("FState", 9);
                        sl.Add("FEndTime", DateTime.Now);


                        arrEn.Add(EntityTypeEnum.EaAcceptBook);
                        arrKey.Add("FID");
                        arrSl.Add(sl);
                        arrSo.Add(SaveOptionEnum.Update);
                    }
                }
                rCenter.SaveEBaseM(en1, sl1, fkey1, so1);
            }

            if (arrEn.Count > 0)
            {
                EntityTypeEnum[] ens = new EntityTypeEnum[arrEn.Count];
                string[] keys = new string[arrEn.Count];
                SortedList[] sls = new SortedList[arrEn.Count];
                SaveOptionEnum[] sos = new SaveOptionEnum[arrEn.Count];

                for (int k = 0; k < arrEn.Count; k++)
                {
                    ens[k] = (EntityTypeEnum)arrEn[k];
                    keys[k] = (string)arrKey[k];
                    sls[k] = new SortedList();
                    sls[k] = (SortedList)arrSl[k];
                    sos[k] = (SaveOptionEnum)arrSo[k];
                }

                rCenter.SaveEBaseM(ens, sls, keys, sos);
            }
            //备份流程
            StringBuilder sPIDs = new StringBuilder();
            for (int i = 0; i < iCount; i++)
            {
                if (i == 0)
                {
                    sPIDs.Append("'" + fProcessInstanceID[i].ToString() + "'");
                }
                else
                {
                    sPIDs.Append(",'" + fProcessInstanceID[i].ToString() + "'");
                }
            }
            sb.Remove(0, sb.Length);
            sb.Append("insert into cf_App_ProcessInstanceBackup(FID, FIsDeleted, FTime, FCreateTime, FBaseInfoID, FEntName, FEmpId, FEmpName, FLinkId, FState, FIsBase, FIsTemp, FListId, FTypeId, FLevelId, FLeadId, FLeadName, FAppLevel, FProcessId, FSubFlowId, FManageDeptId, FManageTypeId, FResult, FYear, FMonth, FSubmitDate, FReportDate, FCurStepID, FRoleId, FBeginRoleId, FDefineDay, FAppState, FBackIdea, FSystemId, FIsNew, FSeeState, fseetime, FPlanTime, FFactTime, FBarCode, FReportCount) ");
            sb.Append("select  FID, FIsDeleted, FTime, FCreateTime, FBaseInfoID, FEntName, FEmpId, FEmpName, FLinkId, FState, FIsBase, FIsTemp, FListId, FTypeId, FLevelId, FLeadId, FLeadName, FAppLevel, FProcessId, FSubFlowId, FManageDeptId, FManageTypeId, FResult, FYear, FMonth, FSubmitDate, FReportDate, FCurStepID, FRoleId, FBeginRoleId, FDefineDay, FAppState, FBackIdea, FSystemId, FIsNew, FSeeState, fseetime, FPlanTime, FFactTime, FBarCode, FReportCount  from cf_App_ProcessInstance where fid in (" + sPIDs.ToString() + ");");

            //sb.Append("delete from cf_App_ProcessInstance where fid in (" + sPIDs.ToString() + ");"); //2014-1-16 不予受理不删除信息

            sb.Append("insert into cf_App_ProcessRecordBackup(FID, FTime, FIsDeleted, FProcessInstanceID, FLinkId, FSubFlowId, FMeasure, FResult, FIdea, FAppPerson, FCompany, FFunction, FManageDeptId, FAppTime, FReportTime, FUserId, FWaiteTime, FDefineDay, FRoleId, FLevel, FOrder, FRoleDesc, FTypeId, FIsQuali, FIsPrint, FCreateTime, FPersonnel, FPerformance, FUpDeptLevel, FReportCount) ");
            sb.Append("select  FID, FTime, FIsDeleted, FProcessInstanceID, FLinkId, FSubFlowId, FMeasure, FResult, FIdea, FAppPerson, FCompany, FFunction, FManageDeptId, FAppTime, FReportTime, FUserId, FWaiteTime, FDefineDay, FRoleId, FLevel, FOrder, FRoleDesc, FTypeId, FIsQuali, FIsPrint, FCreateTime, FPersonnel, FPerformance, FUpDeptLevel, FReportCount from cf_App_ProcessRecord where ");
            sb.Append(" FProcessInstanceID in (" + sPIDs.ToString() + ");");
            //sb.Append("delete from cf_App_ProcessRecord where ");
            //sb.Append(" FProcessInstanceID in (" + sPIDs.ToString() + ");");//2014-1-16 不予受理不删除信息
            rCenter.PExcute(sb.ToString(), true);
        }
        
        #endregion

        #region  得到上报部门
        public DataTable GetCanReportDept(string fBaseInfoId, string fProcessId)
        {
            string fCanReportDept = this.GetEntAllUpDept(fBaseInfoId);
            if (fCanReportDept == null || fCanReportDept == "")
            {
                return null;
            }
            string[] strs = fCanReportDept.Split(',');
            int iCount = strs.Length;
            StringBuilder sb = new StringBuilder();
            sb.Append(" select fname,fnumber from CF_Sys_ManageDept where FLevel in ");
            sb.Append(" (select FLevel from CF_App_SubFlow where FProcessId='" + fProcessId + "' and fisdeleted=0 and FLevel>0) ");
            sb.Append(" and fnumber in (");
            for (int i = 0; i < iCount; i++)
            {
                if (i == 0)
                {
                    sb.Append("'" + strs[i] + "'");
                }
                else
                {
                    sb.Append(",'" + strs[i] + "'");
                }
            }
            sb.Append(") and fclassnumber='102009' ");
            sb.Append(" order by FNumber desc ");
            return rCenter.GetTable(sb.ToString());

        }


        #endregion

        #region 自动预审（施工企业）













        #region 创建用于显示数据的Table
        public DataTable CreateAutoAppTable()
        {
            DataTable dt = new DataTable();

            DataColumn dc = new DataColumn("序号", typeof(int));
            dt.Columns.Add(dc);


            dc = new DataColumn("标准名称", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("标准类别", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("计量单位", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("关系", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("标准数量", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("企业数量", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("是否达标", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("是否必须", typeof(string));
            dt.Columns.Add(dc);

            dc = new DataColumn("FNumber", typeof(int));
            dt.Columns.Add(dc);

            return dt;

        }
        #endregion
        #endregion


        public bool GovReportProcessKCSJ(SortedList[] slData, string FBaseInfoId)
        {
            if (slData.Length <= 0)
            {
                return false;
            }
            int iCount = slData.Length;

            ArrayList aData = new ArrayList();
            ArrayList aKey = new ArrayList();
            ArrayList aEntity = new ArrayList();
            ArrayList aSaveOption = new ArrayList();

            bool isEnd = true;//是否全部审批完毕

            string fFactTime = "";//审批结束时间
            string l = "";
            for (int i = 0; i < iCount; i++)
            {
                string AppId = slData[i]["FProcessInstanceID"].ToString();
                EaProcessInstance ep = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + AppId + "'");
                l = ep.FLinkId;
                EaSubFlow currSub = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + ep.FSubFlowId + "' ");

                EaSubFlow nextSub = null;

                nextSub = GetNextSubFlow(AppId);


                //如果扩权县需要跳转
                if (slData[i].Contains("FUpDeptLevel"))
                {
                    if (slData[i]["FUpDeptLevel"].ToString() != "100")
                    {
                        nextSub = GetNextSubFlow(AppId, EConvert.ToInt(slData[i]["FUpDeptLevel"]));
                    }
                }


                if (slData[i].Contains("FIsQuali"))
                {
                    //如果已经就位定级 直接办结
                    if (slData[i]["FIsQuali"].ToString() == "3")
                    {
                        currSub.FIsEnd = 1;
                    }
                }
                int fReportCount = EConvert.ToInt(rCenter.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + l + "'"));
                //查询当期步骤的subFlowId和实例表的subFlowId 
                if (!IsProcessIsTB(ep.FSubFlowId, slData[i]["FID"].ToString()))
                    continue;
                if (nextSub == null || currSub.FIsEnd == 1) //如果是终审
                {
                    //取出审批记录中 所属审批阶段为终审的审批意见
                    SortedList sl0 = new SortedList();
                    sl0.Clear();
                    sl0.Add("FID", ep.FId);
                    sl0.Add("FState", 6);
                    sl0.Add("FAppState", 6);

                    if (currSub.FIsAppEnd == 1) //是审批结束
                    {
                        if (ep.FFactTime == null ||
                            ep.FFactTime == EConvert.ToDateTime("1900-01-01 00:00:00.000") ||
                            EConvert.ToDateTime(ep.FFactTime) == DateTime.MinValue)
                        {
                            fFactTime = DateTime.Now.ToShortDateString();
                            sl0.Add("FFactTime", DateTime.Now.ToShortDateString());
                        }
                    }
                    //if (currSub.FTypeId == 5) //是审批结束
                    //{
                    sl0.Add("FResult", slData[i]["FResult"].ToString());
                    //}
                    //
                    //如果是审批结束的话
                    aData.Add(sl0);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaProcessInstance);
                    aSaveOption.Add(SaveOptionEnum.Update);

                    //slData[i].Add("FTypeId", currSub.FTypeId); //所属审批阶段 1接件 3正常审批 5终审 7归档
                    aData.Add(slData[i]);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaProcessRecord);
                    aSaveOption.Add(SaveOptionEnum.Update);
                }
                else
                {
                    isEnd = false;
                    StringBuilder sb = new StringBuilder();
                    if (ep.FState == 3) //如果是打回的数据,先把上级的审批记录删除
                    {
                        EaProcessRecord eprGov = GetProcessRecord(ep.FId, nextSub.FId);
                        if (eprGov != null)
                        {
                            rCenter.DelEBase(EntityTypeEnum.EaProcessRecord, "FId='" + eprGov.FId + "'", true);
                        }
                    }
                    string currDept = GetCurrDept(ep.FBaseInfoId, nextSub.FId);

                    SortedList sl1 = new SortedList();
                    sl1.Clear();
                    sl1.Add("FID", AppId);
                    sl1.Add("FAppState", 0); //上报;
                    sl1.Add("FState", 6);
                    sl1.Add("FSubFlowId", nextSub.FId);
                    //sl1.Add("FReportDate", DateTime.Now);
                    sl1.Add("FCurStepID", currDept);//当前阶段审批的主管部门
                    sl1.Add("FRoleId", nextSub.FRoleId);
                    sl1.Add("FDefineDay", nextSub.FDefineDay);

                    //if (currSub.FTypeId == 5) //是审批结束
                    //{
                    sl1.Add("FResult", slData[i]["FResult"].ToString());
                    //}

                    if (currSub.FIsAppEnd == 1) //是审批结束
                    {
                        if (ep.FFactTime == null ||
                            ep.FFactTime <= EConvert.ToDateTime("2000-01-01 00:00:00.000"))
                        {
                            fFactTime = DateTime.Now.ToShortDateString();
                            sl1.Add("FFactTime", DateTime.Now.ToShortDateString());
                        }
                    }

                    aData.Add(sl1);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaProcessInstance);
                    aSaveOption.Add(SaveOptionEnum.Update);

                    slData[i].Add("FTypeId", currSub.FTypeId); //所属审批阶段 1接件 3正常审批 5终审 7归档
                    aData.Add(slData[i]);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaProcessRecord);
                    aSaveOption.Add(SaveOptionEnum.Update);

                    SortedList sl2 = new SortedList();
                    sl2.Clear();
                    sl2.Add("FID", Guid.NewGuid().ToString());
                    sl2.Add("FProcessInstanceId", AppId);
                    sl2.Add("FManageDeptId", currDept);
                    sl2.Add("FLinkId", ep.FLinkId);
                    sl2.Add("FSubFlowId", nextSub.FId);
                    sl2.Add("FMeasure", 0);
                    sl2.Add("FRoleId", nextSub.FRoleId);
                    sl2.Add("FRoleDesc", nextSub.FName);
                    sl2.Add("FTypeId", nextSub.FTypeId); //所属审批阶段 1接件 3正常审批 5终审 7归档
                    sl2.Add("FIsQuali", nextSub.FIsQuali);
                    sl2.Add("FReporttime", DateTime.Now);
                    sl2.Add("FIsPrint", nextSub.FIsPrint);
                    sl2.Add("FOrder", nextSub.FOrder);
                    sl2.Add("FIsdeleted", 0);
                    sl2.Add("FDefineDay", nextSub.FDefineDay);
                    sl2.Add("FLevel", nextSub.FLevel);
                    sl2.Add("FReportCount", fReportCount + 1);//审批步骤
                    //继承上级审批结果
                    if (slData[i].Contains("FResult"))
                    {
                        sl2.Add("FResult", slData[i]["FResult"].ToString());
                    }
                    if (slData[i].Contains("FPersonnel"))
                    {
                        sl2.Add("FPersonnel", slData[i]["FPersonnel"].ToString());
                    }
                    if (slData[i].Contains("FPerformance"))
                    {
                        sl2.Add("FPerformance", slData[i]["FPerformance"].ToString());
                    }
                    aData.Add(sl2);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaProcessRecord);
                    aSaveOption.Add(SaveOptionEnum.Insert);
                }
            }

            if (string.IsNullOrEmpty(slData[0]["FLinkId"].ToString()))
            {
                slData[0]["FLinkId"] = l;
            }
            //提取审批提交表状态
            EaAcceptBook eaBook = (EaAcceptBook)rCenter.GetEBase(EntityTypeEnum.EaAcceptBook, "FID,FApproveState,fisend,FEndTime,fstate", "FLinkId='" + slData[0]["FLinkId"] + "'");
            if (eaBook == null)
            {
                StartKCSJAppDetail(rCenter.GetSignValue(EntityTypeEnum.EaProcessInstance, "FBarCode", "FId='" + slData[0]["FProcessInstanceID"] + "'"), FBaseInfoId, slData[0]["FLinkId"].ToString(), rCenter.getEntSystemId(FBaseInfoId));
            }
            eaBook = (EaAcceptBook)rCenter.GetEBase(EntityTypeEnum.EaAcceptBook, "FID,FApproveState,fisend,FEndTime,fstate", "FLinkId='" + slData[0]["FLinkId"] + "'");
            if (eaBook != null && fFactTime != "")  //是审批结束更新EaAcceptBook
            {
                SortedList sl = new SortedList();
                sl.Add("FID", eaBook.FId);
                sl.Add("FIsAppEnd", 1);
                sl.Add("FAppEndTime", fFactTime);
                sl.Add("FState", 7); //办理结束
                aData.Add(sl);
                aKey.Add("FID");
                aEntity.Add(EntityTypeEnum.EaAcceptBook);
                aSaveOption.Add(SaveOptionEnum.Update);
            }

            if (isEnd)
            {
                SortedList sl5 = new SortedList();
                sl5.Add("FID", FBaseInfoId);
                StringBuilder sb = new StringBuilder();
                sb.Append(" select FState from cf_sys_user where fbaseinfoid='" + FBaseInfoId + "'");
                string fEntState = rCenter.GetSignValue(sb.ToString());
                if (fEntState != null && fEntState != "8")
                    sl5.Add("FState", 2);
                else
                    sl5.Add("FState", 8);

                bool isUpdate = false;
                //int noPassCount = 0;
                string fAppResult = "";

                for (int i = 0; i < slData.Length; i++)
                {

                    fAppResult = slData[i]["FResult"].ToString();


                    if (fAppResult.Trim() == "1")
                    {
                        //noPassCount++;
                        //其中有一个审批通过
                        isUpdate = true;
                        break;
                    }
                }

                if (isUpdate)
                {


                }
                aData.Add(sl5);
                aKey.Add("FID");
                aEntity.Add(EntityTypeEnum.EbBaseInfo);
                aSaveOption.Add(SaveOptionEnum.Update);

                if (eaBook != null && isUpdate)
                {
                    SortedList sl7 = new SortedList();
                    sl7.Add("FID", eaBook.FId);

                    sl7.Add("FApproveState", 1);

                    sb.Remove(0, sb.Length);
                    sb.Append(" select FAppTime from CF_App_ProcessRecord ");
                    sb.Append(" where FLinkId ='" + slData[0]["FLinkId"] + "'");
                    sb.Append(" order by forder desc,ftime desc");//取复审一步的时间
                    DateTime dAppTime = EConvert.ToDateTime(rCenter.GetSignValue(sb.ToString()));
                    if (dAppTime == DateTime.MinValue
                        || dAppTime.ToShortDateString().IndexOf("1900-1-1") > -1)
                        dAppTime = DateTime.Now;
                    sl7.Add("FAppTime", dAppTime);
                    aData.Add(sl7);
                    aKey.Add("FID");
                    aEntity.Add(EntityTypeEnum.EaAcceptBook);
                    aSaveOption.Add(SaveOptionEnum.Update);
                }
            }

            iCount = aKey.Count;
            EntityTypeEnum[] en = new EntityTypeEnum[iCount];
            string[] skey = new string[iCount];
            SortedList[] slup = new SortedList[iCount];
            SaveOptionEnum[] su = new SaveOptionEnum[iCount];
            for (int i = 0; i < iCount; i++)
            {
                en[i] = (EntityTypeEnum)aEntity[i];
                skey[i] = aKey[i].ToString();
                slup[i] = (SortedList)aData[i];
                su[i] = (SaveOptionEnum)aSaveOption[i];
            }

            //提交审批结果
            bool ret = rCenter.SaveEBaseM(en, slup, skey, su);

            iCount = slData.Length;
            for (int i = 0; i < iCount; i++)
            {
                string AppId = slData[i]["FProcessInstanceID"].ToString();
                EaProcessInstance ep = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FState", "FId='" + AppId + "'");
                if (ep.FState == 6)
                {
                    BackUpProcessInstance(ep.FId);
                }
            }

            //查看是否全部审批完毕
            string FLinkId = slData[0]["FLinkId"].ToString();
            iCount = rCenter.GetSQLCount(" select count(fstate) from CF_App_ProcessInstance  where FLinkId='" + FLinkId + "' and fstate<>6 and fstate<>2");
            if (iCount == 0)
            {
                SortedList sltemp = new SortedList();
                sltemp = new SortedList();
                if (eaBook != null)
                {
                    sltemp.Add("FID", eaBook.FId);
                    sltemp.Add("fisend", 1);
                    sltemp.Add("fstate", 9);
                    sltemp.Add("FEndTime", DateTime.Now);
                    ret = rCenter.SaveEBase(EntityTypeEnum.EaAcceptBook, sltemp, "FID", SaveOptionEnum.Update);
                }

                sltemp = new SortedList();
                sltemp.Add("FID", FLinkId);
                sltemp.Add("FState", 6);
                sltemp.Add("FAppDate", fFactTime);
                ret = rCenter.SaveEBase(EntityTypeEnum.EqList, sltemp, "FID", SaveOptionEnum.Update);




            }

            return ret;
        }


        #region 获取申请编号
        public string GetBarCode(string fRegistDeptNumber, string fSystemId)
        {
            string sBarCode = "";

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

            StringBuilder sb = new StringBuilder();
            sb.Append(" select max(FNo) from CF_App_No ");
            sb.Append(" where FNo like '" + sBarCode + "%'");


            SortedList sl = new SortedList();
            EntityTypeEnum en = EntityTypeEnum.EaNo;
            string fKey = "FID";
            SaveOptionEnum so = SaveOptionEnum.Insert;

            string fMaxBarCode = rCenter.GetSignValue(sb.ToString());
            if (fMaxBarCode != null && fMaxBarCode != "")
            {
                int iXh = EConvert.ToInt(fMaxBarCode.Substring(14, 6));
                iXh++;
                string sXh = "000000" + iXh.ToString();
                sXh = sXh.Substring(sXh.Length - 6, 6);
                sBarCode = sBarCode + sXh;
            }
            else
            {
                sBarCode = sBarCode + "000001";
            }

            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FNo", sBarCode);
            sl.Add("FSystemId", fSystemId);
            sl.Add("FIsDeleted", 0);
            sl.Add("FCreateTime", DateTime.Now);

            if (rCenter.SaveEBase(en, sl, fKey, so))
            {
                //反循环查找上报实例表中的条形码，如果已经有了，就再次生成
                //龚成龙 [2010-04-30]
                if (rCenter.GetSQLCount("select count(*) from cf_App_ProcessInstance where FBarCode='" + sBarCode + "'") > 0)
                    return this.GetBarCode(fRegistDeptNumber, fSystemId);
                return sBarCode;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 启动审核表

        public bool StartKCSJAppDetail(string sBarCode, string fBaseInfoId, string fAppId, string fSystemId)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from CF_App_AcceptBook where FLinkId='" + fAppId + "'  ");
            sb.Append(" and FState<>11 ");
            rCenter.PExcute(sb.ToString());

            sb.Remove(0, sb.Length);
            sb.Append(" select t.FName,t.FJuridcialCode,FRegistAddress FAddress,t.FRegistDeptId,''  FPostcode,t.FEmail,t.FLinkMan,t.FTel,");
            sb.Append(" FOTxt5  FManagerName ");
            sb.Append(" from cf_ent_baseinfo t ");
            sb.Append(" where   t.fid='" + fBaseInfoId + "'");
            DataTable dt = rCenter.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }



            EqList eq = (EqList)rCenter.GetEBase(EntityTypeEnum.EqList, "", "fid='" + fAppId + "'");
            if (eq == null)
            {
                return false;
            }

            EntityTypeEnum en = EntityTypeEnum.EaAcceptBook;
            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Insert;

            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FSystemId", fSystemId);
            sl.Add("FBaseInfoId", fBaseInfoId);
            sl.Add("FLinkId", fAppId);
            sl.Add("FAppNo", sBarCode);
            sl.Add("FEntName", dt.Rows[0]["FName"].ToString());
            sl.Add("FEntCode", dt.Rows[0]["FJuridcialCode"].ToString());
            sl.Add("FEntAddress", dt.Rows[0]["FAddress"].ToString());
            sl.Add("FEntPostCode", dt.Rows[0]["FPostcode"].ToString());
            sl.Add("FEntEmail", dt.Rows[0]["FEmail"].ToString());
            sl.Add("FEntLinkMan", dt.Rows[0]["FLinkMan"].ToString());
            sl.Add("FEntLinkTel", dt.Rows[0]["FTel"].ToString());
            sl.Add("FEntManager", dt.Rows[0]["FManagerName"].ToString());
            sl.Add("FAppMangeTypeId", eq.FManageTypeId);
            sl.Add("FAppMangeTypeName", rCenter.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + eq.FManageTypeId + "'"));
            sl.Add("FApplyUserId", fBaseInfoId);
            sl.Add("FApplyTime", eq.FReportDate);
            sl.Add("FApplyAcceptUnit", dt.Rows[0]["FName"].ToString());
            sl.Add("FApplyAcceptPerson", dt.Rows[0]["FManagerName"].ToString());
            //sl.Add("FAppCount", rQuali.GetSQLCount(" select count(1) from CF_App_Detail where fappid='" + fAppId + "' and FIsDeleted=0"));
            sl.Add("FState", 3);
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FIsDeleted", 0);



            return rCenter.SaveEBase(en, sl, "FID", so);
        }
        #endregion






        public bool SeeAppDetail(SortedList[] sDetail)
        {
            if (sDetail == null || sDetail.Length == 0)
            {
                return false;
            }

            EntityTypeEnum[] en = new EntityTypeEnum[sDetail.Length];
            string[] fKey = new string[sDetail.Length];
            SaveOptionEnum[] so = new SaveOptionEnum[sDetail.Length];
            SortedList[] sl = new SortedList[sDetail.Length];


            for (int i = 0; i < sDetail.Length; i++)
            {
                string fId = rCenter.GetSignValue(EntityTypeEnum.EaAcceptBook, "FId", "FLinkId='" + sDetail[i]["FLinkId"].ToString() + "' and FState<>11");
                if (fId == null || fId == "")
                {
                    return false;
                }

                en[i] = EntityTypeEnum.EaAcceptBook;
                fKey[i] = "FID";

                sl[i] = new SortedList();
                sl[i].Add("FID", fId);
                sl[i].Add("FAcceptState", sDetail[i]["FAcceptState"].ToString());
                sl[i].Add("FAcceptUnit", sDetail[i]["FAcceptUnit"].ToString());
                sl[i].Add("FAcceptWindow", sDetail[i]["FAcceptWindow"].ToString());
                sl[i].Add("FAcceptPerson", sDetail[i]["FAcceptPerson"].ToString());
                sl[i].Add("FAcceptPersonTel", sDetail[i]["FAcceptPersonTel"].ToString());
                sl[i].Add("FAcceptTime", sDetail[i]["FAcceptTime"].ToString());
                sl[i].Add("FLegalDay", sDetail[i]["FLegalDay"].ToString());
                sl[i].Add("FConsentEndTime", sDetail[i]["FConsentEndTime"].ToString());
                sl[i].Add("FConsentDay", sDetail[i]["FConsentDay"].ToString());
                sl[i].Add("FState", 5);

                so[i] = SaveOptionEnum.Update;
            }
            return rCenter.SaveEBaseM(en, sl, fKey, so);
        }

        public static string GetSystemConfig(string sName)
        {
            if (hashNumber[sName] != null)
            {
                return hashNumber[sName].ToString();
            }
            string fWebConfigPath = System.Configuration.ConfigurationSettings.AppSettings["DicConfigPath"].ToString();
            if (fWebConfigPath == null || fWebConfigPath == "")
            {
                return "";
            }
            XmlDocument xd = new XmlDocument();
            xd.Load(fWebConfigPath);
            XmlNodeList xl = xd.GetElementsByTagName(sName);
            if (xl == null || xl.Count == 0)
            {
                return "";
            }
            else
            {
                hashNumber.Add(sName, xl[0].InnerText);
            }
            return hashNumber[sName].ToString();
        }

        public static void ClearHashNumber()
        {
            hashNumber.Clear();
        }

        public string GetAppReustDesc(string fManageTypeId, int fType)
        {
            string fReturnValue = "";

            switch (fManageTypeId)
            {
                case "100": //施工企业数据备案
                    if (fType == 1)
                    {
                        fReturnValue = "同意备案";
                    }
                    if (fType == 2)
                    {
                        fReturnValue = "同意备案";
                    }
                    if (fType == 3)
                    {
                        fReturnValue = "不同意备案";
                    }
                    break;

                case "102": //施工企业新办核定
                case "103": //施工企业增项申请
                case "104": //施工企业延续申请
                case "106": //施工企业主项升级 
                case "108": //施工企业增项升级
                case "109": //施工企业转正申请
                case "113": //施工企业变更申请
                case "112": //施工企业其他
                case "115": //施工企业证书增补 
                case "118": //安全生产初次申请
                case "120": //安全生产延期申请

                    if (fType == 1)
                    {
                        fReturnValue = "同意";
                    }
                    if (fType == 2)
                    {
                        fReturnValue = "基本同意";
                    }
                    if (fType == 3)
                    {
                        fReturnValue = "不同意";
                    }
                    break;

                case "124": //施工企业资质核查
                    if (fType == 1)
                    {
                        fReturnValue = "合格";
                    }
                    if (fType == 2)
                    {
                        fReturnValue = "基本合格";
                    }
                    if (fType == 3)
                    {
                        fReturnValue = "不合格";
                    }
                    break;

            }
            return fReturnValue;
        }

        //施工企业获得证书编号
        public string CreateCertiNoSg(string FBaseinfoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select (select top 1 FCertiNo from CF_Sys_Dic where fparentid=105 and fnumber = FListId)+");
            sb.Append(" (select top 1 fdescrible from CF_Sys_QualiLevel where  fnumber = FLevelId)+");
            sb.Append(" (select top 1 FCertiNo from cf_sys_dic where fnumber = FTypeId and FNumber LIKE '105%')+");
            sb.Append(" (select top 1 FCertiNo from cf_sys_dic where fnumber in (");
            sb.Append(" select top 1 FEntTypeId from cf_ent_baseinfo where fid = fbaseinfoid)");
            sb.Append(" and  fparentid=110000)+");//施工系统编号
            sb.Append(" (select top 1 FCertiNo from cf_sys_dic where fnumber in (");
            sb.Append(" select top 1 FSubjectionId from cf_ent_baseinfo where fid = fbaseinfoid)");
            sb.Append(" and  fparentid=124000) ");//隶属关系
            sb.Append(" from CF_Ent_QualiCertiTrade where ");
            sb.Append(" FIsBase=1 and FBaseInfoId='" + FBaseinfoId + "' ");
            DataTable dt = this.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                return "";
            }
            string fNo = dt.Rows[0][0].ToString();

            sb.Remove(0, sb.Length);
            sb.Append(" select substring(convert(char,FRegistDeptId,10),1,4) from cf_ent_baseinfo ");
            sb.Append(" where fid='" + FBaseinfoId + "' ");
            string fRegistDeptId = this.GetSignValue(sb.ToString());
            if (fRegistDeptId == null || fRegistDeptId == "")
            {
                return "";
            }
            int iLength = fRegistDeptId.Trim().Length;
            if (iLength < 4)
            {
                for (int i = 0; i < 4 - iLength; i++)
                {
                    fRegistDeptId = fRegistDeptId.Insert(0, "0");
                }
            }
            fNo += fRegistDeptId.Trim();

            sb.Remove(0, sb.Length);
            sb.Append(" select count(1) from cf_ent_baseinfo ceb,CF_Ent_QualiCertiTrade ceq ");
            sb.Append(" where ceb.fid = ceq.fbaseinfoid ");
            sb.Append(" and ceq.FLevelId  in (");
            sb.Append(" select FLevelId from CF_Ent_QualiCertiTrade ceq1 where fBaseinfoid='" + FBaseinfoId + "' and FIsBase=1 ) ");
            sb.Append(" and ceq.FTypeId  in (");
            sb.Append(" select FTypeId from CF_Ent_QualiCertiTrade ceq2 where fBaseinfoid='" + FBaseinfoId + "' and FIsBase=1 ) ");
            sb.Append(" and ceq.FLevelId  in (");
            sb.Append(" select FLevelId from CF_Ent_QualiCertiTrade ceq3 where fBaseinfoid='" + FBaseinfoId + "' and FIsBase=1 ) ");
            sb.Append(" and ceq.FIsBase=1 ");
            sb.Append(" and ceb.FRegistDeptId in (");
            sb.Append(" select FRegistDeptId from cf_ent_Baseinfo ceb1 where ceb1.fid='" + FBaseinfoId + "') ");

            string fIndex = this.GetSignValue(sb.ToString());
            if (fIndex == null || fIndex == "")
            {
                fIndex = "0";
            }
            try
            {
                fNo += (EConvert.ToInt(fIndex) + 1).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

            return fNo;
        }

        //是否开始审批
        public bool isBeginApp(string FLinkId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select er.fmeasure,ep.fstate from CF_App_ProcessInstance ep,CF_App_ProcessRecord er where ");
            sb.Append("ep.fid=er.FProcessInstanceID and ");
            sb.Append("ep.flinkid='" + FLinkId + "' ");
            sb.Append("and isnull(ep.FState,0)<>2 ");
            //and (er.fmeasure<>0 or ep.fstate=6) 
            DataTable dt = rCenter.GetTable(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select(" fmeasure<>0 or fstate=6 ");
                if (dr.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;

            }
        }


        public bool BackUpProcessInstance(string fPId)
        {
            StringBuilder sb = new StringBuilder();

            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();

            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_App_ProcessInstance where fid='" + fPId + "'");
            DataTable dt = rCenter.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return true;
            }
            SortedList sl = new SortedList();
            sl = GetSlFromDr(dt.Rows[0]);
            arrEn.Add(EntityTypeEnum.EaProcessInstanceBackup);
            arrKey.Add("FID");
            arrSo.Add(SaveOptionEnum.Insert);
            arrSl.Add(sl);

            sb.Remove(0, sb.Length);
            sb.Append(" select * from CF_App_ProcessRecord where FProcessInstanceID='" + fPId + "'");
            dt = rCenter.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return true;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sl = new SortedList();
                sl = GetSlFromDr(dt.Rows[i]);
                arrEn.Add(EntityTypeEnum.EaProcessRecordBackup);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Insert);
                arrSl.Add(sl);
            }

            int iCount = arrEn.Count;
            if (iCount == 0)
            {
                return true;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SortedList[] sls = new SortedList[iCount];
            string[] fkeys = new string[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];

            for (int i = 0; i < iCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = new SortedList();
                sls[i] = (SortedList)arrSl[i];
                fkeys[i] = (string)arrKey[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
            }

            sb.Remove(0, sb.Length);
            sb.Append(" begin ");
            sb.Append(" delete from CF_App_ProcessInstance where fid='" + fPId + "'");
            sb.Append(" delete from CF_App_ProcessRecord where FProcessInstanceID='" + fPId + "'");
            sb.Append(" end ");
            rCenter.PExcute(sb.ToString());
            rCenter.SaveEBaseM(ens, sls, fkeys, sos);
            return true;

        }






        #region  //建造师审批相关
        #region 新方法 2009-07-22
        //新方法 2009-07-22
        public string GetCurrDeptId(string sUpDeptId, EaSubFlow es)
        {
            if (sUpDeptId.Length < 2)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            string sValue = "";
            sValue = sValue.Insert(0, sUpDeptId);

            sUpDeptId = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + sUpDeptId + "'");
            if (!string.IsNullOrEmpty(sUpDeptId))
            {
                sValue = sValue.Insert(0, sUpDeptId + ",");
            }

            sUpDeptId = this.rCenter.GetSignValue(EntityTypeEnum.EsManageDept, "FParentId", "FNumber='" + sUpDeptId + "'");
            if (!string.IsNullOrEmpty(sUpDeptId))
            {
                sValue = sValue.Insert(0, sUpDeptId + ",");
            }

            if (string.IsNullOrEmpty(sValue))
            {
                return "";
            }


            sb.Remove(0, sb.Length);
            sb.Append(" select top 1 FNumber  from cf_sys_managedept ");
            sb.Append(" where  FLevel='" + es.FLevel + "' and FNumber in ");
            sb.Append("(" + sValue + ")");
            return rCenter.GetSignValue(sb.ToString());
        }

        public string GetCurrDept(string FAppId, string fBaseInfoId, string fSubFlowId)
        {
            EaSubFlow es = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + fSubFlowId + "'");
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
            return rCenter.GetSignValue(sb.ToString());
        }
        /// <summary>
        /// 喻勋海  2014-11-20 添加
        /// </summary>
        /// <param name="fnumber"></param>
        /// <param name="FSystemId"></param>
        /// <param name="FManageTypeId"></param>
        /// <param name="FManageDeptId"></param>
        /// <returns></returns>
        public DataTable GetCanReportDeptXM(string fnumber, string FSystemId, string FManageTypeId, string FManageDeptId)
        {
            StringBuilder sb = new StringBuilder();
            string LevelId = "1930100";
            string TypeId = "1930100";
            if (fnumber == null || fnumber == "")
            {
                return null;
            }
            EsManageDept ManageDept = rCenter.GetEBase(EntityTypeEnum.EsManageDept, "fistown,FLevel", "fnumber='" + fnumber + "'") as EsManageDept;
            //提取是否扩权县
            string isTown = ManageDept.FIsTown;
            EaProcess ep = null;
            if (isTown == "1")
            {
                //扩权县判断
                ep = this.GetProcess(FSystemId, FManageTypeId, LevelId, TypeId, "700101");
                if (ep == null)
                {
                    ep = this.GetProcess(FSystemId, FManageTypeId, LevelId, TypeId, FManageDeptId);
                    if (ep == null)
                    {
                        return null;
                    }
                }
            }
            else
            {
                ep = this.GetProcess(FSystemId, FManageTypeId, LevelId, TypeId, FManageDeptId);
            }
            if (ep == null)
            {
                return null;
            }
            //StringBuilder sb = new StringBuilder();
            sb.Append(" select fname,fnumber from CF_Sys_ManageDept where FLevel in ");
            sb.Append(" (select FLevel from CF_App_SubFlow where FProcessId='" + ep.FId + "' and fisdeleted=0 and FLevel>0) ");
            sb.Append(" and fnumber in (");
            string sheng = fnumber.Substring(0, 2);
            string shi = fnumber.Length > 2 ? fnumber.Substring(0, 4) : fnumber;
            string qu = fnumber.Length > 4 ? fnumber.Substring(0, 6) : fnumber;
            string fNumberInId = "'" + sheng + "','" + shi + "','" + qu + "'";
            sb.Append(fNumberInId);
            sb.Append(") and fclassnumber='102009' ");
            sb.Append(" order by FNumber desc");
            return rCenter.GetTable(sb.ToString());
        }


        //省级建造师将人员打回到市级 2010-06-03 (龚成龙)
        #region
        public bool BackToCityApp(SortedList[] slData)
        {
            if (slData == null || slData.Length == 0)
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList arrKey = new ArrayList();
            for (int i = 0; i < slData.Length; i++)
            {
                EaProcessInstance ep = (EaProcessInstance)rCenter.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FProcessId,FEmpId,FLinkId,FSubFlowId",
                    "FId='" + slData[i]["FProcessInstanceId"].ToString() + "'");
                if (ep == null)
                {
                    return false;
                }


                arrEn.Add(EntityTypeEnum.EaProcessRecord);
                SortedList sl = new SortedList();
                sl = slData[i];
                arrSl.Add(sl);
                arrSo.Add(SaveOptionEnum.Update);
                arrKey.Add("FID");


                sb.Remove(0, sb.Length);
                sb.Append(" select FOrder from CF_App_SubFlow where FId='" + ep.FSubFlowId + "'");
                string sOrder = rCenter.GetSignValue(sb.ToString());



                sb.Remove(0, sb.Length);
                //取回市级部门的最高审批阶段
                sb.Append(" FProcessId='" + ep.FProcessId + "' and FOrder<" + sOrder + "");
                sb.Append(" and FProcessId='" + ep.FProcessId + "' and FLevel=2 order by FOrder desc");

                EaSubFlow es = (EaSubFlow)rCenter.GetEBase(EntityTypeEnum.EaSubFlow, "", sb.ToString());
                if (es == null)
                {
                    return false;
                }
                else
                {
                    sb.Remove(0, sb.Length);
                    sb.Append(" select * from CF_App_ProcessRecord ");
                    sb.Append(" where FProcessInstanceID='" + ep.FId + "' ");
                    sb.Append(" and FSubFlowId='" + es.FId + "'");
                    DataTable dt = rCenter.GetTable(sb.ToString());

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {

                        sl = new SortedList();
                        sl.Add("FID", ep.FId);
                        sl.Add("FState", 3);
                        sl.Add("FAppState", 3);
                        sl.Add("FSubFlowId", dt.Rows[0]["FSubFlowId"].ToString());
                        sl.Add("FRoleId", dt.Rows[0]["FRoleId"].ToString());
                        sl.Add("FCurStepID", dt.Rows[0]["FManageDeptId"].ToString());
                        arrEn.Add(EntityTypeEnum.EaProcessInstance);
                        arrSl.Add(sl);
                        arrSo.Add(SaveOptionEnum.Update);
                        arrKey.Add("FID");


                        sl = new SortedList();
                        sl.Add("FID", dt.Rows[0]["FId"].ToString());
                        sl.Add("FMeasure", 0);
                        sl.Add("FResult", "");
                        sl.Add("FIdea", "");
                        arrEn.Add(EntityTypeEnum.EaProcessRecord);
                        arrSl.Add(sl);
                        arrSo.Add(SaveOptionEnum.Update);
                        arrKey.Add("FID");
                    }
                }
            }
            int iCount = arrEn.Count;
            if (iCount == 0)
            {
                return false;
            }

            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SortedList[] sls = new SortedList[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            string[] keys = new string[iCount];

            for (int i = 0; i < iCount; i++)
            {
                ens[i] = (EntityTypeEnum)arrEn[i];
                sls[i] = new SortedList();
                sls[i] = (SortedList)arrSl[i];
                sos[i] = (SaveOptionEnum)arrSo[i];
                keys[i] = (string)arrKey[i];
            }
            return rCenter.SaveEBaseM(ens, sls, keys, sos);
        }
        #endregion

        #endregion
        #endregion





        #region 程序生审查 生成“业务流水号”

        /// <summary>
        /// 程序生审查 生成“业务流水号”
        /// </summary>
        /// <param name="FAppId">业务ID</param>
        /// <returns></returns>
        public string GetAutoNO(string FAppId)
        {
            string v = "";

            //目前规则不确定，先用时间代替
            v = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            return v;
        }


        #endregion
    }
}
