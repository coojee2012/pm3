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

public partial class Government_AppMain_batchList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?')");
            if (Request["fcol"] != null && Request["fcol"] != "")
            {
                lPostion.Text = rc.GetMenuName(Request["fcol"]);
            }
            ShowInfo();
        }
    }
    private string GetCon()
    {
        StringBuilder sb = new StringBuilder();
        if (txtFNumber.Text != "")
        {
            sb.Append(" and t1.FNumber like '%" + txtFNumber.Text + "%'");
        }
        if (txtFTtile.Text != "")
        {
            sb.Append(" and t1.FTtile like '%" + txtFTtile.Text + "%'");
        }
        if (txtFYear.Text != "")
        {
            sb.Append(" and t1.FYear =" + txtFYear.Text);
        }
        if (dbState.SelectedValue != "")
        {
            sb.Append(" and t1.FState =" + dbState.SelectedValue);
        }
        if (Request["fsystemid"] != null && Request["fsystemid"] != "")
        {
            sb.Append(" and t1.fsystemid='" + Request["fsystemid"] + "'");
        }
        return sb.ToString();
    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select t1.fid,t1.FNumber,t1.FTtile,t1.FYear,t1.FDFId,t1.FSystemID, ");
        sb.Append(" case t1.FState when 1 then '办结' when 0 then '未办结' end as FState,");
        sb.Append(" (select count(*) from CF_App_AppBatchNo where FBatchNoId =t1.fid) FCount");
        sb.Append(" from CF_App_BatchNo t1");
        sb.Append(" where t1.fisdeleted=0 and t1.FDFId ='" + this.Session["DFId"].ToString() + "'");
        sb.Append(GetCon());
        sb.Append(" order by t1.FNumber desc,t1.fcreatetime desc");
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
            string fId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            e.Item.Cells[1].Text = ((e.Item.ItemIndex + 1) + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string fDeptId = e.Item.Cells[5].Text;
            e.Item.Cells[5].Text = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FFullName", "fnumber='" + fDeptId + "'");


            string sScript = "showAddWindow('batchEdit.aspx?fid=" + fId + "&fsystemid=" + Request["fsystemid"] + "',600,300)";
            e.Item.Cells[2].Text = "<a href='#' class='link7' onclick=\"" + sScript + "\">" + e.Item.Cells[2].Text + "</a>";
        }
    }

    protected void JustAppInfo_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        String FBatchId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
        String FBatchName = rc.GetSignValue("SELECT FTTILE FROM CF_APP_BATCHNO WHERE FID='" + FBatchId + "'").Replace(" ", "");
        if (e.CommandName == "outPut")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from ");
            sb.Append(" ( ");
            sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName as 企业名称,ep.FManageTypeId,ep.FListId,ep.FTypeId,");
            sb.Append(" ep.FLevelId,ep.FIsPrime,ep.FReportDate,(SELECT FNAME FROM CF_SYS_MANAGEDEPT WHERE FNUMBER=EP.FMANAGEDEPTID )AS 属地,FPersonnel,FPerformance,");
            sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,");
            sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime,");
            sb.Append(" er.fisQuali,(Select FName From CF_Sys_ManageType Where FNumber=ep.FManageTypeId) 业务类型 ");
            sb.Append(" from CF_App_ProcessInstance ep ");
            sb.Append(" inner join CF_App_ProcessRecord er on ep.fId = er.FProcessInstanceID  and ep.FSubFlowId=er.FSubFlowId");
            //sb.Append(@" inner join (select max(ep.Fid) Fid from CF_App_ProcessInstance ep 
            //inner join CF_App_ProcessRecord er on ep.fId = er.FProcessInstanceID where er.FManageDeptId = '" + this.Session["DFId"] + "' group by ep.FLinkId,ep.FBaseInfoId) o on o.Fid=ep.Fid ");
            sb.Append(" where  ");
            sb.Append("  er.FManageDeptId = '" + this.Session["DFId"].ToString() + "' ");
            sb.Append(" and ep.fid in ");
            sb.Append(" (select FAppId from CF_App_AppBatchNo t1 ");
            sb.Append(" where t1.fappid= ep.fid and t1.FBatchNoId='" + FBatchId + "'and FDFId ='" + this.Session["DFId"].ToString() + "')");


            sb.Append(" union  ");

            sb.Append(" select ep.FId,ep.FBaseInfoId,ep.FLinkId,ep.FEntName as 企业名称,ep.FManageTypeId,ep.FListId,ep.FTypeId,");
            sb.Append(" ep.FLevelId,ep.FIsPrime,ep.FReportDate,(SELECT FNAME FROM CF_SYS_MANAGEDEPT WHERE FNUMBER=EP.FMANAGEDEPTID )AS 属地,FPersonnel,FPerformance,");
            sb.Append(" ep.FState,ep.FSeeState,ep.FSeeTime,");
            sb.Append(" ep.FSubFlowId,ep.FYear,ep.FResult,er.FResult FFResult,er.FAppTime,er.FMeasure,er.FReporttime,");
            sb.Append(" er.fisQuali,(Select FName From CF_Sys_ManageType Where FNumber=ep.FManageTypeId) 业务类型 ");
            sb.Append(" from CF_App_ProcessInstancebackup ep ");
            sb.Append(" inner join CF_App_ProcessRecordbackup er on ep.fId = er.FProcessInstanceID  and ep.FSubFlowId=er.FSubFlowId");
            //sb.Append(@" inner join (select max(ep.Fid) Fid from CF_App_ProcessInstancebackup ep 
            //inner join CF_App_ProcessRecordbackup er on ep.fId = er.FProcessInstanceID where er.FManageDeptId = '" + this.Session["DFId"] + "' group by ep.FLinkId,ep.FBaseInfoId) o on o.Fid=ep.Fid ");
            sb.Append(" where  ");
            sb.Append("  er.FManageDeptId = '" + this.Session["DFId"].ToString() + "' ");
            sb.Append(" and ep.fid in ");
            sb.Append(" (select FAppId from CF_App_AppBatchNo t1 ");
            sb.Append(" where t1.fappid= ep.fid and t1.FBatchNoId='" + FBatchId + "'and FDFId ='" + this.Session["DFId"].ToString() + "')");

            sb.Append(" ) t ");

            sb.Append(" order by t.FMeasure,t.FManageTypeId,t.FReporttime desc,t.FBaseInfoId");
            DataTable dtall = rc.GetTable(sb.ToString());
            DataTable _dtDefalut = dtall.DefaultView.ToTable(true, "FBaseInfoId", "FLinkId", "企业名称", "业务类型", "属地");
            _dtDefalut.Columns.Add("序号").SetOrdinal(0);
            _dtDefalut.Columns["企业名称"].SetOrdinal(1);
            _dtDefalut.Columns["业务类型"].SetOrdinal(2);
            _dtDefalut.Columns.Add("申报内容").SetOrdinal(3);
            _dtDefalut.Columns["属地"].SetOrdinal(4);
            _dtDefalut.Columns.Add("存在问题");
            _dtDefalut.Columns.Add("初审建议");
            _dtDefalut.Columns.Add("备注");
            int index = 1;
            foreach (DataRow item in _dtDefalut.Rows)
            {
                item["序号"] = index;
                String appQuali = "";
                DataRow[] drs = dtall.Select("FBaseInfoId='" + item["FBaseInfoId"].ToString() + "'");
                foreach (DataRow sDr in drs)
                {
                    if (appQuali.Length >= 1)
                        appQuali += "<br/>";
                    appQuali += rc.GetSignValue("SELECT (SELECT TOP 1 ISNULL(FNAME,'') FROM CF_SYS_DIC WHERE FNUMBER='" + sDr["FListId"].ToString() + "')+(SELECT TOP 1 ISNULL(FNAME,'') FROM CF_SYS_DIC WHERE FNUMBER='" + sDr["FTypeId"].ToString() + "')+(SELECT TOP 1 ISNULL(FNAME,'') FROM CF_Sys_QualiLevel WHERE FNUMBER='" + sDr["FLevelId"].ToString() + "')");
                }
                item["申报内容"] = appQuali;
                index++;
            }
            _dtDefalut.Columns.Remove("FBaseInfoId");
            _dtDefalut.Columns.Remove("FLinkId");


            string sFileName = FBatchName;
            string sHeadTittle = FBatchName;
            Response.Clear();
            Response.Buffer = true;
            // 设置了类型为中文防止乱码的出现

            // Response.Charset="GB2312";
            Response.Charset = "utf-8";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(sFileName, System.Text.Encoding.UTF8) + ".xls"); //定义输出文件和文件名 
            // 设置输出流为简体中文

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            // 设置输出文件类型为excel文件
            Response.ContentType = "application/ms-excel";

            System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            oHtmlTextWriter.Write("<div align=center height=25 valign=middle><font size='6'>" + sHeadTittle + "</font></div><br>");
            oHtmlTextWriter.Write("<div align='right' height=20 valign=middle>汇总时间：&nbsp;&nbsp;&nbsp;&nbsp;年&nbsp;&nbsp;月&nbsp;&nbsp;日</div><br>");
            DataGrid DG_List = new DataGrid();
            DG_List.DataSource = _dtDefalut;
            DG_List.DataBind();
            DG_List.ShowHeader = true;
            DG_List.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            oHtmlTextWriter.Close();
            oStringWriter.Close();

            Response.End();
            DG_List.Dispose();
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_App_BatchNo", "FId");
        tool.DelInfoFromGrid(this.JustAppInfo_List, sl, "dbShare"); ShowInfo();
    }

}