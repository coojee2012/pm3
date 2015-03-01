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

public partial class Share_WebSide_ManUserCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckUser();
        }
    }



    /// <summary>
    /// 用户验证
    /// </summary>
    /// <param name="ct"></param>
    private void CheckUser()
    {
        string FSystemId = EConvert.ToString(Request.Form["C_FSystemId"]);//系统类型
        if (string.IsNullOrEmpty(FSystemId))
            Response.End();

        string checkType = EConvert.ToString(Request.Form["C_FType"]);//验证类型，1：加密锁验证；2：用户名验证
        string str = "";
        if (checkType == "1")//加密锁验证
            str = check_Lock();
        else if (checkType == "2")//用户名验证
            str = check_Name();
        else if (checkType == "3")//后台登陆验证
            str = check_Back();
        else
        {
            Response.End();
        }


        Share sh = new Share();


        DateTime time = DateTime.Now.AddHours(1);
        string key = SecurityEncryption.DesEncrypt(str + "|" + SecurityEncryption.ConvertDateTimeInt(time), sh.getSystemKey(FSystemId));
        Response.Redirect(Request.UrlReferrer.AbsolutePath + "?key=" + HttpUtility.UrlEncode(key, Encoding.UTF8) + "&checkType=" + checkType);
    }



    /// <summary>
    /// 用户名验证
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

        Share sh = new Share();
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,u.FName,u.FPassWord,u.FLockLabelNumber,u.FLockNumber,u.FBeginTime,u.FEndTime,");//user表
        sb.Append("u.FType,u.FCreateTime,u.FCompany,u.FFunction,u.FManageDeptId,u.FLinkMan,u.FTel,u.FState, ");//user表
        sb.Append("r.FID rFID,r.FUserId rFUserId,r.FCreateTime rFCreateTime,r.FBaseinfoID rFBaseinfoId,");//userright表
        sb.Append("r.FName rFName,r.FPassWord rFPassWord,r.FLockLabelNumber rFLockLabelNumber,r.FLockNumber rFLockNumber,");//userright表
        sb.Append("r.FBeginTime rFBeginTime,r.FEndTime rFEndTime,r.FSystemId rFSystemId,r.FState rFState ");//userright表
        sb.Append("from cf_sys_user u,cf_sys_userright r ");
        sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 ");
        sb.Append("and u.FName=@FName and u.FPassWord=@FPassWord and r.FSystemId=@FSystemId ");
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
                        str = "2";// "您的加密锁已过期。";
                    }
                    else
                    {
                        sb.Remove(0, sb.Length);
                        //user表
                        sb.Append(dt.Rows[0]["FID"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FName"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FPassWord"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FLockLabelNumber"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FLockNumber"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FBeginTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FEndTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FType"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FCreateTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FCompany"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FFunction"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FManageDeptId"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FLinkMan"].ToString() + "@");
                        sb.Append(dt.Rows[0]["FTel"].ToString() + "@");
                        //userright表
                        sb.Append(dt.Rows[0]["rFID"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFUserId"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFCreateTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFBaseinfoId"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFName"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFPassWord"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFLockLabelNumber"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFLockNumber"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFBeginTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFEndTime"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFState"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFCanMod"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFPri"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFSelType"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFRoleId"].ToString() + "@");
                        sb.Append(dt.Rows[0]["rFMenuRoleId"].ToString());
                        str = sb.ToString();
                    }
                }
                catch (Exception ex)
                {
                    str = "2";//"加您的加密锁已过期。";
                }
            }
        }
        else
            str = "3";//"用户名或密码错误";

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

        Share sh = new Share();
        string str = "";
        if (!string.IsNullOrEmpty(rightID))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.FID,u.FName,u.FPassWord,u.FLockLabelNumber,u.FLockNumber,u.FBeginTime,u.FEndTime,");//user表
            sb.Append("u.FType,u.FCreateTime,u.FCompany,u.FFunction,u.FManageDeptId,u.FLinkMan,u.FTel,u.FState, ");//user表
            sb.Append("r.FID rFID,r.FUserId rFUserId,r.FCreateTime rFCreateTime,r.FBaseinfoID rFBaseinfoId,");//userright表
            sb.Append("r.FName rFName,r.FPassWord rFPassWord,r.FLockLabelNumber rFLockLabelNumber,r.FLockNumber rFLockNumber,");//userright表
            sb.Append("r.FBeginTime rFBeginTime,r.FEndTime rFEndTime,r.FSystemId rFSystemId,r.FState rFState ");//userright表
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
                            sb.Remove(0, sb.Length);
                            //user表
                            sb.Append(dt.Rows[0]["FID"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FName"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FPassWord"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLockLabelNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLockNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FBeginTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FEndTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FType"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FCreateTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FCompany"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FFunction"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FManageDeptId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLinkMan"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FTel"].ToString() + "@");
                            //userright表
                            sb.Append(dt.Rows[0]["rFID"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFUserId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFCreateTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFBaseinfoId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFName"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFPassWord"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFLockLabelNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFLockNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFBeginTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFEndTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFState"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFCanMod"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFPri"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFSelType"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFRoleId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFMenuRoleId"].ToString());
                            str = sb.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        str = "2";//"加您的加密锁已过期。";
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
        Share sh = new Share();
        string str = "";

        if (!string.IsNullOrEmpty(FLockNumber))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select u.FID,u.FName,u.FPassWord,u.FLockLabelNumber,u.FLockNumber,u.FBeginTime,u.FEndTime,");//user表
            sb.Append("u.FType,u.FCreateTime,u.FCompany,u.FFunction,u.FManageDeptId,u.FLinkMan,u.FTel,u.FState, ");//user表
            sb.Append("r.FID rFID,r.FUserId rFUserId,r.FCreateTime rFCreateTime,r.FBaseinfoID rFBaseinfoId,");//userright表
            sb.Append("r.FName rFName,r.FPassWord rFPassWord,r.FLockLabelNumber rFLockLabelNumber,r.FLockNumber rFLockNumber,");//userright表
            sb.Append("r.FBeginTime rFBeginTime,r.FEndTime rFEndTime,r.FSystemId rFSystemId,r.FState rFState ");//userright表
            sb.Append("from cf_sys_user u,cf_sys_userright r ");
            sb.Append("where u.fid=r.fuserid and r.fisdeleted=0 ");
            sb.Append("and u.FLockNumber=@FLockNumber and r.FSystemId=@FSystemId ");
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
                            sb.Remove(0, sb.Length);
                            //user表
                            sb.Append(dt.Rows[0]["FID"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FName"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FPassWord"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLockLabelNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLockNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FBeginTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FEndTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FType"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FCreateTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FCompany"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FFunction"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FManageDeptId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FLinkMan"].ToString() + "@");
                            sb.Append(dt.Rows[0]["FTel"].ToString() + "@");
                            //userright表
                            sb.Append(dt.Rows[0]["rFID"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFUserId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFCreateTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFBaseinfoId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFName"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFPassWord"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFLockLabelNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFLockNumber"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFBeginTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFEndTime"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFState"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFCanMod"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFPri"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFSelType"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFRoleId"].ToString() + "@");
                            sb.Append(dt.Rows[0]["rFMenuRoleId"].ToString());
                            str = sb.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        str = "2";//"加您的加密锁已过期。";
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
}
