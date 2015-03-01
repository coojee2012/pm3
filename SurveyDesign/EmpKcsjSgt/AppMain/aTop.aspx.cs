using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;

public partial class EvaluateEntApp_main_top : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
            this.setMessage();
        }
    }
    private void ShowInfo()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            return;
        }
        lit_back.HRef = "../Main/index.aspx";
        HDetpNumber.Value = fDeptNumber;
        lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
    }

    //当前企业、当前业务
    private void setMessage()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<span>当前登录单位：");
        sb.Append(EConvert.ToString(Session["FBaseName"]));
        if (this.Session["FManageTypeId"] != null)
        {
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;业务类型：");
            sb.Append(rc.GetSignValue("select fname from CF_Sys_ManageType where fnumber='" + Session["FManageTypeId"] + "'"));
        }
        sb.Append("</span>");
        this.lMsg.Text = sb.ToString();
    }

    //退出
    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session["FBaseId"] = null;
        Session["FType"] = null;
        Session["FUserId"] = null;
        Session["FUserRightId"] = null;
        Session["FMenuRoleId"] = null;
        Session["FSystemId"] = null;
        Session["FBaseName"] = null;
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>top.close();</script>");
    }
}
