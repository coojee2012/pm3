using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

/// <summary>
/// WYAppPage 的摘要说明
/// </summary>
public class WYPage : Page
{
    public WYPage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //protected virtual void Page_Load(object sender, EventArgs e)
    //{
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iType"></param>
    public void CheckSession()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>alert('Session过期,请重新登录!');parent.close();window.open('../Main/Index.aspx','','');</script>");       

        if (Session["GovLinkID"] == null || Session["XMBH"] == null || Session["FAppId"] == null || Session["FManageTypeId"] == null || Session["FIsApprove"] == null)
        {
            Response.Write(sb.ToString());
            Response.End();
        }

    }
}