using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.Common;
using Approve.EntityBase;

public partial class Share_Sys_BatchList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FName.Text))
        {
            sb.Append(" and fName like '%" + t_FName.Text + "%'");
        }
        if (!string.IsNullOrEmpty(t_FNumber.Text))
        {
            sb.Append(" and fnumber like '%" + t_FNumber.Text + "%'");
        }
        return sb.ToString();
    }

    //显示
    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * ");
        sb.Append(" From CF_Sys_BatchNo ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" Order By FTypeId ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }


    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    //删除按钮
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.DG_List, EntityTypeEnum.SHsBatchNo, "dbShare");
        showInfo();
    }
}
