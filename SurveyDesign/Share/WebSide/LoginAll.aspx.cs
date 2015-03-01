using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Data.SqlClient;
using ProjectData;

public partial class Share_WebSide_LoginAll : System.Web.UI.Page
{
    /*
    * 统一认证登陆页面，  
    * 只验证主帐户（cf_sys_user表）
    * 企业和管理部门都能从这登陆
    */
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
            C_FPwd.TextMode = TextBoxMode.Password;
            C_FPwd.Text = string.Empty;
            C_FName.Text = string.Empty;

            //是否显示演示用户
            if (sh.GetSysObjectContent("_Login_OpenUSER") != "1")
            {
                d_d.Visible = false;
            }
            showInfo();
        }
    }

    private void showInfo()
    {
        /*
        1：默认加密锁
        2：默认用户名
        3：只有加密锁
        4：只有用户名
         */
        string t = sh.GetSysObjectContent("_Login_DefaultTYPE");
        switch (t)
        {
            case "1":
                C_FType.SelectedValue = "ta_lock";
                break;
            case "2":
                C_FType.SelectedValue = "ta_User";
                break;
            case "3":
                C_FType.SelectedValue = "ta_lock";
                ta_User.Visible = false;
                tab_User.Visible = false;
                break;
            case "4":
                C_FType.SelectedValue = "ta_User";
                ta_lock.Visible = false;
                tab_lock.Visible = false;
                break;
        }
    }


    //登陆
    private void Login()
    {
        string str = "";
        pageTool tool = new pageTool(this.Page);

        if (C_FType.SelectedValue == "ta_User")
        {
            #region 用户名密码登陆
            if (1 == 2)//(!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
            {
                str = "验证码输入错误！";
            }
            else
            {
                string FName = C_FName.Text; //用户名
                string FPassWord = C_FPwd.Text; //密码
                FPassWord = SecurityEncryption.DESEncrypt(FPassWord);

                //增加同步用户名密码 ljr 2015-1-6
                string pass = sh.GetSignValue("select FPassWord from LINKER_46.dbCenterSC.dbo.cf_sys_user where FName='" + FName + "' ");
                string table = "user";
                if (string.IsNullOrEmpty(pass))
                {
                    pass = sh.GetSignValue("select FPassWord from LINKER_46.dbCenterSC.dbo.CF_Sys_UserRight where FName='" + FName + "' ");
                    table = "right";
                }
                if (!string.IsNullOrEmpty(pass))
                {
                    pass = Encrypt.MiscClass.decode(pass); pass = SecurityEncryption.DESEncrypt(pass);
                    string synchronous = " exec JKC_PRO_Synchronous '" + FName + "',0,'" + pass + "','" + table + "' ";
                    sh.PExcute(synchronous);
                }


                SortedList sl = new SortedList();
                sl.Add("FName", FName);
                sl.Add("FPassWord", FPassWord);

                StringBuilder sb = new StringBuilder();
                sb.Append("select FID,FState,FEndTime,isnull(FIsUserName,0) FIsUserName,FType,FCompany from CF_Sys_User ");
                sb.Append("where fisdeleted=0 and FName=@FName and FPassWord=@FPassWord and ftype in (1,5,2) ");//管理部门和收费系统用户
                DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
                if (dt != null && dt.Rows.Count > 0)//先验证是否是主帐户登陆。
                {
                    //此项目平台同步到JKCWFDB_WORK_NJS  ljr 2015-1-17
                    if (string.IsNullOrEmpty(pass))
                    {
                        FPassWord = SecurityEncryption.DESDecrypt(FPassWord); FPassWord = Encrypt.MiscClass.encode(FPassWord);
                        string sql = " exec JKC_PRO_SynchronousNJS  '" + dt.Rows[0]["FID"].ToString() + "','" + pass + "' ";
                        sh.PExcute(sql);
                    }

                    if (dt.Rows[0]["FState"].ToString() == "0")
                    {
                        str = "您的用户已被注销，请和管理员联系。";
                    }
                    else
                    {
                        if (dt.Rows[0]["FIsUserName"].ToString() == "0")
                        {
                            str = "您的该帐户被禁用用户名登陆，请使用加密锁登陆。";
                        }
                        else
                        {
                            DateTime endTime = EConvert.ToDateTime(dt.Rows[0]["FEndTime"]);
                            DateTime dTime = DateTime.Now;
                            if (endTime < dTime)
                            {
                                str = "用户已过期。";
                            }
                            else
                            {
                                //登陆
                                LOGIN(dt.Rows[0]["FType"].ToString(), dt.Rows[0]["FCompany"].ToString(), dt.Rows[0]["FID"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    str = "用户名或密码错误！";
                }
            }
            #endregion
        }
        else
        {
            #region 加密锁登陆

            string FLockNumber = C_FLockNumber.Value; //加密锁硬件编号

            SortedList sl = new SortedList();
            sl.Add("FLockNumber", FLockNumber);

            StringBuilder sb = new StringBuilder();
            sb.Append("select FID,FState,FEndTime,FType,FCompany ");
            sb.Append("from CF_Sys_User ");
            sb.Append("where fisdeleted=0 and FLockNumber=@FLockNumber and ftype in (1,2,5) ");
            DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)    //先验证是否是主帐户登陆。
            {
                if (dt.Rows.Count > 1)
                {
                    str = "加密锁重复，请和管理员联系";
                }
                else
                {
                    if (dt.Rows[0]["FState"].ToString() == "0")
                    {
                        str = "您的用户已被注销，请和管理员联系。";
                    }
                    else
                    {
                        DateTime endTime = EConvert.ToDateTime(dt.Rows[0]["FEndTime"]);
                        DateTime dTime = DateTime.Now;
                        if (endTime < dTime)
                        {
                            str = "用户已过期。";
                        }
                        else
                        {
                            //增加同步用户名密码 ljr 2014-11-25
                            string pass = sh.GetSignValue("select FPassWord from LINKER_46.dbCenterSC.dbo.cf_sys_user where FName='"
                                + sh.GetSignValue("select FName from from CF_Sys_User where FID='" + dt.Rows[0]["FID"]) + "' ");
                            string table = "user";
                            if (string.IsNullOrEmpty(pass))
                            {
                                pass = sh.GetSignValue("select FPassWord from LINKER_46.dbCenterSC.dbo.CF_Sys_UserRight where FName='" +
                                     sh.GetSignValue("select FName from from CF_Sys_User where FID='" + dt.Rows[0]["FID"]) + "' ");
                                table = "right";
                            }
                            if (!string.IsNullOrEmpty(pass))
                            {
                                pass = Encrypt.MiscClass.decode(pass); pass = SecurityEncryption.DESEncrypt(pass);
                                string synchronous = "exec JKC_PRO_Synchronous '" + dt.Rows[0]["FID"] + "',1,'" + pass + "','" + table + "' ";
                                sh.PExcute(synchronous);
                            }

                            //登陆
                            LOGIN(dt.Rows[0]["FType"].ToString(), dt.Rows[0]["FCompany"].ToString(), dt.Rows[0]["FID"].ToString());
                        }
                    }
                }
            }
            else
            {
                str = "您的加密锁没有被授权";
            }
            #endregion
        }
        tool.ExecuteScript("show();$(function(){alert('" + str + "');});");
    }

    /// <summary>
    /// 登陆
    /// </summary>
    private void LOGIN(string FType, string FCompany, string FID)
    {
        if (FType == "2")//企业用户
        {
            #region  记录登陆日志
            StringBuilder ss = new StringBuilder();
            ss.Append("\n企业：" + FCompany);
            ss.Append("\nUserID：" + FID);
            ss.Append("\nIP：" + Request.UserHostAddress);
            ss.Append("\n时间：" + DateTime.Now);
            ss.Append("\n事件：通过统一认证选择系统登陆");
            DataLog.Write(LogType.Info, LogSort.Safety, "统一认证主帐户登陆", ss.ToString(), FID);
            #endregion

            REdirect(FID);//加密并登陆，并转入选择系统页面
        }
        else if (FType == "1")//管理部门用户
        {
            #region  记录登陆日志
            StringBuilder ss = new StringBuilder();
            ss.Append("\n管理部门：" + FCompany);
            ss.Append("\nUserID：" + FID);
            ss.Append("\nIP：" + Request.UserHostAddress);
            ss.Append("\n时间：" + DateTime.Now);
            ss.Append("\n事件：通过统一认证选择系统登陆");
            DataLog.Write(LogType.Info, LogSort.Safety, "统一认证主帐户登陆", ss.ToString(), FID);
            #endregion
            REdirect(FID);
        }
        else if (FType == "5")//收费系统管理员
        {
            PayLogin(FID);
        }

    }


    /// <summary>
    /// 企业登陆，加密ID并跳转到系统选择页面。
    /// </summary>
    /// <param name="UserId"></param>
    private void REdirect(string UserId)
    {
        if (!string.IsNullOrEmpty(UserId))
        {
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(UserId + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/LockCheckAll.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            this.Response.Redirect(sUrl);
        }
        else
        {
            pageTool tool = new pageTool(Page);
            tool.showMessage("登陆失败，请重试");
        }
    }

    /// <summary>
    /// 收费管理员登陆
    /// </summary>
    /// <param name="UserId"></param>
    private void PayLogin(string UserId)
    {
        if (!string.IsNullOrEmpty(UserId))
        {
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(UserId + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            this.Response.Redirect("../../Payment/main/LockCheck.aspx?Key=" + HttpUtility.UrlEncode(key, Encoding.UTF8));
        }
        else
        {
            pageTool tool = new pageTool(Page);
            tool.showMessage("登陆失败，请重试");
        }
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


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Login();
    }
}
