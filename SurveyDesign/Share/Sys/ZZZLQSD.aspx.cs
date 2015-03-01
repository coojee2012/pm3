using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Tools;
using Approve.RuleCenter;

public partial class Share_Sys_ZZZLQSD : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
        if (!IsPostBack)
        {
            ShowInfo();
        }

    }
    private string  Con()
    {
        StringBuilder sb = new StringBuilder();
        if(!string.IsNullOrEmpty(t_FTxt.Text.Trim()))
        {
            sb.Append(" and FPrjName like '%"+t_FTxt.Text.Trim()+"%'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Prj_ZzzlQsd where Fisdeleted=0");
        sb.Append(Con());
        sb.Append(" order by FTime desc");
        this.Pager1.sql = sb.ToString();
        this.Pager1.className = "RCenter";
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text.ToString();
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('ZzzlQsdADD.aspx?FID=" + fid + "',896,856);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        int iCount = this.DG_List.Items.Count;
        for (int i = 0; i < iCount; i++)
        {
            CheckBox box = (CheckBox)this.DG_List.Items[i].Cells[0].Controls[1];
            string fId = this.DG_List.Items[i].Cells[this.DG_List.Columns.Count - 1].Text;
            if (box.Checked == true)
            {
                sb.Append(" update CF_Prj_ZzzlQsd set fisdeleted=1  ");
                sb.Append(" where fid='" +fId + "'");
            }
        }
        if (sb.Length > 0)
        {
            rc.PExcute(sb.ToString());
        }
        else
        {
            RegisterStartupScript("key", "<script>alert('请选择要删除的项')</script>");
            return;
        }
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
