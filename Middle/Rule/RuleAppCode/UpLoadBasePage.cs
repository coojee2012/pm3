using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Approve.EntitySys;
using Approve.RuleCenter;
using Approve.EntityCenter;
using Approve.EntityBase;
using Approve.Common;
using System.Text;
using System.Web.SessionState;
/// <summary>
/// 上传图片的
///  
/// </summary>
public class UpLoadBasePage : Page, IRequiresSessionState
{
    private Page _pager = null;
    RCenter rc = new RCenter();
  

    public UpLoadBasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected virtual void Page_Load(object sender, EventArgs e)
    {
        //CehckSession();
        if (!Page.IsPostBack)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("function   EnterToTab(e)");
            sb.Append("{ ");
            sb.Append("if   (event.keyCode   ==   13)   {   ");
            sb.Append("event.keyCode   =   9;  ");
            sb.Append("} ");
            sb.Append("} ");
            sb.Append("document.onkeydown  =  EnterToTab; ");
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>" + sb.ToString() + "</script>");


        }

    }

    public string FAppId
    {
        get { return EConvert.ToString(Request.QueryString["FAppId"]); }
    }
    public string FBaseId
    {
        get { return EConvert.ToString(Request.QueryString["FBaseId"]); }
    }
    public string FPId
    {
        get { return EConvert.ToString(Request.QueryString["FPId"]); }
    }

    public UpLoadBasePage(Page pager)
    {
        this._pager = pager;
    }
    private void CehckSession()
    {

        StringBuilder sb = new StringBuilder();
        string SID = Request.QueryString["SID"];
        DateTime t = SecurityEncryption.GetTime(SecurityEncryption.DesDecrypt(SID, "1234abcd"));
        if ((DateTime.Now - t).TotalHours > 1)
        {
            sb.Append("没有权限访问");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }


    }
}
