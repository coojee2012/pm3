using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_YWHCYInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
    }
    private void showInfo()
    {
        string fid = Request.QueryString["fid"];
        if (fid != "")
        {
            string sql = "select * from WY_XM_YZWYHCY where FID='" + fid + "' ";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_XM.Text = dt.Rows[0]["XM"].ToString();
                t_XB.Text = dt.Rows[0]["XB"].ToString();
                t_NL.Text = dt.Rows[0]["NL"].ToString();
                t_SFZH.Text = dt.Rows[0]["SFZH"].ToString();
                t_ZZMM.Text = dt.Rows[0]["ZZMM"].ToString();
                t_YZWYHZW.Text = dt.Rows[0]["YZWYHZW"].ToString();
                t_LXDH.Text = dt.Rows[0]["LXDH"].ToString();
                t_GZDW.Text = dt.Rows[0]["GZDW"].ToString();
                t_JTDZ.Text = dt.Rows[0]["JTDZ"].ToString();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
}