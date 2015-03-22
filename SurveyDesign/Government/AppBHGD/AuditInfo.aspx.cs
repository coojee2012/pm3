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
using System.Drawing;
using System.Linq;

using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;

using EgovaDAO;
using EgovaBLL;
using ProjectBLL;

public partial class Government_AppZLJDBA_AuditInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            {
               t_fLinkId.Value = Request["FLinkId"].ToString();
            }
            if (Request["fpid"] != null && !string.IsNullOrEmpty(Request["fpid"]))
            {
                t_fProcessInstanceID.Value = Request["fpid"].ToString();
            }
            if (Request["ferid"] != null && !string.IsNullOrEmpty(Request["ferid"]))
            {
                t_fProcessRecordID.Value = Request["ferid"].ToString();
            }
            if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
            {
                t_fSubFlowId.Value = Request["fSubFlowId"].ToString();
            }
            if (Request["fBaseInfoId"] != null && !string.IsNullOrEmpty(Request["fBaseInfoId"]))
            {
                t_fBaseInfoId.Value = Request["fBaseInfoId"].ToString();
            }
            if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
            {
                t_fTypeId.Value = Request["ftype"].ToString();
            }
            
            init();
        }
    }
    //初始化各种信息
   protected void init()
    {
        initLayout();
        BindControl();
        bindBaseInfo();
        bindFileInfo();
        bindAuditInfo();
        bindAuditList();
        
    }
   //初始化各种信息
   protected void initLayout()
   {
       if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
       {
           if (Request["ftype"].ToString() == "1")
           {

               this.lbTitle.Text = "接件";
               accept.Visible = true;
               oneAudit.Visible = false;
               table_audit_list.Visible = false;
               btnUPCS.Visible = false;
               btnUPFS.Visible = false;
               btnUPEND.Visible = false;
               btnBackToPre.Visible = false;


           }
           else if (Request["ftype"].ToString() == "10")
           {
               lbTitle.Text = "初审";
               oneAudit.Visible = true;
               accept.Visible = false;
               btnAccept.Visible = false;
               btnUPFS.Visible = false;
               btnUPEND.Visible = false;
           }
           else if (Request["ftype"].ToString() == "5")
           {
               lbTitle.Text = "复审";
               oneAudit.Visible = true;
               accept.Visible = false;
               btnAccept.Visible = false;
               btnUPCS.Visible = false;
               btnUPFS.Visible = false;
           }
       }
   }
    //绑定项目基本信息
    private void bindBaseInfo()
   {
       StringBuilder sb = new StringBuilder();
       DataTable dt = new DataTable();
       sb.Append(" select pr.FIdea,qa.FAppID, qa.ProjectName, qa.PrjItemName, qa.PrjItemType, qa.RecordNo, i.JSDW, i.JSDWDZ,i.Address  ");
       sb.Append(" from TC_QA_Record qa, CF_App_ProcessInstance pi, TC_Prj_Info i, CF_App_ProcessRecord pr ");
       sb.Append(" where pi.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
       sb.Append(" and pi.flinkId = qa.FAppId and i.FId = qa.FPrjId and pi.fId = pr.FProcessInstanceID and pi.FID = '" + t_fProcessInstanceID.Value + "'");
       dt = rc.GetTable(sb.ToString());
       if (dt != null && dt.Rows.Count > 0)
       {
           ViewState["FAppId"] = dt.Rows[0]["FAppID"].ToString();
           t_ProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
           t_RecordNo.Text = dt.Rows[0]["RecordNo"].ToString();
           t_PrjItemName.Text = dt.Rows[0]["PrjItemName"].ToString();
           t_PrjItemType.SelectedValue = dt.Rows[0]["PrjItemType"].ToString();
           t_JSDW.Text = dt.Rows[0]["JSDW"].ToString();
           t_Address.Text = dt.Rows[0]["Address"].ToString();
           t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
       }
   }
    //绑定项目附件信息
    private void bindFileInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = from t in dbContext.TC_QA_NeedFile
                orderby t.FId
                select new
                {
                    t.FId,
                    t.FFileName,
                    FFileCount = t.FFileCount,
                    AppFile = dbContext.TC_QA_File.Where(f => t.FId == f.FMaterialTypeId && f.FAppId == t_fLinkId.Value)
                };
        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
    }
    //绑定当前审批意见（或接件意见）
    private void bindAuditInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + t_fLinkId.Value + "' and FMeasure=0 and FSubFlowId='" + t_fSubFlowId.Value + "'");
        sb.Append(" and FRoleId in(" + this.Session["DFRoleId"].ToString() + ") and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_Auditer.Text = t_FAppPerson.Text;
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_AuditUnit.Text = t_FAppPersonUnit.Text;
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_AuditFunction.Text = t_FAppPersonJob.Text;
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_AuditTime.Text = t_FAppDate.Text;
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
            t_AuditIdear.Text = t_FAppIdea.Text;
            t_fProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
            t_fProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
            if (dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dResult.SelectedValue = dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;
            if (dAudit.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dAudit.SelectedValue = dAudit.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;
        }
        else
        {
            string sql = (@" select FLinkMan,fcompany,FFunction,FTel,FDepartmentID 
                    from cf_sys_user where fid='" + Session["DFUserId"] + "'");
            dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_FAppPerson.Text = dt.Rows[0]["FLinkMan"].ToString();
                t_FAppPersonUnit.Text = RBase.GetDepartmentName(dt.Rows[0]["FDepartmentID"].ToString()) + RBase.GetDepartmentName(dt.Rows[0]["FCompany"].ToString());
                t_FAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
                t_Auditer.Text = t_FAppPerson.Text; t_AuditFunction.Text = t_FAppPersonJob.Text;
                t_AuditUnit.Text = t_FAppPersonUnit.Text; t_AuditTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

//            string sSql = string.Format(@"select er.FID,a.FID as PFID from CF_App_ProcessInstance a,CF_App_ProcessRecord er
//               where er.FMeasure='0' and a.fid=er.FProcessInstanceID and a.fsubflowid=er.fsubflowid and a.fstate<>6 
//               and a.FLinkId='" + t_fLinkId.Value + "' and a.FRoleId='" + this.Session["DFRoleId"].ToString()
//                                + "' and er.FSubFlowId='" + t_fSubFlowId.Value + "'");
//            dt = rc.GetTable(sSql);
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                t_fProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
//                t_fProcessInstanceID.Value = dt.Rows[0]["PFID"].ToString();
//            }

        }
    }
    private void BindControl()
    {

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
        //查看上报资料按钮
        StringBuilder sb = new StringBuilder();
        sb.Append("FLinkId='" + t_fLinkId.Value + "'");
        EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FManageTypeId,Fsystemid", sb.ToString());
        if (ea == null)
        {
            return;
        }
        string fbid = t_fBaseInfoId.Value;
        string faid = t_fLinkId.Value;
        string fmid = ea.FManageTypeId;
        string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        string fsid = ea.FSystemId;
        string fQurl = rc.getMTypeQurl(ea.FManageTypeId); ;

        string fUrl = fQurl;
        HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");
    }
    //绑定审批意见列表
    private void bindAuditList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);
        sb.Append(" select r.fid,fentname,");
        sb.Append(" p.flistid,R.FAppTime ,r.FReportTime,");
        sb.Append(" p.ftypeid,r.FIdea,r.FRoleDesc,p.flevelid,");
        sb.Append(" p.FManageTypeId,");
        sb.Append(" case r.fresult when 1 then '通过' when 3 then '退回' end fresult,");
        sb.Append(" r.FCompany,r.FFunction,r.FAppPerson ");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(" and p.fid='" + t_fProcessInstanceID.Value + "' ");
        sb.Append(" and r.fMeasure<>0  ");
        //sb.Append(" and ");
        //sb.Append(" (");
        //sb.Append(" (r.ftypeid<>1 and  (select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) >1)");
        //sb.Append(" or");
        //sb.Append(" ((select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) <=1)");
        //sb.Append(" )");
        sb.Append(" order by r.FOrder ");

        this.DG_List.DataSource = rc.GetTable(sb.ToString());
        this.DG_List.DataBind();
    }
  
   protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
       if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
       {
           string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
           string FFileName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
           IQueryable<TC_QA_File> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<TC_QA_File>;
           if (AppFile != null && AppFile.Count() > 0)
           {
               ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
               Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
               rep_File.DataSource = AppFile;
               rep_File.DataBind();
           }
       }

   }
   protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
   {
       Pager1.CurrentPageIndex = e.NewPageIndex;
       bindFileInfo();
   }
   protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
   {
       if (e.Item.ItemIndex > -1)
       {
           e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

       }
   }

   #region 按钮事件
   //保存意见
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try
       {
           if (t_fTypeId.Value == "1")
           {
               WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
           }
           else
           {
               WFApp.Assign(t_fProcessRecordID.Value, t_AuditIdear.Text, dAudit.SelectedValue.Trim(), t_Auditer.Text,
                   t_AuditUnit.Text, t_AuditFunction.Text, t_AuditTime.Text);
           }
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存成功');", true);  
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存失败');", true);  
       }
       
   }
   //接件
   protected void btnAccept_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = this.Session["DFUserId"].ToString();
            WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                    t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件失败');", true);
       }

   }
   //初审
   protected void btnUPCS_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = this.Session["DFUserId"].ToString();
           WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                    t_AuditIdear.Text, dAudit.SelectedValue.Trim(), t_Auditer.Text,
                   t_AuditUnit.Text, t_AuditFunction.Text, t_AuditTime.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('初审提交成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('初审提交失败');", true);
       }
   }
   //复审
   protected void btnUPFS_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = this.Session["DFUserId"].ToString();
           WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                    t_AuditIdear.Text, dAudit.SelectedValue.Trim(), t_Auditer.Text,
                   t_AuditUnit.Text, t_AuditFunction.Text, t_AuditTime.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('复审提交成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('复审提交失败');", true);
       }
   }
    //办结
   protected void btnUPEND_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = this.Session["DFUserId"].ToString();
           WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                    t_AuditIdear.Text, dAudit.SelectedValue.Trim(), t_Auditer.Text,
                   t_AuditUnit.Text, t_AuditFunction.Text, t_AuditTime.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('复审提交成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('复审提交失败');", true);
       }
   }
    //审核不通过，直接结案
   protected void bthEndApp_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = SConvert.ToString(Session["DFUserId"]);
           string sIdea = "";
           if(t_fTypeId.Value == "1")
           {
               sIdea = t_FAppIdea.Text;
           }
           else
           {
               sIdea = t_AuditIdear.Text;
           }
           WFApp.EndApp(t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId, sIdea);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作失败');", true);
       }
   }
   //回退到企业
   protected void btnBackToEnt_Click(object sender, EventArgs e)
   {
       try
       {
           string dfUserId = SConvert.ToString(Session["DFUserId"]);
           string fLevel = SConvert.ToString(Session["FLevel"]);
           WFApp.BackToEnt(t_fProcessInstanceID.Value,dfUserId,fLevel,
               t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);

           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到企业成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到企业失败');", true);
       }
   }
   //回退到上一级
   protected void btnBackToPre_Click(object sender, EventArgs e)
   {
       try
       {
           string dfRoleId = SConvert.ToString(Session["DFRoleId"]);
           WFApp.BackToPre(t_fLinkId.Value,t_fTypeId.Value, dfRoleId, t_Auditer.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到上一级成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到上一级失败');", true);
       }
   }
   #endregion
}