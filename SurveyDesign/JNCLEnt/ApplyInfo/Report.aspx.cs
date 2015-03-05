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
    Share sh = new Share(); 
    RCenter rc = new RCenter(); 
    ProjectDB db = new ProjectDB();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
            //bindDep(); 
            EnabledControl();
        }
    }

    //public void bindDep()
    //{
    //    string upDep = sh.GetSignValue("select FManageDeptId from cf_sys_user where fid='" + CurrentEntUser.UserId + "' ");
    //    DataTable dt = ra.GetCanReportDeptXM(upDep, "1122", hfFManageDeptId.Value, ComFunction.GetDefaultDept());
    //    t_FUpDeptName.DataSource = dt;
    //    t_FUpDeptName.DataTextField = "fname";
    //    t_FUpDeptName.DataValueField = "fnumber";
    //    t_FUpDeptName.DataBind();
    //    if (!string.IsNullOrEmpty(upDep))
    //        t_FUpDeptName.SelectedValue = upDep;
    //}
    public void showInfo()
    {
        string guid = Session["FAppId"].ToString();
        if (!string.IsNullOrEmpty(guid))
        {
            var appList = db.CF_App_List.FirstOrDefault(x => x.FId == guid);
            if (appList != null)
            {
                t_FYear.Text = appList.FYear.ToString();
                t_FName.Text = appList.FName;
            }
            if (!string.IsNullOrEmpty(YWBM))
            {
                string sql = "select top 1 SBBM from YW_JNCL_PRODUCT where YWBM='" + YWBM + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    string upDep = sh.GetSignValue("select QYSZD from YW_JNCL_QYJBXX where YWBM='" + YWBM + "' ");
                    DataTable dt = ra.GetCanReportDeptXM(upDep, "1122", appList.FManageTypeId.Value.ToString(), ComFunction.GetDefaultDept());
                    t_FUpDeptName.DataSource = dt;
                    t_FUpDeptName.DataTextField = "fname";
                    t_FUpDeptName.DataValueField = "fnumber";
                    t_FUpDeptName.DataBind();
                    if (!string.IsNullOrEmpty(row["SBBM"].ToString()))
                        t_FUpDeptName.SelectedValue = row["SBBM"].ToString();
                }
            }
        }
    }
    //public void showInfo()
    //{


    //    string sql = "select * from CF_App_List where FId='" + Session["FAppId"].ToString() + "' ";
    //    DataTable dt = rc.GetTable(sql);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        t_FYear.Text = dt.Rows[0]["FYear"].ToString();
    //        t_FName.Text = dt.Rows[0]["FName"].ToString();
    //        hfFManageDeptId.Value = dt.Rows[0]["FMangeDeptId"].ToString();
    //    }
    //    t_DW.Text = CurrentEntUser.EntName;
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(t_FUpDeptName.SelectedValue))
        {
            tool.showMessage("上报部门不能为空");
            return;
        }
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
            sl[0].Add("FListId", "1930100");
            sl[0].Add("FTypeId", "1930100");
            sl[0].Add("FLevelId", "1930100");
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
            sb.AppendFormat("update YW_JNCL_PRODUCT set SBBM='{0}',SBRQ='{2}' where YWBM='{1}';", t_FUpDeptName.SelectedValue, YWBM, DateTime.Now);
            rc.PExcute(sb.ToString(),true);
        }
        else
        {
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        if (ra.EntStartProcessKCSJ(appList.FBaseinfoId, fAppId, appList.FYear.ToString(), DateTime.Now.Month.ToString(), "1122", ComFunction.GetDefaultDept(), t_FUpDeptName.SelectedValue, sl))
        {
            Session["FIsApprove"] = 1;
            tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
        }
    }
    private void EnabledControl()
    {
        if (Audit == "1" || FIsApprove == "1") //审核页面跳转
        {
            foreach (Control control in this.form1.Controls)
            {
                WebHelper.SetControlEnabled(control);
            }
        }
    }
    private string FIsApprove
    {
        get
        {
            string value = Session["FIsApprove"] == null ? "" : Session["FIsApprove"].ToString();
            if (string.IsNullOrEmpty(value))
                return "0";
            return value;
        }
    }
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
    private string YWBM {
        get {
            return Request.QueryString["YWBM"];
        }
    }
}