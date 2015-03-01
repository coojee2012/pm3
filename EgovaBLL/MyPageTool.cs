using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
namespace EgovaBLL
{

    /// <summary>
    /// pageTool 的摘要说明。
    /// </summary>
    public class MyPageTool
    {
       
        #region 页面弹出消息
        /// <summary>
        /// 页面弹出信息
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        public static void showMessage(string message,Page page)
        {
            page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>");
                
        }
        /// <summary>
        /// 页面弹出信息
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        public static void showMessageAjax(string message, UpdatePanel c)
        {
            ScriptManager.RegisterClientScriptBlock(c, c.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>", true);
        }
        /// <summary>
        /// 页面弹出信息并且跳转到新页面
        /// </summary>
        /// <param name="message">要弹出的信息</param>
        /// <param name="nextUrl">要跳转的新页面的Url</param>
        public static void showMessageAndGoNewPage(string message, string nextUrl, Page page)
        {
            page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');window.location='" + nextUrl + "';</script>");
        }

        public static void showMessageAndRunFunction(string message, string functionName, Page page)
        {

            page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');" + functionName + ";</script>");
        }
        public static void showMessageAndRunFunctionAjax(string message, string functionName, Control c)
        {
            ScriptManager.RegisterClientScriptBlock(c, c.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');" + functionName + ";</script>", true);
        }
        public static void showMessageAndRunFunction(string message, string functionName, string funName, Page page)
        {
            page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');" + functionName + ";" + funName + ";</script>");
        }
        #endregion

    }
}