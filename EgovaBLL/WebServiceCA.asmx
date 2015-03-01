<%@ WebService Language="C#" Class="BJCA_Service" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using System.Linq;
/// <summary>
///WebServiceCA 的摘要说明

/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class BJCA_Service : System.Web.Services.WebService
{

    ProjectData.ProjectDB db = new ProjectData.ProjectDB();
    public BJCA_Service()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    public CredentialSoapHeader Credentials
    {
        get { return m_credentials; }
        set { m_credentials = value; }
    }
    private CredentialSoapHeader m_credentials;// 保存SOAP中验证身份的SOAPHeader

    [WebMethod]
    [SoapHeader("Credentials")]
    public int DoCertEnd_BJCA(string strUserid, string strGuid, CCertInfo certinfo, CUserInfo userinfo, string strSignValue)
    {
        string sLog = "";
        int nRet = 0;
        try
        {
            //获取数据签名原文
            string strOrign = GetSignSoureData(strUserid, strGuid, certinfo, userinfo);
            //验证数据签名
            int nB = ValidateSignValue(strOrign, strSignValue);
            if (nB != 0)
            {
                sLog += String.Format("数字签名验证失败码 {0}", nB);
                //验证数据签名失败.
                nRet = -1;
            }

            //下面进行您的处理

            string QYName = userinfo.UnitName; //企业名称
            string ZZJGDM = userinfo.PaperID; //组织机构代码
            string CAMMKH = certinfo.Envsn; //CA密码卡号
            string CASZZSBH = certinfo.UserUniqueID; //CA数字证书编号
            string[] UserID_S = userinfo.UserId.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries); //用户ID
            string UserID = "";
            if (UserID_S.Length > 0)
                UserID = UserID_S[0];

            sLog += "得到信息: " + QYName + "_" + ZZJGDM + "_" + CAMMKH + "_" + CASZZSBH + "_" + UserID + "_" + certinfo.UserUniqueID;

            //判断数据表（ZZJGAndCAInfo）中是否存在此数据   不存在插入数据

            //if (!SelCaInfoByUserID(UserID, CAMMKH))
            //{
            //插入数据
            InsertZZJGAndCAInfo(QYName, ZZJGDM, CAMMKH, CASZZSBH);
            //}

            nRet = 0;
        }
        catch (Exception e)
        {
            nRet = -2;
            sLog += "\r\n" + e.ToString();
        }
        ProjectData.DataLog.Write(ProjectData.LogType.Info, ProjectData.LogSort.System, "写入CA数字证书编号", sLog);
      
        return nRet;
    }



    [WebMethod]
    public string Get_YXQJLB(int JoinType)
    {
        string sQYList = "";
        string mSql = "";

        try
        {

            //JoinType=1--快到期企业，JoinType=2--资质失效企业
            StringBuilder sb = new StringBuilder();
            mSql = @" SELECT TOP 6 FEntName FROM CF_EntCerti_YXQJLB WHERE FNewJoinType={0} AND ISNULL(FIsRemove,0)<>1
                           ORDER BY FNewJoinTime,fqylx";
            mSql = string.Format(mSql, JoinType);
            //sQYList = rc.GetTable(mSql).ToString();

        }
        catch (Exception e)
        {

        }

        return sQYList;
    }

    [WebMethod]
    public string Get_JIANDUFC()
    {
        string sQYList = "";
        string mSql = "";

        try
        {

            //JoinType=1--快到期企业，JoinType=2--资质失效企业
            StringBuilder sb = new StringBuilder();
            mSql = @" SELECT TOP 6 FEntName FROM CF_JIANDUFC_JLB WHERE ISNULL(FIsRemove,0)<>1
                           ORDER BY FNewJoinTime,fqylx";
            mSql = string.Format(mSql);
            //sQYList = rc.GetTable(mSql).ToString();

        }
        catch (Exception e)
        {

        }

        return sQYList;
    }



    RCenter rc = new RCenter();

    /// <summary>
    /// 插入（ZZJGAndCAInfo）数据   
    /// </summary>
    /// <returns></returns>
    /// </summary>
    /// <param name="QYName">企业名称</param>
    /// <param name="ZZJGDM">组织机构代码</param>
    /// <param name="CAMMKH">CA密码卡号</param>
    /// <param name="CASZZSBH">CA数字证书编号</param>
    /// <returns></returns>
    public bool InsertZZJGAndCAInfo(string QYName, string ZZJGDM, string CAMMKH, string CASZZSBH)
    {
        StringBuilder sb = new StringBuilder();
        var UserCA = db.CF_Sys_UserCA.Where(t => t.FCACardId == CAMMKH);
        foreach (var item in UserCA)
        {
            item.FCANumber = CASZZSBH;
        }
        db.SubmitChanges();
        return true;
    }


    /// <summary>
    /// 通过UserID  判断（ZZJGAndCAInfo）中是否存在数据  
    /// </summary>
    /// <returns></returns>
    public bool SelCaInfoByUserID(string userid, string CardNo)
    {
        if (string.IsNullOrEmpty(userid))
        {
            return true;
        }

        var result = from c in db.CF_Sys_UserCA
                     join u in db.CF_Sys_User on c.FUserID equals u.FID
                     where c.FCACardId == CardNo && c.FUserID == userid
                     select c;
        return result.Any();
    }


    ///// <summary>
    ///// 插入（SQDWZL）表数据  
    ///// </summary>
    ///// <returns></returns>
    //public bool InsertSQDWZL(CUserInfo userinfo ,CCertInfo certinfo)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("select COUNT(*)  from linker_NJS.JKCWFDB_WORK_NJS.dbo.SQDWZL  where USERNAME='" + userinfo.UserId + "'");
    //    return rc.PExcute(sb.ToString());

    //}



    /// <summary>
    /// 通过UserID  判断（SQDWZL）中是否存在数据  
    /// </summary>
    /// <returns></returns>
    public bool SelSQDWZLByUserID(string userid, string CardNo)
    {
        if (string.IsNullOrEmpty(userid))
        {
            return true;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from linker_NJS.JKCWFDB_WORK_NJS.dbo.SQDWZL  where CAMMKH='" + CardNo + "' and USERNAME='" + userid + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 获取数据签名原文
    /// </summary>
    /// <param name="strUserid"></param>
    /// <param name="strGuid"></param>
    /// <param name="cert"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    private string GetSignSoureData(string strUserid, string strGuid, CCertInfo cert, CUserInfo user)
    {
        string strTemp = strUserid + strGuid;
        strTemp += cert.GetCertInfoString();
        strTemp += user.GetUserInfoString();
        return strTemp;
    }

    /// <summary>
    /// 验证数据签名
    /// </summary>
    /// <param name="strOri">数据签名原文</param>
    /// <param name="strSignValue">签名值</param>
    /// <returns></returns>
    private int ValidateSignValue(string strOrign, string strSignValue)
    {
        //数字证书，由BJCA提供，实际上会变更，请放到配置文件中
        string strCert = System.Configuration.ConfigurationManager.AppSettings["BJCA_Cert"];
        BCACOMLib.SecurityEngineV2Class bca = new BCACOMLib.SecurityEngineV2Class();
        try
        {
            bca.SetWebAppName("scjstca");
            int rv = bca.VerifySignedData(strCert, strOrign, strSignValue);
            return rv;
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(bca);
        }
    }

}

#region 数据实体

[Serializable]
public class CCertInfo
{
    public CCertInfo()
    {
        m_ErrorCode = "";
        m_Envsn = "";
        m_UserUniqueID = "";
        m_EncCertSN = "";
        m_EncCert = "";
        m_SignCertSN = "";
        m_SignCert = "";
    }

    private string m_ErrorCode;
    private string m_Envsn;
    private string m_UserUniqueID;
    private string m_EncCertSN;
    private string m_EncCert;
    private string m_SignCertSN;
    private string m_SignCert;

    public string ErrorCode
    {
        get { return m_ErrorCode; }
        set { m_ErrorCode = value.Trim(); }
    }

    public string Envsn
    {
        get { return m_Envsn; }
        set { m_Envsn = value.Trim(); }
    }

    public string UserUniqueID
    {
        get { return m_UserUniqueID; }
        set { m_UserUniqueID = value.Trim(); }
    }

    public string EncCertSN
    {
        get { return m_EncCertSN; }
        set { m_EncCertSN = value.Trim(); }
    }

    public string EncCert
    {
        get { return m_EncCert; }
        set { m_EncCert = value.Trim(); }
    }

    public string SignCertSN
    {
        get { return m_SignCertSN; }
        set { m_SignCertSN = value.Trim(); }
    }

    public string SignCert
    {
        get { return m_SignCert; }
        set { m_SignCert = value.Trim(); }
    }

    public string GetCertInfoString()
    {
        string strTemp = "";
        strTemp = m_ErrorCode;
        strTemp += m_Envsn;
        strTemp += m_UserUniqueID;
        strTemp += m_EncCert;
        strTemp += m_EncCertSN;
        strTemp += m_SignCertSN;
        strTemp += m_SignCert;
        return strTemp;
    }
}

[Serializable]
public class CUserInfo
{
    public CUserInfo()
    {
        m_TradeGuid = "";
        m_Rulesn = "";
        m_UserId = "";
        m_CommonName = "";
        m_PaperType = "";
        m_PaperID = "";
        m_ProvinceName = "";
        m_LocalityName = "";
        m_UnitName = "";
        m_DepartmentName = "";
        m_PostalAddress = "";
        m_PostalCode = "";
        m_AgentMan = "";
        m_TelephoneNumber = "";
        m_Fax = "";
        m_E_mail = "";
        m_MobileTelephone = "";
        m_InsuranceNumber = "";
        m_IcregistCode = "";
        m_LeagalPerson = "";
        m_TaxRegisterID = "";

        m_ExtendValue1 = "";
        m_ExtendValue2 = "";
        m_ExtendValue3 = "";
        m_ExtendValue4 = "";
        m_ExtendValue5 = "";
        m_ExtendValue6 = "";
        m_ExtendValue7 = "";
        m_ExtendValue8 = "";
        m_ExtendValue9 = "";
        m_ExtendValue10 = "";

        m_Envsn = "";
        m_TransName = "";
        m_TransTel = "";
        m_TransEmail = "";
        m_TransMobile = "";
        m_TransPertype = "";
        m_TransPaperID = "";
        m_TradeTime = "";
    }

    private string m_TradeGuid;
    private string m_Rulesn;
    private string m_UserId;
    private string m_CommonName;
    private string m_PaperType;
    private string m_PaperID;
    private string m_ProvinceName;
    private string m_LocalityName;
    private string m_UnitName;
    private string m_DepartmentName;
    private string m_PostalAddress;
    private string m_PostalCode;
    private string m_AgentMan;
    private string m_TelephoneNumber;
    private string m_Fax;
    private string m_E_mail;
    private string m_MobileTelephone;
    private string m_InsuranceNumber;
    private string m_IcregistCode;
    private string m_LeagalPerson;
    private string m_TaxRegisterID;

    private string m_ExtendValue1;
    private string m_ExtendValue2;
    private string m_ExtendValue3;
    private string m_ExtendValue4;
    private string m_ExtendValue5;
    private string m_ExtendValue6;
    private string m_ExtendValue7;
    private string m_ExtendValue8;
    private string m_ExtendValue9;
    private string m_ExtendValue10;

    private string m_Envsn;
    private string m_TransName;
    private string m_TransTel;
    private string m_TransEmail;
    private string m_TransMobile;
    private string m_TransPertype;
    private string m_TransPaperID;
    private string m_TradeTime;



    public string TradeGuid
    {
        get { return m_TradeGuid; }
        set { m_TradeGuid = value.Trim(); }
    }

    public string RuleSn
    {
        get { return m_Rulesn; }
        set { m_Rulesn = value.Trim(); }
    }

    public string UserId
    {
        get { return m_UserId; }
        set { m_UserId = value.Trim(); }
    }

    public string CommonName
    {
        get { return m_CommonName; }
        set { m_CommonName = value.Trim(); }
    }

    public string PaperType
    {
        get { return m_PaperType; }
        set { m_PaperType = value.Trim(); }
    }

    public string PaperID
    {
        get { return m_PaperID; }
        set { m_PaperID = value.Trim(); }
    }

    public string ProvinceName
    {
        get { return m_ProvinceName; }
        set { m_ProvinceName = value.Trim(); }
    }

    public string LocalityName
    {
        get { return m_LocalityName; }
        set { m_LocalityName = value.Trim(); }
    }

    public string UnitName
    {
        get { return m_UnitName; }
        set { m_UnitName = value.Trim(); }
    }

    public string DepartmentName
    {
        get { return m_DepartmentName; }
        set { m_DepartmentName = value.Trim(); }
    }

    public string PostalAddress
    {
        get { return m_PostalAddress; }
        set { m_PostalAddress = value.Trim(); }
    }

    public string PostalCode
    {
        get { return m_PostalCode; }
        set { m_PostalCode = value.Trim(); }
    }

    public string AgentMan
    {
        get { return m_AgentMan; }
        set { m_AgentMan = value.Trim(); }
    }

    public string TelephoneNumber
    {
        get { return m_TelephoneNumber; }
        set { m_TelephoneNumber = value.Trim(); }
    }

    public string Fax
    {
        get { return m_Fax; }
        set { m_Fax = value.Trim(); }
    }

    public string E_mail
    {
        get { return m_E_mail; }
        set { m_E_mail = value.Trim(); }
    }

    public string MobileTelephone
    {
        get { return m_MobileTelephone; }
        set { m_MobileTelephone = value.Trim(); }
    }

    public string InsuranceNumber
    {
        get { return m_InsuranceNumber; }
        set { m_InsuranceNumber = value.Trim(); }
    }

    public string IcregistCode
    {
        get { return m_IcregistCode; }
        set { m_IcregistCode = value.Trim(); }
    }

    public string LeagalPerson
    {
        get { return m_LeagalPerson; }
        set { m_LeagalPerson = value.Trim(); }
    }

    public string TaxRegisterID
    {
        get { return m_TaxRegisterID; }
        set { m_TaxRegisterID = value.Trim(); }
    }


    public string ExtendValue1
    {
        get { return m_ExtendValue1; }
        set { m_ExtendValue1 = value.Trim(); }
    }

    public string ExtendValue2
    {
        get { return m_ExtendValue2; }
        set { m_ExtendValue2 = value.Trim(); }
    }

    public string ExtendValue3
    {
        get { return m_ExtendValue3; }
        set { m_ExtendValue3 = value.Trim(); }
    }

    public string ExtendValue4
    {
        get { return m_ExtendValue4; }
        set { m_ExtendValue4 = value.Trim(); }
    }

    public string ExtendValue5
    {
        get { return m_ExtendValue5; }
        set { m_ExtendValue5 = value.Trim(); }
    }

    public string ExtendValue6
    {
        get { return m_ExtendValue6; }
        set { m_ExtendValue6 = value.Trim(); }
    }

    public string ExtendValue7
    {
        get { return m_ExtendValue7; }
        set { m_ExtendValue7 = value.Trim(); }
    }

    public string ExtendValue8
    {
        get { return m_ExtendValue8; }
        set { m_ExtendValue8 = value.Trim(); }
    }

    public string ExtendValue9
    {
        get { return m_ExtendValue9; }
        set { m_ExtendValue9 = value.Trim(); }
    }

    public string ExtendValue10
    {
        get { return m_ExtendValue10; }
        set { m_ExtendValue10 = value.Trim(); }
    }


    public string Envsn
    {
        get { return m_Envsn; }
        set { m_Envsn = value.Trim(); }
    }

    public string TransName
    {
        get { return m_TransName; }
        set { m_TransName = value.Trim(); }
    }

    public string TransTel
    {
        get { return m_TransTel; }
        set { m_TransTel = value.Trim(); }
    }

    public string TransEmail
    {
        get { return m_TransEmail; }
        set { m_TransEmail = value.Trim(); }
    }

    public string TransMobile
    {
        get { return m_TransMobile; }
        set { m_TransMobile = value.Trim(); }
    }

    public string TransPertype
    {
        get { return m_TransPertype; }
        set { m_TransPertype = value.Trim(); }
    }

    public string TransPaperID
    {
        get { return m_TransPaperID; }
        set { m_TransPaperID = value.Trim(); }
    }

    public string TradeTime
    {
        get { return m_TradeTime; }
        set { m_TradeTime = value.Trim(); }
    }

    public string GetUserInfoString()
    {
        string strTemp = "";
        strTemp = m_TradeGuid;
        strTemp += m_Rulesn;
        strTemp += m_UserId;
        strTemp += m_CommonName;
        strTemp += m_PaperType;
        strTemp += m_PaperID;
        strTemp += m_ProvinceName;
        strTemp += m_LocalityName;
        strTemp += m_UnitName;
        strTemp += m_DepartmentName;
        strTemp += m_PostalAddress;
        strTemp += m_PostalCode;
        strTemp += m_AgentMan;
        strTemp += m_TelephoneNumber;
        strTemp += m_Fax;
        strTemp += m_E_mail;
        strTemp += m_MobileTelephone;
        strTemp += m_InsuranceNumber;
        strTemp += m_IcregistCode;
        strTemp += m_LeagalPerson;
        strTemp += m_TaxRegisterID;

        strTemp += m_ExtendValue1;
        strTemp += m_ExtendValue2;
        strTemp += m_ExtendValue3;
        strTemp += m_ExtendValue4;
        strTemp += m_ExtendValue5;
        strTemp += m_ExtendValue6;
        strTemp += m_ExtendValue7;
        strTemp += m_ExtendValue8;
        strTemp += m_ExtendValue9;
        strTemp += m_ExtendValue10;

        strTemp += m_Envsn;
        strTemp += m_TransName;
        strTemp += m_TransTel;
        strTemp += m_TransEmail;
        strTemp += m_TransMobile;
        strTemp += m_TransPertype;
        strTemp += m_TransPaperID;
        strTemp += m_TradeTime;
        return strTemp;
    }
}

public class CredentialSoapHeader : SoapHeader
{
    public CredentialSoapHeader()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    private string m_userName;
    private string m_passWord;

    /// <summary>
    /// 用户名     
    /// </summary>
    public string UserName
    {
        get { return m_userName; }
        set { m_userName = value; }
    }


    /// <summary>
    /// 用户密码
    /// </summary>
    public string PassWord
    {
        get { return m_passWord; }
        set { m_passWord = value; }
    }
}

#endregion