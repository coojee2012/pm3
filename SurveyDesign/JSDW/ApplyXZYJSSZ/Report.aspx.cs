using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyXZYJS_Report : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private ProjectDB db = new ProjectDB();
    private RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(FIsApprove))
            {
                if (FIsApprove == "1")
                {
                    btnSave.Enabled = false;
                }
            }
            showInfo();
           
        }
    }
    public void showInfo()
    {
        //t_DW.Text = CurrentEntUser.EntName;
        string guid = fAppId;
        if (!string.IsNullOrEmpty(guid))
        {
            var appList = db.CF_App_List.FirstOrDefault(x => x.FId == guid);
            if (appList != null)
            {
                t_FYear.Text = appList.FYear.ToString();
                t_FName.Text = appList.FName;
            }
            if (!string.IsNullOrEmpty(YJS_ID))
            {
                string sql = "select top 1 XZMC,XMSD,XMSDMC,SBBM,IsAudit from YW_XZYJS where ID='" + YJS_ID + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    hfIsAudit.Value = row["IsAudit"].ToString();
                    t_XZMC.Text = row["XZMC"].ToString();
                    //bindDep(row["XMSD"].ToString(), row["XMSDMC"].ToString());
                    DataTable dt = ra.GetCanReportDeptXM(row["XMSD"].ToString(), "1122", appList.FManageTypeId.Value.ToString(), ComFunction.GetDefaultDept());
                    ddlLevel.DataSource = dt;
                    ddlLevel.DataTextField = "fname";
                    ddlLevel.DataValueField = "fnumber";
                    ddlLevel.DataBind();
                    if (!string.IsNullOrEmpty(row["SBBM"].ToString()))
                        ddlLevel.SelectedValue = row["SBBM"].ToString();
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (hfIsAudit.Value != "1")//验证通过
        {
            tool.showMessage("上报信息未完善，请完善后提交");
            return;
        }
        string fDeptNumber = ComFunction.GetDefaultDept();
        if (fDeptNumber == null || fDeptNumber == "")
        {
            tool.showMessage("系统出错,请配置默认管理部门");
            return;
        }
        btnSave.Enabled = false;
        DateTime dTime = DateTime.Now;
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
            sl[0].Add("FUpDept", ddlLevel.SelectedValue);
            sl[0].Add("FEmpId", appList.FLinkId);//CF_Prj_Data.FID
            sl[0].Add("FEmpName", t_XZMC.Text.Trim());
            sl[0].Add("FLeadId", "");//建设单位ID（这里没有）
            sl[0].Add("FLeadName", "");
            StringBuilder sb = new StringBuilder();

            sb.Append("update CF_App_List set FUpDeptId=" + ddlLevel.SelectedValue + ",");
            sb.Append("ftime=getdate(),FState=1,FReportDate=getdate(),FIsDeleted=0 where fid = '" + fAppId + "'");
            sb.AppendFormat("update YW_XZYJS set SBBM='{0}',SBRQ='{2}' where ID='{1}';", ddlLevel.SelectedValue, YJS_ID,DateTime.Now);
            rc.PExcute(sb.ToString());
        }
        else
        {
            btnSave.Enabled = true;
            tool.showMessage("该业务数据有误，请重新申请！");
            return;
        }
        if (ra.EntStartProcessKCSJ(appList.FBaseinfoId, fAppId, appList.FYear.ToString(), DateTime.Now.Month.ToString(), "1122", fDeptNumber, ddlLevel.SelectedValue, sl))
        {
            Session["FIsApprove"] = 1;
            btnSave.Enabled = true;
            tool.showMessageAndRunFunction("提交成功！", "location.href=location.href");
        }
    }
    public void readOnly()
    {
        btnSave.Enabled = false;
    }
    public string YJS_ID
    {
        get
        {
            return Request.QueryString["YJS_GUID"];
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
    private string fAppId {
        get {
            return Request.QueryString["fAppId"];
        }
    }
}