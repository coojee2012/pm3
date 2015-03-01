using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using EgovaBLL;
using EgovaDAO;
using Tools;
using Approve.RuleApp;
using Approve.RuleCenter;

public partial class JSDW_ApplyZBBA_ZBWJReport : System.Web.UI.Page
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
        TC_ZBWJ_Record qa = db.TC_ZBWJ_Record.Where(t => t.FAppId.Equals(fAppId)).FirstOrDefault();
        ViewState["ProjectName"] = qa.ProjectName;
        TC_Prj_Info prjInfo = db.TC_Prj_Info.Where(t => t.FId == qa.FPrjId).FirstOrDefault();
        govd_FRegistDeptId.fNumber = prjInfo.AddressDept;
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
            string sj = prjInfo.AddressDept.Substring(0,4);

            ddlLevel.Items.Clear();
            ddlLevel.Items.Insert(0, new ListItem("四川省", "51"));
            ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(sj)).Select(d => d.FName).FirstOrDefault(), sj));
            ddlLevel.Items.Insert(0, new ListItem(db.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(prjInfo.AddressDept)).Select(d => d.FName).FirstOrDefault(), prjInfo.AddressDept));
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
            txtFPrjName.Text = EConvert.ToString(ViewState["ProjectName"]);
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
        //var v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
        //    && t.FManageType == FMTypeId
        //    && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
        //        && o.FAppId == FAppId) < 1) > 0;
        //return v;
        return false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  this.saveInfo();
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
        //if (!ValidateCallVideo(fAppId))
        //{
        //    MyPageTool.showMessage("是否点名处未设置或者设置超过1个点位，不能上报", this.Page);
        //    return;
        //}
        //if (!ValidateVideo(fAppId))
        //{
        //    MyPageTool.showMessage("摄像头个数如果未达到规定数量，不能上报", this.Page);
        //    return;
        //}
        string fSystemId = CurrentEntUser.URSystemId;
        if (string.IsNullOrEmpty(fSystemId))
        {
            MyPageTool.showMessage("系统出错,获取不到当前登录系统的编码", this.Page);
            return;
        }
        string fNumber = ddlLevel.SelectedValue;
        //if (ddlLevel.SelectedValue == "1")//省级
        //{
        //    if (fNumber.Length < 2)
        //    {
        //        MyPageTool.showMessage("上报部门不存在省级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 2);
        //}
        //else if (ddlLevel.SelectedValue == "2")//市级
        //{
        //    if (fNumber.Length < 4)
        //    {
        //        MyPageTool.showMessage("上报部门不存在市级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 4);
        //}
        //else if (ddlLevel.SelectedValue == "3")//县级
        //{
        //    if (fNumber.Length < 6)
        //    {
        //        MyPageTool.showMessage("上报部门不存在县级", this.Page);
        //        return;
        //    }
        //    fNumber = fNumber.Substring(0, 6);
        //}
        
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