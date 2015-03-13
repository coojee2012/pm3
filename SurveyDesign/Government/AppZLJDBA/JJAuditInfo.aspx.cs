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

public partial class Government_AppZLJDBA_JJAuditInfo : System.Web.UI.Page
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
        
    }
   //初始化各种信息
   protected void initLayout()
   {
       if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
       {
           
       }
   }
    //绑定项目基本信息
    private void bindBaseInfo()
   {
       StringBuilder sb = new StringBuilder();
       DataTable dt = new DataTable();
       sb.Append(" select pr.FIdea,qa.FAppID, qa.ProjectName, qa.PrjItemName, qa.PrjItemType, qa.RecordNo, i.JSDW, i.JSDWDZ,i.Address  ");
       sb.Append(" from TC_QA_Record qa, CF_App_ProcessInstance pi, TC_Prj_Info i, CF_App_ProcessRecord pr ");
       sb.Append(" where pi.flinkId = qa.FAppId and i.FId = qa.FPrjId and pi.fId = pr.FProcessInstanceID and pi.FID = '" + t_fProcessInstanceID.Value + "'");
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
        //当前业务类型
        string FAppId = EConvert.ToString(Session["FAppId"]);

        var v = from t in dbContext.CF_Sys_PrjList
                orderby t.FId
                //where t.FManageType == 11221   
                where t.FManageType == 11222   
                select new
                {
                    t.FId,
                    t.FFileName,
                    FFileCount = t.FFileAmount,
                    AppFile = dbContext.TC_QA_File.Where(f => t.FId == f.FMaterialTypeId && f.FAppId == FAppId)
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

            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();

            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();

            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());

            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();

            t_fProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
            t_fProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
            if (dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dResult.SelectedValue = dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;

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
  
  
   protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
       if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
       {
           string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
           string FFileName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
           TextBox txtFileRemark = e.Item.FindControl("txtFileRemark") as TextBox;
           CheckBox chkIsReady = e.Item.FindControl("chkIsReady") as CheckBox;
           EgovaDB db = new EgovaDB();
           TC_Prj_FileReady data = db.TC_Prj_FileReady.Where(t => t.FAppId == t_fLinkId.Value
                                   && t.FMaterialTypeId == FId).FirstOrDefault();
           if (data != null)
           {
               txtFileRemark.Text = data.FRemarks;
               chkIsReady.Checked = EConvert.ToBool(data.FIsReady);

           }

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


   #region 按钮事件
    private void DisableButton()
   {
       btnAccept.Enabled = false;
       btnSave.Enabled = false;
       bthEndApp.Enabled = false;
       btnBackToEnt.Enabled = false;
   }
    private void initButton()
    {
        btnAccept.Enabled = true;
        btnSave.Enabled = true;
        bthEndApp.Enabled = true;
        btnBackToEnt.Enabled = true;
    }
   //保存意见
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try
       {
           WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
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
           if(WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = this.Session["DFUserId"].ToString();
              dResult.SelectedValue = "1";//接件操作强制选中同意项
                WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                    t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               DisableButton();
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件成功');", true);
           }
           else
           {
               initButton();
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行接件操作');", true);
           }
           
       }
       catch (Exception ee)
       {
           initButton();
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件失败');", true);
       }

   }
   //不予受理，直接结案
   protected void bthEndApp_Click(object sender, EventArgs e)
   {
       try
       {
           DisableButton();
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = SConvert.ToString(Session["DFUserId"]);
               string sIdea = t_FAppIdea.Text;
               dResult.SelectedValue = "3";//强制选中不同意项
               WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                      t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               WFApp.EndApp(t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId, sIdea);
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作成功');", true);
           }
           else
           {
               initButton();
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行该操作');", true);
           }
           
       }
       catch (Exception ee)
       {
           initButton();
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作失败');", true);
       }
   }
   //回退到企业
   protected void btnBackToEnt_Click(object sender, EventArgs e)
   {
       try
       {
           DisableButton();
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = SConvert.ToString(Session["DFUserId"]);
               string fLevel = SConvert.ToString(Session["FLevel"]);
               dResult.SelectedValue = "3";//强制选中不同意项
               WFApp.BackToEnt(t_fProcessInstanceID.Value, dfUserId, fLevel,
                   t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                       t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位成功');", true);
           }
           else
           {
               initButton();
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行该操作');", true);
           }
           
       }
       catch (Exception ee)
       {
           initButton();
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位失败');", true);
       }
   }
   #endregion
   protected void rep_List_ItemCommand(object source, RepeaterCommandEventArgs e)
   {
       switch(e.CommandName)
       {
           case "update":
               string fMaterialTypeId = e.CommandArgument.ToString();//取得参数
               TextBox txtFileRemark = e.Item.FindControl("txtFileRemark") as TextBox;
               CheckBox chkIsReady = e.Item.FindControl("chkIsReady") as CheckBox;
               EgovaDB db = new EgovaDB();
               TC_Prj_FileReady data = db.TC_Prj_FileReady.Where(t => t.FAppId == t_fLinkId.Value
                                       && t.FMaterialTypeId == fMaterialTypeId).FirstOrDefault();
               if (data == null)
               {
                   data = new TC_Prj_FileReady();
                   data.FId = Guid.NewGuid().ToString();
                   data.FMaterialTypeId = fMaterialTypeId;
                   data.FAppId = t_fLinkId.Value;
                   data.FIsReady = chkIsReady.Checked;
                   data.FRemarks = txtFileRemark.Text;
                   db.TC_Prj_FileReady.InsertOnSubmit(data);
               }
               else
               {
                   data.FIsReady = chkIsReady.Checked;
                   data.FRemarks = txtFileRemark.Text;
               }
               db.SubmitChanges();
               MyPageTool.showMessage("保存成功", this.Page);
               break;
       }
   }
}