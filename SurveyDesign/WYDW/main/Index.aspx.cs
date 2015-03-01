using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_main_Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //清空多余session
        Session.Remove("FManageTypeId");
        Session.Remove("FIsApprove");
        Session.Remove("FCanMod");
        Session.Remove("FAppId");
    }
}