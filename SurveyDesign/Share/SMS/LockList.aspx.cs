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

public partial class Admin_mainother_SmsList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConstromBind();
            showInfo();
        }
    }
    private void ConstromBind()
    {
        this.t_FType.Items.Add(new ListItem("已删除", "3"));
        this.t_FType.Items.Add( new ListItem("已修改", "2"));
        this.t_FType.Items.Add( new ListItem("已新增", "1"));
        this.t_FType.Items.Insert(0, new ListItem("请选择", "0"));

    }
    //条件
    private string getCon()
    {
        StringBuilder sb = new StringBuilder();

        if (!string.IsNullOrEmpty(t_FSubmitTime1.Text))
        {
            sb.Append("and  FcreateTime>='" + t_FSubmitTime1.Text + "' ");
        }
        if (!string.IsNullOrEmpty(t_FSubmitTime2.Text))
        {
            sb.Append(" and FcreateTime<='" + t_FSubmitTime2.Text + "' ");
        }
        if (!string.IsNullOrEmpty(t_FAdmin.Text))
        {
            sb.Append(" and FCreateName like '%" + t_FAdmin.Text.Trim() + "%' ");
        }
        if (!string.IsNullOrEmpty(t_FUserName.Text))
        {
            sb.Append(" and FUserName like '%" + t_FUserName.Text.Trim() + "%' ");
        }

        if (t_FType.SelectedValue.ToString()!="0")
        {
            sb.Append(" and FType= '" + t_FType.SelectedValue.ToString() + "' ");
        }
        return sb.ToString();
    }

    //显示
    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select case ftype when 1 then '已新增' when 2 then '已修改' when 3 then '已删除' end 'fState',* from CF_Prj_Log where FIsDeleted=0 ");
        sb.Append(getCon());
        sb.Append("order by FcreateTime desc ");

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
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
