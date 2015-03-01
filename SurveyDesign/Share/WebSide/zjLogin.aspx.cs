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

public partial class Share_WebSide_zjLogin : System.Web.UI.Page
{
    /*
    * 统一认证登陆页面，  
    * 只验证主帐户（cf_sys_user表）
    * 企业和管理部门都能从这登陆
    */
    Share sh = new Share();
    string FSystemId = "8015";//设计单位
    ShareTool st = new ShareTool();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            C_FPwd.TextMode = TextBoxMode.Password;
            C_FPwd.Text = string.Empty;
            C_FName.Text = string.Empty;
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
                //检查专家库是否有
                string cou = sh.GetSignValue("select RegisterID from LINKER_95.dbCenterSC.dbo.CF_Pro_Regist where ReUserName='"
                    + FName + "' and RePassWord='" + C_FPwd.Text + "' ");
                if (string.IsNullOrEmpty(cou))
                { tool.ExecuteScript("show();$(function(){alert('用户名或者密码错误');});"); return; }
                Session["PsID"] = cou;

                StringBuilder sb = new StringBuilder();
                sb.Append("select u.FID uFId,r.FId rFId,u.FState,u.FEndTime, ");
                sb.Append("isnull(u.FIsUserName,0) FIsUserName,u.FType, ");
                sb.Append("u.FCompany from CF_Sys_User u inner join  ");
                sb.Append("CF_Sys_Userright r ");
                sb.Append("on u.fid=r.FUserId  ");
                sb.Append("where r.FSystemId='" + FSystemId + "' ");
                sb.Append("and isnull(u.fisdeleted,0)=0 and u.FName='gfzj'  ");
                //管理部门和收费系统用户
                sb.Append("and u.FPassWord='sdrD5LgCRHQ=' and u.ftype in (1,5,2)");
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
                            //if (endTime < dTime)
                            //{
                            //    str = "用户已过期。";
                            //}
                            //else
                            //{
                            string CAResult = string.Empty;
                            if (IsCheckCA())
                            {
                                CAResult = st.ValidateCA(FName, CaCerti.Value, 1, dt.Rows[0]["uFID"].ToString());
                            }
                            if (string.IsNullOrEmpty(CAResult))
                            {
                                //登陆
                                LOGIN(dt.Rows[0]["FType"].ToString(), dt.Rows[0]["FCompany"].ToString(), dt.Rows[0]["uFId"].ToString());
                            }
                            else
                            {
                                str = CAResult;
                            }
                        }
                    }
                }
                else
                {
                    if (isTry)
                    {
                        isTry = false;
                        //if (st.DownloadFromMarket(FName, 220, 2))
                        //{
                        Login();
                        //}
                        //else
                        //{
                        //    str = "您的没有权限使用该系统";
                        //}
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
    private void LOGIN(string FType, string FCompany, string UserId)
    {
        StringBuilder ss = new StringBuilder();
        ss.Append("\n管理部门：" + FCompany);
        ss.Append("\nUserID：" + UserId);
        ss.Append("\nIP：" + Request.UserHostAddress);
        ss.Append("\n时间：" + DateTime.Now);
        ss.Append("\n事件：通过统一认证选择系统登陆");
        DataLog.Write(LogType.Info, LogSort.Safety, "统一认证主帐户登陆", ss.ToString(), UserId);
        if (!string.IsNullOrEmpty(UserId))
        {
            DateTime time = DateTime.Now.AddHours(1);
            string key = SecurityEncryption.DesEncrypt(UserId + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
            string sUrl = "../../ApproveWeb/main/LockCheck.aspx?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
            this.Response.Redirect(sUrl);
        }
        else
        {
            pageTool tool = new pageTool(Page);
            tool.showMessage("登陆失败，请重试");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Login();
    }

}
