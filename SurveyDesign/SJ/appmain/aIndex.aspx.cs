using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.RuleCenter;
using Approve.EntityBase;

public partial class JSDW_appmain_aIndex : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["fbid"]) || !string.IsNullOrEmpty(Request.QueryString["fBaseId"]))
        {
            SetSession();
        }
        if (!IsPostBack)
        {

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

            //if (FManageTypeId == "280")
            //{ //切换对就应的业务菜单
            //    FManageTypeId = "281";
            //}

            if (FManageTypeId == "414" || FManageTypeId == "424")
            { //数据两边无占位符，需要特殊处理
                FManageTypeId = "411";
                var v = (from t in db.Menu
                         where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                         && t.FIsDeleted == false && t.FLevel == 4 && (t.FName == "报表打印" || t.FQUrl.Contains("cpt"))
                         orderby t.FOrder, t.FCreateTime descending
                         select string.Format(t.FQUrl + "?FBaseId=" + Request.QueryString["fBaseId"] + "&FAppId=" + Request.QueryString["faid"])).FirstOrDefault();

                Response.Redirect(v);
                return;
            }



            var v1 = (from t in db.Menu
                      where t.FManageTypeId == FManageTypeId && t.FIsDis == 1
                      && t.FIsDeleted == false && t.FLevel == 4 && (t.FName == "报表打印" || t.FQUrl.Contains("cpt"))
                      orderby t.FOrder, t.FCreateTime descending
                      select string.Format(t.FQUrl, ReportServer, "&FBaseId=" + Request.QueryString["fBaseId"] + "&FAppId=" + Request.QueryString["faid"])).FirstOrDefault();
            Response.Redirect(v1);
            return;
        }
        this.Session["FSystemId"] = "15501";
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

        if (Request["fuid"] != null && Request["fuid"] != "")
        {
            this.Session["DFUserId"] = Request["fuid"];
            string fCanMod = rc.GetSignValue(EntityTypeEnum.EsUser, "FCanMod", "FId='" + this.Session["DFUserId"].ToString() + "'");
            if (fCanMod != null && fCanMod == "1")
            {
                this.Session["FCanMod"] = 1;
            }
            else
            {
                this.Session["FCanMod"] = 0;
            }
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
