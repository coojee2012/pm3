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
    private RCenter rc = new RCenter();
    private RQuali rq = new RQuali();
    private string FManageTypeId = "8080";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }


    //显示
    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        var data = from t in db.Menu
                where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 3
                orderby t.FOrder, t.FCreateTime ascending
                select new
                {
                    t.FName,
                    FQUrl = GetUrl(t.FQUrl)
                };
        rptMenu.DataSource = data;
        rptMenu.DataBind();
    }
    private string GetUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;
        string YD_fAppId = Request.QueryString["fAppId"];
        string YD_Id = Request.QueryString["XM_Id"];
        if (url.Contains("?"))
        {
            if (url.Contains("ReportPrint"))//报表
            {
                url += "&YWBM=" + YD_fAppId;
            }
            return url;
        }
        string param = url + "?XM_Id=" + YD_Id + "&fAppId=" + YD_fAppId;
        return param;
    }
}
