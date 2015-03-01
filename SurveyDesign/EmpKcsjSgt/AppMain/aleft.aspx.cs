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
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectBLL;
using ProjectData;
using System.Text;
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
        ProjectDB db = new ProjectDB();
        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);

        if (FManageTypeId == "287")
        { //切换对就应的业务菜单
            FManageTypeId = "288";
        }
        //如果是 “程序性审查(勘察)(28801)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件
        //如果是 “审查人员安排(勘察)(28802)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件
        //如果是 “技术性审查(勘察)(28803)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件
        string s = "";
        if (FManageTypeId == "28801" || FManageTypeId == "28802" || FManageTypeId == "28803")
        {
            var app = (from a in db.CF_App_List
                       where a.FManageTypeId == 287 && a.FToBaseinfoId == CurrentEmpUser.EntBaseinfoId && a.FState == 6
                       select new
                       {
                           a.FId,
                           a.FManageTypeId
                       }).FirstOrDefault();
            if (app != null)
            {
                s = "?FAppId=" + app.FId;
            }
        }


        var v = from t in db.Menu
                where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 4
                orderby t.FOrder, t.FCreateTime descending
                select new
                {
                    t.FName,
                    FQUrl = t.FQUrl + s,
                    t.FNumber,
                    t.FPicName,
                    t.FSelcePicName,
                    t.FTarget
                };

        rep_Menu.DataSource = v;
        rep_Menu.DataBind();
    }

 


}
