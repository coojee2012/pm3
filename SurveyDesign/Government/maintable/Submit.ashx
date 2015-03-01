<%@ WebHandler Language="C#" Class="Submit" %>

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;

public class Submit : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        showInfo(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



    private void showInfo(HttpContext ct)
    {
        if (!string.IsNullOrEmpty(ct.Request.QueryString["mytable"]))
        {
            saveMyTable(ct, ct.Request.QueryString["mytable"]);
        }
    }

    /// <summary>
    /// 保存我的桌面板块位置
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="mytable"></param>
    private void saveMyTable(HttpContext ct, string mytable)
    {
        RCenter rc = new RCenter();
        string sysId = ct.Request.QueryString["sysId"];
        string FLinkId = EConvert.ToString(ct.Session["DFUserRightId"]);
        SortedList ss = new SortedList();
        ss.Add("FLinkId", FLinkId);
        ss.Add("sysId", sysId);
        string FID = rc.GetSignValue("select FID from cf_user where FLinkId=@FLinkId and FType=@sysId ", rc.ConvertParameters(ss));
        if (!string.IsNullOrEmpty(FID))
        {
            SortedList sl = new SortedList();
            SaveOptionEnum so = SaveOptionEnum.Update;
            sl.Add("FID", FID);
            sl.Add("FMyTable", mytable);//保存位置
            rc.SaveEBase("cf_user", sl, "FID", so);
        }
    }
}