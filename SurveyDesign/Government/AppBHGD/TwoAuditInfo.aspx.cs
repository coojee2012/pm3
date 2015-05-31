using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using System.Linq;
using EgovaDAO;
using EgovaBLL;
using Approve.RuleApp;
using System.Collections;
using System.Text;
using Approve.EntityCenter;
using Approve.EntityBase;
using ProjectBLL;

public partial class Government_AppBZGD_TwoAuditInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    WorkFlowApp wf = new WorkFlowApp();
    ArrayList arrCon = new ArrayList();
    private string sTemp = "";
    private StringBuilder sScript = new StringBuilder();
    private string fLinkId = null;
    private string fSubFlowId = null;
    private string fBaseInfoId = null;
    private string fpid = null;
    private string ferid = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        BindControl();
        if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
        {
            t_YWBM.Value = Request["FLinkId"].ToString();
            fLinkId = Request["FLinkId"].ToString();
        }
        if (Request["fpid"] != null && !string.IsNullOrEmpty(Request["fpid"]))
        {
            fpid = Request["fpid"].ToString();     //流程fpid
        }
        if (Request["ferid"] != null && !string.IsNullOrEmpty(Request["ferid"]))
        {
            ferid = Request["ferid"].ToString();
        }
        if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
        {
            t_fSubFlowId.Value = Request["fSubFlowId"].ToString();
            fSubFlowId = Request["fSubFlowId"].ToString();
        }
        if (Request["fBaseInfoId"] != null && !string.IsNullOrEmpty(Request["fBaseInfoId"]))
        {
            t_fBaseInfoId.Value = Request["fBaseInfoId"].ToString();
            fBaseInfoId = Request["fBaseInfoId"].ToString();
        }
        if (!IsPostBack)
        {
            this.ShowLinkInfo();
            this.ShowInfo();
        }
    }

    //显示连接信息
    public void ShowLinkInfo()
    {
        if (fBaseInfoId == null)
        {
            return;
        }
        if (fLinkId == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("FLinkId='" + fLinkId + "'");
        EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FManageTypeId,Fsystemid", sb.ToString());
        if (ea == null)
        {
            return;
        }
        string fbid = this.fBaseInfoId;
        string faid = this.fLinkId;
        string fmid = ea.FManageTypeId;
        string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        string fsid = ea.FSystemId;
        string fQurl = rc.getMTypeQurl(ea.FManageTypeId); ;

        string fUrl = fQurl;
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        object fuserId = Session["DFUserId"];
        sb.Remove(0, sb.Length);
        sb.Append(" select FLinkMan,fcompany,FFunction,FTel,FDepartmentID ");
        sb.Append(" from cf_sys_user where fid='" + fuserId + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            txtFSeeTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now); ;
            t_FAppPerson.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppPersonUnit.Text = RBase.GetDepartmentName(dt.Rows[0]["FDepartmentID"].ToString()) + RBase.GetDepartmentName(dt.Rows[0]["FCompany"].ToString());
        }
        sb.Remove(0, sb.Length);
        sb.Append(" select pr.FIdea,qa.FAppID, qa.ProjectName, i.JSDW, i.JSDWDZ ");
        sb.Append(" from TC_BZGD_Record qa, CF_App_ProcessInstance pi, v_Prj_Info i, CF_App_ProcessRecord pr ");
        sb.Append(" where pi.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and pi.flinkId = qa.FAppId and i.FId = qa.FPrjId and pi.fId = pr.FProcessInstanceID and pi.FID = '" + fpid + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FAppId"] = dt.Rows[0]["FAppID"].ToString();
            t_ProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
            //t_RecordNo.Text = dt.Rows[0]["RecordNo"].ToString();
            //t_PrjItemName.Text = dt.Rows[0]["PrjItemName"].ToString();
            //t_PrjItemType.SelectedValue = dt.Rows[0]["ProjectType"].ToString();
            t_JSDW.Text = dt.Rows[0]["JSDW"].ToString();
            t_Address.Text = dt.Rows[0]["JSDWDZ"].ToString();
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
        }
        EgovaDB dbContext = new EgovaDB();
        sb.Remove(0, sb.Length);
        sb.Append(" select FId,FAppPerson,FCompany,FFunction,FIdea,FRoleDesc,case FResult when 1 then '通过' when 3 then '不通过' end as FResultStr from CF_App_ProcessRecord where FTypeID in (1,10) and FLinkId = '" + ViewState["FAppId"] + "'");
        dt = rc.GetTable(sb.ToString());
        AppInfo_List.DataSource = dt;
        AppInfo_List.DataBind();
    }

    protected void AppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

            string fappResult = e.Item.Cells[7].Text;
            switch (fappResult)
            {
                case "1":
                    e.Item.Cells[7].Text = "通过";
                    break;

                case "3":
                    e.Item.Cells[7].Text = "不通过";
                    break;
            }
        }
    }
    void BindControl()
    {
        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // this.AcceptApp();
        try
        {
            EgovaDB db = new EgovaDB();
            string userID = Session["DFUSerId"].ToString();
            int userLevel = int.Parse(Session["DFLevel"].ToString());
            string idea = t_FAppIdea.Text;
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == userID).FirstOrDefault();
            CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == ferid).FirstOrDefault();
            //CF_App_ProcessRecord
            er.FAppPerson = user.FName;
            er.FCompany = user.FCompany;
            er.FFunction = user.FFunction;
            er.FIdea = idea;
            er.FLevel = userLevel;
            er.FUserId = userID;

            db.SubmitChanges();
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('保存成功');window.returnValue='1';", true);
        }
        catch
        {
            
        }
        ShowInfo();
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        // this.AcceptApp();
        string userID = Session["DFUSerId"].ToString();
        int userLevel = int.Parse(Session["DFLevel"].ToString());
        string idea = t_FAppIdea.Text;

        if(wf.Accept(ferid, userID, userLevel, idea))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('办结成功');window.returnValue='1';", true);
        }
        //ShowInfo();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // this.BatchApp();
        string userID = Session["DFUSerId"].ToString();
        string id = Session["DFId"].ToString();
        string roleId = Session["DFRoleId"].ToString();
        string idea = t_FAppIdea.Text;
        if(wf.Rollback(fLinkId, userID, roleId, id, idea))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "js", "alert('退回成功');window.returnValue='1';", true);
        }
        //ShowInfo();
    }
    protected void btnEndApp_Click(object sender, EventArgs e)
    {
        // this.BatchEndApp();
        string userID = Session["DFUSerId"].ToString();
        string idea = t_FAppIdea.Text;
        wf.BackAndEnd(fLinkId, userID, idea);
        //ShowInfo();
    }
}