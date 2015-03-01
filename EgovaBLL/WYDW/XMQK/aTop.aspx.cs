using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;

public partial class WYDW_XMQK_aTop : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
            showdiv();
            setMessage();
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
    protected void bntExit_Click(object sender, EventArgs e)
    {
        Session.Remove("Fid");
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>top.close();</script>");
    }

    private void showdiv()
    {
        if (Session["GovLinkID"] != null)
        {
            BackMain.Style.Remove("display");
            BackMain.Style.Add("display", "none");
        }
    }

    //当前企业、当前业务
    private void setMessage()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<span>当前登录单位：");
        sb.Append(CurrentEntUser.EntName);

        if (Session["XMQK_XMBH"] != null)
        {
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;项目名称：");
            sb.Append(rc.GetSignValue("select XMMC from wy_xm_jbxx where XMBH = '" + Session["XMQK_XMBH"] + "'"));
        }

        sb.Append("</span>");
        lMsg.Text = sb.ToString();
    }
}