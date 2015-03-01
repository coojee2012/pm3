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
   // private DateTime date = new DateTime();

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
            //接到的传递的参数

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
                this.ddlBeginH.SelectedIndex = 8;
                this.ddlEndH.SelectedIndex = 18;

                for (int i = 0; i < 60; i++)
                {
                    timeI = i.ToString().PadLeft(2, '0');
                    this.ddlBeginM.Items.Add(new ListItem(timeI, timeI));
                    this.ddlEndM.Items.Add(new ListItem(timeI, timeI));
                }
                this.ddlBeginM.SelectedIndex = 30;
                this.ddlEndM.SelectedIndex = 0;
        #endregion
                //开始不是代下属上报
                //this.assign.Visible = false;
                this.lblDate.Text = "当前日期:" + date.ToString("yyyy-MM-dd");
    }

    //保存补入信息
    protected void btnConfirmOff_ServerClick(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        #region 保存补入信息的验证
        //    //如果在选定的时间上已经做过补入信息
        //    string strSql = " select * from CF_OA_WorkDateList  where convert(char(10),FDate,121)  = '" + this.t_FBirthday.Text.ToString()
        //                  + "' and FUserID = '" + this.Session["FEmpID"] + "' and FRightOffdutyTime is not null "　
        //                  + " and FIsDeleted = 0  ";
        //    DataTable dt = oa.GetTable(strSql);
        //    if (dt != null && dt.Rows.Count > 0)
        //    { 
        //        Response.Write("<script language='javascript'>alert('您选定的这个日期已经有考勤记录!');</script>");
        //        return;
        //    }

        //    //只能选定以前的日期
        //    string sdate = this.t_FBirthday.Text.ToString();
        //    if (DateTime.Now.ToString().Substring(0, 10).CompareTo(sdate) <= 0 )
        //    {
        //        Response.Write("<script language='javascript'>alert('补录操作只能针对以前的日期来操作!');</script>");
        //        return;
        //    }
        #endregion
        if (this.txtTitle.Text.Length == 0)
        {
            tool.showMessage("请输入事务标题名称!");
            this.txtTitle.Focus();
            return;
        }

        #region 生成保存上，下班时间的变量
        int onhh = Convert.ToInt32(this.ddlBeginH.SelectedValue);
        int offhh = Convert.ToInt32(this.ddlEndH.SelectedValue);
        if (offhh < onhh)
        {
            tool.showMessage("您输入的结束时间小于开始时间!");
            return;
        }
        int count = offhh - onhh;//计算工作时间（小时）
        string s_ontime = date.ToString("yyyy-MM-dd") + " " + this.ddlBeginH.Text + "." + this.ddlBeginM.Text + ".00";//录入上班时间
        DateTime d_ontime = DateTime.ParseExact(s_ontime, "yyyy-MM-dd HH.mm.ss", null);
        string s_offtime = date.ToString("yyyy-MM-dd") + " " + this.ddlEndH.Text + "." + this.ddlEndM.Text + ".00";//录入下班时间
        DateTime d_offtime = DateTime.ParseExact(s_offtime, "yyyy-MM-dd HH.mm.ss", null);


        #endregion

        #region 保存
        /////////////////////
        //如果不是为下属代报，则保存的都是自己的ＩＤ
        if (strUserID == "")
        { 
        strUserID = this.Session["FEmpID"].ToString().Trim();
        }
        string strSql = "";
        if (this.ViewState["FID"] == null)
        {
            this.ViewState["FID"] = Guid.NewGuid();
            strSql = "Insert into cf_oa_Calendar([FID],[FCalendarType],[FUserId],[FAssignID],[FTitle],[FContent],[FTermly],[FIsPersonal],[FInTime],[FbeginDate],[FOverDate],[FIteranceTime],[FAwokeTime],[FIsOver] ,[FTime],[FCreateTime],[FIsDeleted]) Values "
                   + "('" + this.ViewState["FID"].ToString() 
                   + "','" + ddlType.SelectedValue.ToString() 
                   + "','" + strUserID + "','" 
                   + this.Session["FEmpID"] + "','" 
                   + this.txtTitle.Text.ToString().Trim() + "','" 
                   + this.txtContent.Text.ToString() + "'," 
                   + ddlTermly.SelectedValue.ToString().Trim() 
                   + ",'1','" 
                   + DateTime.Now + "','"
                   + d_ontime + "','" 
                   + d_offtime + "','" 
                   + DateTime.Now + "','" 
                   + DateTime.Now + "','0','" 
                   + DateTime.Now + "','" 
                   + DateTime.Now + "',0 )";
        }
        else
            strSql = " update cf_oa_Calendar set [FCalendarType] = '" + ddlType.SelectedValue.ToString()
                 + "',[FTitle] = '" + this.txtTitle.Text.ToString().Trim()
                 + "',[FContent] = '" + this.txtContent.Text.ToString().Trim()
                 + "',[FTermly] = " + ddlTermly.SelectedValue.ToString().Trim()
                 + ",[FInTime] = '" + DateTime.Now
                 + "',[FbeginDate] ='" + d_ontime
                 + "',[FOverDate] =  '" + d_offtime
                 + "',[FTime] = '" + DateTime.Now
                 + "', FIsRead = 1,FIsOver = 0 "
                 + " where FIsDeleted = 0 and  FId = '" + this.ViewState["FID"].ToString() + "'";

        try
        {
            oa.PExcute(strSql);

            #region   发送短消息
            //        //创建实例
            //        SendSMsg ssm = new SendSMsg();
            //        //给出短信内容
            //        ssm.MsgContent = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ": 新的日程安排";
            //        //这里给出短信的类型，根据业务部同请区分不同类型
            //        ssm.MsgType = "工作短消息";
            //        //是谁发送的，这里一般区当前登录的办公人员的FID

            //        string empid = "";
            //        empid = oa.GetSignValue("select fbaseid from cf_sys_user where fid='" + this.Session["FEmpID"].ToString() + "'");
            //        ssm.userId = empid;
            //        //这里是IList的数据，取的是发送对象的人员的FID集合
            //        //这个是必须的
            //        //注意这个
            //        IList list = new ArrayList();

            //        empid = oa.GetSignValue("select fbaseid from cf_sys_user where fid='" + strUserID + "'");
            //        list.Add(empid);
            //        ssm.presonFID = list;

            //        //这个是发送新的消息给发送对象
            //        bool bo = ssm.SendNewMsg();

            //        IList orgFIDs = new ArrayList();
            //        orgFIDs.Add("");

            //        SaveDevelopment.SaveDevel("发了一条新的工作汇报", "../images/gzhb.jpg", empid, this.txtContent.Text.ToString(), list, orgFIDs);
 

            //        if (bo)
            //        {
            //           // RegisterClientScriptBlock("js", "<Script>alert('发送成功！');</Script>");
                           
            //        }
            #endregion
            tool.showMessage("添加成功!");
        }
        catch (Exception ex)
        {
           tool.showMessage("保存失败,请检查网络异常,并重试!");
            return;
        }

        #endregion

    }

    protected void exit_ServerClick1(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleNew.aspx");
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

    //            //如果是代下属上报，自己的身份是代报人，人员ID要保存指定人的　

    //            string userId = oa.GetSignValue("select fid from cf_sys_user where FBaseId='" + this.t_FBaseId.Value.ToString().Trim() + "'");
    //            strUserID = userId;
    //        }
    //    }

    //}
    //是否代下属汇报
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
}
