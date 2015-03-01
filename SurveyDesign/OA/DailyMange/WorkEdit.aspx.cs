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

public partial class OA_DailyMange_WorkEdit : System.Web.UI.Page
{
    OA oa = new OA();
    //private DateTime date = new DateTime();

    public string strOldDate
    {
        get
        {
            if (ViewState["strOldDate"] == null)
            {
                return "";
            }
            else
            {
                return (string)ViewState["strOldDate"];
            }
        }
        set
        {
            ViewState["strOldDate"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //接到的传递的参数
            if (Request["Fid"] != null && Request["Fid"] != "")
            {
                this.ViewState["Fid"] = Request["Fid"];
            }

            //if (Request.QueryString["date"] != null)
            //{
            //    date = DateTime.Parse(Request.QueryString["date"]);
            //    //txtYear.Text = date.Year.ToString();
            //    //dropMonth.SelectedIndex = date.Month - 1;
            //}
            //else
            //{
            //    date = DateTime.Now;
            //    //txtYear.Text = date.Year.ToString();
            //    // dropMonth.SelectedIndex = date.Month - 1;
            //}
            showinfo();

        }
    }
    private void showinfo()
    {
        #region 初始化记时下拉框
        string timeI = "";
        for (int i = 0; i < 24; i++)
        {
            timeI = i.ToString().PadLeft(2, '0');
            this.ddlBeginH.Items.Add(new ListItem(timeI, timeI));
            this.ddlEndH.Items.Add(new ListItem(timeI, timeI));
        }

        for (int i = 0; i < 60; i++)
        {
            timeI = i.ToString().PadLeft(2, '0');
            this.ddlBeginM.Items.Add(new ListItem(timeI, timeI));
            this.ddlEndM.Items.Add(new ListItem(timeI, timeI));
        }

        #endregion

        #region  标识为已读
        string strSql = " update cf_oa_Calendar set  FIsRead = 1 "
        + " where FIsDeleted = 0 and  FId = '" + ViewState["Fid"] + "'";
        oa.PExcute(strSql);
        #endregion

        #region 初始化信息
        strSql = "select convert(char(10),FbeginDate,121) as oldData, datepart(hh,FbeginDate) as beginH,datepart(minute,FbeginDate) as beginM,datepart(hh,FOverDate) as overH,datepart(minute,FOverDate) as overM "
                 + ",* from  cf_oa_Calendar where FIsDeleted = 0 and  FId = '" + this.ViewState["Fid"] + "'";
        DataTable dtDataReader = oa.GetTable(strSql);
        if (dtDataReader == null || dtDataReader.Rows.Count == 0)
        { return; }

        this.ddlBeginH.SelectedIndex = int.Parse(dtDataReader.Rows[0]["beginH"].ToString());
        this.ddlEndH.SelectedIndex = int.Parse(dtDataReader.Rows[0]["overH"].ToString());
        this.ddlBeginM.SelectedIndex = int.Parse(dtDataReader.Rows[0]["beginM"].ToString());
        this.ddlEndM.SelectedIndex = int.Parse(dtDataReader.Rows[0]["overM"].ToString());
        this.ddlTermly.SelectedIndex = int.Parse(dtDataReader.Rows[0]["FTermly"].ToString());
        this.ddlType.SelectedIndex = int.Parse(dtDataReader.Rows[0]["FCalendarType"].ToString().Trim());
        this.txtTitle.Text = dtDataReader.Rows[0]["FTitle"].ToString().Trim();
        this.txtContent.Text = dtDataReader.Rows[0]["FContent"].ToString().Trim();

        if (dtDataReader.Rows[0]["FIsOver"].ToString() == "1")
        {
            this.cheDone.Checked = true;
        }

        strOldDate = dtDataReader.Rows[0]["oldData"].ToString();
        this.lblDate.Text = "当前日期:" + strOldDate;
        #endregion

    }

    //保存补入信息
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        #region 生成保存上，下班时间的变量
        int onhh = Convert.ToInt32(this.ddlBeginH.SelectedValue);
        int offhh = Convert.ToInt32(this.ddlEndH.SelectedValue);
        if (offhh < onhh)
        {
            tool.showMessage("您输入的结束时间小于开始时间");
            return;
        }

        #endregion

        #region 保存
        /////////////////////
        string strIsDone = "0";
        if (this.cheDone.Checked == true)
        {
            strIsDone = "1";
        }

        string strSql = " update cf_oa_Calendar set [FCalendarType] = '" + ddlType.SelectedValue.ToString()
            + "',[FTitle] = '" + this.txtTitle.Text.ToString().Trim()
            + "',[FContent] = '" + this.txtContent.Text.ToString().Trim()
            + "',[FTermly] = " + ddlTermly.SelectedValue.ToString().Trim()
            + ",[FInTime] = '" + DateTime.Now
            + "',[FbeginDate] ='" + strOldDate + " " + this.ddlBeginH.Text + ":" + this.ddlBeginM.Text + ":00"
            + "',[FOverDate] =  '" + strOldDate + " " + this.ddlEndH.Text + ":" + this.ddlEndM.Text + ":00"
            + "',[FTime] = '" + DateTime.Now
            + "', FIsRead = 1,FIsOver = " + strIsDone  //标识为已读,同时更改完成状态
            + " where FIsDeleted = 0 and  FId = '" + this.ViewState["Fid"].ToString() + "'";


        try
        {
            oa.PExcute(strSql);
            tool.showMessage("修改成功!");
        }
        catch (Exception ex)
        {
            tool.showMessage("保存失败,请检查网络异常,并重试!");
            return;
        }

        #endregion


    }

    protected void exit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("Schedule.aspx");
    }
}
