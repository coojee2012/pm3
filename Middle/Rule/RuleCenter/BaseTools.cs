using System;
using System.Collections.Generic;
using System.Text;
using Approve.PersistBase;
using Approve.EntityBase;
using System.Data;

using Approve.EntityCenter;
using System.Collections;
using Approve.EntitySys;
using Approve.EntityQuali;

namespace Approve.RuleCenter
{
    public class BaseTools : IBaseTools
    {
        #region 实体和表对应关系函数

        public string GetName(EntityTypeEnum eType)
        {
            string eName = "";
            switch (eType)
            {

                #region sys
                case EntityTypeEnum.EsDic:
                    eName = "CF_Sys_Dic";
                    break;
                case EntityTypeEnum.EsDicClass:
                    eName = "CF_Sys_DicClass";
                    break;
                case EntityTypeEnum.EsSystemName:
                    eName = "CF_Sys_SystemName";
                    break;
                case EntityTypeEnum.EsManageDept:
                    eName = "CF_Sys_ManageDept";
                    break;
                case EntityTypeEnum.EsManageType:
                    eName = "CF_Sys_ManageType";
                    break;
                case EntityTypeEnum.EsUserLockInfo:
                    eName = "CF_Sys_UserLockInfo";
                    break;

                case EntityTypeEnum.EsUserInfo:
                    eName = "CF_Sys_UserInfo";
                    break;

                case EntityTypeEnum.EsUserFinance:
                    eName = "CF_Sys_UserFinance";
                    break;
                case EntityTypeEnum.EsUserRight:
                    eName = "CF_Sys_UserRight";
                    break;
                case EntityTypeEnum.EsRole:
                    eName = "CF_Sys_Role";
                    break;
                case EntityTypeEnum.EsUser:
                    eName = "CF_Sys_User";
                    break;
                case EntityTypeEnum.EPText:
                    eName = "CF_Pub_Text";
                    break;
                case EntityTypeEnum.EsCode:
                    eName = "CF_Sys_Code";
                    break;
                case EntityTypeEnum.EsMenu:
                    eName = "CF_Sys_Menu";
                    break;
                case EntityTypeEnum.EsQualiLevel:
                    eName = "CF_Sys_QualiLevel";
                    break;
                case EntityTypeEnum.EsTree:
                    eName = "CF_Sys_Tree";
                    break;

                case EntityTypeEnum.EsBadActionCode:
                    eName = "CF_Sys_BadActionCode";
                    break;
                case EntityTypeEnum.EsStatis:
                    eName = "CF_Sys_Statis";
                    break;
                case EntityTypeEnum.EsStatisInfo:
                    eName = "CF_Sys_StatisInfo";
                    break;

                case EntityTypeEnum.EsObject:
                    eName = "CF_Sys_Object";
                    break;
                case EntityTypeEnum.EsRoleRight:
                    eName = "CF_Sys_RoleRight";
                    break;

                case EntityTypeEnum.EsProjectType:
                    eName = "CF_Sys_ProjectType";
                    break;

                case EntityTypeEnum.EsAppStand:
                    eName = "CF_Sys_AppStand";
                    break;

                case EntityTypeEnum.EsManageDeptInfo:
                    eName = "CF_Sys_ManageDeptInfo";
                    break;

                case EntityTypeEnum.EaAppPrintIc:
                    eName = "CF_App_PrintIc";
                    break;

                case EntityTypeEnum.EsCstUser:
                    eName = "CF_Sys_CstUser";
                    break;

                case EntityTypeEnum.EsHolidays:
                    eName = "CF_Sys_Holidays";
                    break;
                //自动审查表


                #endregion

                #region app
                case EntityTypeEnum.EaProcessRecord:
                    eName = "CF_App_ProcessRecord";
                    break;
                case EntityTypeEnum.EaAppBackIdea:
                    eName = "CF_App_BackIdea";
                    break;
                case EntityTypeEnum.EaSubFlow:
                    eName = "CF_App_SubFlow";
                    break;
                case EntityTypeEnum.EaProcessInstance:
                    eName = "CF_App_ProcessInstance";
                    break;
                case EntityTypeEnum.EaQualiType:
                    eName = "CF_App_QualiType";
                    break;
                case EntityTypeEnum.EaProcess:
                    eName = "CF_App_Process";
                    break;
                case EntityTypeEnum.EaQualiLevel:
                    eName = "CF_App_QualiLevel";
                    break;
                case EntityTypeEnum.EaManageType:
                    eName = "CF_App_ManageType";
                    break;
                case EntityTypeEnum.EaAcceptBook:
                    eName = "CF_App_AcceptBook";
                    break;
                case EntityTypeEnum.EaQuailAppList:
                    eName = "CF_App_QuailAppList";
                    break;
                case EntityTypeEnum.EaQualiCondition:
                    eName = "CF_App_QualiCondition";
                    break;

                case EntityTypeEnum.EaQualiConditionData:
                    eName = "CF_App_QualiConditionData";
                    break;

                case EntityTypeEnum.EaQualiConditionOther:
                    eName = "CF_App_QualiConditionOther";
                    break;

                case EntityTypeEnum.EaCheck:
                    eName = "CF_App_Check";
                    break;

                case EntityTypeEnum.EaNo:
                    eName = "CF_App_No";
                    break;

                case EntityTypeEnum.EaBatchNo:
                    eName = "CF_App_BatchNo";
                    break;

                case EntityTypeEnum.EaAppBatchNo:
                    eName = "CF_App_appBatchNo";
                    break;

                case EntityTypeEnum.EaProcessInstanceBackup:
                    eName = "CF_App_ProcessInstanceBackup";
                    break;

                case EntityTypeEnum.EaProcessRecordBackup:
                    eName = "CF_App_ProcessRecordBackup";
                    break;

                case EntityTypeEnum.EaAppProcessPublic:
                    eName = "CF_App_ProcessPublic";
                    break;
                case EntityTypeEnum.EaAppProcessComplaint:
                    eName = "CF_App_ProcessComplaint";
                    break;
                case EntityTypeEnum.EaAppAcceptBook:
                    eName = "CF_App_AppAcceptBook";
                    break;

                case EntityTypeEnum.EaPrintRecord:
                    eName = "CF_App_PrintRecord";
                    break;

                case EntityTypeEnum.EaAppActionRecord:
                    eName = "CF_App_ActionRecord";
                    break;

                case EntityTypeEnum.EqAppChange:
                    eName = "CF_App_ChangeRecord";
                    break;

                #endregion
                #region 三类人员
                case EntityTypeEnum.EqEeDetail:
                    eName = "CF_Person_AppDetial";
                    break;
                case EntityTypeEnum.EqPersonChangeDetial:
                    eName = "CF_Person_ChangeDetial";
                    break;
                case EntityTypeEnum.EqEeOtherInfo:
                    eName = "CF_Person_OtherInfo";
                    break;
                case EntityTypeEnum.EqEeContinueStudy:
                    eName = "CF_Person_AppEducation";
                    break;
                case EntityTypeEnum.EqEeGoodAction:
                    eName = "CF_AppEmp_GoodAction";
                    break;
                case EntityTypeEnum.EqEeBadAction:
                    eName = "CF_AppEmp_BadAction";
                    break;
                case EntityTypeEnum.EeRemoveRecord:
                    eName = "CF_Emp_RemoveRecord";
                    break;
                case EntityTypeEnum.EePersonBaseInfo:
                    eName = "CF_Person_BaseInfo";
                    break;
                case EntityTypeEnum.EePersonSpecial:
                    eName = "CF_Person_Special";
                    break;
                #endregion

                #region eb

                case EntityTypeEnum.EbBaseInfo:
                    eName = "CF_Ent_BaseInfo";
                    break;

                case EntityTypeEnum.EbOutBaseInfo:
                    eName = "CF_EntOut_BaseInfo";
                    break;

                case EntityTypeEnum.EbBadAction:
                    eName = "CF_Ent_BadAction";
                    break;

                case EntityTypeEnum.EbGoodAchieve:
                    eName = "CF_ENT_GoodAchieve";
                    break;

                case EntityTypeEnum.EbBankAccount:
                    eName = "CF_Ent_BankAccount";
                    break;

                case EntityTypeEnum.EbBaseInfoOther:
                    eName = "CF_Ent_BaseInfoOther";
                    break;

                case EntityTypeEnum.EbCompact:
                    eName = "CF_Ent_Compact";
                    break;

                case EntityTypeEnum.EbDevice:
                    eName = "CF_Ent_Device";
                    break;

                case EntityTypeEnum.EbFinance:
                    eName = "CF_Ent_Finance";
                    break;

                case EntityTypeEnum.EbGoodAction:
                    eName = "CF_Ent_GoodAction";
                    break;

                case EntityTypeEnum.EbInvestPerson:
                    eName = "CF_Ent_InvestPerson";
                    break;
                case EntityTypeEnum.EbLeader:
                    eName = "CF_Ent_Leader";
                    break;

                case EntityTypeEnum.EbProject:
                    eName = "CF_Ent_Project";
                    break;

                case EntityTypeEnum.EbProjectCerti:
                    eName = "CF_Ent_ProjectCerti";
                    break;

                case EntityTypeEnum.EbProjectManager:
                    eName = "CF_Ent_ProjectManager";
                    break;

                case EntityTypeEnum.EbProjectOther:
                    eName = "CF_Ent_ProjectOther";
                    break;

                case EntityTypeEnum.EbOffence:
                    eName = "CF_Ent_ProjectOffense";
                    break;

                case EntityTypeEnum.EbProjectBid:
                    eName = "CF_Ent_ProjectBid";
                    break;


                case EntityTypeEnum.EbQualiCerti:
                    eName = "CF_Ent_QualiCerti";
                    break;


                case EntityTypeEnum.EbQualiCertiTrade:
                    eName = "CF_Ent_QualiCertiTrade";
                    break;


                case EntityTypeEnum.EbQuality:
                    eName = "CF_Ent_Quality";
                    break;

                case EntityTypeEnum.EbSafetyCerti:
                    eName = "CF_Ent_SafetyCerti";
                    break;
                case EntityTypeEnum.EbBadActionCode:
                    eName = "CF_Ent_BadActionCode";
                    break;

                case EntityTypeEnum.EbZCFZ:
                    eName = "CF_Ent_ZCFZ";
                    break;

                case EntityTypeEnum.EbSurvey:
                    eName = "CF_Ent_Survey";
                    break;

                case EntityTypeEnum.EbSurveyTime:
                    eName = "CF_Ent_SurveyTime";
                    break;
                //case EntityTypeEnum.EbProjectCerti:
                //    eName = "CF_Ent_ProjectCerti";
                //    break;
                //case EntityTypeEnum.EbProjectManager:
                //    eName = "CF_Ent_ProjectManager";
                //    break;
                //case EntityTypeEnum.EbBankAccount:
                //    eName = "CF_Ent_BankAccount";
                //    break;
                //case EntityTypeEnum.EbInvestPerson:
                //    eName = "CF_Ent_InvestPerson";
                //    break;
                //case EntityTypeEnum.EbQuality:
                //    eName = "CF_Ent_Quality";
                //    break;
                case EntityTypeEnum.EbCreditLevel:
                    eName = "CF_Ent_CreditLevel";
                    break;
                case EntityTypeEnum.EbProjBackUp:
                    eName = "CF_Ent_ProjBackUp";
                    break;

                case EntityTypeEnum.EbProjectProcessIdear:
                    eName = "CF_Ent_PorjectProcessIdear";
                    break;
                case EntityTypeEnum.EbProjectAdd:
                    eName = "CF_Ent_ProjectAdd";
                    break;
                case EntityTypeEnum.EbProjectAppEmployee:
                    eName = "CF_Ent_ProjectAppEmployee";
                    break;
                case EntityTypeEnum.EbProjectDetail:
                    eName = "CF_Ent_projectDetail";
                    break;
                case EntityTypeEnum.EbProjectEmployee:
                    eName = "CF_Ent_ProjectEmployee";
                    break;
                case EntityTypeEnum.EbProjectIdea:
                    eName = "CF_Ent_ProjectIdea";
                    break;
                case EntityTypeEnum.EbProjectResult:
                    eName = "CF_Ent_ProjectResult";
                    break;

                case EntityTypeEnum.EbProjectBackupIdea:
                    eName = "CF_Ent_ProjectBackupIdea";
                    break;


                case EntityTypeEnum.EbSingleProject:
                    eName = "CF_Ent_SingleProject";
                    break;

                case EntityTypeEnum.EbDesignReport:
                    eName = "CF_Ent_DesignReport";
                    break;


                case EntityTypeEnum.EbKCSJFloatReport:
                    eName = "CF_Ent_KCSJFloatReport";
                    break;


                case EntityTypeEnum.EbKCSJIntReport:
                    eName = "CF_Ent_KCSJIntReport";
                    break;


                case EntityTypeEnum.EbCheckParameter:
                    eName = "CF_Ent_CheckParameter";
                    break;

                case EntityTypeEnum.EqEbQualiCertiTrade:
                    eName = "CF_AppEnt_QualiCertiTrade";
                    break;



                #endregion

                #region ee

                case EntityTypeEnum.EeBaseinfo:
                    eName = "CF_Emp_Baseinfo";
                    break;

                case EntityTypeEnum.EeBadAction:
                    eName = "CF_Emp_BadAction";
                    break;

                case EntityTypeEnum.EeProPer:
                    eName = "CF_Emp_ProPer";
                    break;
                case EntityTypeEnum.EeProTech:
                    eName = "CF_Emp_ProTech";
                    break;
                case EntityTypeEnum.EeProCheck:
                    eName = "CF_Emp_ProCheck";
                    break;


                case EntityTypeEnum.EeBaseinfoOther:
                    eName = "CF_Emp_BaseinfoOther";
                    break;

                case EntityTypeEnum.EeCerti:
                    eName = "CF_Emp_Certi";
                    break;

                case EntityTypeEnum.EeContinueStudy:
                    eName = "CF_Person_Education";
                    break;

                case EntityTypeEnum.EeGoodAction:
                    eName = "CF_Emp_GoodAction";
                    break;

                case EntityTypeEnum.EeProjectBuildStandard:
                    eName = "CF_Emp_ProjectBuildStandard";
                    break;

                case EntityTypeEnum.EeResume:
                    eName = "CF_Emp_RegistResume";
                    break;

                case EntityTypeEnum.EeWorkExperience:
                    eName = "CF_Emp_WorkExperience";
                    break;

                case EntityTypeEnum.EeWorkAchievement:
                    eName = "CF_Emp_WorkAchievement";
                    break;


                case EntityTypeEnum.EeRegistCerti:
                    eName = "CF_Emp_RegistCerti";
                    break;

                case EntityTypeEnum.EeRegistSpecial:
                    eName = "CF_Emp_RegistSpecial";
                    break;

                case EntityTypeEnum.EeConstructUserInfo:
                    eName = "CF_Construct_UserInfo";
                    break;


                case EntityTypeEnum.EeSafeBaseinfo:
                    eName = "CF_Emp_SafeBaseinfo";
                    break;

                case EntityTypeEnum.EeIni:
                    eName = "CF_Emp_Ini";
                    break;

                case EntityTypeEnum.EeConstructChangeRecord:
                    eName = "CF_Construct_ChangeRecord";
                    break;

                case EntityTypeEnum.EeOtherRegist:
                    eName = "CF_Emp_OtherRegist";
                    break;


                #endregion

                #region eq
                case EntityTypeEnum.EqEbGoodAchieve:
                    eName = "CF_AppEnt_GoodAchieve";
                    break;
                case EntityTypeEnum.EqEbApplyAchieve:
                    eName = "CF_AppEnt_ApplyAchieve";
                    break;
                case EntityTypeEnum.EqEbApplyDevice:
                    eName = "CF_AppEnt_ApplyDevice";
                    break;
                case EntityTypeEnum.EqEbApplyEmployee:
                    eName = "CF_AppEnt_ApplyEmployee";
                    break;
                case EntityTypeEnum.EqEbApplyProject:
                    eName = "CF_AppEnt_ApplyProject";
                    break;
                case EntityTypeEnum.EqEbApplySpecial:
                    eName = "CF_AppEnt_ApplySpecial";
                    break;
                case EntityTypeEnum.EqEbApplyTrade:
                    eName = "CF_AppEnt_ApplyTrade";
                    break;


                case EntityTypeEnum.EqDetail:
                    eName = "CF_App_Detail";
                    break;


                case EntityTypeEnum.EqList:
                    eName = "CF_App_List";
                    break;

                case EntityTypeEnum.EqIdea:
                    eName = "CF_App_Idea";
                    break;

                case EntityTypeEnum.EqEeBaseinfo:
                    eName = "CF_AppEmp_Baseinfo";
                    break;

                case EntityTypeEnum.EqEeSafeBaseinfo:
                    eName = "CF_AppEmp_SafeBaseinfo";
                    break;

                case EntityTypeEnum.EqEeRegistCerti:
                    eName = "CF_AppEmp_RegistCerti";
                    break;

                case EntityTypeEnum.EqEeRegistSpecial:
                    eName = "CF_AppEmp_RegistSpecial";
                    break;


                case EntityTypeEnum.EqEeResume:
                    eName = "CF_AppEmp_Resume";
                    break;
                case EntityTypeEnum.EqEeWorkExperience:
                    eName = "CF_AppEmp_WorkExperience";
                    break;

                case EntityTypeEnum.EqEeWorkAchievement:
                    eName = "CF_AppEmp_WorkAchievement";
                    break;


                case EntityTypeEnum.EqEbBadAction:
                    eName = "CF_AppEnt_BadAction";
                    break;

                case EntityTypeEnum.EqEbBaseInfo:
                    eName = "CF_AppEnt_BaseInfo";
                    break;

                case EntityTypeEnum.EqEbBaseInfoOther:
                    eName = "CF_AppEnt_BaseInfoOther";
                    break;

                case EntityTypeEnum.EqEbDevice:
                    eName = "CF_AppEnt_Device";
                    break;

                case EntityTypeEnum.EqEbFinance:
                    eName = "CF_AppEnt_FINANCE";
                    break;

                case EntityTypeEnum.EqEbGoodAction:
                    eName = "CF_AppEnt_GoodAction";
                    break;

                case EntityTypeEnum.EqEbLeader:
                    eName = "CF_AppEnt_Leader";
                    break;

                case EntityTypeEnum.EqEbProject:
                    eName = "CF_AppEnt_Project";
                    break;

                case EntityTypeEnum.EqEbProjectOther:
                    eName = "CF_AppEnt_ProjectOther";
                    break;

                case EntityTypeEnum.EqEbBaseInfoChange:
                    eName = "CF_AppEnt_BaseInfoChange";
                    break;


                case EntityTypeEnum.EqPText:
                    eName = "CF_AppPub_Text";
                    break;

                case EntityTypeEnum.EqPPic:
                    eName = "CF_App_PubPic";
                    break;

                case EntityTypeEnum.EqCheckList:
                    eName = "CF_App_CheckList";
                    break;

                case EntityTypeEnum.EqSafetyCerti:
                    eName = "CF_AppEnt_SafetyCerti";
                    break; ;

                case EntityTypeEnum.EqEbCheckParameter:
                    eName = "CF_AppEnt_CheckParameter";
                    break;

                case EntityTypeEnum.EqEbZCFZ:
                    eName = "CF_AppEnt_ZCFZ";
                    break;
                case EntityTypeEnum.EqEbProjectCerti:
                    eName = "CF_AppEnt_ProjectCerti";
                    break;
                case EntityTypeEnum.EqEbProjectManager:
                    eName = "CF_AppEnt_ProjectManager";
                    break;
                case EntityTypeEnum.EqEbBankAccount:
                    eName = "CF_AppEnt_BankAccount";
                    break;
                case EntityTypeEnum.EqEbInvestPerson:
                    eName = "CF_AppEnt_InvestPerson";
                    break;
                case EntityTypeEnum.EqEbQuality:
                    eName = "CF_AppEnt_Quality";
                    break;
                case EntityTypeEnum.EqEbCreditLevel:
                    eName = "CF_AppEnt_CreditLevel";
                    break;
                case EntityTypeEnum.EqEbProjBackUp:
                    eName = "CF_AppEnt_ProjBackUp";
                    break;
                case EntityTypeEnum.EqEbQualiCerti:
                    eName = "CF_AppEnt_QualiCerti";
                    break;


                case EntityTypeEnum.EqProjectProcessIdear:
                    eName = "CF_AppEnt_PorjectProcessIdear";
                    break;
                case EntityTypeEnum.EqProjectAdd:
                    eName = "CF_AppEnt_ProjectAdd";
                    break;
                case EntityTypeEnum.EqProjectAppEmployee:
                    eName = "CF_AppEnt_ProjectAppEmployee";
                    break;
                case EntityTypeEnum.EqProjectDetail:
                    eName = "CF_AppEnt_projectDetail";
                    break;
                case EntityTypeEnum.EqProjectEmployee:
                    eName = "CF_AppEnt_ProjectEmployee";
                    break;
                case EntityTypeEnum.EqProjectIdea:
                    eName = "CF_AppEnt_ProjectIdea";
                    break;
                case EntityTypeEnum.EqProjectResult:
                    eName = "CF_AppEnt_ProjectResult";
                    break;

                case EntityTypeEnum.EqEeConstructAppDetial:
                    eName = "CF_Construct_AppDetial";
                    break;

                case EntityTypeEnum.EqEbChangeRecord:
                    eName = "CF_App_EntChangeRecord";
                    break;



                case EntityTypeEnum.EqEeOtherRegist:
                    eName = "CF_AppEmp_OtherRegist";
                    break;

                case EntityTypeEnum.EqEeEntUserInfo:
                    eName = "CF_EntUserDetail";
                    break;

                case EntityTypeEnum.CityReport:
                    eName = "CF_Gov_CityReport";
                    break;
                case EntityTypeEnum.CityReportDetails:
                    eName = "CF_Gov_CityReportDetails";
                    break;
                case EntityTypeEnum.EqPrjRegistration:
                    eName = "CQ_Prj_Registration";
                    break;
                case EntityTypeEnum.EqPrjRegWorks:
                    eName = "CQ_Prj_RegWorks";
                    break;
                case EntityTypeEnum.EqPrjEmp:
                    eName = "CQ_Prj_Emp";
                    break;
                case EntityTypeEnum.EcPrjOrder:
                    eName = "CF_Prj_Order";
                    break;

                #endregion

                #region 留言表
                case EntityTypeEnum.ElInfo:
                    eName = "CF_LevelWord_Info";
                    break;
                #endregion

                #region 新闻相关表
                case EntityTypeEnum.EnComment:
                    eName = "CF_News_Comment";
                    break;
                case EntityTypeEnum.EnContent:
                    eName = "CF_News_Content";
                    break;
                case EntityTypeEnum.EnCol:
                    eName = "CF_News_Col";
                    break;
                case EntityTypeEnum.EnTitle:
                    eName = "CF_News_Title";
                    break;
                case EntityTypeEnum.EnEntList:
                    eName = "CF_News_EntList";
                    break;
                case EntityTypeEnum.EnRecUnit:
                    eName = "CF_News_RecUnit";
                    break;
                #endregion

                #region 省政务中心使用表

                case EntityTypeEnum.EPWorkList:
                    eName = "WorkList";
                    break;
                case EntityTypeEnum.EPPeopleName:
                    eName = "PeopleName";
                    break;

                #endregion
                #region 政务大厅
                case EntityTypeEnum.EaOnLineRe:
                    eName = "CF_App_OnLineRe";
                    break;
                case EntityTypeEnum.EaOnLine:
                    eName = "CF_App_OnLine";
                    break;
                #endregion

                #region 施工许可证用表
                //dbcenter 
                case EntityTypeEnum.EPrjBaseInfo:
                    eName = "CF_Prj_BaseInfo";
                    break;
                case EntityTypeEnum.EPrjBaseInfoList:
                    eName = "CF_Prj_BaseInfoList";
                    break;
                case EntityTypeEnum.EPrjCerti:
                    eName = "CF_Prj_Certi";
                    break;
                case EntityTypeEnum.EPrjEnt:
                    eName = "CF_Prj_Ent";
                    break;
                case EntityTypeEnum.EwLink:
                    eName = "CF_City_Link";
                    break;
                case EntityTypeEnum.EsPrjList: //附件类型表
                    eName = "cf_sys_prjlist";
                    break;
                case EntityTypeEnum.EsPrjSDJC:
                    eName = "CF_Prj_SDJC";
                    break;
                //dbqualic
                case EntityTypeEnum.EqPrjBaseInfo:
                    eName = "CQ_Prj_BaseInfo";
                    break; ; ;
                case EntityTypeEnum.EqPrjData:
                    eName = "CF_AppPrj_Data";
                    break;
                case EntityTypeEnum.EqPrjEnt:
                    eName = "CF_AppPrj_Ent";
                    break;
                case EntityTypeEnum.EqPrjFile:
                    eName = "CF_AppPrj_File";
                    break;
                case EntityTypeEnum.EqPrjFileOther:
                    eName = "CF_AppPrj_FileOther";
                    break;
                case EntityTypeEnum.EqPubText:
                    eName = "CF_AppPub_Text";
                    break;
                case EntityTypeEnum.ReportInfo:
                    eName = "CF_ReportInfo";
                    break;



                #endregion

                #region 外埠入监理企业
                case EntityTypeEnum.EaJLEntOutCerti:
                    eName = "CF_JLRecord_Certi";
                    break;
                case EntityTypeEnum.EqEbJLProject:
                    eName = "CF_AppEnt_JLProject";
                    break;
                case EntityTypeEnum.EbJLProject:
                    eName = "CF_Ent_JLProject";
                    break;
                case EntityTypeEnum.EbPrjFile:
                    eName = "cf_Ent_PrjFile";
                    break;
                #endregion
                #region 家装企业+工程表
                case EntityTypeEnum.EbYLStatus:
                    eName = "CF_Ent_YieldStatus";
                    break;
                case EntityTypeEnum.EqEbHSProject:
                    eName = "CF_AppEnt_HSProject";
                    break;
                case EntityTypeEnum.EbHSProject:
                    eName = "CF_Ent_HSProject";
                    break;
                #endregion

                #region OA相关表
                case EntityTypeEnum.EOAConferenceApp:
                    eName = "CF_OA_ConferenceApp";
                    break;
                case EntityTypeEnum.EOASMSList:
                    eName = "CF_OA_SMSList";
                    break;
                case EntityTypeEnum.EOADeveReal:
                    eName = "CF_OA_DeveReal";
                    break;
                case EntityTypeEnum.EOADevelopment:

                    eName = "CF_OA_Development";
                    break;
                case EntityTypeEnum.EOABulletin:
                    eName = "CF_OA_Bulletin";
                    break;
                case EntityTypeEnum.EOABullReal:
                    eName = "CF_OA_BullReal";
                    break;
                case EntityTypeEnum.EOABulType:
                    eName = "CF_OA_BulType";
                    break;

                case EntityTypeEnum.EOAMsgReal:
                    eName = "CF_OA_MsgReal";
                    break;
                case EntityTypeEnum.EOAShortMsg:
                    eName = "CF_OA_ShortMsg";
                    break;
                case EntityTypeEnum.EOAOrganization:
                    eName = "CF_OA_Organization";
                    break;
                case EntityTypeEnum.EOAEmp:
                    eName = "CF_OA_Emp";
                    break;
                case EntityTypeEnum.EOAAddressList:
                    eName = "CF_OA_AddressList";
                    break;
                case EntityTypeEnum.EOAAddressListGroup:
                    eName = "CF_OA_AddressListGroup";
                    break;
                case EntityTypeEnum.EOACalendar:
                    eName = "CF_OA_Calendar";
                    break;
                case EntityTypeEnum.EOADirectory:
                    eName = "CF_OA_Directory";
                    break;
                case EntityTypeEnum.EOAEmailTO:
                    eName = "CF_OA_EmailTO";
                    break;
                case EntityTypeEnum.EOAInsideEmail:
                    eName = "CF_OA_InsideEmail";
                    break;
                case EntityTypeEnum.EOAJobPlan:
                    eName = "CF_OA_JobPlan";
                    break;
                case EntityTypeEnum.EOAJobPlanPerson:
                    eName = "CF_OA_JobPlanPerson";
                    break;
                case EntityTypeEnum.EOAJobPlanPostil:
                    eName = "CF_OA_JobPlanPostil";
                    break;
                case EntityTypeEnum.EOAJobPlanRevert:
                    eName = "CF_OA_JobPlanRevert";
                    break;
                case EntityTypeEnum.EOALunchType:
                    eName = "CF_OA_LunchType";
                    break;
                case EntityTypeEnum.EOAOrderLunch:
                    eName = "CF_OA_OrderLunch";
                    break;
                case EntityTypeEnum.EOAPersonGoto:
                    eName = "CF_OA_PersonGoto";
                    break;
                case EntityTypeEnum.EOAPresonLog:
                    eName = "CF_OA_PresonLog";
                    break;
                case EntityTypeEnum.EOATreePepo:
                    eName = "CF_OA_TreePepo";
                    break;
                case EntityTypeEnum.EOAUserGroup:
                    eName = "CF_OA_UserGroup";
                    break;
                case EntityTypeEnum.EOAWebDisk:
                    eName = "CF_OA_WebDisk";
                    break;
                case EntityTypeEnum.EOAWorkList:
                    eName = "CF_OA_WorkList";
                    break;
                case EntityTypeEnum.EOAWorkDateList:
                    eName = "CF_OA_WorkDateList";
                    break;
                case EntityTypeEnum.EOADic:
                    eName = "CF_OA_Dic";
                    break;
                case EntityTypeEnum.EOADicSub:
                    eName = "CF_OA_DicSub";
                    break;
                case EntityTypeEnum.EOAWork:
                    eName = "CF_OA_Work";
                    break;
                case EntityTypeEnum.EOAColuValue:
                    eName = "CF_OA_ColuValue";
                    break;


                case EntityTypeEnum.EOAInternetEmailConfig:
                    eName = "CF_OA_InternetEmailConfig";
                    break;
                case EntityTypeEnum.EOAChat:
                    eName = "CF_OA_Chat";
                    break;
                //流程相关表
                case EntityTypeEnum.EOAWorkFlow:
                    eName = "CF_OA_WorkFlow";
                    break;
                case EntityTypeEnum.EOAWorkFlowSub:
                    eName = "CF_OA_WorkFlowSub";
                    break;
                case EntityTypeEnum.EOAWorkFlowType:
                    eName = "CF_OA_WorkFlowType";
                    break;
                case EntityTypeEnum.EOAFlowExa:
                    eName = "CF_OA_FlowExa";
                    break;
                case EntityTypeEnum.EOAFlowExaList:
                    eName = "CF_OA_FlowExaList";
                    break;
                case EntityTypeEnum.ETalkProcess:
                    eName = "CF_Talk_Process";
                    break;
                case EntityTypeEnum.ETalkManage:
                    eName = "CF_Talk_TalkManage";
                    break;
                case EntityTypeEnum.ETalkRelation:
                    eName = "CF_Talk_Relation";
                    break;
                //.......
                #endregion

                #region 合同备案
                case EntityTypeEnum.EContractRecord:
                    eName = "CF_Prj_ContRecord";
                    break;
                #endregion

                #region Share库相关表
                case EntityTypeEnum.SHsBatchNo:
                    eName = "CF_Sys_BatchNo";
                    break;
                case EntityTypeEnum.SHsLock:
                    eName = "CF_Sys_Lock";
                    break;
                #endregion

                //电子监察
                #region 电子监察
                case EntityTypeEnum.JC_Acceptance:
                    eName = "CF_JC_Acceptance";
                    break;
                case EntityTypeEnum.JC_ApprovalProcess:
                    eName = "CF_JC_ApprovalProcess";
                    break;
                #endregion
                case EntityTypeEnum.EPrjFile:
                    eName = "CF_Prj_File";
                    break;
                case EntityTypeEnum.EPrjFileOther:
                    eName = "CF_Prj_FileOther";
                    break;
                case EntityTypeEnum.EcPrjBiddoc:
                    eName = "CF_Prj_Biddoc";
                    break;
                case EntityTypeEnum.EPrjItemBaseInfo:
                    eName = "CF_PrjItem_BaseInfo";
                    break;
                case EntityTypeEnum.EPrjText:
                    eName = "CF_Prj_Text";
                    break;
                case EntityTypeEnum.EPrjTendReco:
                    eName = "CF_Prj_TendReco";
                    break;
                case EntityTypeEnum.EPrjRegistration:
                    eName = "CF_Prj_Registration";
                    break;
                case EntityTypeEnum.EPrjProcInfo:
                    eName = "CF_Prj_ProcInfo";
                    break;
                case EntityTypeEnum.EPrjNoticeEntList:
                    eName = "CF_Prj_NoticeEntList";
                    break;
                case EntityTypeEnum.EPrjNotice:
                    eName = "CF_Prj_Notice";
                    break;
                case EntityTypeEnum.EPrjExPrLand:
                    eName = "CF_Prj_ExPrLand";
                    break;
                case EntityTypeEnum.EPrjEmp:
                    eName = "CF_Prj_Emp";
                    break;
                case EntityTypeEnum.EPrjDocuInfo:
                    eName = "CF_Prj_DocuInfo";
                    break;
                case EntityTypeEnum.EPrjData:
                    eName = "CF_Prj_Data";
                    break;
                case EntityTypeEnum.EPrjContRemark:
                    eName = "CF_Prj_ContRemark";
                    break;
                case EntityTypeEnum.EPrjChange:
                    eName = "CF_Prj_Change";
                    break;
                case EntityTypeEnum.EPrjRegWorks:
                    eName = "CF_Prj_RegWorks";
                    break;
                case EntityTypeEnum.EsMessage:
                    eName = "CF_Sys_Message";
                    break;
              
                default:
                    break;
            }
            if (eName == "")
            {
                throw new Exception("" + eType.ToString() + "获取表名失败");
            }
            return eName;
        }

        /// Function:以不同的结构返回实体:table,row,dict等;Author:jack;Time:2004-11-29
        /// <param name="eType">实体名称</param><param name="data">需要包装成实体的数据:例如row</param> 
        /// <param name="Relation">实体间的关系</param><param name="ecType">出入数据类型:例如row</param>

        public IEBase ConstructEntity(EntityTypeEnum eType, object data, IDictionary Relation, ConstructTypeEnum ecType)
        {
            IEBase eb = null;
            switch (eType)
            {
                #region sys

                case EntityTypeEnum.EsDic:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;


                case EntityTypeEnum.EsManageDeptInfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsManageDeptInfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsManageDeptInfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsManageDeptInfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsManageDeptInfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsManageDeptInfo();
                            break;
                    }
                    break;


                case EntityTypeEnum.EsDicClass:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDicClass();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDicClass((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDicClass((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDicClass((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDicClass();
                            break;
                    }
                    break;


                case EntityTypeEnum.EsSystemName:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsSystemName();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsSystemName((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsSystemName((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsSystemName((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsSystemName();
                            break;
                    }
                    break;
                //case EntityTypeEnum.EsPlatform:
                //    switch (ecType)
                //    {
                //        case ConstructTypeEnum.No:
                //            eb = new EsDic();
                //            break;
                //        case ConstructTypeEnum.Row:
                //            eb = new EsDic((DataRow)data);
                //            break;
                //        case ConstructTypeEnum.Dict:
                //            eb = new EsDic((IDictionary)data);
                //            break;
                //        case ConstructTypeEnum.RowRelation:
                //            eb = new EsDic((DataRow)data, Relation);
                //            break;
                //        default:
                //            eb = new EsDic();
                //            break;
                //    }
                //    break;

                case EntityTypeEnum.EsManageDept:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsManageDept();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsManageDept((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsManageDept((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsManageDept((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsManageDept();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsManageType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsManageType();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsManageType((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsManageType((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsManageType((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsManageType();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsUserLockInfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsUserLockInfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsUserLockInfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsUserLockInfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsUserLockInfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsUserLockInfo();
                            break;
                    }
                    break;



                case EntityTypeEnum.EsUserInfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsUserInfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsUserInfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsUserInfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsUserInfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsUserInfo();
                            break;
                    }
                    break;


                case EntityTypeEnum.EsUserFinance:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsUserFinance();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsUserFinance((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsUserFinance((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsUserFinance((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsUserFinance();
                            break;
                    }
                    break;


                case EntityTypeEnum.EsUserRight:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsUserRight();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsUserRight((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsUserRight((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsUserRight((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsUserRight();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsRole:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsRole();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsRole((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsRole((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsRole((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsRole();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsUser:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsUser();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsUser((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsUser((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsUser((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsUser();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsCode:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsCode();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsCode((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsCode((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsCode((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsCode();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsMenu:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsMenu();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsMenu((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsMenu((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsMenu((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsMenu();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsQualiLevel:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsQualiLevel();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsQualiLevel((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsQualiLevel((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsQualiLevel((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsQualiLevel();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsTree:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsTree();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsTree((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsTree((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsTree((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsTree();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsBadActionCode:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsBadActionCode();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsBadActionCode((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsBadActionCode((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsBadActionCode((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsBadActionCode();
                            break;
                    }
                    break;




                case EntityTypeEnum.EsObject:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsObject();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsObject((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsObject((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsObject((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsObject();
                            break;
                    }
                    break;
                case EntityTypeEnum.EsRoleRight:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsRoleRight();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsRoleRight((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsRoleRight((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsRoleRight((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsRoleRight();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsProjectType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsProjectType();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsProjectType((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsProjectType((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsProjectType((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsProjectType();
                            break;
                    }
                    break;

                case EntityTypeEnum.EsAppStand:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsAppStand();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsAppStand((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsAppStand((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsAppStand((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsAppStand();
                            break;
                    }
                    break;



                #endregion

                #region app
                case EntityTypeEnum.EaAppBackIdea:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppBackIdea();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppBackIdea((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppBackIdea((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppBackIdea((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppBackIdea();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaProcessRecord:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaProcessRecord();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaProcessRecord((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaProcessRecord((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaProcessRecord((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaProcessRecord();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaSubFlow:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaSubFlow();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaSubFlow((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaSubFlow((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaSubFlow((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaSubFlow();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaProcessInstance:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaProcessInstance();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaProcessInstance((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaProcessInstance((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaProcessInstance((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaProcessInstance();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaQualiType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQualiType();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQualiType((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQualiType((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQualiType((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQualiType();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaProcess:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaProcess();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaProcess((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaProcess((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaProcess((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaProcess();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaQualiLevel:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQualiLevel();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQualiLevel((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQualiLevel((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQualiLevel((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQualiLevel();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaManageType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaManageType();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaManageType((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaManageType((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaManageType((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaManageType();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAcceptBook:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAcceptBook();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAcceptBook((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAcceptBook((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAcceptBook((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAcceptBook();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaQuailAppList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQuailAppList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQuailAppList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQuailAppList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQuailAppList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQuailAppList();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaQualiCondition:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQualiCondition();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQualiCondition((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQualiCondition((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQualiCondition((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQualiCondition();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaQualiConditionData:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQualiConditionData();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQualiConditionData((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQualiConditionData((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQualiConditionData((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQualiConditionData();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaQualiConditionOther:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaQualiConditionOther();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaQualiConditionOther((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaQualiConditionOther((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaQualiConditionOther((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaQualiConditionOther();
                            break;
                    }

                    break;


                case EntityTypeEnum.EaCheck:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaCheck();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaCheck((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaCheck((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaCheck((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaCheck();
                            break;
                    }

                    break;

                case EntityTypeEnum.EaNo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaNo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaNo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaNo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaNo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaNo();
                            break;
                    }

                    break;

                case EntityTypeEnum.EaBatchNo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaBatchNo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaBatchNo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaBatchNo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaBatchNo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaBatchNo();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAppBatchNo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppBatchNo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppBatchNo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppBatchNo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppBatchNo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppBatchNo();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaProcessInstanceBackup:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaProcessInstanceBackup();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaProcessInstanceBackup((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaProcessInstanceBackup((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaProcessInstanceBackup((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaProcessInstanceBackup();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaProcessRecordBackup:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaProcessRecordBackup();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaProcessRecordBackup((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaProcessRecordBackup((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaProcessRecordBackup((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaProcessRecordBackup();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAppProcessPublic:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppProcessPublic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppProcessPublic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppProcessPublic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppProcessPublic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppProcessPublic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaAppProcessComplaint:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppProcessComplaint();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppProcessComplaint((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppProcessComplaint((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppProcessComplaint((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppProcessComplaint();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAppPrintIc:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppPrintIc();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppPrintIc((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppPrintIc((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppPrintIc((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppPrintIc();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAppAcceptBook:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppAcceptBook();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppAcceptBook((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppAcceptBook((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppAcceptBook((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppAcceptBook();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaPrintRecord:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaPrintRecord();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaPrintRecord((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaPrintRecord((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaPrintRecord((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaPrintRecord();
                            break;
                    }
                    break;

                case EntityTypeEnum.EaAppActionRecord:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaAppActionRecord();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaAppActionRecord((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaAppActionRecord((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaAppActionRecord((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaAppActionRecord();
                            break;
                    }
                    break;




                    break;
                #endregion

                #region eb

                case EntityTypeEnum.EbBaseInfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbBaseInfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbBaseInfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbBaseInfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbBaseInfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbBaseInfo();
                            break;
                    }
                    break;




                case EntityTypeEnum.EbBadAction:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbBadAction();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbBadAction((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbBadAction((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbBadAction((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbBadAction();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbBankAccount:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbBankAccount();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbBankAccount((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbBankAccount((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbBankAccount((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbBankAccount();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbBaseInfoOther:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbBaseInfoOther();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbBaseInfoOther((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbBaseInfoOther((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbBaseInfoOther((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbBaseInfoOther();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbCompact:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbCompact();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbCompact((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbCompact((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbCompact((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbCompact();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbDevice:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbDevice();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbDevice((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbDevice((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbDevice((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbDevice();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbFinance:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbFinance();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbFinance((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbFinance((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbFinance((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbFinance();
                            break;
                    }
                    break;





                case EntityTypeEnum.EbQualiCerti:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbQualiCerti();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbQualiCerti((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbQualiCerti((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbQualiCerti((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbQualiCerti();
                            break;
                    }
                    break;


                case EntityTypeEnum.EbQualiCertiTrade:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbQualiCertiTrade();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbQualiCertiTrade((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbQualiCertiTrade((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbQualiCertiTrade((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbQualiCertiTrade();
                            break;
                    }
                    break;



                case EntityTypeEnum.EbBadActionCode:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbBadActionCode();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbBadActionCode((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbBadActionCode((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbBadActionCode((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbBadActionCode();
                            break;
                    }
                    break;



                case EntityTypeEnum.EbCreditLevel:

                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbCreditLevel();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbCreditLevel((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbCreditLevel((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbCreditLevel((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbCreditLevel();
                            break;
                    }
                    break;



                case EntityTypeEnum.EbDesignReport:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbDesignReport();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbDesignReport((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbDesignReport((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbDesignReport((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbDesignReport();
                            break;
                    }
                    break;

                case EntityTypeEnum.EbCheckParameter:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EbCheckParameter();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EbCheckParameter((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EbCheckParameter((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EbCheckParameter((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EbCheckParameter();
                            break;
                    }
                    break;






                #endregion

                #region ee

                case EntityTypeEnum.EeBaseinfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EeBaseinfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EeBaseinfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EeBaseinfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EeBaseinfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EeBaseinfo();
                            break;
                    }
                    break;


                case EntityTypeEnum.EeBadAction:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EeBadAction();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EeBadAction((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EeBadAction((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EeBadAction((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EeBadAction();
                            break;
                    }
                    break;


                case EntityTypeEnum.EeCerti:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EeCerti();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EeCerti((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EeCerti((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EeCerti((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EeCerti();
                            break;
                    }
                    break;

                case EntityTypeEnum.EeGoodAction:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EeGoodAction();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EeGoodAction((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EeGoodAction((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EeGoodAction((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EeGoodAction();
                            break;
                    }
                    break;




                #endregion


                #region 新闻相关表

                case EntityTypeEnum.EnComment:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnComment();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnComment((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnComment((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnComment((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnComment();
                            break;
                    }
                    break;


                case EntityTypeEnum.EnEntList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnEntList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnEntList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnEntList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnEntList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnEntList();
                            break;
                    }
                    break;

                case EntityTypeEnum.EnContent:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnContent();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnContent((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnContent((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnContent((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnContent();
                            break;
                    }
                    break;


                case EntityTypeEnum.EnCol:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnCol();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnCol((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnCol((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnCol((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnCol();
                            break;
                    }
                    break;


                case EntityTypeEnum.EnTitle:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnTitle();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnTitle((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnTitle((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnTitle((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnTitle();
                            break;
                    }
                    break;

                case EntityTypeEnum.EnRecUnit:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EnRecUnit();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EnRecUnit((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EnRecUnit((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EnRecUnit((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EnRecUnit();
                            break;
                    }
                    break;

                #endregion

                #region 行政审核中心表

                case EntityTypeEnum.EPWorkList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPWorkList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPWorkList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPWorkList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPWorkList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPWorkList();
                            break;
                    }
                    break;


                case EntityTypeEnum.EPPeopleName:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPPeopleName();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPPeopleName((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPPeopleName((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPPeopleName((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPPeopleName();
                            break;
                    }
                    break;

                #endregion
                #region 政务大厅
                case EntityTypeEnum.EaOnLineRe:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaOnLineRe();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaOnLineRe((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaOnLineRe((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaOnLineRe((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaOnLineRe();
                            break;
                    }
                    break;
                case EntityTypeEnum.EaOnLine:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EaOnLine();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EaOnLine((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EaOnLine((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EaOnLine((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EaOnLine();
                            break;
                    }
                    break;
                #endregion


                #region 施工许可证用表
                //dbcenter
                case EntityTypeEnum.EPrjBaseInfo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPrjBaseInfo();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPrjBaseInfo((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPrjBaseInfo((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPrjBaseInfo((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPrjBaseInfo();
                            break;
                    }
                    break;
                case EntityTypeEnum.EwLink:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EwLink();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EwLink((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EwLink((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EwLink((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EwLink();
                            break;
                    }
                    break;
                case EntityTypeEnum.EPrjBaseInfoList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPrjBaseInfoList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPrjBaseInfoList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPrjBaseInfoList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPrjBaseInfoList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPrjBaseInfoList();
                            break;
                    }
                    break;
                case EntityTypeEnum.EPrjCerti:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPrjCerti();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPrjCerti((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPrjCerti((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPrjCerti((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPrjCerti();
                            break;
                    }
                    break;
                case EntityTypeEnum.EPrjEnt:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EPrjEnt();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EPrjEnt((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EPrjEnt((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EPrjEnt((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EPrjEnt();
                            break;
                    }
                    break;
                case EntityTypeEnum.EsPrjList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsPrjList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsPrjList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsPrjList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsPrjList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsPrjList();
                            break;
                    }
                    break;



                #endregion




                #region OA相关
                case EntityTypeEnum.EOADeveReal:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOADevelopment:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOABulletin:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOABullReal:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOABulType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAMsgReal:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAShortMsg:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAOrganization:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAEmp:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAAddressList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAAddressListGroup:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOACalendar:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOADirectory:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAEmailTO:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAInsideEmail:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAJobPlan:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAJobPlanPerson:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAJobPlanPostil:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAJobPlanRevert:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOALunchType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAOrderLunch:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAPersonGoto:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAPresonLog:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOATreePepo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAUserGroup:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWebDisk:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }

                    break;
                case EntityTypeEnum.EOAWorkList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWorkDateList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOADic:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOADicSub:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAInternetEmailConfig:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                //工作和流程
                case EntityTypeEnum.EOAColuValue:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWork:

                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                case EntityTypeEnum.EOAFlowExa:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAFlowExaList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWorkFlow:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWorkFlowSub:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAWorkFlowType:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EOAChat:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;


                case EntityTypeEnum.ETalkProcess:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.ETalkManage:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.ETalkRelation:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                //....

                #endregion



                #region Share库相关表

                case EntityTypeEnum.SHsBatchNo:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.SHsLock:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                #endregion

                #region 电子监察

                case EntityTypeEnum.JC_Acceptance:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.JC_ApprovalProcess:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;

                #endregion
                case EntityTypeEnum.EPrjFile:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EPrjFileOther:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                case EntityTypeEnum.EqList:
                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EqList();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EqList((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EqList((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EqList((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EqList();
                            break;
                    }
                    break;


                case EntityTypeEnum.EqIdea:
                case EntityTypeEnum.EPrjItemBaseInfo:
                case EntityTypeEnum.EPrjText:
                case EntityTypeEnum.EPrjTendReco:
                case EntityTypeEnum.EPrjRegistration:
                case EntityTypeEnum.EPrjProcInfo:
                case EntityTypeEnum.EPrjNoticeEntList:
                case EntityTypeEnum.EPrjNotice:
                case EntityTypeEnum.EPrjExPrLand:
                case EntityTypeEnum.EPrjEmp:
                case EntityTypeEnum.EPrjDocuInfo:
                case EntityTypeEnum.EPrjData:
                case EntityTypeEnum.EPrjContRemark:
                case EntityTypeEnum.EPrjChange:
                case EntityTypeEnum.EPrjRegWorks:

                    switch (ecType)
                    {
                        case ConstructTypeEnum.No:
                            eb = new EsDic();
                            break;
                        case ConstructTypeEnum.Row:
                            eb = new EsDic((DataRow)data);
                            break;
                        case ConstructTypeEnum.Dict:
                            eb = new EsDic((IDictionary)data);
                            break;
                        case ConstructTypeEnum.RowRelation:
                            eb = new EsDic((DataRow)data, Relation);
                            break;
                        default:
                            eb = new EsDic();
                            break;
                    }
                    break;
                default:
                    break;
            }
            return eb;
        }
        #endregion
    }
}
