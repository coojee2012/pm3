using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using ProjectBLL;
using Approve.EntityBase;
using Approve.Common;
using Approve.RuleCenter;
using Approve.RuleApp;

public partial class Government_AppYDGH_PrintAccept : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Audit == "1")//已受理
                btnNoAccept.Visible = false;
            else
                btnAccept.Visible = false;
            hfFLinkId.Value = YWBM;
            if (!string.IsNullOrEmpty(YWBM))
            {
                string sql = @"select top 1 * from [YW_YDGH] where YWBM='" + YWBM + "'";
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page, "txt");
                    tool.fillPageControl(row);
                    ShowFile(row["ID"].ToString());
                    bindAuditList(row["YWBM"].ToString());
                    //BindProcessRecordFID();
                    bindAuditOne();
                    hfId.Value = row["ID"].ToString();
                    hfProjectType.Value = row["ProjectType"].ToString(); 
                    ltrTZSText.Text = string.Format("<a href=\"../../ReportPrint/PrintList.aspx?TypeId=505001&YWBM={0}\" target=\"_blank\" id=\"SLDY\" style=\"display:none\"></a><a href=\"../../ReportPrint/PrintList.aspx?TypeId=505002&YWBM={0}\" target=\"_blank\" id=\"NOSLDY\" style=\"display:none\"></a>", row["ID"]);
                }
            }
        }
    }
    public void bindAuditOne()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + YWBM + "' and FMeasure=0 and FSubFlowId='" + fSubFlowId + "'");
        sb.Append(" and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();

            if (dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dResult.SelectedValue = dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;
        }
        else
        {
            string sql = (@" select FLinkMan,fcompany,FFunction,FTel,FDepartmentID 
                    from cf_sys_user where fid='" + Session["DFUserId"] + "'");
            dt = rc.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_FAppPerson.Text = dt.Rows[0]["FLinkMan"].ToString();
                t_FAppPersonUnit.Text = RBase.GetDepartmentName(dt.Rows[0]["FDepartmentID"].ToString()) + RBase.GetDepartmentName(dt.Rows[0]["FCompany"].ToString());
                t_FAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

        }
    }
    /// <summary>
    /// 查询附件
    /// </summary>
    /// <param name="YJS_ID"></param>
    private void ShowFile(string YJS_ID)
    {
        string sql = string.Format("select A.ID,A.[FILE_NAME],COUNT(B.ID) TOTAL FROM YW_FILE A LEFT JOIN (select * from YW_FILE_DETAIL) B ON A.ID=B.[FileId] WHERE A.YWBM='{0}'  GROUP BY A.[FILE_NAME],a.ID", YJS_ID);
        DataTable table = rc.GetTable(sql);
        DataTable tableFile = GetSaveFileMessage();
        if (table != null && table.Rows.Count > 0)
        {
            System.Text.StringBuilder _builder = new System.Text.StringBuilder();
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                bool isHave = false;
                string reMark = string.Empty;
                if (tableFile != null)
                {
                    foreach (DataRow item in tableFile.Rows)
                    {
                        if (item["FileDetailId"].ToString() == row["ID"].ToString())
                        {
                            isHave = item["IsHave"].ToString() == "1" ? true : false;
                            reMark = item["Remark"].ToString();
                            break;
                        }
                    }
                }
                num++;
                _builder.Append("<tr class='clDetail'>");
                _builder.AppendFormat("<td value='{1}'>{0}</td>", num, row["ID"]);
                _builder.AppendFormat("<td>{0}</td>", row["FILE_NAME"]);
                _builder.AppendFormat("<td>{0}</td>", row["TOTAL"]);
                if (tableFile != null)
                {
                    _builder.AppendFormat("<td><input type='checkbox' {0} /></td>", isHave == true ? "checked='true'" : "");
                }
                else
                    _builder.AppendFormat("<td><input type='checkbox' /></td>");
                _builder.AppendFormat("<td><input type='button' value='查看({2})' onclick=\"ChooseFile('{0}','{1}','{3}')\"  class=\"m_btn_w2\"  /></td>", row["ID"], YJS_ID, row["TOTAL"], row["FILE_NAME"]);
                _builder.AppendFormat("<td><input type='text' value='{0}' /></td>", reMark);
                _builder.Append("</tr>");
            }
            ltrText.Text = _builder.ToString();
        }
    }
    /// <summary>
    /// 获取保存的文件信息
    /// </summary>
    /// <returns></returns>
    private DataTable GetSaveFileMessage()
    {
        string fileSql = string.Format("select * from YW_FILE_REMARK where YWBM='{0}'", YWBM);
        DataTable table = rc.GetTable(fileSql);
        if (table != null && table.Rows.Count > 0)
            return table;
        return null;
    }
    //绑定审批意见列表
    public void bindAuditList(string FId)
    {
        if (!string.IsNullOrEmpty(FId))
        {
            StringBuilder sb = new StringBuilder();
            sb.Remove(0, sb.Length);
            sb.Append(" select r.fid,fentname,");
            sb.Append(" p.flistid,R.FAppTime ,r.FReportTime,");
            sb.Append(" p.ftypeid,r.FIdea,r.FRoleDesc,p.flevelid,");
            sb.Append(" p.FManageTypeId,");
            sb.Append(" case r.fresult when 1 then '通过' when 3 then '退回' end fresult,");
            sb.Append(" r.FCompany,r.FFunction,r.FAppPerson ");
            sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
            sb.Append(" p.fid=r.FProcessInstanceID ");
            sb.Append(" and p.FLinkId='" + FId + "' ");
            sb.Append(" and r.fMeasure<>0  ");
            //sb.Append(" and ");
            //sb.Append(" (");
            //sb.Append(" (r.ftypeid<>1 and  (select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) >1)");
            //sb.Append(" or");
            //sb.Append(" ((select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) <=1)");
            //sb.Append(" )");
            sb.Append(" order by r.FOrder ");

            this.DG_List.DataSource = rc.GetTable(sb.ToString());
            this.DG_List.DataBind();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    private string Audit
    {
        get
        {
            return Request.QueryString["audit"];
        }
    }
    private string YWBM
    {
        get
        {
            return Request.QueryString["YD_Id"];
        }
    }
    private string fSubFlowId
    {
        get
        {
            return Request.QueryString["fSubFlowId"];
        }
    }
}
