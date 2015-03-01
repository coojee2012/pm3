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

public partial class UrbanAndTownPanning_gzmain_ShowAppInfo : System.Web.UI.Page
{
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
        ProjectDB db = new ProjectDB();
        StringBuilder sb = new StringBuilder();
        string fContent = db.CF_App_List.Where(t => t.FId == Request.QueryString["fid"]).Select(t => t.FResult).FirstOrDefault();
        this.lContent.Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + fContent;
    }
}
