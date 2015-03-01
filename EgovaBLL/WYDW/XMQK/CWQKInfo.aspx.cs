using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_CWQKInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
    }
    private void showInfo()
    {
        string Fid = Request.QueryString["FID"];
        if (!string.IsNullOrEmpty(Fid))
        {
            string sql = "select * from WY_XM_CWQK where FID='" + Fid + "' ";
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
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
}