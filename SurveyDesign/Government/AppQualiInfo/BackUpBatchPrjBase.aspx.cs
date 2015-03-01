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
using Approve.RuleApp;
using Approve.Common;
using System.Linq;
using ProjectData;
using ProjectBLL;
public partial class Government_AppQualiInfo_BackUpBatchPrjBase : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RApp ra = new RApp();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowAppEmpInfo();
        }
    }

    //显示审核人员信息
    private void ShowAppEmpInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("FId='" + Session["DFUserId"].ToString() + "'");
        EsUser eu = (EsUser)rc.GetEBase(EntityTypeEnum.EsUser, "FLinkMan,FFunction,FDepartmentID,FCompany", sb.ToString());
        if (eu != null)
        {
            this.t_FAppPerson.Text = eu.FLinkMan;
            this.t_FAppPersonJob.Text = eu.FFunction;
            this.t_FAppPersonUnit.Text = RBase.GetDepartmentName(eu.FDepartmentID) + RBase.GetDepartmentName(eu.FCompany);
            this.t_FAppDate.Text = DateTime.Now.ToShortDateString();
        }
    }

    private void ShowEntInfo()
    {
        string fpids = this.HFID1.Value;
        if (fpids == "")
        {
            return;
        }
        string[] strs = fpids.Trim().Split(',');

        if (strs.Length <= 0)
        {
            return;
        }
        int iCount = strs.Length;
        StringBuilder sb = new StringBuilder();

        sb.Append(" select tt.*");
        sb.Append(" from (");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FSystemId,ep.FLinkId,ep.FReportDate,FEmpName FPrjName, ");
        sb.Append(" (select top 1 FName from CF_Sys_ManageType where FNumber = ep.FManageTypeId) FManageType,er.fid frid ");
        sb.Append(" from CF_App_ProcessInstance ep,CF_App_ProcessRecord er where ep.flinkid in (");
        sb.Append("select flinkid from CF_App_ProcessInstance where fid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i > 0)
                sb.Append(",");
            sb.Append("'" + strs[i].Trim() + "'");
        }
        sb.Append("))");
        sb.Append(" and ep.fid = er.FProcessInstanceID ");
        sb.Append(" and ep.FSubFlowId = er.FSubFlowId ");
        sb.Append(" and er.froleid in(" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and isnull(er.FReportCount,0) in (select isnull(max(FReportCount),0) from CF_App_ProcessRecord where fMeasure<>3 and fprocessInstanceId=ep.fid group by fprocessInstanceId)");
        sb.Append(" ) tt   order by tt.FReportDate desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.JustAppInfo_List.DataSource = dt;
        this.JustAppInfo_List.DataBind();
    }
    private void BatchApp()
    {
        StringBuilder sbDescription = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        int iCount = JustAppInfo_List.Items.Count;
        if (iCount == 0)
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("没有数据，无法审核！");
            return;
        }

        SortedList[] sl = new SortedList[iCount];
        for (int i = 0; i < iCount; i++)
        {
            sb.Remove(0, sb.Length);

            string fId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 1].Text;

            EaProcessInstance pi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FLinkId,FId,FRoleId,FSubFlowId,FSystemId,FManageTypeId", "fid='" + fId + "'");
            if (pi == null)
            {
                return;
            }

            sl[i] = new SortedList();
            sb.Append(" FProcessInstanceID='" + pi.FId + "' and FRoleId='" + pi.FRoleId + "' ");
            sb.Append(" and FSubFlowId='" + pi.FSubFlowId + "' order by fReportCount desc");
            sl[i].Add("FID", rc.GetSignValue(EntityTypeEnum.EaProcessRecord, "FId", sb.ToString()));
            sl[i].Add("FProcessInstanceId", pi.FId);
            sl[i].Add("FAppPerson", this.t_FAppPerson.Text);
            sl[i].Add("FAppTime", EConvert.ToDateTime(this.t_FAppDate.Text));
            sl[i].Add("FCompany", this.t_FAppPersonUnit.Text);
            sl[i].Add("FFunction", this.t_FAppPersonJob.Text);
            sl[i].Add("FIdea", this.t_FAppIdea.Text);
            sl[i].Add("FLevel", EConvert.ToInt(Session["DFLevel"].ToString()));
            sl[i].Add("FLinkId", pi.FLinkId);
            sl[i].Add("FMeasure", 5);//dResult.SelectedValue);
            sl[i].Add("FResult", dResult.SelectedItem.Value);
            sl[i].Add("FUserId", Session["DFUserId"].ToString());
            sl[i].Add("FReporttime", DateTime.Now);
            DateTime fReportTime = pi.FReportDate;
            DateTime nowTime = DateTime.Now;
            TimeSpan spanTime = nowTime - fReportTime;
            sl[i].Add("FWaiteTime", spanTime.Days);

            //保存到CF_App_Idea
            CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == pi.FLinkId).FirstOrDefault();
            if (idea == null)
            {
                idea = new CF_App_Idea()
                {
                    FId = Guid.NewGuid().ToString(),
                    FCreateTime = DateTime.Now,
                    FLinkId = pi.FLinkId,
                    FIsdeleted = 0,
                    FReportCount = db.CF_App_List.Where(t => t.FId == pi.FLinkId).Select(t => t.FReportCount).FirstOrDefault(),

                };
                db.CF_App_Idea.InsertOnSubmit(idea);
            }
            idea.FResult = dResult.SelectedItem.Text;
            idea.FResultInt = EConvert.ToInt(dResult.SelectedValue);
            idea.FContent = t_FAppIdea.Text;
            idea.FAppTime = DateTime.Now;
            idea.FSystemId = EConvert.ToInt(pi.FSystemId);
            idea.FUserId = EConvert.ToString(Session["DFUserId"]);
            idea.FType = EConvert.ToInt(pi.FManageTypeId);
            idea.FTime = DateTime.Now;


            CF_Prj_Certi certi = db.CF_Prj_Certi.Where(t => t.FAppId == pi.FLinkId).FirstOrDefault();
  
            if (certi != null)
            {
                if (dResult.SelectedValue == "3")
                {
                    certi.FIsValid = 0;
                }
                else
                {
                    certi.FIsValid = 1;
                }
            }
        }
        ra.BatchAppKCSJ(sl);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        BatchApp();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Write("<script>this.close()</script>");
    }
    protected void btnBackSubDept_Click(object sender, EventArgs e)
    {

    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;

            StringBuilder sb = new StringBuilder();
            sb.Append("FBaseInfoId,FManageTypeId,FSystemId,FLinkId");
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, sb.ToString(), "fid='" + fid + "'");
            if (ea == null)
                return;
            //工程业主
            string fbid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoid"));
            if (!string.IsNullOrEmpty(fbid))
            {
                ProjectDB db = new ProjectDB();
                e.Item.Cells[2].Text = db.CF_Ent_BaseInfo.Where(t => t.FId == fbid).Select(t => t.FName).FirstOrDefault();
            }
            string faid = ea.FLinkId;
            string fmid = ea.FManageTypeId;
            string frid = rc.GetSignValue(EntityTypeEnum.EsUserRight, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
            //string sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            //sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString();
            //e.Item.Cells[2].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[2].Text + "</a>";

        }
    }
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowEntInfo();
    }
}
