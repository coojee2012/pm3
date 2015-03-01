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

public partial class Admin_main_BadActionTypeList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
        }
    }


    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFname.Text != "")
        {
            sb.Append(" and Fname like '");
            sb.Append(this.txtFname.Text + "%'");
        }
        if (this.txtFnumber.Text != "")
        {
            sb.Append(" and Fnumber like '");
            sb.Append(this.txtFnumber.Text + "%'");
        }
        sb.Append(" and fparentid is null ");
        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,fnumber,FOrder,FName ");
        sb.Append("From CF_Sys_BadActionCode ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By Forder,FCreateTime Desc");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "BadActioin_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }
    private void SaveAllInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.BadActioin_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.BadActioin_List.Items[i].Cells[this.BadActioin_List.Columns.Count - 1].Text;
            TextBox Forder = (TextBox)BadActioin_List.Items[i].Cells[BadActioin_List.Columns.Count - 4].Controls[1];
            if (Forder.Text == "")
            {
                this.BadActioin_List.Items[i].BackColor = System.Drawing.Color.FromArgb(248, 250, 73);
                Forder.Text = "0";
            }
            sb.Append(" update CF_Sys_BadActionCode set forder=" + Forder.Text + " where fid='" + fid + "' ");
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
    protected void BadActioin_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('BadActionTypeAdd.aspx?fid=" + fid + "',500,194);\">" + e.Item.Cells[2].Text + "</a>";


        }
    }
    protected void BadActioin_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];
            if (e.CommandName == "Save")
            {
                pageTool tool = new pageTool(this.Page);
                if (sh.PExcute("update CF_Sys_BadActionCode set forder=" + Forder.Text + " where fid='" + fid + "'"))
                {
                    tool.showMessage("保存成功");
                    this.Pager1.dataBind();
                }
                else
                {
                    tool.showMessage("保存失败");
                }
            }
            if (e.CommandName == "Add")
            {
                this.Response.Redirect("BadActioinKindList.aspx?fparentid=" + fid);
            }

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //tool.DelInfoFromGrid(this.BadActioin_List, EntityTypeEnum.EsBadActionCode, "RCenter");
        //ShowInfo();
        int iCount = this.BadActioin_List.Items.Count;
        ArrayList array1 = new ArrayList();
        ArrayList array2 = new ArrayList();
        for (int i = 0; i < iCount; i++)
        {
            string fid = this.BadActioin_List.Items[i].Cells[this.BadActioin_List.Columns.Count - 1].Text;
            string fnumber = this.BadActioin_List.Items[i].Cells[3].Text;
            CheckBox box = (CheckBox)this.BadActioin_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                array1.Add(fid);
                array2.Add(fnumber);
            }
        }
        if (array1.Count == 0)
        {
            tool.showMessage("请选择");
            return;
        }
        StringBuilder sb = new StringBuilder();
        for (int j = 0; j < array1.Count; j++)
        {
            sb.Append(" delete from CF_Sys_BadActionCode where fparentid in (");
            sb.Append("select fnumber from CF_Sys_BadActionCode where fparentid = '" + array2[j].ToString() + "') ");

            sb.Append(" delete from CF_Sys_BadActionCode where fparentid in (");
            sb.Append("select fnumber from CF_Sys_BadActionCode where fid = '" + array1[j].ToString() + "') ");

            sb.Append(" delete from CF_Sys_BadActionCode where  fid = '" + array1[j].ToString() + "' ");

        }
        if (sh.PExcute(sb.ToString()))
        {
            tool.showMessage("删除成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("删除失败");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

}
