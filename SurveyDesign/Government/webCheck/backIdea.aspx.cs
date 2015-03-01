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

public partial class Government_webCheck_backIdea : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.showData();
        }
    }
    private void showData()
    {
        string fidea = "&nbsp;&nbsp;&nbsp;&nbsp;" + rc.GetSignValue(EntityTypeEnum.EaProcessInstance, "FBackIdea", "fid='" + this.Request.QueryString["aid"].ToString() + "'");
        this.lBackIdea.Text = fidea;
    }
}
