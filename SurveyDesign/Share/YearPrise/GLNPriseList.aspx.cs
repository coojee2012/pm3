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
using Approve.EntityBase;
using Approve.Common;
using Approve.RuleCenter;

public partial class Admin_mainother_GLNPriseList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确定删除所选项吗？');");
            showInfo();
        }
    }

    //条件
    private string getCon()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["fcol"]))
            lPostion.Text = rc.GetSignValue("select fname from cf_Sys_tree where fnumber='" + Request.QueryString["fcol"] + "'");
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(t_FKHNR.Text.Trim()))
            sb.Append(" and FKHNR like '%" + t_FKHNR.Text.Trim() + "%' ");
        sb.Append(" and FType='" + Request.QueryString["FType"] + "'");
        return sb.ToString();
    }
    //显示
    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_YearPrise where FIsDeleted=0 ");
        sb.Append(getCon());
        sb.Append("order by FType desc ");

        this.Pager1.className = "RCenter";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;
        this.Pager1.controltopage = "Dic_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();

    }

    //列表
    protected void Dic_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sUrl = "showApproveWindow('GLNPriseEdit.aspx?fid=" + fid + "&fType=" + Request.QueryString["fType"] + "',600,350);";
            e.Item.Cells[2].Text = "<a href='#' onclick=\"" + sUrl + "\">" + e.Item.Cells[2].Text + "</a>";
        }
    }


    #region 按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_Sys_YearPrise", "FID");
        tool.DelInfoFromGrid(Dic_List, sl, "");
        showInfo();
    }
    #endregion
}
