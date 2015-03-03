using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectBLL;
using ProjectData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class JSDW_ApplyXZYJS_AuditMain_FirstAcceptJieJian : System.Web.UI.Page
{
    private RCenter rc = new RCenter();
    private RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfFLinkId.Value = YWBM;
            hfFlowId.Value = fSubFlowId;
            hfType.Value = TypeId;
            if (TypeId == "10")
                ltrTitle.Text = "初审";
            else
            {
                ltrTitle.Text = "复审";
                btnAccept.Text = "办结";
            }
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
                    hfXMBH.Value = row["XMBH"].ToString();
                    hfProjectType.Value = row["ProjectType"].ToString();
                    hfYWBM.Value = row["YWBM"].ToString();
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
            hfFunction.Value = dt.Rows[0]["FFunction"].ToString();
            txtFunction.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();

            hfProcessRecordFID.Value = dt.Rows[0]["FId"].ToString();
            hfProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
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
                hfFunction.Value = dt.Rows[0]["FFunction"].ToString();
                txtFunction.Text = dt.Rows[0]["FFunction"].ToString();
            }

            string sSql = string.Format(@"select er.FID,a.FID as PFID from CF_App_ProcessInstance a,CF_App_ProcessRecord er
               where er.FMeasure='0' and a.fid=er.FProcessInstanceID and a.fsubflowid=er.fsubflowid and a.fstate<>6 
               and a.FLinkId='" + YWBM + "' and er.FSubFlowId='" + fSubFlowId + "'");
            dt = rc.GetTable(sSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                hfProcessRecordFID.Value = dt.Rows[0]["FId"].ToString();
                hfProcessInstanceID.Value = dt.Rows[0]["PFID"].ToString();
            }
        }
    }
    /// <summary>
    /// 查询附件
    /// </summary>
    /// <param name="YJS_ID"></param>
    private void ShowFile(string YJS_ID)
    {
        string sql = string.Format("select A.ID,A.[FILE_NAME],COUNT(B.ID) TOTAL,A.Number FROM YW_FILE A LEFT JOIN (select * from YW_FILE_DETAIL) B ON A.ID=B.[FileId] WHERE A.YWBM='{0}'  GROUP BY A.[FILE_NAME],a.ID,A.Number", YJS_ID);
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
                _builder.AppendFormat("<td>{0}</td>", row["Number"]);
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
    protected void btnSaveYJ_Click(object sender, EventArgs e)
    {
        //SaveFileMessage();
        saveIdear();
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("保存成功");
    }
    private void SaveFileMessage()
    {
        string file = hfFile.Value;
        if (!string.IsNullOrEmpty(file))
        {
            StringBuilder _builder = new StringBuilder();
            _builder.AppendFormat("delete from YW_FILE_REMARK where YWBM='{0}'", YWBM);
            string[] files = hfFile.Value.Split('|');
            foreach (var item in files)
            {
                string[] items = item.Split('-');
                _builder.AppendFormat("insert into YW_FILE_REMARK(YWBM,FileDetailId,IsHave,Remark)values('{0}',{1},{2},'{3}');", YWBM, items[0], items[1], items[2]);
            }
            rc.PExcute(_builder.ToString());
        }
    }
    private void saveIdear()
    {
        SortedList osort = new SortedList();
        if (!string.IsNullOrEmpty(hfProcessRecordFID.Value))
        {
            osort.Add("FID", hfProcessRecordFID.Value);
            osort.Add("FAppPerson", t_FAppPerson.Text);
            osort.Add("FCompany", t_FAppPersonUnit.Text);
            osort.Add("FFunction", txtFunction.Text);
            osort.Add("FAppTime", t_FAppDate.Text);
            osort.Add("FIdea", t_FAppIdea.Text);
            osort.Add("FResult", dResult.SelectedValue.Trim());
            rc.SaveEBase(EntityTypeEnum.EaProcessRecord, osort, "FID", SaveOptionEnum.Update);
        }
    }
    /// <summary>
    /// 上报流程
    /// </summary>
    private void ReportProcess()
    {
        saveIdear();
        string FLinkId = YWBM;
        EaProcessRecord Er = (EaProcessRecord)rc.GetEBase(EntityTypeEnum.EaProcessRecord, "FResult,FProcessInstanceId", "FID='" + hfProcessRecordFID.Value + "'");
        SortedList[] sl = new SortedList[1];
        sl[0] = new SortedList();
        sl[0].Add("FID", hfProcessRecordFID.Value);
        //sl[0].Add("FProcessInstanceId", t_FProcessInstanceID.Value);
        sl[0].Add("FProcessInstanceID", hfProcessInstanceID.Value);
        sl[0].Add("FLinkId", FLinkId);
        sl[0].Add("FMeasure", 5);
        if (Er != null && !string.IsNullOrEmpty(Er.FResult))
            sl[0].Add("FResult", Er.FResult);
        else
            sl[0].Add("FResult", dResult.SelectedValue.Trim());
        sl[0].Add("FUserId", this.Session["DFUserId"].ToString());
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + hfProcessInstanceID.Value + "'");
        if (epi == null)
        {
            pageTool tool = new pageTool(this.Page);
            tool.showMessage("流程错误");
            return;
        }
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        sl[0].Add("FWaiteTime", spanTime.Days);
        //ra.BatchAppKCSJ(sl);
        ra.GovReportProcessKCSJ(sl, "");
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        //SaveFileMessage();
        ReportProcess();
        //if (TypeId == "10")
        //    tool.showMessageAndRunFunction("初审提交成功", "window.returnValue='1';window.close();");
        //else {
        if (TypeId == "10")
            tool.showMessageAndRunFunction(btnAccept.Text + "成功", "window.returnValue='1';window.close();");
        else
        {
            //if (dResult.SelectedValue == "1")
            //{
            SaveYDGH();
            string sql = "EXEC SP_GD_YDGH @XMBH,@BH";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter() { ParameterName = "@XMBH", Value = hfId.Value, SqlDbType = SqlDbType.VarChar };//编号
            param[1] = new SqlParameter() { ParameterName = "@BH", Value = hfXMBH.Value, SqlDbType = SqlDbType.VarChar }; //项目编码
            rc.PExcute(sql, param);//办结归档

            sql = "EXEC JKC_PRO_DZ_insertYDGH @ywbm";//打证
            rc.PExcute(sql, new SqlParameter() { ParameterName = "@ywbm", Value = hfYWBM.Value, SqlDbType = SqlDbType.VarChar });
            // }
            tool.showMessageAndRunFunction(btnAccept.Text + "成功", "window.returnValue='1';window.close();");
        }
        //tool.showMessageAndRunFunction(btnAccept.Text + "提交成功", "window.returnValue='1';window.close();");
        //}
    }
    private void BatchApp()
    {
        ProjectDB db = new ProjectDB();
        string backIdea = t_FAppIdea.Text;
        backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
        if (hfFLinkId.Value == "")
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        StringBuilder sbDescription = new StringBuilder();//记录该操作的关键信息
        string[] strs = hfFLinkId.Value.Split(',');
        int iCount = strs.Length;
        sb.Append("select fid,fbaseInfoId from CF_App_list where fid in (");
        for (int i = 0; i < iCount; i++)
        {
            if (i == 0)
            {
                sb.Append("'" + strs[i].Trim() + "'");
            }
            else
            {
                sb.Append(",'" + strs[i].Trim() + "'");
            }
        }
        sb.Append(") ");
        DataTable dtFLinkIds = rc.GetTable(sb.ToString());
        if (dtFLinkIds != null && dtFLinkIds.Rows.Count > 0)
        {
            iCount = dtFLinkIds.Rows.Count;
            for (int ii = 0; ii < iCount; ii++)
            {
                string FLinkId = EConvert.ToString(dtFLinkIds.Rows[ii][0]);
                string fbaseInfoId = EConvert.ToString(dtFLinkIds.Rows[ii][1]);
                //查询是否有办结的，如果没有，可以打回到企业
                sb.Remove(0, sb.Length);
                sb.Append("select count(*) from (");
                sb.Append(" select fid from cf_App_ProcessInstanceBackup where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                sb.Append(" union ");
                sb.Append(" select fid from cf_App_ProcessInstance where fstate=6 and flinkId='" + FLinkId + "'");//已经办结的流程
                sb.Append(")tt");
                iCount = rc.GetSQLCount(sb.ToString());
                if (iCount > 0)//如果有办结的流程，不可打回 |继续下一轮
                    continue;
                //否则
                sb.Remove(0, sb.Length);
                sb.Append("select ep.fId,er.FId erFId,'" + Session["DFUserId"] + "' FUserId ");
                sb.Append(" from cf_App_ProcessInstance ep ");
                sb.Append(" inner join cf_App_ProcessRecord er on ep.FId=er.FProcessInstanceId ");
                sb.Append(" where ep.FSubFlowId=er.FSubFlowId ");
                //sb.Append(" and er.FRoleId in (" + Session["DFRoleId"].ToString() + ")");
                sb.Append(" and ep.FManageDeptId = '" + Session["DFId"].ToString() + "' ");
                sb.AppendFormat(" and er.FtypeId={0} and isnull(er.FMeasure,0) in (0,4) ", TypeId); //存子流");
                sb.Append(" and ep.FState<>6 and ep.FLinkId='" + FLinkId + "'");
                DataTable dt = rc.GetTable(sb.ToString());
                if (ra.BatchBack(dt, FLinkId, backIdea))
                {
                    //记录打回 企业 操作
                    string title = "打回企业操作 操作人(DFUserId)：" + Session["DFUserId"];
                    string description = "关键key:FLinkId" + FLinkId + " backIdea:" + backIdea;
                    DataLog.Write(LogType.Info, LogSort.Operation, title, description);
                }
                //保存到CF_App_Idea
                CF_App_Idea idea = db.CF_App_Idea.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
                if (idea == null)
                {
                    idea = new CF_App_Idea()
                    {
                        FId = Guid.NewGuid().ToString(),
                        FCreateTime = DateTime.Now,
                        FLinkId = FLinkId,
                        FIsdeleted = 0,
                    };
                    db.CF_App_Idea.InsertOnSubmit(idea);
                }
                idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FReportCount).FirstOrDefault();
                idea.FResult = "打回";
                idea.FResultInt = 2;
                idea.FContent = t_FAppIdea.Text;
                idea.FAppTime = DateTime.Now;
                idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                idea.FTime = DateTime.Now;
            }
            db.SubmitChanges();
        }
    }
    private string TypeId
    {
        get
        {
            return Request.QueryString["typeId"];
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
    protected void btnExit_Click(object sender, EventArgs e)
    {
        BatchApp();
        pageTool tool = new pageTool(this.Page);
        tool.showMessageAndRunFunction("打回成功", "window.returnValue='1';window.close();");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //SaveFileMessage();
        saveIdear();
        SaveYDGH();
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("保存成功");
    }
    private void SaveYDGH()
    {
        if (TypeId == "5")
        {
            string sql = "Update YW_YDGH set YDGHXKZBH=@YDGHXKZBH,HFJG=@HFJG,HFRQ=@HFRQ where YWBM=@YWBM";
            SqlParameter[] prams = new SqlParameter[4];
            prams[0] = new SqlParameter() { ParameterName = "@YDGHXKZBH", Value = txtYDGHXKZBH.Text, SqlDbType = SqlDbType.VarChar };
            prams[1] = new SqlParameter() { ParameterName = "@HFJG", Value = txtHFJG.Text, SqlDbType = SqlDbType.VarChar };
            prams[2] = new SqlParameter() { ParameterName = "@HFRQ", Value = txtHFRQ.Text, SqlDbType = SqlDbType.Date };
            prams[3] = new SqlParameter() { ParameterName = "@YWBM", Value = YWBM, SqlDbType = SqlDbType.VarChar };
            rc.PExcute(sql, prams);
        }
    }
}
