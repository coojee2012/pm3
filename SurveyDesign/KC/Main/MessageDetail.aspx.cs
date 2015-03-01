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
using Approve.RuleCenter;

public partial class EntApprove_gzmain_MessageDetail : System.Web.UI.Page
{

    RNews rn = new RNews();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            ShowInfo();
        }
    }
  
    private void ShowInfo()
    {
        this.lTitle.Text = rn.ShowTitle(Request["fid"]);
        this.lContent.Text =  rn.ShowData(Request["fid"]);
        this.lDesc.Text = rn.getWZDJ(Request["fid"], "发布单位："+rn.GetNewsFPubDepart(Request["fid"]));
    }
}
