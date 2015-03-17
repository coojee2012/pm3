using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Data;
using EgovaBLL;
using EgovaDAO;
using Tools;
using Approve.RuleApp;
using Approve.RuleCenter;

public partial class JSDW_ApplySGXKZGL_Report : System.Web.UI.Page
{
    EgovaDB db = new EgovaDB();
    RCenter rc = new RCenter();
    string fAppId = "";
    WorkFlowApp wfApp = new WorkFlowApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        fAppId = EConvert.ToString(Session["FAppId"]);
        if (!IsPostBack)
        {

            BindControl();
            ShowEntInfo();
            showInfo();
            pageTool tool1 = new pageTool(this.Page);
        }
    }
    //绑定
    private void BindControl()
    {
        //备案部门
        string deptId = ComFunction.GetDefaultDept();
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then FName when 3 then FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市属' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        p_FManageDeptId.DataSource = dt;
        p_FManageDeptId.DataTextField = "FName";
        p_FManageDeptId.DataValueField = "FNumber";
        p_FManageDeptId.DataBind();
    }
    /// <summary>
    /// 显示企业信息
    /// </summary>
    void ShowEntInfo()
    {
        // string fAppId = EConvert.ToString(Session["FAppId"]);
        var tt = from t in db.CF_Ent_BaseInfo
                 join a in db.CF_App_List
                 on t.FId equals a.FBaseinfoId
                 where a.FId == fAppId
                 select new
                 {
                     t.FName,
                     FBaseInfoId = t.FId
                 };
        var ent = tt.FirstOrDefault();
        if (ent != null)
        {
            pageTool tool = new pageTool(this.Page, "k_");
            tool.fillPageControl(ent);
        }
        TC_SGXKZ_PrjInfo qa = db.TC_SGXKZ_PrjInfo.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        TC_Prj_Info prjInfo = db.TC_Prj_Info.Where(t => t.FId == qa.PrjId).FirstOrDefault();
        govd_FRegistDeptId.fNumber = prjInfo.AddressDept;
        if (!string.IsNullOrEmpty(prjInfo.AddressDept))
        {
            if (prjInfo.AddressDept.Length == 2)
            {
                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
            }
            else if (prjInfo.AddressDept.Length == 4)
            {
                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(prjInfo.AddressDept)).Select(d => d.FName).FirstOrDefault(), prjInfo.AddressDept));
            }
            else if (prjInfo.AddressDept.Length == 6)
            {
                string sj = prjInfo.AddressDept.Substring(0, 4);

                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(sj)).Select(d => d.FName).FirstOrDefault(), sj));
                ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(prjInfo.AddressDept)).Select(d => d.FName).FirstOrDefault(), prjInfo.AddressDept));
            }
        }

    }
    //显示
    private void showInfo()
    {
        // string fAppId = EConvert.ToString(Session["FAppId"]);
        var app = db.CF_App_List.Where(t => t.FId == fAppId)
            .Select(t => new { t.FName, t.FYear, t.FPrjId, t.FState, t.FUpDeptId }).FirstOrDefault();
        if (app != null)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            tool.fillPageControl(app);
            txtFPrjName.Text = db.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == fAppId).Select(t => t.PrjItemName).FirstOrDefault();
            //已提交不能修改
            if (app.FState == 1 || app.FState == 6)
            {
                govd_FRegistDeptId.fNumber = app.FUpDeptId.ToString();
                if (app.FUpDeptId.ToString().Length == 2)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
                }
                else if (app.FUpDeptId.ToString().Length == 4)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(app.FUpDeptId.ToString())).Select(d => d.FName).FirstOrDefault(), app.FUpDeptId.ToString()));
                }
                else if (app.FUpDeptId.ToString().Length == 6)
                {
                    ddlLevel.Items.Clear();
                    ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(app.FUpDeptId.ToString())).Select(d => d.FName).FirstOrDefault(), app.FUpDeptId.ToString()));
                }
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
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.Report();
    }
    private void Report()
    {
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            MyPageTool.showMessage("系统出错,请配置默认管理部门", this.Page);
            return;
        }
        if (!WFApp.ValidateReport(fAppId))
        {
            MyPageTool.showMessage("该条业务已经上报或者不符合上报条件，不能继续上报操作", this.Page);
            return;
        }

        string fSystemId = CurrentEntUser.URSystemId;
        if (string.IsNullOrEmpty(fSystemId))
        {
            MyPageTool.showMessage("系统出错,获取不到当前登录系统的编码", this.Page);
            return;
        }

        //验证各个项目环节
        var dataOne = db.TC_SGXKZ_JSYDGHXKZ.FirstOrDefault(t => t.FId == fAppId);
        if (dataOne == null || string.IsNullOrEmpty(dataOne.YDGHXKZBH))
        {
            MyPageTool.showMessage("请完善项目环节:[建设用地规划许可证]", this.Page);
            return;
        }
        var dataTwo = db.TC_SGXKZ_JSGCGHXKZ.FirstOrDefault(t => t.FId == fAppId);
        if (dataTwo == null || string.IsNullOrEmpty(dataTwo.GCGHXKZBH))
        {
            MyPageTool.showMessage("请完善项目环节:[建设工程规划许可证]", this.Page);
            return;
        }

        var dataThree = db.TC_SGXKZ_SGTSC.FirstOrDefault(t => t.FId == fAppId);
        if (dataThree == null
            || string.IsNullOrEmpty(dataThree.SGTSCHGSBH)
            || string.IsNullOrEmpty(dataThree.SGTSCJGId)
            || string.IsNullOrEmpty(dataThree.SGTSCZZJGDM)
            || dataThree.SCWCRQ == null
            || string.IsNullOrEmpty(dataThree.KCDWId)
            || string.IsNullOrEmpty(dataThree.KCDWZZJGDM)
            || string.IsNullOrEmpty(dataThree.SJDWId)
            || string.IsNullOrEmpty(dataThree.SJDWZZJGDM)
            || dataThree.YCSCSFTG == null
            || dataThree.YCSCWFTS == null
            || string.IsNullOrEmpty(dataThree.YCSCWFTM))
        {
            MyPageTool.showMessage("请完善项目环节:[施工图审查信息]", this.Page);
            return;
        }

        var dataFour = db.TC_SGXKZ_JDSX.FirstOrDefault(t => t.FId == fAppId);
        if (dataFour == null || string.IsNullOrEmpty(dataFour.ZLBABH) || string.IsNullOrEmpty(dataFour.AQBABH))
        {
            MyPageTool.showMessage("请完善项目环节:[质量安全监督手续]", this.Page);
            return;
        }

        var dataFive = db.TC_SGXKZ_ZJBH.FirstOrDefault(t => t.FId == fAppId);
        if (dataFive == null || dataFive.ISDBS == null || string.IsNullOrEmpty(dataFive.ZJBH) || string.IsNullOrEmpty(dataFive.JF) || string.IsNullOrEmpty(dataFive.YF))
        {
            MyPageTool.showMessage("请完善项目环节:[资金保函或证明]", this.Page);
            return;
        }
        var dataSix = db.TC_SGXKZ_QTZL.FirstOrDefault(t => t.FId == fAppId);
        if (dataSix == null || string.IsNullOrEmpty(dataSix.SGTJ) || string.IsNullOrEmpty(dataSix.CNS))
        {
            MyPageTool.showMessage("请完善项目环节:[其它材料]", this.Page);
            return;
        }

        string fNumber = ddlLevel.SelectedValue;

        if (WFApp.Report(fAppId, fSystemId, fDeptNumber, fNumber))
        {
            Session["FIsApprove"] = 1;
            MyPageTool.showMessage("上报成功", this.Page);
            showInfo();
        }
        else
        {
            MyPageTool.showMessage("上报失败", this.Page);
        }
    }
}