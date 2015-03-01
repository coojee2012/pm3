using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Share_Main_loginXZSP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string URL = System.Configuration.ConfigurationSettings.AppSettings["dbXZSPPage"];
        string FName = Session["DFUserName"].ToString(); //用户名
        string pk = System.Configuration.ConfigurationSettings.AppSettings["dbXZSPMY"]; ; //密钥
        DateTime time = DateTime.Now.AddHours(1); //时限
        string key = XZSPSecurityEncryption.DesEncrypt(FName + "|" + XZSPSecurityEncryption.ConvertDateTimeInt(time), pk); //加密
        URL = URL + "?OutSysCheck=1&key=" + HttpUtility.UrlEncode(key, Encoding.UTF8);
        Response.Redirect(URL); //跳转

    }
}