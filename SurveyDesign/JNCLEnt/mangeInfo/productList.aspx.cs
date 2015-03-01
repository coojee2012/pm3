using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.Common;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_mangeInfo_productList : System.Web.UI.Page
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
        string sql = "select * from YW_JN_Product where fbaseid='" + CurrentEntUser.EntId + "' ";
        if (!string.IsNullOrEmpty(t_BZH.Text.Trim())) { sql += " and bzh like '%"+t_BZH.Text.Trim()+"%' "; }
        if (!string.IsNullOrEmpty(t_BZMC.Text.Trim())) { sql += " and bzh like '%" + t_BZMC.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_DJBH.Text.Trim())) { sql += " and bzh like '%" + t_DJBH.Text.Trim() + "%' "; }
        if (!string.IsNullOrEmpty(t_MC.Text.Trim())) { sql += " and bzh like '%" + t_MC.Text.Trim() + "%' "; }
        sql += " order by ftime desc  ";
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sql;
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("YW_JN_Product", "FID");
        tool.DelInfoFromGrid(this.DG_List, sl, "dbShare"); ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string SBMC = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "MC"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('editProduct.aspx?fid=" + FID + "',800,400);\" >" + SBMC + "</a>";
        }
    }
}