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
using Approve.RuleCenter;
using Approve.EntityBase;
using System.Text;
public partial class Common_ValidateUserId : System.Web.UI.UserControl
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hfFUserId.Value = Convert.ToString(Session["FUserId"]);
            hfFCompany.Value = Server.UrlEncode(string.Format("{0}", Session["FBaseName"]));
        }
    }
    public bool ValidateUserId()
    {
        if (this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.Form[hfFUserId.UniqueID]))
            {
                if (Request.Form[hfFUserId.UniqueID] != Convert.ToString(Session["FUserId"]))
                {
                    return false;
                }
            }
        }
        return true;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (!ValidateUserId())
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "test", string.Format("alert('有新的用户进来了，窗口\\n({0})\\n必须退出！点确定按钮之后将强制退出。');top.window.opener=null;top.window.open('','_self');top.close();", HttpUtility.UrlDecode(hfFCompany.Value, Encoding.Default)), true);
        }
    }
}
