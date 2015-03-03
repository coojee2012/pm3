using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSDW_ApplyYDGH_AuditMain_LandPlanList : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
           // uegovdeptid.fNumber = "-1";
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));//e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fFResult = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult"));//e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//e.Item.Cells[e.Item.Cells.Count - 5].Text;
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));//e.Item.Cells[9].Text;
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string Id = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ID"));
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            string YWBM = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "YWBM"));
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fSubFlowId"] = fSubFlowId;

            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                if (FManageTypeId == "5050")//房建
                    sUrl = "../../JSDW/ApplyYDGH/AppMain/index.aspx?YD_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
                else//市政
                    sUrl = "../../JSDW/ApplyYDGHSZ/AppMain/index.aspx?YD_Id=" + Id + "&fAppId=" + YWBM + "&audit=1";
                e.Item.Cells[12].Text = "未审核";
                fColor = "#ff9900";
            }
            else if (fMeasure == "5" && fFResult == "1")
            {
                e.Item.Cells[12].Text = "准予受理";
                //box.Enabled = false;
                box.Attributes["audit"] = "1";
                fColor = "#339933";
            }else if (fState == "6" && fFResult == "3") //流程结束并且不同意
            {

                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[12].Text = "不准予受理";
                //box.Enabled = false;
                box.Attributes["audit"] = "0";
                fColor = "#ff0000";
            }
            else if (fState == "2")
            {
                //sUrl = "AcceptInfoGF.aspx?FLinkId" + FLinkId;
                e.Item.Cells[12].Text = "打回企业,重新上报";
                box.Enabled = false;
                fColor = "#b6589d";
            }
            e.Item.Cells[3].Text = "<font color='" + fColor + "'>" + e.Item.Cells[3].Text + "</font>";
            if (!string.IsNullOrEmpty(sUrl))
                e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('" + sUrl + "',1000,600);\" >" + e.Item.Cells[3].Text + "</a>";
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
        sb.Append(" case ep.FManageTypeId when '5050' then '房建' when '5060' then '市政' end as AuditType,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");

        sb.Append(" ,b.XMSDMC,b.ID,b.YWBM,b.BH,b.XMMC,b.JSDZ,b.JSDWMC,b.YDMJ,b.ZJZMJ,b.LXDH ");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_YDGH b on l.FId=b.YWBM");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=1122 and ep.FManageTypeId in('5050','5060')");
        //if (string.IsNullOrEmpty(ddlGCLB.SelectedValue))
        //    sb.Append(" and ep.FManageTypeId in('5050','5060')");
        //else
        //    sb.AppendFormat(" and ep.FManageTypeId ='{0}'",ddlGCLB.SelectedValue);
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
       // sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
       // sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        //sb.Append(" and er.FtypeId=1 "); //存子流程类别 1接件
        sb.Append(GetCondition());
        sb.AppendLine(" ) ttt where 1=1 ");
        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");
      
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "DG_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();
    }
    private string GetCondition()
    {
        StringBuilder _builer = new StringBuilder();
        //if (!string.IsNullOrEmpty(txtCompanyName.Text))
        //    _builer.AppendFormat(" AND ep.FEntName like '%{0}%'", txtCompanyName.Text);
        //if (!string.IsNullOrEmpty(uegovdeptid.fNumber) && uegovdeptid.fNumber != "-1")
        //    _builer.AppendFormat(" AND b.XMSD like '{0}%'", uegovdeptid.fNumber);
        if (!string.IsNullOrEmpty(txtXMMC.Text))
            _builer.AppendFormat(" AND b.XMMC like '%{0}%'", txtXMMC.Text);
        if (!string.IsNullOrEmpty(txtXMBH.Text))
            _builer.AppendFormat(" AND b.BH like '%{0}%'", txtXMBH.Text);
        if (!string.IsNullOrEmpty(txtJSDW.Text))
            _builer.AppendFormat(" AND b.JSDWMC like '%{0}%'", txtJSDW.Text);
        if (!string.IsNullOrEmpty(txtStart.Text))
            _builer.AppendFormat(" AND CONVERT(varchar(100), ep.FReportDate, 23) >= '{0}'", txtStart.Text);
        if (!string.IsNullOrEmpty(txtEnd.Text))
            _builer.AppendFormat(" AND CONVERT(varchar(100), ep.FReportDate, 23) <= '{0}'", txtEnd.Text);
        if (dbSeeState.SelectedValue != "")
        {
            if (dbSeeState.SelectedValue == "0") //未审核
                _builer.Append(" and er.FMeasure=0 ");
            else if (dbSeeState.SelectedValue == "1")//初审已通过fMeasure == "5" && fFResult == "1"
                _builer.Append(" and er.FMeasure=5 and er.FResult=1");
            else //初审未通过
                _builer.Append(" and er.FMeasure=5 and er.FResult in(2,3)");
            //switch (dbSeeState.SelectedValue.Trim())
            //{
            //    case "0": //未接件
            //        _builer.Append(" and er.FMeasure=0 ");
            //        break;
            //    case "1": //准予受理
            //        _builer.Append(" and (er.FMeasure=5 and er.FResult=1) ");
            //        break;
            //    case "3": //不准予受理
            //        _builer.Append(" and (ep.fstate=6 and er.FResult=3) ");
            //        break;
            //    case "5": //打回企业,重新上报
            //        _builer.Append(" and (ep.fstate=2) ");
            //        break;
            //}
        }
        return _builer.ToString();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}