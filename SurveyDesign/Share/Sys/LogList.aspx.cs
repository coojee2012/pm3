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

public partial class Admin_MainOther_LogList : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            txt_IP.Attributes.Add("onchange", "IsIp(this)");
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");
            InitPageControls();
            ShowInfo();
        }
    }

    //初始化页面控件
    private void InitPageControls()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = sh.GetTable("select fname,fnumber from cf_sys_systemname where fisdeleted=0");
        this.ddlFSystemType.DataSource = dt;
        this.ddlFSystemType.DataTextField = "FName";
        this.ddlFSystemType.DataValueField = "FNumber";
        this.ddlFSystemType.DataBind();
        this.ddlFSystemType.Items.Insert(0, new ListItem("---所有系统---", ""));
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_Log where 1=1 ");

        if (txt_BeginTime.Text.Trim() != "")
        {
            DateTime dt_begin = EConvert.ToDateTime(txt_BeginTime.Text.Trim());//begin time
            DateTime dt_end = DateTime.Now;
            if (txt_EndTime.Text.Trim() != "")
                dt_end = EConvert.ToDateTime(txt_EndTime.Text.Trim());

            dt_end = dt_end.AddDays(1);

            sb.Append(" and FLogTime between '" + dt_begin.ToShortDateString() + "' and '" + dt_end.ToShortDateString() + "' ");
        }

        //查询条件
        if (dpl_LogType.SelectedValue != "0")
        sb.Append(" and FLogType=" + dpl_LogType.SelectedValue);

        if (dpl_FOperation.SelectedValue != "0")
        sb.Append(" and FOperation=" + dpl_FOperation.SelectedValue);

        if (txt_Title.Text.Trim() != "")
        sb.Append(" and FTitle  like '%" + txt_Title.Text+"%' ");

        if (txt_Content.Text.Trim() != "")
        sb.Append(" and FDescription like '%" + txt_Content.Text.Trim()+"%' ");

        if (txt_IP.Text.Trim() != "")
        sb.Append(" and FIPAddress = '" + txt_IP.Text.Trim()+"'");

        if (txt_Server.Text.Trim() != "")
        sb.Append(" and FServerName like '%" + txt_Server.Text.Trim()+"%'");

        if (txt_Operator.Text.Trim() != "")
        {
            string opera = getFidByName(txt_Operator.Text.Trim());
            sb.Append(" and FOperator like '%" + opera + "%'");
        }
        if (!string.IsNullOrEmpty(txt_FMac.Text))
        {
            sb.Append(" and FMac like '%" + txt_FMac.Text+"%' ");
        }
        if (!string.IsNullOrEmpty(this.ddlFSystemType.SelectedValue))
        {
            sb.Append(" and FSysCode=" + this.ddlFSystemType.SelectedValue + " ");
        }

        sb.Append(" order by FLogTime desc ");
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

    private string getOperation(int? tid)
    {
        switch (tid)
        {

            case 1:
                return "系统";
            case 2:
                return "安全";
            case 3:
                return "应用";
            case 4:
                return "操作";
        }
        return "";
    }

    public string getUrl(object obj)
    {
        return string.Format("showApproveWindow(\"LogInfo.aspx?FID={0}\",940,790);", obj);

    }

    public string getFidByName(string fname)
    {
        string sql = "select FID from CF_Sys_User where FNAME='"+fname+"'";

        //CF_Sys_User cfu = pdb.CF_Sys_User.Where(c => c.FName.Equals(fname)).FirstOrDefault();

        //if (cfu != null)
        //{
        //    return cfu.FID;
        //}
        return "";
    }

    protected void LogInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            e.Item.Cells[2].Text = getLogType(EConvert.ToInt(e.Item.Cells[2].Text));
            e.Item.Cells[3].Text = getOperation(EConvert.ToInt(e.Item.Cells[3].Text));
            //操作者            
            string foperator = DataBinder.Eval(e.Item.DataItem,"FOperator").ToString();
            string str = "<a href=\"#\" onClick='showApproveWindow(\"ActionRecord.aspx?fuserid="+foperator+"\",940,790)'>";
            e.Item.Cells[5].Text = str+sh.GetSignValue("select FNAME from cf_sys_user where FID='"+foperator+"'")+"</a>";           
        }
    }
}
