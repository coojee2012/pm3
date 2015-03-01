
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.Design;
using Approve.EntityBase;

#region 使用嵌入资源
[assembly: System.Web.UI.WebResource("Resources/EndyVerifyCode.yzm.jpg", "image/jpeg")]
#endregion
namespace Tools
{
    [DefaultProperty("Text")]
    [Description("绘制并生成随机验证码图片")]
    [Designer(typeof(design1))]
    [ToolboxData("<{0}:EndyVCode runat=server></{0}:EndyVCode>")]

    public class EndyVCode : WebControl
    {
        string text = "";
        string startcolor = "#FF0000";
        string endcolor = "#000000";
        string bgcolor = "#EFEFEF";
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        private string Text
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["VNum"]);
            }

            set
            {
                HttpContext.Current.Session["VNum"] = value;
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("#FF0000")]
        [Localizable(true)]
        public string StartColor
        {
            get
            {
                return startcolor;
            }

            set
            {
                startcolor = value;
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("#000000")]
        [Localizable(true)]
        public string EndColor
        {
            get
            {
                return endcolor;
            }

            set
            {
                endcolor = value;
            }
        }
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("#EFEFEF")]
        [Localizable(true)]
        public string BGColor
        {
            get
            {
                return bgcolor;
            }

            set
            {
                bgcolor = value;
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            if (this.Width.ToString() == "")
                this.Width = new Unit(60);
            if (this.Height.ToString() == "")
                this.Height = new Unit(20);
            if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["vcadd"]))
            {
                DrawCode();
            }
            string str = "?";
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
            {
                str = HttpContext.Current.Request.Url.AbsoluteUri + "&";
            }
            output.Write("<span id=\"spcode\" name=\"Scode\" style=\"border:1px solid #CCCCCC;font-size:12px;padding:1px;\">生成中..</span><img id=\"img1\" onload=\"document.getElementById('spcode').style.display='none';$('span[name=Scode]').hide();\" src=\"" + str + "vcadd=" + SecurityEncryption.ConvertDateTimeInt(DateTime.Now) + "\" onclick=\"this.src='" + str + "vcadd='+new Date();\" style=\"cursor:pointer;\" alt=\"点击切换\" border=\"0\" align=\"absmiddle\" width=\"" + this.Width + "\" height=\"" + this.Height + "\"/>");
        }
        protected void DrawCode()
        {
            Random rand = new Random();
            string n = "123456789acdefkbhxwv";
            string VNum = "";
            for (int i = 0; i < 4; i++)
            {
                VNum += n[rand.Next(0, n.Length - 1)];
            }
            Text = VNum;
            Bitmap bp = new Bitmap(60, 20, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.Clear(System.Drawing.ColorTranslator.FromHtml(this.BGColor));
            for (int i = 0; i < 6; i++)
            {
                int x1 = rand.Next(0, bp.Width);
                int y1 = rand.Next(0, bp.Height);
                int si = rand.Next(6, 10);
                g.DrawString(n[rand.Next(0, n.Length)].ToString(), new Font("Arial", si), new System.Drawing.SolidBrush(Color.White), new PointF(x1, y1));
            }
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(0, 0, bp.Width, bp.Height), System.Drawing.ColorTranslator.FromHtml(this.StartColor), System.Drawing.ColorTranslator.FromHtml(this.EndColor), 90, true);
            Bitmap p = new Bitmap(60, 20, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            Graphics r = Graphics.FromImage(p);
            r.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            for (int j = 0; j < VNum.Length; j++)
            {
                r.RotateTransform(rand.Next(-2, 2));
                r.DrawString(VNum[j].ToString(), new Font("Arial", rand.Next(11, 14), FontStyle.Bold | FontStyle.Italic), brush, new PointF(j * (rand.Next(12, 15)), 1));
            }
            g.DrawImage(p, new Point(1, 1));
            g.DrawRectangle(new Pen(Color.FromArgb(182, 182, 182), 1), new Rectangle(0, 0, 59, 19));
            r.Dispose();
            p.Dispose();
            g.Dispose();


            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent(); //需要输出图象信息 要修改HTTP头
            HttpContext.Current.Response.ContentType = "image/JPEG";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            bp.Dispose();
            HttpContext.Current.Response.End();
            ms.Flush();
            ms.Close();
            ms.Dispose();
            //Response.OutputStream不行，必须用上面这种方式创建内存流
            //bp.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);           
        }

        public bool IsPass(string code)
        {
            if (HttpContext.Current.Session["VNum"] == null || EConvert.ToString(HttpContext.Current.Session["VNum"]).Equals(code, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
    public class design1 : ControlDesigner
    {
        private EndyVCode mycontrol;
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            mycontrol = (EndyVCode)component;
        }
        public override string GetDesignTimeHtml()
        {
            if (mycontrol.Width.ToString() == "")
                mycontrol.Width = new Unit(60);
            if (mycontrol.Height.ToString() == "")
                mycontrol.Height = new Unit(20);
            return "<img src='" + mycontrol.Page.ClientScript.GetWebResourceUrl(this.GetType(), "EndyVerifyCode.yzm.jpg") + "' width=" + mycontrol.Width + " height=" + mycontrol.Height + "/>";
        }
    }
}