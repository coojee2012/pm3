using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;
using Approve.RuleBase;
using Approve.RuleCenter;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Web.UI;
using System.Web.Handlers;
using System.Security.Cryptography;
using Approve.EntityBase;
using System.Xml.Serialization;
using System.Linq;

//namespace Approve.Common
//{
public class ComFunction
{
    static Hashtable hashNumber = new Hashtable();

    public ComFunction()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static bool ValidateSession(Page page)
    {
        if (page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(page.Request.Form["ValidateSession"]))
            {
                if (page.Request.Form["ValidateSession"] != page.Session["FBaseId"].ToString())
                {
                    page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), "", "<script type='text/javascript'>alert('Session错乱不能进行任何操作')</script>");
                    page.Response.Write("<span style='color:red'>Session错乱不能进行任何操作！请关闭所有已打开的网页，然后重新登录。</span>");
                    page.Response.End();
                    return false;
                }
            }
        }
        return true;
    }


    //得到webconfig中默认省份
    public static string GetDefaultDept()
    {
        string fDeptNumber = System.Configuration.ConfigurationSettings.AppSettings["DefaultDept"].ToString();
        if (string.IsNullOrEmpty(fDeptNumber))
            fDeptNumber = "21";
        return fDeptNumber;
    }
    /// <summary>
    /// 查询审核、菜单角色
    /// 根据企业类型
    /// </summary>
    /// <param name="fsystemId"></param>
    /// <returns></returns>
    public static string GetRoleId(string fsystemId)
    {
        string frole_menu = string.Empty;
        if (string.IsNullOrEmpty(fsystemId))
            return string.Empty;
        switch (fsystemId)
        {
            case "101"://建筑施工
                frole_menu = "601";
                break;
            case "120"://招标代理
                frole_menu = "610";
                break;
            case "125"://工程监理
                frole_menu = "620";
                break;
            case "130"://房地产
                frole_menu = "630";
                break;
            case "135"://园林
                frole_menu = "635";
                break;
            case "140"://外来勘察
                frole_menu = "640";
                break;
            case "145"://施工图
                frole_menu = "645";
                break;
            case "150"://安全生产
                frole_menu = "651";
                break;
            case "155"://勘察设计
                frole_menu = "650";
                break;
            case "165"://建造师
                frole_menu = "652";
                break;
            case "175"://质量检测
                frole_menu = "655";
                break;
            case "180"://外来施工
                frole_menu = "660";
                break;
            case "182"://外来监理
                frole_menu = "665";
                break;
            case "185"://造价咨询
                frole_menu = "680";
                break;
            case "186"://房产估价
                frole_menu = "675";
                break;
            case "187"://物业服务
                frole_menu = "670";
                break;
            case "196"://设计施工一体化
                frole_menu = "690";
                break;
            case "199"://建筑材料和备案管理
                frole_menu = "695";
                break;
            case "202"://规划编制
                frole_menu = "683";
                break;
            case "203"://规划编制
                frole_menu = "684";
                break;
            case "183"://外来园林
                frole_menu = "675";
                break;
            case "220"://三类人员企业
                frole_menu = "701";
                break;
            default:
                frole_menu = fsystemId;
                break;

        }
        return frole_menu;
    }
    public static string GetExCounty(string fnumber)
    {
        RCenter rc = new RCenter();
        StringBuilder sb = new StringBuilder();
        sb.Append("select fnumber from CF_Sys_ManageDept where fnumber like '" + fnumber + "%' and FIsTown=1 and fnumber<>'" + fnumber + "' ");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            return string.Join(",", dt.AsEnumerable().Select(t => "'" + EConvert.ToString(t["fnumber"]) + "'").ToArray());
        }
        return "''";
    }
    public static string GetValueByName(string sName)
    {
        string sValue = System.Configuration.ConfigurationSettings.AppSettings[sName];
        return sValue;
    }
    public static string GetAppDeptName(object o, object FEmpId)
    {
        RCenter rc = new RCenter();
        string sql = @"select FName DeptName from  CF_App_ProcessInstance p
            inner join CF_Sys_ManageDept d on d.FNumber=FCurStepID 
            where FLinkId='" + o + "' and FEmpId='" + FEmpId + "'";
        return rc.GetSignValue(sql);
    }
    /// <summary>
    /// 是否开始审核
    /// </summary>
    /// <param name="FLinkId"></param>
    /// <param name="fEmpId"></param>
    /// <returns></returns>
    public static bool isBeginApp(string FLinkId, string fEmpId)
    {
        RCenter rc = new RCenter();
        StringBuilder sb = new StringBuilder();
        sb.Append("select count(*) from CF_App_ProcessInstance ep,CF_App_ProcessRecord er where ");
        sb.Append("ep.fid=er.FProcessInstanceID and (er.fmeasure<>0 or ep.fstate=6) and ");
        sb.Append("ep.flinkid='" + FLinkId + "' and FEmpId='" + fEmpId + "'");
        int iCount = rc.GetSQLCount(sb.ToString());
        return iCount > 0;
    }
    public static string GetSystemConfig(string sName)
    {
        if (hashNumber[sName] != null)
        {
            return hashNumber[sName].ToString();
        }
        string fWebConfigPath = HttpContext.Current.Server.MapPath(GetValueByName("DicConfigPath"));
        if (fWebConfigPath == null || fWebConfigPath == "")
        {
            return "";
        }
        XmlDocument xd = new XmlDocument();
        xd.Load(fWebConfigPath);
        XmlNodeList xl = xd.GetElementsByTagName(sName);
        if (xl == null || xl.Count == 0)
        {
            return "";
        }
        else
        {
            if (hashNumber.Contains(sName))//包含
            {
                hashNumber[sName] = xl[0].InnerText;
            }
            else
                hashNumber.Add(sName, xl[0].InnerText);
        }
        return hashNumber[sName].ToString();
    }

    public static void ClearHashNumber()
    {
        hashNumber.Clear();
    }


    public static string FileServer(string path)
    {
        RCenter rc = new RCenter();
        string FileServerPath = rc.GetSysObjectContent("FileServerPath");
        if (string.IsNullOrEmpty(FileServerPath))
        {
            FileServerPath = System.Configuration.ConfigurationSettings.AppSettings["FileServerPath"];
        }
        string fUrl = string.Format("{0}{1}{2}", FileServerPath,
           Path.GetDirectoryName(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Substring(1)).Replace("\\", "/") + "/",
       path.Replace(
       Path.GetFileNameWithoutExtension(path),
       HttpUtility.UrlEncode(Path.GetFileNameWithoutExtension(path)).Replace("+", "%20"))
       );
        if (string.IsNullOrEmpty(path))//显示图片
        {
            fUrl = "../../common/UploadFrame.aspx?furl=" + fUrl;
        }
        return fUrl;
    }
    /// <summary>
    /// 判断网络地址是否存在
    /// </summary>
    /// <param name="URL"></param>
    /// <returns></returns>
    public static string TestImgUrl(string URL)
    {
        try
        {
            System.Net.WebRequest s = System.Net.WebRequest.Create(URL);
            s.Timeout = 10000;
            System.Net.WebResponse a = s.GetResponse();
            return URL;
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string SID
    {
        get
        {
            string sid = SecurityEncryption.DesEncrypt(SecurityEncryption.ConvertDateTimeInt(DateTime.Now).ToString(), "1234abcd");
            HttpContext.Current.Session["SID"] = HttpUtility.UrlEncode(sid, Encoding.GetEncoding("gbk"));
            return EConvert.ToString(HttpContext.Current.Session["SID"]);
        }
    }
    /// <summary>
    /// 15位身份证转换为18位的算法
    /// </summary>
    /// <param name="perIDSrc"></param>
    /// <returns></returns>
    public static string per15To18(string perIDSrc)
    {
        int iS = 0;

        //加权因子常数 
        int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        //校验码常数 
        string LastCode = "10X98765432";
        //新身份证号 
        string perIDNew;

        perIDNew = perIDSrc.Substring(0, 6);
        //填在第6位及第7位上填上‘1’，‘9’两个数字 
        perIDNew += "19";

        perIDNew += perIDSrc.Substring(6, 9);

        //进行加权求和 
        try
        {
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }
        }
        catch
        {
            return "输入格式不正确";
        }
        //取模运算，得到模值 
        int iY = iS % 11;
        //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
        perIDNew += LastCode.Substring(iY, 1);

        return perIDNew;
    }

    /// <summary>
    /// 18位身份证转换为15的算法
    /// </summary>
    /// <param name="perIDSrc"></param>
    /// <returns></returns>
    public static string per18To15(string perIDSrc)
    {
        string perIDNew;

        perIDNew = perIDSrc.Substring(0, 6);
        perIDNew += perIDSrc.Substring(8, 9);
        return perIDNew;

    }
    /// <summary>
    /// 得到webconfig中默认市级
    /// </summary>
    /// <returns></returns>
    public static string GetDefaultCityDept()
    {
        string fDeptNumber = System.Configuration.ConfigurationSettings.AppSettings["DefaultCity"].ToString();
        return fDeptNumber;
    }

    #region 加/解密DataSet

    #endregion
    #region DropDownList控件 实现树形结构
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">空格(隔断符号)</param>
    /// <param name="DirId">父路径ID</param>
    /// <param name="dt">返回的DataTable</param>
    /// <param name="deep">树形的深度</param>
    /// <param name="ddl">DropDownList控件</param>
    //public static void addTreeDropDownList(string str,int DirId,DataTable dt,int deep,DropDownList ddl)
    //{
    //    DataRow[] rowlist = dt.Select("FParentId='"+DirId+"'");
    //    foreach( DataRow row in rowlist)
    //    {
    //        string strPading = "";
    //        for (int j = 0; j < deep; j++)
    //        {
    //            strPading += "　";
    //        }
    //        ListItem li = new ListItem(strPading+"--"+row["FName"].ToString());
    //        ddl.Items.Add(li);
    //        addTreeDropDownList(strPading,Convert.ToInt32(row["FParentId"]),dt,deep+1,ddl );
    //    }
    //}

    #endregion
}

//}
