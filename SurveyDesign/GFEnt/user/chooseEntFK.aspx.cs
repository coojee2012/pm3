using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GFEnt_user_chooseEntFK : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEnt();
        }
    }
    public void bindEnt()
    {
        string sql = string.Format(@"select * from LINKER_95.dbCenterSC.dbo.cf_sys_user where FBaseInfoId='WFK_GF'");
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
        { sql += " and FName like '%" + t_FName.Text.Trim() + "%'"; }
        if (!string.IsNullOrEmpty(t_passWord.Text.Trim()))
        { sql += " and FPassWord like '%" + t_passWord.Text.Trim() + "%'"; }
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindEnt();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue='" + fId + "';window.close();</script>");
        }
    }
}