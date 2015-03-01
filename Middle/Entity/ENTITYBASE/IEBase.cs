using System;
using System.Collections;

namespace Approve.EntityBase
{
    /// <summary>
    /// Function:ʵ�������Ľӿ�
    /// Author:Wengmj,jack
    /// Time:2004-11-16
    /// </summary>
    public enum EntityTypeEnum  //ʵ������ö��
    {
        EBase,
        EEnterpriseBase,

        #region �Ⲻ���ɼ�����ҵ ֤���
        EaJLEntOutCerti,
        EqEbJLProject,//ҵ��
        EbJLProject,//ҵ��
        EbPrjFile,//�����ļ�
        #endregion
        #region ��װ��ҵ �±� ���̿�
        EbYLStatus,//��װ��ҵ�±���
        EqEbHSProject,//��װ��ҵ���̿�
        EbHSProject,//��װ��ҵ���̿�
        #endregion
        #region ʩ�����֤�ñ�
        EPrjBaseInfo,
        EPrjBaseInfoList,
        EPrjCerti,
        EPrjEnt,
        EsPrjList,
        //ǰ̨��վ�м���ϵ��

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
        /// ������(CF_App_BackIdea)
        /// </summary>
        EaAppBackIdea,

        EaAppProcessPublic,
        EaAppProcessComplaint,
        EaAppPrintIc,
        EaAppActionRecord,
        #endregion

        #region ��ҵ��صı�
        //��ҵ��صı�
        EbBaseInfo,
        EbOutBaseInfo, //������ҵ��Ϣ
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

        EbProjectBackupIdea, //����ʩ��ͼ���̱��������
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

        #region ��Ա��صı�
        //��Ա��صı�
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
        //����ִҵ��Ա
        EeProPer,
        EeProTech,
        EeProCheck,
        EeConstructUserInfo,
        EeIni,
        EeConstructChangeRecord,
        #endregion

        #region ���ñ�
        //���ñ�
        EPText,
        #endregion

        #region  //���Ա�
        ElInfo,
        #endregion

        #region dbQuali���ݿ��
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

        EqEbChangeRecord, //��¼��ҵ֤������Ϣ

        //////ʩ�����֤�ñ�///
        EqPrjBaseInfo,
        EqPrjData,
        EqPrjEnt,
        EqPrjFile,
        EqPrjFileOther,
        EqPubText,
        ReportInfo,
        ////////////////////////

        #endregion

        #region ������ر�
        EnComment,
        EnContent,
        EnCol,
        EnTitle,
        EnEntList,
        EnRecUnit,
        #endregion

      
        EsHolidays,
        //�Զ�����
        EsProjectType,
        EsAppStand,



        #region ������Ա
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

        //����һ���������ݵ�ʵ��
        EData,

        //�����������ݿ�ʹ�ñ�
        EPWorkList,
        EPPeopleName,
        CityReport,
        CityReportDetails,
        #region ����ʦ
        EqEeEntUserInfo,
        EqEeOtherRegist,
        EeOtherRegist,
        #endregion
        #region ���񹫿�
        EaOnLineRe,
        EaOnLine,
        #endregion
        EwLink,
        EqPrjRegistration,
        EqPrjRegWorks,
        EqPrjEmp,
        EcPrjOrder,
        #region OA��ر�
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
        EOAInternetEmailConfig,//�������ñ�
        EOAChat,
        EOASMSList,//����Ϣ��

        ETalkProcess,//��������
        ETalkManage,//�б�
        ETalkRelation,//������Ա
        #endregion

        #region Share����ر�
        SHsBatchNo,//���α�
        SHsLock,//��������
        #endregion
        #region ��ͬ����
        EContractRecord,
        #endregion


        //���Ӽ��
        #region ���Ӽ��

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


    public enum ConstructTypeEnum  //ʵ��ṹ����ö��
    {
        No,
        Dict,
        Row,
        RowRelation
    }

    public enum SaveOptionEnum   //sql��������
    {
        Insert,
        Update,
        Unknown
    }

    public interface IEBase   //
    {
        string FId               ////ʵ��id
        {
            get;
            set;
        }

        EntityTypeEnum EntityType  ////ʵ�����ͣ�ʵ�����ƣ�
        {
            get;
        }

        System.DateTime FTime  ////����ʱ��
        {
            get;
            set;
        }

        bool FIsDeleted   ////�Ƿ�ɾ��
        {
            get;
            set;
        }

        bool IsReduce    ////�Ƿ�ü�
        {
            get;
            set;
        }

        string ReduceOption  //�ü�ѡ��
        {
            get;
            set;
        }
        string sField      //����ֶ������ܳ�
        {
            get;
            set;
        }
        ////���ʵ������ m_dict;
        IDictionary GetData();  ////���ʵ������ m_dict;
    }




}
