using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;
using System.Drawing;
using ProjectBLL;
using ProjectData;

public partial class Government_AppMain_AcceptInfoJNCL : System.Web.UI.Page
{
    RCenter rc = new RCenter(); RApp ra = new RApp(); ProjectDB db = new ProjectDB();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfTypeId.Value = TypeId;
            if (TypeId == "1")
                btnAcccpt.Text = "接件";
            else if (TypeId == "10")
                btnAcccpt.Text = "上报审批";
            else if (TypeId == "5")
                btnAcccpt.Text = "复审";
            else if (TypeId == "20")
                btnAcccpt.Text = "办结";

            if (TypeId != "1")
            {
                ltrPerSon.Text = "审查人";
                ltrUnit.Text = "审查单位";
                ltrTime.Text = "审查时间";
                ltrComment.Text = "审查意见";
                ltrFunction.Text = "审查人职务";
            }
            if (!string.IsNullOrEmpty(YWBM))
            {
                hfFLinkId.Value = YWBM;
                string sql = string.Format(@"select TOP 1 B.QYMC,B.FRDB,A.SBRQ,B.LXR,B.LXDH,A.SQCPMC,A.CPLBMC,A.BSDJMC from YW_JNCL_PRODUCT A LEFT JOIN YW_JNCL_QYJBXX B ON A.YWBM = B.YWBM WHERE A.YWBM = '{0}'", YWBM);
                DataTable table = rc.GetTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    pageTool tool = new pageTool(this.Page,"txt");
                    tool.fillPageControl(row);
                    ltrSQXX.Text = string.Format("<tr><td>1</td><td>{0}</td><td>{1}</td><td>{2}</td></tr>", row["SQCPMC"], row["BSDJMC"], row["CPLBMC"]);
                }
                ShowFile();
                bindAccept();
                bindAuditList();
            }
        }
    }
    private void ShowFile()
    {
        string sql = string.Format("select A.ID,A.[FILE_NAME],COUNT(B.ID) TOTAL,A.Number FROM YW_FILE A LEFT JOIN YW_FILE_DETAIL B ON A.ID=B.[FileId] WHERE A.YWBM='{0}'  GROUP BY A.[FILE_NAME],a.ID,A.Number", YWBM);
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
                _builder.AppendFormat("<td><input type='button' value='查看({2})' onclick=\"ChooseFile('{0}','{1}','{3}')\"  class=\"m_btn_w2\"  /></td>", row["ID"], YWBM, row["TOTAL"], row["FILE_NAME"]);
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
    private void SaveFileMessage()
    {
        string file = hfFile.Value;
        if (!string.IsNullOrEmpty(file))
        {
            StringBuilder _builder = new StringBuilder();
            _builder.AppendFormat("delete from YW_FILE_REMARK where YWBM='{0}';", YWBM);
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
        if (!string.IsNullOrEmpty(t_ProcessRecordID.Value))
        {
            osort.Add("FID", t_ProcessRecordID.Value);
            osort.Add("FAppPerson", t_FAppPerson.Text);
            osort.Add("FCompany", t_FAppPersonUnit.Text);
            osort.Add("FFunction", txtFunction.Text);
            osort.Add("FAppTime", t_FAppDate.Text);
            osort.Add("FIdea", t_FAppIdea.Text);
            osort.Add("FResult", dResult.SelectedValue.Trim());
            rc.SaveEBase(EntityTypeEnum.EaProcessRecord, osort, "FID", SaveOptionEnum.Update);
        }
    }
    //绑定审批意见列表
    public void bindAuditList()
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
        sb.Append(" and p.fid='" + t_FProcessInstanceID.Value + "' ");
        sb.Append(" and r.fMeasure<>0  ");
        sb.Append(" order by r.FOrder ");

        this.DG_List.DataSource = rc.GetTable(sb.ToString());
        this.DG_List.DataBind();
    }
    //绑定接件意见
    public void bindAccept()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + YWBM + "' and FMeasure=0 and FSubFlowId='" + fSubFlowId + "'");
        sb.Append(" and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            //hfFunction.Value = dt.Rows[0]["FFunction"].ToString();
            txtFunction.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();

            t_ProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
            t_FProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
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
                //hfFunction.Value = dt.Rows[0]["FFunction"].ToString();
                txtFunction.Text = dt.Rows[0]["FFunction"].ToString();
            }

            string sSql = string.Format(@"select er.FID,a.FID as PFID from CF_App_ProcessInstance a,CF_App_ProcessRecord er
               where er.FMeasure='0' and a.fid=er.FProcessInstanceID and a.fsubflowid=er.fsubflowid and a.fstate<>6 
               and a.FLinkId='" + YWBM + "' and er.FSubFlowId='" + fSubFlowId + "'");
            dt = rc.GetTable(sSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_ProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
                t_FProcessInstanceID.Value = dt.Rows[0]["PFID"].ToString();
            }
        }
    }
    #region DataGrid 事件处理
    //各级审批意见
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    #endregion

    #region 上报流程
    private void ReportProcess()
    {
        saveIdear();
        string FLinkId = YWBM;
        EaProcessRecord Er = (EaProcessRecord)rc.GetEBase(EntityTypeEnum.EaProcessRecord, "FResult", "FID='" + t_ProcessRecordID.Value + "'");
        SortedList[] sl = new SortedList[1];
        sl[0] = new SortedList();
        sl[0].Add("FID", t_ProcessRecordID.Value);
        //sl[0].Add("FProcessInstanceId", t_FProcessInstanceID.Value);
        sl[0].Add("FProcessInstanceID", t_FProcessInstanceID.Value);
        sl[0].Add("FLinkId", FLinkId);
        sl[0].Add("FMeasure", 5);
        if (Er != null && !string.IsNullOrEmpty(Er.FResult))
            sl[0].Add("FResult", Er.FResult);
        else if (t_ftype.Value == "1")
            sl[0].Add("FResult", dResult.SelectedValue.Trim());
        //else sl[0].Add("FResult", dAudit.SelectedValue.Trim());
        sl[0].Add("FUserId", this.Session["DFUserId"].ToString());
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + t_FProcessInstanceID.Value + "'");
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
    #endregion

    #region 打回到企业
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
                sb.Append(" and ep.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
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
    #endregion

    #region 打回
    protected void BackToPre(string appid)
    {
        string FLinkId = appid;
        int fReportCount = EConvert.ToInt(rc.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + FLinkId + "'"));//最大步骤
        fReportCount++;
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessInstance, "FID", "flinkid='" + FLinkId + "' AND fstate=1");
        int iCount = dt.Rows.Count;
        if (iCount > 0)
        {
            ArrayList arrEn = new ArrayList();
            ArrayList arrSl = new ArrayList();
            ArrayList arrKey = new ArrayList();
            ArrayList arrSo = new ArrayList();
            for (int i = 0; i < iCount; i++)
            {
                string fProcessInstanceId = dt.Rows[i]["FID"].ToString();
                SortedList sl = new SortedList();
                SortedList sl1 = new SortedList();
                SortedList sl2 = new SortedList();
                DataTable dtTemp = rc.GetTable("select * from cf_App_ProcessRecord where FProcessinstanceId='" + fProcessInstanceId + "' ");
                string fSubFlowId = string.Empty;
                string fRoleId = string.Empty;
                if (dtTemp != null && dtTemp.Rows.Count > 0)//如果有这一步
                {
                    sl1.Clear();
                    DataRow drTemp = dtTemp.Rows[0];
                    sl1.Add("FID", System.Guid.NewGuid().ToString());
                    sl1.Add("FProcessInstanceID", fProcessInstanceId);
                    sl1.Add("FLinkId", drTemp["FLinkId"]);
                    sl1.Add("FRoleDesc", drTemp["FRoleDesc"]);
                    sl1.Add("FMeasure", 4);//被打回状态
                    sl1.Add("FReportTime", DateTime.Now);
                    sl1.Add("FRoleId", drTemp["FRoleId"]);
                    sl1.Add("FSubFlowId", drTemp["FSubFlowId"]);
                    fRoleId = drTemp["FRoleId"].ToString();//角色
                    fSubFlowId = drTemp["FSubFlowId"].ToString();//步骤
                    sl1.Add("FIsQuali", drTemp["FIsQuali"]);
                    sl1.Add("FIsPrint", drTemp["FIsPrint"]);
                    sl1.Add("FManageDeptId", drTemp["FManageDeptId"]);
                    sl1.Add("FDefineDay", drTemp["FDefineDay"]);
                    sl1.Add("FTypeId", drTemp["FTypeId"]);
                    sl1.Add("FOrder", drTemp["FOrder"]);
                    sl1.Add("FLevel", drTemp["FLevel"]);
                    sl1.Add("FIsDeleted", 0);
                    sl1.Add("FReportCount", fReportCount);//审核步骤
                    arrEn.Add(EntityTypeEnum.EaProcessRecord);
                    arrSl.Add(sl1);
                    arrKey.Add("FID");
                    arrSo.Add(SaveOptionEnum.Insert);//新插入一条 

                    string fnewId = rc.GetSignValue("select fid from cf_App_ProcessRecord where fprocessInstanceId='" + fProcessInstanceId
                        + "' and ftypeId='" + t_ftype.Value + "' and froleId in(" + Session["DFRoleId"] + ") order by FReportCount desc");
                    if (!string.IsNullOrEmpty(fnewId))
                    {
                        sl2.Clear();
                        sl2.Add("FID", fnewId);
                        sl2.Add("FMeasure", 3);//标识为打回到上一步状态
                        //sl2.Add("FAppPerson", t_Auditer.Text);
                        sl2.Add("FIdea", "打回");
                        arrEn.Add(EntityTypeEnum.EaProcessRecord);
                        arrSl.Add(sl2);
                        arrKey.Add("FID");
                        arrSo.Add(SaveOptionEnum.Update);
                    }

                }
                sl.Clear();
                sl.Add("FID", fProcessInstanceId);
                if (!string.IsNullOrEmpty(fSubFlowId))
                    sl.Add("FSubFlowId", fSubFlowId);
                if (!string.IsNullOrEmpty(fRoleId))
                    sl.Add("FRoleId", fRoleId);
                arrEn.Add(EntityTypeEnum.EaProcessInstance);
                arrSl.Add(sl);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
            }
            StringBuilder sb = new StringBuilder();
            iCount = arrSo.Count;
            EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
            SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
            string[] fkeys = new string[iCount];
            SortedList[] sls = new SortedList[iCount];

            for (int t = 0; t < iCount; t++)
            {
                ens[t] = (EntityTypeEnum)arrEn[t];
                sos[t] = (SaveOptionEnum)arrSo[t];
                fkeys[t] = (string)arrKey[t];

                sls[t] = new SortedList();
                sls[t] = (SortedList)arrSl[t];
            }
            rc.SaveEBaseM(ens, sls, fkeys, sos);
        }
    }
    #endregion

    #region 不予受理
    //不予受理
    private void BatchEndApp()
    {
        ProjectDB db = new ProjectDB();
        string backIdea = t_FAppIdea.Text;
        backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;

        StringBuilder sb = new StringBuilder();
        string erIds = t_ProcessRecordID.Value;
        string s = t_FProcessInstanceID.Value;
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(erIds))
            return;
        string[] strs = s.Trim().Split(',');
        string[] strs1 = erIds.Trim().Split(',');
        if (strs.Length <= 0)
        {
            return;
        }

        int iCount = strs.Length;

        ArrayList arrSl = new ArrayList();
        ArrayList arrEn = new ArrayList();
        ArrayList arrKey = new ArrayList();
        ArrayList arrSo = new ArrayList();
        ArrayList array = new ArrayList();
        ArrayList arrLink = new ArrayList();

        for (int i = 0; i < iCount; i++)
        {
            array.Add(strs[i]);

            SortedList sl = new SortedList();
            sl.Add("FID", strs[i]);
            sl.Add("FSeeState", 3);
            sl.Add("FSeeTime", DateTime.Now);

            arrSl.Add(sl);
            arrEn.Add(EntityTypeEnum.EaProcessInstance);
            arrKey.Add("FID");
            arrSo.Add(SaveOptionEnum.Update);
            if (strs1.Length > i)
            {
                sl = new SortedList();
                sl.Add("FID", strs1[i]);
                sl.Add("FUserId", Session["DFUserId"]);

                arrSl.Add(sl);
                arrEn.Add(EntityTypeEnum.EaProcessRecord);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
            }
            sb.Remove(0, sb.Length);
            sb.Append(" select flinkid from CF_App_ProcessInstance  ");
            sb.Append(" where fid='" + strs[i] + "'");

            string fLinkId = rc.GetSignValue(sb.ToString());
            if (!arrLink.Contains(fLinkId))
            {
                arrLink.Add(fLinkId);
            }
            //【2015-01-16】 FMeasure=5 已审核,FResult=3 未通过
            rc.PExcute("update CF_App_ProcessRecord set FAppTime=getdate(),FMeasure=5,FResult=3 where fprocessInstanceId='" + strs[i] + "' and FTypeId=1");//修改退件的时间

        }
        iCount = arrLink.Count;
        for (int j = 0; j < iCount; j++)
        {
            sb.Remove(0, sb.Length);
            sb.Append(" select fid from CF_App_AcceptBook ");
            sb.Append(" where flinkid='" + arrLink[j].ToString() + "'");

            string fId = rc.GetSignValue(sb.ToString());
            if (fId != null && fId != "")
            {
                SortedList sl = new SortedList();
                sl.Add("FID", fId);
                sl.Add("FState", 13);
                arrSl.Add(sl);
                arrEn.Add(EntityTypeEnum.EaAcceptBook);
                arrKey.Add("FID");
                arrSo.Add(SaveOptionEnum.Update);
            }

            string FLinkId = arrLink[j].ToString();
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

        iCount = arrSo.Count;

        SortedList[] sls = new SortedList[iCount];
        EntityTypeEnum[] ens = new EntityTypeEnum[iCount];
        string[] fkeys = new string[iCount];
        SaveOptionEnum[] sos = new SaveOptionEnum[iCount];
        for (int k = 0; k < iCount; k++)
        {
            sls[k] = new SortedList();
            sls[k] = (SortedList)arrSl[k];
            ens[k] = (EntityTypeEnum)arrEn[k];
            fkeys[k] = (string)arrKey[k];
            sos[k] = (SaveOptionEnum)arrSo[k];
        }
        rc.SaveEBaseM(ens, sls, fkeys, sos);
        ra.BatchEnd(array, backIdea, "3");
    }
    #endregion


    private string YWBM
    {
        get
        {
            return Request.QueryString["YWBM"];
        }
    }
    private string TypeId
    {
        get
        {
            return Request.QueryString["typeId"];
        }
    }
    private string fSubFlowId
    {
        get
        {
            return Request.QueryString["fSubFlowId"];
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (TypeId == "1") //接件环节才可保存附件信息
        {
            SaveFileMessage();
        }
        saveIdear();
        pageTool tool = new pageTool(this.Page);
        tool.showMessage("保存成功");
        if (TypeId == "1")
            ShowFile();
    }
    protected void btnAcccpt_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (TypeId == "1") //接件环节才可保存附件信息
        {
            SaveFileMessage();
        }
        ReportProcess();
        if (TypeId == "20")
        {
            
        }
        tool.showMessageAndRunFunction(btnAcccpt.Text + "成功", "window.returnValue='1';window.close();");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        BatchApp();
        pageTool tool = new pageTool(this.Page);
        tool.showMessageAndRunFunction("打回成功", "window.returnValue='1';window.close();");
    }
    protected void btnNoAccept_Click(object sender, EventArgs e)
    {
        saveIdear();
        BatchEndApp();
        pageTool tool = new pageTool(this.Page);
        tool.showMessageAndRunFunction("操作成功", "window.returnValue='1';window.close();");
    }
}