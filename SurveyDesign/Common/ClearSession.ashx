<%@ WebHandler Language="C#" Class="ClearSession" %>

using System;
using System.Web;
using ProjectBLL;
using System.Web.SessionState;

public class ClearSession : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (string.IsNullOrEmpty(context.Request.QueryString["Type"]))
        {
            context.Session.Remove("FBaseId");
            context.Session.Remove("FType");
            context.Session.Remove("FUserId");
            context.Session.Remove("FUserRightId");
            context.Session.Remove("FMenuRoleId");
            context.Session.Remove("FRoleId");
            context.Session.Remove("FSystemId");
            context.Session.Remove("FBaseName");
            context.Session.Remove("FBaseinfoId");
            context.Session.Remove("EntUserId");
        }
        context.Session.Remove("fly");
        context.Session.Remove("FManageTypeId");
        context.Session.Remove("FIsApprove");
        context.Session.Remove("FCanMod");
        context.Session.Remove("FAppId");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
   
}