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
using System.Linq;
using System.Data.SqlClient;
using ProjectData;

public partial class Government_statis_EntBadAction : System.Web.UI.Page
{
    RCenter rc = new RCenter("dbJST");
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["fid"] != "")
                showInfo(Request.QueryString["fid"]);
        }
    }
    private void showInfo(string fid)
    {
        pageTool pt = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * from QY_BLXW_XXB where id=@fid ");
        DataTable dt = rc.GetTable(sb.ToString(),new SqlParameter("fid",fid));
        pt.fillPageControl(dt.Rows[0]);

    }
}
