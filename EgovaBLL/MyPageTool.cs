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
    /// pageTool ��ժҪ˵����
    /// </summary>
    public class MyPageTool
    {
       
        #region ҳ�浯����Ϣ
        /// <summary>
        /// ҳ�浯����Ϣ
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        public static void showMessage(string message,Page page)
        {
            page.ClientScript.RegisterStartupScript(page.ClientScript.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>");
                
        }
        /// <summary>
        /// ҳ�浯����Ϣ
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        public static void showMessageAjax(string message, UpdatePanel c)
        {
            ScriptManager.RegisterClientScriptBlock(c, c.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "')</script>", true);
        }
        /// <summary>
        /// ҳ�浯����Ϣ������ת����ҳ��
        /// </summary>
        /// <param name="message">Ҫ��������Ϣ</param>
        /// <param name="nextUrl">Ҫ��ת����ҳ���Url</param>
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