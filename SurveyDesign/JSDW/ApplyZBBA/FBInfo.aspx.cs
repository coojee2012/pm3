﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using Approve.RuleCenter;
using Tools;
using System.Data;
using EgovaBLL;

public partial class JSDW_ApplyZBBA_FBInfo : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FID"] = Request.QueryString["fid"];
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                TC_PBBG_FBYY pb = dbContext.TC_PBBG_FBYY.Where(t => t.FId == Convert.ToString(Request.QueryString["fid"])).FirstOrDefault();
                if (pb != null)
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.fillPageControl(pb, divSetup2);
                }
            }
            else
            {
                ViewState["FLinkId"] = Request.QueryString["fLinkId"];
                ViewState["FPrjId"] = Request.QueryString["fPrjId"];
                ViewState["FAppId"] = Request.QueryString["fAppId"];
                ViewState["BDId"] = Request.QueryString["BDId"];
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
        TC_PBBG_FBYY Emp = new TC_PBBG_FBYY();
        if (!string.IsNullOrEmpty(fId))
        {
            Emp = dbContext.TC_PBBG_FBYY.Where(t => t.FId == fId).FirstOrDefault();
        }
        else
        {
            fId = Guid.NewGuid().ToString();
            Emp.FId = fId;
            Emp.FPrjId = EConvert.ToString(ViewState["FPrjId"]);
            Emp.BDId = EConvert.ToString(ViewState["BDId"]);
            Emp.FLinkId = EConvert.ToString(ViewState["FLinkId"]);
            Emp.FAppId = EConvert.ToString(ViewState["FAppId"]);
            Emp.QYId = t_QYId.Value;
            dbContext.TC_PBBG_FBYY.InsertOnSubmit(Emp);
        }
        pageTool tool = new pageTool(this.Page);
        Emp = tool.getPageValue(Emp);
        EgovaDB db = new EgovaDB();
        dbContext.SubmitChanges();
        ViewState["FID"] = fId;
        ScriptManager.RegisterClientScriptBlock(up_Main, typeof(UpdatePanel), "js", "alert('保存成功');window.returnValue='1';", true);
        //     MyPageTool.showMessageAjax("保存成功ii", up_Main);
        //    MyPageTool.showMessageAndRunFunctionAjax("保存成功", "window.returnValue='1';", up_Main);
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        var result = (from t in dbContext.TC_PBBG_TBQY
                      where t.FId == this.t_FBId.Value
                      select t).SingleOrDefault();
        t_TBR.Text = result.QYMC;
        t_QYId.Value = result.QYId;
    }
}