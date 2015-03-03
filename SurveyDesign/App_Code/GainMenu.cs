using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///GainMenu 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class GainMenu : System.Web.Services.WebService
{
    private RCenter rc = new RCenter();
    public GainMenu()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public DataTable GetUserMenu(string userId)
    {
        DataTable dt = new DataTable("CF_Sys_Menu");
        if (userId.Trim().Length == 0)
            return dt;
        string sql = @"with cte as(
                        select FName,Fnumber,'{0}'+FQUrl FUrl,FpicName,FSelcePicName,FTarget,FOrder,FCreateTime,FLevel,FParentId,FRoleId 
                        from CF_Sys_Menu
                        where FIsDis=1 and FParentId='45002'
                        union all
                        select b.FName,b.Fnumber,'{0}'+b.FQUrl,b.FpicName,b.FSelcePicName,b.FTarget,b.FOrder,b.FCreateTime,b.FLevel,b.FParentId,b.FRoleId from CF_Sys_Menu b
                        inner join cte on b.FParentId=cte.fnumber
                        where FIsDis=1
                        )
                        select * from cte where dbo.getRoleid(FRoleId,(STUFF((select ','+FMenuRoleId FROM CF_Sys_UserRight WHERE FUserId =@userId FOR XML PATH('')),1,1,''))) = 1  order by fLevel,FOrder";
        dt = rc.GetTable(sql, new SqlParameter() { ParameterName = "@userId", SqlDbType = SqlDbType.VarChar, Value = userId });
        return dt;
    }
}
