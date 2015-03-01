using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Seaskyer.Strings;

public partial class WYDW_LSXM_LSXMList : System.Web.UI.Page
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
        //if (e.Item.ItemIndex > -1)
        //{
        //    Session["XMBH"] = e.Item.Cells[5].Text;
        //    if (e.CommandName == "See")
        //    {               
        //        Response.Write("<script language='javascript'>parent.parent.document.location='../XMQK/aIndex.aspx';</script>");
        //    }
        //}
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            //e.Item.Cells[2].Text = rc.GetSignValue("select FFullName from CF_Sys_ManageDept where FNumber='" + e.Item.Cells[6].Text + "'");
            //e.Item.Cells[4].Text = rc.GetSignValue("select FName from CF_Sys_Dic where FParentid='" + e.Item.Cells[7].Text + "'");
            e.Item.Cells[1].Text = "<a href='#' onclick=\"ShowXMPage('LSXMInfo.aspx?FID="+e.Item.Cells[e.Item.Cells.Count-1].Text+"','980','560')\">"+e.Item.Cells[e.Item.Cells.Count-2].Text+"</a>";
        }
    }
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {

    }

    private void showInfo()
    {
        string strsql = "select H.Fid, H.JSDW,H.XMBH,H.XMMC,M.FName As XMSD,D.FName As XMLX,Case H.InSystemLostReasonID When 1 Then '企业自动申请' When 3 Then '管理部门强制失去' When 2 Then '其它企业申请在管获批' End As SQYY  from  WY_XM_JBXX_History H Left Outer Join CF_Sys_ManageDept M On H.XMSD = M.FNumber Left Outer Join CF_Sys_Dic D ON H.XMLX = D.FNumber where FBaseInfoID='" + CurrentEntUser.EntId + "'";

        this.Pager1.sql = strsql;
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "dg_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
}