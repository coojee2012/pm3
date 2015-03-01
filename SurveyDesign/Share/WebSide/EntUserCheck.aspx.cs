using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using System.Collections;
using Approve.RuleCenter;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Approve.PersistBase;


public partial class Share_WebSide_EntUserCheck : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Write("没有仅限访问");
                Response.End();
            }
            CheckUser();
        }
    }



    /// <summary>
    /// 用户验证
    /// </summary>
    /// <param name="ct"></param>
    private void CheckUser()
    {
        string checkType = EConvert.ToString(Request.Form["C_FType"]);//验证类型，1：加密锁验证；2：用户名验证
        string FSystemId = "";//系统类型
        string str = "";
        if (checkType == "1")//加密锁验证
        {
            str = check_Lock();
            FSystemId = EConvert.ToString(Request.Form["C_FSystemId"]);
        }
        else if (checkType == "2")//用户名验证
        {
            str = check_Name();
            FSystemId = EConvert.ToString(Request.Form["C_FSystemId"]);
        }
        else if (checkType == "3")//后台登陆验证
        {
            str = check_Back();
            string rightID = EConvert.ToString(Request.Form["dbSystem"]);//
            FSystemId = sh.GetSignValue("select FSystemId from cf_sys_userright where fid='" + rightID + "'");
        }
        else
        {
            Response.Redirect(Request.UrlReferrer.ToString());
        }
        DateTime time = DateTime.Now.AddHours(1);
        string key = SecurityEncryption.DesEncrypt(str + "|" + SecurityEncryption.ConvertDateTimeInt(time), sh.getSystemKey(FSystemId));
        Response.Redirect(getRequestUrl() + "?code=" + HttpUtility.UrlEncode(key, Encoding.UTF8) + "&SystemId=" + FSystemId + "&checkType=" + checkType);
    }



    /// <summary>
    /// 用户名验证（企业用户名登陆时，直接验证CF_User_Reg表,即注册用户表）
    /// </summary>
    /// <param name="ct"></param>
    private string check_Name()
    {
        string FSystemId = EConvert.ToString(Request.Form["C_FSystemId"]);//系统类型
        string FName = EConvert.ToString(Request.Form["C_FName"]);//用户名
        string FPassWord = EConvert.ToString(Request.Form["C_FPwd"]);//密码
        string str = "";

        SortedList sl = new SortedList();
        sl.Add("FSystemId", FSystemId);
        sl.Add("FName", FName);
        sl.Add("FPassWord", FPassWord);


        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,r.FID rFID,u.FState,r.FState rFState,r.FEndTime rFEndTime,isnull(r.FIsUserName,0) FIsUserName ");
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 ");
        sb.Append("and r.FName=@FName and r.FPassWord=@FPassWord ");
        sb.Append("and r.FSystemId=@FSystemId and ftype=2  ");
        DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["rfstate"].ToString() == "0" || dt.Rows[0]["FState"].ToString() == "0")
            {
                str = "1";// "该用户已被注销，请和管理员联系";
            }
            else
            {
                if (dt.Rows[0]["FIsUserName"].ToString() == "0")
                {
                    str = "6";// "该用户已发锁，请用加密锁登陆";
                }
                else
                {
                    try
                    {
                        DateTime endTime = EConvert.ToDateTime(dt.Rows[0]["rFEndTime"]);
                        DateTime dTime = DateTime.Now;
                        if (endTime < dTime)
                        {
                            str = "2";// "加您的加密锁已过期。";
                        }
                        else
                        {
                            str = GetKey(dt.Rows[0]["rFID"].ToString(), LogType.Info, "企业登陆");
                        }
                    }
                    catch (Exception ex)
                    {
                        str = "0";//"登陆失败。";
                    }
                }
            }
        }
        else
            str = "3";//"登陆失败,用户不存在";


        return str;
    }


    /// <summary>
    /// 后台登陆验证
    /// </summary>
    /// <param name="ct"></param>
    private string check_Back()
    {
        string rightID = EConvert.ToString(Request.Form["dbSystem"]);//

        SortedList sl = new SortedList();
        sl.Add("FID", rightID);


        string str = "";
        if (!string.IsNullOrEmpty(rightID))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.FID,r.FID rFID,u.FState,r.FState rFState,r.FEndTime rFEndTime ");
            sb.Append("from cf_sys_user u,cf_sys_userright r ");
            sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 and r.FID=@FID ");
            DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["rfstate"].ToString() == "0" || dt.Rows[0]["FState"].ToString() == "0")
                {
                    str = "1";// "该用户已被注销，请和管理员联系";
                }
                else
                {
                    try
                    {
                        DateTime endTime = EConvert.ToDateTime(dt.Rows[0]["rFEndTime"]);
                        DateTime dTime = DateTime.Now;
                        if (endTime < dTime)
                        {
                            str = "2";// "加您的加密锁已过期。";
                        }
                        else
                        {
                            str = GetKey(dt.Rows[0]["rFID"].ToString(), LogType.Info, "后台登陆企业");
                        }
                    }
                    catch (Exception ex)
                    {
                        str = "0";//"登陆失败。";
                    }
                }
            }
            else
                str = "3";//"登陆失败,用户不存在";
        }
        else
        {
            str = "0"; //"登陆失败。";
        }
        return str;
    }

    /// <summary>
    /// 加密锁验证
    /// </summary>
    /// <param name="ct"></param>
    private string check_Lock()
    {
        string FLockNumber = EConvert.ToString(Request.Form["C_FLockNumber"]);//加密锁硬件编号
        string FSystemId = EConvert.ToString(Request.Form["C_FSystemId"]);//系统类型

        SortedList sl = new SortedList();
        sl.Add("FSystemId", FSystemId);
        sl.Add("FLockNumber", FLockNumber);

        string str = "";

        if (!string.IsNullOrEmpty(FLockNumber))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.FID,r.FID rFID,u.FState,r.FState rfstate,r.FEndTime rFEndTime ");
            sb.Append("from cf_sys_user u,cf_sys_userright r ");
            sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 and r.FLockNumber=@FLockNumber and r.FSystemId=@FSystemId ");
            DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["rfstate"].ToString() == "0" || dt.Rows[0]["FState"].ToString() == "0")
                {
                    str = "1";// "该用户已被注销，请和管理员联系";
                }
                else
                {
                    try
                    {
                        DateTime endTime = EConvert.ToDateTime(dt.Rows[0]["rFEndTime"]);
                        DateTime dTime = DateTime.Now;
                        if (endTime < dTime)
                        {
                            str = "2";// "加您的加密锁已过期。";
                        }
                        else
                        {
                            str = GetKey(dt.Rows[0]["rFID"].ToString(), LogType.Info, "企业登陆");
                        }
                    }
                    catch (Exception ex)
                    {
                        str = "0";//"登陆失败。";
                    }
                }
            }
            else
                str = "3";//"登陆失败,用户不存在";
        }
        else
        {
            str = "0"; //"登陆失败。";
        }
        return str;
    }


    /// <summary>
    /// 得到加密串，并保存登陆日志
    /// </summary>
    /// <param name="UserRightId"></param>
    /// <returns></returns>
    private string GetKey(string UserRightId, Approve.PersistBase.LogType logType, string strTs)
    {
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
            ss.Append("\n事件：通过" + strTs);
            DataLog.Write( logType, LogSort.Safety, strTs, ss.ToString(), UserRightId);
        }
        return str;
    }



    private string getRequestUrl()
    {
        string url = Request.UrlReferrer.ToString();
        int n = url.IndexOf("?");
        if (n > -1)
        {
            url = url.Substring(0, n);
        }

        return url;
    }
}
