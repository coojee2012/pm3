using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using System.Drawing;
using System.Linq;
using ProjectData;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;


public partial class JNCLEnt_ApplyInfo_qyGoodBL : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
        }
    }
    private void ShowInfo()
    {
        string sql = string.Format(@"select * from (
                    select *,case QYMC when '广安市盛世园物业管理有限责任公司' then 0 else 1 end type
                    from Demo_qy_goodBL where 1=1 
                    ) a order by type ");
        if (!string.IsNullOrEmpty(t_MC.Text))
        { sql += " and FNAME like '" + t_MC.Text + "%' "; }
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string spid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "id"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            e.Item.Cells[4].Text = "<a href=\"javascript:showAddWindow('EditYLXWForm.aspx?id=" + e.Item.Cells[e.Item.Cells.Count - 1].Text + "',900,600);\" >" + e.Item.Cells[4].Text + "</a>";
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}