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
using System.Drawing;
using ProjectData;

public partial class Government_AppMain_AcceptListJNCL : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ShowInfo();
            string abc = Request["fcol"].ToString();
            ShowPostion();
        }
    }
    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

        }
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (Request.QueryString["fmanagetypeid"] != null)
        {
            if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
                sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
            else
                sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        }
        if (!string.IsNullOrEmpty(txtFName.Text))
        { sb.Append(" and ep.FEntName like '%" + txtFName.Text + "%' "); }
        if (dbSeeState.SelectedValue != "")
        {
            switch (dbSeeState.SelectedValue.Trim())
            {
                case "0": //未接件
                    sb.Append(" and er.FMeasure=0 ");
                    break;
                case "1": //准予受理
                    sb.Append(" and (er.FMeasure=5 and er.FResult=1) ");
                    break;
                case "3": //不准予受理
                    sb.Append(" and (ep.fstate=6 and er.FResult=3) ");
                    break;
                case "5": //打回企业,重新上报
                    sb.Append(" and (ep.fstate=2) ");
                    break;
            }
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
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '上报审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");

        sb.Append(" ,l.FName as ywName");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=222 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }

    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));//e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fFResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult"));//e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));//e.Item.Cells[9].Text;
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));

            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[5].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;

            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                sUrl = "AcceptInfoJNCL.aspx?ftype=1&FLinkId=" + FLinkId + "&fSubFlowId=" + fSubFlowId;
                e.Item.Cells[8].Text = "未审核";
                fColor = "#ff9900";
            }
            if (fMeasure == "5" && fFResult == "1")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId=" + FLinkId;
                e.Item.Cells[8].Text = "准予受理";
                box.Enabled = false;
                fColor = "#339933";
            }
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance,
                "FBaseInfoId,FManageTypeId,fsystemid,FResult,FTime", "fid='" + fid + "'");
            EaSubFlow es = (EaSubFlow)rc.GetEBase(EntityTypeEnum.EaSubFlow, "", "fid='" + fSubFlowId + "'");
            if (fState == "6" && ea.FResult == "3") //流程结束并且不同意
            {

                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[8].Text = "不准予受理";
                box.Enabled = false;
                fColor = "#ff0000";
            }
            if (fState == "2")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[8].Text = "打回企业,重新上报";
                box.Enabled = false;
                fColor = "#b6589d";
            }
            e.Item.Cells[3].Text = "<font color='" + fColor + "'>" + e.Item.Cells[3].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',800,600);\" >" + e.Item.Cells[2].Text + "</a>";

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        string sql = this.Pager1.sql.ToString();
        DataTable dt = rc.GetTable(sql);
        if (dt != null)
        {
            this.JustAppInfo_List.DataSource = dt;
            this.JustAppInfo_List.DataBind();
            JustAppInfo_List.Columns[0].Visible = false;
            string fOutTitle = lPostion.Text;
            sab.SaveAsExc(this.JustAppInfo_List, fOutTitle, this.Response);
        }
    }
    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            int fState = EConvert.ToInt(e.Item.Cells[e.Item.Cells.Count - 2].Text);
            if (e.CommandName == "See")
            {
                this.Session["FAppId"] = FLinkId;
                this.Session["FManageTypeId"] = "4001";
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>window.open('../../NCCLEnt/AppMain/aIndex.aspx');</script>");
            }
        }
    }
}