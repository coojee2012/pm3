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
using System.Text; 
/// <summary>
/// cstPerBasePager 的摘要说明
/// </summary>
public class cstPerBasePager:Page
{
    private Page _pager = null;
    RCenter rc = new RCenter();
 
    public cstPerBasePager()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected virtual void Page_Load(object sender, EventArgs e)
    {
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
            CehckSession();
        }
    }
    public cstPerBasePager(Page pager)
    {
        this._pager = pager;
    }
    private void CehckSession()
    {

        StringBuilder sb = new StringBuilder();
        if (Session["FAppId"] == null || Session["FBaseId"] == null)
        {

            string fDeptNumber = System.Configuration.ConfigurationSettings.AppSettings["DefaultDept"].ToString();
            if (fDeptNumber == null || fDeptNumber == "")
            {
                return;
            }
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            switch (fDeptNumber)
            {
                case "52":
                    sb.Append("parent.close();window.open('../../ApproveWeb/gzmain/fdcLogin.aspx','','');");
                    break;
                case "61":
                    sb.Append("parent.close();window.open('../../ApproveWeb/lnmain/zbbackLogin.aspx','','');");
                    break;
                case "51":
                    sb.Append("parent.close();window.open('../../ApproveWeb/scmain/zbbackLogin.aspx','','');");
                    break;
                case "36":
                    sb.Append("parent.close();window.open('../../ApproveWeb/jxmain/zbbackLogin.aspx','','');");
                    break;
                default:
                    sb.Append("parent.close();window.open('../../ApproveWeb/gzmain/fdcLogin.aspx','','');");
                    break;
            }



            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }
        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove();</script>");
        }
    }

}

