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
/// <summary>
/// adminBasePage 的摘要说明
/// 管理员的父页面
/// </summary>
public class adminBasePage : System.Web.UI.Page
{
    public adminBasePage()
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
        if (Session["FIsAdmin"] == null)
        {
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            string sDeptNumber = ComFunction.GetDefaultDept();
            switch (sDeptNumber)
            {
                case "21":
                    sb.Append("parent.close();window.open('../../ApproveWeb/main/backlogin.aspx','','');");
                    break;
                case "36":
                    sb.Append("parent.close();window.open('../../ApproveWeb/jxmain/AppBackLogin.aspx','','');");
                    break;
            }
            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }
    }
}
