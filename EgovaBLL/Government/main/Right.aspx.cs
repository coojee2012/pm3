using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections.Generic;
using Ext.Net.Utilities;
using Approve.Common;
public partial class Government_AppMain_Right : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            firstName.Value = Request.QueryString["name"];
        }

        if (!X.IsAjaxRequest)
        {
            this.ResourceManager1.DirectEventUrl = this.Request.Url.AbsoluteUri;

            // Reset the Session Theme on Page_Load.
            // The Theme switcher will persist the current theme only 
            // until the main Page is refreshed.
            this.Session["Ext.Net.Theme"] = Ext.Net.Theme.Default;

            //this.TriggerField1.Focus();
        }
    }
    [DirectMethod]
    public static int GetHashCode(string s)
    {
        return Math.Abs("/Examples".ConcatWith(s).ToLower().GetHashCode());
    }
}
