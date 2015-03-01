using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ProjectData;
using Tools;
using System.ComponentModel;


namespace ProjectBLL
{
    public class DataLog
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="LogType">日志类别LogType</param>
        /// <param name="Operation">日志类型LogSort</param>
        /// <param name="Title">日志标题</param>
        /// <param name="Description">日志描述</param>
        public static void Write(LogType lt, LogSort Operation, string Title, string Description)//写日志
        {

            try
            {


                ProjectDB pdb = new ProjectDB();
                CF_Sys_Log log = new CF_Sys_Log();


                string Url = "";//当前url
                string IP = "";//当前IP
                string SystemType = "";//系统类型
                string FID = Guid.NewGuid().ToString();//GUID
                string BrowserType = "";//浏览器类型
                string Operator = "";//操作者
                string Desc = "";
                string ServerName = "";//服务器名
                if (HttpContext.Current != null)
                {
                    HttpRequest req = HttpContext.Current.Request;
                    if (req != null)
                    {

                        if (req.Url != null)
                            Url = req.Url.ToString();//绝对url

                        IP = req.UserHostAddress;//访问者IP
                        SystemType = GetOSNameByUserAgent(req.UserAgent);//系统信息


                        if (HttpContext.Current.Session != null)
                        {
                            if (CurrentDeptUser.DeptUserId != "")
                            {
                                Operator =CurrentDeptUser.DeptUserId;
                                Desc += "CurrentDeptUser.DeptUserId：" + Operator + ",";
                            }
                            if (CurrentAdminUser.AdminUserId != "")
                            {
                                Operator = CurrentAdminUser.AdminUserId;
                                Desc += "CurrentAdminUser.AdminUserId：" + Operator + ",";
                            }
                            Desc ="\n "+Desc;
                        }
                        if (req.Browser!=null)
                        BrowserType = req.Browser.Browser + req.Browser.Version;//浏览器类型

                    }
                    if(HttpContext.Current.Server!=null)
                    {
                        ServerName=HttpContext.Current.Server.MachineName;
                    }

                }
                Desc = Description + Desc;
                log.FServerName=ServerName;
                log.FBrowserType = BrowserType;
                log.FDirectory = Url;
                log.FID = FID;
                log.FIPAddress = IP;
                log.FLogTime = DateTime.Now;
                log.FLogType = (int)lt;
                log.FOperation = (int)Operation;
                log.FOperator = Operator;
                log.FSystemType = SystemType;
                log.FTitle = Title;
                log.FDescription = Desc;

                pdb.CF_Sys_Log.InsertOnSubmit(log);

                pdb.SubmitChanges();


            }
            catch (Exception e)
            { 
            
            }

        }






        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        public static string GetOSNameByUserAgent(string userAgent)
        {
            string osVersion = "未知";

            if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }




    }



    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        [Description("信息")]
        Info = 1,
        [Description("错误")]
        Error = 2,
        [Description("警告")]
        Warning = 3
    }

    /// <summary>
    /// 日志类别
    /// </summary>
    public enum LogSort
    {
        [Description("系统")]
        System = 1,
        [Description("安全")]
        Safety = 2,
        [Description("应用")]
        Apply = 3,
        [Description("操作")]
        Operation = 4
    }

}
