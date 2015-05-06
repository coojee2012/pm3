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

public partial class JSDW_ApplyAQJDBA_Lift_CZRY : System.Web.UI.Page
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
                TC_AJBA_QZSB_CZRY emp = dbContext.TC_AJBA_QZSB_CZRY.Where(t => t.ID == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fLinkId"]))
            {
                TC_AJBA_QZSB aj = dbContext.TC_AJBA_QZSB.Where(t => t.FId == Request.QueryString["fLinkId"]).FirstOrDefault();
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
        TC_AJBA_QZSB_CZRY Emp = new TC_AJBA_QZSB_CZRY();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_QZSB_CZRY.Where(t => t.ID == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.ID = fId;
            Emp.FPrjItemID = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppID = EConvert.ToString(ViewState["FAppId"]);
            Emp.FLinkID = EConvert.ToString(ViewState["FLinkId"]);
            Emp.Name = t_Name.Text.Trim();
            Emp.Trades = t_Trades.Text.Trim();
            Emp.CZZH = t_CZZH.Text.Trim();
            dbContext.TC_AJBA_QZSB_CZRY.InsertOnSubmit(Emp);
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