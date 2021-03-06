﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.RuleCenter;

public partial class Government_AppWY_XMAuditList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        sb.Append(" select qa.XMMC,qa.XMDZ,ap.Fid as AppFid,ap.FBaseName,qa.JSDW,qa.XMSD,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ap.FName,ap.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '上报审批中' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, YW_WY_XM_JBXX qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=5");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId ");
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ap.FUpDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");
        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");

        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "JustAppInfo_List";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();

        if (Session["DFID"] != null)
        {
            govd_FRegistDeptId.fNumber = (string)Session["DFID"];
        }
        if (Session["DFLevel"] != null)
        {
            switch ((string)Session["DFLevel"])
            {
                case "1":
                    govd_FRegistDeptId.Dis(1);
                    break;
                case "2":
                    govd_FRegistDeptId.Dis(2);
                    break;
                case "3":
                    govd_FRegistDeptId.Dis(3);
                    break;
                default: break;
            }
        }
    }
    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string appfid = e.Item.Cells[e.Item.Cells.Count - 4].Text;
            string fmtype = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            //int fState = EConvert.ToInt(e.Item.Cells[e.Item.Cells.Count - 2].Text);
            if (e.CommandName == "See")
            {
                Session["GovLinkID"] = "1";
                Session["FManageTypeId"] = fmtype;
                Session["XMBH"] = "";
                Session["FAppId"] = appfid;
                //if (fState != 0 && fState != 2)
                Session["FIsApprove"] = 1;
                //else
                //    Session["FIsApprove"] = 0;BType
                Response.Write("<script language='javascript'>window.open('../../WYDW/AppMain/aIndex.aspx');</script>");
            }
        }
    }
    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ferId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FprId"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            string fManagetypeid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fpid"] = fid;
            box.Attributes["ferid"] = ferId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["fBaseInfoId"] = fBaseInfoId;
            box.Attributes["fMeasure"] = fMeasure;
            box.Attributes["fManagetypeid"] = fManagetypeid;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            e.Item.Cells[6].Text = rc.GetSignValue("select FFullName from CF_Sys_ManageDept where FNumber='" + e.Item.Cells[13].Text + "'");
        }
    }

    private string getCondi()
    {
        string strsql = "";
        if (txtXMMC.Text.Trim() != "")
        {
            strsql += " and qa.XMMC like '%" + txtXMMC.Text.Trim() + "%'";
        }
        if (txtQYMC.Text.Trim() != "")
        {
            strsql += " and ap.FBaseName like '%" + txtQYMC.Text.Trim() + "%'";
        }
        if (govd_FRegistDeptId.FNumber != null && govd_FRegistDeptId.FNumber != "51")
        {
            strsql += " and qa.XMSD like '" + govd_FRegistDeptId.FNumber + "%'";
        }
        if (ddrYWLX.SelectedValue != "-1")
        {
            strsql += " and ap.FManageTypeId='" + ddrYWLX.SelectedValue + "'";
        }
        return strsql;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}