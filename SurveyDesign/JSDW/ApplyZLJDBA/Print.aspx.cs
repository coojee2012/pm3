using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Tools;

public partial class JSDW_ApplyZLJDBA_Print : System.Web.UI.Page
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
            fAppId = EConvert.ToString( Request.QueryString["FAppId"] );
            Response.Redirect(string.Format("{0}{1}{2}", ReportServer,getFileName(fAppId), "&FAppId=" + fAppId));
        }
        else
        {
            fAppId = EConvert.ToString(Session["FAppId"]);
            Response.Redirect(string.Format("{0}{1}{2}", ReportServer,getFileName(fAppId), "&FAppId=" + fAppId));
        }

    }
    private string getFileName(string fAppId)
    {
        EgovaDB db = new EgovaDB();
        TC_QA_Record record = db.TC_QA_Record.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        if (!string.IsNullOrEmpty(record.PrjItemType))
        {
            //建筑工程
            if (record.PrjItemType.Equals("2000101"))
            {
                return "ZLJDBA_JZGC.cpt";
            }
            //市政基础
            if (record.PrjItemType.Equals("2000102"))
            {
                return "ZLJDBA_SZJC.cpt";
            }
        }        
        return "ZLJDBA.cpt";
    }
}