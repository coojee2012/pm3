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

public partial class Government_AppQualiInfo_ShowAppInfo : System.Web.UI.Page  
{
    RQuali rq = new RQuali();
    RCenter rc = new RCenter();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            ShowReportInfo();
            ShowAppInfo();
            ShowFileList();
        }
    }

    private void ShowReportInfo()
    {

        string sEntName = "";
        string sManageTypeId = "";
        string sReportDate = "";
        string sYear = "";
        string sId = "";


        if (Request["pid"] != null)
        {
            EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FEntName,FManageTypeId,FReportDate,FYear", "fid='" + Request["pid"] + "'");
            if (ep != null)
            {
                sEntName = ep.FEntName;
                sManageTypeId = ep.FManageTypeId.ToString();
                sReportDate = ep.FReportDate.ToShortDateString();
                sYear = ep.FYear.ToString();
                sId = ep.FId;
            }
            else
            {
                EaProcessInstanceBackup epb = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "FId,FEntName,FManageTypeId,FReportDate,FYear", "fid='" + Request["pid"] + "'");
                if (epb != null)
                {
                    sEntName = epb.FEntName;
                    sManageTypeId = epb.FManageTypeId.ToString();
                    sReportDate = epb.FReportDate.ToShortDateString();
                    sYear = epb.FYear.ToString();
                    sId = epb.FId;
                }
            }
        }
        else
        {

            EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FId,FEntName,FManageTypeId,FReportDate,FYear", "FlinkId='" + Request.QueryString["FID"] + "'");
            if (ep != null)
            {
                sEntName = ep.FEntName;
                sManageTypeId = ep.FManageTypeId.ToString();
                sReportDate = ep.FReportDate.ToShortDateString();
                sYear = ep.FYear.ToString();
                sId = ep.FId;
            }
            else
            {
                EaProcessInstanceBackup epb = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "FId,FEntName,FManageTypeId,FReportDate,FYear", "FlinkId='" + Request.QueryString["FID"] + "'");
                if (epb != null)
                {
                    sEntName = epb.FEntName;
                    sManageTypeId = epb.FManageTypeId.ToString();
                    sReportDate = epb.FReportDate.ToShortDateString();
                    sYear = epb.FYear.ToString();
                    sId = epb.FId;
                }
            }

        }

        liter_FBaseName.Text = sEntName;
        liter_FManageType.Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + sManageTypeId + "'");
        liter_FReportDate.Text = sReportDate;
        liter_FYear.Text = sYear;
        ViewState["PID"] = sId;
    }

    private void ShowAppInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" select r.fid,fentname,");
        sb.Append(" p.flistid,");
        sb.Append(" p.ftypeid,");
        sb.Append(" p.flevelid,");
        sb.Append(" p.FManageTypeId,");
        sb.Append(" r.fresult,R.FAppTime ,r.FReportTime,r.FIdea,r.FRoleDesc,");
        sb.Append(" r.FCompany,r.FFunction,r.FAppPerson, ");
        sb.Append(" (select top 1 fname from cf_sys_managetype where fnumber =p.fmanagetypeid)fmanagetypename");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(" and p.fid='" + ViewState["PID"].ToString() + "' ");
        sb.Append(" and r.fMeasure<>0 ");
        sb.Append(" order by FOrder");
        //sb.Append(" order by r.FManageDeptId, r.FAppTime desc ");

        DataTable dt = rc.GetTable(sb.ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            sb.Remove(0, sb.Length);
            sb.Append(" select r.fid,fentname,");
            sb.Append(" p.flistid,");
            sb.Append(" p.ftypeid,");
            sb.Append(" p.flevelid,");
            sb.Append(" p.FManageTypeId,");
            sb.Append(" r.fresult,R.FAppTime ,r.FReportTime,r.FIdea,r.FRoleDesc,");
            sb.Append(" r.FCompany,r.FFunction,r.FAppPerson, ");
            sb.Append(" (select top 1 fname from cf_sys_managetype where fnumber =p.fmanagetypeid)fmanagetypename");
            sb.Append(" from CF_App_ProcessInstanceBackUp p,CF_App_ProcessRecordBackUp r where ");
            sb.Append(" p.fid=r.FProcessInstanceID ");
            sb.Append(" and p.fid='" + ViewState["PID"].ToString() + "' ");
            sb.Append(" and r.fMeasure<>0 ");
            sb.Append(" order by FOrder");
            //sb.Append(" order by r.FManageDeptId, r.FAppTime desc ");
            dt = rc.GetTable(sb.ToString());
        }

        this.AppInfo_List.DataSource = dt;
        this.AppInfo_List.DataBind();
    }

    protected void AppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

            string fappResult = e.Item.Cells[7].Text;

            //string flevelid = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            //string ftypeid = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            //string flistid = e.Item.Cells[e.Item.Cells.Count - 4].Text;

            //string fReportInfo = "";
            //fReportInfo += rc.GetDicRemark(flistid);
            //if (fReportInfo != "")
            //{
            //    fReportInfo += "";
            //}
            //fReportInfo += rc.GetDicRemark(ftypeid);
            //if (fReportInfo != "")
            //{
            //    fReportInfo += "";
            //}
            //fReportInfo += rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber='" + flevelid + "'");

            switch (fappResult)
            {
                case "1":
                    e.Item.Cells[7].Text = "通过";
                    break;

                case "3":
                    e.Item.Cells[7].Text = "不通过";
                    break;
            }


            //e.Item.Cells[1].Text = fReportInfo;
        }
    }

    void ShowFileList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,FContent,FType from CF_Pub_Text where ftype='Approval' and  FLinkId='" + ViewState["lid"] +
                @"'  order by  ftime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DG_FileList.DataSource = dt;
            DG_FileList.DataBind();
        }
        else
        {
            DG_FileList.Visible = false;
        }

    }
    protected void DG_FileList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string fcontent = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FContent"));
            string fcontent1 = fcontent;
            if (!string.IsNullOrEmpty(fcontent) && fcontent.Contains("/"))
                fcontent = fcontent.Substring(fcontent.LastIndexOf("/") + 1).Substring(14);
            e.Item.Cells[1].Text = "<a href='" + ComFunction.FileServer(fcontent1) + "' target='_blank'>" + fcontent + "</a>";

        }
    }
}
