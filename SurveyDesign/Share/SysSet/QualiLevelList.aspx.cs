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

public partial class Admin_main_QualiLevelList : Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ControlBind();
            ShowInfo();
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,Fname,fnumber from CF_Sys_SystemName where fisdeleted=0 order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.drop_FSystemIdName.DataSource = dt;
        this.drop_FSystemIdName.DataTextField = "FName";
        this.drop_FSystemIdName.DataValueField = "FNumber";
        this.drop_FSystemIdName.DataBind();
        this.drop_FSystemIdName.Items.Insert(0, new ListItem("请选择", ""));
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

        if (this.drop_FSystemIdName.SelectedValue != "")
        {
            sb.Append(" and FSystemName='");
            sb.Append(this.drop_FSystemIdName.SelectedValue + "'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FName,fnumber,FLevel,FSystemId, ");
        sb.Append("(select top 1 fname from CF_Sys_SystemName where fnumber=FSystemName) FSystemName ");
        sb.Append("From CF_Sys_QualiLevel ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By FSystemId,FLevel Desc");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "QualiLevel_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void QualiLevel_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('QualiLevelAdd.aspx?fid=" + fid + "',557,360);\">" + e.Item.Cells[2].Text + "</a>";

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
        tool.DelInfoFromGrid(this.QualiLevel_List, EntityTypeEnum.EsQualiLevel, "dbShare");
        ShowInfo();
    }
}
