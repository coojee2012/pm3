using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

/// <summary>
/// WebHelper 的摘要说明
/// </summary>
public class WebHelper
{
	public WebHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static void FileDownload(string FullFileName,string fileName)
    {
        FileInfo DownloadFile = new FileInfo(FullFileName);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = false;
        HttpContext.Current.Response.ContentType = "application/octet-stream ";
        HttpContext.Current.Response.AppendHeader("Content-Disposition ", "attachment;filename= " + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.AppendHeader("Content-Length ", DownloadFile.Length.ToString());
        HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    public static void DownLoad(string filePath,string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = Guid.NewGuid().ToString();
        }
        //以字符流的形式下载文件 
        FileStream fs = new FileStream(filePath, FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        //通知浏览器下载文件而不是打开 
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.BinaryWrite(bytes);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End(); 
    }
    public static void SetControlEnabled(Control control)
    {
        if (control is TextBox)
        {
            TextBox box = (TextBox)control;
            box.Enabled = false;
        }
        else if (control is DropDownList)
        {
            DropDownList ddl = (DropDownList)control;
            ddl.Enabled = false;
        }
        else if (control is Button)
        {
            Button btn = (Button)control;
            btn.Enabled = false;
        }
        else if (control is CheckBox)
        {
            CheckBox box = (CheckBox)control;
            box.Enabled = false;
        }
    }
    public static void SetCookie(string key, string value)
    {
        HttpCookie cookie = new HttpCookie(key, value);
        HttpContext.Current.Response.Cookies.Add(cookie);
    }
    public static string GetCookie(string key)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
        if (cookie == null)
            return string.Empty;
        return cookie.Value;
    }
    public static string XZSP_DataBase {
        get {
            return ConfigurationManager.AppSettings["XZSP_DataBase"];
        }
    }
}