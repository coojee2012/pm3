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

public partial class Admin_main_HolidaysList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ShowInfo();
        }
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(text_FYear.Text.Trim()))
            sb.Append(" and year(FDate)='" + text_FYear.Text.Trim() + "'");
        if (this.text_FName.Text != "")
        {
            sb.Append(" and FDate = '");
            sb.Append(this.text_FName.Text + "'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId, convert(varchar(10),FDate,120) FDate from CF_Sys_Holidays ");
        sb.Append(" where fisdeleted=0 and fistrue=1 ");
        sb.Append(GetCon());
        sb.Append(" order by FDate");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "Holidays_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void Holidays_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('HolidaysAdd.aspx?fid=" + fid + "',460,290);\">" + e.Item.Cells[2].Text + "</a>";

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string FId = "";
        int Count = 0;
        int RowCount = Holidays_List.Items.Count;
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)Holidays_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = Holidays_List.Items[i].Cells[Holidays_List.Columns.Count - 1].Text.Trim();
                Count++;

            }
        }
        if (Count == 0)
        {
            tool.showMessage("请选择");
            return;
        }
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)Holidays_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                FId = Holidays_List.Items[i].Cells[Holidays_List.Columns.Count - 1].Text.Trim();
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from CF_Sys_Holidays where fid='" + FId + "'");
                sh.PExcute(sb.ToString());
            }
        }
        tool.showMessage("共删除" + Count.ToString() + "条数据");
        ShowInfo();
    }
}
