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

public partial class Government_AppMain_GFseeAppList : System.Web.UI.Page
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
        if (this.txtFName.Text.Trim() != "" && this.txtFName.Text.Trim() != null)
        {
            sb.Append(" and ep.FEntName like '%" + this.txtFName.Text.Trim() + "%' ");
        }
        if (this.txtFPrjName.Text.Trim() != "" && this.txtFPrjName.Text.Trim() != null)
        {
            sb.Append(" and ep.FEmpName like '%" + this.txtFPrjName.Text.Trim() + "%' ");
        }

        if (Request.QueryString["fmanagetypeid"] != null)
        {
            if (Request.QueryString["fmanagetypeid"].IndexOf(",") > -1)
                sb.Append(" and ep.fmanagetypeid in (" + Request.QueryString["fmanagetypeid"] + ") ");
            else
                sb.Append(" and ep.fmanagetypeid='" + Request["fmanagetypeid"] + "'");
        }

        if (!string.IsNullOrEmpty(t_FListName.SelectedValue.Trim()))
        {
            sb.Append(" and b.FListName = '" + t_FListName.SelectedValue.Trim() + "'");

            if (t_FListName.SelectedValue == "其他")
            { sb.Append(" and b.FTypeName like '%" + t_FTypeName1.Text.Trim() + "%'"); }
            else if (!string.IsNullOrEmpty(t_FTypeName.SelectedValue.Trim()))
            {
                sb.Append(" and b.FTypeName = '" + t_FTypeName.SelectedValue.Trim() + "'");
            }
        }
        if (!string.IsNullOrEmpty(t_FUpDeptName.fNumber))
        {
            if (t_FUpDeptName.fNumber != "51")
                sb.Append(" and b.FUpDeptName='" + t_FUpDeptName.fNumber + "'");
        }
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

        sb.Append(" ,b.FUpDeptName,b.FListName,b.FTypeName ");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_GF_JBQK b on l.FId=b.YWBM");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=220 ");
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
            if (e.Item.Cells[6].Text != "&nbsp;")
                e.Item.Cells[6].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FName", "Fnumber='" + e.Item.Cells[6].Text + "'");
            e.Item.Cells[7].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;

            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                sUrl = "AcceptInfoGF.aspx?ftype=1&FLinkId=" + FLinkId + "&fSubFlowId=" + fSubFlowId;
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
            e.Item.Cells[2].Text = "<font color='" + fColor + "'>" + e.Item.Cells[2].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"javascript:showAddWindow('" + sUrl + "',800,600);\" >" + e.Item.Cells[2].Text + "</a>";

        }
    }
    protected void t_FListName_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindTypeName();
    }
    public void bindTypeName()
    {
        if (!string.IsNullOrEmpty(t_FListName.SelectedValue) && t_FListName.SelectedValue != "--请选择--")
        {
            if (t_FListName.SelectedValue == "房屋建筑工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("地基与基础", "地基与基础"));
                this.t_FTypeName.Items.Insert(2, new ListItem("主体结构", "主体结构"));
                this.t_FTypeName.Items.Insert(3, new ListItem("钢结构", "钢结构"));
                this.t_FTypeName.Items.Insert(4, new ListItem("装饰与屋面", "装饰与屋面"));
                this.t_FTypeName.Items.Insert(5, new ListItem("水电与智能", "水电与智能"));
                this.t_FTypeName.Items.Insert(6, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "土木工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("公路", "公路"));
                this.t_FTypeName.Items.Insert(2, new ListItem("铁路", "铁路"));
                this.t_FTypeName.Items.Insert(3, new ListItem("隧道", "隧道"));
                this.t_FTypeName.Items.Insert(4, new ListItem("桥梁", "桥梁"));
                this.t_FTypeName.Items.Insert(5, new ListItem("堤坝与电站", "堤坝与电站"));
                this.t_FTypeName.Items.Insert(6, new ListItem("矿山", "矿山"));
                this.t_FTypeName.Items.Insert(7, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "工业安装工程")
            {
                t_FTypeName.Visible = true; t_FTypeName1.Visible = false;
                this.t_FTypeName.Items.Clear();
                this.t_FTypeName.Items.Insert(0, new ListItem("--请选择--", ""));
                this.t_FTypeName.Items.Insert(1, new ListItem("工业设备", "工业设备"));
                this.t_FTypeName.Items.Insert(2, new ListItem("工业管道", "工业管道"));
                this.t_FTypeName.Items.Insert(3, new ListItem("电气装置与自动化", "电气装置与自动化"));
                this.t_FTypeName.Items.Insert(4, new ListItem("其他", "其他"));
            }
            else if (t_FListName.SelectedValue == "其他")
            {
                this.t_FTypeName.Visible = false; t_FTypeName1.Visible = true;
                t_FTypeName1.Text = null;
            }
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
                this.Session["FManageTypeId"] = "4000";
                Session["FIsApprove"] = 1;
                Response.Write("<script language='javascript'>window.open('../../GFEnt/AppMain/aIndex.aspx');</script>");
            }
        }
    }
}