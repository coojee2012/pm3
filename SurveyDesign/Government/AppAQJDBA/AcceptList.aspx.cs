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

public partial class Government_AppAQJDBA_AcceptList : govBasePage
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

    private string getCondi()
    {
        StringBuilder sb = new StringBuilder();
        if (this.txtFProjectName.Text.Trim() != "" && this.txtFProjectName.Text.Trim() != null)
        {
            sb.Append(" and qa.ProjectName like '%" + this.txtFProjectName.Text.Trim() + "%' ");
        }
        if (this.txtFPrjItemName.Text.Trim() != "" && this.txtFPrjItemName.Text.Trim() != null)
        {
            sb.Append(" and qa.PrjItemName like '%" + this.txtFPrjItemName.Text.Trim() + "%' ");
        }
        if (this.txtRecordNo.Text.Trim() != "" && this.txtRecordNo.Text.Trim() != null)
        {
            sb.Append(" and qa.RecordNo like '%" + this.txtRecordNo.Text.Trim() + "%' ");
        }
        if (this.txtJSDW.Text.Trim() != "" && this.txtJSDW.Text.Trim() != null)
        {
            sb.Append(" and qa.JSDW like '%" + this.txtJSDW.Text.Trim() + "%' ");
        }
        if (this.txtSDate.Text.Trim() != "" && this.txtSDate.Text.Trim() != null)
        {
            sb.Append(" and ep.FReportDate >='" + this.txtSDate.Text.Trim() + " " + "00:00:00' ");
        }
        if (this.txtEDate.Text.Trim() != "" && this.txtEDate.Text.Trim() != null)
        {
            sb.Append(" and ep.FReportDate  <='" + this.txtEDate.Text.Trim() + " " + "23:59:59' ");
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
        sb.Append(" select qa.ProjectName,qa.PrjItemName,qa.RecordNo,qa.JSDW,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '待接件' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then '审批完成' end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, TC_AJBA_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=1 ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
        sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
        sb.Append(" and ap.FUpDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        sb.Append(getCondi());
        //下面的查询备份表
        sb.Append(" union all ");
        sb.Append(" select qa.ProjectName,qa.PrjItemName,qa.RecordNo,qa.JSDW,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '待接件' when 2 then '打回企业' when 3 then '打回下级' ");
        sb.Append(" when 5 then '未审批证书' when 6 then case er.FResult when 1 then '审批完成,通过' when 3 then '审批完成,不通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstanceBackup ep , CF_App_ProcessRecordBackup er, TC_AJBA_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=1 ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId "); //去掉这行，表示可以查询已经处理了到了下一阶段的业务
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

    protected void dbSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 1].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                if (!array.Contains(fLinkId))
                {
                    array.Add(fLinkId);
                }
            }
        }
        if (array.Count == 0)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择')</script>");
            return;
        }


        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Count; i++)
        {
            if (i == 0)
            {
                sb.Append(array[i].ToString());
            }
            else
            {
                sb.Append("," + array[i].ToString());
            }
        }
        if (sb.Length > 0)
        {
            StringBuilder sScript = new StringBuilder();
            sScript.Append("<script>function app(){var obj = new Object();");
            sScript.Append(" var tmpVal='';");
            sScript.Append(" obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" obj.fsystemid= 1122;");
            sScript.Append(" ShowWindow('AcceptSeeOneReportInfo.aspx?e=0',900,700,obj);} window.onload=app;</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }




    protected void btnBack_Click(object sender, EventArgs e)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 2].Text;
            CheckBox cb = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (cb.Checked == true)
            {
                array.Add(fLinkId);
            }
        }
        if (array.Count == 0)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择')</script>");
            return;
        }


        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Count; i++)
        {
            if (i == 0)
            {
                sb.Append(array[i].ToString());
            }
            else
            {
                sb.Append("," + array[i].ToString());
            }
        }
        if (sb.Length > 0)
        {
            StringBuilder sScript = new StringBuilder();
            sScript.Append("<script>function app(){var obj = new Object();");
            sScript.Append("var tmpVal='';");
            sScript.Append("obj.name='51js';");
            sScript.Append(" tmpVal ='" + sb.ToString() + "';");
            sScript.Append(" obj.id= tmpVal;");
            sScript.Append(" ShowWindow('BackSeeOneReportInfo.aspx?e=0',900,700,obj);}window.onload=app;</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
        }
    }
    protected void btnAddBathNo_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //if (this.dbFBatchNoId.SelectedValue == "")
        //{
        //    tool.showMessage("请选择要加入的批次");
        //    return;
        //}

        int iCount = JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();
        for (int i = 0; i < iCount; i++)
        {
            string fId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 1].Text;
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Columns.Count - 2].Text;

            CheckBox box = (CheckBox)JustAppInfo_List.Items[i].Cells[0].Controls[1];
            if (box.Checked == true)
            {
                if (!array.Contains(fLinkId))
                {
                    array.Add(fLinkId);
                }
            }
        }


        if (array.Count == 0)
        {
            tool.showMessage("请选择要加入批次的行");
            return;
        }


        iCount = array.Count;

        StringBuilder sb = new StringBuilder();
        sb.Append(" select fid from CF_App_ProcessInstance ");
        sb.Append(" where flinkid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i == 0)
            {
                sb.Append("'" + array[i].ToString() + "'");
            }
            else
            {
                sb.Append(",'" + array[i].ToString() + "'");
            }
        }
        sb.Append(")");

        DataTable dt = rc.GetTable(sb.ToString());

        iCount = dt.Rows.Count;
        SortedList[] sls = new SortedList[iCount];
        string[] keys = new string[iCount];
        EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
        SaveOptionEnum[] sos = new SaveOptionEnum[iCount];


        for (int j = 0; j < iCount; j++)
        {
            sls[j] = new SortedList();
            keys[j] = "FID";
            ens[j] = EntityTypeEnum.EaAppBatchNo;

            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_App_AppBatchNo ");
            sb.Append(" where FAppId='" + dt.Rows[j]["FID"].ToString() + "'");
            sb.Append(" and FDFId='" + this.Session["DFId"].ToString() + "'");
            sb.Append(" and fisdeleted=0 ");

            string fid = rc.GetSignValue(sb.ToString());
            if (fid == null || fid == "")
            {
                sos[j] = SaveOptionEnum.Insert;
                sls[j].Add("FID", Guid.NewGuid().ToString());
                //sls[j].Add("FBatchNoId", this.dbFBatchNoId.SelectedValue.Trim());
                sls[j].Add("FAppId", dt.Rows[j]["FID"].ToString());
                sls[j].Add("FDFId", this.Session["DFId"].ToString());
                sls[j].Add("FIsDeleted", 0);
                sls[j].Add("FCreateTime", DateTime.Now);
            }
            else
            {
                sos[j] = SaveOptionEnum.Update;
                sls[j].Add("FID", fid);
                //sls[j].Add("FBatchNoId", this.dbFBatchNoId.SelectedValue.Trim());
            }
        }
        if (rc.SaveEBaseM(ens, sls, keys, sos))
        {
            tool.showMessage("加入批次成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("加入批次失败");
        }
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