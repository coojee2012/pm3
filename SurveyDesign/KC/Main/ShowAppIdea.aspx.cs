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
using Approve.EntityBase;
using Approve.RuleBase;
using Approve.RuleCenter;
using System.Text;
using Approve.Common;
public partial class Government_AppQualiInfo_ShowAppIdea : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["fid"]))
            {
                ShowInfo();
            }
        }
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select FTime,FResume  ");
        sb.Append(" from CF_Emp_Baseinfo where fid='" + Request["FId"] + "' ");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.t_FAppTime.Text = rc.StrToDate(dt.Rows[0]["FTime"].ToString());
            this.t_FIdea.Text = dt.Rows[0]["FResume"].ToString();
        }
    }
}
