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
using ProjectBLL;

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
        ProjectDB db = new ProjectDB();
        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);

        if (FManageTypeId == "287")
        { //切换对就应的业务菜单
            FManageTypeId = "288";
        }
        else if (FManageTypeId == "300")
        { //切换对就应的业务菜单
            FManageTypeId = "301";
        }
        else if (FManageTypeId == "413" || FManageTypeId == "423")
        { //切换对就应的业务菜单
            FManageTypeId = "411";
        }


        //如果是 “程序性审查(勘察)(28801)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件
        //如果是 “审查人员安排(勘察)(28802)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件
        //如果是 “技术性审查(勘察)(28803)”业务要从“勘察文件审查合同备案(287)”中取FAppID的附件

        string s = "";
        if (FManageTypeId == "28801" || FManageTypeId == "28802" || FManageTypeId == "28803")
        {
            var app = (from a in db.CF_App_List
                       join na in db.CF_App_List on a.FLinkId equals na.FLinkId
                       where a.FManageTypeId == 287 && a.FToBaseinfoId == CurrentEntUser.EntId && a.FState == 6
                            && na.FId == EConvert.ToString(Session["FAppId"])
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
        //如果是 “程序性审查(设计)(30101)”业务要从“勘察文件审查合同备案(301)”中取FAppID的附件
        //如果是 “审查人员安排(设计)(30102)”业务要从“勘察文件审查合同备案(301)”中取FAppID的附件
        //如果是 “技术性审查(设计)(30103)”业务要从“勘察文件审查合同备案(301)”中取FAppID的附件
        else if (FManageTypeId == "30101" || FManageTypeId == "30102" || FManageTypeId == "30103")
        {
            var app = (from a in db.CF_App_List
                       join na in db.CF_App_List on a.FLinkId equals na.FLinkId
                       where a.FManageTypeId == 300 && a.FToBaseinfoId == CurrentEntUser.EntId && a.FState == 6
                            && na.FId == EConvert.ToString(Session["FAppId"])
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
                    FQUrl = string.Format(t.FQUrl, ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FApp=" + Session["FAppId"]) + s,
                    t.FNumber,
                    t.FPicName,
                    t.FSelcePicName,
                    FTarget = string.IsNullOrEmpty(t.FTarget) ? "main" : t.FTarget
                };

        rep_Menu.DataSource = v;
        rep_Menu.DataBind();
    }




}
