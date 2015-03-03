<%@ WebHandler Language="C#" Class="DelFile" %>

using System;
using System.Web;
using Approve.RuleCenter;
public class DelFile : IHttpHandler {
    private RCenter rc = new RCenter();
    public void ProcessRequest (HttpContext context) {
        string Id = context.Request["Id"];
        string result = "0";
        if (!string.IsNullOrEmpty(Id))
        {
            string sql = @"delete from YW_FILE_DETAIL where Id='" + Id + "'";
            bool success = rc.PExcute(sql);
            if (success)
                result = "1";
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(result);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}