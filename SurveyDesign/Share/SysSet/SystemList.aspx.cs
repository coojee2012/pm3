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
using System.Data.SqlClient;
public partial class Admin_main_SystemList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
        DataTable dt = sh.GetTable(sb.ToString());
        t_FPlatId.DataSource = dt;
        t_FPlatId.DataTextField = "FName";
        t_FPlatId.DataValueField = "FNumber";
        t_FPlatId.DataBind();
        t_FPlatId.Items.Insert(0, new ListItem("", ""));
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '%");
            sb.Append(this.text_FName.Text + "%' ");
        }
        if (this.text_FNumber.Text != "")
        {
            sb.Append(" and fnumber like '%" + text_FNumber.Text + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FPlatId.SelectedValue))
            sb.Append(" and FPlatId='" + t_FPlatId.SelectedValue + "' ");
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FNumber,FOrder,FDesc,FPlatId from CF_Sys_SystemName ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" order by forder,ftime desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 15;
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
            sb.Append(" update CF_Sys_SystemName set forder=" + Forder.Text + " where fid='" + fid + "' ");
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
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FPlatId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPlatId"));
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('SystemAdd.aspx?fid=" + FID + "',480,550);\">" + FName + "</a>";

            e.Item.Cells[3].Text = sh.GetSignValue("select FName from CF_Sys_Platform where FNumber=@FNumber", new SqlParameter("@FNumber", FPlatId));
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
                if (sh.PExcute("update CF_Sys_SystemName set forder=" + Forder.Text + " where fid='" + fid + "'"))
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
        tool.DelInfoFromGrid(this.SysInfo_List, EntityTypeEnum.EsSystemName, "dbShare");
        ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
}
