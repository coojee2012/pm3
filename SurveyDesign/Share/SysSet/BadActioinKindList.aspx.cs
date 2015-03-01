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

public partial class Admin_main_BadActioinKindList : Page
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
        if (this.txtFnumber.Text != "")
        {
            sb.Append(" and csbc.fnumber like '%");
            sb.Append(this.txtFnumber.Text + "%'");
        }
        if (this.txtFname.Text != "")
        {
            sb.Append(" and csbc1.fname like '%");
            sb.Append(this.txtFname.Text + "%' ");
        }
        if (Request["fparentid"] != null && Request["fparentid"] != "")
        {
            string fNumber = sh.GetSignValue(EntityTypeEnum.EsBadActionCode, "FNumber", "FId='" + Request["fparentid"] + "' ");
            sb.Append(" and csbc.fparentid='");
            sb.Append(fNumber + "' ");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("select csbc1.fid fparentid,csbc.FId,csbc.FCodeId,csbc.FDesc,csbc.FOrder,csbc.FScore,csbc.fnumber,");
        sb.Append("csbc.fname,");
        sb.Append("csbc1.fname fparentName ");
        sb.Append("From CF_Sys_BadActionCode csbc,");
        sb.Append("CF_Sys_BadActionCode csbc1 ");
        sb.Append("Where csbc.FIsDeleted=0 and csbc1.fisdeleted=0 ");
        sb.Append(" and csbc.fparentid = csbc1.fnumber ");
        sb.Append(GetCon());
        sb.Append("Order By csbc.Forder,csbc.FCreateTime Desc");

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
            TextBox Forder = (TextBox)BadActioin_List.Items[i].Cells[BadActioin_List.Columns.Count - 5].Controls[1];
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
            string fparentid = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('BadActioinKindAdd.aspx?fid=" + fid + "&fparentid=" + fparentid + "',500,240);\">" + e.Item.Cells[3].Text + "</a>";


        }
    }
    protected void BadActioin_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 5].Controls[1];
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
                this.Response.Redirect("BadActionList.aspx?fparentid=" + fid);
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
            string fnumber = this.BadActioin_List.Items[i].Cells[4].Text;
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

    protected void btnReturn_Click1(object sender, EventArgs e)
    {
        this.Response.Redirect("BadActionTypeList.aspx");
    }
}


