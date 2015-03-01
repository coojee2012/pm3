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
public partial class KC_ApplyKCXXBA_Report : EntAppPage
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected override void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("tab();");
        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove();</script>");
        }
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            btnSave.Attributes["onclick"] = "return checkInfo();";
            BindControl();
            showInfo();
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
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        p_FManageDeptId.DataSource = dt;
        p_FManageDeptId.DataTextField = "FName";
        p_FManageDeptId.DataValueField = "FNumber";
        p_FManageDeptId.DataBind();
    }

    //显示
    private void showInfo()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var app = (from t in db.CF_App_List
                   join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                   where t.FId == FAppId
                   select new
                   {
                       t.FName,
                       t.FYear,
                       t.FLinkId,
                       t.FBaseName,
                       t.FPrjId,
                       d.FPrjName,
                       d.FInt1,
                   }).FirstOrDefault();
        if (app != null)
        {
            t_FName.Text = app.FName;
            p_FPrjName.Text = app.FPrjName;
            p_FManageDeptId.SelectedValue = app.FInt1.ToString();


            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(st => st.FPrjId == app.FPrjId).FirstOrDefault();
            if (stop != null)
            {
                btnSave.Enabled = false;
                btnSave.ToolTip = "该项目已被中止，所有业务停止进行。"; 
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
    public void Report()
    {
        string FAppId = EConvert.ToString(Session["FAppId"]);
        RCenter rc = new RCenter();
        pageTool tool = new pageTool(this.Page);
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            tool.showMessage("系统出错,请配置默认管理部门");
            return;
        }
        //验证是否选择了主要负责人和勘察人员
        if (db.CF_Prj_Data.Count(t => t.FCreateTime == t.FTime && t.FAppId == FAppId) > 0)
        {
            tool.showMessage("请先保存基本信息！");
            return;
        }

        var app = (from t in db.CF_App_List
                   join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                   where t.FId == FAppId
                   select new
                   {
                       t.FId,
                       t.FLinkId,
                       t.FYear,
                       t.FBaseinfoId,
                       t.FManageTypeId,
                       d.FPrjName,
                       d.FTxt1,//建设单位 
                   }).FirstOrDefault();
        SortedList[] sl = new SortedList[1];
        if (app != null)
        {
            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, FAppId))
            {
                tool.showMessage("“附件上传”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            sl[0] = new SortedList();
            sl[0].Add("FID", app.FId);
            sl[0].Add("FAppId", app.FId);
            sl[0].Add("FBaseInfoId", app.FBaseinfoId);
            sl[0].Add("FManageTypeId", app.FManageTypeId);
            sl[0].Add("FListId", "19301");
            sl[0].Add("FTypeId", "1930100");
            sl[0].Add("FLevelId", "1930100");
            sl[0].Add("FIsPrime", 0);

            //sl.Add("FAppDeptId", row["FAppDeptId"].ToString());
            //sl.Add("FAppDeptName", row["FAppDeptName"].ToString());
            sl[0].Add("FAppTime", DateTime.Now);
            sl[0].Add("FIsNew", 0);
            sl[0].Add("FIsBase", 0);
            sl[0].Add("FIsTemp", 0);
            sl[0].Add("FUpDept", p_FManageDeptId.SelectedValue);
            sl[0].Add("FEmpId", app.FLinkId);//CF_Prj_Data.FID
            sl[0].Add("FEmpName", app.FPrjName);
            sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            sl[0].Add("FLeadName", app.FTxt1);
            StringBuilder sb = new StringBuilder();

            sb.Append("update CF_App_List set FUpDeptId=" + p_FManageDeptId.SelectedValue + ",");
            sb.Append("ftime=getdate() where fid = '" + FAppId + "'");
            rc.PExcute(sb.ToString());

            string fsystemid = CurrentEntUser.SystemId;
            
            RApp ra = new RApp();
            if (ra.EntStartProcessKCSJ(app.FBaseinfoId, FAppId, app.FYear.ToString(), DateTime.Now.Month.ToString(), fsystemid, fDeptNumber, p_FManageDeptId.SelectedValue, sl))
            {
                sb.Remove(0, sb.Length);

                this.Session["FIsApprove"] = 1;




                tool.showMessageAndRunFunction("上报成功！", "location.href=location.href");
            }

        }
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Report();
    }
}
