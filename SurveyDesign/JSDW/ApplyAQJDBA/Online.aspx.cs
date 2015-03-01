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

public partial class JSDW_ApplyAQJDBA_Online : System.Web.UI.Page
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
                TC_AJBA_CZSG emp = dbContext.TC_AJBA_CZSG.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (emp != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(emp);
                }
                ViewState["FID"] = Request.QueryString["fid"];
            }
            BindControl();
            if (!string.IsNullOrEmpty(Request.QueryString["fAppId"]))
            {
                TC_AJBA_Record aj = dbContext.TC_AJBA_Record.Where(t => t.FAppId == Request.QueryString["fAppId"]).FirstOrDefault();
                ViewState["FAppId"] = aj.FAppId;
                ViewState["FPrjItemId"] = aj.FPrjItemId;
            }
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {
        //项目职位
        DataTable dt = rc.getDicTbByFNumber("112202");
        t_XMZW.DataSource = dt;
        t_XMZW.DataTextField = "FName";
        t_XMZW.DataValueField = "FNumber";
        t_XMZW.DataBind();
        //证件类型
        dt = rc.getDicTbByFNumber("112203");
        t_ZJLX.DataSource = dt;
        t_ZJLX.DataTextField = "FName";
        t_ZJLX.DataValueField = "FNumber";
        t_ZJLX.DataBind();
    }
    //保存
    private void saveInfo()
    {
        string fId = EConvert.ToString(ViewState["FID"]);
        TC_AJBA_CZSG Emp = new TC_AJBA_CZSG();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_AJBA_CZSG.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_AJBA_CZSG.InsertOnSubmit(Emp);
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