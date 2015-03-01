using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Data;

public partial class Share_Sys_SysNameList : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowTitle();
            ControlBind();
            showInfo();
            if (Request["ftype"].Trim() == "1" || Request["ftype"].Trim() == "2")
            {
                this.DG_List.Columns[8].Visible = false;
            }
            if (Request["fparentid"] == null || Request["fparentid"] == "")
            {
                btnReturn.Visible = false;
            }
        }
    }
    void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fname,fnumber from CF_Sys_Platform order by fnumber");
        DataTable dt = sh.GetTable(sb.ToString());
        t_FPlamId.DataSource = dt;
        t_FPlamId.DataTextField = "FName";
        t_FPlamId.DataValueField = "FNumber";
        t_FPlamId.DataBind();
        t_FPlamId.Items.Insert(0, new ListItem("全部", ""));
    }
    private void ShowTitle()
    {
        if (Request["ftype"] != null)
        {
            switch (Request["ftype"])
            {
                case "1":
                    lTitle.Text = "审核角色";
                    break;
                case "2":
                    lTitle.Text = "菜单角色";
                    break;
                case "3":
                    lTitle.Text = "管理角色";
                    break;
            }
        }
    }
    //条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.t_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.t_FName.Text + "%'");
        }
        if (this.t_FNumber.Text != "")
        {
            sb.Append(" and fnumber ='");
            sb.Append(this.t_FNumber.Text + "'");
        }
        if (!string.IsNullOrEmpty(t_FPlamId.SelectedValue))
            sb.Append(" and FPlatId='" + t_FPlamId.SelectedValue + "' ");
        if (!string.IsNullOrEmpty(Request.QueryString["fparentid"]))
        {
            sb.Append(" and fparentid='");
            sb.Append(sh.GetSignValue(EntityTypeEnum.EsRole, "FNumber", "fid='" + Request["fparentid"] + "'"));
            sb.Append("' ");
        }
        else
        {
            sb.Append(" and (fparentid is null or fparentid = '') ");
        }
        if (Request["ftype"] != null && Request["ftype"] != "")
        {
            sb.Append(" and ftypeid=" + Request["ftype"]);
        }
        return sb.ToString();
    }

    //显示
    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select * ");
        sb.Append(" From CF_Sys_Role ");
        sb.Append(" where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" Order By forder,FCreateTime desc ");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }


    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fNumber = e.Item.Cells[3].Text;
            string fParentNumber = sh.GetSignValue(EntityTypeEnum.EsRole, "fparentid", "fid='" + fid + "'");
            string fParentId = sh.GetSignValue(EntityTypeEnum.EsRole, "fid", "fnumber='" + fParentNumber + "'");
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('RoleAdd.aspx?fid=" + fid + "&fparentid=" + fParentId + "',500,300);\">" + e.Item.Cells[2].Text + "</a>";

            e.Item.Cells[e.Item.Cells.Count - 3].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('RoleSet.aspx?froleid=" + fNumber + "',600,700);\">权限设置</a>";

            StringBuilder sb = new StringBuilder();
            sb.Append(" select fname from cf_sys_platform where fnumber='" + e.Item.Cells[5].Text + "'");
            string fName = sh.GetSignValue(sb.ToString());
            e.Item.Cells[5].Text = fName;

        }
    }
    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    //删除按钮
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.DG_List, EntityTypeEnum.EsRole, "dbShare");
        showInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (e.CommandName == "Add")
            {
                this.Response.Redirect("RoleList.aspx?fparentid=" + fid + "&ftype=" + Request["ftype"]);
            }
            TextBox Forder = (TextBox)e.Item.Cells[e.Item.Cells.Count - 5].Controls[1];
            if (e.CommandName == "Save")
            {
                pageTool tool = new pageTool(this.Page);
                if (sh.PExcute("update CF_Sys_Role set forder=" + Forder.Text + " where fid='" + fid + "'"))
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
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (Request["fparentid"] != null && Request["fparentid"] != "")
        {
            string fparentNumber = sh.GetSignValue(EntityTypeEnum.EsRole, "fparentid", "fid='" + Request["fparentid"] + "'");
            if (fparentNumber == null || fparentNumber == "")
            {
                this.Response.Redirect("RoleList.aspx?ftype=" + Request["ftype"]);
            }
            string fparentid = sh.GetSignValue(EntityTypeEnum.EsRole, "fid", "fnumber='" + fparentNumber + "'");
            this.Response.Redirect("RoleList.aspx?fparentid=" + fparentid + "&ftype=" + Request["ftype"]);
        }
    }
}
