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
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
public partial class Government_ProcessManager_FSubFlowList : System.Web.UI.Page
{
    Share sh = new Share();
    Share rc = new Share();
    RApp ra = new RApp("dbShare");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
        }
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        if (Request["fpid"] == null || Request["fpid"] == "")
        {
            return;
        }

        EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + Request["fpid"] + "'");
        if (ep == null)
        {
            return;
        }
        if (string.IsNullOrEmpty(Request.QueryString["Backup"]))
        {
            btnQuery.Visible = false;
        }
        this.lPostion.Text = sh.GetSignValue(EntityTypeEnum.EaProcess, "Fname", "fid='" + ep.FProcessId + "'");

        this.ViewState["FSubFowId"] = ep.FSubFlowId;
        this.ViewState["FID"] = ep.FId;

        sb.Remove(0, sb.Length);
        sb.Append(" select * ");
        sb.Append(" from CF_App_SubFlow ");
        sb.Append(" where FProcessId='" + ep.FProcessId + "' order by forder");
        DataTable dt = sh.GetTable(sb.ToString());
        this.JustAppInfo_List.DataSource = dt;
        this.JustAppInfo_List.DataBind();
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();

            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;

            if (fId == this.ViewState["FSubFowId"].ToString())
            {
                e.Item.BackColor = System.Drawing.Color.Green;
                if (string.IsNullOrEmpty(Request.QueryString["Backup"]))
                {
                    e.Item.Enabled = false;
                }
            }
            string fTypeId = e.Item.Cells[3].Text;
            string fRoleId = e.Item.Cells[4].Text;
            string fIsEend = e.Item.Cells[6].Text;
            string fIsQuali = e.Item.Cells[7].Text;

            e.Item.Cells[4].Text = sh.GetSignValue(EntityTypeEnum.EsRole, "FName", "FNumber='" + fRoleId + "'");


            switch (fTypeId)
            {
                case "1":
                    e.Item.Cells[3].Text = "接件";
                    break;

                case "3":
                    e.Item.Cells[3].Text = "正常审核";
                    break;

                case "10":
                    e.Item.Cells[3].Text = "初审";
                    break;

                case "5":
                    e.Item.Cells[3].Text = "负责人审核";
                    break;

                case "7":
                    e.Item.Cells[3].Text = "归档";
                    break;

                case "15":
                    e.Item.Cells[3].Text = "公示";
                    break;


                case "20":
                    e.Item.Cells[3].Text = "建设部审核";
                    break;

                case "25":
                    e.Item.Cells[3].Text = "转外审核";
                    break;



            }


            switch (fIsQuali)
            {
                case "1":
                    e.Item.Cells[7].Text = "是";
                    break;

                case "2":
                    e.Item.Cells[7].Text = "否";
                    break;

            }


            switch (fIsEend)
            {
                case "1":
                    e.Item.Cells[6].Text = "是";
                    break;

                case "2":
                    e.Item.Cells[6].Text = "否";
                    break;

            }




        }
    }
    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

            EaProcessInstance ep = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + Request["fpid"] + "'");
            if (ep == null)
            {
                return;
            }

            pageTool tool = new pageTool(this.Page);

            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            EaSubFlow es = (EaSubFlow)sh.GetEBase(EntityTypeEnum.EaSubFlow, "", "FId='" + fId + "'");

            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrSo = new ArrayList();
            ArrayList arrKey = new ArrayList();

            SortedList sl = new SortedList();
            sl.Add("FID", ViewState["FID"].ToString());
            sl.Add("FSubFlowId", es.FId);

            string sManageDeptId = ep.FManageDeptId.ToString();
            string sManageDeptIdNow = string.Empty;

            if (ep.FSystemId == "182")//入监理企业 
            {
                //ra.GetCurrDeptJlOut(ep.FManageDeptId.ToString(), es.FId);
            }
            else
                sManageDeptIdNow = ra.GetCurrDept(ep.FLinkId, ep.FBaseInfoId, es.FId);
            if (sManageDeptIdNow == null)
            {
                tool.showMessage("跳转失败,找不到上报部门:" + sManageDeptIdNow);
                return;
            }
            if (sManageDeptId.Length < sManageDeptIdNow.Length)
            {
                sl.Add("FManageDeptId", sManageDeptIdNow);
                sl.Add("FCurStepID", sManageDeptIdNow);
            }
            sl.Add("FRoleId", es.FRoleId);

            arrEn.Add(EntityTypeEnum.EaProcessInstance);
            arrSl.Add(sl);
            arrKey.Add("FID");
            arrSo.Add(SaveOptionEnum.Update);




            int fReportCount = EConvert.ToInt(rc.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where FProcessInstanceId='" + ViewState["FID"].ToString() + "'"));//最大步骤
            fReportCount++;

            sl = new SortedList();
            sl.Add("FID", Guid.NewGuid().ToString());
            sl.Add("FSubFlowId", es.FId);
            sl.Add("FIsDeleted", 0);
            sl.Add("FProcessInstanceID", ViewState["FID"].ToString());
            sl.Add("FLinkId", ep.FLinkId);
            sl.Add("FMeasure", 0);
            sl.Add("FResult", "");
            sl.Add("FManageDeptId", ra.GetCurrDept(ep.FLinkId, ep.FBaseInfoId, es.FId));
            sl.Add("FRoleId", es.FRoleId);
            sl.Add("FRoleDesc", es.FName);
            sl.Add("FTypeId", es.FTypeId);
            sl.Add("FIsPrint", es.FIsPrint);
            sl.Add("FIsQuali", es.FIsQuali);
            sl.Add("FOrder", es.FOrder);
            sl.Add("FReporttime", DateTime.Now);
            sl.Add("FReportCount", fReportCount);//审核步骤
            if (EConvert.ToString(Session["FIsAdmin"]) == "1")//如果是超级管理员
            {
                sl.Add("FUserId", "超级管理员");
            }
            arrEn.Add(EntityTypeEnum.EaProcessRecord);
            arrSl.Add(sl);
            arrKey.Add("FID");
            arrSo.Add(SaveOptionEnum.Insert);

            string shareName = sh.GetSysObjectContent("_Sys_dbShareName");
            StringBuilder sb = new StringBuilder();
            sb.Append(" delete from CF_App_ProcessRecord ");
            sb.Append(" where FProcessInstanceID = '" + ep.FId + "'");
            sb.Append(" and (FSubFlowId in ");
            sb.Append(" (select fid  from " + shareName + "CF_App_SubFlow where FProcessId='" + ep.FProcessId + "' and Forder>=" + es.FOrder + ")");
            sb.Append(" or FSubFlowId not in (select fid from " + shareName + "CF_App_SubFlow where FProcessId='" + ep.FProcessId + "'))");


            sb.Append(" update CF_App_ProcessRecord set FMeasure=5 ,FResult=1,FIdea='同意'");
            sb.Append(" where FProcessInstanceID = '" + ep.FId + "'");
            sb.Append(" and FSubFlowId in ");
            sb.Append(" (select fid  from " + shareName + "CF_App_SubFlow where FProcessId='" + ep.FProcessId + "' and Forder<" + es.FOrder + ")");
            rc.PExcute(sb.ToString());
            SaveInfo(arrEn, arrSl, arrKey, arrSo);

            tool.showMessage("跳转成功");
            ShowInfo();
        }
    }
    private void SaveInfo(ArrayList arrEn, ArrayList arrSl, ArrayList arrKey, ArrayList arrSo)
    {
        int iCount = arrEn.Count;
        EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
        SortedList[] sls = new SortedList[iCount];
        string[] fkeys = new string[iCount];
        SaveOptionEnum[] sos = new SaveOptionEnum[iCount];

        for (int i = 0; i < iCount; i++)
        {
            ens[i] = (EntityTypeEnum)arrEn[i];
            sls[i] = new SortedList();
            sls[i] = (SortedList)arrSl[i];
            fkeys[i] = "FID";
            sos[i] = (SaveOptionEnum)arrSo[i];
        }

        rc.SaveEBaseM(ens, sls, fkeys, sos);
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["fpid"]))
        {
            string sql = @"insert into CF_App_ProcessInstance
                    select * from CF_App_ProcessInstancebackup where FId='" + Request.QueryString["fpid"] + @"'
                    if(@@rowcount>0)
                    begin
                    delete from CF_App_ProcessInstancebackup where FId='" + Request.QueryString["fpid"] + @"'
                    update CF_App_ProcessInstance set FAppState=0,FState=0 where  FId='" + Request.QueryString["fpid"] + @"'
                    end
                    insert into CF_App_ProcessRecord 
                    select * from CF_App_ProcessRecordbackup where FProcessInstanceID='" + Request.QueryString["fpid"] + @"'
                    if(@@rowcount>0)
                    begin
                    delete from CF_App_ProcessRecordbackup where FProcessInstanceID='" + Request.QueryString["fpid"] + @"'
                    update CF_App_ProcessRecord set FMeasure=0 from CF_App_ProcessRecord r 
                    inner join CF_App_ProcessInstance i on r.FSubFlowId=i.FSubFlowId and FProcessInstanceID=i.FId where  i.FId='" + Request.QueryString["fpid"] + @"' 
                    end";

            rc.PExcute(sql);
            ShowInfo();
        }
    }
}
