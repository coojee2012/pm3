using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.Common;
using Approve.EntityBase;
using Approve.RuleCenter;
using System.Data;

public partial class Admin_main_LogRecord : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    /// <summary>
    /// 日志查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtTitle.Text = DateTime.Now.ToShortDateString();//显示当天的日期
            txtOverTitle.Text = DateTime.Now.ToShortDateString();
            InitPageControls();
            //ShowInfo();
        }
    }

    //初始化页面控件
    private void InitPageControls()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = rc.GetTable("select fname,fnumber from cf_sys_systemname where fisdeleted=0");
        this.ddlFSystemType.DataSource = dt;
        this.ddlFSystemType.DataTextField = "FName";
        this.ddlFSystemType.DataValueField = "FNumber";
        this.ddlFSystemType.DataBind();
        this.ddlFSystemType.Items.Insert(0, new ListItem("-全部-", ""));
    }

    //查询条件
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            sb.Append(" and Convert(datetime,Title)>='" + txtTitle.Text.Trim() + " 0:00:00'");
        }
        if (!string.IsNullOrEmpty(txtOverTitle.Text.Trim()))
        {
            sb.Append(" and Convert(datetime,Title)<='" + txtOverTitle.Text.Trim() + " 23:59:59'");
        }
        if (!string.IsNullOrEmpty(txtUser.Text.Trim()))
        {
            sb.Append(" and t1.FUserId in (select FId from cf_sys_User where FName like '%" + txtUser.Text.Trim() + "%') ");
        }
        //if (!string.IsNullOrEmpty(txtDb.Text.Trim()))
        //{
        //    sb.Append(" and FDbName='" + txtDb.Text.Trim() + "' ");
        //}
        if (ddlAction.SelectedIndex > 0 && ddlAction.SelectedIndex < 4)
        {
            sb.Append(" and Content like '%" + ddlAction.SelectedValue + "%'");
        }
        if (ddlAction.SelectedIndex == 4)
        {
            sb.Append(" and (Content like '" + ddlAction.SelectedValue + "%' ");
            sb.Append(" or Content like 'begin " + ddlAction.SelectedValue + "%')");
        }
        if (!string.IsNullOrEmpty(txtTable.Text.Trim()))
        {
            sb.Append(" and Content like '%" + txtTable.Text.Trim() + "%'");
        }
        if (ddlKey.SelectedIndex == 0)
        {
            if (!string.IsNullOrEmpty(txtWordKeys.Text.Trim()))
                sb.Append(" and (Content like '%" + txtWordKeys.Text.Trim() + "%' or errmsg like '%" + txtWordKeys.Text.Trim() + "%')");
            if (!ckIsNull.Checked)
            {
                sb.Append(" and convert(varchar,[content])<>'' and isNull(errmsg,'')<>''");
            }
        }
        if (ddlKey.SelectedIndex > 0)
        {
            sb.Append(" and FLogType='" + ddlKey.SelectedValue + "' ");
            if (!string.IsNullOrEmpty(txtWordKeys.Text.Trim()))
                sb.Append(" and (Content like '%" + txtWordKeys.Text.Trim() + "%' or errmsg like '%" + txtWordKeys.Text.Trim() + "%')");
            if (!ckIsNull.Checked)
            {
                sb.Append(" and convert(varchar,[content])<>'' and isNull(errmsg,'')<>''");
            }
        }
        return sb.ToString();
    }
    //显示信息
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * ");
        sb.Append(" from CF_Com_News t1 where 1=1 ");
        sb.Append(this.GetCon());
        sb.Append(" and datediff(mm,Title,getDate())<=3 ");
        sb.Append(" order by Title desc ");

        switch (dbType.SelectedIndex)
        {
            case 0:
                Pager1.className = "RCenter";
                break;
            case 1:
                Pager1.className = "RQuali";
                break;
        }
        Pager1.sql = sb.ToString();
        Pager1.controltype = "DataGrid";
        Pager1.controltopage = "Log_List";
        Pager1.pagecount = 15;
        Pager1.dataBind();
    }
    //搜索信息
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ShowInfo();
    }
    //绑定数据库信息
    int count = 0;
    protected void AppQuali_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            count++;
            e.Item.Cells[1].Text = count.ToString();
            e.Item.Cells[2].Text = Convert.ToDateTime(e.Item.Cells[2].Text).ToShortDateString();
            string info = e.Item.Cells[3].Text;
            if (info.Length > 50)
            {
                e.Item.Cells[3].Text = info.Substring(0, 50) + "....";
            }
            e.Item.Cells[5].Text = rc.GetSignValue("select FName from cf_sys_user where fid='" + DataBinder.Eval(e.Item.DataItem, "FUserId") + "'");
            e.Item.Cells[6].Text = dbType.SelectedValue;
            string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string sUrl = "showInfo('LogDetail.aspx?FId=" + FId + "&dbType=" + dbType.SelectedValue + "')";
            e.Item.Cells[3].Text = "<a href='#' onclick=\"" + sUrl + "\">" + e.Item.Cells[3].Text + "</a>";
            info = e.Item.Cells[4].Text;
            if (info.Length > 40)
            {
                e.Item.Cells[4].Text = info.Substring(0, 40) + "....";
            }
        }
    }
}
