using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class Government_AppWY_XMSearch : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    private string FFid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showinfo();
        }
    }

    private void showinfo()
    {
        string strsql = "select k.SCJGRQ,w.FID,w.XMMC,w.XMSD,w.JSDW,w.XMBH,w.XMLX,Isnull(c.Fname,'<font color=red>无企业在管此项目</font>') As FName from wy_xm_Jbxx w left join cf_ent_baseinfo c on w.fbaseinfoid=c.fid left join WY_XM_KZXX k on w.XMBH=k.XMBH where 1=1";
        if (txtXMMC.Text.Trim() != "")
        {
            strsql += " and w.XMMC like '%" + txtXMMC.Text.Trim() + "%'";
        }
        if (txtQYMC.Text.Trim() != "")
        {
            strsql += " and c.Fname like '%" + txtQYMC.Text.Trim() + "%'";
        }
        if (govd_FRegistDeptId.FNumber != null && govd_FRegistDeptId.FNumber != "51")
        {
            strsql += " and w.XMSD like '" + govd_FRegistDeptId.FNumber + "%'";
        }
        if (txtJSDW.Text.Trim() != "")
        {
            strsql += " and w.JSDW like '%" + txtJSDW.Text.Trim() + "%'";
        }

        this.Pager1.sql = strsql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    protected void dg_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[3].Text = rc.GetSignValue("select FFullName from CF_Sys_ManageDept where FNumber='" + e.Item.Cells[8].Text + "'");
            e.Item.Cells[5].Text = rc.GetSignValue("select FName from CF_Sys_Dic where FParentid='" + e.Item.Cells[9].Text + "'");
            //e.Item.Cells[6].Text = "<a href='#' onclick=\"showApproveWindow1('XMQZSQ.aspx?XMBH=" + e.Item.Cells[7].Text + "','600','260')\">强制失去</a>";
            //try
            //{
            //    e.Item.Cells[6].Text = DateTime.Parse(e.Item.Cells[10].Text.ToString()).ToShortDateString();
            //}
            //catch (Exception)
            //{
            //    e.Item.Cells[6].Text = "";
            //}
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showinfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string xmbh = e.Item.Cells[7].Text;
            string qymc = e.Item.Cells[10].Text;
            string fid = e.Item.Cells[11].Text;
            if (e.CommandName == "See")
            {
                Session["GovLinkID"] = "1";
                Session["FManageTypeId"] = "";
                Session["FAppID"] = "1";
                Session["XMBH"] = xmbh;
                //this.Session["FManageTypeId"] = fMType;
                //if (fState != 0 && fState != 2)
                Session["FIsApprove"] = 1;
                //else
                //    Session["FIsApprove"] = 0;BType
                if (qymc != "<font color=red>无企业在管此项目</font>")
                {
                    Response.Write("<script language='javascript'>window.open('../../WYDW/XMQK/aIndex.aspx');</script>");
                }
                else
                {
                    this.RegisterStartupScript(new Guid().ToString(), "<script>ShowXMPage('../../WYDW/LSXM/LSXMInfo.aspx?FID=" + fid + "&type=3','980','560');</script>");
                }
            }
        }
    }
}