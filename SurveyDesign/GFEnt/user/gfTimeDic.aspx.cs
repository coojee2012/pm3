using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Linq;
using System.Data.SqlClient;
using ProjectData;

public partial class GFEnt_user_gfTimeDic : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    public void bind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"select (select COUNT(1) from CF_App_List 
         where convert(nvarchar(10),FReportDate,121)>= convert(nvarchar(10),d.FStime,121)
         and convert(nvarchar(10),FReportDate,121) <= convert(nvarchar(10),d.FEtime,121)
         ) cou,* from YW_CF_DICtime d where 1=1 ");
        if (!string.IsNullOrEmpty(t_FYear.Text.Trim()))
        {
            sb.Append(" and FYear like '%" + t_FYear.Text.Trim() + "%'");
        }
        sb.Append(" Order By FTime Desc ");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("YW_CF_DICtime", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare");
        bind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[e.Item.Cells.Count - 3].Text = "<a href=\"javascript:showAddWindow('sbEnt.aspx?fid=" + FID + "',800,600);\" >已上报（" + e.Item.Cells[e.Item.Cells.Count - 3].Text + "）</a>";
            e.Item.Cells[e.Item.Cells.Count - 2].Text = "&nbsp;<a href=\"javascript:showAddWindow('timeEdit.aspx?fid=" + FID + "',800,385);\" >编辑</a>";
        }
    }
}