﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Approve.RuleCenter;

public partial class Government_AppXZYJS_QueryBLTJ : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowInfo();
            uegovdeptid.fNumber = "-1";
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
            string FtypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "typeId"));
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();

            string fColor = "", sUrl = "";
            if (fMeasure == "0")
            {
                string result = string.Empty;
                if (FtypeId == "1")
                    result = "未审核";
                else if (FtypeId == "2")//初审
                    result = "已接件";
                else if (FtypeId == "3")//复审
                    result = "初审已通过";
                e.Item.Cells[5].Text = result;
                fColor = "#ff9900";
            }
            else if (fMeasure == "5" && fFResult == "1")
            {
                string result = string.Empty;
                if (FtypeId == "2")//初审
                    result = "已接件";
                else if (FtypeId == "3")//复审
                    result = "初审已通过";
                e.Item.Cells[5].Text = result;
                fColor = "#339933";
            }
            else if (fState == "6" && fFResult == "3") //流程结束并且不同意
            {
                string result = string.Empty;
                if (FtypeId == "2")//初审
                    result = "接件未通过";
                else if (FtypeId == "3")//复审
                    result = "初审未通过";
                e.Item.Cells[5].Text = result;
                fColor = "#ff0000";
            }
            else if (fState == "6" && fFResult == "3") //流程结束并且不同意
            {
                e.Item.Cells[5].Text = "不准予受理";
                fColor = "#ff0000";
            }
            else if (fState == "2")
            {
                e.Item.Cells[5].Text = "打回企业,重新上报";
                fColor = "#b6589d";
            }
            e.Item.Cells[1].Text = "<font color='" + fColor + "'>" + e.Item.Cells[1].Text + "</font>";
            //if (!string.IsNullOrEmpty(sUrl))
            //    e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showWindow('" + sUrl + "');\" >" + e.Item.Cells[2].Text + "</a>";
        }
    }
    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,temp.typeId,");
        sb.Append(" case ep.fState when 1 then '上报审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" case ep.FManageTypeId when '4050' then '房建' when '4060' then '市政' end as AuditType,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");

        sb.Append(" ,b.XMSDMC,b.ID,b.YWBM ");

        sb.Append(" from CF_App_ProcessInstance ep inner join CF_App_ProcessRecord er");
        sb.Append(" on ep.fId = er.FProcessInstanceID ");

        sb.Append(" left join CF_App_List l on ep.FLinkId=l.FId ");
        sb.Append(" left join YW_XZYJS b on l.FId=b.YWBM");

        sb.Append(" inner join ( ");
        sb.Append(" select max(er.fid)fid,max(er.FtypeId) typeId from CF_App_ProcessInstance ep,(select FProcessInstanceID,FMeasure,FResult,case FtypeId when 10 then 2 when 5 then 3 else 1 end FtypeId,FID from CF_App_ProcessRecord) er");
        sb.Append(" where ep.fId = er.FProcessInstanceID and ep.fsystemid=1122 and ep.FManageTypeId in('4050','4060')");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and er.FtypeId in (1,2,3) "); //存子流程类别 1接件
        sb.Append(" group by ep.flinkId )temp on er.fid=temp.fid where 1=1 ");
        //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        //sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
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
        if (!string.IsNullOrEmpty(txtCompanyName.Text))
            _builer.AppendFormat(" AND ep.FEntName like '%{0}%'", txtCompanyName.Text);
        if (!string.IsNullOrEmpty(txtXZMC.Text))
            _builer.AppendFormat(" AND ep.FEmpName like '%{0}%'", txtXZMC.Text);
        if (!string.IsNullOrEmpty(uegovdeptid.fNumber) && uegovdeptid.fNumber != "-1")
            _builer.AppendFormat(" AND b.XMSD like '{0}%'", uegovdeptid.fNumber);
        //if (!string.IsNullOrEmpty(txtXMMC.Text))
        //    _builer.AppendFormat(" AND b.XMMC like '%{0}%'", txtXMMC.Text);
        //if (!string.IsNullOrEmpty(txtXMBH.Text))
        //    _builer.AppendFormat(" AND b.BH like '%{0}%'", txtXMBH.Text);
        //if (!string.IsNullOrEmpty(txtJSDW.Text))
        //    _builer.AppendFormat(" AND b.JSDWMC like '%{0}%'", txtJSDW.Text);
        //if (!string.IsNullOrEmpty(txtSQRQ.Text))
        //    _builer.AppendFormat(" AND CONVERT(varchar(100), b.CreateTime, 23) = '{0}'", txtSQRQ.Text);
        if (dbSeeState.SelectedValue != "")
        {
            switch (dbSeeState.SelectedValue.Trim())
            {
                case "0": //未审核
                    _builer.Append(" and er.FMeasure=0 and temp.typeId=1 ");
                    break;
                case "1": //审核中
                    _builer.Append(" and temp.typeId in (2,3) and er.FMeasure in(0,5) and er.FResult=1 ");
                    break;
                case "2":
                    _builer.Append(" and er.FMeasure=5 and er.FResult in(2,3)");
                    break;
                case "3": //不准予受理
                    _builer.Append(" and (ep.fstate=6 and er.FResult=3) ");
                    break;
            }
        }
        return _builer.ToString();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}