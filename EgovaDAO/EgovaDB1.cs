using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Tools;

namespace EgovaDAO
{
    public class EgovaDB1 : DB1DataContext
    {
        public EgovaDB1()
            : base(CreateConnection())
        {
            //记录日志（每天一个文件，记录所有更改sql,日志会存在第一个盘的log文件夹下）
            //if (ConfigurationManager.AppSettings["IsTraceLinqLog"].ToString().ToLower() == "true")
            //{
            //    string directory = Path.Combine(GetPath(), "log");
            //    Directory.CreateDirectory(directory);
            //    DateTime now = DateTime.Now;
            //    string logFile = Path.Combine(directory, "log" + now.Year + now.Month + now.Day + ".txt");
            //    using (StreamWriter sw = File.AppendText(logFile))
            //    {
            //        sw.WriteLine("发生时间:{0}", DateTime.Now.ToString());
            //        sw.WriteLine("日志内容为:");
            //        this.Log = sw;
            //        sw.WriteLine("--------------------------------------------------------------");
            //    }
            //}
        }
        /// <summary>
        /// try to close streamwriter
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //if(sw != null)
            //{
            //    sw.Flush();
            //}
           // sw.Flush();

        }
       /// <summary>
       /// 添加日志记录
       /// </summary>
       /// <param name="failureMode"></param>
        //public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        //{
        //    // 记录日志（每天一个文件，记录所有更改sql,日志会存在第一个盘的log文件夹下）
        //    try
        //    {
        //        object obj = ConfigurationManager.AppSettings["IsTraceLinqLog"];
        //        if (obj != null && obj.ToString().ToLower() == "true")
        //        {
        //            //   string directory = Path.Combine(Directory.GetLogicalDrives().First(), "log");
        //            string directory = Path.Combine(GetPath(), "log");
        //            Directory.CreateDirectory(directory);
        //            DateTime now = DateTime.Now;
        //            string logFile = Path.Combine(directory, "log" + now.Year + now.Month + now.Day + "linq.txt");
        //            using (StreamWriter w = File.AppendText(logFile))
        //            {
        //                w.WriteLine("发生时间:{0}", DateTime.Now.ToString());
        //                w.WriteLine("日志内容为:");
        //                this.Log = w;
        //                try
        //                {
        //                    base.SubmitChanges(failureMode);
        //                }
        //                catch (Exception e)
        //                {
        //                    w.WriteLine("发生错误，错误信息：");
        //                    w.WriteLine(e.Message);
        //                    throw e;
        //                }
        //                w.WriteLine("--------------------------------------------------------------");
        //            }
        //        }
        //        else
        //        {
        //            base.SubmitChanges(failureMode);
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }            
        //}
        private static string CreateConnection()
        {
            //LicenseTools lt = new LicenseTools();
            //string access = lt.GetConnectionString("dbCenter");
            //return access;
            string message = "";
            string key = "JST_XZSPBaseInfo";
            if (!((ConfigurationManager.ConnectionStrings[key] == null) || string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[key].ConnectionString)))
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[key].ConnectionString);

                message = builder.ToString();
                System.Diagnostics.Debug.WriteLine(message);
                return message;
            }
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 获取Assembly的运行路径
        /// </summary>
        ///<returns></returns>
        private string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;

            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是file:// 的长度

            string[] arrSection = _CodeBase.Split(new char[] { '/' });

            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "/";
            }

            return _FolderPath;
        }
        /// <summary>
        /// 获取这个动态链接库的位置
        /// </summary>
        /// <returns></returns>
        private static string GetPath()
        {
            string str = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            int start = 8;// 去除file:///
            int end = str.LastIndexOf('/');// 去除文件名xxx.dll及文件名前的/
            str = str.Substring(start, end - start);
            return str;
        }

    }
}
