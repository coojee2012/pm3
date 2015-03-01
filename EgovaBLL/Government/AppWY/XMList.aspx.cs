using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Ext.Net.Utilities;

public partial class Government_AppWY_XMList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showinfo();
        }
    }

    private void showinfo()
    {
        string strsql = "select w.XMMC,w.XMSD,w.JSDW,w.XMBH,w.XMLX,c.Fname from wy_xm_Jbxx w inner join cf_ent_baseinfo c on w.fbaseinfoid=c.fid where 1=1";
        if (txtXMMC.Text.Trim() != "")
        {
            strsql += " and w.XMMC='" + txtXMMC.Text.Trim() + "'";
        }
        if (txtQYMC.Text.Trim() != "")
        {
            strsql += " and c.Fname='" + txtQYMC.Text.Trim() + "'";
        }
        if (govd_FRegistDeptId.FNumber != null && govd_FRegistDeptId.FNumber != "51")
        {
            strsql += " and w.XMSD='" + govd_FRegistDeptId.FNumber + "'";
        }
        if (txtJSDW.Text.Trim() != "")
        {
            strsql += " and w.JSDW='" + txtJSDW.Text.Trim() + "'";
        }

        this.Pager1.sql = strsql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void dg_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = rc.GetSignValue("select FFullName from CF_Sys_ManageDept where FNumber='" + e.Item.Cells[7].Text + "'");
            e.Item.Cells[5].Text = rc.GetSignValue("select FName from CF_Sys_Dic where FParentid='" + e.Item.Cells[8].Text + "'");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showinfo();
    }
}