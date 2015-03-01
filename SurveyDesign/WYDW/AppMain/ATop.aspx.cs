using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WYDW_AppMain_ATop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
            this.setMessage();
            RemoveBack();
        }
    }

    private void ShowInfo()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            return;
        }
        lit_back.HRef = "../main/Index.aspx";
        if (!string.IsNullOrEmpty(Request.QueryString["fbid"]))
        {
            lit_back.Visible = false;
        }
        HDetpNumber.Value = fDeptNumber;
        lDate.Text = "今天是：" + string.Format("{0:yyyy年MM月dd日}", DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString();
    }

    //当前企业、当前业务
    private void setMessage()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<span>当前登录单位：");
        sb.Append(CurrentEntUser.EntName);

        if (Session["FAppId"] != null)
        {
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;项目名称：");
            sb.Append(rc.GetSignValue("select XMMC from yw_wy_xm_jbxx where FAppID = '" + Session["FAppId"] + "'"));
        }

        if (this.Session["FManageTypeId"] != null)
        {
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;业务类型：");
            sb.Append(rc.GetSignValue("select fname from CF_Sys_ManageType where fnumber='" + Session["FManageTypeId"] + "'"));
        }
        sb.Append("</span>");
        lMsg.Text = sb.ToString();
    }

    //退出
    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session.Remove("FBaseId");
        Session.Remove("FType");
        Session.Remove("FUserId");
        Session.Remove("FUserRightId");
        Session.Remove("FMenuRoleId");
        Session.Remove("FRoleId");
        Session.Remove("FSystemId");
        Session.Remove("FBaseName");
        Session.Remove("FIsApprove");
        Session.Remove("FCanMod");
        Session.Remove("FAppId");
        Session.Remove("FBaseinfoId");
        Session.Remove("EntUserId");
        Session.Remove("FManageTypeId");
        Session.Remove("fly");
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>top.close();</script>");
    }

    protected void RemoveBack()
    {
        if (Session["GovLinkID"] != null && Session["GovLinkID"].ToString() == "1")
        {
            BackMain.Style.Add("display", "none");
        }
    }
}