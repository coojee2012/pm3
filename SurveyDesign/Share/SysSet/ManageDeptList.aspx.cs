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

public partial class Share_SysSet_ManageDeptList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
            this.btnAdd.Attributes.Add("onclick", "showAddWindow('ManageDeptAdd.aspx?fparentid=" + Request["fparentid"] + "',460,420)");
            if (Request["fparentid"] == null || Request["fparentid"] == "")
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
        if (this.drop_Flevel.SelectedValue != "")
        {
            sb.Append(" and Flevel=");
            sb.Append(this.drop_Flevel.SelectedValue + "");
        }
        if (this.drop_FClassNumber.SelectedValue != "")
        {
            sb.Append(" and FClassNumber='");
            sb.Append(this.drop_FClassNumber.SelectedValue + "' ");
        }
        if (this.text_FCNumber.Text != "")
        {
            sb.Append(" and FCNumber ='");
            sb.Append(this.text_FNumber.Text + "'");
        }
        if (Request["fparentid"] != null && Request["fparentid"] != "")
        {
            sb.Append(" and fparentid='");
            sb.Append(sh.GetSignValue(EntityTypeEnum.EsManageDept, "FNumber", "fid='" + Request["fparentid"] + "'"));
            sb.Append("' ");
        }
        else
        {
            sb.Append(" and Flevel in (0,1,999999992)");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["provice"]))
            sb.Append(" and fnumber like '" + Request.QueryString["provice"] + "%'");
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,FNumber,FClassNumber,FCNumber,FLevel,FFullName,");
        sb.Append("(select top 1 fname from cf_sys_dic where fnumber = FClassNumber) FClassNumberName ");
        sb.Append("From CF_Sys_ManageDept ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By FOrder,Fnumber,FCreateTime Desc");
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
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('ManageDeptAdd.aspx?fid=" + fid + "&fparentid=" + Request["fparentid"] + "',460,420);\">" + e.Item.Cells[2].Text + "</a>";
            e.Item.Cells[4].Text = sh.GetDicName("103", e.Item.Cells[4].Text);


        }
    }
    protected void Dic_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;

            if (e.CommandName == "Add")
            {
                this.Response.Redirect("ManageDeptList.aspx?fparentid=" + fid);
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
            string fparentNumber = sh.GetSignValue(EntityTypeEnum.EsManageDept, "fparentid", "fid='" + Request["fparentid"] + "'");
            if (fparentNumber == null || fparentNumber == "")
            {
                this.Response.Redirect("ManageDeptList.aspx");
            }
            string fparentid = sh.GetSignValue(EntityTypeEnum.EsManageDept, "fid", "fnumber='" + fparentNumber + "'");
            string sUrl = "ManageDeptList.aspx?fparentid=" + fparentid;
            if (!string.IsNullOrEmpty(Request.QueryString["provice"]))
                sUrl += "&provice=" + Request.QueryString["provice"];
            this.Response.Redirect(sUrl);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.ManageDept_List, EntityTypeEnum.EsManageDept, "dbShare", "DelManageDept");
        ShowInfo();
    }
}
