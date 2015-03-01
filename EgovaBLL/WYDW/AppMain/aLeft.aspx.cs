using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_Project_Main_aLeft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch ((string)Session["FManageTypeId"])
        {
            case "14401": pnlXMSQ.Visible = false; pnlCWQK.Visible = false; break;
            case "14402": pnlJBXX.Visible = false; pnlKZXX.Visible = false; pnlHTBZ.Visible = false; pnlYWH.Visible = false; pnlRYQK.Visible = false; pnlSFQK.Visible = false; pnlCWQK.Visible = false; pnlLPB.Visible = false; break;
            case "14403": pnlJBXX.Visible = false; pnlKZXX.Visible = false; pnlHTBZ.Visible = false; pnlCWQK.Visible = false; pnlXMSQ.Visible = false; break;
            case "14404": pnlJBXX.Visible = false; pnlKZXX.Visible = false; pnlYWH.Visible = false; pnlRYQK.Visible = false; pnlSFQK.Visible = false; pnlCWQK.Visible = false; pnlLPB.Visible = false; pnlXMSQ.Visible = false; break;
            case "14405": pnlJBXX.Visible = false; pnlKZXX.Visible = false; pnlHTBZ.Visible = false; pnlRYQK.Visible = false; pnlSFQK.Visible = false; pnlCWQK.Visible = false; pnlLPB.Visible = false; pnlXMSQ.Visible = false; break;
            case "14406": pnlJBXX.Visible = false; pnlKZXX.Visible = false; pnlHTBZ.Visible = false; pnlYWH.Visible = false; pnlRYQK.Visible = false; pnlSFQK.Visible = false; pnlLPB.Visible = false; pnlXMSQ.Visible = false; break;
        }
    }
}