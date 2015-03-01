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
			//this.PageBegin("�ҵ��ճ�",false);
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

        //����������ʾ
		public void calSchedule_DayRender(object sender, DayRenderEventArgs e) 
		{
			//�Զ�����ʾ����
			CalendarDay d = ((DayRenderEventArgs)e).Day;
			//��ȡ��ʾ�����ڿؼ��еĵ�Ԫ��
			TableCell c = ((DayRenderEventArgs)e).Cell;
            c.Height = 60;
			//ũ��ת������
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
                    //aHyperLink.Text = "���������ճ�";
                    //aHyperLink.ToolTip = "���������ճ�";
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
				string Subject;//�ճ̱���
                string SubjectTitle = "";//�ճ̱�����������ʾ
				SqlCommand objcommand = new SqlCommand();
				SqlDataReader objdatareader;  
				DateTime Date = new DateTime();
				Date = d.Date;
				//��ȡ������Ϣ ��ȡ���ݿ�
                //ScheduleDA scheduleDadaAccess = new ScheduleDA();
                //objcommand = scheduleDadaAccess.SchRead(Convert.ToInt16(this.Empid),Date);
                //objcommand.Connection.Open();
                //objdatareader=objcommand.ExecuteReader();
                string strSql = "select * from  cf_oa_Calendar where FIsDeleted = 0 and  FUserId = '" + this.Session["FEmpID"] + "'";
                DataTable dtDataReader = oa.GetTable(strSql);
                if (dtDataReader == null || dtDataReader.Rows.Count == 0)
                { return; }



               
				//��ʾ����������Ϣ
				try
				{		
					//while(objdatareader.Read())��//ѭ����ʾ���ݼ�
                   for(int i = 0; i < dtDataReader.Rows.Count; i++)
					{

                       //��������ճ̼�¼������
                        DateTime date = (DateTime)dtDataReader.Rows[i]["FbeginDate"];////////////////
                        DateTime Edate = (DateTime)dtDataReader.Rows[i]["FOverDate"];////////////////
                        if (date.Year == e.Day.Date.Year && date.Month == e.Day.Date.Month && date.Day == e.Day.Date.Day)
                        {


                            //�õ��ճ�����ID
                            //int ID = Int32.Parse(objdatareader.GetInt32(1).ToString());

                            //�õ��ճ̰�������
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
                            //�õ��ճ̱���
                            Subject = dtDataReader.Rows[i]["FTitle"].ToString();
                            if (Subject.Length > 9)
                            {
                                Subject = Subject.Substring(0, 9);
                                Subject += "..";
                            }
                            //HtmlAnchor a = new HtmlAnchor();
                            //a.HRef = "schaddup.aspx?reurl=schedule.aspx&id="+ID+"&day="+d.Date.ToShortDateString();

                            ////��ʾ����ÿһ����ճ̱���


                            string sScript = "showAddWindow_re('WorkEdit.aspx?fid=" + dtDataReader.Rows[i]["Fid"].ToString() + "',600,500,document.getElementById('btnReload'))";
                            sScript = "<a href='#' class='link5' onclick=\"" + sScript + "\"><font color= '#fc4912'>" + FDate_hour + ":" + FDate_Minute + "-" + EDate_hour + ":" + EDate_Minute + "<br> ����:" + Subject + "</font></a>";


                            c.Controls.Add(new LiteralControl("<br>"));
                           // c.Controls.Add(new LiteralControl(FDate_hour + ":" + FDate_Minute + " " + Subject));
                            c.Controls.Add(new LiteralControl(sScript));

                            SubjectTitle += FDate_hour + "." + FDate_Minute + ": " + dtDataReader.Rows[i]["FTitle"].ToString()+"��";
                        }
					}
                            //��ҵ��ʱ��������ʽ 
                            //e.Cell.BackColor = System.Drawing.Color.Red;
                    //e.Cell.ToolTip = "�������飺" + SubjectTitle; 
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
			//�ж�����һ���»�����һ����
			if (e.NewDate > e.PreviousDate) 
			{
				if((year == 2050) && (month == 12))
				{
					//��ʾ������Ϣ
					lblErr.Visible = true;
					lblErr.Text = "��ѡ��1900--2050�꣡";
					txtYear.Text = "2050";
					dropMonth.SelectedIndex = 11;
					calSchedule.VisibleDate = e.PreviousDate;
				}
				else
				{
					if(e.PreviousDate.Month == 12)
					{
						//�������¿ؼ�
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
					lblErr.Text = "��ѡ��1900--2050�꣡";
					txtYear.Text = "1900";
					dropMonth.SelectedIndex = 0;
					calSchedule.VisibleDate = e.PreviousDate; 
				}
				else
				{
					//�����ǰ�·���1�����·ݱ��12�£���ݼ�1
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
			// CODEGEN���õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{   

		}
		#endregion

        //  ���������ҳ��
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
                pt.showMessage("����д��ȷ����ݲ�ѯ������־��");
            }
        }

    //�½�����
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
    ////�Ƿ�������㱨
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

