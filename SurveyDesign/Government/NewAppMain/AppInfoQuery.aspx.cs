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
using System.Linq;
using ProjectData;

public partial class Government_NewAppMain_AppInfoQuery : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            ControlBind();
            showDDType(Request["fsystemid"]);
            ShowInfo();
            ShowPostion();
        }
    }

    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }

    private void showDDType(string FSystemId)
    {
        ProjectDB db = new ProjectDB();
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(" select fname,fnumber from CF_Sys_ManageType ");
        sb.AppendLine(" where  fmtypeid<>'0'");

        sb.AppendLine("   order by forder ");
        var varManageType = db.ManageType;
        if (string.IsNullOrEmpty(FSystemId))
        {
            varManageType = varManageType.Where(t => t.FMTypeId == 193);

        }
        else
        {
            varManageType = varManageType.Where(t => t.FSystemId == EConvert.ToInt(FSystemId));

        }
        var result = from m in varManageType
                     join m1 in db.ManageType.GroupBy(t => t.FName).Select(g => g.Max(t => t.FID)) on m.FID equals m1
                     where m.FMTypeId != 0
                     orderby m.FOrder
                     select new
                     {
                         m.FName,
                         FNumber = string.Join(",", db.ManageType.Where(t => t.FName == m.FName).Select(t => t.FNumber.ToString()).ToArray())
                     };


        //DataTable dt = rc.GetTable(sb.ToString());
        this.dmType.DataSource = result;
        this.dmType.DataTextField = "fname";
        this.dmType.DataValueField = "fnumber";
        this.dmType.DataBind();
        this.dmType.Items.Insert(0, new ListItem("全部", ""));

        if (!string.IsNullOrEmpty(Request.QueryString["FManageTypeId"]))
        {
            ListItem li = dmType.Items.FindByValue(Request.QueryString["FManageTypeId"]);
            if (li != null)
            {
                dmType.ClearSelection();
                li.Selected = true;
            }
        }

    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();


    }


    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text.Trim() != "" && this.txtFName.Text.Trim() != null)
        {
            sb.Append(" and ep.FEntName like '%" + this.txtFName.Text.Trim() + "%' ");
        }

        if (this.dmType.SelectedValue.Trim() != "")
        {
            sb.Append(" and ep.FManageTypeId in(" + dmType.SelectedValue + ") ");
        }

        if (Request["fsystemid"] != null && Request["fsystemid"] != "")
        {
            sb.Append(" and ep.FSystemId ='" + Request["fsystemid"] + "'");
        }


        if (dbFState.SelectedValue != "")
        {
            sb.Append(" and ep.fstate = " + dbFState.SelectedValue);
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FStartTime"]))
        {
            txtFReportDate.Text = Request.QueryString["FStartTime"];
            //txtFReportDate1.Text = EConvert.ToShortDateString(EConvert.ToDateTime(Request.QueryString["time"]).AddMonths(1));
        }
        else
        {
            sb.Append(" and ep.fstate in (1,2)");
        }
        if (!string.IsNullOrEmpty(Request.QueryString["FEndTime"]))
        {
            txtFReportDate1.Text = Request.QueryString["FEndTime"];

        }
        if (txtFReportDate.Text.Trim() != "")
        {
            sb.Append(" and ep.FReportDate >= '" + txtFReportDate.Text.Trim() + "'");
        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            sb.Append(" and ep.FReportDate <= '" + txtFReportDate1.Text.Trim() + "'");
        }
        sb.AppendLine(" and er.FRoleId in (" + Session["DFRoleId"] + ")");
        sb.AppendLine(" and ep.FManageDeptId like '" + Session["DFId"] + "%' ");
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select * from ");
        sb.Append(" (");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FManageTypeId,ep.FListId,ep.FTypeId,");
        sb.Append(" ep.FLevelId,ep.FIsPrime,ep.FReportDate,ep.FManageDeptId,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" ep.FSubFlowId,ep.FCurStepID,ep.FYear,ep.FResult,er.FMeasure,ep.FEmpName,ep.FEmpId,ep.FLeadId,ep.FLeadName,ep.fsystemid ");
        sb.Append(" from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");

        sb.Append(" and er.fid in ");
        sb.Append(" (select top 1 fid from CF_App_ProcessRecord t1 where t1.FProcessInstanceID= ep.fid order by t1.FReporttime desc)");
        sb.Append(getCondi());
        sb.Append(" union ");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName,ep.FManageTypeId,ep.FListId,ep.FTypeId,");
        sb.Append(" ep.FLevelId,ep.FIsPrime,ep.FReportDate,ep.FManageDeptId,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" ep.FSubFlowId,ep.FCurStepID,ep.FYear,ep.FResult,er.FMeasure,ep.FEmpName,ep.FEmpId,ep.FLeadId,ep.FLeadName,ep.fsystemid ");
        sb.Append(" from CF_App_ProcessInstanceBackup ep,CF_App_ProcessRecordBackup er");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");

        sb.Append(" and er.fid in ");
        sb.Append(" (select top 1 fid from CF_App_ProcessRecordBackup t1 where t1.FProcessInstanceID= ep.fid order by t1.FReporttime desc)");
        sb.Append(getCondi());
        sb.Append(" ) ttt");
        sb.Append(" order by ttt.FReportDate desc,ttt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            StringBuilder sb = new StringBuilder();
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLinkId"));
            string fManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fManageTypeId"));
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fState"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fBaseInfoId"));
            string fLevelId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fLevelId"));
            string fTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fTypeId"));
            string fListId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fListId"));
            string fManageDeptId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fManageDeptId"));

            string sSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));
            string sCurStepID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FCurStepID"));
            string sMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));

            e.Item.Cells[4].Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");

            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fBaseInfoId + "'");
            string fsystemid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fsystemid"));
            //查询查询地址
            string sUrl = "";
            sUrl = rc.getMTypeQurl(fManageTypeId);
            sUrl += "?fbid=" + fBaseInfoId + "&faid=" + fLinkId + "&frid=" + frid + "&fmid=" + fManageTypeId + "&fly=1";

            string sCurPos = "";
            sCurPos += rc.getDept(sCurStepID, 1);
            string stepName = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + sSubFlowId + "'");
            sCurPos += "<font color=red>(" + stepName + ")</font>";
            if (string.IsNullOrEmpty(stepName))
                stepName = "";
            string sDesc = stepName.PadLeft(2, ' ').Replace("厅", "").Replace("市", "").Replace("县", "").Replace("省", "").Replace("负责人", "审查");
            sDesc = sDesc.Substring(sDesc.Length - 2);
            string sResult = "";
            if (sMeasure != null)
            {
                if (sMeasure == "0")
                {
                    sResult = "<font color=red>未" + sDesc + "</font>";
                }
                if (sMeasure == "5")
                {
                    sResult = "<font color=green>已" + sDesc + "，已提交</font>";
                }
                if (sMeasure == "1")
                {
                    sResult = "<font color=green>已" + sDesc + "，未提交</font>";
                }
            }


            if (fState == "2")
            {
                sResult = "<font color=red>已打回</font>";
            }
            sCurPos += sResult;

            e.Item.Cells[5].Text = sCurPos;

            if (fState == "6")//已经办结
            {
                string fCertiState = "已办结";
                if (Request["fsystemid"] == "130"
                    || Request["fsystemid"] == "186"
                    || Request["fsystemid"] == "187")
                {
                    //需要检验证书的打印、领取状态
                    DataTable dt = rc.GetTable("select FIsPrint,FIsReceive from cf_Ent_QualiCerti where fbaseInfoId='" + fBaseInfoId + "' and fIsValid=1 and FAppId='" + fLinkId + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (EConvert.ToInt(dt.Rows[0]["FIsPrint"]) == 0)
                            fCertiState = "<font color='#111111;'>待打证</font>";
                        else if (EConvert.ToInt(dt.Rows[0]["FIsReceive"]) == 0)
                            fCertiState = "<font color='blue'>待领证</font>";
                    }
                }
                e.Item.Cells[5].Text = fCertiState;
            }
            string sStateColor = "";
            switch (fState)
            {
                case "6":
                    sStateColor = "#339933";
                    break;
                case "1":
                    sStateColor = "#000099";
                    break;
                case "2":
                    sStateColor = "#ff0000";
                    break;
            }
            sUrl = rc.getMTypeQurl(fManageTypeId);

            sUrl += "?e=0&fbid=" + fBaseInfoId + "&faid=" + fLinkId + "&frid=" + frid + "&fmid=" + fManageTypeId + "&sysid=" + fsystemid;


            string prjName = e.Item.Cells[1].Text;
            e.Item.Cells[1].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>";
            e.Item.Cells[1].Text += "<font color='" + sStateColor + "'>" + prjName + "</font></a>";

            string fBaseName = e.Item.Cells[3].Text;
            sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");
            if (fManageTypeId == "294")
            {
                fBaseName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntName"));
                sUrl += "?fbid=" + fBaseInfoId;

            }
            else
            {

                sUrl += "?fbid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLeadId"));
            }



            e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showApproveWindow1('" + sUrl + "&fly=1',980,450)\"  >" + fBaseName + "</a>";

            string prjid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEmpId"));

            if (fManageTypeId == "411" || fManageTypeId == "421"
                     || fManageTypeId == "412" || fManageTypeId == "422"
                     || fManageTypeId == "413" || fManageTypeId == "423"
                     || fManageTypeId == "414" || fManageTypeId == "424"
                        )//合同备案
            {
                CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FId == fLinkId).FirstOrDefault();
                if (data != null)
                {
                    e.Item.Cells[2].Text = data.FDeptName;
                }
            }
            else
            {
                e.Item.Cells[2].Text = db.CF_Prj_BaseInfo.Where(t => t.FId == prjid).Select(t => t.FAllAddress).FirstOrDefault();
            }


            string sAppContent = e.Item.Cells[5].Text;
            sUrl = "../AppQualiInfo/ShowAppList.aspx";
            sUrl += "?e=0&pid=" + fId;
            e.Item.Cells[5].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>";
            e.Item.Cells[5].Text += "" + sAppContent + "</a>";

            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(fLinkId))
            {
                string fPrjId = db.CF_App_List.Where(t => t.FId == fLinkId).Select(t => t.FPrjId).FirstOrDefault();
                if (!string.IsNullOrEmpty(fPrjId))
                {
                    var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == fPrjId)
                        .Select(t => new { t.FBGTime, t.FCount })
                        .FirstOrDefault();
                    if (prjBG != null && prjBG.FCount > 0)
                    {
                        e.Item.Cells[1].Text += "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                    }
                }
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }


    protected void btnOut_Click(object sender, EventArgs e)
    {
        JustAppInfo_List.Columns[0].Visible = false;
        string fOutTitle = lPostion.Text;
        sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
    }


}
