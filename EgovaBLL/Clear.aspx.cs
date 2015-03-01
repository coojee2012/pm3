using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.RuleApp;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using Tools;
public partial class Clear : System.Web.UI.Page
{
    public string myTime = (Environment.TickCount / 1000 / 60 / 60 % 24) + ":" + (Environment.TickCount / 1000 / 60 % 60) + ":" + (Environment.TickCount / 1000 % 60);
    protected void Page_Load(object sender, EventArgs e)
    {
        Color color = Color.FromArgb(10, 20, 30);
        ViewState["aa"] = 123;
        if (ViewState["AA"] == null)
        {
            //Response.Write((Environment.TickCount / 1000 / 60 / 60 % 24) + ":" + (Environment.TickCount / 1000 / 60 % 60) + ":" + (Environment.TickCount / 1000 % 60));
        }

        //FileStream s = new FileStream(@"D:\a.jpg", FileMode.Open, FileAccess.Read);
        //byte[] bt = new byte[(int)s.Length];
        ////s.Read(bt, 0, bt.Length);
        ////s.Close();
        //bt = File.ReadAllBytes(@"D:\a.jpg");
        //Response.ContentType = "image/jpeg";
        //Response.OutputStream.Write(bt, 0, bt.Length);
        ////Response.End();
        //SqlConnection con = new SqlConnection(@"server=.\sqlexpress;uid=sa;pwd=sa;database=myTest");
        //con.Open();
        //Response.Write(con.ServerVersion);    
    }
    private string staticStringbSubstring(string s, int length)//按字节截取字符串
    {
        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
        int n = 0;　//　表示当前的字节数
        int i = 0;　//　要截取的字节数
        for (; i < bytes.GetLength(0) && n < length; i++)
        {
            //　偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
            if (i % 2 == 0)
            {
                n++;　　　//　在UCS2第一个字节时n加1
            }
            else
            {
                //　当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                if (bytes[i] > 0)
                {
                    n++;
                }
            }
        }
        //　如果i为奇数时，处理成偶数
        if (i % 2 == 1)
        {
            //　该UCS2字符是汉字时，去掉这个截一半的汉字
            if (bytes[i] > 0)
                i = i - 1;
            //　该UCS2字符是字母或数字，则保留该字符
            else
                i = i + 1;
        }
        return System.Text.Encoding.Unicode.GetString(bytes, 0, i).ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComFunction.ClearHashNumber();
        DataCache.RemoveAll();
        RApp.ClearHashNumber();
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        ShareTool st = new ShareTool();
        st.DownloadJSDWAll();
    }
}
