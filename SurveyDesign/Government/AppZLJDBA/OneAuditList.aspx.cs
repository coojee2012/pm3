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

public partial class Government_AppZLJDBA_OneAuditList : govBasePage
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
            ControlBind();
            ShowInfo();
        }
        else
        {
            ShowInfo();
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
        StringBuilder sb = new StringBuilder();
        //sb.Append(" select fdesc,fnumber from cf_sys_systemname ");
        //sb.Append(" where fisdeleted=0 ");
        //sb.Append(" and fnumber=260 ");//施工许可证的权限  
        //DataTable dt = rc.GetTable(sb.ToString());
        //if (dt == null || dt.Rows.Count == 0)
        //{
        //    Response.Write("<center><font style='font-size:12px' color='red'>对不起,您没有该系统的接见权限!</font></center>");
        //    Response.End();
        //    return;
        //}
        //this.dbSystemId.DataSource = dt;
        //this.dbSystemId.DataTextField = "FDesc";
        //this.dbSystemId.DataValueField = "FNumber";
        //this.dbSystemId.DataBind(); 

        //ShowBatnNo();


        //sb.Remove(0, sb.Length);

        //string fDefaultManageDept = Session["DFId"].ToString();
        //DataTable dt = rc.getAllupDeptId(fDefaultManageDept, 0, 3);
        //DataRow[] row = dt.Select();
        //if (row != null && row.Length > 0)
        //{
        //    for (int i = 0; i < row.Length; i++)
        //    {
        //        dt.Rows.Remove(row[i]);
        //    }
        //}

        //dbFManageDeptId.DataSource = dt;
        //dbFManageDeptId.DataTextField = "FName";
        //dbFManageDeptId.DataValueField = "FNumber";
        //dbFManageDeptId.DataBind();
        //dbFManageDeptId.Items.Insert(0, new ListItem("--请选择--", ""));

        //dbFManageDeptId.SelectedIndex = dbFManageDeptId.Items.IndexOf(dbFManageDeptId.Items.FindByValue(Session["DFId"].ToString()));
        //if (EConvert.ToInt(this.Session["DFLevel"]) > 1)
        //{
        //    dbFManageDeptId.Enabled = false;
        //}

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
                case "0": //未初审
                    sb.Append(" and er.FMeasure=0 and ep.fstate<>2 ");
                    break;
                case "1": //初审已通过
                    sb.Append(" and (er.FMeasure=5 and er.FResult=1) ");
                    break;
                case "3": //初审未通过
                    sb.Append(" and (ep.fstate=6 and er.FResult=3 and er.FMeasure<>0) ");
                    break;
                case "5": //已退回
                    sb.Append(" and (ep.fstate=2) ");
                    break;
            }
        }

        if (dbSystemId.SelectedValue != "")
        {
            sb.Append(" and ep.fsystemid='" + this.dbSystemId.SelectedValue.Trim() + "'");
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
        sb.Append(" case ep.fState when 1 then '未初审' when 2 then '已退回' when 3 then '打回下级' ");
        sb.Append(" when 5 then '初审未通过' when 6 then case er.FResult when 1 then '初审已通过' when 3 then '初审未通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstance ep , CF_App_ProcessRecord er, TC_QA_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=10 ");
      //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId ");
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
    //    sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")"); 
        sb.Append(" and ap.FUpDeptId like '" + Session["DFId"].ToString() + "' ");
        sb.Append(" and ep.FLinkId = ap.FId ");
        sb.Append(getCondi());
        //下面的查询备份表
        sb.Append(" union all ");
        sb.Append(" select qa.ProjectName,qa.PrjItemName,qa.RecordNo,qa.JSDW,ep.FId,er.FId as FprId,ep.FBaseInfoId,ep.FEntName,ep.FLinkId,ep.FEmpName,ep.FManageTypeId,ep.FListId,ep.FTypeId,ep.FLevelId,ep.FIsBase FIsPrime,ep.FReportDate,");
        sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,ep.FBarCode,");
        sb.Append(" case ep.fState when 1 then '未初审' when 2 then '已退回' when 3 then '打回下级' ");
        sb.Append(" when 5 then '初审未通过' when 6 then case er.FResult when 1 then '初审已通过' when 3 then '初审未通过' end end as FStatedesc,");
        sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime");
        sb.Append(" from CF_App_ProcessInstanceBackup ep , CF_App_ProcessRecordBackup er, TC_QA_Record qa, CF_APP_LIST ap");
        sb.Append(" where ep.fId = er.FProcessInstanceID and  er.FtypeId=10 ");
        //  sb.Append(" and ep.FSubFlowId = er.FSubFlowId ");
        sb.Append(" and ep.flinkId = er.FLinkId  and ep.flinkId = qa.FAppId ");
     //   sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
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

    protected void dbSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ShowBatnNo();
        ShowInfo();
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        int iCount = this.JustAppInfo_List.Items.Count;
        ArrayList array = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            string fLinkId = JustAppInfo_List.Items[i].Cells[JustAppInfo_List.Items[i].Cells.Count - 2].Text;
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
            sScript.Append(" obj.fsystemid= " + dbSystemId.SelectedValue.Trim() + ";");
            sScript.Append(" ShowWindow('OneAuditInfo.aspx?e=0',1300,9700,obj);} window.onload=app;</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), sScript.ToString());
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
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}