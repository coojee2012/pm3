using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;
using Approve.RuleCenter;

public partial class JSDW_ApplySGXKZGL_Print : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        string ReportServer = rc.GetSysObjectContent("_ReportServer");
        string fAppId = "";
        if (string.IsNullOrEmpty(ReportServer))
        {
            //本地部署的服务
            ReportServer = "http://" + Request.Url.Host + ":8080/WebReport/ReportServer?reportlet=";
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FAppId"]))
        {
            fAppId = EConvert.ToString(Request.QueryString["FAppId"]);
            Response.Redirect(string.Format("{0}{1}{2}", ReportServer, "BHGD.cpt", "&FAppId=" + fAppId));
        }
        else
        {
            fAppId = EConvert.ToString(Session["FAppId"]);
            Response.Redirect(string.Format("{0}{1}{2}", ReportServer, "BHGD.cpt", "&FAppId=" + fAppId));
        }

    }
}