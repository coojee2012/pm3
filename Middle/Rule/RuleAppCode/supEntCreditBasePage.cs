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
using Approve.Common;
/// <summary>
/// energyEntCreditBasePage 的摘要说明
/// </summary>
public class supEntCreditBasePage : Page
{
    RCenter rc = new RCenter();
    public supEntCreditBasePage()
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


            string sLoadingImg = ComFunction.GetSystemConfig("JZJNLoadingImg");
            string sLoadingStr = ComFunction.GetSystemConfig("JZJNLoadingStr");


            //sb.Remove(0, sb.Length);
            //sb.Append("<script>");
            //sb.Append("document.write('<div id=\"loadingDiv\"  style=\"text-align:center;word-break:normal;vertical-align:middle;\">");
            //sb.Append("<div  style=\"top:50%;bottom:50%;left:auto;position:absolute;text-align:center;word-break:normal;\">");
            //sb.Append("<img src=\"" + sLoadingImg + "\"/>");
            //sb.Append("<div style=\"font-size:12px;background-color: #F7EEE9;margin-top:5px;word-break:normal;\">" + sLoadingStr + "</div>");
            //sb.Append("</div>");
            //sb.Append("</div>')");
            //sb.Append("</script>");
            //this.Response.Write(sb.ToString());

            //sb.Remove(0, sb.Length);
            //sb.Append("<script>");
            //sb.Append("function loading(){document.getElementById('loadingDiv').style.display='none';}");


            //sb.Append("if (window.attachEvent)");
            //sb.Append("{");
            //sb.Append("window.attachEvent(\"onload\",function(){loading();})");
            ////IE 的事件代码
            //sb.Append("}");
            //sb.Append("else");
            //sb.Append("{");
            //sb.Append("window.addEventListener(\"load\",function(){loading();})");

            ////其它浏览器的事件代码
            //sb.Append("}");
            //sb.Append("</script>");
            //this.RegisterStartupScript(Guid.NewGuid().ToString(), sb.ToString());


            CheckSessoin();
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
        if (fState == null || fState.Trim() == "0")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void CheckSessoin()
    {
        if (Session["FCanMod"] != null && Session["FCanMod"].ToString() != "1")
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
