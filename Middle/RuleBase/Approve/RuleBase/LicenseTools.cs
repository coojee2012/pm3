namespace Approve.RuleBase
{
    using Approve.EntityBase;
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public class LicenseTools
    {
        public int ConvertDateTimeInt(DateTime time)
        {
            DateTime time2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            TimeSpan span = (TimeSpan) (time - time2);
            return (int) span.TotalSeconds;
        }

        public string DesDecrypt(string strText, string sDecrKey)
        {
            return this.DesDecrypt(strText, sDecrKey, new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef });
        }

        public string DesDecrypt(string strText, string sDecrKey, byte[] IV)
        {
            byte[] rgbKey = null;
            byte[] buffer = new byte[strText.Length];
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                buffer = Convert.FromBase64String(strText);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, IV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                return encoding.GetString(stream.ToArray());
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return "";
        }

        public string DesEncrypt(string strText, string strEncrKey)
        {
            byte[] rgbKey = null;
            byte[] rgbIV = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            try
            {
                rgbKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, strEncrKey.Length));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.UTF8.GetBytes(strText);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return "";
        }

        public string GetConnectionString(string key)
        {
            string message = "";
            if (!((ConfigurationManager.ConnectionStrings[key] == null) || string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[key].ConnectionString)))
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[key].ConnectionString);
                
                message = builder.ToString();
                System.Diagnostics.Debug.WriteLine(message);
                return message;
            }
            return ConfigurationManager.AppSettings[key];
        }

        public string GetLicenseEndTime()
        {
            LicenseResult result = this.GetResult();
            if (result.IsPass)
            {
                return result.EndTime.ToString();
            }
            return result.strMessage;
        }

        public string GetProduct()
        {
            string product = "";
            Assembly assembly = Assembly.GetAssembly(this.GetType("Ass"));
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
                if (customAttributes.Length != 1)
                {
                    return product;
                }
                AssemblyProductAttribute attribute = customAttributes[0] as AssemblyProductAttribute;
                if (attribute != null)
                {
                    product = attribute.Product;
                }
            }
            return product;
        }

        public LicenseResult GetResult()
        {
            LicenseResult result = new LicenseResult() { IsPass = true };
            return result;
            string strText = "";
            TextReader reader = null;
            try
            {
                reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Licenses.ini"));
                strText = reader.ReadToEnd();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("读取注册码失败");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            string product = this.GetProduct();
            if ((strText != "") && (product != ""))
            {
                string[] strArray = this.DesDecrypt(strText, product.PadRight(8, '0').Substring(0, 8)).Split(new char[] { '|' });
                if (strArray.Length == 2)
                {
                    result.EndTime = this.GetTime(strArray[0]);
                    if (EConvert.ToInt(strArray[0]) > this.ConvertDateTimeInt(DateTime.Now))
                    {
                        if ((strArray[1] == HardDiskVal.HDVal()) || (EConvert.ToString(this.GetConnectionString("dbCenter")).IndexOf(strArray[1]) > -1))
                        {
                            result.IsPass = true;
                            return result;
                        }
                        result.strMessage = "许可证号无效";
                        return result;
                    }
                    result.strMessage = "许可证过期了";
                    return result;
                }
                result.strMessage = "许可证号无效";
                return result;
            }
            result.strMessage = "未授权的版本";
            return result;
        }

        public DateTime GetTime(string timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            long ticks = long.Parse(timeStamp + "0000000");
            TimeSpan span = new TimeSpan(ticks);
            return time.Add(span);
        }

        public Type GetType(string p_TypeFullName)
        {
            Type type = Type.GetType(p_TypeFullName);
            if (type != null)
            {
                return type;
            }
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i != assemblies.Length; i++)
            {
                type = assemblies[i].GetType(p_TypeFullName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
}

