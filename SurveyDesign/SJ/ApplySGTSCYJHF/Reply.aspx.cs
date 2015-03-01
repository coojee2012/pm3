using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using Approve.RuleApp;

public partial class SJ_ApplySGTSCYJHF_Reply : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    public string ReportServer = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportServer = db.getSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        pageTool tool = new pageTool(this.Page);
        if (!IsPostBack)
        {
            btnReport.Attributes["onclick"] = "return checkInfo();";
            BindControl();
            showInfo();
        }
    }

    //绑定
    private void BindControl()
    {

    }

    //显示
    private void showInfo()
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        string FLinkId = Request.QueryString["FLinkId"];
        var vv = (from a in db.CF_App_List
                  join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                  join p in db.CF_Prj_BaseInfo on d.FPrjId equals p.FId
                  where a.FManageTypeId == 30103 && a.FState > 0 && d.FId == FLinkId
                  select new
                  {
                      a.FId,
                      d.FPrjName,
                      d.FPrjId,
                      p.FType, //工程类别 
                      a.FState, //技术性审查结果 
                      SGTBaseName = a.FBaseName,
                      a.FBaseName,
                      a.FBaseinfoId,
                  }).FirstOrDefault();
        if (vv != null)
        {
            GCMC.Text = vv.FPrjName;
            JSDW.Text = vv.FBaseName;
            JG.Text = (vv.FState == 3 ? "<tt>不合格</tt>" : "<font color='green'>合格</font>");


            //告知书链接
            string p = "", nn = "告知书";
            if (vv.FState == 3)
            { //不合格，打告知书
                p = "SCYJGZS-JZGC.cpt";//房屋建筑
                if (vv.FType == 2000102)//市政基础
                    p = "SCYJGZS-SZGC.cpt";
            }
            else if (vv.FState == 6)
            { //合格，打合格书
                p = "SCHGS-JZGC.cpt";//房屋建筑
                if (vv.FType == 2000102)//市政基础
                    p = "SCHGS-SZGC.cpt";
                nn = "合格书";
            }
            a_GZS.HRef = ReportServer + p + "&FAppId=" + vv.FId + "&FBaseId=" + vv.FBaseinfoId + "&FPrjId=" + vv.FPrjId + "&PrjId=" + vv.FPrjId;
            a_GZS.InnerText = "查看施工图设计文件审查意见" + nn;
        }

        pageTool tool = new pageTool(this.Page);
        var v = (from t in db.CF_Prj_Reply
                 where t.FLinkId == FLinkId && t.FBaseinfoId == FBaseinfoId && t.FType == 1
                 select t).FirstOrDefault();
        if (v != null)
        {
            tool.fillPageControl(v);
            ViewState["FID"] = v.FID;

            if (v.FState >= 1)
            {
                btnSave.Visible = btnReport.Visible = false;
                ta_SGT.Visible = true;

                if (v.FState != 2)
                {
                    t_FTxt.Text = "<font color='#AAAAAA'>暂无</font>";
                }
            }
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="isReport">是否提交</param>
    private void saveInfo(bool isReport)
    {
        string FBaseinfoId = CurrentEntUser.EntId;
        pageTool tool = new pageTool(this.Page);
        CF_Prj_Reply r = db.CF_Prj_Reply.Where(t => t.FID == EConvert.ToString(ViewState["FID"])).FirstOrDefault();
        if (r == null)
        {
            r = new CF_Prj_Reply();
            r.FID = Guid.NewGuid().ToString();
            r.FIsDeleted = 0;
            r.FCreateTime = DateTime.Now;
            r.FTime = DateTime.Now;
            r.FType = 1; //审查意见回复
            r.FLinkId = Request.QueryString["FLinkId"];
            r.FBaseinfoId = FBaseinfoId;
            db.CF_Prj_Reply.InsertOnSubmit(r);
            ViewState["FID"] = r.FID;
        }
        r.FState = isReport ? 1 : 0;//1:已提交；0:暂存
        r = tool.getPageValue(r);
        db.SubmitChanges();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue=1;");
    }

    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo(false);
    }

    //提交按钮
    protected void btnReport_Click(object sender, EventArgs e)
    {
        saveInfo(true);
        btnSave.Visible = btnReport.Visible = false;
        ta_SGT.Visible = true;
    }
}
