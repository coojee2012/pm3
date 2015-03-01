using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class audit_XM_text : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Button1.Attributes.Add("onclick", "tz();");
    }
}