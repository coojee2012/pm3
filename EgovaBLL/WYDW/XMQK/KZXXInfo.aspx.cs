using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_KZXXInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    private void showInfo()
    {
        string Fid = Request.QueryString["FID"];
        if (!string.IsNullOrEmpty(Fid))
        {
            string strsql = "select * from WY_XM_KZXX where FID='" + Fid + "'";
            DataTable dt = new DataTable();
            dt = rc.GetTable(strsql);
            if (dt != null && dt.Rows.Count > 0)
            {
                Approve.Common.pageTool tool = new Approve.Common.pageTool(this.Page);
                tool.fillPageControl(dt.Rows[0]);
                ViewState["FID"] = dt.Rows[0]["FID"].ToString();
                hidMapX.Value = dt.Rows[0]["MapX"].ToString();
                hidMapY.Value = dt.Rows[0]["MapY"].ToString();
                //ViewState["FID"] = dt.Rows[0]["FID"].ToString();
                //t_ZDMJ.Text = dt.Rows[0]["ZDMJ"].ToString();
                //t_KFDW.Text = dt.Rows[0]["KFDW"].ToString();
                //t_JCND.Text = dt.Rows[0]["JCND"].ToString();
                //t_IsZZXQ.SelectedValue = dt.Rows[0]["IsZZXQ"].ToString();
                //t_JMHS.Text = dt.Rows[0]["JMHS"].ToString();
                //t_FJMHS.Text = dt.Rows[0]["FJMHS"].ToString();
                //t_JZRS.Text = dt.Rows[0]["JZRS"].ToString();
                //t_JGRQ.Text = dt.Rows[0]["JGRQ"].ToString();
                //t_SCJGRQ.Text = dt.Rows[0]["SCJGRQ"].ToString();
                //t_WYYFMJ.Text = dt.Rows[0]["WYYFMJ"].ToString();
                //t_DC_MJ.Text = dt.Rows[0]["DC_MJ"].ToString();
                //t_DCDT_MJ.Text = dt.Rows[0]["DCDT_MJ"].ToString();
                //t_GC_MJ.Text = dt.Rows[0]["GC_MJ"].ToString();
                //t_BS_MJ.Text = dt.Rows[0]["BS_MJ"].ToString();
                //t_BG_MJ.Text = dt.Rows[0]["BG_MJ"].ToString();
                //t_SY_MJ.Text = dt.Rows[0]["SY_MJ"].ToString();
                //t_GY_MJ.Text = dt.Rows[0]["GY_MJ"].ToString();
                //t_QT_MJ.Text = dt.Rows[0]["QT_MJ"].ToString();
                //t_LTCW_MJ.Text = dt.Rows[0]["LTCW_MJ"].ToString();
                //t_LTCW_GS.Text = dt.Rows[0]["LTCW_GS"].ToString();
                //t_DXCW_MJ.Text = dt.Rows[0]["DXCW_MJ"].ToString();
                //t_DXCW_GS.Text = dt.Rows[0]["DXCW_GS"].ToString();
                //t_ZXC_MJ.Text = dt.Rows[0]["ZXC_MJ"].ToString();
                //t_AFXT.SelectedValue = dt.Rows[0]["AFXT"].ToString();
                //t_CRKGS.Text = dt.Rows[0]["CRKGS"].ToString();
                //t_JKXFSGS.Text = dt.Rows[0]["JKXFSGS"].ToString();
                //t_DTGS.Text = dt.Rows[0]["DTGS"].ToString();
                //t_PDFS.Text = dt.Rows[0]["PDFS"].ToString();
                //t_FDJS.Text = dt.Rows[0]["FDJS"].ToString();
                //t_YGGDH.Text = dt.Rows[0]["YGGDH"].ToString();
                //t_DJBPGS.Text = dt.Rows[0]["DJBPGS"].ToString();
                //t_ECGSSXS.Text = dt.Rows[0]["ECGSSXS"].ToString();
                //t_HFCS.Text = dt.Rows[0]["HFCS"].ToString();
                //t_YGGSH.Text = dt.Rows[0]["YGGSH"].ToString();
                //t_MJJKXTTS.Text = dt.Rows[0]["MJJKXTTS"].ToString();
                //t_XFXTTS.Text = dt.Rows[0]["XFXTTS"].ToString();
                //t_TCGLXTTS.Text = dt.Rows[0]["TCGLXTTS"].ToString();
                //t_SJMJ.Text = dt.Rows[0]["SJMJ"].ToString();
                //hidMapX.Value = dt.Rows[0]["MapX"].ToString();
                //hidMapY.Value = dt.Rows[0]["MapY"].ToString();
            }

        }
        else
        {
            this.RegisterStartupScript(new Guid().ToString(), "<script>alert('数据加载失败！');parent.location.href='../main/Index.aspx';</script>");
        }
    }
}