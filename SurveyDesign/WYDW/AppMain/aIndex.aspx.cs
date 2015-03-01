using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using Approve.RuleCenter;
using ProjectData;

public partial class WYDW_Project_Main_aIndex : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string mainSrc = "../ApplyJBXX/JBXXInfo.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置默认页面的src
        if (Session["FManageTypeId"] != null)
        {
            switch (Session["FManageTypeId"].ToString())
            {
                case "14402": mainSrc = "../ApplyXMSQ/XMSQInfo.aspx"; break;
                case "14403": mainSrc = "../ApplyRYQK/RYQKList.aspx"; break;
                case "14404": mainSrc = "../ApplyHTBA/HTBAInfo.aspx"; break;
                case "14405": mainSrc = "../ApplyYWH/YWHInfo.aspx"; break;
                case "14406": mainSrc = "../ApplyCWQK/CWQKInfo.aspx"; break;
            }
        }
        if (!string.IsNullOrEmpty(Request.QueryString["fbid"]) || !string.IsNullOrEmpty(Request.QueryString["fBaseId"]))
        {
            SetSession();
            
        }
    }
    private void SetSession()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["isPrint"]) && Request.QueryString["isPrint"] == "1")
        {
            ProjectDB db = new ProjectDB();
            string ReportServer = rc.GetSysObjectContent("_ReportServer");
            if (string.IsNullOrEmpty(ReportServer))
            {
                ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
            }
            string FManageTypeId = EConvert.ToString(Request["fmid"]);
        }
        this.Session["FSystemId"] = "144";
        if (Request["fbid"] != null && Request["fbid"] != "")
        {
            Session["FBaseId"] = Request["fbid"];
            CurrentEntUser.EntId = Request["fbid"];
            Session["FUserId"] = rc.GetSignValue(EntityTypeEnum.EsUser, "FID", "FBaseInfoId='" + Request["fbid"] + "'");
        }
        if (Request["frid"] != null && Request["frid"] != "")
        {
            this.Session["FRoleId"] = Request["frid"];
            this.Session["FMenuRoleId"] = Request["frid"];
        }
        else
        {
            this.Session["FMenuRoleId"] = null;
        }

        if (Request["fmid"] != null && Request["fmid"] != "")
        {
            this.Session["FManageTypeId"] = Request["fmid"];
        }

        if (Request["faid"] != null && Request["faid"] != "")
        {
            this.Session["FAppId"] = Request["faid"];
        }
        this.Session["FIsApprove"] = "1";
    }
}