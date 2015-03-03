using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Data.SqlClient;
using ProjectData;
using Approve.EntityBase;
using System.Threading;

public partial class UserAutoLogin : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(UserId) && UserId.Trim().Length > 0)
        {
            GetLogIn(UserId);
        }
        else
        {
            string callbackfun = Request["callbackfun"];
            Response.Write(callbackfun + "({ret:\"-1\"})");
            Response.End();
        }

    }
    /// <summary>
    /// 管理部门和企业登陆
    /// </summary>
    /// <param name="rightFID">userright.FID</param>
    private void GetLogIn(string userID)
    {
        //增加同步用户名密码 ljr 2014-11-25
        DataTable dtTable = sh.GetTable(string.Format("select FPassWord,FName from LINKER_XZSP.{0}.dbo.cf_sys_user where Fid='" + userID + "'",WebHelper.XZSP_DataBase));
        if (dtTable == null || dtTable.Rows.Count < 1)
        {
            string callbackfun = Request["callbackfun"];
            Response.Write(callbackfun + "({ret:\"-1\"})");
            Response.End();
            return;
        }

        DataRow row = dtTable.Rows[0];
        string pass = row["FPassWord"].ToString();//sh.GetSignValue("select FPassWord from LINKER_46.dbCenterSC.dbo.cf_sys_user where FName='" + FName + "' ");
        string FName = row["FName"].ToString();
        string table = "user";
        if (string.IsNullOrEmpty(pass))
        {
            pass = sh.GetSignValue(string.Format("select FPassWord from LINKER_XZSP.{0}.dbo.CF_Sys_UserRight where FName='" + FName + "' ", WebHelper.XZSP_DataBase));
            table = "right";
        }
        if (!string.IsNullOrEmpty(pass))
        {
            pass = Encrypt.MiscClass.decode(pass); pass = SecurityEncryption.DESEncrypt(pass);
            string synchronous = " exec JKC_PRO_Synchronous '" + userID + "',1,'" + pass + "','" + table + "' ";
            //System.IO.File.AppendAllText("C:\\yujiajun.log", synchronous, Encoding.Default);
            sh.PExcute(synchronous);
        }

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
                string callbackfun = Request["callbackfun"];
                Response.Write(callbackfun + "({ret:\"0\"})");
                Response.End();
            }
            #endregion
            #region 管理部门
            else if (FType == "1")
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
                    if (!string.IsNullOrEmpty(fRightId))
                        GetEntLogIn(fRightId);
                    else
                    {
                        string callbackfun = Request["callbackfun"];
                        Response.Write(callbackfun + "({ret:\"-1\"})");
                        Response.End();
                    }
                }
                else
                {
                    string callbackfun = Request["callbackfun"];
                    Response.Write(callbackfun + "({ret:\"-1\"})");
                    Response.End();
                }
            }
            #endregion

        }
        else
        {
            string callbackfun = Request["callbackfun"];
            Response.Write(callbackfun + "({ret:\"0\"})");
            Response.End();
        }
    }
    private void GetEntLogIn(string UserRightID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select u.FID uFID,r.FID,u.FType,u.FName,r.FMenuRoleId,");
        sb.Append("u.FManageDeptId,u.FSystemId,r.FSystemId rFSystemId,");
        sb.Append("r.FBaseinfoId,u.FCompany,r.FRoleId,u.FBaseInfoId uFBaseInfoId ");
        sb.Append("from cf_sys_user u,CF_Sys_UserRight r ");
        sb.Append("where u.FID=r.FUserId and r.FID=@FID ");
        //Response.Write(sb.ToString());
        //Response.End();
        DataTable dt = rc.GetTable(sb.ToString(), new SqlParameter("@FID", UserRightID));
        if (dt != null && dt.Rows.Count > 0)
        {
            string FType = dt.Rows[0]["ftype"].ToString();//用户类型
            string FUserRightId = dt.Rows[0]["FID"].ToString();//CF_Sys_UserRight.FID
            string FSystemId = dt.Rows[0]["FSystemId"].ToString();
            string rFSystemId = dt.Rows[0]["rFSystemId"].ToString();
            #region

            if (FType == "2")  // 企业用户
            {
                string callbackfun = Request["callbackfun"];
                Response.Write(callbackfun + "({ret:\"0\"})");
                Response.End();
            }
            else if (FType == "1")
            {
                if (FSystemId.Trim() == "150")
                    FSystemId = "101";
                Session["FType"] = FType;//管理部门
                Session["DFSystemId"] = FSystemId;
                Session["DFName"] = dt.Rows[0]["fcompany"].ToString();
                Session["DFUserId"] = dt.Rows[0]["uFID"].ToString();
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
                //StringBuilder ss = new StringBuilder();
                //ss.Append("系统：" + sh.getSystemName(dt.Rows[0]["rFSystemId"].ToString()));
                //ss.Append("\n管理部门用户：" + dt.Rows[0]["FName"].ToString());
                //ss.Append("\nUserrightID：" + dt.Rows[0]["FID"].ToString());
                //ss.Append("\n时间：" + DateTime.Now);
                //ss.Append("\n事件：管理部门登陆到系统");
                //DataLog.Write(LogType.EntLogin, LogSort.Safety, "管理部门登陆到系统,来源：直接登录", ss.ToString(), dt.Rows[0]["FID"].ToString());
                string callbackfun = Request["callbackfun"];
                //Response.Headers.Add("P3P", "CP=CAO PSA OUR");//iframe访问时为IE浏览器提供个人隐私保护策略 保证cookie不丢失 本地测试需注释该代码
                Response.Write(callbackfun + "({ret:\"1\"})");
                Response.End();
            }
            else
            {
                string callbackfun = Request["callbackfun"];
                Response.Write(callbackfun + "({ret:\"-1\"})");
                Response.End();
            }
            #endregion
        }
        else
        {
            string callbackfun = Request["callbackfun"];
            Response.Write(callbackfun + "({ret:\"0\"})");
            Response.End();
        }
    }
    private string UserId {
        get {
            return Request.QueryString["userId"];
        }
    }
}