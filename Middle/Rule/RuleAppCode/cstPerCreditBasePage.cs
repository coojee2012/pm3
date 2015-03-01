using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;

/// <summary>
/// cstPerCreditBasePage 的摘要说明
/// </summary>
public class cstPerCreditBasePage:Page
{
    RCenter rc = new RCenter();
    public cstPerCreditBasePage()
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
            //CheckSessoin();
        }
        
    }

    protected bool IsNewEnt()
    {
        if (Session["DFSystemId"] != null)
        {
            return true;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fstate from cf_ent_baseinfo where fid='" + Session["FBaseId"].ToString() + "'");
        string fState = rc.GetSignValue(sb.ToString());
        if (fState == null || fState.Trim() == "")
        {
            sb.Remove(0, sb.Length);
            sb.Append(" select FIsFirst from cf_sys_user where fid ='" + Session["FUserId"].ToString() + "'");
            fState = rc.GetSignValue(sb.ToString());
            if (fState != null && fState.Trim() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            if (fState.Trim() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    
    private void CheckSessoin()
    {
        if (Session["FCanMod"] == null || Session["FCanMod"].ToString() != "1")
        { 
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApproveVis();</script>"); 
        }
        if (Session["FBaseId"] == null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>alert('登录超时,请重新登录');parent.close();</script>");
            this.RegisterStartupScript(Guid.NewGuid().ToString(), sb.ToString());
        }
    }
}
