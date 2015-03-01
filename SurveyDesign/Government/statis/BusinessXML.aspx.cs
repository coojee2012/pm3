using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using System.Drawing;
using Approve.EntityBase;

public partial class Government_statis_BusinessXML : System.Web.UI.Page
{
    RCenter rc = new RCenter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showXML();
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

    private void showXML()
    {
        string FSystemId = Request.QueryString["FSystemId"];// "150";//系统类型
        pageTool tool = new pageTool(this.Page);
        //月份
        DateTime dTime = DateTime.Now;
        int bY = string.IsNullOrEmpty(Request.QueryString["txt_Year1"]) ? dTime.Year : EConvert.ToInt(Request.QueryString["txt_Year1"]);
        int bM = string.IsNullOrEmpty(Request.QueryString["txt_Month1"]) ? 1 : EConvert.ToInt(Request.QueryString["txt_Month1"]);
        int eY = string.IsNullOrEmpty(Request.QueryString["txt_Year2"]) ? dTime.Year : EConvert.ToInt(Request.QueryString["txt_Year2"]);
        int eM = string.IsNullOrEmpty(Request.QueryString["txt_Month2"]) ? 12 : EConvert.ToInt(Request.QueryString["txt_Month2"]);


        //投诉件记录
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

        DateTime FBeginTime = EConvert.ToDateTime(bY + "-1-1");
        DateTime FEndTime = EConvert.ToDateTime(eY + "-12-31");

        string cat = "", dat = "", str = "";

        sb = new StringBuilder();
        sb.AppendLine(" select fname,fnumber from CF_Sys_ManageType ");
        sb.AppendLine(" where  fmtypeid<>'0'");
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
        dt = rc.GetTable(sb.ToString());
        for (int i = 0; dt != null && i < dt.Rows.Count; i++)
        {
            getValue(EConvert.ToString(dt.Rows[i]["FName"]), dtReport, EConvert.ToString(dt.Rows[i]["fnumber"]), i, FBeginTime, FEndTime, ref cat, ref dat);
        }



        str += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        str += "<graph caption='业务统计'  baseFontSize='12' >";
        str += "<categories>" + cat + "</categories>" + dat;
        str += "</graph>";



        Response.ContentType = "text/xml";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.Write(str);
    }


    private void getValue(string name, DataTable dt, string mType, int index, DateTime FBeginTime, DateTime FEndTime, ref string cat, ref string dat)
    {
        dat += "<dataset seriesName='" + name + "' color='" + getStrColor() + "'>";

        string categoryname = "";
        if (Request.QueryString["Quarter"] == "True")
        {
            for (int i = 0; i < 12; i++)
            {
                if (index == 0)
                {
                    cat += "<category name='" + (i + 1) + "月' />";
                }
                string str = dt.Select("FManageTypeId='" + mType + "'   and FReportDate>'" +
                    EConvert.ToShortDateString(new DateTime(FBeginTime.Year, i + 1, 1)) + "' and FReportDate<'"
                        + EConvert.ToShortDateString(new DateTime(FBeginTime.Year, i + 1, 1).AddMonths(1)) + "'").Count().ToString();
                if (EConvert.ToInt(str) > 0)
                {
                    dat += "<set value='" + str + "' />";
                }
                else
                {
                    dat += "<set value='0' />";
                }
            }

        }
        else
        {
            for (int i = EConvert.ToInt(FBeginTime.Year); i <= EConvert.ToInt(FEndTime.Year); i++)
            {
                if (index == 0)
                {
                    cat += "<category name='" + i + "年' />";
                }
                string str = dt.Select("FManageTypeId='" + mType + "'   and FReportDate>'" +
                 EConvert.ToShortDateString(new DateTime(i, 1, 1)) + "' and FReportDate<'"
                     + EConvert.ToShortDateString(new DateTime(i + 1, 1, 1)) + "'").Count().ToString();
                if (EConvert.ToInt(str) > 0)
                {
                    dat += "<set value='" + str + "' />";
                }
                else
                {
                    dat += "<set value='0' />";
                }

            }
            //categoryname = timeAdd.ToString("yyyy年");
        }



        dat += "</dataset>";
    }

    private static Random rand = new Random();
    public static string getStrColor()
    {
        Color color = Color.FromArgb(rand.Next());
        string strColor = Convert.ToString(color.ToArgb(), 16).PadLeft(8, '0').Substring(2, 6);
        return strColor;
    }
}
