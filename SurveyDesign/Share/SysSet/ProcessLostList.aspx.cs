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
public partial class Government_ProcessManager_ProcessLostList : Page
{
    Share sh = new Share();
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    SaveAsBase sab = new SaveAsBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ControlBind();
            ShowInfo();
            ShowPostion();
        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = sh.GetSignValue(EntityTypeEnum.EsTree, "FName", "FNumber='" + Request["fcol"] + "'");
        }
    }

    private void ControlBind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fdesc,fnumber from cf_sys_systemname ");
        sb.Append(" where fisdeleted=0 ");
        sb.Append(" and fnumber not in (160,165)");
        sb.Append(" and fisdeleted=0 ");
        sb.Append(" order by forder ");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt == null || dt.Rows.Count == 0)
        {
            return;
        }
        this.dbSystemId.DataSource = dt;
        this.dbSystemId.DataTextField = "FDesc";
        this.dbSystemId.DataValueField = "FNumber";
        this.dbSystemId.DataBind();
        showDDType(this.dbSystemId.SelectedValue.Trim());
    }

    private void showDDType(string FSystemId)
    {
        if (FSystemId == null || FSystemId == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" select fname,fnumber from CF_Sys_ManageType ");
        sb.Append(" where FSystemId='" + FSystemId + "' and fmtypeid<>'0' order by forder ");
        DataTable dt = sh.GetTable(sb.ToString());
        this.dmType.DataSource = dt;
        this.dmType.DataTextField = "fname";
        this.dmType.DataValueField = "fnumber";
        this.dmType.DataBind();
        this.dmType.Items.Insert(0, new ListItem("全部", ""));
        this.ViewState["FSystemId"] = FSystemId;

    }

    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFName.Text.Trim() != "" && this.txtFName.Text.Trim() != null)
        {
            sb.Append(" and t3.FEntName like '%" + this.txtFName.Text.Trim() + "%' ");
        }

        if (this.dmType.SelectedValue.Trim() != "")
        {
            sb.Append(" and t3.FManageTypeId='" + dmType.SelectedValue + "'");
        }


        if (dbSystemId.SelectedValue != "")
        {
            sb.Append(" and t3.fsystemid='" + this.dbSystemId.SelectedValue.Trim() + "'");
        }

        if (sb.Length > 0)
        {
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }

    private void ShowInfo()
    {
        string shareName = sh.GetSysObjectContent("_Sys_dbShareName");
        StringBuilder sb = new StringBuilder();
        sb.Append(" select t3.fid,t3.fbaseinfoid,t3.flinkid,t3.fentname,t3.fmanagetypeid,t3.flistid,t3.ftypeid,t3.flevelid,t3.FReportDate ");
        sb.Append(" from CF_App_ProcessInstance t3");
        sb.Append(" where not exists ");
        sb.Append(" (select t1.fid from CF_App_ProcessInstance t1," + shareName + "CF_App_Process t2 where t1.fprocessid = t2.fid and t1.fid=t3.fid) ");
        sb.Append(GetCon());
        sb.Append(" order by FReportDate desc ");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void dbSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        showDDType(dbSystemId.SelectedValue.Trim());
        ShowInfo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fManageTypeId = e.Item.Cells[3].Text;
            string fListId = e.Item.Cells[4].Text;
            string fTypeId = e.Item.Cells[5].Text;
            string fLevelId = e.Item.Cells[6].Text;

            e.Item.Cells[3].Text = sh.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");


            if (fListId == "" || fListId == "&nbsp;")
            {
                fListId = "";
            }
            if (fTypeId == "" || fTypeId == "&nbsp;")
            {
                fTypeId = "";
            }
            if (fLevelId == "" || fLevelId == "&nbsp;")
            {
                fLevelId = "";
            }


            e.Item.Cells[4].Text = "";

            if (fListId != "")
            {
                fListId = sh.GetDicRemark(fListId);
                e.Item.Cells[4].Text += fListId;
            }

            if (fTypeId != "")
            {
                fTypeId = sh.GetDicRemark(fTypeId);
                e.Item.Cells[4].Text += fTypeId;
            }

            if (fLevelId != "")
            {
                fLevelId = sh.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber=" + fLevelId);
                e.Item.Cells[4].Text += fLevelId;
            }


            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance,
                "FBaseInfoId,FManageTypeId,fsystemid,FResult,FTime", "fid='" + pId + "'");
            if (ea == null)
            {
                return;
            }
            string fbid = ea.FBaseInfoId;
            string faid = fLinkId.Trim();
            string fmid = ea.FManageTypeId;
            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");


            string sUrl = "";
            sUrl = sh.getMTypeQurl(ea.FManageTypeId);
            sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1";
            e.Item.Cells[3].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[3].Text + "</a>";




            sUrl = sh.getSysQurl(ea.FSystemId);
            sUrl += "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid;
            string fDesc = "查询企业审核通过的信息";


        }
    }

    protected void btnOut_Click(object sender, EventArgs e)
    {

        JustAppInfo_List.Columns[0].Visible = false;
        string fOutTitle = lPostion.Text;
        sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
    }

    private void ReportProcess()
    {
        pageTool tool = new pageTool(this.Page);
        int iCount = this.JustAppInfo_List.Items.Count;
        if (iCount == 0)
        {
            return;
        }

        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 1].Text;
            CheckBox box = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                array.Add(fId);
            }
        }

        if (array.Count == 0)
        {
            tool.showMessage("请选择要重新上报的企业");
            return;
        }

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < array.Count; i++)
        {
            string sId = array[i].ToString();
            sb.Remove(0, sb.Length);
            sb.Append(" select fLinkId,FBaseInfoId,FSystemId,FIsNew from CF_App_ProcessInstance ");
            sb.Append(" where fid='" + sId + "'");

            DataTable dt = rc.GetTable(sb.ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                continue;
            }

            string sBaseInfoId = dt.Rows[0]["FBaseInfoId"].ToString();
            string sAppId = dt.Rows[0]["fLinkId"].ToString();
            string sSystemId = dt.Rows[0]["FSystemId"].ToString();
            string sIsNew = dt.Rows[0]["FIsNew"].ToString();


            sb.Remove(0, sb.Length);
            sb.Append(" select * from cf_app_detail where fappid='" + sAppId + "'");
            sb.Append(" and fisdeleted=0 ");
            DataTable dtTemp = rq.GetTable(sb.ToString());
            if (dtTemp == null || dtTemp.Rows.Count == 0)
            {
                continue;
            }

            sb.Remove(0, sb.Length);
            sb.Append(" select FUpDeptId from cf_app_list where fid ='" + sAppId + "'");
            string sUpDeptId = rq.GetSignValue(sb.ToString());
            if (sUpDeptId == null || sUpDeptId == "")
            {
                continue;
            }

            SortedList[] sl = new SortedList[dtTemp.Rows.Count];
            for (int q = 0; q < dtTemp.Rows.Count; q++)
            {
                DataRow row = dtTemp.Rows[q];
                sl[q] = new SortedList();
                sl[q].Add("FID", row["FID"].ToString());
                sl[q].Add("FAppId", row["FAppId"].ToString());
                sl[q].Add("FBaseInfoId", row["FBaseInfoId"].ToString());
                sl[q].Add("FManageTypeId", row["FManageTypeId"].ToString());
                sl[q].Add("FListId", row["FListId"].ToString());
                sl[q].Add("FTypeId", row["FTypeId"].ToString());
                sl[q].Add("FLevelId", row["FLevelId"].ToString());
                sl[q].Add("FIsPrime", row["FIsPrime"].ToString());
                sl[q].Add("FIsTemp", row["FIsTemp"].ToString());
                sl[q].Add("FBeginRoleId", row["FBeginRoleId"].ToString());
                sl[q].Add("FEndRoleId", row["FEndRoleId"].ToString());
                sl[q].Add("FAppDeptId", row["FAppDeptId"].ToString());
                sl[q].Add("FAppDeptName", row["FAppDeptName"].ToString());
                sl[q].Add("FAppTime", row["FAppTime"].ToString());
                sl[q].Add("FLeadId", row["FLeadId"].ToString());
                sl[q].Add("FLeadName", row["FLeadName"].ToString());
                sl[q].Add("FIsNew", sIsNew);
                sl[q].Add("FUpDept", sUpDeptId);
            }

            //if (ra.EntStartProcess(sBaseInfoId, sAppId, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), sSystemId, ComFunction.GetDefaultDept(), sl))
            //{
            //    tool.showMessage("重新上报成功");
            //    sb.Append(" update CF_App_List set FState=1 ");
            //    sb.Append(" where fid = '" + sAppId + "'");
            //    rq.PExcute(sb.ToString());
            //    ShowInfo();
            //}
            //else
            //{
            //    tool.showMessage("上报失败");
            //}
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ReportProcess();
    }
}
