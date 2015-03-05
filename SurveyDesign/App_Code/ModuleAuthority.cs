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
using System.Text;
using ProjectBLL;

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
            exceptURL.Add("~/clear.aspx");
            exceptURL.Add("~/common/empinfo.aspx");
            exceptURL.Add("~/payment/main/login.aspx");
            exceptURL.Add("~/share/main/login.aspx");
            exceptURL.Add("~/share/main/lockcheck.aspx");
            exceptURL.Add("~/payment/main/lockcheck.aspx");
            exceptURL.Add("~/share/main/loginall.aspx");
            exceptURL.Add("~/share/main/lockcheckAll.aspx");

            exceptURL.Add("~/common/ajaxpage.aspx");
            exceptURL.Add("~/common/help/help.aspx");//帮助页面

            exceptURL.Add("~/gmap/locationmap/locationmap.aspx");//地图定位
            exceptURL.Add("~/gmap/locationmap/selectaear.aspx");//地图定位
            exceptURL.Add("~/gmap/locationmap/mapconfig.aspx");//地图定位
            exceptURL.Add("~/tiny_mce/plugins/ajaxfilemanager/filemanager.aspx");
            exceptURL.Add("~/share/user/userqxfb.aspx");//权限分配
            exceptURL.Add("~/userautologin.aspx");//自动登录
            exceptURL.Add("~/userauthority.aspx");//分配权限

        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
            //context.AuthenticateRequest += OnAuthenticate;
        }

        public void Dispose()
        {

        }

        #region 验证规则
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
                    if (url.IndexOf("~/admin/") > -1)
                    {
                        if (app.Session["Admin_FID"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问1");
                            app.Response.End();
                        }
                    }
                    else if (url.IndexOf("~/government/") > -1)
                    {
                        if (app.Session["DFUserId"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问2");

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
                                app.Response.Write("您没有登录或者没有权限访问3");
                                app.Response.End();
                            }
                        }
                        else if (app.Session["Pay_FUserId"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问4");
                            app.Response.End();
                        }
                    }
                    else if (url.IndexOf("~/EmpJZDW/".ToLower()) > -1 || url.IndexOf("~/EmpKcsjSgt/".ToLower()) > -1)
                    {
                        if (string.IsNullOrEmpty(CurrentEmpUser.EmpId) && string.IsNullOrEmpty(CurrentEntUser.EntUserId))
                        {

                            app.Response.Write("您没有登录或者没有权限访问5");
                            app.Response.End();

                        }
                    }
                    else if (url.IndexOf("~/gfent/") > -1) { }
                    else if (url.IndexOf("~/jnclent/") > -1) { }
                    else if (url.IndexOf("~/share/") > -1)
                    {
                        if (app.Session["SH_UserId"] == null)
                        {
                            if (url.IndexOf("~/share/webside/") > -1)
                            {
                            }
                            else
                            {
                                app.Response.Write("您没有登录或者没有权限访问6");
                                app.Response.End();
                            }
                        }
                    }
                    else if ((!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["fbid"]) || !string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["IsView"])))
                    {
                        if (app.Session["DFUserId"] == null)
                        {
                            app.Response.Write("您没有登录或者没有权限访问!7");
                            app.Response.End();
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
                                    app.Response.Write("您没有登录或者没有权限访问8");
                                    app.Response.End();
                                }
                            }
                            else
                            {
                                if (url.IndexOf("~/approveweb/") == -1 && url.IndexOf("jnclent") == -1 && url.IndexOf("audit") == -1 && url.IndexOf("BuildUnitList.aspx".ToLower()) == -1)
                                {
                                    Response.Write(url);
                                    app.Response.Write("您没有登录或者没有权限访问9");
                                    app.Response.End();
                                }
                            }
                        }
                    }
                }
            } if (extend == ".svc")
            {
                string creds = GetBase64CredentialsFromHeader();
            }
        }


        #endregion


        void OnAuthenticate(object sender, EventArgs e)
        {
            if (!Authenticated())
            {
                SendAuthenticate();
            }
        }


        HttpRequest Request
        {
            get
            {
                return (HttpContext.Current.Request);
            }
        }
        HttpResponse Response
        {
            get
            {
                return (HttpContext.Current.Response);
            }
        }
        HttpApplication Application
        {
            get
            {
                return (HttpContext.Current.ApplicationInstance);
            }
        }
        bool Authenticated()
        {
            bool auth = false;

            //try
            //{
            string creds = GetBase64CredentialsFromHeader();

            if (creds != null)
            {
                string userName, password;

                DecodeBase64UsernamePassword(creds, out userName,
                  out password);

                if (!string.IsNullOrEmpty(userName) &&
                  !string.IsNullOrEmpty(password))
                {
                    //auth = Membership.ValidateUser(userName, password);

                    //if (auth)
                    //{
                    //GenericIdentity id = new GenericIdentity(userName);

                    //HttpContext.Current.User = new RolePrincipal(id);
                    //}
                    auth = true;
                }
                else
                {
                    Response.StatusCode = 401;
                    Response.StatusDescription = accessDeniedStatus;
                    Response.Write(accessDeniedHtml);
                }
            }
            //}
            //catch (NullReferenceException)
            //{
            //}
            return (auth);
        }
        void SendAuthenticate()
        {
            Response.StatusCode = 401;
            Response.StatusDescription = accessDeniedStatus;
            Response.Write(accessDeniedHtml);

            // TODO: not sure this is quite right wrt realm.
            Response.AddHeader(authServerHeader,
              string.Format(realmFormatString,
              Request.Url.GetLeftPart(UriPartial.Authority)));

            //Application.CompleteRequest();
        }
        string GetBase64CredentialsFromHeader()
        {
            string credsHeader = Request.Headers[authClientHeader];
            string creds = null;
            if (!string.IsNullOrEmpty(credsHeader))
            {
                int credsPosition =
                  credsHeader.IndexOf(basicAuth, StringComparison.OrdinalIgnoreCase);

                if (credsPosition != -1)
                {
                    credsPosition += basicAuth.Length + 1;

                    creds = credsHeader.Substring(credsPosition,
                      credsHeader.Length - credsPosition);
                }
            }
            return (creds);
        }
        void DecodeBase64UsernamePassword(string creds, out string userName, out string password)
        {
            userName = password = null;

            string decoded =
              ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(creds));

            int separatorIndex = decoded.IndexOf(basicCredentialsSeparator);

            if (separatorIndex != -1)
            {
                userName = decoded.Substring(0, separatorIndex);
                password = decoded.Substring(separatorIndex + 1,
                  decoded.Length - (separatorIndex + 1));
            }
        }
        const string accessDeniedStatus = "Access Denied";
        const string accessDeniedHtml = "<html><body>401 Access Denied</body></html>";
        const string realmFormatString = "Basic realm=\"{0}\"";
        const string authServerHeader = "WWW-Authenticate";
        const string authClientHeader = "Authorization";
        const string basicAuth = "Basic";
        const char basicCredentialsSeparator = ':';

    }

}