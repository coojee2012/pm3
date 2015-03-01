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

public partial class Admin_main_BackIdeasList : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlBind();
            this.btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ShowInfo();
        }
    }
    void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_Platform where fisdeleted=0 order by fnumber");//ftype=2：管理部门系统类型
        DataTable dt = rc.GetTable(sb.ToString());
        t_FPlatId.DataSource = dt;
        t_FPlatId.DataTextField = "FName";
        t_FPlatId.DataValueField = "FNumber";
        t_FPlatId.DataBind();
        t_FPlatId.Items.Insert(0, new ListItem("全部", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FPlatId.SelectedValue))
            sb.Append(" and FPlatId='" + t_FPlatId.SelectedValue + "' ");
        if (!string.IsNullOrEmpty(t_FSystemId.SelectedValue))
            sb.Append(" and FSystemId='" + t_FSystemId.SelectedValue + "' ");
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FContent,case FType when 1 then '个人' else '公开' end FType,");
        sb.Append(" convert(varchar(10),FTime,120) FTime from cf_App_BackIdea ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by FOrder");
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
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('BackIdeaAdd.aspx?fid=" + fid + "',460,290,$('#btnReload'));\">" + e.Item.Cells[2].Text + "</a>";

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
                sb.Append("delete from cf_App_BackIdea where fid='" + FId + "'");
                rc.PExcute(sb.ToString());
            }
        }
        tool.showMessage("共删除" + Count.ToString() + "条数据");
        ShowInfo();
    }
    protected void t_FPlatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        t_FSystemId.Items.Clear();
        if (!string.IsNullOrEmpty(t_FPlatId.SelectedValue))
        {
            DataTable dt = rc.GetTable("select fname,fnumber from cf_sys_SystemName where fplatId='" + t_FPlatId.SelectedValue + "' order by fnumber");
            t_FSystemId.DataSource = dt;
            t_FSystemId.DataTextField = "FName";
            t_FSystemId.DataValueField = "FNumber";
            t_FSystemId.DataBind();
            t_FSystemId.Items.Insert(0, new ListItem("全部", ""));
        }
    }
}
