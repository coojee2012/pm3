using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

/// <summary>
/// WYAppPage 的摘要说明
/// </summary>
public class WYAppPage :Page
{
    public WYAppPage()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    protected virtual void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
        CehckSession();
    }

    private void CehckSession()
    {

        StringBuilder sb = new StringBuilder();
        if (Session["FAppId"] == null)
        {
            string fDeptNumber = ComFunction.GetDefaultDept();
            if (fDeptNumber == null || fDeptNumber == "")
            {
                return;
            }
            sb.Append("<script>");
            sb.Append("alert('Session过期,请重新登录!');");
            sb.Append("parent.close();window.open('../Main/Index.aspx','','');");
            sb.Append("</script>");
            this.Response.Write(sb.ToString());
            this.Response.End();
        }

        //if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        //{
        //    this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>btnEnable();</script>");
        //}

    }
}