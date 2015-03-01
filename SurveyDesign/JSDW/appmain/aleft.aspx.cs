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
        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);
        ProjectDB db = new ProjectDB();
        db.Log = Console.Out;
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
                    //SubMenus = from f in db.Menu
                    //           where f.FParentId == t.FNumber
                    //           orderby f.FOrder, f.FCreateTime descending
                    //           select new
                    //           {
                    //               f.FName,
                    //               FQUrl = string.Format(f.FQUrl, ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FAppId=" + Session["FAppId"]),
                    //               f.FNumber,
                    //               f.FPicName,
                    //               f.FSelcePicName,
                    //               FTarget = string.IsNullOrEmpty(f.FTarget) ? "main" : f.FTarget
                    //            //   SubMenus = (CF_Sys_Menu)null
                    //           }
                    //db.Menu.Where(f => f.FParentId == t.FNumber).Select(f=>                             
                    //            f.FName,
                    //        FQUrl = string.Format(t.FQUrl, ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FAppId=" + Session["FAppId"]),
                    //        f.FNumber,
                    //        f.FPicName,
                    //        f.FSelcePicName,
                    //        FTarget = string.IsNullOrEmpty(t.FTarget) ? "main" : t.FTarget
                    //        )
                };

        rep_Menu.DataSource = v;
        rep_Menu.DataBind();
    }




    protected void rep_Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProjectDB db = new ProjectDB();
        string ReportServer = rc.GetSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {           
            string fNumber = DataBinder.Eval(e.Item.DataItem, "FNumber").ToString();
            var SubMenus = from f in db.Menu
                           where f.FParentId == fNumber
                           orderby f.FOrder, f.FCreateTime descending
                           select new
                           {
                               f.FName,
                               FQUrl = string.Format(f.FQUrl, ReportServer, "&FBaseId=" + Session["FBaseId"] + "&FAppId=" + Session["FAppId"]),
                               f.FNumber,
                               f.FPicName,
                               f.FSelcePicName,
                               FTarget = string.IsNullOrEmpty(f.FTarget) ? "main" : f.FTarget
                           };
            //IQueryable<CF_Sys_Menu> SubMenus = DataBinder.Eval(e.Item.DataItem, "SubMenus") as IQueryable<CF_Sys_Menu>;
            if (SubMenus != null && SubMenus.Count() > 0)
            {
                Repeater rep_SubMenu = (Repeater)e.Item.FindControl("rep_SubMenu");
                rep_SubMenu.DataSource = SubMenus;
                rep_SubMenu.DataBind();
            }
        }
    }
}
