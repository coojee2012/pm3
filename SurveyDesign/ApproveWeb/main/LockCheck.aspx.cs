using System;
using System.Data;
using System.Linq;
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
using Approve.EntityCenter;
using Approve.EntityBase;
using Approve.Common;
using System.Data.SqlClient;
using ProjectData;
using ProjectBLL;
using System.Threading;

public partial class ApproveWeb_Main_LockCheck : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    OA oa = new OA();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                GetLogIn(Request.QueryString["key"]);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["UserId"]))//系统管理员登陆
            {
                GetLogInSys(Request.QueryString["UserId"]);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["UserRightID"]))
            {
                string str = "";
                string code = Request.QueryString["UserRightID"];
                string sysId = Request.QueryString["sysId"];
                string[] strArray = SecurityEncryption.DesDecrypt(code, sh.getSystemKey(sysId)).Split('|');
                if (strArray.Length == 2)
                {
                    if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                        str = strArray[0];
                } GetEntLogIn(str);
            }

        }
    }



    /// <summary>
    /// 管理部门和企业登陆
    /// </summary>
    /// <param name="rightFID">userright.FID</param>
    private void GetLogIn(string code)
    {
        string userID = "";
        string[] strArray = SecurityEncryption.DesDecrypt(code, "32165498").Split('|');
        if (strArray.Length == 2)
        {
            if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                userID = strArray[0];
            else
            {
                Response.Clear();
                Response.Write("链接已失效");
                Response.End();
            }
        }

        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,u.FManageDeptId,u.FSystemId,u.FBaseinfoId,u.ftype,u.FCompany ");
        sb.Append("from cf_sys_user u where u.FID=@FID ");
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", userID));
        if (dt != null && dt.Rows.Count > 0)
        {

            string UserId = dt.Rows[0]["FID"].ToString();
            string FType = dt.Rows[0]["ftype"].ToString();//用户类型 
            string FCompany = dt.Rows[0]["FCompany"].ToString();
            string FSystemId = dt.Rows[0]["FSystemId"].ToString();

            #region 企业登陆
            if (FType == "2")
            {
                sb = new StringBuilder();
                sb.Append("select u.FID uFID,r.FID,u.FType,u.FName,r.FMenuRoleId,");
                sb.Append("u.FManageDeptId,u.FSystemId,r.FSystemId rFSystemId,");
                sb.Append("u.FBaseinfoId,u.FCompany,r.FRoleId ");
                sb.Append("from cf_sys_user u,CF_Sys_UserRight r ");
                sb.Append("where u.FID=r.FUserId and u.FID=@FID ");

                dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", userID));
                if (dt != null && dt.Rows.Count > 0)
                {
                    string rFSystemId = dt.Rows[0]["rFSystemId"].ToString();
                    //登陆日志
                    StringBuilder ss = new StringBuilder();
                    ss.Append("系统：" + sh.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
                    ss.Append("\n企业：" + dt.Rows[0]["FCompany"].ToString());
                    ss.Append("\nUserrightID：" + dt.Rows[0]["FID"].ToString());
                    ss.Append("\n时间：" + DateTime.Now);
                    ss.Append("\n事件：企业登陆到系统");
                    DataLog.Write(LogType.EntLogin, LogSort.Safety, "企业登陆到系统", ss.ToString(), dt.Rows[0]["FID"].ToString());

                    Session["FRoleId"] = dt.Rows[0]["FRoleId"].ToString().Trim();
                    Session["FMenuRoleId"] = dt.Rows[0]["FMenuRoleId"].ToString();
                    CurrentEntUser.UserId = dt.Rows[0]["uFID"].ToString();
                    CurrentEntUser.EntUserId = dt.Rows[0]["FID"].ToString();
                    CurrentEntUser.EntId = EConvert.ToString(dt.Rows[0]["FBaseinfoId"]);
                    CurrentEntUser.SystemId = FSystemId;
                    CurrentEntUser.URSystemId = rFSystemId;

                    Session["FBaseId"] = CurrentEntUser.EntId;
                    Session["FBaseName"] = EConvert.ToString(dt.Rows[0]["FCompany"]);
                    string baseinfoId = EConvert.ToString(dt.Rows[0]["FBaseinfoId"]);
                    if (FSystemId == "155" || FSystemId == "15501" || FSystemId == "145" || FSystemId == "126")
                    {
                        if (HttpContext.Current != null && HttpContext.Current.Application != null)
                        {
                            RuleApp.WorkQueue<string> workQueue = Application["workQueue"] as RuleApp.WorkQueue<string>;
                            if (workQueue != null)
                            {
                                ThreadPool.QueueUserWorkItem(o => workQueue.EnqueueItem(baseinfoId));
                            }
                        }

                    }
                    ProjectDB db = new ProjectDB();
                    CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == baseinfoId).FirstOrDefault();
                    if (ent == null)
                    {
                        CF_Ent_BaseInfo NewEnt = new CF_Ent_BaseInfo()
                        {
                            FName = EConvert.ToString(dt.Rows[0]["FCompany"]),
                            FSystemId = EConvert.ToInt(FSystemId),
                            FIsDeleted = false,
                            FTime = DateTime.Now,
                            FCreateTime = DateTime.Now,
                            FState = 1
                        };
                        if (string.IsNullOrEmpty(baseinfoId))
                        {
                            NewEnt.FId = Guid.NewGuid().ToString();
                            CurrentEntUser.EntId = NewEnt.FId;
                        }
                        else
                        {
                            NewEnt.FId = baseinfoId;
                        }
                        db.CF_Ent_BaseInfo.InsertOnSubmit(NewEnt);
                        db.SubmitChanges();
                    }
                    string url = getURL(rFSystemId);
                    if (!string.IsNullOrEmpty(url))
                        this.Response.Redirect(url);
                    else
                    {
                        Response.Clear();
                        Response.Write("链接错误！");
                        Response.End();
                    }
                }
            }

            #endregion

            #region 管理部门
            if (FType == "1")
            {//管理部门 目前只有一个userRight。以下强制用第一个userRight登陆
                ProjectDB db = new ProjectDB();
                var v = (from r in db.CF_Sys_UserRight
                         join s in db.CF_Sys_SystemName on r.FSystemId equals s.FNumber
                         where r.FUserId == userID
                         select new
                         {
                             r.FSystemId,
                             r.FId,
                             s.FLUrl,
                             s.FShareKey
                         }).FirstOrDefault();
                if (v != null)
                {
                    string fsysId = v.FSystemId.ToString();
                    string fLUrl = v.FLUrl;//登录地址
                    string fShareKey = v.FShareKey;
                    string fRightId = v.FId;
                    if (!string.IsNullOrEmpty(fLUrl))
                    {
                        DateTime time = DateTime.Now.AddHours(3);
                        if (!(!string.IsNullOrEmpty(fShareKey) && fShareKey.Length == 8))
                            fShareKey = "12345678";
                        string key = SecurityEncryption.DesEncrypt(fRightId + "|" + SecurityEncryption.ConvertDateTimeInt(time), fShareKey);
                        fLUrl = string.Format(fLUrl + "?UserRightID={0}&sysId={1}", key, fsysId);
                        Response.Redirect(fLUrl);//登录到管理部门
                    }
                    else
                        Response.Write("<script>alert('登录地址无效!');window.close();</script>");
                }
                else
                    Response.Write("<script>alert('没有任何权限!');window.close();</script>");

            }
            #endregion

        }
        else
        {
            Response.Write("没权限访问");
            Response.Write("<script>window.history.go(-1);</script>");
        }
    }
    //登陆
    private void GetEntLogIn(string UserRightID)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID uFID,r.FID,u.FType,u.FName,r.FMenuRoleId,");
        sb.Append("u.FManageDeptId,u.FSystemId,r.FSystemId rFSystemId,");
        sb.Append("r.FBaseinfoId,u.FCompany,r.FRoleId,u.FBaseInfoId uFBaseInfoId ");
        sb.Append("from cf_sys_user u,CF_Sys_UserRight r ");
        sb.Append("where u.FID=r.FUserId and r.FID=@FID ");
        DataTable dt = sh.GetTable(sb.ToString(), new SqlParameter("@FID", UserRightID));
        if (dt != null && dt.Rows.Count > 0)
        {

            string FType = dt.Rows[0]["ftype"].ToString();//用户类型
            string FUserRightId = dt.Rows[0]["FID"].ToString();//CF_Sys_UserRight.FID
            string FSystemId = dt.Rows[0]["FSystemId"].ToString();
            string rFSystemId = dt.Rows[0]["rFSystemId"].ToString();
            #region

            if (FType == "2")  // 企业用户
            {

                //登陆日志
                StringBuilder ss = new StringBuilder();
                ss.Append("系统：" + sh.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
                ss.Append("\n企业：" + dt.Rows[0]["FCompany"].ToString());
                ss.Append("\nUserrightID：" + dt.Rows[0]["FID"].ToString());
                ss.Append("\n时间：" + DateTime.Now);
                ss.Append("\n事件：企业登陆到系统");
                DataLog.Write(LogType.EntLogin, LogSort.Safety, "企业登陆到系统", ss.ToString(), dt.Rows[0]["FID"].ToString());

                Session["FRoleId"] = dt.Rows[0]["FRoleId"].ToString().Trim();
                Session["FMenuRoleId"] = dt.Rows[0]["FMenuRoleId"].ToString();
                CurrentEntUser.UserId = dt.Rows[0]["uFID"].ToString();
                CurrentEntUser.EntUserId = dt.Rows[0]["FID"].ToString();
                CurrentEntUser.EntId = EConvert.ToString(dt.Rows[0]["FBaseinfoId"]);
                CurrentEntUser.SystemId = FSystemId;
                CurrentEntUser.URSystemId = rFSystemId;
                CurrentEntUser.FHUid = Request.QueryString["fhuid"];
                if (string.IsNullOrEmpty(CurrentEntUser.EntId))
                {
                    tool.showMessage("登录失败！");
                    return;
                }
                Session["FBaseId"] = CurrentEntUser.EntId;
                Session["FBaseName"] = EConvert.ToString(dt.Rows[0]["FCompany"]);
                string baseinfoId = EConvert.ToString(dt.Rows[0]["FBaseinfoId"]);
                if (FSystemId == "155" || FSystemId == "15501" || FSystemId == "145" || FSystemId == "126")
                {
                    if (HttpContext.Current != null && HttpContext.Current.Application != null)
                    {
                        RuleApp.WorkQueue<string> workQueue = Application["workQueue"] as RuleApp.WorkQueue<string>;
                        if (workQueue != null)
                        {
                            ThreadPool.QueueUserWorkItem(o => workQueue.EnqueueItem(EConvert.ToString(dt.Rows[0]["uFBaseInfoId"])));
                        }
                    }
                }
                ProjectDB db = new ProjectDB();
                CF_Ent_BaseInfo ent = db.CF_Ent_BaseInfo.Where(t => t.FId == baseinfoId).FirstOrDefault();
                if (ent == null)
                {
                    CF_Ent_BaseInfo NewEnt = new CF_Ent_BaseInfo()
                    {
                        FName = EConvert.ToString(dt.Rows[0]["FCompany"]),
                        FSystemId = EConvert.ToInt(FSystemId),
                        FIsDeleted = false,
                        FTime = DateTime.Now,
                        FCreateTime = DateTime.Now,
                        FState = 1
                    };
                    if (string.IsNullOrEmpty(baseinfoId))
                    {
                        NewEnt.FId = Guid.NewGuid().ToString();
                        CurrentEntUser.EntId = NewEnt.FId;
                    }
                    else
                    {
                        NewEnt.FId = baseinfoId;
                    }
                    db.CF_Ent_BaseInfo.InsertOnSubmit(NewEnt);
                    db.SubmitChanges();
                }
                string url = getURL(rFSystemId);
                if (!string.IsNullOrEmpty(url))
                    this.Response.Redirect(url);
                else
                {
                    Response.Clear();
                    Response.Write("链接错误！");
                    Response.End();
                }
            }
            else if (FType == "1")
            {
                if (FSystemId.Trim() == "150")
                    FSystemId = "101";
                Session["FType"] = FType;//管理部门
                Session["DFSystemId"] = FSystemId;
                Session["DFName"] = dt.Rows[0]["fcompany"].ToString();
                Session["DFUserId"] = dt.Rows[0]["uFID"].ToString();
                Session["FUserId"] = dt.Rows[0]["uFID"].ToString();
                Session["DFUserRightId"] = dt.Rows[0]["FId"].ToString();
                Session["DFRoleId"] = dt.Rows[0]["FRoleId"].ToString();
                Session["DFMenuRoleId"] = dt.Rows[0]["FMenuRoleId"].ToString();
                string mDeptId = dt.Rows[0]["FManageDeptId"].ToString();
                Session["DFId"] = mDeptId;
                Session["DFLevel"] = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FLevel", "FNumber='" + mDeptId + "'");
                //记录登录部门是否是 扩权县
                Session["DFIsTown"] = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FIsTown", "FNumber='" + mDeptId + "'");
                //GetOAUser(dt.Rows[0]["FId"].ToString(), dt.Rows[0]["flinkman"].ToString(), dt.Rows[0]["foaorg"].ToString());//向OA更新信息

                //登陆日志
                StringBuilder ss = new StringBuilder();
                ss.Append("系统：" + sh.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
                ss.Append("\n管理部门用户：" + dt.Rows[0]["FName"].ToString());
                ss.Append("\nUserrightID：" + dt.Rows[0]["FID"].ToString());
                ss.Append("\n时间：" + DateTime.Now);
                ss.Append("\n事件：管理部门登陆到系统");
                DataLog.Write(LogType.EntLogin, LogSort.Safety, "管理部门登陆到系统", ss.ToString(), dt.Rows[0]["FID"].ToString());

                this.Response.Redirect("../../Government/Main/aIndex.aspx");
            }
            #endregion

        }

        else
        {
            this.Response.Write("<script>window.history.go(-1);</script>");
        }
    }

    /// <summary>
    /// 系统管理员登陆
    /// </summary>
    /// <param name="rightFID">user.FID</param>
    private void GetLogInSys(string UserId)
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID,u.FManageDeptId,u.ftype,u.FCompany,u.flinkman,u.FName,u.FRoleId ");
        sb.Append("from cf_sys_user u ");
        sb.Append("where u.fid=@FID ");
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", UserId));
        if (dt != null && dt.Rows.Count > 0)
        {
            //用户类型
            string FType = dt.Rows[0]["ftype"].ToString();
            #region 系统管理部门，不受别的限制
            if (FType == "0")
            {
                Session["Admin_FID"] = dt.Rows[0]["FId"].ToString();
                Session["Admin_Name"] = dt.Rows[0]["FName"].ToString();
                Session["FIsAdmin"] = 1;
                this.Response.Redirect("../../admin/main/aIndex.aspx");
            }
            if (FType == "3")
            {
                Session["Admin_FID"] = dt.Rows[0]["FId"].ToString();
                Session["Admin_Name"] = dt.Rows[0]["FName"].ToString();
                Session["Admin_FRoleId"] = dt.Rows[0]["FRoleId"].ToString();
                Session["FIsAdmin"] = 0;
                this.Response.Redirect("../../admin/main/aIndex.aspx");
            }
            #endregion
        }
        else
        {
            this.Response.Write("<script>window.history.go(-1);</script>");
        }
    }


    private string getURL(string sysId)
    {
        string str = "";
        switch (sysId)
        {
            case "1001"://建设单位
                str = "~/JSDW/main/Index.aspx";
                break;
            case "1122"://建设工程项目综合监管系统
                str = "~/JSDW/main/Index.aspx";
                break;
            case "1451"://施工图
                str = "~/KcsjSgt/Main/Index.aspx";
                break;
            case "1554"://勘察企业
                str = "~/KC/main/Index.aspx";
                break;
            case "1553"://设计企业
                str = "~/SJ/Main/Index.aspx";
                break;
            case "1261"://见证单位子系统
                str = "~/JZDW/Main/Index.aspx";
                break;
            case "1961"://设计施工一体化子系统
                str = "~/SgYTH/Main/Index.aspx";
                break;
            case "220"://工法系统
                str = "~/GFEnt/main/Index.aspx";
                break;
            case "222"://节能材料系统
                str = "~/JNCLEnt/main/Index.aspx";
                break;
            default:
                str = "";
                break;
        }

        return str;
    }


    #region OA相关的
    private void GetOAUser(string UserId, string linkMan, string oaorg)
    {
        StringBuilder sb = new StringBuilder();
        DataTable oadt = oa.GetTable("select fid from cf_oa_emp where fid='" + UserId + "'");
        if (oadt != null && oadt.Rows.Count > 0)
        {
            sb.Append("update cf_oa_emp set FName='" + linkMan + "',FOrganId='" + oaorg + "',FTime=getdate()  ");
            sb.Append("where fid='" + UserId + "'");
        }
        else
        {
            sb.Append("insert into cf_oa_emp(FID,FName,FOrganId,FIsDeleted,FCreateTime,FTime) ");
            sb.Append("values ('" + UserId + "','" + linkMan + "','" + oaorg + "',0,getdate(),getdate()) ");
        }
        if (oa.PExcute(sb.ToString()))
        {
            Session["FEmpID"] = UserId;
        }

    }
    #endregion

    #region
    public string sUrl
    {
        get { return this.HFUrl.Text; }
    }

    public DateTime sEndTime
    {
        get { return EConvert.ToDateTime(this.HFEndTime.Text); }
    }
    //判断字符串是否是数字
    public bool IsNumeric(string str)
    {

        bool isNum = true;
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (!Char.IsNumber(chars[i]))
                isNum = false;
        }
        return isNum;
    }
    #endregion


}
