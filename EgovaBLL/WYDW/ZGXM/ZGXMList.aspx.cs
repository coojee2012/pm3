using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using Approve.RuleCenter;
using Seaskyer.Strings;


public partial class WYDW_ZGXM_ZGXMList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    protected void t_ProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            Session["XMQK_XMBH"] = e.Item.Cells[5].Text;

            if (e.CommandName == "See")
                Response.Write("<script language='javascript'>parent.parent.document.location='../XMQK/aIndex.aspx';</script>");
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = rc.GetSignValue("select FFullName from CF_Sys_ManageDept where FNumber='" + e.Item.Cells[6].Text + "'");
            e.Item.Cells[4].Text = rc.GetSignValue("select FName from CF_Sys_Dic where FParentid='" + e.Item.Cells[7].Text + "'");
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {

    }

    private void showInfo()
    {
        string strsql = "select * from WY_XM_JBXX where FBaseInfoID='" + CurrentEntUser.EntId + "'";

        this.Pager1.sql = strsql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
}