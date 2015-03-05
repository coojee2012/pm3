using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Approve.EntityBase;
using ProjectData;

public partial class JSDW_appmain_aIndex : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
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
        this.Session["FSystemId"] = "33010";
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
