using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using Approve.EntityBase;
using System.Collections;
using System.Text;
using System.Data;
using Approve.RuleCenter;
using System.Web;
using ProjectData;
using System.Linq;

public partial class Share_Main_LoginAll : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (IsPass())
            {
                this.liC_TechSupport.Text = ComFunction.GetValueByName("TechSupport");//技术支持
                this.liC_webtel.Text = ComFunction.GetValueByName("WebTel");//电话
                this.liC_Developer.Text = ComFunction.GetValueByName("Developer");//开发单位
                C_FPwd.TextMode = TextBoxMode.Password;
                C_FPwd.Text = string.Empty;
                C_FName.Text = string.Empty;
            }
            else
            {
                Response.Clear();
                Response.Write("没有权限访问!!!");
                Response.End();
            }
        }
    }

    //登陆
    private void Login()
    {

        pageTool tool = new pageTool(this.Page);
        this.ClientScript.RegisterStartupScript(GetType(), "jj", "<script>show();</script>");
        if (C_FType.SelectedValue == "2")
        {
            if (!vilideCode.IsPass(YZM.Text.Trim().ToLower()))
            {
                tool.showMessage("验证码输入错误！");
            }
            else
            {
                string FName = C_FName.Text; //用户名
                //加密密码
                string FPassWord = C_FPwd.Text; //密码
                FPassWord = SecurityEncryption.DESEncrypt(FPassWord);

                //增加同步用户名密码 ljr 2014-11-25
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
                sb.Append("select FID,FName  ");
                sb.Append("from CF_Sys_User ");
                sb.Append("where fisdeleted=0 and FName=@FName and FPassWord=@FPassWord and FType in(6,0)");//in(6,0)系统管理员和超级管理员
                DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
                if (dt != null && dt.Rows.Count > 0)
                {
                    //此项目平台同步到JKCWFDB_WORK_NJS  ljr 2015-1-17
                    if (string.IsNullOrEmpty(pass))
                    {
                        FPassWord = SecurityEncryption.DESDecrypt(FPassWord); FPassWord = Encrypt.MiscClass.encode(FPassWord);
                        string sql = " exec JKC_PRO_SynchronousNJS  '" + dt.Rows[0]["FId"].ToString() + "','" + pass + "' ";
                        sh.PExcute(sql);
                    }

                    string UserID = dt.Rows[0]["FID"].ToString();

                    Session["CreateID"] = UserID;
                    Session["CreateName"] = dt.Rows[0]["FName"].ToString();
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        DateTime time = DateTime.Now.AddHours(1);
                        string key = SecurityEncryption.DesEncrypt(UserID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
                        //记录登陆日记
                        DataLog.Write(LogType.Info, LogSort.Safety, "帐号登陆后台", string.Format("用户[{0}]于[{1}]登陆四川省勘察设计科技信息系统", FName + "(" + UserID + ")", DateTime.Now.ToString()));

                        this.Response.Redirect("LockCheckAll.aspx?UserID=" + HttpUtility.UrlEncode(key, Encoding.UTF8));
                    }
                }
                else
                {
                    tool.showMessage("用户名或密码错误");
                    return;
                }
            }
        }
        else
        {
            string FLockNumber = C_FLockNumber.Value; //加密锁硬件编号

            SortedList sl = new SortedList();
            sl.Add("FLockNumber", FLockNumber);

            StringBuilder sb = new StringBuilder();
            sb.Append("select FID ");
            sb.Append("from CF_Sys_User ");
            sb.Append("where fisdeleted=0 and FLockNumber=@FLockNumber and FType in(6,0)");//in(6,0)系统管理员和超级管理员
            DataTable dt = sh.GetTable(sb.ToString(), sh.ConvertParameters(sl));
            if (dt != null && dt.Rows.Count > 0)
            {
                string UserID = dt.Rows[0]["FID"].ToString();
                if (!string.IsNullOrEmpty(UserID))
                {
                    //增加同步用户名密码 ljr 2014-11-26
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


                    DateTime time = DateTime.Now.AddHours(1);
                    string key = SecurityEncryption.DesEncrypt(UserID + "|" + SecurityEncryption.ConvertDateTimeInt(time), "32165498");
                    //记录登陆日记
                    DataLog.Write(LogType.Info, LogSort.Safety, "加密锁登陆后台", string.Format("用户[{0}]于[{1}]登陆四川省勘察设计科技信息系统", FLockNumber + "(" + UserID + ")", DateTime.Now.ToString()));

                    this.Response.Redirect("LockCheckAll.aspx?UserID=" + HttpUtility.UrlEncode(key, Encoding.UTF8));
                }
            }
            else
            {
                tool.showMessage("加密锁不存在");
                return;
            }
        }

    }

    #region 访问权限

    /// <summary>
    /// 得到是否允许访问
    /// </summary>
    /// <returns></returns>
    private bool IsPass()
    {
        string XZ = sh.GetSysObjectContent("_sys_BLogin_IPRest");
        if (XZ == "1")
        {
            string IPstr = getIpAddr();//客户端IP
            System.Net.IPAddress IPA = System.Net.IPAddress.Parse(IPstr);

            if (IPstr != "127.0.0.1")
            {
                string BlackIP = sh.GetSysObjectContent("_sys_BLogin_BlackIPList");//黑名单
                if (!string.IsNullOrEmpty(BlackIP))
                {
                    foreach (string str in BlackIP.Split(',')) //遍历黑名单
                    {
                        if (getIPList(str, IPA))
                        {
                            return false;//IP在黑名单中，不通过
                        }
                    }
                }

                string WhiteIP = sh.GetSysObjectContent("_sys_BLogin_WhiteIPList");//白名单
                if (!string.IsNullOrEmpty(WhiteIP))
                {
                    foreach (string str in WhiteIP.Split(',')) //遍历白名单
                    {
                        if (getIPList(str, IPA))
                        {
                            return true;//IP在白名单中，通过
                        }
                    }
                    return false;//IP没在黑名单中，也没在白名单中，不通过
                }
                return true;//如果只设置了黑名单，没设置白名单；IP没在黑名单中，直接通过
            }
        }
        return true;//啥也没设置或IP为127.0.0.1（本机），直接通过
    }


    /// <summary>
    /// 将指定的4位IP地址转换成整型数字
    /// </summary>
    /// <param name="strip">4位IP地址</param>
    /// <returns></returns>
    public static uint GetUIntIP(string strip)
    {
        uint ip1 = Convert.ToUInt32(strip.Split('.')[0]);//截取IP地址第0位，并转为无符号的整数 
        uint ip2 = Convert.ToUInt32(strip.Split('.')[1]);
        uint ip3 = Convert.ToUInt32(strip.Split('.')[2]);
        uint ip4 = Convert.ToUInt32(strip.Split('.')[3]);
        return ip1 * 256 * 256 * 256 + ip2 * 256 * 256 + ip3 * 256 + ip4 - 1;//返回最终10进制结果 
    }


    /// <summary>
    /// 指定IP是否在IPstr范围内
    /// </summary>
    /// <param name="IPstr">单个IP或范围</param>
    /// <param name="IP">IP</param>
    /// <returns></returns>
    private bool getIPList(string IPstr, System.Net.IPAddress IP)
    {
        bool rv = false;
        try
        {
            if (IPstr.Contains("~"))
            {
                string[] arr = IPstr.Split('~');
                if (arr.Length == 2)
                {
                    System.Net.IPAddress IPA1 = System.Net.IPAddress.Parse(arr[0]);//起始
                    System.Net.IPAddress IPA2 = System.Net.IPAddress.Parse(arr[1]);//结束

                    if (GetUIntIP(IPA1.ToString()) <= GetUIntIP(IP.ToString()) && GetUIntIP(IP.ToString()) <= GetUIntIP(IPA2.ToString()))
                    {
                        rv = true;
                    }
                }
            }
            else
            {
                System.Net.IPAddress IPA = System.Net.IPAddress.Parse(IPstr);
                if (IP.Address == IPA.Address)
                {
                    rv = true;
                }
            }
        }
        catch (Exception ex)
        {

            if (GetIPbyDomain(IPstr, IP))
            {
                rv = true;
            }
            else
                rv = false;
        }
        return rv;
    }

    /// <summary>
    /// 从域名中判断
    /// </summary>
    /// <param name="pDomain">域名</param>
    /// <param name="IP">System.Net.IPAddress</param>
    /// <returns></returns>
    private bool GetIPbyDomain(string pDomain, System.Net.IPAddress IP)
    {
        bool rv = false;
        try
        {

            System.Net.IPHostEntry Host = new System.Net.IPHostEntry();
            Host = System.Net.Dns.Resolve(pDomain);
            System.Net.IPEndPoint IPe = new System.Net.IPEndPoint(Host.AddressList[0], 0);

            System.Net.IPAddress IPA = System.Net.IPAddress.Parse(IPe.Address.ToString());

            if (IP.Address == IPA.Address)
            {
                rv = true;
            }
        }
        catch (Exception ex)
        {
            rv = true;
        }
        return rv;
    }


    /// <summary>
    /// 得到客户端IP
    /// </summary>
    /// <returns></returns>
    public string getIpAddr()
    {
        string ip = HttpContext.Current.Request.Headers["x-forwarded-for"];
        if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
        {
            ip = HttpContext.Current.Request.Headers["Proxy-Client-IP"];
        }
        if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
        {
            ip = HttpContext.Current.Request.Headers["WL-Proxy-Client-IP"];
        }
        if (string.IsNullOrEmpty(ip) || "unknown".Equals(ip, StringComparison.InvariantCultureIgnoreCase))
        {
            ip = HttpContext.Current.Request.UserHostAddress;
        }
        return ip;
    }

    #endregion

    //登陆按钮
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Login();
    }
}
