using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;
using EgovaDAO;
using Tools;
using System.Data;

public partial class JSDW_ApplySGXKZGL_ProjectInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindControl();
            if (!string.IsNullOrEmpty(EConvert.ToString(Session["FAppId"])))
            {
                ViewState["FAppId"] = EConvert.ToString(Session["FAppId"]);
            }
            ShowTitle();
            pageTool tool1 = new pageTool(this.Page);
            if (EConvert.ToInt(Session["FIsApprove"]) != 0)
            {
                tool1.ExecuteScript("btnEnable();");
            }
        }
    }
    void BindControl()
    {
        //项目类型
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_ProjectType.DataSource = dt;
        t_ProjectType.DataTextField = "FName";
        t_ProjectType.DataValueField = "FNumber";
        t_ProjectType.DataBind();

        //建筑性质
        dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();

        //立项级别
        dt = rc.getDicTbByFNumber("112204");
        t_ProjectLevel.DataSource = dt;
        t_ProjectLevel.DataTextField = "FName";
        t_ProjectLevel.DataValueField = "FNumber";
        t_ProjectLevel.DataBind();

        //立项分类

    }

    private void ShowTitle()
    {
        EgovaDB dbContext = new EgovaDB();
        TC_SGXKZ_PrjInfo qa = dbContext.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        ViewState["FPrjItemId"] = qa.FPrjItemId;
        TC_SGXKZ_ProjectInfo sp = dbContext.TC_SGXKZ_ProjectInfo.Where(t => t.FAppId == EConvert.ToString(ViewState["FAppId"])).FirstOrDefault();
        if (sp != null)
        {
            txtFId.Value = sp.FId;
        }
        
        pageTool tool = new pageTool(this.Page, "t_");
        tool.fillPageControl(sp);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fId = txtFId.Value;
        TC_SGXKZ_ProjectInfo qa = new TC_SGXKZ_ProjectInfo();
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(fId))
        {
            qa = dbContext.TC_SGXKZ_ProjectInfo.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            qa.FId = fId;
            qa.FprjItemId = EConvert.ToString(ViewState["FPrjItemId"]);
            qa.FAppId = EConvert.ToString(ViewState["FAppId"]);
            dbContext.TC_SGXKZ_ProjectInfo.InsertOnSubmit(qa);
        }
        qa = tool.getPageValue(qa);
        dbContext.SubmitChanges();
        //  showPrjData();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
    }
}