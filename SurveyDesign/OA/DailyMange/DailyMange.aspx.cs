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

public partial class OA_DailyMange_DailyMange : System.Web.UI.Page
{
    OA oa = new OA();

    // private  DateTime date = new DateTime();

    private DateTime date
    {
        get
        {
            if (ViewState["date"] == null)
            {
                return DateTime.Now;
            }
            else
            {
                return (DateTime)ViewState["date"];
            }
        }
        set
        {
            ViewState["date"] = value;
        }
    }
    string strMonth = "";
    string strDepart = "";
    string strDayNumber = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 接收的参数

        // strdate = this.Request.QueryString["date"].ToString();
        //strMonth = this.Request.QueryString["month"].ToString();
        //strDepart =  this.Request.QueryString["Depart"].ToString();
        //strDayNumber =  this.Request.QueryString["DayNumber"].ToString();
        #endregion


        if (!Page.IsPostBack)
        {
            // this.l_BB.Text = strYear + this.l_BB.Text;

            //接收过来的日期参数
            if (Request.QueryString["date"] != null)
            {
                date = DateTime.Parse(Request.QueryString["date"]);
                //txtYear.Text = date.Year.ToString();
                //dropMonth.SelectedIndex = date.Month - 1;
            }
            else
            {
                date = DateTime.Now;
                //txtYear.Text = date.Year.ToString();
                // dropMonth.SelectedIndex = date.Month - 1;
            }
            showinfo();
        }
    }
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            #region 显示事务类型
            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "每日";
            }
            else if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "每周";
            }
            else if (e.Row.Cells[2].Text == "2")
            {
                e.Row.Cells[2].Text = "每月";
            }
            else if (e.Row.Cells[2].Text == "3")
            {
                e.Row.Cells[2].Text = "只有一次";
            }

            if (e.Row.Cells[3].Text.Trim() == "0")
            {
                e.Row.Cells[3].Text = "工作事务";
            }
            else if (e.Row.Cells[3].Text.Trim() == "1")
            {
                e.Row.Cells[3].Text = "个人事务";
            }
            #endregion

            #region //显示工作状态
            if (e.Row.Cells[10].Text.Trim() == "False")
            {
                e.Row.Cells[5].Text = "<font color= blue>未读</font>";
            }
            else if (e.Row.Cells[5].Text.Trim() == "0")
            {
                e.Row.Cells[5].Text = "<font color=red>进行中</font>";

            }
            else if (e.Row.Cells[5].Text.Trim() == "1")
            {
                e.Row.Cells[5].Text = "<font color=green>已完成</font>";
            }
            #endregion

            string sScript = "showAddWindow_re('WorkEdit.aspx?fid=" + e.Row.Cells[e.Row.Cells.Count-2].Text.Trim() + "',630,450,document.getElementById('btnReload'))";
            e.Row.Cells[1].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + e.Row.Cells[1].Text + "</a>";
            e.Row.Cells[8].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">修改</a>";

            //e.Row.Cells[8].Text = "<a href='WorkEdit.aspx?fid=" + e.Row.Cells[0].Text.Trim() + "' class='link5'>修改</a>";
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleNew.aspx");
    }

    private void showinfo()
    {
        string strSql = "select *,Convert(varchar(30),FbeginDate,108) as fFbeginDate ,Convert(varchar(30),FOverDate,108)  as fFOverDate from  cf_oa_Calendar where FIsDeleted = 0 and  FUserId = '" + this.Session["FEmpID"] + "'"
                  + " and convert(char(10),FbeginDate,121) = '" + date.ToString("yyyy-MM-dd") + "'";
        //+" and convert(char(10),FbeginDate,121) = '"+ date.ToString("yyyy-MM-dd") +"')";
        //convert(char(10),FbeginDate,121) 
        //DataTable dtDataReader = oa.GetTable(strSql);
        //if (dtDataReader == null || dtDataReader.Rows.Count == 0)
        //{ return; }         
        //显示该天所有信息	

        this.Pager1.controltopage = "dgList";
        this.Pager1.className = "dbOA";
        this.Pager1.sql = strSql;
        this.Pager1.pagecount = 15;
        this.Pager1.controltype = "GridView";
        this.Pager1.dataBind();
        this.lblDate.Text = "日期:" + date.ToString("yyyy-MM-dd");
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        showinfo();
    }

    protected void dgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strFID =dgList.Rows[e.RowIndex].Cells[dgList.Rows[e.RowIndex].Cells.Count - 2].Text.ToString();

        try
        {
            string strSql = "update  cf_oa_Calendar set FIsDeleted = 1 where Fid = '" + strFID + "'";
            oa.PExcute(strSql);

            Response.Write("<script language='javascript'>alert('删除成功!');</script>");
            showinfo();

        }
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('删除失败，请与管理员联系!');</script>");
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkNew.aspx?date=" + date.ToShortDateString());
    }
}
