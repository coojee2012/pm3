<%@ WebHandler Language="C#" Class="FileManage" %>

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using Approve.Common;
using Seaskyer.Strings;
using Seaskyer.WebApp.Utility;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.IO;

public class FileManage : IHttpHandler
{
    RCenter rc = new RCenter();    
    public void ProcessRequest (HttpContext context) {
        switch (context.Request.QueryString["F"])
        {
            case "FFileDel":
                FileDel(context);
                break;
            case "FGetFileCount":
                GetFileCount(context);
                break;
            default:
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public void FileDel(HttpContext context)
    {
        string strFID = context.Request["FID"];
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("select * from WY_FileList where FID='{0}' Delete WY_FileList Where FID = '{0}'", strFID));
        try 
        {
            System.Data.DataTable dt = rc.GetTable(sb.ToString());
            if (dt.Rows.Count == 1) 
            {
                string filePath = dt.Rows[0]["FFilePath"].ToString();
                File.Delete(context.Request.PhysicalApplicationPath + filePath);
            }
            
        }
        catch { }
        
        context.Response.Write("OK");

    }

    public void GetFileCount(HttpContext context)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string strCount = rc.GetSignValue("select Count(FID) from WY_FileList where "+ string.Format(" PID = '{0}' And FAppID = '{1}' And FileNum = '{2}'", context.Request["PID"], context.Request["FAppID"], context.Request["FileNum"]));

            //Context.Response.Write(Context.Request["PID"] + "^" + Context.Request["FAppID"] + "^" + Context.Request["FileNum"]);
            context.Response.Write(strCount);
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

}