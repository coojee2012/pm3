using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using ProjectData;
using System.Linq;

public partial class EvaluateEntApp_main_left1 : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CreateTree();

            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        string ReportServer = rc.GetSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);

        
        ProjectDB db = new ProjectDB();
        var v = from t in db.Menu
                where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 3// 4级  3级 系统菜单配置那
                orderby t.FNumber
                select new
                {
                    t.FName,
                    FQUrl = GetUrl(t.FQUrl),
                    t.FNumber,
                    t.FPicName,
                    t.FSelcePicName,
                    FTarget = string.IsNullOrEmpty(t.FTarget) ? "main" : t.FTarget
                };

        rep_Menu.DataSource = v;
        rep_Menu.DataBind();
    }
    private string GetUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;
        string YWBM = Request.QueryString["YWBM"];
        //if (url.Contains("?"))
        //{
        //    if (url.Contains("ReportPrint"))//报表
        //    {
        //        url += "&YWBM=" + fAppId;
        //    }
        //    return url;
        //}
        string param = url + "?YWBM=" + YWBM;
        return param;
    }
}
