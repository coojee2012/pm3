using System;
using System.Collections;

namespace Approve.EntityBase
{
    /// <summary>
    /// Function:实体类基类的接口
    /// Author:Wengmj,jack
    /// Time:2004-11-16
    /// </summary>
    public enum EntityTypeEnum  //实体类型枚举
    {
        EBase,
        EEnterpriseBase,

        #region 外埠入辽监理企业 证书库
        EaJLEntOutCerti,
        EqEbJLProject,//业绩
        EbJLProject,//业绩
        EbPrjFile,//工程文件
        #endregion
        #region 家装企业 月报 工程库
        EbYLStatus,//家装企业月报表
        EqEbHSProject,//家装企业工程库
        EbHSProject,//家装企业工程库
        #endregion
        #region 施工许可证用表
        EPrjBaseInfo,
        EPrjBaseInfoList,
        EPrjCerti,
        EPrjEnt,
        EsPrjList,
        //前台网站市级联系人

        EsDic,
        EsPrjSDJC,
        EsDicClass,
        EsSystemName,
        EsManageDept,
        EsManageType,
        EsUserLockInfo,
        EsUserFinance,
        EsUserRight,
        EsUser,
        EsRole,
        EsBadActionCode,
        EsCode,
        EsMenu,
        EsQualiLevel,
        EsTree,
        EsStatis,
        EsStatisInfo,
        EsObject,
        EsRoleRight,
        EsManageDeptInfo,
        EsUserInfo,
        EsCstUser,
        #endregion

        #region EApp
        EaProcessRecord,
        EaProcessRecordBackup,
        EaAppAcceptBook,
        EaPrintRecord,

        EaSubFlow,
        EaProcessInstance,
        EaProcessInstanceBackup,

        EaQualiType,
        EaProcess,
        EaQualiLevel,
        EaManageType,
        EaAcceptBook,
        EaQuailAppList,
        EaQualiCondition,
        EaQualiConditionData,
        EaQualiConditionOther,
        EaCheck,
        EaNo,
        EaBatchNo,
        EaAppBatchNo,
        /// <summary>
        /// 打回意见(CF_App_BackIdea)
        /// </summary>
        EaAppBackIdea,

        EaAppProcessPublic,
        EaAppProcessComplaint,
        EaAppPrintIc,
        EaAppActionRecord,
        #endregion

        #region 企业相关的表
        //企业相关的表
        EbBaseInfo,
        EbOutBaseInfo, //出川企业信息
        EbBadAction,
        EbBankAccount,
        EbBaseInfoOther,
        EbCompact,
        EbDevice,
        EbFinance,
        EbGoodAction,
        EbInvestPerson,
        EbLeader,
        EbGoodAchieve,

        EbSingleProject,
        EbProject,
        EbProjectCerti,
        EbProjectManager,
        EbProjectOther,

        EbProjectProcessIdear, //
        EbProjectAdd, //
        EbProjectAppEmployee, //
        EbProjectDetail, //
        EbProjectEmployee, //
        EbProjectIdea, //
        EbProjectResult, //

        EbProjectBackupIdea, //特殊施工图工程备案审查用
        EbOffence,

        EbProjectBid,



        EbQualiCerti,
        EbQualiCertiTrade,
        EbQuality,
        EbSafetyCerti,
        EbBadActionCode,
        EbZCFZ,
        //EbInvestPerson,

        EbCreditLevel,
        EbProjBackUp,


        EbDesignReport,
        EbKCSJFloatReport,
        EbKCSJIntReport,

        EbCheckParameter,
        EqEbQualiCertiTrade,
        EbSurvey,
        EbSurveyTime,
        #endregion

        #region 人员相关的表
        //人员相关的表
        EeBaseinfo,
        EeBadAction,
        EeBaseinfoOther,
        EeCerti,
        EeContinueStudy,
        EeGoodAction,
        EeProjectBuildStandard,
        EeResume,
        EeWorkExperience,
        EeWorkAchievement,
        EeRegistCerti,
        EeRegistSpecial,

        EeSafeBaseinfo,
        //技能执业人员
        EeProPer,
        EeProTech,
        EeProCheck,
        EeConstructUserInfo,
        EeIni,
        EeConstructChangeRecord,
        #endregion

        #region 公用表
        //公用表
        EPText,
        #endregion

        #region  //留言表
        ElInfo,
        #endregion

        #region dbQuali数据库表
        EqDetail,
        EqList,
        EqIdea,
        EqCheckList,
        EqEeBaseinfo,
        EqEeSafeBaseinfo,
        EqEeRegistCerti,
        EqEeRegistSpecial,
        EqEeResume,
        EqEeWorkExperience,
        EqEeWorkAchievement,
        EqEbBadAction,
        EqEbBaseInfo,
        EqEbBaseInfoOther,
        EqEbDevice,
        EqEbFinance,
        EqEbGoodAction,
        EqEbLeader,
        EqEbProject,
        EqEbProjectOther,
        EqPText,
        EqPPic,
        EqEbZCFZ,
        EqEbProjectCerti,
        EqEbProjectManager,
        EqEbBaseInfoChange,
        EqAppChange,
        EqSafetyCerti,

        EqProjectProcessIdear, //
        EqProjectAdd, //
        EqProjectAppEmployee, //
        EqProjectDetail, //
        EqProjectEmployee, //
        EqProjectIdea, //
        EqProjectResult, //


        EqEbBankAccount,
        EqEbInvestPerson,
        EqEbQuality,
        EqEbCreditLevel,
        EqEbProjBackUp,

        EqEbGoodAchieve,
        EqEbApplyAchieve,
        EqEbApplyDevice,
        EqEbApplyEmployee,
        EqEbApplyProject,
        EqEbApplySpecial,
        EqEbApplyTrade,
        EqEeConstructAppDetial,


        EqEbQualiCerti,
        EqEbCheckParameter,

        EqEbChangeRecord, //记录企业证书变更信息

        //////施工许可证用表///
        EqPrjBaseInfo,
        EqPrjData,
        EqPrjEnt,
        EqPrjFile,
        EqPrjFileOther,
        EqPubText,
        ReportInfo,
        ////////////////////////

        #endregion

        #region 新闻相关表
        EnComment,
        EnContent,
        EnCol,
        EnTitle,
        EnEntList,
        EnRecUnit,
        #endregion

      
        EsHolidays,
        //自动审查表
        EsProjectType,
        EsAppStand,



        #region 三类人员
        EqEeDetail,
        EqPersonChangeDetial,
        EqEeOtherInfo,
        EqEeContinueStudy,
        EqEeGoodAction,
        EqEeBadAction,
        EeRemoveRecord,
        EePersonBaseInfo,
        EePersonSpecial,
        #endregion

        //增加一个传递数据的实体
        EData,

        //政务中心数据库使用表
        EPWorkList,
        EPPeopleName,
        CityReport,
        CityReportDetails,
        #region 建造师
        EqEeEntUserInfo,
        EqEeOtherRegist,
        EeOtherRegist,
        #endregion
        #region 政务公开
        EaOnLineRe,
        EaOnLine,
        #endregion
        EwLink,
        EqPrjRegistration,
        EqPrjRegWorks,
        EqPrjEmp,
        EcPrjOrder,
        #region OA相关表
        EOAConferenceApp,
        EOAOrganization,
        EOAEmp,
        EOAAddressList,
        EOAAddressListGroup,
        EOACalendar,
        EOADirectory,
        EOAEmailTO,
        EOAInsideEmail,
        EOAJobPlan,
        EOAJobPlanPerson,
        EOAJobPlanPostil,
        EOAJobPlanRevert,
        EOALunchType,
        EOAOrderLunch,
        EOAPersonGoto,
        EOAPresonLog,
        EOATreePepo,
        EOAUserGroup,
        EOAWebDisk,
        EOAWorkList,
        EOAWorkDateList,
        EOAWorkFlowType,
        EOAWorkFlow,
        EOAWorkFlowSub,
        EOAFlowExa,
        EOAFlowExaList,
        EOADic,
        EOADicSub,
        EOAWork,
        EOAColuValue,
        EOAMsgReal,
        EOAShortMsg,
        EOABulletin,
        EOABullReal,
        EOABulType,
        EOADevelopment,
        EOADeveReal,
        EOAInternetEmailConfig,//邮箱配置表
        EOAChat,
        EOASMSList,//短信息表

        ETalkProcess,//讨论留言
        ETalkManage,//列表
        ETalkRelation,//关联人员
        #endregion

        #region Share库相关表
        SHsBatchNo,//批次表
        SHsLock,//加密锁表
        #endregion
        #region 合同备案
        EContractRecord,
        #endregion


        //电子监察
        #region 电子监察

        JC_Acceptance,
        JC_ApprovalProcess,

        #endregion

        EPrjFile,
        EPrjFileOther,
        EcPrjBiddoc,
        EPrjItemBaseInfo,
        EPrjText,
        EPrjTendReco,
        EPrjRegistration,
        EPrjProcInfo,
        EPrjNoticeEntList,
        EPrjNotice,
        EPrjExPrLand,
        //EPrjEnt,
        EPrjEmp,
        EPrjDocuInfo,
        EPrjData,
        EPrjContRemark,
        EPrjChange,
        EPrjRegWorks,
        //EPrjCerti,
        //EPrjBaseInfo,
        EsMessage,
       
 
    }


    public enum ConstructTypeEnum  //实体结构类型枚举
    {
        No,
        Dict,
        Row,
        RowRelation
    }

    public enum SaveOptionEnum   //sql操作类型
    {
        Insert,
        Update,
        Unknown
    }

    public interface IEBase   //
    {
        string FId               ////实体id
        {
            get;
            set;
        }

        EntityTypeEnum EntityType  ////实体类型（实体名称）
        {
            get;
        }

        System.DateTime FTime  ////插入时间
        {
            get;
            set;
        }

        bool FIsDeleted   ////是否删除
        {
            get;
            set;
        }

        bool IsReduce    ////是否裁剪
        {
            get;
            set;
        }

        string ReduceOption  //裁剪选项
        {
            get;
            set;
        }
        string sField      //获得字段名的总称
        {
            get;
            set;
        }
        ////获得实体数据 m_dict;
        IDictionary GetData();  ////获得实体数据 m_dict;
    }




}
