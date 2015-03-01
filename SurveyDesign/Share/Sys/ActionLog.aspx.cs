using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Approve.EntityBase;
using Approve.RuleCenter;
using Approve.Common;

public partial class Share_Sys_ActionLog : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowInfo();
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Com_News where 1=1 ");

        if (txt_BeginTime.Text.Trim() != "")
        {
            DateTime dt_begin = EConvert.ToDateTime(txt_BeginTime.Text.Trim());//begin time
            DateTime dt_end = DateTime.Now;
            if (txt_EndTime.Text.Trim() != "")
                dt_end = EConvert.ToDateTime(txt_EndTime.Text.Trim());

            dt_end = dt_end.AddDays(1);

            sb.Append(" and Title between '" + dt_begin.ToShortDateString() + "' and '" + dt_end.ToShortDateString() + "' ");
        }

        //查询条件
        if (dpl_LogType.SelectedValue != "0")
            sb.Append(" and FLogType=" + dpl_LogType.SelectedValue);
        
        sb.Append(" order by Title desc ");
        this.Pager1.className = "dbShare";
        this.Pager1.sql = sb.ToString();
        this.Pager1.pagecount = 20;

        this.Pager1.controltopage = "LogInfo_List";
        this.Pager1.controltype = "DataGrid";
        this.Pager1.dataBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        //sl.Add("",);

        //tool.DelInfoFromGrid(LogInfo_List,,"dbShare");
        ShowInfo();
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    private string getLogType(int? tid)
    {
        switch (tid)
        {
            case 1:
                return "<img src=\"../../image/info.jpg\" alt=\"信息\" title=\"信息日志\" />";
            case 4:
                return "<img src=\"../../image/info.jpg\" alt=\"信息\" title=\"信息日志\" />";
            case 5:
                return "<img src=\"../../image/info.jpg\" alt=\"信息\" title=\"信息日志\" />";
            case 2:
                return "<img src=\"../../image/error.jpg\" alt=\"错误\" title=\"错误日志\" />";
            case 3:
                return "<img src=\"../../image/warring.jpg\" alt=\"警告\" title=\"警告日志\" />";

        }
        return "";
    }

    public string getUrl(object obj)
    {
        return string.Format("showApproveWindow(\"ACLogInfo.aspx?FID={0}\",940,790);", obj);

    }

    protected void LogInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

         e.Item.Cells[2].Text = getLogType(EConvert.ToInt(e.Item.Cells[2].Text));
        }
    }
}
