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

public partial class Share_WebSide_SJDWLogin1 : System.Web.UI.Page
{
    /*
    * 统一认证登陆页面，  
    * 只验证主帐户（cf_sys_user表）
    * 企业和管理部门都能从这登陆
    */
    Share sh = new Share();
    string FSystemId = "1553";//设计单位
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
            //this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
            //this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
            C_FPwd.TextMode = TextBoxMode.Password;
            C_FPwd.Text = string.Empty;
            C_FName.Text = string.Empty;

            ////是否显示演示用户
            //if (sh.GetSysObjectContent("_Login_OpenUSER") != "1")
            //{
            //    d_dEmp.Visible = d_d.Visible = false;
            //}
            showInfo();
        }
    }
    public bool IsCheckCA()
    {
        if (sh.GetSysObjectContent("_IsVerifyCA") == "1")
        {
            if (sh.GetSysObjectContent("_VerifyCAUserType").Contains("[" + FSystemId + "]"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public bool IsCheckEmpCA()
    {
        if (sh.GetSysObjectContent("_IsVerifyCA") == "1")
        {
            if (sh.GetSysObjectContent("_VerifyCAUserType").Contains("[" + FSystemId + "Emp]"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void showInfo()
    {
        /*
        1：默认加密锁
        2：默认用户名
        3：只有加密锁
        4：只有用户名
         */
        //string t = sh.GetSysObjectContent("_Login_DefaultTYPE");
        //switch (t)
        //{
        //    case "1":
        //        C_FType.SelectedValue = "ta_lock";
        //        break;
        //    case "2":
        //        C_FType.SelectedValue = "ta_User";
        //        break;
        //    case "3":
        //        C_FType.SelectedValue = "ta_lock";
        //        Ent_ta_User.Visible = false;
        //        tab_User.Visible = false;
        //        break;
        //    case "4":
        //        C_FType.SelectedValue = "ta_User";
        //        Ent_ta_lock.Visible = false;
        //        //tab_lock.Visible = false;
        //        break;
        //}
    }

    bool isTry = true;
    //登陆
    private void Login()
    {
        string str = "";
        pageTool tool = new pageTool(this.Page);
        ShareTool st = new ShareTool();
        if (1 == 1)
        {
            #region 用户名密码登陆
            if (!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
            {
                str = "验证码输入错误！";
            }
            else
            {
                string FName = C_FName.Text; //用户名
                string FPassWord = C_FPwd.Text; //密码
                FPassWord = SecurityEncryption.DESEncrypt(FPassWord);

                SortedList sl = new SortedList();
                sl.Add("FName", FName);
                sl.Add("FPassWord", FPassWord);

                StringBuilder sb = new StringBuilder();
                sb.Append("select u.FID uFId,r.FId rFId,u.FState,u.FEndTime, ");
                sb.Append("isnull(u.FIsUserName,0) FIsUserName,u.FType, ");
                sb.Append("u.FCompany from CF_Sys_User u inner join  ");
                sb.Append("CF_Sys_Userright r ");
                sb.Append("on u.fid=r.FUserId  ");
                sb.Append("where r.FSystemId='" + FSystemId + "' ");
                sb.Append("and u.fisdeleted=0 and u.FName=@FName  ");
                //管理部门和收费系统用户
                sb.Append("and u.FPassWord=@FPassWord and u.ftype in (1,5,2)");
                DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
                if (dt != null && dt.Rows.Count > 0)//先验证是否是主帐户登陆。
                {
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
                                string CAResult = string.Empty;
                                if (IsCheckCA())
                                {
                                    CAResult = st.ValidateCA(FName, CaCerti.Value, 1, dt.Rows[0]["uFID"].ToString());
                                }
                                if (string.IsNullOrEmpty(CAResult))
                                {
                                    //登陆
                                    LOGIN(dt.Rows[0]["FType"].ToString(), dt.Rows[0]["FCompany"].ToString(), dt.Rows[0]["rFID"].ToString());
                                }
                                else
                                {
                                    str = CAResult;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isTry)
                    {
                        isTry = false;
                      
                        if (st.DownloadFromMarket(FName, 155, 2))
                        {
                            Login();
                        }
                        else
                        {
                            str = "您没有权限使用该系统";
                        }
                    }
                    else
                    {
                        str = "用户名或密码错误！";
                    }
                }
            }
            #endregion
        }
        else
        {

        }
        if (!string.IsNullOrEmpty(str))
        {
            tool.ExecuteScript("show();$(function(){alert('" + str + "');});");
        }
    }

    /// <summary>
    /// 登陆
    /// </summary>
    private void LOGIN(string FType, string FCompany, string rFID)
    {
        if (FType == "2")//企业用户
        {
            #region  记录登陆日志
            StringBuilder ss = new StringBuilder();
            ss.Append("\n企业：" + FCompany);
            ss.Append("\nUserID：" + rFID);
            ss.Append("\nIP：" + Request.UserHostAddress);
            ss.Append("\n时间：" + DateTime.Now);
            ss.Append("\n事件：通过统一认证选择系统登陆");
            DataLog.Write(LogType.Info, LogSort.Safety, "统一认证主帐户登陆", ss.ToString(), rFID);
            #endregion
            Response.Redirect(REdirect(FSystemId, rFID));//加密并登陆，并转入选择系统页面
        }
    }
    /// 得到链接登陆地址
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    private string REdirect(string FSystemId, string rFID)
    {
        string str = "";
        string FID = rFID;
        //得到配置的地址
        string Url = sh.getSystemLoginURL(FSystemId);
        if (!string.IsNullOrEmpty(Url))
        {
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(FID + "|" + SecurityEncryption.ConvertDateTimeInt(time), sh.getSystemKey(FSystemId));
            str = Url + "?sysId=" + FSystemId + "&UserRightID=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
        }
        return str;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Login();
    }

    protected void btnReg_Click(object sender, EventArgs e)
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        if (!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
        {
            tool.ExecuteScript("show();$(function(){alert('验证码填写错误！');});");
            return;
        }
        string FPassWord = this.C_FPwd.Text.Trim();
        FPassWord = SecurityEncryption.DESEncrypt(FPassWord);
        var v = (from t in db.CF_Emp_BaseInfo
                 where t.FUserName == this.C_FName.Text && t.FPassword == FPassWord
                 select new
                 {
                     t.FId,
                     t.FUserName
                 }).FirstOrDefault();
        if (v != null)
        {
            string CAResult = string.Empty;
            if (IsCheckEmpCA())
            {
                CAResult = st.ValidateCA(v.FUserName, CaCerti.Value, 2);
            }
            if (string.IsNullOrEmpty(CAResult))
            {
                //加密登陆链接
                DateTime time = DateTime.Now.AddHours(3);
                string key = SecurityEncryption.DesEncrypt(v.FId + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
                string sUrl = "../../ApproveWeb/main/EmpLockCheck.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
                Response.Redirect(sUrl);
            }
            else
            {
                tool.ExecuteScript("show();$(function(){alert('" + CAResult + "');});");
            }
        }
        else
            tool.ExecuteScript("show();$(function(){alert('用户名或密码错误！');});");
    }
}
