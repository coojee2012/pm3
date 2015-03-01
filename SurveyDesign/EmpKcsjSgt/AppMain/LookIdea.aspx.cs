using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
public partial class KcsjSgt_AppMain_LookIdea : Page
{
    ProjectDB db = new ProjectDB();
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
        pageTool tool = new pageTool(this.Page);
        string FID = Request.QueryString["FID"];
        string FAppId = Request.QueryString["FAppId"];
        var v = (from t in db.CF_App_List
                 join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                 join i in db.CF_App_Idea on t.FId equals i.FLinkId
                 where i.FId == FID || (t.FId == FAppId && i.FUserId == null)
                 orderby t.FTime descending
                 select new
                 {
                     t.FName,
                     t.FPrjId,
                     d.FPrjName,
                     i.FResult,
                     i.FContent,
                     i.FAppTime
                 }).FirstOrDefault();

        if (v != null)
        {
            tool.fillPageControl(v);
        }

    }
}
