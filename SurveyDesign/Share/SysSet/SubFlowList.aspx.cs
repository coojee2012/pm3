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


public partial class Admin_main_SubFlowList : Page
{

    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();
            ShowInfo();
            this.btnAdd.Attributes.Add("onclick", "showApproveWindow('SubFlowAdd.aspx?fprocessid=" + Request["fprocessid"] + "',800,600)");
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from cf_sys_role where fisdeleted=0 order by forder,ftime desc");
        this.drop_fRoleId.DataSource = rc.GetTable(sb.ToString());
        this.drop_fRoleId.DataTextField = "FName";
        this.drop_fRoleId.DataValueField = "Fnumber";
        this.drop_fRoleId.DataBind();
        this.drop_fRoleId.Items.Insert(0, new ListItem("请选择", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.text_FName.Text + "%'");
        }
        if (this.text_FLevel.Text != "")
        {
            sb.Append(" and FLevel =");
            sb.Append(this.text_FLevel.Text + "");
        }
        if (this.drop_fRoleId.SelectedValue != "")
        {
            sb.Append(" and fRoleId='");
            sb.Append(drop_fRoleId.SelectedValue + "' ");
        }
        if (Request["fprocessid"] != null && Request["fprocessid"] != "")
        {
            sb.Append(" and fprocessid='");
            sb.Append(Request["fprocessid"] + "' ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FOrder,FLevel,FDefineDay, FProcessId,FTypeId,");
        sb.Append("case FIsSend when 1 then '是' when 2 then '否' end as FIsSend,");
        sb.Append("case FIsEnd when 1 then '是' when 2 then '否' end as FIsEnd,");
        sb.Append("(select top 1 fname from CF_App_Process where fid=FProcessId) FProcessName,");
        sb.Append("(select top 1 fname from cf_sys_role where fnumber=FRoleId) FRoleName ");
        sb.Append("From CF_App_SubFlow ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By forder,FCreateTime Desc");
        repSubFlow.DataSource = rc.GetTable(sb.ToString());
        repSubFlow.DataBind();
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "SubFlow_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    private void SaveAllInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.SubFlow_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.SubFlow_List.Items[i].Cells[this.SubFlow_List.Columns.Count - 1].Text;
            TextBox Forder = (TextBox)SubFlow_List.Items[i].Cells[SubFlow_List.Columns.Count - 4].Controls[1];
            if (Forder.Text == "")
            {
                this.SubFlow_List.Items[i].BackColor = System.Drawing.Color.FromArgb(248, 250, 73);
                Forder.Text = "0";
            }
            sb.Append(" update CF_App_SubFlow set forder=" + Forder.Text + " where fid='" + fid + "' ");
        }
        if (rc.PExcute(sb.ToString()))
        {
            tool.showMessage("保存成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("保存失败");
        }
    }

    protected void SubFlow_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fProcessId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('SubFlowAdd.aspx?fid=" + fid + "&fprocessid=" + fProcessId + "',800,600);\">" + e.Item.Cells[2].Text + "</a>";

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
        tool.DelInfoFromGrid(this.SubFlow_List, EntityTypeEnum.EaSubFlow, "dbShare");
        ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
    protected void SubFlow_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            StringBuilder sb = new StringBuilder();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (e.CommandName == "Save")
            {
                TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];
                sb.Append(" update CF_App_SubFlow set forder=" + Forder.Text + " where fid='" + fid + "' ");
                rc.PExcute(sb.ToString());
            }
        }
    }
}
