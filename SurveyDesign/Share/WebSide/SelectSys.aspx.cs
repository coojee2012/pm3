using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using Approve.Common;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Collections;
using ProjectData;

public partial class Share_WebSide_SelectSys : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
            showInfo();
        }
    }


    //显示
    private void showInfo()
    {

        string UserId = "";
        string Key = Request.QueryString["Key"];
        if (!string.IsNullOrEmpty(Key))
        {
            Key = Key.Replace("%20", "+");
            string[] strArray = SecurityEncryption.DesDecrypt(Key, "32165498").Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                    UserId = strArray[0];
            }

            if (!string.IsNullOrEmpty(UserId))
            {
                ViewState["UserId"] = UserId;
                ShowSysList(UserId);//显示可进入系统列表

                showOther(UserId);//可开通系统
            }
            else
            {
                Response.Clear();
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            Response.Clear();
            Response.Redirect("Login.aspx");
        }
    }

    #region 列出已有权限，登陆

    /// <summary>
    /// 显示选择系统列表
    /// </summary>
    /// <param name="UserId"></param>
    private void ShowSysList(string UserId)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID,FType,FCompany,FLinkMan,FTel,FState,FEndTime ");
        sb.Append("from cf_sys_user ");
        sb.Append("where fid=@FID ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FID", UserId));
        if (dt != null && dt.Rows.Count > 0)
        {
            #region 帐户基本信息
            lit_EntName.Text = "&nbsp;&nbsp;" + dt.Rows[0]["FCompany"].ToString();//企业名
            lit_helo.Text = "您好！您本次登陆时间：";
            //lit_Time.Text = DataLog.getLastMainLoginTime(UserId);
            lit_LinkMan.Text = dt.Rows[0]["FLinkMan"].ToString();
            lit_Tel.Text = dt.Rows[0]["FTel"].ToString();
            lit_State.Text = dt.Rows[0]["FState"].ToString() == "1" ? "<font color='green'>正常</font>" : "<font color='red'>注销</font>";
            lit_EndTime.Text = EConvert.ToDateTime(dt.Rows[0]["FEndTime"].ToString()).ToShortDateString();
            #endregion


            string company = dt.Rows[0]["FCompany"].ToString();
            string str = "";

            sb.Remove(0, sb.Length);
            sb.Append("select FID,FSystemId,FState,FEndTime ");
            sb.Append("from cf_sys_userright ");
            sb.Append("where isnull(fisdeleted,0)=0 and FUserId=@FUserId ");
            dt = sh.GetTable(sb.ToString(), new SqlParameter("@FUserId", UserId));
            if (dt != null && dt.Rows.Count > 0)
            {

                //列出该用户的系统权限
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FSystemId = dt.Rows[i]["FSystemId"].ToString();
                    string rightID = dt.Rows[i]["FID"].ToString();
                    DateTime time = DateTime.Now.AddHours(1);
                    string key = SecurityEncryption.DesEncrypt(rightID + "|" + SecurityEncryption.ConvertDateTimeInt(time), sh.getSystemKey(FSystemId));

                    DataTable d = sh.GetTable("select FDesc,FRemark,FPic from CF_Sys_System where fnumber=@fnumber", new SqlParameter("@fnumber", dt.Rows[i]["FSystemId"].ToString()));
                    if (d != null && d.Rows.Count > 0)
                    {
                        string img = "<img src='" + d.Rows[0]["FPic"].ToString() + "'/>";
                        string state = "<font color='green'>正常</font>";

                        DateTime dTime = DateTime.Now;
                        DateTime endTime = EConvert.ToDateTime(dt.Rows[i]["FEndTime"]);
                        if (endTime < dTime) //过期了
                        {

                            state = "<font color='red'>已于" + endTime.ToShortDateString() + "过期</font>";
                            str += "<a href='javascript:alert(\"对不起，该帐户已过期\\n请和管理员联系\");' style='FILTER: gray;'><span>" + img + "</span>";
                        }
                        else
                        {
                            str += "<a href='javascript:login(\"" + key + "\",\"" + FSystemId + "\");'><span>" + img + "</span>";
                        }
                        str += "<strong>";
                        str += "<samp>" + d.Rows[0]["FDesc"].ToString() + "</samp>";
                        str += "<b>&nbsp;&nbsp;&nbsp;&nbsp;" + d.Rows[0]["FRemark"].ToString() + "</b>";
                        str += "<big>状态：" + state + "</big>";
                        str += "</strong>";
                        str += "</a>";

                    }


                }

            }
            else
            {
                //没有任何系统权限
            }
            sys_List.InnerHtml = str;
            sys_List.Style.Add(HtmlTextWriterStyle.Height, (145 * dt.Rows.Count).ToString());
        }
        else
        {
            this.Response.Redirect("EntLogin.aspx");
        }
    }

    /// <summary>
    /// 得到加密串，并保存登陆日志
    /// </summary>
    /// <param name="UserRightId"></param>
    /// <returns></returns>
    private string GetKey(string UserRightId)
    {
        pageTool tool = new pageTool(Page);
        string str = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,u.FType,u.FCreateTime,u.FCompany,u.FManageDeptId,u.FLinkMan,u.FTel,u.FState, ");//user表
        sb.Append("r.FID rFID,r.FUserId rFUserId,r.FCreateTime rFCreateTime,r.FYBaseinfoID rFBaseinfoId,");//userright表
        sb.Append("r.FName rFName,r.FPassWord rFPassWord,r.FLockLabelNumber rFLockLabelNumber,r.FLockNumber rFLockNumber,");//userright表
        sb.Append("r.FBeginTime rFBeginTime,r.FEndTime rFEndTime,r.FSystemId rFSystemId,r.FState rFState ");//userright表
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 and r.FID=@FID ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FID", UserRightId));
        if (dt != null && dt.Rows.Count > 0)
        {

            sb.Remove(0, sb.Length);
            //user表
            sb.Append(dt.Rows[0]["FID"].ToString() + ",");
            sb.Append(dt.Rows[0]["FType"].ToString() + ",");
            sb.Append(dt.Rows[0]["FCreateTime"].ToString() + ",");
            sb.Append(dt.Rows[0]["FCompany"].ToString() + ",");
            sb.Append(dt.Rows[0]["FManageDeptId"].ToString() + ",");
            sb.Append(dt.Rows[0]["FLinkMan"].ToString() + ",");
            sb.Append(dt.Rows[0]["FTel"].ToString() + ",");
            //userright表
            sb.Append(dt.Rows[0]["rFID"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFUserId"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFCreateTime"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFBaseinfoId"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFName"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFPassWord"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFLockLabelNumber"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFLockNumber"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFBeginTime"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFEndTime"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFSystemId"].ToString() + ",");
            sb.Append(dt.Rows[0]["rFState"].ToString());
            str = sb.ToString();



            //登陆日志
            StringBuilder ss = new StringBuilder();
            ss.Append("系统：" + sh.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
            ss.Append("\n企业：" + dt.Rows[0]["FCompany"].ToString());
            ss.Append("\nUserrightID：" + UserRightId);
            ss.Append("\n时间：" + DateTime.Now);
            ss.Append("\n事件：通过统一认证选择系统登陆");
            DataLog.Write(LogType.Info, LogSort.Safety, "统一认证选择系统登陆", ss.ToString(), UserRightId);
        }
        return str;
    }


    #endregion

    #region 开通其它系统权限

    private void showOther(string UserId)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID from cf_user_reg ");
        sb.Append("where frfid=@UserId and ftype=8 and isnull(FIsApp,0)=0 ");
        sb.Append("and isnull(FIsdeleted,0)=0 ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@UserId", UserId));
        if (dt != null && dt.Rows.Count > 0)
        {
            showIsOpen();
        }
        else
        {
            sb = sb.Remove(0, sb.Length);
            sb.Append("select FName,FNumber from CF_Sys_System ");
            sb.Append("where fisdeleted=0 and ftype=1 ");//ftype=1：企业系统类型
            sb.Append("and FNumber not in(select FSystemId from cf_sys_userright ");
            sb.Append("where fuserid=@FUserID)");
            dt = sh.GetTable(sb.ToString(), new SqlParameter("@FUserID", UserId));
            if (dt != null && dt.Rows.Count > 0)
            {
                t_FSysList.DataSource = dt;
                t_FSysList.DataTextField = "FName";
                t_FSysList.DataValueField = "FNumber";
                t_FSysList.DataBind();

                Other_IsReg.Visible = false;
                Other_reg.Visible = true;
                Other_NoReg.Visible = false;
            }
            else
            {

                lit_NoOther.Text = "<font style='color:red'>当前没有可以开通的系统。</font>";

                Other_IsReg.Visible = false;
                Other_reg.Visible = false;
                Other_NoReg.Visible = true;
            }
        }
    }


    /// <summary>
    /// 显示已提交的开通其它
    /// </summary>
    private void showIsOpen()
    {
        Other_IsReg.Visible = true;
        Other_NoReg.Visible = true;
        Other_reg.Visible = false;

        lit_NoOther.Text = "<b style='color:green;font-size:13px;font-weight:bold;' >您已成功提交开通其它系统权限表单，等待管理员审核</b>";

        StringBuilder sb = new StringBuilder();
        sb.Append("select FID,FSysList,FLicencePic,FIsApp from cf_user_reg ");
        sb.Append("where frfid=@UserId and ftype=8 and isnull(FIsApp,0)=0 ");
        sb.Append("and isnull(FIsdeleted,0)=0 ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@UserId", ViewState["UserId"]));
        if (dt != null && dt.Rows.Count > 0)
        {
            string FID = dt.Rows[0]["FID"].ToString();
            IsReg_YY.Text = dt.Rows[0]["FLicencePic"].ToString();
            IsReg_State.Text = dt.Rows[0]["FIsApp"].ToString() == "1" ? "<font color='green'>已审核通过</font>" : "<font color='red'>待审核</font>";

            sb = sb.Remove(0, sb.Length);
            string str = "";
            sb.Append("select s.FName,r.FState,r.FIsApp from cf_user_regright r,cf_sys_system s ");
            sb.Append("where r.fsystemid=s.fnumber and  FRegId=@FRegId ");
            dt = sh.GetTable(sb.ToString(), new SqlParameter("@FRegId", FID));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(str))
                    str += ",";
                str += dt.Rows[i]["FName"].ToString();

            }
            IsReg_SysList.Text = str;
        }
    }



    /// <summary>
    /// 提交 开通其它
    /// </summary>
    private void OpenOther()
    {
        pageTool tool = new pageTool(this.Page);
        show();
        if (!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
        {
            tool.showMessage("验证码输入错误！");
          
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID from cf_user_reg ");
        sb.Append("where frfid=@UserId and ftype=8 and isnull(FIsApp,0)=0 ");
        sb.Append("and isnull(FIsdeleted,0)=0 ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@UserId", ViewState["UserId"]));
        if (dt != null && dt.Rows.Count > 0)
        {
            showIsOpen();
            return;
        }

        dt = sh.GetTable("select FManageDeptId,FCompany from cf_sys_user where fid=@FID", new SqlParameter("@FID", ViewState["UserId"]));
        if (dt != null && dt.Rows.Count > 0)
        {
            sb.Remove(0, sb.Length);
            SortedList sl = new SortedList();
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FIsDeleted", 0);
            sl.Add("FBaseInfoId", Guid.NewGuid().ToString());
            sl.Add("FRFID", ViewState["UserId"]);//当前UserId
            sl.Add("FCreateTime", DateTime.Now);
            sl.Add("FType", 8);//企业用户开通其它
            sl.Add("FState", 0);
            sl.Add("FLicencePic", t_FYY.Text);//开通原由
            sl.Add("FCompany", dt.Rows[0]["FCompany"]);//企业名 
            sl.Add("FManageDeptId", dt.Rows[0]["FManageDeptId"]);//主管部门 
            sl.Add("FTime", DateTime.Now);
            sl.Add("FIsApp", 0);

            sb.Append("insert into CF_User_Reg ");
            sb.Append("(FID,FIsDeleted,FRFID,FCreateTime,FType,FState,FLicencePic,FCompany,FManageDeptId,FTime,FIsApp )");
            sb.Append("values ");
            sb.Append("(@FID,@FIsDeleted,@FRFID,@FCreateTime,@FType,@FState,@FLicencePic,@FCompany,@FManageDeptId,@FTime,@FIsApp )");

            int n = 0;
            foreach (ListItem item in t_FSysList.Items)
            {
                if (item.Selected)
                {
                    sl.Add("FSystemId" + n.ToString(), item.Value);

                    sb.Append("insert into CF_User_RegRight ");
                    sb.Append("(FID, FRegId, FSystemId, FType, FState, FIsApp, FTime, FCreateTime, FIsDeleted) ");
                    sb.Append("values ");
                    sb.Append("(newid(),@FID,@FSystemId" + n.ToString() + ",@FType,@FState,@FIsApp,getdate(),@FCreateTime,@FIsDeleted) ");
                    n++;
                }
            }

            if (sh.PExcute(sb.ToString(), sh.ConvertParameters(sl)))//提交
            {
                showIsOpen();
            }
            else
            {
                tool.showMessage("提交失败");
            }
        }
        else
        {
            tool.showMessage("提交失败");
        }
    }

    #endregion

    #region 按钮

    //登陆按钮
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool();
        string sysId = hidd_sysId.Value;
        string str = hidd_FID.Value;
        if (!string.IsNullOrEmpty(str))
        {
            string rFID = "";
            string[] strArray = SecurityEncryption.DesDecrypt(str, sh.getSystemKey(sysId)).Split('|');
            if (strArray.Length == 2)
            {
                if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                    rFID = strArray[0];
            }


            str = GetKey(rFID);
            if (!string.IsNullOrEmpty(str))
            {
                string url = getLoginURL(sysId);
                if (!string.IsNullOrEmpty(url))
                {
                    DateTime time = DateTime.Now.AddHours(1);
                    string key = SecurityEncryption.DesEncrypt(str + "|" + SecurityEncryption.ConvertDateTimeInt(time), sh.getSystemKey(sysId));
                    Response.Redirect(url + "?code=" + HttpUtility.UrlEncode(key, Encoding.UTF8) + "&SystemId=" + sysId + "&checkType=" + 2);
                }
                else
                {
                    tool.showMessage("登陆失败，配置登陆地址错误。请和管理员联系。");
                }
            }
            else
            {
                tool.showMessage("登陆失败，请重试或联系管理员");
            }


        }
        else
        {
            tool.showMessage("登陆失败，请重试或联系管理员");
        }
    }


    //提交按钮
    protected void btnREG_Click(object sender, EventArgs e)
    {
        OpenOther();
    }

    //选择卡换
    protected void btn_n_Click(object sender, EventArgs e)
    {
        show();
    }

    #endregion

    #region 公共方法

    private void show()
    {
        pageTool tool = new pageTool(Page);
        tool.ExecuteScript("show();");
    }

    /// <summary>
    /// 从指定的share库系统编号得到该系统登陆地址
    /// </summary>
    /// <param name="ShareSysId"></param>
    /// <returns></returns>
    private string getLoginURL(string ShareSysId)
    {
        string str = "";
        str = sh.GetSignValue("select FLoginURL from CF_Sys_System where FNumber=@FNumber", new SqlParameter("@FNumber", ShareSysId));
        return str;
    }


    /// <summary>
    /// 得到中文的期星几
    /// </summary>
    /// <param name="dTime"></param>
    /// <returns></returns>
    private string getWeekName(DateTime dTime)
    {
        string str = "";
        switch (dTime.DayOfWeek)
        {
            case DayOfWeek.Monday:
                str = "星期一";
                break;
            case DayOfWeek.Tuesday:
                str = "星期二";
                break;
            case DayOfWeek.Wednesday:
                str = "星期三";
                break;
            case DayOfWeek.Thursday:
                str = "星期四";
                break;
            case DayOfWeek.Friday:
                str = "星期五";
                break;
            case DayOfWeek.Saturday:
                str = "星期六";
                break;
            case DayOfWeek.Sunday:
                str = "星期日";
                break;
        }
        return str;
    }

    #endregion

}
