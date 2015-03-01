using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class KC_ApplyHTBA_Print : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        string ReportServer = rc.GetSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        //Response.Redirect(string.Format("{0}JSGC-KCHT.cpt{1}", ReportServer, "&FBaseId=" + Request.QueryString["FBaseId"] + "&FAppId=" + Request.QueryString["FAppId"]));

        if (!string.IsNullOrEmpty(Request.QueryString["FBaseId"]) && !string.IsNullOrEmpty(Request.QueryString["FAppId"]))
        {
            Response.Redirect(string.Format("{0}JSGC-KCHT.cpt{1}", ReportServer, "&FBaseId=" + Request.QueryString["FBaseId"] + "&FAppId=" + Request.QueryString["FAppId"]));
        }
        else
        {
            Response.Redirect(string.Format("{0}JSGC-KCHT.cpt{1}", ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FAppId=" + Session["FAppId"]));
        }
    }
}
