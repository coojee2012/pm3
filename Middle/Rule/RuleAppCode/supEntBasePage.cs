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
using Approve.Common;
/// <summary>
/// hourseEntBasePage 的摘要说明
/// </summary>
public class supEntBasePage : Page
{
    private Page _pager = null;
    RCenter rc = new RCenter();
 

    public supEntBasePage()
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


            //string sLoadingImg = ComFunction.GetSystemConfig("FDCLoadingImg");
            //string sLoadingStr = ComFunction.GetSystemConfig("FDCLoadingStr");


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


            CehckSession();
        }
    }
    public supEntBasePage(Page pager)
    {
        this._pager = pager;
    }
    private void CehckSession()
    {

        StringBuilder sb = new StringBuilder();
        if (Session["FAppId"] == null || Session["FBaseId"] == null)
        {
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            string sDeptNumber = ComFunction.GetDefaultDept();
            switch (sDeptNumber)
            {
                case "61":
                    sb.Append("parent.close();window.open('../../ApproveWeb/lnmain/fdcBackLogin.aspx','','');");
                    break;
                case "36":
                    sb.Append("parent.close();window.open('../../ApproveWeb/jxmain/fdcBackLogin.aspx','','');");
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

