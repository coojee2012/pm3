using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.Security;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;

public partial class OA_DailyMange_ScheduleNew : System.Web.UI.Page
	{
		protected string str_NoticeMsg ;
		private static DateTime date = new DateTime();
       OA oa = new OA();
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//this.PageBegin("我的日程",false);
			if(!IsPostBack)
			{
                //DataTable tipTB = (new ScheduleDA()).cpScheduleTip(Convert.ToInt16(this.Empid)).Tables[0];
                //if(tipTB.Rows.Count > 0 && Session["isScheduleNoticed"] == null)
                //{
                //    str_NoticeMsg = "onload=\"javascript:window.open('ScheduleNotice.aspx','','top=1000,left=1000')\"";
                //    Session["isScheduleNoticed"] = "1";
                //}
				if(Request.QueryString["date"] != null)
				{
					date = DateTime.Parse(Request.QueryString["date"]);
					txtYear.Text = date.Year.ToString();
					dropMonth.SelectedIndex = date.Month - 1;
				}
				else
				{
					date = DateTime.Now;
					txtYear.Text = date.Year.ToString();
					dropMonth.SelectedIndex = date.Month - 1;
				}
			}
		}

        //在日期中显示
		public void calSchedule_DayRender(object sender, DayRenderEventArgs e) 
		{
			//自定义显示内容
			CalendarDay d = ((DayRenderEventArgs)e).Day;
			//获取表示呈现在控件中的单元格
			TableCell c = ((DayRenderEventArgs)e).Cell;
            c.Height = 60;
			//农历转换对象
			//CNDate dt = new CNDate(d.Date); 
			if (d.IsOtherMonth) 
			{
				c.Controls.Clear();
			}
			else 
			{
				try
				{
                    
					HyperLink aHyperLink = new HyperLink();
					//aHyperLink.ImageUrl = "../img/add_small.gif";
                    //aHyperLink.Text = "新增个人日程";
                    //aHyperLink.ToolTip = "新增个人日程";
                    //aHyperLink.NavigateUrl = "schaddup.aspx?reurl=schedule.aspx&day="+d.Date.ToShortDateString();
                    //c.Controls.Add(new LiteralControl("&nbsp;"+"&nbsp;"+"&nbsp;"+"&nbsp;")); 
                    //c.Controls.Add(aHyperLink); 
					//c.Controls.Add(new LiteralControl("<br>"+dt.FormatLunarYear()));
				}
				catch (Exception exc) 
				{
					Response.Write(exc.ToString());
				}
				DateTime FDate = new DateTime();
				string Subject;//日程标题
                string SubjectTitle = "";//日程标题用气球显示
				SqlCommand objcommand = new SqlCommand();
				SqlDataReader objdatareader;  
				DateTime Date = new DateTime();
				Date = d.Date;
				//读取该天信息 读取数据库
                //ScheduleDA scheduleDadaAccess = new ScheduleDA();
                //objcommand = scheduleDadaAccess.SchRead(Convert.ToInt16(this.Empid),Date);
                //objcommand.Connection.Open();
                //objdatareader=objcommand.ExecuteReader();
                string strSql = "select * from  cf_oa_Calendar where FIsDeleted = 0 and  FUserId = '" + this.Session["FEmpID"] + "'";
                DataTable dtDataReader = oa.GetTable(strSql);
                if (dtDataReader == null || dtDataReader.Rows.Count == 0)
                { return; }



               
				//显示该天所有信息
				try
				{		
					//while(objdatareader.Read())　//循环显示数据集
                   for(int i = 0; i < dtDataReader.Rows.Count; i++)
					{

                       //如果当天日程记录有内容
                        DateTime date = (DateTime)dtDataReader.Rows[i]["FbeginDate"];////////////////
                        DateTime Edate = (DateTime)dtDataReader.Rows[i]["FOverDate"];////////////////
                        if (date.Year == e.Day.Date.Year && date.Month == e.Day.Date.Month && date.Day == e.Day.Date.Day)
                        {


                            //得到日程主键ID
                            //int ID = Int32.Parse(objdatareader.GetInt32(1).ToString());

                            //得到日程安排日期
                            FDate = DateTime.Parse(dtDataReader.Rows[i]["FbeginDate"].ToString());
                            Edate = DateTime.Parse(dtDataReader.Rows[i]["FOverDate"].ToString());
                            string FDate_hour = FDate.Hour.ToString();
                            string FDate_Minute = FDate.Minute.ToString();
                            string EDate_hour = Edate.Hour.ToString();
                            string EDate_Minute = Edate.Minute.ToString();
                            if (FDate_Minute == "0")
                            {
                                FDate_Minute = "00";
                            }
                            //得到日程标题
                            Subject = dtDataReader.Rows[i]["FTitle"].ToString();
                            if (Subject.Length > 9)
                            {
                                Subject = Subject.Substring(0, 9);
                                Subject += "..";
                            }
                            //HtmlAnchor a = new HtmlAnchor();
                            //a.HRef = "schaddup.aspx?reurl=schedule.aspx&id="+ID+"&day="+d.Date.ToShortDateString();

                            ////显示日历每一天的日程标题


                            string sScript = "showAddWindow_re('WorkEdit.aspx?fid=" + dtDataReader.Rows[i]["Fid"].ToString() + "',600,500,document.getElementById('btnReload'))";
                            sScript = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color= '#fc4912'>" + FDate_hour + ":" + FDate_Minute + "-" + EDate_hour + ":" + EDate_Minute + "<br> 工作:" + Subject + "</font></a>";


                            c.Controls.Add(new LiteralControl("<br>"));
                           // c.Controls.Add(new LiteralControl(FDate_hour + ":" + FDate_Minute + " " + Subject));
                            c.Controls.Add(new LiteralControl(sScript));

                            SubjectTitle += FDate_hour + "." + FDate_Minute + ": " + dtDataReader.Rows[i]["FTitle"].ToString()+"；";
                        }
					}
                            //有业务时，设置样式 
                            //e.Cell.BackColor = System.Drawing.Color.Red;
                    //e.Cell.ToolTip = "待办事情：" + SubjectTitle; 
				}
				catch(Exception exc) 
				{
					Response.Write(exc.ToString());
				}
			}
            if (!(e.Day.Date.Year==DateTime.Now.Year  && DateTime.Now.Month == e.Day.Date.Month && DateTime.Now.Day == e.Day.Date.Day))
            {
                e.Cell.Attributes["onmouseover"] = "javascript:this.style.backgroundColor='#EBF1FF';cursor='hand';";
                e.Cell.Attributes["onmouseout"] = "javascript:this.style.backgroundColor='#ffffff';";
            }
		}

    protected void cmdQuery_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int year,month;
			year = Int32.Parse(txtYear.Text);
			month = dropMonth.SelectedIndex + 1;
			DateTime querydate = new DateTime(year,month,1);
			calSchedule.VisibleDate = querydate;
		}

		private void calSchedule_monthchanged(object sender, MonthChangedEventArgs  e)
		{
			int year = Int16.Parse(e.PreviousDate.Year.ToString());
			int month = Int16.Parse(e.PreviousDate.Month.ToString());
			//判断是上一个月还是下一个月
			if (e.NewDate > e.PreviousDate) 
			{
				if((year == 2050) && (month == 12))
				{
					//显示错误信息
					lblErr.Visible = true;
					lblErr.Text = "请选择1900--2050年！";
					txtYear.Text = "2050";
					dropMonth.SelectedIndex = 11;
					calSchedule.VisibleDate = e.PreviousDate;
				}
				else
				{
					if(e.PreviousDate.Month == 12)
					{
						//设置年月控件
						dropMonth.SelectedIndex = 0;
						txtYear.Text = (e.PreviousDate.Year + 1).ToString();
					}
					else
					{
						dropMonth.SelectedIndex = e.PreviousDate.Month - 1 + 1;
						txtYear.Text = e.PreviousDate.Year.ToString();
					}
				}
			}
			else
			{
				if((year == 1900) && (month == 1))
				{
					lblErr.Visible = true;
					lblErr.Text = "请选择1900--2050年！";
					txtYear.Text = "1900";
					dropMonth.SelectedIndex = 0;
					calSchedule.VisibleDate = e.PreviousDate; 
				}
				else
				{
					//如果当前月份是1月则月份变成12月，年份减1
					if(e.PreviousDate.Month == 1)
					{
						dropMonth.SelectedIndex = 11;
						txtYear.Text = (e.PreviousDate.Year - 1).ToString();
					}
					else
					{
						dropMonth.SelectedIndex = e.PreviousDate.Month - 1 - 1;
						txtYear.Text = e.PreviousDate.Year.ToString();
					}
				}	
			}
		}

		private void cmdWeek_Click(object sender, System.EventArgs e)
		{
			date = DateTime.Now;
			Response.Redirect("SchedByWeek.aspx?date="+date.ToShortDateString());
		}

		private void cmdDay_Click(object sender, System.EventArgs e)
		{
			date = DateTime.Now;
			Response.Redirect("SchedByDay.aspx?date="+date.ToShortDateString());
		}

		public DateTime GetDate()
		{
			return date;
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN：该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{   

		}
		#endregion

        //  点击进入新页面
    protected void calSchedule_SelectionChanged(object sender, System.EventArgs e)
		{
            
			date = calSchedule.SelectedDate;
			Response.Redirect("DailyMange.aspx?date="+date.ToShortDateString());
		}

        protected void cmdWeek_Click1(object sender, EventArgs e)
        {

        }
        protected void cmdQuery_Click(object sender, EventArgs e)
        {
            pageTool pt = new pageTool(this.Page);
            try
            {
                int year, month;
                year = Int32.Parse(txtYear.Text);
                month = dropMonth.SelectedIndex + 1;
                DateTime querydate = new DateTime(year, month, 1);
                calSchedule.VisibleDate = querydate;
            }
            catch
            {
                pt.showMessage("请填写正确的年份查询工作日志！");
            }
        }

    //新建事务
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("WorkNew.aspx");
    }
    


    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    if (this.t_FBaseId.Value != "")//CF_OA_Emp.FID = CF_Sys_User.FBaseId 
    //    {
    //        DataTable dt = oa.GetTable(EntityTypeEnum.EOAEmp, "fname,fcall", "fid='" + this.t_FBaseId.Value.ToString() + "'");
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            this.t_FUserName.Text = dt.Rows[0]["fname"].ToString().Trim();
    //            // this.t_FTel.Text = dt.Rows[0]["fcall"].ToString().Trim();
    //        }
    //    }
       
    //}
    ////是否代下属汇报
    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox1.Checked == true)
    //    {
    //        this.assign.Visible = true;
    //    }
    //    else
    //    {
    //        this.assign.Visible = false;
    //    }
    
    //}


    protected void btnReload_Click(object sender, EventArgs e)
    {
        int year, month;
        year = Int32.Parse(txtYear.Text);
        month = dropMonth.SelectedIndex + 1;
        DateTime querydate = new DateTime(year, month, 1);
        calSchedule.VisibleDate = querydate;
    }
}

