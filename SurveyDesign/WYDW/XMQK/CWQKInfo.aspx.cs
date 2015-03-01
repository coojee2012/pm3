using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_CWQKInfo : WYPage
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSession();
        showInfo();
    }
    private void showInfo()
    {
        string Fid = Request.QueryString["FID"];
        string sql = "select * from WY_XM_CWQK where XMBH='" + (string)Session["XMBH"] + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_ND.Text = DateTime.Now.AddYears(-1).ToString("yyyy");
            t_WYFZE.Text = dt.Rows[0]["WYFZE"].ToString();
            t_WYFSFL.Text = dt.Rows[0]["WYFSFL"].ToString();
            t_TCF.Text = dt.Rows[0]["TCF"].ToString();
            t_GGF.Text = dt.Rows[0]["GGF"].ToString();
            t_QT.Text = dt.Rows[0]["QT"].ToString();
            t_YYCB.Text = dt.Rows[0]["YYCB"].ToString();
            t_YYLR.Text = dt.Rows[0]["YYLR"].ToString();
        }

    }
}