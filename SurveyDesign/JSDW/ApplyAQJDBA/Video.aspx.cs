using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaDAO;

public partial class JSDW_ApplyAQJDBA_Video : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["fid"]))
            {

            }
            else
            {
                TC_AJBA_Video emp = dbContext.TC_AJBA_Video.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fLinkId"]))
            {
                TC_AJBA_VideoSupply aj = dbContext.TC_AJBA_VideoSupply.Where(t => t.FId == Request.QueryString["fLinkId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
                ViewState["FLinkId"] = Request.QueryString["fLinkId"];
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_Video Emp = new TC_AJBA_Video();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_Video.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            Emp.FLinkId = EConvert.ToString(ViewState["FLinkId"]);
            dbContext.TC_AJBA_Video.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}