using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;
using ProjectData;

public partial class Share_WebSide_CJWT : System.Web.UI.Page
{
    Share rc = new Share();

    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string FCol = "60401";//常见问题
        var v = from t in db.CF_News_Title
                join c in db.CF_News_Col on t.FID equals c.FNewsId
                where c.FColNumber == FCol
                orderby t.FOrder, t.FCreateTime descending
                select new { t.FName, t.FID };

        rep_BSZN.DataSource = v;
        rep_BSZN.DataBind();

    }


}
