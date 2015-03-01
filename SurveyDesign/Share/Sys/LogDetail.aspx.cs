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
using System.Text;
using Approve.RuleCenter;

public partial class Admin_main_LogDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["FId"]))
        {
            string dbType = Request.QueryString["dbType"];
            StringBuilder sb = new StringBuilder();
            pageTool tool = new pageTool(this.Page);
            RQuali rq = new RQuali();
            RCenter rc = new RCenter();
            DataTable dt = null;
            sb.Append("select * from cf_Com_news t1");
            sb.Append(" where FId='" + Request["FId"] + "'");
            switch (dbType)
            {
                case "dbCenter":
                    dt = rc.GetTable(sb.ToString());
                    t_FDbName.Text = "dbCenter";
                    break;
                case "dbQuali":
                    dt = rq.GetTable(sb.ToString());
                    t_FDbName.Text = "dbQuali";
                    break;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.fillPageControl(dt.Rows[0]);
                t_FUserId.Text = rc.GetSignValue("select FName from cf_sys_user where fid='" + dt.Rows[0]["FUserId"] + "'");
            }


        }
        else
        {
            return;
        }
    }
}
