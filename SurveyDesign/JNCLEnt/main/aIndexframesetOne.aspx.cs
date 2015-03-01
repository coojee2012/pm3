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
using ProjectBLL;

public partial class JSDW_main_aIndexframesetOne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
        }
    }
    void ShowInfo()
    {
        string fsystemId = CurrentEntUser.SystemId;
        RCenter rc = new RCenter();
        this.Title = rc.GetSignValue("select FName from cf_Sys_SystemName where fnumber='" + fsystemId + "'");
    }
}
