using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;


public class SecurityEncryption
{
    /// <summary>
    /// MD5加密算法
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="code">加密方式,16或32</param>
    /// <returns></returns>
    public static string MD5(string str, int code)
    {
        if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper().Substring(8, 16);
        }
        else//32位加密 
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToUpper();
        }
    }

    public static string DesEncrypt(string strText, string strEncrKey)
    {
        byte[] byKey = null;
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, strEncrKey.Length));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());


        }
        catch (System.Exception error)
        {
            Debug.WriteLine(error.Message);
        }
        return "";
    }


    public static string DesDecrypt(string strText, string sDecrKey)
    {

        byte[] byKey = null;
        byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        try
        {
            strText = strText.Replace(" ", "+").Replace("%20", "+");
            byte[] inputByteArray = new Byte[strText.Length];
            byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }
        catch (System.Exception error)
        {
            Debug.WriteLine(error.Message);
        }
        return "";
    }


    /// <summary>
    /// 将c# DateTime时间格式转换为Unix时间戳格式
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns>double</returns>
    public static int ConvertDateTimeInt(System.DateTime time)
    {
        int intResult = 0;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        intResult = (int)(time - startTime).TotalSeconds;
        return intResult;
    }

    public static DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }
    private static byte[] key = ASCIIEncoding.ASCII.GetBytes("Ceeyiuio");
    private static byte[] iv = ASCIIEncoding.ASCII.GetBytes("liaoning");
    /// <summary>
    /// DES加密。
    /// </summary>
    /// <param name="inputString">输入字符串。</param>
    /// <returns>加密后的字符串。</returns>
    public static string DESEncrypt(string inputString)
    {
        MemoryStream ms = null;
        CryptoStream cs = null;
        StreamWriter sw = null;







        DESCryptoServiceProvider des = new DESCryptoServiceProvider();



        try
        {
            ms = new MemoryStream();
            cs = new CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            sw = new StreamWriter(cs);
            sw.Write(inputString);
            sw.Flush();

            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);


        }
        finally
        {
            if (sw != null) sw.Close();
            if (cs != null) cs.Close();
            if (ms != null) ms.Close();
        }

    }

    /// <summary>
    /// DES解密。
    /// </summary>
    /// <param name="inputString">输入字符串。</param>
    /// <returns>解密后的字符串。</returns>
    public static string DESDecrypt(string inputString)
    {
        MemoryStream ms = null;
        CryptoStream cs = null;
        StreamReader sr = null;

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        try
        {
            ms = new MemoryStream(Convert.FromBase64String(inputString));
            cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        catch
        {
            return string.Empty;
        }
        finally
        {
            try
            {
                if (sr != null) sr.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
            catch
            {
            }
        }
    }
}
