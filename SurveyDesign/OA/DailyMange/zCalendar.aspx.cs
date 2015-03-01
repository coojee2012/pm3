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
using System.Drawing;

public partial class OA_DailyMange_zCalendar : Page
{
    DataTable dtDate = null;
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            ShowInfo(EConvert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + "-01"));
        }
    }

    //绑定默认
    private void conBind()
    {
        DateTime dTime = DateTime.Now;
        for (int i = dTime.Year - 5; i <= dTime.Year + 5; i++)
        {
            t_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        for (int i = 1; i <= 12; i++)
        {
            t_Month.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    //显示
    private void ShowInfo(DateTime date)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from  cf_oa_Calendar ");
        sb.Append("where FIsDeleted = 0 and  FUserId = '" + Session["FEmpID"] + "'  ");
        sb.Append("order by FDate desc");
        dtDate = rc.GetTable(sb.ToString());

        //上下月
        DateTime dTime = date;
        if (!string.IsNullOrEmpty(Request.QueryString["date"]))
        {
            dTime = EConvert.ToDateTime(Request.QueryString["date"]);
            if (dTime == DateTime.MinValue)
                dTime = DateTime.Now;
        }
        if (date.ToString("yyyyMM") == EConvert.ToDateTime(t_Year.SelectedValue + "-" + t_Month.SelectedValue + "-01").ToString("yyyyMM"))
            dTime = date;
        t_Year.SelectedValue = dTime.Year.ToString();
        t_Month.SelectedValue = dTime.Month.ToString();

        btn_LastMonth.HRef = "zCalendar.aspx?date=" + dTime.AddMonths(-1).ToString("yyyy-MM-dd");
        btn_NextMonth.HRef = "zCalendar.aspx?date=" + dTime.AddMonths(+1).ToString("yyyy-MM-dd");

        string str = dTime.ToString("yyyy年MM月");
        //本月
        if (dTime.ToString("yyyy-MM") == DateTime.Now.ToString("yyyy-MM"))
        {
            btn_ThisMonth.Visible = false;
            str += "<tt>[本月]</tt>";
        }
        else
            btn_ThisMonth.Visible = true;

        //月名称
        la_MonthName.Text = str;

        //查看日历模式
        btn_Disp.HRef = "HolidaysList.aspx?date=" + dTime.ToString("yyyy-MM-dd");

        //绑定
        C_Date.TodaysDate = EConvert.ToDateTime(t_Year.SelectedValue + "-" + t_Month.SelectedValue + "-" + DateTime.Now.Day);
    }

    //绑定每天
    protected void C_Date_DayRender(object sender, DayRenderEventArgs e)
    {

        DateTime dTime = e.Day.Date;
        string tip = dTime.ToString("yyyy年MM月dd日") + " " + OA.getWeekName(dTime) + " ";
        string day = dTime.Day.ToString();

        string str = "";
        if (dTime.DayOfWeek == DayOfWeek.Saturday || dTime.DayOfWeek == DayOfWeek.Sunday) //周六日
            day = "<tt>" + day + "</tt>";
        else
            day = "<b>" + day + "</b>";

        //今天
        if (dTime.ToShortDateString() == DateTime.Now.ToShortDateString())
        {
            e.Cell.CssClass = e.Cell.CssClass + " day_b3";
            str += "<div><span>今天</span>" + day + "</div>";
        }
        else
        {
            str += "<div>" + day + "</div>";
        }

        DataRow[] dt = dtDate.Select("FDate='" + dTime.ToString("yyyy-MM-dd") + "'");
        if (dt.Length > 0) //有内容日期
        {
            DataRow dr = dt[0];
            //点击打开
            e.Cell.Attributes.Add("onclick", "showAddWindow('zAdd.aspx?Date=" + dr["FDate"] + "',460,290);");
            string FContent = dr["FContent"].ToString();
            if (!string.IsNullOrEmpty(FContent))
            {
                str += "<s>" + FContent + "</s>";


                e.Cell.CssClass += " day_content";
            }

        }
        else //无内容日期 
        {
            //点击打开
            e.Cell.Attributes.Add("onclick", "showAddWindow('zAdd.aspx?Date=" + dTime.ToShortDateString() + "',460,290);");

        }

        str += "<i>" + OA.GetLunarCalendar(dTime, "day") + "</i>";
        e.Cell.Text = str;

        if (e.Day.IsOtherMonth)
        {
            e.Cell.CssClass = e.Cell.CssClass.Replace("day_day", "day_other");
            tip = "点击进入：" + dTime.ToString("yyyy年MM月");
            e.Cell.Attributes.Add("onclick", "javascript:window.location='zCalendar.aspx?Date=" + dTime.ToShortDateString() + "';");
        }

        e.Cell.ToolTip = tip;

    }

    //月份改变时
    protected void C_Date_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        ShowInfo(e.NewDate);
    }

    //查询
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo(EConvert.ToDateTime(t_Year.SelectedValue + "-" + t_Month.SelectedValue + "-01"));
    }

}
