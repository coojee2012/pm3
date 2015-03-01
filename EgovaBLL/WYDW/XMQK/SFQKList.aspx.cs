using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_XMQK_SFQKList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showinfo();
    }

    private void showinfo()
    {
        string strsql = "select FID,XMMC,DC_MJ,GC_MJ,SY_MJ from WY_XM_XMQTXX where FBaseInfoID='" + CurrentEntUser.EntId + "' and XMBH='" + (string)Session["XMQK_XMBH"] + "'";
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
        }
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string Fid = e.Item.Cells[5].Text;
            if (e.CommandName == "See")
            {
                this.RegisterStartupScript(new Guid().ToString(), "<script>ShowAppPage('SFQKInfo.aspx?FID=" + Fid + "','800','410');</script>");
            }
        }
    }
}