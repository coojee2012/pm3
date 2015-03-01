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
using Approve.RuleApp;
public partial class JSDW_applycbsjwt_Report : energyEntBasePage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            ShowEntInfo();
            showInfo();
        }
    }
    /// <summary>
    /// 显示企业信息
    /// </summary>
    void ShowEntInfo()
    {
        string fAppId = EConvert.ToString(Session["FAppId"]);
        var ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId
            && t.FEntType == 155)
            .Select(t => new { t.FName, t.FBaseInfoId }).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "k_");
            tool.fillPageControl(ent);
        }
    }
    //显示
    private void showInfo()
    {
        string fAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FId == fAppId)
            .Select(t => new { t.FName, t.FYear, t.FPrjId, t.FState }).FirstOrDefault();
        if (app != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(app);

            txtFPrjName.Text = db.CF_Prj_Data.Where(t => t.FAppId == fAppId).Select(t => t.FPrjName).FirstOrDefault();

            //已提交不能修改
            if (app.FState == 1 || app.FState == 6)
            {
                tool.ExecuteScript("btnEnable();");
            }
        }
    }
    /// <summary>
    /// 验证附件是否上传
    /// </summary>
    /// <returns></returns>
    bool IsUploadFile(int? FMTypeId, string FAppId)
    {
        var v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
            && t.FManageType == FMTypeId
            && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
                && o.FAppId == FAppId) < 1) > 0;
        return v;
    }
    //保存
    private void saveInfo()
    {
        RApp ra = new RApp();
        string dept = ComFunction.GetDefaultDept();
        SMS sms = new SMS();
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fAppId = EConvert.ToString(Session["FAppId"]);
        //设计端业务
        CF_App_List kclist = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
        if (kclist != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(kclist.FManageTypeId, fAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            kclist.FState = 1;
            kclist.FReportDate = DateTime.Now;
            kclist.FReportCount++;
            kclist.FIsDeleted = false;
            kclist.FToBaseinfoId = k_FBaseInfoId.Value;
            kclist.FBarCode = ra.GetBarCode(dept, "155");

            //清空下打回意见等
            CF_Prj_Data d = db.CF_Prj_Data.Where(t => t.FId == kclist.FLinkId).FirstOrDefault();
            if (d != null)
            {
                d.FDate2 = null;
                d.FInt1 = 0;//置为清空状态
                d.FTxt20 = "";
            }
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        db.SubmitChanges();
        Session["FIsApprove"] = 1;
        //发送系统消息
        sms.SendMessage(kclist.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的初步设计合同，请及时处理。");
        tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
