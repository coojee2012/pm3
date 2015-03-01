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

public partial class OA_DailyMange_ViewWork : System.Web.UI.Page
{
    OA oa = new OA();

    private DateTime date = new DateTime();
    public string strSql
    {
        get
        {
            if (ViewState["strSql"] == null)
            {
                return "";
            }
            else
            {
                return (string)ViewState["strSql"];
            }
        }
        set
        {
            ViewState["strSql"] = value;
        }
    }

    public string strUserID
    {
        get
        {
            if (ViewState["strUserID"] == null)
            {
                return "";
            }
            else
            {
                return (string)ViewState["strUserID"];
            }
        }
        set
        {
            ViewState["strUserID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //接收过来的日期参数
            if (Request.QueryString["date"] != null)
            {
                date = DateTime.Parse(Request.QueryString["date"]);
            }
            else
            {
                date = DateTime.Now;
            }
            strSql = "select *,convert(char(10),FbeginDate,121) as oldData,Convert(varchar(30),FbeginDate,108) as fFbeginDate ,Convert(varchar(30),FOverDate,108)  as fFOverDate from  cf_oa_Calendar where FIsDeleted = 0 and  FUserId = '" + this.Session["FEmpID"] + "'  order by FCreateTime desc";

            showinfo();
        }
    }
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string fid = EConvert.ToString(DataBinder.Eval(e.Row.DataItem,"FID"));
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
            if (e.Row.Cells[9].Text.Trim() == "False")
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


            if (e.Row.Cells[1].Text.Trim().Length > 10)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 10) + "...";
            }
            if (e.Row.Cells[4].Text.Trim().Length > 30)
            {
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 30) + "..";
            }

            //string  sScript = "showApproveWindow('WorkEdit.aspx?fid=" + this.Session["FEmpID"] + "',700,350)";

            string sScript = "showAddWindow_re('WorkEdit.aspx?fid=" + fid + "',800,500,document.getElementById('btnReload'))";
            e.Row.Cells[1].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">" + e.Row.Cells[1].Text + "</a>";
            e.Row.Cells[8].Text = "<a href='#' class='link5' onclick=\"" + sScript + "\">查看</a>";

        }
    }

    private void showinfo()
    {



        this.Pager1.controltopage = "dgList";
        this.Pager1.className = "dbOA";
        this.Pager1.sql = strSql;
        this.Pager1.pagecount = 15;
        this.Pager1.controltype = "GridView";
        this.Pager1.dataBind();
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        showinfo();
    }

    

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        strSql = "select *,convert(char(10),FbeginDate,121) as oldData,Convert(varchar(30),FbeginDate,108) as fFbeginDate ,Convert(varchar(30),FOverDate,108)  as fFOverDate"
              + " from  cf_oa_Calendar where FIsDeleted = 0 ";
        if (this.txt_BeginTime.Text != "")
        {
            strSql += " and FbeginDate >= '" + this.txt_BeginTime.Text.ToString() + "'";
        }
        if (this.txtEndtime.Text != "")
        {
            strSql += " and FOverDate <= '" + this.txtEndtime.Text.ToString() + " 23:59:00'";
        }
        if (this.ddlTermly.SelectedIndex != 0)
        {
            strSql += " and FTermly = " + this.ddlTermly.SelectedValue + "";
        }
        if (this.ddlType.SelectedIndex != 0)
        {
            strSql += " and FCalendarType = '" + this.ddlType.SelectedValue + "'";
        }
        if (this.txtTitle.Text != "")
        {
            strSql += " and FTitle like '%" + this.txtTitle.Text.ToString().Trim() + "%'";
        }

        //如果不是为下属代报，则保存的都是自己的ＩＤ
        if (strUserID == "")
        {
            strUserID = this.Session["FEmpID"].ToString();
        }
        strSql += " and FUserId ='" + strUserID + "' order by FCreateTime desc ";


        showinfo();
    }
}
