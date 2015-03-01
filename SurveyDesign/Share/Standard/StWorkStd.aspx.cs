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
using System.Reflection;

public partial class Admin_Standard_StWorkStd : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            ShowInfo();
        }
    }
     
    private void ControlBind()
    {
 
            ShowType();
    }
    private void ShowType()
    {
        //t_FFQUALIFICATIONID.Items.Clear();
        //if (dbListId.SelectedValue == "")
        //    return;
        //DataTable dt = sh.getDicTbByFNumber(dbListId.SelectedValue);
        //t_FFQUALIFICATIONID.DataSource = dt;
        //t_FFQUALIFICATIONID.DataTextField = "FName";
        //t_FFQUALIFICATIONID.DataValueField = "FNumber";
        //t_FFQUALIFICATIONID.DataBind();
        //if (dt.Rows.Count != 1)
        //    t_FFQUALIFICATIONID.Items.Insert(0, new ListItem("请选择", ""));
    }
 

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (txtFName.Text != "")
            sb.Append(" and fname like '%" + txtFName.Text + "%'");
       
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fid,fname,FRELATION,FTARGETVALUE,FTARGETUNIT,FFQUALIFICATIONID,FLEVELID,FTTYPEID,FISLEAF,FNEEDCOUNT,");
        sb.Append(" case FOption when 1 then '是' when 0 then '否' end as FOption");
        sb.Append(" from CF_Sys_AppStand t ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(" and fkind=1 ");
        sb.Append(GetCon());
        sb.Append(" and fsystemId='" + Request.QueryString["fsysId"] + "'");
        sb.Append(" order by FFQUALIFICATIONID,flevelid,forder");


        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "DG_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLevelId = e.Item.Cells[3].Text;
            string fTypeId = e.Item.Cells[4].Text;

        
          //  e.Item.Cells[4].Text = sh.GetSignValue(EntityTypeEnum.EsProjectType, "FContent", "fid='" + fTypeId + "'");
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('StWorkStdEdit.aspx?fsysId=" + Request.QueryString["fsysId"] + "&fid=" +
                fid + "&fpid=" + Request["fpid"] + "',600,450);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(this.DG_List, EntityTypeEnum.EsAppStand, "dbShare");
        ShowInfo();
    }
}
