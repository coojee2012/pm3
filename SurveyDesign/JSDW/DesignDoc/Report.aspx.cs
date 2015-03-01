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
public partial class JSDW_DesignDoc_Report : Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("tab();");
        if (Session["FIsApprove"] != null && Session["FIsApprove"].ToString() == "1")
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>FIsApprove();</script>");
        }
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
                   where t.FId == FAppId
                   select new
                   {
                       t.FName,
                       t.FYear,
                       t.FLinkId,
                       t.FBaseName,
                       t.FPrjId,
                       t.FState,
                   }).FirstOrDefault();
        pageTool tool = new pageTool(this.Page, "p_");
        if (app != null)
        {
            t_FName.Text = app.FName;
            //已提交不能修改
            if (app.FState == 1 || app.FState == 6)
            {
                tool.ExecuteScript("btnEnable();");
            }
            //显示工程信息
            CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == app.FPrjId).FirstOrDefault();
            if (prj != null)
            {
                tool.fillPageControl(prj);
            }
        }
    }
    /// <summary>
    /// 验证附件是否上传
    /// </summary>
    /// <returns></returns>
    bool IsUploadFile(int? FMTypeId, string FAppId)
    {
        CF_Prj_BaseInfo prj = (from p in db.CF_Prj_BaseInfo
                               join a in db.CF_App_List on p.FId equals a.FPrjId
                               where a.FId == FAppId
                               select p).FirstOrDefault();
        var v =false ;
        if (prj != null)
        {
             v = db.CF_Sys_PrjList.Count(t => t.FIsMust == 1
          && t.FManageType == FMTypeId && t.FIsPrjType.Contains(prj.FType.ToString())
          && db.CF_AppPrj_FileOther.Count(o => o.FPrjFileId == t.FId
              && o.FAppId == FAppId) < 1) > 0;
        }

      
        return v;
    }
    public void Report()
    {
        RCenter rc = new RCenter();


        pageTool tool = new pageTool(this.Page);
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            tool.showMessage("系统出错,请配置默认管理部门");
            return;
        }
        string FAppId = EConvert.ToString(Session["FAppId"]);
        var app = (from t in db.CF_App_List
                   where t.FId == FAppId
                   select t).FirstOrDefault();
        SortedList[] sl = new SortedList[1];
        if (app != null)
        {

            //验证必需的附件是否上传
            if (IsUploadFile(app.FManageTypeId, FAppId))
            {
                tool.showMessage("“上传行政批文、上传设计文件”菜单中存在未上传的附件(必需上传的)，请先上传！");
                return;
            }
            CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == app.FPrjId).FirstOrDefault();
            if (prj != null)
            {
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
                sl[0].Add("FEmpId", prj.FId);

                sl[0].Add("FEmpName", prj.FPrjName);
                //存设计单位
                var s = (from t in db.CF_Prj_Ent
                         join a in db.CF_App_List on t.FAppId equals a.FId
                         where a.FPrjId == app.FPrjId && a.FManageTypeId == 291 && t.FEntType == 155 && a.FState == 6
                         select new
                         {
                             t.FId,
                             t.FBaseInfoId,
                             t.FName,
                             t.FLevelName,
                             t.FCertiNo,
                             t.FMoney,
                             t.FPlanDate,
                             t.FAppId
                         }).FirstOrDefault();
                if (s != null)
                {
                    sl[0].Add("FLeadId", s.FBaseInfoId);
                    sl[0].Add("FLeadName", s.FName);
                }
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
    }
    //保存按钮
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Report();
    }
}
