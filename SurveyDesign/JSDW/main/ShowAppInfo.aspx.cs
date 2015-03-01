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
using Approve.EntityBase;
using Approve.RuleCenter;
using ProjectData;
using System.Linq;
public partial class EntApprove_gzmain_ShowAppInfo : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["fid"] != null && Request["fid"] != "")
            {
                ShowInfo();
            }
        }
    }
    private void ShowInfo()
    {
        var v = db.CF_App_Idea.Where(t => t.FLinkId == Request["fid"]).OrderBy(t => t.FReportCount).OrderBy(t => t.FTime).Select(t => new
        {
            FTime = t.FTime,
            t.FContent
        }).ToList();
        rptIdea.DataSource = v;
        rptIdea.DataBind();
    }
}
