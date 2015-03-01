using Approve.RuleCenter;
using EgovaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_AppMain_Default : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    void showInfo()
    {


        dg_List.DataSource = rc.GetTable("use XM_BaseInfo select * from XM_XMJBXX"); ;
        dg_List.DataBind();
    }
}