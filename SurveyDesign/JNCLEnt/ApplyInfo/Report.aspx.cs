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
using System.Collections;


public partial class JNCLEnt_ApplyInfo_Report : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter(); ProjectDB db = new ProjectDB();
    RApp ra = new RApp(); SMS sms = new SMS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDep(); showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    public void bindDep()
    {
        string deptId = ComFunction.GetDefaultDept();
        StringBuilder sb = new StringBuilder();
        sb.Append("select case FLevel when 1 then FFullName when 2 then FName when 3 then FName end FName,");
        sb.Append("FNumber from cf_Sys_ManageDept ");
        sb.Append("where fnumber like '" + deptId + "%' ");
        sb.Append("and fname<>'市辖区' ");
        sb.Append("order by left(FNumber,4),flevel");
        DataTable dt = rc.GetTable(sb.ToString());
        t_FUpDeptName.DataSource = dt;
        t_FUpDeptName.DataTextField = "FName";
        t_FUpDeptName.DataValueField = "FNumber";
        t_FUpDeptName.DataBind();
    }

    public void showInfo()
    {
        string sql = "select * from CF_App_List where FId='" + Session["FAppId"].ToString() + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FYear.Text = dt.Rows[0]["FYear"].ToString();
            t_FName.Text = dt.Rows[0]["FName"].ToString();
        }
        t_DW.Text = CurrentEntUser.EntName;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fAppId = EConvert.ToString(Session["FAppId"]);
        //设计端业务
        CF_App_List appList = db.CF_App_List.Where(t => t.FId == fAppId).FirstOrDefault();
        SortedList[] sl = new SortedList[1];
        if (appList != null)
        {
            sl[0] = new SortedList();
            sl[0].Add("FID", appList.FId);
            sl[0].Add("FAppId", appList.FId);
            sl[0].Add("FBaseInfoId", appList.FBaseinfoId);
            sl[0].Add("FManageTypeId", appList.FManageTypeId);
            sl[0].Add("FListId", "912001001");
            sl[0].Add("FTypeId", "912001001");
            sl[0].Add("FLevelId", "912001");
            sl[0].Add("FIsPrime", 0);
            sl[0].Add("FAppTime", DateTime.Now);
            sl[0].Add("FIsNew", 0);
            sl[0].Add("FIsBase", 0);
            sl[0].Add("FIsTemp", 0);
            sl[0].Add("FUpDept", t_FUpDeptName.SelectedValue);
            sl[0].Add("FEmpId", appList.FLinkId);//CF_Prj_Data.FID
            sl[0].Add("FEmpName", "");
            sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            sl[0].Add("FLeadName", "");
            StringBuilder sb = new StringBuilder();

            sb.Append("update CF_App_List set FUpDeptId=" + t_FUpDeptName.SelectedValue + ",");
            sb.Append("ftime=getdate(),FState=1,FReportDate=getdate(),FIsDeleted=0 where fid = '" + fAppId + "'");
            rc.PExcute(sb.ToString());
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        if (ra.EntStartProcessKCSJ(appList.FBaseinfoId, fAppId, appList.FYear.ToString(), DateTime.Now.Month.ToString(), "220", fDeptNumber, t_FUpDeptName.SelectedValue, sl))
        {
            Session["FIsApprove"] = 1;
            tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
        }
    }
    public void readOnly()
    {
        btnSave.Enabled = false;
    }
}