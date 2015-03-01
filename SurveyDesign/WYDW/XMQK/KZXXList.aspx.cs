using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_XMQK_KZXXList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        showinfo();
    }

    private void showinfo()
    {
        string strsql = "select XMMC,ZDMJ,JMHS,FJMHS,JZRS,FID from WY_XM_KZXX where FBaseInfoID='" + CurrentEntUser.EntId + "' and XMBH = '" + (string)Session["XMBH"] + "'";
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
            string Fid = e.Item.Cells[6].Text;
            if (e.CommandName == "See")
            {
                this.RegisterStartupScript(new Guid().ToString(), "<script>ShowAppPage('KZXXInfo.aspx?FID=" + Fid + "','1200','650');</script>");
            }
        }
    }
}