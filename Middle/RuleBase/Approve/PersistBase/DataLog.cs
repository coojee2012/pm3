namespace Approve.PersistBase
{
    using Approve.EntityBase;
    using Approve.RuleBase;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;

    public class DataLog
    {
        public static void AddLog(IConnection Cn, string sSql, string sError, int iType)
        {
            string @operator = "";
            int num = 0;
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["FUserId"] != null)
                {
                    @operator = HttpContext.Current.Session["FUserId"].ToString();
                    num = 2;
                }
                if (HttpContext.Current.Session["DFUserId"] != null)
                {
                    @operator = HttpContext.Current.Session["DFUserId"].ToString();
                    num = 1;
                }
            }
            else
            {
                @operator = "";
                num = 3;
            }
            sError = sError + "；" + EConvert.ToString(HttpContext.Current.Request.RawUrl);
            try
            {
                if (sSql.ToLower().IndexOf("cf_sys_log") == -1)
                {
                    if (ConfigurationManager.ConnectionStrings["LogServer"] != null)
                    {
                        if (sSql.Length > 40)
                        {
                            Write("操作数据的记录：" + sSql.Substring(0, 40), sSql, @operator);
                        }
                        else
                        {
                            Write("操作数据的记录：" + sSql, sSql, @operator);
                        }
                    }
                    else
                    {
                        sSql = sSql.Replace("'", "''");
                        sError = sError.Replace("'", "''");
                        StringBuilder builder = new StringBuilder();
                        builder.Append(" insert into CF_Com_News ");
                        builder.Append(" (FId,Title,Content,errmsg,FUserId,FLogType,FUserType)");
                        builder.Append(string.Concat(new object[] { " values(newid(),'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "','", sSql, "','", sError, "','", @operator, "',", iType, ",", num, ")" }));
                        Cn.Execute(builder.ToString(), SqlResultEnum.No, new IDbDataParameter[0]);
                    }
                }
            }
            catch (Exception exception)
            {
                Cn.Dispose();
                throw exception;
            }
        }

        public static string GetOSNameByUserAgent(string userAgent)
        {
            string str = "未知";
            if (userAgent.Contains("NT 6.1"))
            {
                return "Windows 7";
            }
            if (userAgent.Contains("NT 6.0"))
            {
                return "Windows Vista/Server 2008";
            }
            if (userAgent.Contains("NT 5.2"))
            {
                return "Windows Server 2003";
            }
            if (userAgent.Contains("NT 5.1"))
            {
                return "Windows XP";
            }
            if (userAgent.Contains("NT 5"))
            {
                return "Windows 2000";
            }
            if (userAgent.Contains("NT 4"))
            {
                return "Windows NT4";
            }
            if (userAgent.Contains("Me"))
            {
                return "Windows Me";
            }
            if (userAgent.Contains("98"))
            {
                return "Windows 98";
            }
            if (userAgent.Contains("95"))
            {
                return "Windows 95";
            }
            if (userAgent.Contains("Mac"))
            {
                return "Mac";
            }
            if (userAgent.Contains("Unix"))
            {
                return "UNIX";
            }
            if (userAgent.Contains("Linux"))
            {
                return "Linux";
            }
            if (userAgent.Contains("SunOS"))
            {
                str = "SunOS";
            }
            return str;
        }

        public static void Write(string Title, string Description, string Operator)
        {
            Write(LogType.Info, LogSort.Operation, Title, Description, Operator);
        }

        public static void Write(LogType lt, LogSort Operation, string Title, string Description)
        {
            Write(lt, Operation, Title, Description, "");
        }

        public static void Write(LogType lt, LogSort Operation, string Title, string Description, string Operator)
        {
            try
            {
                string str = "";
                string userHostAddress = "";
                string oSNameByUserAgent = "";
                string str4 = Guid.NewGuid().ToString();
                string str5 = "";
                string str6 = "";
                string machineName = "";
                if (HttpContext.Current != null)
                {
                    HttpRequest request = HttpContext.Current.Request;
                    if (request != null)
                    {
                        if (request.Url != null)
                        {
                            str = request.Url.ToString();
                        }
                        userHostAddress = request.UserHostAddress;
                        oSNameByUserAgent = GetOSNameByUserAgent(request.UserAgent);
                        if (HttpContext.Current.Session != null)
                        {
                            if (EConvert.ToString(HttpContext.Current.Session["FUserID"]) != "")
                            {
                                str6 = str6 + "FUserID：" + EConvert.ToString(HttpContext.Current.Session["FUserID"]);
                            }
                            if (EConvert.ToString(HttpContext.Current.Session["DeptUserId"]) != "")
                            {
                                str6 = str6 + "CurrentDeptUser.DeptUserId：" + EConvert.ToString(HttpContext.Current.Session["DeptUserId"]);
                            }
                            if (EConvert.ToString(HttpContext.Current.Session["AdminUserId"]) != "")
                            {
                                str6 = str6 + "CurrentAdminUser.AdminUserId：" + EConvert.ToString(HttpContext.Current.Session["AdminUserId"]);
                            }
                            str6 = "\n " + str6;
                        }
                        if (request.Browser != null)
                        {
                            str5 = request.Browser.Browser + request.Browser.Version;
                        }
                    }
                    if (HttpContext.Current.Server != null)
                    {
                        machineName = HttpContext.Current.Server.MachineName;
                    }
                }
                str6 = Description + str6;
                string str8 = "INSERT INTO [CF_Sys_Log]\r\n           ([FID]\r\n           ,[FDirectory]\r\n           ,[FLogType]\r\n           ,[FTitle]\r\n           ,[FOperation]\r\n           ,[FOperator]\r\n           ,[FLogTime]\r\n           ,[FDescription]\r\n           ,[FIPAddress]\r\n           ,[FSystemType]\r\n           ,[FBrowserType]\r\n           ,[FServerName])\r\n     VALUES\r\n           (@FID\r\n           ,@FDirectory\r\n           ,@FLogType\r\n           ,@FTitle\r\n           ,@FOperation\r\n           ,@FOperator\r\n           ,@FLogTime\r\n           ,@FDescription\r\n           ,@FIPAddress\r\n           ,@FSystemType\r\n           ,@FBrowserType\r\n           ,@FServerName)";
                string connectionString = "";
                if (ConfigurationManager.ConnectionStrings["LogServer"] != null)
                {
                    connectionString = new LicenseTools().GetConnectionString("LogServer");
                }
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = ConfigurationManager.AppSettings[ConfigurationManager.AppSettings["dbType"]];
                }
                if (!string.IsNullOrEmpty(connectionString))
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = str8;
                    SqlParameter[] values = new SqlParameter[12];
                    values[0] = new SqlParameter("@FID", SqlDbType.Char);
                    values[0].Value = str4;
                    values[1] = new SqlParameter("@FDirectory", SqlDbType.VarChar);
                    values[1].Value = str;
                    values[2] = new SqlParameter("@FLogType", SqlDbType.Int);
                    values[2].Value = (int) lt;
                    values[3] = new SqlParameter("@FTitle", SqlDbType.NVarChar);
                    values[3].Value = Title;
                    values[4] = new SqlParameter("@FOperation", SqlDbType.Int);
                    values[4].Value = (int) Operation;
                    values[5] = new SqlParameter("@FOperator", SqlDbType.Char);
                    values[5].Value = Operator;
                    values[6] = new SqlParameter("@FLogTime", SqlDbType.DateTime);
                    values[6].Value = DateTime.Now;
                    values[7] = new SqlParameter("@FDescription", SqlDbType.NVarChar, -1);
                    values[7].Value = str6;
                    values[8] = new SqlParameter("@FIPAddress", SqlDbType.VarChar);
                    values[8].Value = userHostAddress;
                    values[9] = new SqlParameter("@FSystemType", SqlDbType.VarChar);
                    values[9].Value = oSNameByUserAgent;
                    values[10] = new SqlParameter("@FBrowserType", SqlDbType.VarChar);
                    values[10].Value = str5;
                    values[11] = new SqlParameter("@FServerName", SqlDbType.VarChar);
                    values[11].Value = machineName;
                    command.Parameters.AddRange(values);
                    connection.Open();
                    int num = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
            }
        }
    }
}

