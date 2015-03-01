using System;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Drawing;
using System.Security.Cryptography;
using System.Diagnostics;
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Linq;
using ProjectData;

[assembly: TagPrefix("Link", "asp")]
internal class resfinder
{

}
namespace Web
{


    /// <summary>
    ///  样式
    /// </summary>
    [DefaultProperty("Text")]
    [DisplayName("HtmlLink")]
    [Description("HtmlLink")]
    [ToolboxData("<{0}:Link runat=server></{0}:Link>")]
    //[ToolboxBitmap(typeof(resfinder), "favicon.ico")]
    public class Link : WebControl
    {
        private string href = "~/skin/blue/main.css";

        [Description("获取或设置在 System.Web.UI.HtmlControls.HtmlLink 控件中指定的链接的 URL 目标。")]
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("~/skin/blue/main.css")]
        [Localizable(true)]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        //重写父类Render生成HTML方法#region 重写父类Render生成HTML方法
        protected override void Render(HtmlTextWriter writer)
        {
            //注释父类Render的方法，因为WebControl默认会生成一个<span>标签
            //base.Render(writer);
            //调用HtmlTextArea控件的Render方法生成HTML
            //this.htmlLink.RenderControl(writer);
            //当前上下文为空则返回（在VS的ASPX设计视图页面）

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<link id='skinLink' href='" + href + "' type='text/css' rel='stylesheet' title='default' />"); //默认
            if (Context != null)
            {
                string skinName = "";

                //从数据库中得到风格名              
                ProjectDB db = new ProjectDB();
                string sysSkinName = db.getSysObjectContent("_sys_skinName");//得到后台设置的风格名称
                skinName = sysSkinName;

                //从cookie中得到本机设置的风格名
                if (Context.Request.Cookies["_SYS_QS_SKINNAME"] != null)
                {
                    string cookiesSkinName = Context.Server.HtmlEncode(Context.Request.Cookies["_SYS_QS_SKINNAME"].Value);
                    skinName = cookiesSkinName;//如果存在就替换掉
                }



                if (!string.IsNullOrEmpty(skinName))
                {
                    //如果cookies中取到的风格不存在，就直接用系统保存的
                    if (!File.Exists(Context.Server.MapPath("~/Skin/" + skinName + "/main.css")))
                        skinName = sysSkinName;

                    //整理为网格路径，以适应iss两种网站布署方式。
                    string str = "";
                    string first = "http://" + Context.Request.Headers["Host"];

                    string root = Context.Request.ApplicationPath;
                    if (root != "/")
                        root += "/";
                    str = first + root;
                    string newHref = str + "Skin/" + skinName + "/main.css";

                    //束理样式引用串 
                    sb.Remove(0, sb.Length);

                    DirectoryInfo TheFolder = new DirectoryInfo(Context.Server.MapPath("~/Skin/" + skinName + "/"));
                    //遍历文件
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        if (NextFile.Extension.ToUpper() == ".CSS")
                            sb.Append("<link id='skinLink' href='" + str + "Skin/" + skinName + "/" + NextFile.Name + "' type='text/css' rel='stylesheet' title='" + skinName + "' />");
                    }
                }
            }

            //输出
            writer.WriteLine(sb.ToString());
        }



    }
}
