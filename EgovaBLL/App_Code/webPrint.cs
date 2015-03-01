using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Data.SqlClient;

/// <summary>
/// webPrint 的摘要说明
/// </summary>
[WebService(Namespace = "http://www.ceeyi.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class webPrint : System.Web.Services.WebService
{
    public SecurityContext m_SecurityContext;

    private bool ValidateUser()
    {
        if (m_SecurityContext == null)
        {
            throw new Exception("没有指定用户名和密码");
        }

        string userName = m_SecurityContext.UserID;
        string passWord = m_SecurityContext.Password;
        RCenter rc = new RCenter();
        string sql = string.Empty;
        string pass = string.Empty;
        if (userName != "" && passWord != "")
        {
            sql = "select FPassWord from cf_sys_user where FName=@FName and FPassWord=@FPassWord and ftype=1 and fisdeleted=0 ";
            pass = EConvert.ToString(rc.GetSignValue(sql, new SqlParameter[]{
            new SqlParameter("FName",userName),
                new SqlParameter("FPassWord",passWord)
            }));
        }
        else
        {
            sql = "select FPassWord from cf_sys_user where flocknumber=@flocknumber  and ftype=1 and fisdeleted=0 ";
            pass = EConvert.ToString(rc.GetSignValue(sql, new SqlParameter[]{
            new SqlParameter("flocknumber", SecurityEncryption.DESDecrypt(passWord)) 
            }));
        }
        if (pass == null || pass == "")
        {
            return false;
        }
        else
        {
            if (userName != "" && passWord != "")
            {
                if (passWord != pass)
                {
                    return false;
                }
            }
        }
        return true;
    }

    [WebMethod]
    [SoapHeader("m_SecurityContext")]
    public DataSet GetDs(string SQL)
    {
        if (!ValidateUser())
        {
            return null;
        }

        DataSet ds = new DataSet();
        RCenter rc = new RCenter();
        DataTable dt = rc.GetTable(SQL);
        ds.Tables.Add(dt.Copy());
        return ds;
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
        return rc.PExcute(SQL);
    }
}



public class SecurityContext : SoapHeader
{
    public string UserID;
    public string Password;
}


