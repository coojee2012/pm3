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
using Approve.EntityBase;
using Approve.RuleApp;
public partial class JSDW_appmain_Report : energyEntBasePage
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
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
            && t.FEntType == 15501)
            .Select(t => new { t.FName, t.FBaseInfoId }).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "k_");
            tool.fillPageControl(ent);
        }
        ent = db.CF_Prj_Ent.Where(t => t.FAppId == fAppId
          && t.FEntType == 126)
          .Select(t => new { t.FName, t.FBaseInfoId }).FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "j_");
            tool.fillPageControl(ent);
        }
    }
    //显示
    private void showInfo()
    {
        string fAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FLinkId == fAppId)
            .Select(t => new { t.FName, t.FYear, t.FLinkId, t.FState }).FirstOrDefault();
        if (app != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(app);

            txtFPrjName.Text = db.CF_Prj_Data.Where(t => t.FId == fAppId).Select(t => t.FPrjName).FirstOrDefault();



            //已提交不能修改
            if (app.FState > 0)
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
        //勘察端业务
        CF_App_List kclist = db.CF_App_List.Where(t => t.FLinkId == fAppId && t.FManageTypeId == 280).FirstOrDefault();
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
            kclist.FIsDeleted = false;
            kclist.FReportCount++;
            kclist.FToBaseinfoId = k_FBaseInfoId.Value;
            kclist.FBarCode = ra.GetBarCode(dept, "15501");
            //发送系统消息
            sms.SendMessage(kclist.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的勘察合同，请及时处理。");
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        //见证端业务|如果选择了见证单位
        if (!string.IsNullOrEmpty(j_FBaseInfoId.Value.Trim()))
        {
            CF_App_List jzlist = db.CF_App_List.Where(t => t.FLinkId == fAppId && t.FManageTypeId == 28001).FirstOrDefault();
            if (jzlist != null)
            {
                jzlist.FState = 1;
                jzlist.FReportDate = DateTime.Now;
                jzlist.FIsDeleted = false;
                jzlist.FReportCount++;
                jzlist.FToBaseinfoId = j_FBaseInfoId.Value;
                jzlist.FBarCode = ra.GetBarCode(dept, "126");
                //发送系统消息
                sms.SendMessage(jzlist.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的见证合同，请及时处理。");
            }
            else
            {
                CF_App_List lJZ = new CF_App_List();//见证企业业务
                lJZ.FId = Guid.NewGuid().ToString();
                lJZ.FLinkId = fAppId;
                lJZ.FBaseinfoId = CurrentEntUser.EntId;
                lJZ.FPrjId = kclist.FPrjId;
                lJZ.FName = t_FYear.Text + "年 " + rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='28001'");
                lJZ.FManageTypeId = 28001;
                lJZ.FwriteDate = DateTime.Now;
                lJZ.FYear = EConvert.ToInt(t_FYear.Text.Trim());
                lJZ.FMonth = DateTime.Now.Month;
                lJZ.FBaseName = CurrentEntUser.EntName;
                lJZ.FTime = DateTime.Now;
                lJZ.FCreateTime = DateTime.Now;
                lJZ.FCount = kclist.FCount;
                lJZ.FState = 1;
                lJZ.FReportDate = DateTime.Now;
                lJZ.FIsDeleted = false;
                lJZ.FReportCount++;
                lJZ.FToBaseinfoId = j_FBaseInfoId.Value;
                lJZ.FBarCode = ra.GetBarCode(dept, "126");
                db.CF_App_List.InsertOnSubmit(lJZ);
                //发送系统消息
                sms.SendMessage(lJZ.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + txtFPrjName.Text + "”工程的见证合同，请及时处理。");
            }
        }
        else
        {
            //删除见证单位的业务信息
            rc.PExcute("delete cf_App_list where flinkId='" + kclist.FLinkId + "' and FManageTypeId=28001");
        }
        db.SubmitChanges();
        Session["FIsApprove"] = 1;
        tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
    }


    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveInfo();
    }
}
