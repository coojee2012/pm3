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

public partial class Share_SysSet_DepartmentList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
            this.btnAdd.Attributes.Add("onclick", "showAddWindow('DepartmentAdd.aspx?fparentid=" + Request.QueryString["fparentid"] + "',460,370)");

            if (String.IsNullOrEmpty(Request.QueryString["fparentid"]))
            {
                this.btnReturn.Visible = false;
            }
        }
    }
    private void ControlBind()
    {
        DataTable dt = sh.getDicTbByFNumber("103");
        this.drop_Flevel.DataSource = dt;
        this.drop_Flevel.DataTextField = "FName";
        this.drop_Flevel.DataValueField = "FNumber";
        this.drop_Flevel.DataBind();
        this.drop_Flevel.Items.Insert(0, new ListItem("请选择", ""));

        dt = sh.getDicTbByFNumber("102");
        this.drop_FClassNumber.DataSource = dt;
        this.drop_FClassNumber.DataTextField = "FName";
        this.drop_FClassNumber.DataValueField = "FNumber";
        this.drop_FClassNumber.DataBind();
        this.drop_FClassNumber.Items.Insert(0, new ListItem("请选择", ""));
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FIsDeleted,FName,FNumber,FClassNumber,FCnumber,FLevel,FFullName,");
        sb.Append("(select top 1 FName from cf_sys_dic where fnumber=t.FClassNumber and fisDeleted=0)FClassNumberName,");
        sb.Append("(select top 1 fname from cf_sys_dic where fnumber=t.FLevel and FParentId=103)FLevelName from CF_Sys_Department t where t.FIsDeleted=0 ");
        #region
        if (this.text_FNumber.Text.Trim() != "")
        {
            sb.Append(" and t.FNumber='" + EConvert.ToInt(text_FCNumber.Text.Trim()) + "'");
        }

        if (this.drop_Flevel.SelectedValue != "")
        {
            int Flevel = EConvert.ToInt(drop_Flevel.SelectedValue);
            sb.Append(" and t.FLevel='" + Flevel + "' ");
        }

        if (this.drop_FClassNumber.SelectedValue != "")
        {
            int FClassNumber = EConvert.ToInt(drop_FClassNumber.SelectedValue);
            sb.Append(" and t.FClassNumber='" + FClassNumber + "' ");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["fparentid"]))
        {
            sb.Append(" and fparentid in ");
            sb.Append("(select top 1 fnumber from CF_Sys_Department where fid='" + Request["fparentid"] + "')");
        }
        else
        {
            sb.Append(" and t.fparentId is null ");
        }
        #endregion
        sb.Append("Order By Fnumber,FCreateTime Desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "ManageDept_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLevel = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLevel"));
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('DepartmentAdd.aspx?fid=" + fid + "&fparentid=" + Request.QueryString["fparentid"] + "',460,370);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Dic_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;

            if (e.CommandName == "Add")
            {
                this.Response.Redirect("DepartmentList.aspx?fparentid=" + fid);
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (Request["fparentid"] != null && Request["fparentid"] != "")
        {
            string fparentNumber = sh.GetSignValue("select fparentid from CF_Sys_Department where fid='" + Request["fparentid"] + "'");
            if (fparentNumber == null || fparentNumber == "")
            {
                this.Response.Redirect("DepartmentList.aspx");
            }
            string fparentid = sh.GetSignValue("select fid from CF_Sys_Department where fnumber='" + fparentNumber + "'");
            this.Response.Redirect("DepartmentList.aspx?fparentid=" + fparentid);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sbFIds = new StringBuilder();
        int iCout = ManageDept_List.Columns.Count;
        for (int i = 0; i < ManageDept_List.Items.Count; i++)
        {
            CheckBox cb = ManageDept_List.Items[i].Cells[0].Controls[1] as CheckBox;
            if (cb != null && cb.Checked)
            {
                if (sbFIds.Length > 0)
                    sbFIds.Append(",");
                sbFIds.Append("'" + ManageDept_List.Items[i].Cells[iCout - 1].Text + "'");
            }
        }
        if (sbFIds.Length > 0)
            sh.PExcute("delete CF_Sys_Department where fid in (" + sbFIds + ")");
        ShowInfo();
    }
}
