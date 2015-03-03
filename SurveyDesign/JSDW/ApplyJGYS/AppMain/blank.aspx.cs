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

public partial class EvaluateEntApp_main_blank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (EConvert.ToString(Session["FIsSafeApp"]) == "1")
            {
                td_BG.Attributes.Add("class", "blank_10");
            }



        }
    }
}
