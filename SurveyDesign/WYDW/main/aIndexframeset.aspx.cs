using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_main_aIndexframeset : System.Web.UI.Page
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