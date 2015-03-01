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


public partial class Admin_main_QualiAppConditionList : Page
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
        sb.Append(" select fnumber, fname from cf_sys_systemname where fisdeleted=0 ");
        sb.Append(" order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.dbSystem.DataSource = dt;
        this.dbSystem.DataTextField = "FName";
        this.dbSystem.DataValueField = "FNumber";
        this.dbSystem.DataBind();


        dbSystem.Items.Insert(0, new ListItem("请选择", ""));
        dbQualiLevel.Items.Insert(0, new ListItem("请先选择所属系统", ""));
    }

    private void ShowQualiLevelData(string fSystemId)
    {


        string sSystemid = "";
        switch (fSystemId)
        {
            case "101": //施工企业
                sSystemid = "100";
                break;

            case "120": //招标代理企业
                sSystemid = "108";
                break;

            case "125": //工程监理企业
                sSystemid = "105";
                break;

            case "130": //房地产企业
                sSystemid = "150";
                break;

            case "135": //园林绿化企业
                sSystemid = "180";
                break;

            case "140": //外来勘察设计企业
                sSystemid = "170";
                break;

            case "145": //施工图审查企业
                sSystemid = "160";
                break;

            case "150": //安全生产许可证管理信息系统
                sSystemid = "100";
                break;

            case "155": //勘察设计企业
                sSystemid = "190";

                break;
            case "160": //监理工程师管理系统
                sSystemid = "190";

                break;
            case "165": //建造师管理系统
                sSystemid = "210";
                break;
        }
        if (sSystemid == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from CF_Sys_QualiLevel");
        sb.Append(" where fsystemid=" + sSystemid);
        sb.Append(" and fisdeleted=0 order by flevel");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            this.dbQualiLevel.Items.Clear();
            this.dbQualiLevel.DataSource = dt;
            this.dbQualiLevel.DataTextField = "FName";
            this.dbQualiLevel.DataValueField = "FNumber";
            this.dbQualiLevel.DataBind();
            this.dbQualiLevel.Items.Insert(0, new ListItem("请选择", ""));
        }
    }


    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.text_FName.Text != "")
        {
            sb.Append(" and fname like '");
            sb.Append(this.text_FName.Text + "%'");
        }
        if (this.dbSystem.SelectedValue.Trim() != "")
        {
            sb.Append(" and fsystemid =");
            sb.Append(this.dbSystem.SelectedValue.Trim() + "");
        }
        if (this.dbQualiLevel.SelectedValue.Trim() != "")
        {
            sb.Append(" and FQualiLevelId =");
            sb.Append(this.dbQualiLevel.SelectedValue.Trim() + "");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select FId,FName,");
        sb.Append(" (select top 1 fname from cf_sys_systemname where fnumber = fsystemid) fsystemname ,");
        sb.Append(" (select top 1 fname from CF_Sys_QualiLevel where fnumber = FQualiLevelId) FQualiLevel ");

        sb.Append(" From CF_App_QualiCondition ");
        sb.Append(" Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append(" and fparentid is null ");
        sb.Append(" Order By FSystemId,Ftime Desc");
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
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showApproveWindow('QualiAppConditionAdd.aspx?fid=" + fid + "',557,360);\">" + e.Item.Cells[2].Text + "</a>";



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
        tool.DelInfoFromGrid(this.QualiLevel_List, EntityTypeEnum.EaQualiCondition, "dbShare", "DelQualiAppCondition");
        ShowInfo();
    }
    protected void dbSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowQualiLevelData(dbSystem.SelectedValue.Trim());
    }
    protected void QualiLevel_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            this.Response.Redirect("SQualiAppConditionList.aspx?fpid=" + fid);
        }
    }
}
