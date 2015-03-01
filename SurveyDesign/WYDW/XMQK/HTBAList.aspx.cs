using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class WYDW_XMQK_HTBAInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string filecount = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        showInfo();
    }

    private void showInfo()
    {
        string strsql = "select * from WY_XM_HTBA where FBaseInfoID='" + CurrentEntUser.EntId + "' and XMBH='" + (string)Session["XMBH"] + "'";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = strsql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "dg_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void dg_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = DateTime.Parse(e.Item.Cells[3].Text).ToShortDateString();
            e.Item.Cells[4].Text = DateTime.Parse(e.Item.Cells[4].Text).ToShortDateString();
            e.Item.Cells[5].Text = DateTime.Parse(e.Item.Cells[5].Text).ToShortDateString();
            e.Item.Cells[6].Text = DateTime.Parse(e.Item.Cells[6].Text).ToShortDateString();
        }
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string Fid = e.Item.Cells[7].Text;
            if (e.CommandName == "See")
            {
                this.RegisterStartupScript(new Guid().ToString(), "<script>ShowAppPage('HTBAInfo.aspx?FID=" + Fid + "','800','410');</script>");
            }
        }
    }
}