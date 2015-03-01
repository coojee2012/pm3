using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_SFQKInfo : WYPage
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
        string sql = "select * from WY_XM_XMQTXX where XMBH='" + (string)Session["XMBH"] + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_DC_MJ.Text = dt.Rows[0]["DC_MJ"].ToString();
            t_DC_SF.Text = dt.Rows[0]["DC_SF"].ToString();
            t_DCDT_MJ.Text = dt.Rows[0]["DCDT_MJ"].ToString();
            t_DCDT_SF.Text = dt.Rows[0]["DCDT_SF"].ToString();
            t_GC_MJ.Text = dt.Rows[0]["GC_MJ"].ToString();
            t_GC_SF.Text = dt.Rows[0]["DC_SF"].ToString();
            t_BS_MJ.Text = dt.Rows[0]["BS_MJ"].ToString();
            t_BS_SF.Text = dt.Rows[0]["BS_SF"].ToString();
            t_BG_MJ.Text = dt.Rows[0]["BG_MJ"].ToString();
            t_BG_SF.Text = dt.Rows[0]["BG_SF"].ToString();
            t_SY_MJ.Text = dt.Rows[0]["SY_MJ"].ToString();
            t_SY_SF.Text = dt.Rows[0]["SY_SF"].ToString();
            t_GY_MJ.Text = dt.Rows[0]["GY_MJ"].ToString();
            t_GY_SF.Text = dt.Rows[0]["GY_SF"].ToString();
            t_QT_MJ.Text = dt.Rows[0]["QT_MJ"].ToString();
            t_QT_SF.Text = dt.Rows[0]["QT_SF"].ToString();
            t_LTCW_MJ.Text = dt.Rows[0]["LTCW_MJ"].ToString();
            t_LTCW_SF.Text = dt.Rows[0]["LTCW_SF"].ToString();
            t_LTCW_GS.Text = dt.Rows[0]["LTCW_GS"].ToString();
            t_DXCW_MJ.Text = dt.Rows[0]["DXCW_MJ"].ToString();
            t_DXCW_SF.Text = dt.Rows[0]["DXCW_SF"].ToString();
            t_DXCW_GS.Text = dt.Rows[0]["DXCW_GS"].ToString();
            t_ZXC_MJ.Text = dt.Rows[0]["ZXC_MJ"].ToString();
            t_ZXC_SF.Text = dt.Rows[0]["ZXC_SF"].ToString();
            t_DPC_SF.Text = dt.Rows[0]["DPC_SF"].ToString();
            t_RYPZ_JL.Text = dt.Rows[0]["RYPZ_JL"].ToString();
            t_RYPZ_KF.Text = dt.Rows[0]["RYPZ_KF"].ToString();
            t_RYPZ_XZ.Text = dt.Rows[0]["RYPZ_XZ"].ToString();
            t_RYPZ_AQ.Text = dt.Rows[0]["RYPZ_AQ"].ToString();
            t_RYPZ_WX.Text = dt.Rows[0]["RYPZ_WX"].ToString();
            t_RYPZ_BJ.Text = dt.Rows[0]["RYPZ_BJ"].ToString();
            t_RYPZ_LH.Text = dt.Rows[0]["RYPZ_LH"].ToString();
            t_RYPZ_QT.Text = dt.Rows[0]["RYPZ_QT"].ToString();

        }
    }
}