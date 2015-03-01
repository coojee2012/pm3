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
using System.Linq;
using ProjectData;
public partial class Government_AppQualiInfo_BackEntBatchPrjBase : govBasePage
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            //显示打回信息选择项
            ShowBackIdea();
        }
    }
    /// <summary>
    /// 显示打回信息选择项
    /// </summary>
    void ShowBackIdea()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,fcontent from CF_App_BackIdea where ftype=0 order by forder ");
        DataTable dt = rc.GetTable(sb.ToString());
        ckListIdea.DataSource = dt;
        ckListIdea.DataTextField = "FContent";
        ckListIdea.DataValueField = "FId";
        ckListIdea.DataBind();
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

        sb.Append(" select tt.* ");
        sb.Append(" from (");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FSystemId,ep.FLinkId,ep.FReportDate,FEmpName FPrjName,");
        sb.Append(" (select top 1 FName from CF_Sys_ManageType where FNumber = ep.FManageTypeId) FManageType ");
        sb.Append(" from CF_App_ProcessInstance ep where ep.flinkid in (");
        sb.Append("select flinkid from CF_App_ProcessInstance where fid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i > 0)
                sb.Append(",");
            sb.Append("'" + strs[i].Trim() + "'");
        }
        sb.Append("))");
        sb.Append(" ) tt  order by tt.FReportDate desc");
        DataTable dt = rc.GetTable(sb.ToString());
        this.JustAppInfo_List.DataSource = dt;
        this.JustAppInfo_List.DataBind();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Write("<script>this.close()</script>");

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
            {
                return;
            }
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

        }
    }
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowEntInfo();
    }
    /// <summary>
    /// 打回企业
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBackEnt_Click(object sender, EventArgs e)
    {
        string backIdea = txtFBackIdea.Text;
        backIdea = backIdea.Length > 100 ? backIdea.Substring(0, 100) : backIdea;
        if (this.HFID1.Value.Trim() == "")
        {
            return;
        }
        string[] strs = this.HFID1.Value.Trim().Split(',');
        if (strs.Length <= 0)
        {
            return;
        }
        StringBuilder sbFLinkId = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        int iCount = strs.Length;
        sb.Append("select flinkid,fbaseInfoId from CF_App_ProcessInstance where fid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i == 0)
            {
                sb.Append("'" + strs[i].Trim() + "'");
            }
            else
            {
                sb.Append(",'" + strs[i].Trim() + "'");
            }
        }
        sb.Append(")");//查询要打回的企业的业务id
        DataTable dtFLinkIds = rc.GetTable(sb.ToString());
        if (dtFLinkIds != null && dtFLinkIds.Rows.Count > 0)
        {
            iCount = dtFLinkIds.Rows.Count;
            for (int ii = 0; ii < iCount; ii++)
            {
                string FLinkId = EConvert.ToString(dtFLinkIds.Rows[ii][0]);
                string fbaseInfoId = EConvert.ToString(dtFLinkIds.Rows[ii][1]);
                //查询是否有办结的，如果没有，可以打回到企业
                sb.Remove(0, sb.Length);
                sb.Append("select count(*) from (");
                sb.Append(" select fid from cf_App_ProcessInstanceBackup where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                sb.Append(" union ");
                sb.Append(" select fid from cf_App_ProcessInstance where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                sb.Append(")tt");
                iCount = rc.GetSQLCount(sb.ToString());
                if (iCount > 0)//如果有办结的流程，不可打回 |继续下一轮
                    continue;
                //否则
                sb.Remove(0, sb.Length);
                sb.Append("select ep.fId,er.FId erFId,'" + Session["DFUserId"] + "' FUserId ");
                sb.Append(" from cf_App_ProcessInstance ep ");
                sb.Append(" inner join cf_App_ProcessRecord er on ep.FId=er.FProcessInstanceId ");
                sb.Append(" where ep.FSubFlowId=er.FSubFlowId ");
                sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
                sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
                sb.Append(" and isnull(er.FMeasure,0) in (0,1,4) "); //存子流");
                sb.Append(" and ep.FState<>6 and ep.FLinkId='" + FLinkId + "'");
                DataTable dt = rc.GetTable(sb.ToString());
                CF_App_ProcessInstance ProcessInstance = db.CF_App_ProcessInstance.Where(t => t.FLinkId == FLinkId).FirstOrDefault();

                if (ra.BatchBack(dt, FLinkId, backIdea))
                {
                    if (ProcessInstance != null)
                    {
                        //保存到CF_App_Idea
                        CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
                        if (idea == null)
                        {
                            idea = new CF_App_Idea()
                            {
                                FId = Guid.NewGuid().ToString(),
                                FCreateTime = DateTime.Now,
                                FLinkId = FLinkId,
                                FIsdeleted = 0,

                            };
                            db.CF_App_Idea.InsertOnSubmit(idea);
                        }
                        idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FReportCount).FirstOrDefault();
                        idea.FResult = "打回";
                        idea.FResultInt = 2;
                        idea.FContent = txtFBackIdea.Text;
                        idea.FAppTime = DateTime.Now;
                        idea.FSystemId = ProcessInstance.FSystemId;
                        idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                        idea.FType = ProcessInstance.FManageTypeId;
                        idea.FTime = DateTime.Now;

                    }
                }
                this.RegisterStartupScript("jj", "<script>window.returnValue=1;window.close();</script>");
                sbFLinkId.AppendFormat(" {0} , ", FLinkId);
            }

            //操作日志 记录批量打回
            string title = " 批量打回企业 操作人" + Session["DFUserId"];
            string description = "打回意见" + txtFBackIdea.Text + "打回的FLinkId:" + sbFLinkId.ToString();
            //DataLog.Write(rc, LogType.Info, LogSort.Operation, title, description); 
            this.RegisterStartupScript("js", "<script>window.returnValue=1;window.close();</script>");
        }
        else
        {
            this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('没有需要处理的数据！');window.returnValue=1;window.close();</script>");
        }
        db.SubmitChanges();
    }

}
