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
        string FManageTypeId = EConvert.ToString(Session["FManageTypeId"]);


        if (FManageTypeId == "414" || FManageTypeId == "424")
        { //切换对就应的业务菜单
            FManageTypeId = "411";
        }

        ProjectDB db = new ProjectDB();
        var v = from t in db.Menu
                where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                && t.FIsDeleted == false && t.FLevel == 4
                orderby t.FOrder, t.FCreateTime descending
                select new
                {
                    t.FName,
                    t.FQUrl,
                    t.FNumber,
                    t.FPicName,
                    t.FSelcePicName,
                    t.FTarget
                };

        rep_Menu.DataSource = v;
        rep_Menu.DataBind();
    }




}
