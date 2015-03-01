using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class Common_help_HelpLink : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
    }

    private void showInfo()
    {
        ProjectDB db = new ProjectDB();
        string FCol = Request.QueryString["FCol"];
        string str = "";
        CF_Sys_HelpMsg v = db.CF_Sys_HelpMsg.Where(t => t.FLinkNumber == FCol).FirstOrDefault();
        if (v != null)
        {
            str = "<a class=\"a_help\" href=\"javascript:openHelp('" + v.FLinkNumber + "');\">帮助信息</a>";
        }
        lit_HelpLink.Text = str;
    }
}
