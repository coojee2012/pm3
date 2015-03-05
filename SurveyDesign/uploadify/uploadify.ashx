<%@ WebHandler Language="C#" Class="uploadify" %>

using System;
using System.Web;
using System.IO;

public class uploadify : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        context.Request.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        var files = context.Request.Files;
        if (files.Count > 0)
        {
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string fileName = file.FileName.Replace("|"," ");
                int fileSize = file.ContentLength;
                string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileName);
                string path = CurrentTimeDirectory + newFileName;
                file.SaveAs(context.Server.MapPath(path));
                context.Response.Write(fileName + "|" + fileSize.ToString() + "|" + path);
                context.Response.End();
            }
        }
    }
    private string CurrentTimeDirectory
    {
        get
        {
            string filePath = "~/upload/YSLZ/" + DateTime.Now.ToString("yyyy-MM") + "/";
            string directoryPath =HttpContext.Current.Server.MapPath(filePath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            return filePath;
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}