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

public partial class Share_SysSet_Platformlist : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ShowInfo();
        }
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.text_FName.Text + "%'");
        }
        if (this.text_FNumber.Text != "")
        {
            sb.Append(" and fnumber ='");
            sb.Append(this.text_FNumber.Text + "'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FNumber,FOrder from CF_Sys_Platform ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by forder,ftime desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "SysInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    private void SaveAllInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.SysInfo_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.SysInfo_List.Items[i].Cells[this.SysInfo_List.Columns.Count - 1].Text;
            TextBox Forder = (TextBox)SysInfo_List.Items[i].Cells[SysInfo_List.Columns.Count - 3].Controls[1];
            if (Forder.Text == "")
            {
                this.SysInfo_List.Items[i].BackColor = System.Drawing.Color.FromArgb(248, 250, 73);
                Forder.Text = "0";
            }
            sb.Append(" update CF_Sys_Platform set forder=" + Forder.Text + " where fid='" + fid + "' ");
        }
        if (sh.PExcute(sb.ToString()))
        {
            tool.showMessage("保存成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('PlatformAdd.aspx?fid=" + fid + "',460,300);\">" + e.Item.Cells[2].Text + "</a>";


        }
    }
    protected void Dic_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 3].Controls[1];
            if (e.CommandName == "Save")
            {
                pageTool tool = new pageTool(this.Page);
                if (sh.PExcute("update CF_Sys_Platform set forder=" + Forder.Text + " where fid='" + fid + "'"))
                {
                    tool.showMessage("保存成功");
                    this.Pager1.dataBind();
                }
                else
                {
                    tool.showMessage("保存失败");
                }
            }
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
        StringBuilder sb = new StringBuilder();
        int RowCount = SysInfo_List.Items.Count;
        for (int i = 0; i < RowCount; i++)
        {
            CheckBox cbx = (CheckBox)SysInfo_List.Items[i].Cells[0].Controls[1];
            if (cbx.Checked)
            {
                string FId = SysInfo_List.Items[i].Cells[SysInfo_List.Columns.Count - 1].Text.Trim();
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append("'" + FId + "'");
            }
        }
        if (sb.Length <= 0)
        {
            tool.showMessage("请选择");
            return;
        }
        else
        {
            string sql = "delete CF_Sys_Platform where fid in (" + sb + ")";
            if (sh.PExcute(sql))
            {
                tool.showMessage("删除成功！");
                ShowInfo();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
}
