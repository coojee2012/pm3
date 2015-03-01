using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Approve.RuleCenter;
using Approve.EntityBase;
using ProjectData;

public partial class Enterprise_main_index : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        //清空多余session
        Session.Remove("FManageTypeId");
        Session.Remove("FIsApprove");
        Session.Remove("FCanMod");
        Session.Remove("FAppId");
    }
    

}
