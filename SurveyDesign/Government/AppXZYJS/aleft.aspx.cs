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

            //showInfo();
            showMenu();
        }
    }
    private void showMenu()
    {
        string sql = string.Format(@"select * from CF_Sys_Menu m where FParentId='4500216' and FisDis=1 and dbo.getRoleid(m.FRoleId,(select FMenuRoleId FROM CF_Sys_UserRight WHERE FUserId ='" + Session["DFUserId"] + "')) = 1 ");
        StringBuilder _builder = new StringBuilder();
        DataTable table = rc.GetTable(sql);
        if (table != null && table.Rows.Count > 0)
        {
            foreach (DataRow row in table.Rows)
            {
                _builder.AppendFormat("<a class=\"o_m02_1\" href='{0}' target=\"appMain\"><span>{1}</span></a>",row["FUrl"],row["FName"]);
            }
        }
        ltrText.Text = _builder.ToString();
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
                && t.FIsDeleted == false && t.FLevel == 4
                orderby t.FOrder, t.FCreateTime descending
                select new
                {
                    t.FName,
                    FQUrl = string.Format(t.FQUrl, ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FAppId=" + Session["FAppId"]),
                    t.FNumber,
                    t.FPicName,
                    t.FSelcePicName,
                    FTarget = string.IsNullOrEmpty(t.FTarget) ? "main" : t.FTarget
                };

        //rep_Menu.DataSource = v;
        //rep_Menu.DataBind();
    }




}
