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

public partial class OA_zSys_HolidaysList : Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
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

        btn_LastMonth.HRef = "HolidaysList.aspx?date=" + dTime.AddMonths(-1).ToString("yyyy-MM-dd");
        btn_NextMonth.HRef = "HolidaysList.aspx?date=" + dTime.AddMonths(+1).ToString("yyyy-MM-dd");

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
        btn_Disp.HRef = "HolidaysCalendar.aspx?date=" + dTime.ToString("yyyy-MM-dd");


        //绑定
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_Sys_Holidays ");
        sb.Append("where FIsDeleted=0 and FIsTrue=1 ");
        sb.Append(" and year(FDate)='" + t_Year.SelectedValue + "' ");
        sb.Append(" and month(FDate)='" + t_Month.SelectedValue + "' ");
        sb.Append("order by FDate ");

        Holidays_List.DataSource = rc.GetTable(sb.ToString());
        Holidays_List.DataBind();
    }
    protected void Holidays_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FDate = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FDate"));
            string FType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            string FTxt = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FTxt"));
            string FONTime = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FONTime"));
            string FOFFTime = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FOFFTime"));


            //日期 
            e.Item.Cells[2].Text = "<a href=\"javascript:showAddWindow('HolidaysAdd.aspx?fid=" + FID + "',460,290);\">" + FDate + "（" + OA.getWeekName(EConvert.ToDateTime(FDate)) + "）" + "</a>";

            //类型
            e.Item.Cells[3].Text = FType == "1" ? "节假日" : FType == "2" ? "特殊日期" : "";

            //备注
            e.Item.Cells[4].Text = FTxt + (FType == "2" ? "（上班时间：" + FONTime + "--" + FOFFTime + "）" : "");

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo(EConvert.ToDateTime(t_Year.SelectedValue + "-" + t_Month.SelectedValue + "-01"));
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(Holidays_List, "CF_Sys_Holidays", "RCenter");
        ShowInfo(EConvert.ToDateTime(t_Year.SelectedValue + "-" + t_Month.SelectedValue + "-01"));
    }
}
