using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Protocols;
using RuleAppCode;
using System.Collections;
/// <summary>
///DataService 的摘要说明
/// </summary>
/// <summary>
/// webPrint 的摘要说明
/// </summary>
[WebService(Namespace = "http://www.ceeyi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DataService : System.Web.Services.WebService
{

    public SecurityContext m_SecurityContext;

    private bool ValidateUser()
    {
        if (m_SecurityContext == null)
        {
            throw new Exception("没有指定用户名和密码");
        }

        string userName = m_SecurityContext.UserID;
        string passWord = SecurityEncryption.DESDecrypt(m_SecurityContext.Password);
        if (userName == passWord && userName == "locknumberLogin")//如果是加密锁登陆
            return true;
        RCenter rc = new RCenter();
        string pass = rc.GetSignValue("select FPassWord from cf_sys_user where FName=@fname and FPassWord=@pwd and ftype=1 and fisdeleted=0 ", new SqlParameter[]{ 
            new SqlParameter("@fname",userName),
            new SqlParameter("@pwd",passWord)
        });
        if (pass == null || pass == "")
        {
            return false;
        }
        else
        {
            if (passWord != pass)
            {
                return false;
            }
        }
        return true;
    }

    //[WebMethod(MessageName = "GetDs(string SQL)")]
    //[SoapHeader("m_SecurityContext")]
    //public DataSet GetDs(string SQL)
    //{
    //    return GetDs(SQL, null);
    //}
    [WebMethod]
    [SoapHeader("m_SecurityContext")]
    public DataSet GetDs(string EncryptSQL, params byte[] Parameter)
    {
        if (!ValidateUser())
        {
            return null;
        }
        RCenter rc = new RCenter();
        if (!string.IsNullOrEmpty(EncryptSQL)
            && (EncryptSQL.ToLower().IndexOf("insert") > -1
             || EncryptSQL.ToLower().IndexOf("drop") > -1))
            return null;
        if (!string.IsNullOrEmpty(EncryptSQL))
            EncryptSQL = SecurityEncryption.DESDecrypt(EncryptSQL);
        SortedList sl = SerializeFunction.DeserializeObject<SortedList>(Parameter);

        DataTable dt = rc.GetTable(EncryptSQL, rc.ConvertParameters(sl));

        return dt.DataSet;
    }

    [WebMethod]
    [SoapHeader("m_SecurityContext")]
    public bool Excute(string SQL)
    {
        if (!ValidateUser())
        {
            return false;
        }
        RCenter rc = new RCenter();
        if (!string.IsNullOrEmpty(SQL)
          && (SQL.ToLower().IndexOf("insert") > -1
           || SQL.ToLower().IndexOf("drop") > -1)
            && SQL.ToLower().IndexOf("cf_app_printrecord") == -1)
            return false;
        if (!string.IsNullOrEmpty(SQL))
            SQL = SecurityEncryption.DESDecrypt(SQL);
        return rc.PExcute(SQL);
    }



    [WebMethod]
    [SoapHeader("m_SecurityContext")]
    public bool setCANumber(string CACardId, string CANumber, string FJuridcialCode)
    {
        if (!ValidateUser())
        {
            return false;
        }
        RCenter rc = new RCenter();
        SortedList sl = new SortedList();
        string sqltem = " select fid from CF_Sys_UserCA where FCACardId=@caid and FJuridcialCode=@code ";
        sl.Add("caid", CACardId);
        sl.Add("code", FJuridcialCode);
        string fid = rc.GetSignValue(sqltem, rc.ConvertParameters(sl));
        if (fid != null && fid != "")
        {
            return rc.PExcute(" update CF_Sys_UserCA set FCANumber=@fnum where fid='" + fid + "' ", new SqlParameter("fnum", CANumber));
        }
        else
            return false;
    }
}

public class SecurityContext : SoapHeader
{
    public string UserID;
    public string Password;
}
