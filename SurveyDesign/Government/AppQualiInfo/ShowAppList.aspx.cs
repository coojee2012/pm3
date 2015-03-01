using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.EntitySys;
using Approve.EntityCenter;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntityQuali;
using Approve.RuleApp;
public partial class Government_AppQualiInfo_ShowAppList : govBasePage
{
    RQuali rq = new RQuali();
    public RCenter rc = new RCenter();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            ShowReportInfo();
            ShowAppInfo();
        }
    }

    private void ShowReportInfo()
    {

        string sEntName = "";
        string sManageTypeId = "";
        string sReportDate = "";
        string sYear = "";
        string sId = "";

        string sLevelid = "";
        string sTypeid = "";
        string sListid = "";




        if (Request["pid"] != null)
        {
            EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FEntName,FManageTypeId,FReportDate,FYear,FListId,FTypeId,FLevelId", "fid='" + Request["pid"] + "'");
            if (ep != null)
            {
                sEntName = ep.FEntName;
                sManageTypeId = ep.FManageTypeId.ToString();
                sReportDate = ep.FReportDate.ToShortDateString();
                sYear = ep.FYear.ToString();
                sId = ep.FId;
                sLevelid = ep.FLevelId;
                sTypeid = ep.FTypeId;
                sListid = ep.FListId;
            }
            else
            {
                EaProcessInstanceBackup epb = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "FId,FEntName,FManageTypeId,FReportDate,FYear,FListId,FTypeId,FLevelId", "fid='" + Request["pid"] + "'");
                if (epb != null)
                {
                    sEntName = epb.FEntName;
                    sManageTypeId = epb.FManageTypeId.ToString();
                    sReportDate = epb.FReportDate.ToShortDateString();
                    sYear = epb.FYear.ToString();
                    sId = epb.FId;
                    sLevelid = epb.FLevelId;
                    sTypeid = epb.FTypeId;
                    sListid = epb.FListId;
                }
            }
        }
        else
        {

            EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FEntName,FManageTypeId,FReportDate,FYear,FListId,FTypeId,FLevelId", "FlinkId='" + Request["lid"] + "'");
            if (ep != null)
            {
                sEntName = ep.FEntName;
                sManageTypeId = ep.FManageTypeId.ToString();
                sReportDate = ep.FReportDate.ToShortDateString();
                sYear = ep.FYear.ToString();
                sId = ep.FId;
                sLevelid = ep.FLevelId;
                sTypeid = ep.FTypeId;
                sListid = ep.FListId;
            }
            else
            {
                EaProcessInstanceBackup epb = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "FId,FEntName,FManageTypeId,FReportDate,FYea,FListId,FTypeId,FLevelIdr", "FlinkId='" + Request["lid"] + "'");
                if (epb != null)
                {
                    sEntName = epb.FEntName;
                    sManageTypeId = epb.FManageTypeId.ToString();
                    sReportDate = epb.FReportDate.ToShortDateString();
                    sYear = epb.FYear.ToString();
                    sId = epb.FId;
                    sLevelid = epb.FLevelId;
                    sTypeid = epb.FTypeId;
                    sListid = epb.FListId;
                }
            }

        }

        liter_FBaseName.Text = sEntName;
        liter_FManageType.Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + sManageTypeId + "'");
        liter_FReportDate.Text = sReportDate;
        liter_FYear.Text = sYear;
        ViewState["PID"] = sId;

        string fReportInfo = "";
        fReportInfo += rc.GetDicRemark(sListid);
        if (fReportInfo != "")
        {
            fReportInfo += "";
        }
        fReportInfo += rc.GetDicRemark(sTypeid);
        if (fReportInfo != "")
        {
            fReportInfo += "";
        }
        fReportInfo += rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber='" + sLevelid + "'");
        liter_FReportInfo.Text = fReportInfo;
    }

    private void ShowAppInfo()
    {
        StringBuilder sb = new StringBuilder();
        //查询企业类型、审批状态
        sb.Append("select fSystemId,FState,FLinkId from cf_App_ProcessInstance ");
        sb.Append("where fid='" + ViewState["PID"].ToString() + "' ");
        sb.Append(" union ");
        sb.Append("select fSystemId,FState,FLinkId from cf_App_ProcessInstanceBackup ");
        sb.Append("where fid='" + ViewState["PID"].ToString() + "' ");
        DataTable dtTemp = rc.GetTable(sb.ToString());
        string fsystemId = string.Empty;
        string fLinkId = string.Empty;
        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            fsystemId = dtTemp.Rows[0]["FSystemId"].ToString();
            fLinkId = dtTemp.Rows[0]["FLinkId"].ToString();
        }

        DataTable dt = dtGetDT(fsystemId, fLinkId);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string fstate = dt.Rows[i]["fstate"].ToString();
                if (fstate == "2")
                {
                    string s1 = dt.Rows[i]["s1"].ToString();
                    string s2 = dt.Rows[i]["s2"].ToString();
                    if (s1 == s2)
                    {
                        string pid = dt.Rows[i]["fid"].ToString();
                        string fBackIdea = rc.GetSignValue("select FBackIdea from CF_App_ProcessInstance where fid='" + pid + "'");
                        dt.Rows[i]["FResult"] = "3";
                        dt.Rows[i]["FIdea"] = fBackIdea;
                        dt.Rows[i]["FState"] = "-1";
                    }
                }
            }
        }
        this.AppInfo_List.DataSource = dt;
        this.AppInfo_List.DataBind();
    }

    DataTable dtGetDT(string fSystemId, string fLinkId)
    {
        string sCon = " and p.fid='" + ViewState["PID"] + "' ";
        if (fSystemId == "130" || fSystemId == "186" || fSystemId == "187")
            sCon = " and p.FlinkId='" + fLinkId + "' ";
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);
        sb.Append("select * from (");
        sb.Append(" select p.fid,fentname,r.FUserId,");
        sb.Append(" p.FSubFLowId s1,r.FSubFlowId s2,");
        sb.Append(" p.FReportCount,r.FOrder,");
        sb.Append(" p.FState,");
        sb.Append(" p.flistid,");
        sb.Append(" p.ftypeid,");
        sb.Append(" p.flevelid,");
        sb.Append(" p.FManageTypeId,");
        sb.Append(" (select top 1 replace(FName,'人','')FName from cf_App_SubFlow where fid=r.FSubFlowId)FStepName,");
        sb.Append(" r.fresult,R.FAppTime ,r.FReportTime,r.FIdea,r.FRoleDesc,");
        sb.Append(" r.FCompany,r.FFunction,r.FAppPerson ");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(sCon);
        sb.Append(" and (case p.FState when '1' then r.fMeasure else '5' end)='5' ");
        sb.Append(" union ");
        sb.Append(" select p.fid,fentname,r.FUserId,");
        sb.Append(" p.FSubFLowId s1,r.FSubFlowId s2,");
        sb.Append(" p.FReportCount,r.FOrder,");
        sb.Append(" p.FState,");
        sb.Append(" p.flistid,");
        sb.Append(" p.ftypeid,");
        sb.Append(" p.flevelid,");
        sb.Append(" p.FManageTypeId,");
        sb.Append(" (select top 1 replace(FName,'人','')FName from cf_App_SubFlow where fid=r.FSubFlowId)FStepName,");
        sb.Append(" r.fresult,R.FAppTime ,r.FReportTime,r.FIdea,r.FRoleDesc,");
        sb.Append(" r.FCompany,r.FFunction,r.FAppPerson ");
        sb.Append(" from CF_App_ProcessInstanceBackup p,CF_App_ProcessRecordBackup r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(sCon);
        sb.Append(" and (case p.FState when '1' then r.fMeasure else '5' end)='5' ");
        sb.Append(")t");
        sb.Append(" order by t.FReportCount,t.FOrder ");
        DataTable dt = rc.GetTable(sb.ToString());
        return dt;
    }

    protected void AppInfo_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string stepName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FStepName"));
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string sDesc = stepName.PadLeft(2, ' ').Replace("厅", "").Replace("市", "").Replace("县", "").Replace("省", "").Replace("负责", "审批");

            sDesc = sDesc.Substring(sDesc.Length - 2);
            Literal lAppResult = e.Item.FindControl("lAppResult") as Literal;
            lAppResult.Text = sDesc;
            if (fState == "-1")
                sDesc = "打回";
            Literal lAppTime = e.Item.FindControl("lAppTime") as Literal;
            lAppTime.Text = sDesc;
            Literal lAppIdea = e.Item.FindControl("lAppIdea") as Literal;
            if (fState == "-1")
                sDesc = "<font color='red'><b>打回</b></font>";
            lAppIdea.Text = sDesc;

            Label lFAppPerson = e.Item.FindControl("t_FAppPerson") as Label;
            if (string.IsNullOrEmpty(lFAppPerson.Text.Trim().Replace("&nbsp;", "")))
            {
                string fUserId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUserId"));
                lFAppPerson.Text = rc.GetSignValue("select fLinkMan from cf_Sys_User where fid='" + fUserId + "'");
            }
        }
    }

    public string GetResult(string fappResult)
    {
        switch (fappResult)
        {
            case "1":
                return "通过";
            case "3":
                return "不通过";
            case "7":
                return "待定";
            default:
                return "";
        }
    }
}
