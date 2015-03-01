using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using Approve.EntityBase;

namespace Web
{
    public class ModuleAuthority : IHttpModule
    {
        private List<string> exceptURL = new List<string>();
        public ModuleAuthority()
        {
            //exceptURL.Add("~/admin/main/managerlogin.aspx");
            exceptURL.Add("~/government/appmain/empcredit.aspx");
            exceptURL.Add("~/errorpage.aspx");
            exceptURL.Add("~/default.aspx");

            exceptURL.Add("~/share/main/login.aspx");
            exceptURL.Add("~/share/main/BJZY.aspx");
            exceptURL.Add("~/share/main/lockcheck.aspx");
            exceptURL.Add("~/payment/main/lockcheck.aspx");
            exceptURL.Add("~/payment/main/login.aspx");
            exceptURL.Add("~/common/empinfo.aspx");
            exceptURL.Add("~/common/ajaxpage.aspx");
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }

        public void Dispose()
        {

        }


        private void context_AcquireRequestState(object sender, EventArgs e)
        {

            HttpApplication app = sender as HttpApplication;

            string url = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower();
            string extend = Path.GetExtension(url).ToLower();
            if (extend == ".aspx")//只验证aspx的页面
            {
                //排除访问的页面

                if (!exceptURL.Contains(url))
                {
                    //if (url.IndexOf("~/admin/") > -1)
                    //{
                    //    if (app.Session["Admin_FID"] == null)
                    //    {
                    //        app.Response.Write("您没有登录或者没有权限访问");
                    //        app.Response.End();
                    //    }
                    //}
                    if (url.IndexOf("~/government/") > -1)
                    {
                        if (app.Session["DFUserId"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问");

                            app.Response.End();
                        }
                    }
                    else if (url.IndexOf("~/website/") > -1)
                    {

                    }
                    else if (url.IndexOf("~/payment/") > -1)
                    {
                        if (url.IndexOf("~/payment/sys/") > -1)
                        {
                            if (app.Session["SH_UserId"] == null)
                            {
                                app.Response.Write("您没有登录或者没有权限访问");
                                app.Response.End();
                            }
                        }
                        else if (app.Session["Pay_FUserId"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问");
                            app.Response.End();
                        }
                    }
                    else if (url.IndexOf("~/share/") > -1)
                    {
                        if (app.Session["SH_UserId"] == null)
                        {
                            if (url.IndexOf("~/share/webside/") > -1)
                            {
                            }
                            else
                            {
                                app.Response.Write("您没有登录或者没有权限访问");
                                app.Response.End();
                            }
                        }
                    }
                    else
                    {
                        if (url.IndexOf("~/approveweb/") == -1 && (app.Session["DFUserId"] != null || app.Session["FUserId"] != null))
                        {

                        }
                        else
                        {
                            if (url.IndexOf("uploadpic") > -1
                                || url.IndexOf("newupload") > -1
                                || url.IndexOf("uploadcertipic") > -1)
                            {
                                string SID = app.Request.QueryString["SID"];
                                DateTime t = SecurityEncryption.GetTime(SecurityEncryption.DesDecrypt(SID, "1234abcd"));
                                if ((DateTime.Now - t).TotalHours > 1)
                                {
                                    app.Response.Write("您没有登录或者没有权限访问");
                                    app.Response.End();
                                }
                            }
                            else
                            {
                                //if (url.IndexOf("~/approveweb/") == -1)
                                //{
                                //    app.Response.Write("您没有登录或者没有权限访问");
                                //    app.Response.End();
                                //}
                            }
                        }
                    }
                }
            }
        }


    }

}