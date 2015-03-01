<%@ WebHandler Language="C#" Class="vCodeTool" %>

using System;
using System.Web;
using Approve.EntityBase;
using System.Web.SessionState;

public class vCodeTool : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (context.Request.QueryString["ee"] == "1")
        {
            string vNum = EConvert.ToString(context.Session["VNum"]);
            if (string.IsNullOrEmpty(vNum))
                vNum = "------";
            context.Response.Write(vNum.ToLower());
        }
        else
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
        if (context.Session["VNum"] == null || EConvert.ToString(context.Session["VNum"]).Equals(context.Request.QueryString["YZM"], StringComparison.InvariantCultureIgnoreCase))
            context.Response.Write("1");
        else
            context.Response.Write("0");

    }

 
}