using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_YWHInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showBAInfo();
            showCYInfo();
        }
    }
    private void showBAInfo()
    {
        string Fid = Request.QueryString["FID"];
        if (Session["XMQK_XMBH"] != null)
        {
            string sql = "select * from WY_XM_YZWYHBA where XMBH='" + (string)Session["XMQK_XMBH"] + "' ";
            DataTable dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                //t_YZDHMC.Text = dt.Rows[0]["YZDHMC"].ToString();
                //t_YZDHCLSJ.Text = dt.Rows[0]["YZDHCLSJ"] == null ? "" : DateTime.Parse(dt.Rows[0]["YZDHCLSJ"].ToString()).ToString("yyyy-MM-dd");
                t_YZWYHMC.Text = dt.Rows[0]["YZWYHMC"].ToString();
                t_YZWYHCLSJ.Text = dt.Rows[0]["YZWYHCLSJ"] == null ? "" : DateTime.Parse(dt.Rows[0]["YZWYHCLSJ"].ToString()).ToString("yyyy-MM-dd");
                t_YZWYHBGDZ.Text = dt.Rows[0]["YZWYHBGDZ"].ToString();
                //t_YCXYZDHYZS.Text = dt.Rows[0]["YCXYZDHYZS"].ToString();
                //t_YZDBDHRS.Text = dt.Rows[0]["YZDBDHRS"].ToString();
                //t_SCYZDBDHZKSJ.Text = dt.Rows[0]["SCYZDBDHZKSJ"] == null ? "" : DateTime.Parse(dt.Rows[0]["SCYZDBDHZKSJ"].ToString()).ToString("yyyy-MM-dd");
                //t_SCYZDBDHCXRS.Text = dt.Rows[0]["SCYZDBDHCXRS"].ToString();
                t_BZ.Text = dt.Rows[0]["BZ"].ToString();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
    private void showCYInfo()
    {
        if (Session["XMQK_XMBH"] != null)
        {
            string sql = "select * from WY_XM_YZWYHCY where XMBH='" + (string)Session["XMQK_XMBH"] + "' order by FCreateTime desc";

            this.Pager1.className = "dbShare";
            this.Pager1.sql = sql;
            this.Pager1.pagecount = 20;
            this.Pager1.controltopage = "ywhcy_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('数据加载失败！');</script>");
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showCYInfo();
    }
    protected void ywhcy_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    protected void ywhcy_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string Fid = e.Item.Cells[10].Text;
            if (e.CommandName == "See")
            {
                this.RegisterStartupScript(new Guid().ToString(), "<script>ShowAppPage('YWHCYInfo.aspx?fid=" + Fid + "','800','210');</script>");
            }
        }
    }
}