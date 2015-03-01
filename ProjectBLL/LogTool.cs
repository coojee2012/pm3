using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ProjectData;


namespace ProjectBLL
{
    public class LogTool
    {

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="LogType">日志类别｛1应用 2安全 3系统｝</param>
        /// <param name="Operation">日志类型｛1信息 2安全 3警告｝</param>
        /// <param name="Operator">操作者</param>
        /// <param name="Title">日志标题</param>
        /// <param name="Description">日志描述</param>
        public static void Write(int LogType, int Operation, int Operator, string Title, string Description)//写日志
        {
            
            string FID = Guid.NewGuid().ToString();//GUID
            HttpRequest req=HttpContext.Current.Request;
            string Url = req.UrlReferrer == null ? "" : req.UrlReferrer.ToString();//绝对url
            string IP = req.UserHostAddress;//访问者IP
            string SystemType = req.UserAgent.ToString();//系统信息
            string BrowserType = req.Browser.Browser+req.Browser.Version;//浏览器类型


            ProjectDB pdb = new ProjectDB();
            

        
        }







    }
}
