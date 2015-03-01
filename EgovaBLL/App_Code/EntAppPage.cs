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

/// <summary>
/// QuaEntAppBasePage 的摘要说明
/// </summary>
public class EntAppPage : Page
{
    private Page _pager = null;
    RCenter rc = new RCenter();
 

    public EntAppPage()
    {
        //
        // TODO: 常规业务页面继承类
        //
    }
    protected virtual void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
        CehckSession();
    }
    public EntAppPage(Page pager)
    {
        this._pager = pager;
    }
    private void CehckSession()
    {

        StringBuilder sb = new StringBuilder();
        if (Session["FAppId"] == null || Session["FBaseId"] == null)
        {
            string fDeptNumber = ComFunction.GetDefaultDept();
            if (fDeptNumber == null || fDeptNumber == "")
            {
                return;
            }
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            sb.Append("top.close();window.open('../../Share/WebSide/Default.aspx','','');");
            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }

        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>btnEnable();</script>");
        }

    }

}
