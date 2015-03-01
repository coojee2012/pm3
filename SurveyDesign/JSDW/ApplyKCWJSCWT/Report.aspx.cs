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
public partial class JSDW_appmain_Report : EntAppPage
{
    ProjectDB db = new ProjectDB();
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string fAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FLinkId == fAppId).Select(t => new
        {
            t.FName,
            t.FYear,
            t.FLinkId
        }).FirstOrDefault();
        if (app != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(app);


        }


        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FId equals d.FAppId
                 where a.FId == fAppId
                 select new
                 {
                     a.FName,
                     a.FYear,
                     a.FLinkId,
                     d.FPrjName
                 }).FirstOrDefault();
        if (v != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(v);

            //审图机构
            var s = (from t in db.CF_Prj_Ent
                     where t.FAppId == fAppId && t.FEntType == 145
                     select new { t.FName, t.FBaseInfoId }).FirstOrDefault();
            if (s != null)
            {
                s_FName.Text = s.FName;
                s_FBaseinfoId.Value = s.FBaseInfoId;
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
        CF_App_List app = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, fAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            app.FState = 1;
            app.FReportDate = DateTime.Now;
            app.FToBaseinfoId = s_FBaseinfoId.Value;
            app.FBarCode = ra.GetBarCode(dept, "145");
            //发送系统消息
            sms.SendMessage(app.FToBaseinfoId, "“" + CurrentEntUser.EntName + "”给您发来了“" + t_FPrjName.Text + "”工程的勘察文件审查" + (app.FReportCount > 1 ? ("[" + app.FReportCount + "审]") : "") + "合同，请及时处理。");
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
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
