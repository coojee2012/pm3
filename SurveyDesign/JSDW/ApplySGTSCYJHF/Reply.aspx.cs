using System;
using System.Linq;
using System.Web.UI;
using ProjectData;
using Tools;

public partial class JSDW_ApplySGTSCYJHF_Reply : Page
{
    ProjectDB db = new ProjectDB(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (!IsPostBack)
        { 
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {

    }

    //显示
    private void showInfo()
    {
        string FID = Request.QueryString["FID"];


        pageTool tool = new pageTool(this.Page);
        var v = (from t in db.CF_Prj_Reply
                 where t.FID == FID
                 select t).FirstOrDefault();
        if (v != null)
        {
            tool.fillPageControl(v);

            if (v.FState != 2)
            {
                t_FTxt.Text = "<font color='#AAAAAA'>暂无</font>";
            }

        }
    }

}
