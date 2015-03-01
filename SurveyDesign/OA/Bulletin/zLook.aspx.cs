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
public partial class OA_Bulletin_LookBull : System.Web.UI.Page
{
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ShowInfo();
            }
        }
    }

    protected void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        DataTable dt = oa.GetTable("select * from CF_OA_Bulletin where FID ='" + Request.QueryString["fid"] + "'");
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);

        }
    }
}
