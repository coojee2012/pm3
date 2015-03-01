using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Text;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.Common;
using System.Data.SqlClient;
using ProjectData;
using ProjectBLL;

public partial class ApproveWeb_Main_LockCheck : System.Web.UI.Page
{
    OA oa = new OA();
    Share sh = new Share();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            EmpLogin();
        }
    }


    private void EmpLogin()
    {
        string EmpId = "";
        string key = Request.QueryString["key"];
        string[] strArray = SecurityEncryption.DesDecrypt(key, "32165498").Split('|');
        if (strArray.Length == 2)
        {
            if (EConvert.ToInt(strArray[1]) > SecurityEncryption.ConvertDateTimeInt(DateTime.Now))
                EmpId = strArray[0];
            else
            {
                Response.Clear();
                Response.Write("链接已失效");
                Response.End();
            }
        }

        var v = (from e in db.CF_Emp_BaseInfo
                 join b in db.CF_Ent_BaseInfo on e.FBaseInfoID equals b.FId
                 join r in db.CF_Sys_UserRight on b.FId equals r.FBaseinfoID
                 where e.FId == EmpId && (
                 r.FSystemId == 1451
                 || r.FSystemId == 1261
                 || r.FSystemId == 1553
                 || r.FSystemId == 1554
                 )
                 select new
                 {
                     e.FName,
                     r.FSystemId,
                     e.FId,
                 }).FirstOrDefault();

        if (v != null)
        {
            //登陆日志
            StringBuilder ss = new StringBuilder();
            string s = "人员登陆到系统";
            ss.Append("系统：" + "人员系统");
            ss.Append("\n姓名：" + v.FName);
            ss.Append("\nCF_Emp_BaseInfo.FID：" + v.FId);
            ss.Append("\n时间：" + DateTime.Now);
            if (Request.QueryString["admin"] == "1")
                s = "管理员后台登陆人员用户";
            ss.Append("\n事件：" + s);
            DataLog.Write(LogType.EntLogin, LogSort.Safety, s, ss.ToString(), v.FId);

            //清除掉企业session
            CurrentEntUser.EntId = null;
            //赋上session 
            Session["FUserId"] = CurrentEmpUser.EmpId = v.FId;
            //进入系统
            string url = getURL(v.FSystemId.ToString());
            if (!string.IsNullOrEmpty(url))
                this.Response.Redirect(url);
            else
            {
                Response.Clear();
                Response.Write("链接错误！");
                Response.End();
            }
        }
        else
        {
            Response.Clear();
            Response.Write("用户不存在");
            Response.End();
        }
    }




    private string getURL(string sysId)
    {
        string str = "";
        switch (sysId)
        {
            case "1451"://审图机构人员
                str = "~/EmpKcsjSgt/main/Index.aspx";
                break;
            case "1261"://见证单位人员
                str = "~/EmpJZDW/main/Index.aspx";
                break;
            case "1553"://设计单位人员
                str = "~/EmpSJDW/main/Index.aspx";
                break;
            case "1554"://勘察单位人员
                str = "~/EmpKCDW/main/Index.aspx";
                break;
            default:
                str = "";
                break;
        }

        return str;
    }


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
