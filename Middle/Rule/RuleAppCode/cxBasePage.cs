﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.Common;

/// <summary>
/// cxBasePage 的摘要说明
/// </summary>
public class cxBasePage : Page
{
    public cxBasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    protected void Page_Load(object sender, EventArgs e)
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
    private void CehckSession()
    {
        StringBuilder sb = new StringBuilder();
        if (Session["DFSystemId"] == null || Session["DFName"] == null ||
            Session["DFUserId"] == null || Session["DFRoleId"] == null ||
            Session["DFId"] == null || Session["DFLevel"] == null)
        { 

            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            sb.Append("parent.close();window.open('../../ApproveWeb/lnmain/appbackLogin.aspx','','');");
            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }
    }
}
