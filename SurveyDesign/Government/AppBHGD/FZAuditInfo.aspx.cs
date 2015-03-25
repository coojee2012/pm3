using Approve.RuleApp;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Government_AppBHGD_FZAuditInfo : System.Web.UI.Page
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
        sb.Append(" select pr.FIdea,qa.FAppID, qa.ProjectName,  i.JSDW, i.JSDWDZ,i.Address  ");
        sb.Append(" from TC_BZGD_Record qa, CF_App_ProcessInstance pi, TC_Prj_Info i, CF_App_ProcessRecord pr ");
        sb.Append(" where pi.flinkId = qa.FAppId and i.FId = qa.FPrjId and pi.fId = pr.FProcessInstanceID and pi.FID = '" + t_fProcessInstanceID.Value + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FAppId"] = dt.Rows[0]["FAppID"].ToString();
            Session["FAppId"] = dt.Rows[0]["FAppID"].ToString();
            t_ProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
            //t_RecordNo.Text = dt.Rows[0]["RecordNo"].ToString();
            //t_PrjItemName.Text = dt.Rows[0]["PrjItemName"].ToString();
            //t_PrjItemType.SelectedValue = dt.Rows[0]["PrjItemType"].ToString();
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
        //   HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");
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

}