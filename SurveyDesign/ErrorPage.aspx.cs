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
using System.Net;
using Approve.RuleCenter;
using System.Text;
using Approve.Common;
public partial class ErrorPage : System.Web.UI.Page
{
    public string sMessage = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["Message"]))
            {
                sMessage = ComFunction.GetValueByName("EerrorMessage");
                if (sMessage == null || sMessage == "")
                {
                    sMessage = "抱歉,您访问的页面出错了,请您重新登录";
                }
            }
            else
            {
                sMessage = Request.QueryString["Message"];
            } 
        } 
        
    }  
     
}
 
 
