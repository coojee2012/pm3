using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_DownLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(fileDownPath))
        {
            string path = Server.MapPath(fileDownPath);
            if (System.IO.File.Exists(path))
               WebHelper.DownLoad(Server.MapPath(fileDownPath), fileName);
            else
            {
                Response.Write("文件不存在或已删除");
                Response.End();
            }
        }
        else
        {
            Response.Write("传递参数错误");
            Response.End();
        }
        
    }
    private string fileDownPath
    {
        get
        {
            return Request.QueryString["filePath"];
        }
    }
    private string fileName
    {
        get
        {
            return Request.QueryString["fileName"];
        }
    }
}