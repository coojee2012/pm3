using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;

public partial class WYDW_ApplyRYQK_RYQKList : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    public ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //conBind();
            showInfo();
        }
    }

    public void showInfo()
    {
        if (Session["FAppId"] != null)
        {
            string FBaseinfoID = CurrentEntUser.EntId;
            string sql = "select r.* from CF_App_List l inner join YW_WY_RY_JBXX r on l.FID=r.FAppID " +
                         "where l.FBaseinfoId='"
                + FBaseinfoID + "'and l.FCreateUser = '" + CurrentEntUser.UserId + "' and r.FAppId='" + Session["FAppId"].ToString() + "'";
            if (!string.IsNullOrEmpty(t_fPersonName.Text.Trim()))
            {
                sql += " and r.fPersonName like '%" + t_fPersonName.Text.Trim() + "%' ";
            }
            sql += " order by l.FCreateTime desc ";
            this.Pager1.className = "dbShare";
            this.Pager1.sql = sql;
            this.Pager1.pagecount = 20;
            this.Pager1.controltopage = "DG_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[8].Text;
            //if (e.CommandName == "See")
            //{
            //    this.Session["RYID"] = FId;
            //    ClientScript.RegisterStartupScript(GetType(), "", "<script>ShowAppPage('RYQKInfo.aspx?Type=2',900,500);</script>");
            //}

            if (e.CommandName == "Del")
            {
                //删除人员信息

                string sql = "delete YW_WY_RY_JBXX where FID='" + FId + "'";
                rc.PExcute(sql, true);
                pageTool tool = new pageTool(this.Page);
                showInfo();
                tool.showMessage("删除成功！");
            }
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");
            btnOp.Text = "删除";
            btnOp.CommandName = "Del";
            btnOp.Attributes.Add("onclick", "return confirm('确认要删除该人员信息吗?');");

            e.Item.Cells[1].Text = "<a href='#' onclick = \"ShowAppPage('RYQKInfo.aspx?FID=" + e.Item.Cells[8].Text + "','900','640');\">" + e.Item.Cells[1].Text + "</a>";
        }
    }
    protected void btnSXRY_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}