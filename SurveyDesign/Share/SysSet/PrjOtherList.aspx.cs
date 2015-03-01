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
using Approve.EntitySys;

public partial class Admin_yamain_PrjListAdd : adminBasePage
{
    Share rc = new Share();
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
        if (this.text_FName.Text != "")
        {
            sb.Append(" and FFileName like '");
            sb.Append(this.text_FName.Text + "%'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FFileName,FFileAmount,FRemark,FManageId,FOrder,FType,");
        sb.Append(" case FIsMust when 1 then '<span style=\"color:red\">是</span>' else '否' end FIsMust from CF_Sys_PrjList");
        sb.Append(" where FIsDeleted=0");
        sb.Append(" and FManageId='" + Request["FManageId"] + "'");
        sb.Append(GetCon());
        sb.Append("Order By forder,FCreateTime Desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "PrjOhter_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    private void SaveAllInfo()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        int itemCount = this.PrjOhter_List.Items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            string fid = this.PrjOhter_List.Items[i].Cells[this.PrjOhter_List.Columns.Count - 1].Text;
            TextBox Forder = (TextBox)PrjOhter_List.Items[i].Cells[PrjOhter_List.Columns.Count - 4].Controls[1];
            if (Forder.Text == "")
            {
                this.PrjOhter_List.Items[i].BackColor = System.Drawing.Color.FromArgb(248, 250, 73);
                Forder.Text = "0";
            }
            sb.Append(" update CF_Sys_PrjList set forder=" + Forder.Text + " where fid='" + fid + "' ");
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

    protected void PrjOhter_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FManageId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageId"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('PrjOhterAdd.aspx?fid=" + FID + "&FManageId=" + FManageId + "',500,450);\">" + e.Item.Cells[2].Text + "</a>";

            //文件形式
            e.Item.Cells[3].Text = FType == "1" ? "上传文件" : FType == "2" ? "<tt>查看业务办理情况</tt>" : FType == "3" ? "<tt>施工许可证</tt>" : "";
        }
    }

    protected void PrjOhter_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            StringBuilder sb = new StringBuilder();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (e.CommandName == "Save")
            {
                TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 4].Controls[1];
                sb.Append(" update CF_Sys_PrjList set forder=" + Forder.Text + " where fid='" + fid + "' ");
                rc.PExcute(sb.ToString());
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAllInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.PrjOhter_List, EntityTypeEnum.EsPrjList, "RCenter");
        ShowInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
