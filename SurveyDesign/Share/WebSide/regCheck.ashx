<%@ WebHandler Language="C#" Class="regCheck" %>

using System;
using System.Web;
using System.Data;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Collections;

public class regCheck : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        YZ(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private void YZ(HttpContext context)
    {
        Share sh = new Share();
        string type = context.Request.QueryString["type"];
        if (type == "1")//企业名称重复验证
        {
            string FSystemId = context.Request.QueryString["FSystemId"];
            string FCompany = context.Request.QueryString["FCompany"];
            if (!string.IsNullOrEmpty(FCompany))
            {
                SortedList sl = new SortedList();
                sl.Add("FCompany", FCompany);
                sl.Add("FSystemId", FSystemId);

                DataTable dtReg = sh.GetTable("select fid from CF_User_Reg where FCompany=@FCompany and FSystemId=@FSystemId and Ftype=2 and isnull(FIsApp,0)=0 ", sh.ConvertParameters(sl));
                DataTable dtUser = sh.GetTable("select fid from CF_Sys_User where FCompany=@FCompany and FSystemId=@FSystemId ", sh.ConvertParameters(sl));
                if (dtReg != null && dtReg.Rows.Count > 0)
                    context.Response.Write("3");//正在注册，并未审核完
                else if (dtUser != null && dtUser.Rows.Count > 0)
                    context.Response.Write("2");//有重复
                else
                    context.Response.Write("1");
            }
            else
                context.Response.Write("4");//未填写
        }
        else if (type == "2")//用户名重复验证
        {
            string FName = context.Request.QueryString["FName"];
            DataTable dtReg = sh.GetTable("select fid from CF_User_Reg where FName=@FName and Ftype=2 and isnull(FIsApp,0)=0 ", new SqlParameter("@FName", FName));
            DataTable dtUser = sh.GetTable("select fid from CF_Sys_User where  FName=@FName", new SqlParameter("@FName", FName));
            if ((dtReg != null && dtReg.Rows.Count > 0) || (dtUser != null && dtUser.Rows.Count > 0))
                context.Response.Write("2");//有重复
            else
                context.Response.Write("1");
        }
        else if (type == "3")//组织机构代码重复验证
        {
            string FJuridcialCode = context.Request.QueryString["FJuridcialCode"];
            string FSystemId = context.Request.QueryString["FSystemId"];
            if (!string.IsNullOrEmpty(FJuridcialCode))
            {
                SortedList sl = new SortedList();
                sl.Add("FJuridcialCode", FJuridcialCode);
                sl.Add("FSystemId", FSystemId);

                DataTable dt = sh.GetTable("select fid from CF_Sys_User where FJuridcialCode=@FJuridcialCode and FSystemId=@FSystemId", sh.ConvertParameters(sl));
                if (dt != null && dt.Rows.Count > 0)
                    context.Response.Write("2");//有重复
                else
                {
                    dt = sh.GetTable("select fid from CF_User_Reg where FJuridcialCode=@FJuridcialCode and FSystemId=@FSystemId and Ftype=2 and isnull(FIsApp,0)=0 ", sh.ConvertParameters(sl));
                    if (dt != null && dt.Rows.Count > 0)
                        context.Response.Write("3");//正在注册，并未审核完
                    else
                        context.Response.Write("1");
                }
            }
            else
                context.Response.Write("4");//未填写
        }
        else if (type == "4")//营业执照号
        {
            string FLicence = context.Request.QueryString["FLicence"];
            string FSystemId = context.Request.QueryString["FSystemId"];
            if (!string.IsNullOrEmpty(FLicence))
            {
                SortedList sl = new SortedList();
                sl.Add("FLicence", FLicence);
                sl.Add("FSystemId", FSystemId);

                DataTable dt = sh.GetTable("select fid from CF_Sys_User where FLicence=@FLicence and FSystemId=@FSystemId", sh.ConvertParameters(sl));
                if (dt != null && dt.Rows.Count > 0)
                    context.Response.Write("2");//有重复
                else
                {
                    dt = sh.GetTable("select fid from CF_User_Reg where FLicence=@FLicence and FSystemId=@FSystemId and Ftype=2 and isnull(FIsApp,0)=0 ", sh.ConvertParameters(sl));
                    if (dt != null && dt.Rows.Count > 0)
                        context.Response.Write("3");//正在注册，并未审核完
                    else
                        context.Response.Write("1");
                }
            }
            else
                context.Response.Write("4");//未填写
        }
        else
        {
            context.Response.Write("0");//未知验证
        }
    }
}