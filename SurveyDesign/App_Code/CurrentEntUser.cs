using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ProjectData;
using Tools;
public class CurrentEntUser
{
    /// <summary>
    /// 管理员FId
    /// </summary>
    public static string EntUserId
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["EntUserId"]);
        }
        set
        {
            HttpContext.Current.Session["EntUserId"] = value;
        }
    }
    /// <summary>
    /// 用户User.FId
    /// </summary>
    public static string UserId
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["FUserId"]);
        }
        set
        {
            HttpContext.Current.Session["FUserId"] = value;
        }
    }

    /// <summary>
    /// baseinfoId
    /// </summary>
    public static string EntId
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["FBaseinfoId"]);
        }
        set
        {
            HttpContext.Current.Session["FBaseinfoId"] = value;
        }
    }

    /// <summary>
    /// 用户UserRight.FSystemId
    /// </summary>
    public static string SystemId
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["FSystemId"]);
        }
        set
        {
            HttpContext.Current.Session["FSystemId"] = value;
        }
    }
    /// <summary>
    /// 用户UserRight.FSystemId
    /// </summary>
    public static string URSystemId
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["FURSystemId"]);
        }
        set
        {
            HttpContext.Current.Session["FURSystemId"] = value;
        }
    }
    public static string UserRightID
    {
        get
        {
            ProjectDB projectDB = new ProjectDB();
            return projectDB.CF_Sys_UserRight.Where(u => u.FUserId == UserId).Select(u => u.FId).FirstOrDefault();
        }
    }

    /// <summary>
    /// 主管部门
    /// </summary>
    public static int UpDeptId
    {
        get
        {
            ProjectDB projectDB = new ProjectDB();
            int UpDeptID = projectDB.CF_Ent_BaseInfo.Where(u => u.FId == EntId).Select(u => u.FUpDeptId.Value).FirstOrDefault();
            return UpDeptID;
        }
    }
    /// <summary>
    /// 企业名称 
    /// </summary>
    public static string EntName
    {
        get
        {
            ProjectDB projectDB = new ProjectDB();
            string Name = projectDB.CF_Ent_BaseInfo.Where(u => u.FId == EntId).Select(u => u.FName).FirstOrDefault();
            return Name;
        }
    }

    public static string FHUid
    {
        get
        {
            return EConvert.ToString(HttpContext.Current.Session["FHUserid"]);
        }
        set
        {
            HttpContext.Current.Session["FHUserid"] = value;
        }
    }
}

