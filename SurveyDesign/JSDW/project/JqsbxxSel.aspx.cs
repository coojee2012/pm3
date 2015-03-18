using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class JSDW_project_JqsbxxSel : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Show();
        }
    }
    private void Show()
    {
        StringBuilder _builder = new StringBuilder();

        _builder.Append("select a.* from GC_JQSBXX a,[XM_XMJBXX] b where a.xmbh=b.xmbh ");
        if (t_FName.Text.Trim() != "")
        {
            _builder.AppendFormat(" and a.SBMC LIKE '%{0}%'", t_FName.Text.Trim());
        }
        if (t_QYMC.Text.Trim() != "")
        {
            _builder.AppendFormat(" and b.jsdw LIKE '%{0}%'", t_QYMC.Text.Trim());
        }
        this.Pager1.className = "XM_BaseInfo";
        this.Pager1.sql = _builder.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Show();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }
}