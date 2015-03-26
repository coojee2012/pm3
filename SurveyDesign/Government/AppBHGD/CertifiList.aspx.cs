using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EgovaDAO;
using System.Text;

public partial class Government_AppBHGD_FZList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadList();
        }
    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        {
            sb.Append(" and qa.ProjectName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        }
        //if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        //{
        //    sb.Append(" and qa.PrjItemName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        //}
        //if (this.txtRecordNo.Text.Trim() != "" && this.txtRecordNo.Text.Trim() != null)
        //{
        //    sb.Append(" and qa.RecordNo like '%" + this.txtRecordNo.Text.Trim() + "%' ");
        //}
        //if (this.txtJSDW.Text.Trim() != "" && this.txtJSDW.Text.Trim() != null)
        //{
        //    sb.Append(" and qa.JSDW like '%" + this.txtJSDW.Text.Trim() + "%' ");
        //}
        //if (this.txtSDate.Text.Trim() != "" && this.txtSDate.Text.Trim() != null)
        //{
        //    sb.Append(" and ep.FReportDate >='" + this.txtSDate.Text.Trim() + " " + "00:00:00' ");
        //    //        sb.Append(" and DATEDIFF(day,'2008-12-29','2008-12-30') ");
        //}
        //if (this.txtEDate.Text.Trim() != "" && this.txtEDate.Text.Trim() != null)
        //{
        //    sb.Append(" and ep.FReportDate  <='" + this.txtEDate.Text.Trim() + " " + "23:59:59' ");
        //}
        //if (dbFBatchNoId.SelectedValue != "")
        //{
        //    sb.Append(" and ep.fid in ");
        //    sb.Append(" (select FAppId from CF_App_AppBatchNo t1 ");
        //    sb.Append(" where t1.fappid= ep.fid ')");
        //}


        if (ddlState.SelectedValue != "-1")
        {
            switch (ddlState.SelectedValue.Trim())
            {
                case "0": //未复审
                    sb.Append(" and er.FMeasure=0 and ep.fstate<>2 ");
                    break;
                case "1": //复审已通过
                    sb.Append(" and (er.FMeasure=5 and er.FResult=1) ");
                    break;
                case "3": //复审未通过
                    sb.Append(" and (ep.fstate=6 and er.FResult=3 and er.FMeasure<>0) ");
                    break;
                case "5": //已退回
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

    private void LoadList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from ( ");
        //下面的查询备份表
        sb.Append(" select qa.ProjectName,qa.JSDW,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '未复审' when 2 then '已退回' when 3 then '打回下级' ");
        sb.Append(" when 5 then '复审未通过' when 6 then case er.FResult when 1 then '复审已通过' when 3 then '复审未通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstanceBackup ep , CF_App_ProcessRecordBackup er, TC_BZGD_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=10 ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId ");
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        //        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ap.FUpDeptId like '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        sb.Append(getCondi());
        sb.AppendLine(" ) ttt where 1=1 ");

        //if (!string.IsNullOrEmpty(t_FInt3.SelectedValue))
        //{
        //    sb.Append(" and FInt3='" + t_FInt3.SelectedValue + "' ");
        //}

        sb.AppendLine(" order by ttt.FReporttime desc,ttt.FBaseInfoId");
        this.Pager1.sql = sb.ToString();
        this.Pager1.controltype = "DataGrid";
        this.Pager1.controltopage = "gv_list";
        this.Pager1.pagecount = 15;
        this.Pager1.dataBind();       

    }


    protected void JustAppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ferId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FprId"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = fLinkId;
            box.Attributes["fpid"] = fid;
            box.Attributes["ferid"] = ferId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["fBaseInfoId"] = fBaseInfoId;
            box.Attributes["fMeasure"] = fMeasure;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.RecordCount * (this.Pager1.curpage - 1)).ToString();
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.curpage = e.NewPageIndex;
        LoadList();
    }
}