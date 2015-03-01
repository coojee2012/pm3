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
/// enAppBasePage 的摘要说明
/// 施工企业的父页面
/// </summary>
public class entAppBasePage : Page, IRequiresSessionState 
{
    private Page _pager = null;
    RCenter rc = new RCenter();

    public entAppBasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected virtual void Page_Load(object sender, EventArgs e)
    {
        CehckSession();
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



            //string sLoadingImg = ComFunction.GetSystemConfig("SGLoadingImg");
            //string sLoadingStr = ComFunction.GetSystemConfig("SGLoadingStr");


            //sb.Remove(0, sb.Length);
            //sb.Append("<script>");
            //sb.Append("document.write('<div id=\"loadingDiv\"  style=\"text-align:center;word-break:normal;vertical-align:middle;\">");
            //sb.Append("<div  style=\"top:50%;bottom:50%;left:auto;position:absolute;text-align:center;word-break:normal;\">");
            //sb.Append("<img src=\"" + sLoadingImg + "\"/>");
            //sb.Append("<div style=\"font-size:12px;background-color: #F7EEE9;margin-top:5px;word-break:normal;\">" + sLoadingStr + "</div>");
            //sb.Append("</div>");
            //sb.Append("</div>');");


            //sb.Append("document.write('<div id=\"OverDiv\" style=\"filter: alpha(opacity=50);position:\"absolute\" background-color:ECF0F5;height:100%;width:100%;z-index:1000;\">");
            //sb.Append(" </div>')");

            //sb.Append("</script>");



            //this.Response.Write(sb.ToString());




//            function ShowQueryDiv(obj)
//{
//    var cDiv = document.getElementById('Div_Query');
//    if( cDiv== null)
//    {
//        return;
//    }
//    else
//    {
//        cDiv.style.display="";  
//        cDiv.style.top = (document.body.clientHeight-parseInt(cDiv.style.height.replace("px","")))/2;
//        cDiv.style.left = (document.body.clientWidth-parseInt(cDiv.style.width.replace("px","")))/2;
//        cDiv.style.position = "absolute";
////        cDiv.style.backgroundColor="Transparent";
//        cDiv.style.backgroundColor="#FFFFFF";
        
        
//        with(document.getElementById('overDiv'))
//        {
//            style.display="";
//            style.width = document.body.clientWidth;
//            style.height = document.body.clientHeight;
//            style.top=0;
//            style.left=0;
//            style.position = "absolute";
//            style.zIndex = 10; 
//            style.backgroundColor="#0000FF";  
//        }  
//    }
//    document.getElementById("hidden_type").value = obj.id;
//}






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


            
        }

    }
    public entAppBasePage(Page pager)
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
                    sb.Append("parent.close();window.open('../../ApproveWeb/lnmain/backLogin.aspx','','');");
                    break;
                case "36":
                    sb.Append("parent.close();window.open('../../ApproveWeb/jxmain/backLogin.aspx','','');");
                    break;
            }  
            
            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }

        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove()</script>");
        }

    }

}
