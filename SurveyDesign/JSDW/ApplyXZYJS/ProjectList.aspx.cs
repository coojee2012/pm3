using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_ProjectList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
        }
    }   
    private void ShowInfo()
    {
        StringBuilder _builder = new StringBuilder();
        _builder.Append("select XMBM,XMBH,XMMC,B.FName,XMDZ,JSDW,LXSJ from XM_XMInfo A LEFT JOIN CF_Sys_ManageDept B ON A.XMSD = B.FNumber order by LXSJ desc");
        if (!string.IsNullOrEmpty(txtProjectName.Text.Trim()))
            _builder.AppendFormat(" where XMMC like '%{0}%'",txtProjectName.Text);

        this.Pager1.className = "dbCenter";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("XM_XMInfo", "XMBM");
        tool.DelInfoFromGrid(this.DG_List, sl, "RCenter");
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"EditshowWindow('" + e.Item.Cells[e.Item.Cells.Count - 1].Text + "')\">" + e.Item.Cells[3].Text + "</a>";
        }
    }
}   