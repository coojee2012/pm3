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
public partial class Admin_main_UserLockInfoList : Page
{
    Share rc = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            ShowInfo();
        }
    }


    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();

        if (this.text_FFactory.Text != "")
        {
            sb.Append(" and FFactory  like '");
            sb.Append(this.text_FFactory.Text + "%'");
        }
        if (this.text_FMode.Text != "")
        {
            sb.Append(" and FMode  like '");
            sb.Append(this.text_FMode.Text + "%'");
        }
        if (this.text_FNo.Text != "")
        {
            sb.Append(" and FNo  like '");
            sb.Append(this.text_FNo.Text + "%'");
        }
        return sb.ToString();
    }
    private void ShowInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("select FId,FNo,FDate,FMode,FFactory,FBuyPerson,FPrice,FLinkMan,FLinkTel,FCount ");
        sb.Append("From CF_Sys_UserLockInfo ");
        sb.Append("Where FIsDeleted=0 ");
        sb.Append(GetCon());
        sb.Append("Order By FDate Desc");

        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "UserLockInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[2].Text = "<a href='#' class='link3' onclick=\"showAddWindow('UserLockInfoAdd.aspx?fid=" + fid + "',378,400);\">" + e.Item.Cells[2].Text + "</a>";


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
        tool.DelInfoFromGrid(this.UserLockInfo_List, EntityTypeEnum.EsUserLockInfo, "dbShare");
        ShowInfo();
    }
}
