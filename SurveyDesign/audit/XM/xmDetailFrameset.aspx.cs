using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class audit_XM_xmDetailFrameset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["XMBH"] != null && !string.IsNullOrEmpty(Request["XMBH"]))
            { Session["XMBH"] = Request["XMBH"].ToString(); }
        }
    }
}