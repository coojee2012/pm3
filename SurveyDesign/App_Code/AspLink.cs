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
    ///  ��ʽ
    /// </summary>
    [DefaultProperty("Text")]
    [DisplayName("HtmlLink")]
    [Description("HtmlLink")]
    [ToolboxData("<{0}:Link runat=server></{0}:Link>")]
    //[ToolboxBitmap(typeof(resfinder), "favicon.ico")]
    public class Link : WebControl
    {
        private string href = "~/skin/blue/main.css";

        [Description("��ȡ�������� System.Web.UI.HtmlControls.HtmlLink �ؼ���ָ�������ӵ� URL Ŀ�ꡣ")]
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("~/skin/blue/main.css")]
        [Localizable(true)]
        public string Href
        {
            get { return href; }
            set { href = value; }
        }

        //��д����Render����HTML����#region ��д����Render����HTML����
        protected override void Render(HtmlTextWriter writer)
        {
            //ע�͸���Render�ķ�������ΪWebControlĬ�ϻ�����һ��<span>��ǩ
            //base.Render(writer);
            //����HtmlTextArea�ؼ���Render��������HTML
            //this.htmlLink.RenderControl(writer);
            //��ǰ������Ϊ���򷵻أ���VS��ASPX�����ͼҳ�棩

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<link id='skinLink' href='" + href + "' type='text/css' rel='stylesheet' title='default' />"); //Ĭ��
            if (Context != null)
            {
                string skinName = "";

                //�����ݿ��еõ������              
                ProjectDB db = new ProjectDB();
                string sysSkinName = db.getSysObjectContent("_sys_skinName");//�õ���̨���õķ������
                skinName = sysSkinName;

                //��cookie�еõ��������õķ����
                if (Context.Request.Cookies["_SYS_QS_SKINNAME"] != null)
                {
                    string cookiesSkinName = Context.Server.HtmlEncode(Context.Request.Cookies["_SYS_QS_SKINNAME"].Value);
                    skinName = cookiesSkinName;//������ھ��滻��
                }



                if (!string.IsNullOrEmpty(skinName))
                {
                    //���cookies��ȡ���ķ�񲻴��ڣ���ֱ����ϵͳ�����
                    if (!File.Exists(Context.Server.MapPath("~/Skin/" + skinName + "/main.css")))
                        skinName = sysSkinName;

                    //����Ϊ����·��������Ӧiss������վ����ʽ��
                    string str = "";
                    string first = "http://" + Context.Request.Headers["Host"];

                    string root = Context.Request.ApplicationPath;
                    if (root != "/")
                        root += "/";
                    str = first + root;
                    string newHref = str + "Skin/" + skinName + "/main.css";

                    //������ʽ���ô� 
                    sb.Remove(0, sb.Length);

                    DirectoryInfo TheFolder = new DirectoryInfo(Context.Server.MapPath("~/Skin/" + skinName + "/"));
                    //�����ļ�
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        if (NextFile.Extension.ToUpper() == ".CSS")
                            sb.Append("<link id='skinLink' href='" + str + "Skin/" + skinName + "/" + NextFile.Name + "' type='text/css' rel='stylesheet' title='" + skinName + "' />");
                    }
                }
            }

            //���
            writer.WriteLine(sb.ToString());
        }



    }
}
