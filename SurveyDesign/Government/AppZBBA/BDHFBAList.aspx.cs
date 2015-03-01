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

public partial class Government_AppZBBA_BDHFBAList : govBasePage
{
    RCenter rc = new RCenter();
    SaveAsBase sab = new SaveAsBase();
    RApp ra = new RApp();
    RAppBacth rap = new RAppBacth();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
         //   ControlBind();
            ShowInfo();
            //ShowPostion();

        }
    }

    private void ShowPostion()
    {
        if (Request["fcol"] != null)
        {
            this.lPostion.Text = rc.GetMenuName(Request["fcol"].ToString());

        }
    }

    private void ControlBind()
    {
        

    }

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFProjectName.Text.Trim() != "" && this.txtFProjectName.Text.Trim() != null)
        {
            sb.Append(" and qa.ProjectName like '%" + this.txtFProjectName.Text.Trim() + "%' ");
        }

        if (ddlState.SelectedValue != "-1")
        {
            switch (ddlState.SelectedValue.Trim())
            {
                case "0": //待接件
                    sb.Append(" and er.FMeasure=0 and ep.fstate<>2 ");
                    break;
                case "1": //准予受理
                    sb.Append(" and (er.FMeasure=5 and er.FResult=1) ");
                    break;
                case "3": //不准予受理
                    sb.Append(" and (ep.fstate=6 and er.FResult=3 and er.FMeasure<>0) ");
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
        sb.Append(" select qa.ProjectName,qa.ZBBM,qa.ZGYSFS,qa.ZBLB,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '未审核' when 2 then '已退回' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未通过' when 6 then case er.FResult when 1 then '通过' when 3 then '不通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, TC_BDHF_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");
     //   sb.Append(" and  er.FtypeId=3  ");
      //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ap.FUpDeptId like '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        sb.Append(getCondi());
        //下面的查询备份表
        sb.Append(" union all ");
        sb.Append(" select qa.ProjectName,qa.ZBBM,qa.ZGYSFS,qa.ZBLB,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '未审核' when 2 then '已退回' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未通过' when 6 then case er.FResult when 1 then '通过' when 3 then '不通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstanceBackup ep , CF_App_ProcessRecordBackup er, TC_BDHF_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID ");
        //   sb.Append(" and  er.FtypeId=3  ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
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
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string fSubFlowId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSubFlowId"));
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string ferId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FprId"));
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            string fMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Attributes["id"] = "span" + box.ClientID;
            box.Attributes["name"] = FLinkId;
            box.Attributes["fpid"] = fid;
            box.Attributes["ferid"] = ferId;
            box.Attributes["fSubFlowId"] = fSubFlowId;
            box.Attributes["fBaseInfoId"] = fBaseInfoId;
            box.Attributes["fMeasure"] = fMeasure;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
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
}