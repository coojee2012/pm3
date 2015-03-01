using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class Common_help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => (t.FLinkNumber == Request.QueryString["FLinkNumber"])).FirstOrDefault();
        if (v != null)
        {
            t_FTitle.Text = v.FTitle;
            t_FContent.Text = v.FContent;
        }
    }
}
