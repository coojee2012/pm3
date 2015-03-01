using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Data.SqlClient;
using System.Collections;
using ProjectData;

public partial class Government_statis_Business : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public DataTable dt = new DataTable();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            showInfo();
        }
    }


    //绑定下拉框
    private void conBind()
    {
        string sql = @"select min(FReportDate) from 
                    (select FReportDate from  CF_App_ProcessInstance union 
                    select FReportDate from CF_App_ProcessInstancebackup)t";
        string Year = DateTime.Now.Year.ToString();
        int minYear = EConvert.ToInt(Year);

        for (int i = minYear; i <= DateTime.Now.Year; i++)
        {
            txt_Year1.Items.Add(new ListItem(i.ToString(), i.ToString()));
            txt_Year2.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        for (int i = 1; i <= 12; i++)
        {
            DateTime dTime = DateTime.Now;

            txt_Year1.SelectedValue = dTime.Year.ToString();



            txt_Year2.SelectedValue = dTime.Year.ToString();
        }
    }

    private string getCondi()
    {

        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        sb.Append(" and ep.fstate = 6 ");
        return sb.ToString();
    }

    //显示
    private void showInfo()
    {

        string FSystemId = Request.QueryString["FSystemId"];// //系统类型
        //string mType = hidd_n.Value;//业务Number
        StringBuilder sb = new StringBuilder();
        /*
        * cf_App_ProcessRecord表 FMeasure 字段说明：
        * 0：刚报上来的
        * 1：已填写意见的
        * 3：已打下回级
        * 4：被上级打回
        * 5：已上报
        */
        sb.AppendLine(" select * from ");
        sb.AppendLine(" (");
        sb.AppendLine(" select ep.FManageTypeId,ep.FReportDate ");
        sb.AppendLine(" from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.AppendLine(" where ep.fId = er.FProcessInstanceID ");

        sb.AppendLine(" and er.fid in ");
        sb.AppendLine(" (select top 1 fid from CF_App_ProcessRecord t1 where t1.FProcessInstanceID= ep.fid order by t1.FReporttime desc)");
        sb.AppendLine(getCondi());
        sb.AppendLine(" union ");
        sb.AppendLine(" select ep.FManageTypeId,ep.FReportDate ");

        sb.AppendLine(" from CF_App_ProcessInstanceBackup ep,CF_App_ProcessRecordBackup er");
        sb.AppendLine(" where ep.fId = er.FProcessInstanceID ");

        sb.AppendLine(" and er.fid in ");
        sb.AppendLine(" (select top 1 fid from CF_App_ProcessRecordBackup t1 where t1.FProcessInstanceID= ep.fid order by t1.FReporttime desc)");
        sb.AppendLine(getCondi());
        sb.AppendLine(" ) ttt");

        DataTable dtReport = rc.GetTable(sb.ToString());

        //找出企业类型下所有业务


        DataTable dtMonth = new DataTable();

        if (rbQuarter.Checked)
        {
            dtMonth.Columns.Add(new DataColumn("月份"));
        }
        else
        {
            dtMonth.Columns.Add(new DataColumn("年度"));
        }

        sb = new StringBuilder();
        sb.AppendLine("select fname,fnumber from CF_Sys_ManageType ");
        sb.AppendLine("where fmtypeid<>'0' and FName not like '合同备案%' ");
        if (string.IsNullOrEmpty(FSystemId))
        {
            sb.AppendLine(" and fmtypeid=193 ");
        }
        else
        {
            sb.AppendLine(" and FSystemId='" + EConvert.ToInt(FSystemId) + "' ");
        }
        sb.AppendLine("   order by forder ");


        DataTable dt = rc.GetTable(sb.ToString());
        for (int i = 0; dt != null && i < dt.Rows.Count; i++)
        {
            dtMonth.Columns.Add(dt.Rows[i]["fname"].ToString());
        }
        //查询合同备案  
        dtMonth.Columns.Add("合同备案");
        //查询境外合同备案  
        dtMonth.Columns.Add("合同备案(省外)");
        dtMonth.Columns.Add("合计");
        if (rbQuarter.Checked)
        {
            for (int i = 0; i < 12; i++)
            {
                DataRow row = dtMonth.NewRow();
                row[0] = (i + 1) + "月";
                dtMonth.Rows.Add(row);
                GetValue(row, dtReport, new DateTime(EConvert.ToInt(txt_Year1.SelectedValue), i + 1, 1),
                    new DateTime(EConvert.ToInt(txt_Year1.SelectedValue), i + 1, 1).AddMonths(1));

            }
            DataRow rowCount = dtMonth.NewRow();
            rowCount[0] = "合计";
            dtMonth.Rows.Add(rowCount);
            GetValue(rowCount, dtReport, new DateTime(EConvert.ToInt(txt_Year1.SelectedValue), 1, 1),
              new DateTime(EConvert.ToInt(txt_Year1.SelectedValue) + 1, 1, 1));
        }
        else
        {
            for (int i = EConvert.ToInt(txt_Year1.SelectedValue); i <= EConvert.ToInt(txt_Year2.SelectedValue); i++)
            {
                DataRow row = dtMonth.NewRow();
                row[0] = i + "年";
                dtMonth.Rows.Add(row);
                GetValue(row, dtReport, new DateTime(i, 1, 1), new DateTime(i + 1, 1, 1));
            }
            DataRow rowCount = dtMonth.NewRow();
            rowCount[0] = "合计";
            dtMonth.Rows.Add(rowCount);
            GetValue(rowCount, dtReport, new DateTime(EConvert.ToInt(txt_Year1.SelectedValue), 1, 1),
              new DateTime(EConvert.ToInt(txt_Year2.SelectedValue) + 1, 1, 1).AddDays(-1));
        }
        DG_List.DataSource = dtMonth;
        DG_List.DataBind();
    }
    private void GetValue(DataRow drMonth, DataTable dtReport, DateTime StartTime, DateTime EndTime)
    {
        string FSystemId = Request.QueryString["FSystemId"];
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("select fname,convert(varchar,fnumber)fnumber ");
        sb.AppendLine("from CF_Sys_ManageType where  fmtypeid<>'0' ");
        sb.AppendLine("and FName not like '合同备案%' ");
        if (string.IsNullOrEmpty(FSystemId))
        {
            sb.AppendLine(" and fmtypeid=193 ");
        }
        else
        {
            sb.AppendLine(" and FSystemId='" + EConvert.ToInt(FSystemId) + "' ");
        }
        sb.AppendLine("order by forder ");
        dt = rc.GetTable(sb.ToString());
        //查询合同备案 
        var htList = db.CF_Sys_ManageType.Where(t => t.FName == "合同备案")
            .Select(t => t.FNumber.ToString()).ToList();
        if (htList != null)
        {
            DataRow dr = dt.NewRow();
            dr["FName"] = "合同备案";
            dr["FNumber"] = string.Join(",", htList.ToArray());
            dt.Rows.Add(dr);
        }
        //查询境外合同备案 
        var jwList = db.CF_Sys_ManageType.Where(t => t.FName.Contains("合同备案")
            && t.FName.Contains("省外"))
            .Select(t => t.FNumber.ToString()).ToList();
        if (jwList != null)
        {
            DataRow dr = dt.NewRow();
            dr["FName"] = "合同备案(省外)";
            dr["FNumber"] = string.Join(",", jwList.ToArray());
            dt.Rows.Add(dr);
        }
        for (int i = 0; dt != null && i < dt.Rows.Count; i++)
        {
            string strSelect = " FReportDate>'"
            + EConvert.ToShortDateString(StartTime)
            + "' and FReportDate<'" + EConvert.ToShortDateString(EndTime) + "'";
            string fMType = dt.Rows[i]["fnumber"].ToString();
            if (!string.IsNullOrEmpty(fMType))
                strSelect += " and FManageTypeId in (" + fMType + ") ";

            string str = dtReport.Select(strSelect).Count().ToString();
            string url = "<a href='../AppMain/AppEndInfoList.aspx?b=1&"
                + "FManageTypeId=" + dt.Rows[i]["fnumber"];

            url += "&FStartTime=" + EConvert.ToShortDateString(StartTime)
                + "&FEndTime=" + EConvert.ToShortDateString(EndTime);


            url += "'>" + str + "</a>";

            drMonth[dt.Rows[i]["fname"].ToString()] = url;

        }


        string str1 = dtReport.Select("     FReportDate>'"
                    + EConvert.ToShortDateString(StartTime)
                    + "' and FReportDate<'" + EConvert.ToShortDateString(EndTime) + "'").Count().ToString();



        drMonth["合计"] = "<a href='../AppMain/AppEndInfoList.aspx?b=1&"
           + "FStartTime=" + EConvert.ToShortDateString(StartTime)
            + "&FEndTime=" + EConvert.ToShortDateString(EndTime) +
            "'>" + str1 + "</a>"; ;
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void rbQuarter_CheckedChanged(object sender, EventArgs e)
    {

        showInfo();
        if (rbQuarter.Checked)
        {
            span1.Visible = false;
        }
        else
        {
            span1.Visible = true;
        }
    }
}
