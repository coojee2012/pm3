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
using Approve.EntitySys;
using Approve.EntityCenter;
using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.RuleApp;
using Approve.EntityQuali;
using Approve.Common;
using ProjectData;
using System.Linq;
using ProjectBLL;

public partial class Government_AppPage_PrjFileAppPager1 : govBasePage
{

    RCenter rc = new RCenter();
    RApp ra = new RApp();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.ExecuteScript("showdiv();");
        if (!Page.IsPostBack)
        {
            base.Page_Load(sender, e);
            string LinkId = Request["lid"].ToString();
            string btnId = Request.QueryString["btnId"];//保证选项卡的选中性
            if (!string.IsNullOrEmpty(btnId))
            {
                HbtnId.Value = btnId;
            }
            this.ViewState["lid"] = LinkId;
            this.ViewState["fTypeId"] = Request.QueryString["ftypeid"];

            //显示上报信息
            ShowReportInfo();

            //显示审批信息
            ShowAppInfo();

            //确定按牛状态
            ShowBtn();

            //显示连接信息
            ShowLinkInfo();

            //显示审批意见
            ShowAppIdea();

            //显示打回信息选择项
            ShowBackIdea();
            #region 提交确认
            btnReport.Attributes.Add("onclick", "if(confirm('确认要提交么？')){this.disabled=true;this.value='数据提交中...';}else{return false;}");
            btnFinallyApp.Attributes.Add("onclick", "if(confirm('确认要办结么？')){this.disabled=true;this.value='数据提交中...';}else{return false;}");
            #endregion


            //显示流程图
            showLCT();

            //显示时序图
            showSXT();

            //显示打回步骤选择项
            ShowBackStep();

            ShowFileList();

            string sUrl = "showAddWindow('UpLoadPrjPic.aspx?FLinkId=" + ViewState["lid"] + "',450,240);";
            aFile.Attributes.Add("onclick", sUrl);
        }

    }




    #region 流程图和时序图

    //显示流程图
    private void showLCT()
    {
        string FLinkId = Request.QueryString["lid"];
        StringBuilder sb = new StringBuilder();
        sb.Append("select FID,FProcessId,FSubFlowId, len(FManageDeptId)/2 FLevel from CF_App_ProcessInstance where FLinkId='" + FLinkId + "'");
        DataTable dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            string FProcessInstanceID = dt.Rows[0]["FID"].ToString();
            string FProcessId = dt.Rows[0]["FProcessId"].ToString();
            string FSubFlowId = dt.Rows[0]["FSubFlowId"].ToString();
            int FLevel = EConvert.ToInt(dt.Rows[0]["FLevel"]);
            sb.Remove(0, sb.Length);
            sb.Append("select top 1 s.FOrder from CF_App_SubFlow s,CF_App_ProcessRecord r ");
            sb.Append("where s.FID=r.FSubFlowId and r.FProcessInstanceID='" + FProcessInstanceID + "' ");
            sb.Append("order by r.FAppTime desc ");
            string FOrder = rc.GetSignValue(sb.ToString());


            sb.Remove(0, sb.Length);
            sb.Append("select s.FName,s.FOrder,s.FTypeId,case s.FID when '" + FSubFlowId + "' then 1 else 0 end as IsNow,");
            sb.Append("r.FID FReFID,r.FReportTime,r.FAppTime,r.FAppPerson,r.FMeasure ");
            sb.Append("From CF_App_SubFlow s ");
            sb.Append("left join CF_App_ProcessRecord r on s.FID=r.FSubFlowId  and r.FProcessInstanceID='" + FProcessInstanceID + "'");
            sb.Append("Where s.FProcessId='" + FProcessId + "' and s.FLevel<= " + FLevel);
            sb.Append(" and s.forder <=(select  top 1  forder from  CF_App_SubFlow where FIsEnd=1 and FProcessId='" + FProcessId
                + "' and forder>=" + EConvert.ToInt(FOrder) + " order by FIsEnd desc,forder,FCreateTime Desc) ");
            sb.Append("Order By s.forder,s.FCreateTime Desc ");

            repSubFlow.DataSource = rc.GetTable(sb.ToString());
            repSubFlow.DataBind();

            rep_SXT.DataSource = rc.GetTable(sb.ToString());
            rep_SXT.DataBind();
        }
    }




    //显示时序图
    private void showSXT()
    {
        string FLinkId = Request.QueryString["lid"];
        EqList eq = (EqList)rc.GetEBase(EntityTypeEnum.EqList, "", "fid='" + FLinkId + "'");
        if (eq == null)
        {
            return;
        }
        lit_step_EntName.Text = eq.FBaseName;
        lit_step_EntReportDate.Text = EConvert.ToString(eq.FReportDate);
        lit_step_EntReportDate1.Text = EConvert.ToString(eq.FReportDate);
    }
    protected void rep_SXT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FName"));
            string FOrder = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FOrder"));
            string FReFID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FReFID"));
            string FReportTime = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FReportTime"));
            string FAppTime = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppTime"));
            string FAppPerson = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppPerson"));
            string FMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            string FTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FTypeId"));


            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='step_0" + FOrder + "' class='" + (FTypeId == "1" ? "step_jj" : "step_sp") + "'>");
            sb.Append("<div class='step_name'>" + FName + "</div>");
            sb.Append("<div class='step_xx'></div>");

            if (!string.IsNullOrEmpty(FReFID))
            {
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td class='step_left'>&nbsp;</td>");
                sb.Append("<td class='step_word'>");
                sb.Append("名 &nbsp;称：<b>" + FName + "</b><br />");
                sb.Append("执行者：" + FAppPerson + "<br />");
                sb.Append("开始时间：" + FReportTime + "<br />");
                sb.Append("结束时间：" + FAppTime + "<br />");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");


                sb.Append("<div class='step_jt'>");
                sb.Append("<span>&nbsp;</span>");
                if (FMeasure == "5")
                {
                    sb.Append("<div>" + FAppTime + "</div>");
                    sb.Append("<samp>&nbsp;</samp>");
                }
                sb.Append("</div>");
            }

            sb.Append("</div>");

            Literal SXT_Step = (Literal)e.Item.FindControl("SXT_Step");
            SXT_Step.Text = sb.ToString();
        }
    }

    #endregion

    /// <summary>
    /// 显示打回信息选择项
    /// </summary>
    void ShowBackIdea()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,fcontent from CF_App_BackIdea where ftype=0 order by forder ");
        DataTable dt = rc.GetTable(sb.ToString());
        ckListIdea.DataSource = dt;
        ckListIdea.DataTextField = "FContent";
        ckListIdea.DataValueField = "FId";
        ckListIdea.DataBind();
    }
    void ShowBackStep()
    {
        string fRoleId = EConvert.ToString(Session["DFRoleId"]);
        StringBuilder sb = new StringBuilder();
        sb.Append("select max(er.FOrder) from CF_App_ProcessInstance ep,CF_App_ProcessRecord er ");
        sb.Append("Where ep.FlinkId='" + ViewState["lid"] + "' ");
        sb.Append("and ep.fid=er.FProcessInstanceID and ep.fsubflowid=er.fsubflowid ");
        sb.Append("and ep.fstate=1 and ep.froleid in(" + fRoleId + ") ");
        //所属审核阶段
        if (!string.IsNullOrEmpty(EConvert.ToString(ViewState["fTypeId"])))
            sb.Append(" and er.ftypeid=" + ViewState["fTypeId"].ToString() + " ");
        sb.Append("group by ep.FLinkId");
        int fOrder = EConvert.ToInt(rc.GetSignValue(sb.ToString()));//查询当前步骤的FOrder

        sb.Remove(0, sb.Length);
        sb.Append(" select t1.fid,t1.FRoleDesc from (select r.fid,r.FRoleDesc,r.FReportCount ");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(" and p.fLinkId='" + ViewState["lid"] + "' ");
        sb.Append(" and (r.fMeasure<>0 and r.fMeasure<>3 and r.FMeasure<>4) and r.ftypeid<>1 ");
        //非接件、非上级打回、非未保存意见
        sb.Append(" and r.FOrder<'" + fOrder + "' and r.fRoleId not in (" + fRoleId + "))t1 inner join ");
        sb.Append(" (select max(r.FReportCount)FReportCount ");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(" and p.fLinkId='" + ViewState["lid"] + "' ");
        sb.Append(" and (r.fMeasure<>0 and r.fMeasure<>3 and r.FMeasure<>4) and r.ftypeid<>1 ");
        //非接件、非上级打回、非未保存意见
        sb.Append(" and r.FOrder<'" + fOrder + "' and r.fRoleId not in (" + fRoleId + ") ");
        sb.Append(" group by r.fRoleId,r.FTypeId)t2 on t1.FReportCount=t2.FReportCount ");
        sb.Append(" order by t1.FReportCount desc");
        DataTable dt = rc.GetTable(sb.ToString());
        ddlFStep.DataSource = dt;
        ddlFStep.DataTextField = "FRoleDesc";
        ddlFStep.DataValueField = "FId";
        ddlFStep.DataBind();
        ddlFStep.Items.Insert(0, new ListItem("企业端", "-1"));
        ddlFStep.Items.Insert(0, new ListItem("请选择", ""));
    }
    private void ShowBtn()
    {
        string fLinkId = ViewState["lid"].ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(" select s.fisend,p.froleid,p.fbeginroleid,p.FCurStepID from CF_App_ProcessInstance p,CF_App_SubFlow s ");
        sb.Append(" where p.FSubFlowId=s.fid and p.flinkid='" + fLinkId + "'");
        DataTable dt = rc.GetTable(sb.ToString());

        int iCount = dt.Rows.Count;

        bool isEnd = true;

        bool isBegin = true;
        //如果有一项不能终审，则不能终审
        for (int i = 0; i < iCount; i++)
        {
            if (dt.Rows[i]["fisend"].ToString() != "1")
            {
                isEnd = false;
            }
            else
            {
                //当前管理部门等级
                string fCurStepLevel = rc.GetSignValue(EntityTypeEnum.EsManageDept, "FLevel", "FNumber='" + dt.Rows[i]["FCurStepID"].ToString() + "'");
                //如果终审的流程中当前管理部门等级有比登录角色的管理部门等级高的不能，登录角色不能终审
                if (EConvert.ToInt(Session["DFLevel"]) > EConvert.ToInt(fCurStepLevel))
                {
                    isEnd = false;
                }
            }

            if (dt.Rows[i]["froleid"].ToString() != dt.Rows[i]["fbeginroleid"].ToString())
            {
                isBegin = false;
            }
        }
        if (isEnd)
        {
            this.btnFinallyApp.Visible = true;
            this.btnReport.Visible = false;
        }
        else
        {
            this.btnFinallyApp.Visible = false;
        }
        if (Session["DFLevel"] == "1")
        {
            this.btnReport.Text = "上报建设部";
        }

        if (t_FResult.SelectedValue == "3")
        {
            tab_BJ.Visible = false;
        }
        else
        {
            tab_BJ.Visible = true;
        }
    }

    //显示上报数据
    private void ShowReportInfo()
    {
        string fLinkId = this.ViewState["lid"].ToString();
        string fBId = rc.GetSignValue("select FBaseInfoId from cf_App_ProcessInstance where flinkId='" + fLinkId + "'");
        EqList eq = (EqList)rc.GetEBase(EntityTypeEnum.EqList, "", "fid='" + fLinkId + "'");
        if (eq == null)
            return;
        ViewState["eqftype"] = eq.FManageTypeId;
        string fentName = rc.GetSignValue(EntityTypeEnum.EbBaseInfo, "FName", "FId='" + fBId + "'");
        this.liter_FBaseName.Text = fentName;
        btnSeeSign.Visible = eq.FManageTypeId == "561";//如果是建筑节能验收备案 
        if (eq.FManageTypeId == "294")
        {
            liJsEnt.Text = "设计单位";
        }
        else
        { }
        RBase rb = new RBase();
        ProjectDB db = new ProjectDB();

        if (eq.FManageTypeId == "290")//勘察文件审查备案
        {
            //技术性审查结果
            var result = (from l1 in db.CF_App_List
                          join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                          join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 290) on l2.FId equals id.FLinkId
                          where l1.FId == fLinkId && l2.FManageTypeId == 28803
                          orderby l2.FCount descending
                          select id.FResultInt).FirstOrDefault();
            if (EConvert.ToInt(result) == 6)
            {
                liTechnical.Text = "技术性审查合格";
            }
            else
            {
                liTechnical.Text = "技术性审查不合格";
                Response.Redirect("PrjBASQAppPager1.aspx" + Request.Url.Query);
                return;
            }
            hfTechnical.Value = result.ToString();
        }
        else if (eq.FManageTypeId == "305")//施工图设计文件备案
        {
            //技术性审查结果
            var result = (from l1 in db.CF_App_List
                          join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                          join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 305) on l2.FId equals id.FLinkId
                          where l1.FId == fLinkId && l2.FManageTypeId == 30103
                          orderby l2.FCount descending
                          select id.FResultInt).FirstOrDefault();
            if (EConvert.ToInt(result) == 6)
            {
                liTechnical.Text = "技术性审查合格";
            }
            else
            {
                liTechnical.Text = "技术性审查不合格";
                Response.Redirect("PrjBASQAppPager1.aspx" + Request.Url.Query);
                return;
            }
            hfTechnical.Value = result.ToString();
        }
        tab_BJ.Visible = true;
        //备案编号
        string FPrjItemNo = db.CF_Prj_Certi.Where(t => t.FAppId == fLinkId).Select(t => t.FCertiNo).FirstOrDefault();
        string PrjFID = ""; //rq.GetSignValue("select FID from CQ_PrjItem_BaseInfo where FAppId='" + fLinkId + "'");
        txtFCertiNo.Text = FPrjItemNo;

        this.liter_FManageType.Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + eq.FManageTypeId + "'");
        this.liter_FReportDate.Text = EConvert.ToShortDateString(eq.FReportDate);

        this.ViewState["FBaseinfoId"] = fBId;
        string fsid = rc.getEntSystemId(fBId);
        this.ViewState["fsid"] = fsid;

        StringBuilder sb = new StringBuilder();

        sb.Append(" select tt.*  ");

        sb.Append(" from (");
        sb.Append(" select ep.Fid,ep.FSystemId,ep.FBaseInfoId,ep.FLinkId,er.FReporttime,ep.FManageTypeId,ep.FLeadId,");
        sb.Append(" er.FIsQuali,er.FIsPrint,er.fresult,er.fid FRId,er.FReportCount,ep.FEmpName,ep.FEntName,ep.FLeadName from CF_App_ProcessInstance ep,");
        sb.Append(" CF_App_ProcessRecord er Where ep.FlinkId='" + fLinkId + "' ");
        sb.Append(" and ep.fid=er.FProcessInstanceID and ep.fsubflowid=er.fsubflowid ");
        sb.Append(" and ep.fstate<>6 ");
        sb.Append(" and ep.froleid in(" + Session["DFRoleId"].ToString() + ") ");
        //所属审批阶段
        if (ViewState["fTypeId"] != null && ViewState["fTypeId"].ToString() != "")
            sb.Append(" and er.ftypeid=" + ViewState["fTypeId"].ToString());
        sb.Append(")tt  ");

        sb.Append(" order by tt.FReporttime desc,FBaseInfoId");
        DataTable dt = rc.GetTable(sb.ToString());
        this.ReportInfo_List.DataSource = dt;
        this.ReportInfo_List.DataBind();
        //显示工程信息
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            t_FPrjItemName.Text = dr["FEmpName"].ToString();
            liter_FBaseName0.Text = EConvert.ToString(dr["FLeadName"]);

            string sysname = db.CF_Sys_SystemName.Where(t => t.FNumber == EConvert.ToInt(dr["FSystemId"])).Select(t => t.FName).FirstOrDefault();

            liEntType.Text = sysname;

            string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + dr["FBaseInfoId"] + "'");

            string sUrl = "";
            sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber='" + EConvert.ToInt(dr["FSystemId"]) + "'"); ;

            sUrl += "?fbid=" + dr["FBaseInfoId"] + "&faid=" + dr["FLinkId"] + "&frid=" + frid + "&fmid=" + dr["FManageTypeId"] + "&fly=1";
            liter_FBaseName.Text = "<a href='" + sUrl + "' class='link5' target='_blank'>" + liter_FBaseName.Text + "</a>";


            if (eq.FManageTypeId == "294")
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=155 ");
            }
            else
            {
                sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

            }

            sUrl += "?fbid=" + dr["FLeadId"] + "&faid=" + dr["FLinkId"] + "&frid=" + frid + "&fmid=" + dr["FManageTypeId"] + "&fly=1";
            liter_FBaseName0.Text = "<a href='" + sUrl + "' class='link5' target='_blank'>" + liter_FBaseName0.Text + "</a>";
        }
    }



    protected void ReportInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string sUrl = "";
            string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            StringBuilder sb = new StringBuilder();
            string fIsNewAdd = e.Item.Cells[e.Item.Cells.Count - 5].Text;
            if (fIsNewAdd == "3")
            {
                e.Item.Cells[e.Item.Cells.Count - 8].Text = "已就位定级";
                btnBackEnt.ToolTip = "证书已就位，无法再打回！";
                e.Item.Cells[e.Item.Cells.Count - 7].Text = "已就位证书";
                btnBackEnt.Enabled = false;
                btnDHQY.Visible = false;
                ViewState["FIsNewAdd"] = 3;//已经就位
            }
            string fBaseInfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseInfoId"));
            if (fIsNewAdd == "1")// && dt.Rows[0]["fisend"].ToString() == "1")
            {
                sUrl = "showApproveWindow1('../EntQualiCerti/YLJwQuali.aspx?fpid=" + FId;
                sUrl += "&fsid=" + EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId")) + "&fbid=" + fBaseInfoId + "','980','700')";
                e.Item.Cells[e.Item.Cells.Count - 8].Text = "<a href='#' class='link5' onclick=" + sUrl + " style=\"color:red;\">" + "就位证书" + "</a>";
                tab_BJ.Visible = true;
            }
            //工程业主

            if (!string.IsNullOrEmpty(fBaseInfoId))
            {
                ProjectDB db1 = new ProjectDB();
                string fHostName = db1.CF_Ent_BaseInfo.Where(t => t.FId == fBaseInfoId).Select(t => t.FName).FirstOrDefault();
                e.Item.Cells[2].Text = fHostName;
            }
            //得到审批意见
            e.Item.Cells[7].Text = ra.getAppResult(FId).Replace("href", "").Replace("title", "");
            sUrl = "javascript:showApproveWindow1('../NewAppMain/PreAppCheck.aspx?pid=" + FId + "',800,600)";
            //e.Item.Cells[e.Item.Cells.Count - 7].Text = "<a class='link5' href=\"" + sUrl + "\"  >" + e.Item.Cells[e.Item.Cells.Count - 7].Text + "</a>";
            EaSubFlow epSub = ra.GetPreviousFlow(FId);
            if (epSub != null)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select FResult,FPersonnel,FPerformance from CF_App_ProcessRecord where FProcessInstanceID='" + FId + "'");
                sb.Append(" and FSubFlowId='" + epSub.FId + "'");
                DataTable dt = rc.GetTable(sb.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    DropDownList dp = (DropDownList)e.Item.Cells[6].Controls[1];
                    if (dp != null)
                    {
                        dp.SelectedIndex = dp.Items.IndexOf(dp.Items.FindByValue(dt.Rows[0]["FResult"].ToString().Trim()));
                    }
                }
            }
        }
    }
    //显示审批人员信息
    private void ShowAppInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + ViewState["lid"].ToString() + "' and FMeasure=1 ");
        sb.Append(" and froleid in(" + Session["DFRoleId"].ToString() + ")");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
            t_FResult.SelectedIndex = t_FResult.Items.IndexOf(t_FResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString().Trim()));
            for (int i = 0; i < ReportInfo_List.Items.Count; i++)
            {
                string fId = ReportInfo_List.Items[i].Cells[ReportInfo_List.Columns.Count - 6].Text;

                DataRow[] row = dt.Select(" fid='" + fId + "'");

                if (row != null && row.Length > 0)
                {
                    DropDownList dResult = (DropDownList)ReportInfo_List.Items[i].Cells[6].Controls[1];
                    dResult.ClearSelection();
                    dResult.SelectedIndex = dResult.Items.IndexOf(dResult.Items.FindByValue(row[0]["FResult"].ToString()));
                }
            }

            this.btnReport.Enabled = true;
            this.btnFinallyApp.Enabled = true;
        }

        else
        {
            sb.Remove(0, sb.Length);
            sb.Append("FId='" + Session["DFUserId"].ToString() + "'");
            EsUser eu = (EsUser)rc.GetEBase(EntityTypeEnum.EsUser, "FLinkMan,FFunction,FDepartmentID,FCompany", sb.ToString());
            if (eu != null)
            {
                this.t_FAppPerson.Text = eu.FLinkMan;
                this.t_FAppPersonJob.Text = eu.FFunction;
                this.t_FAppPersonUnit.Text = RBase.GetDepartmentName(eu.FDepartmentID) + RBase.GetDepartmentName(eu.FCompany);
                this.t_FAppDate.Text = DateTime.Now.ToShortDateString();
            }

            this.btnReport.Enabled = false;
            this.btnFinallyApp.Enabled = false;
        }
    }

    public int GetMesure()
    {
        int temp = 0;

        return temp;
    }

    //显示连接信息
    public void ShowLinkInfo()
    {
        if (this.ViewState["FBaseinfoId"] == null)
        {
            return;
        }

        if (this.ViewState["lid"] == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("FLinkId='" + this.ViewState["lid"].ToString() + "'");
        EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FManageTypeId,Fsystemid", sb.ToString());
        if (ea == null)
        {
            return;
        }
        string fbid = this.ViewState["FBaseinfoId"].ToString();
        string faid = this.ViewState["lid"].ToString();
        string fmid = ea.FManageTypeId;
        string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        string fsid = ea.FSystemId;
        string fQurl = rc.getMTypeQurl(ea.FManageTypeId); ;

        string fUrl = fQurl;
        HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");
        fUrl = fQurl;
        HSeePrintInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fBaseId=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&isPrint=1')");

    }

    private void ShowAppIdea()
    {
        string FAppId = rc.GetSignValue(EntityTypeEnum.EaProcessInstance, "fid", "flinkid='" + this.ViewState["lid"] + "'");
        if (FAppId != null && FAppId != "")
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" select r.fid,fentname,");
            sb.Append(" p.flistid,");
            sb.Append(" p.ftypeid,");
            sb.Append(" p.flevelid,");
            sb.Append(" p.FManageTypeId,");
            sb.Append(" r.fresult,R.FAppTime ,r.FReportTime,r.FIdea,r.FRoleDesc,");
            sb.Append(" r.FCompany,r.FFunction,r.FAppPerson ");
            sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r where ");
            sb.Append(" p.fid=r.FProcessInstanceID ");
            sb.Append(" and p.fid='" + FAppId + "' ");
            sb.Append(" and r.fMeasure<>0  ");
            //sb.Append(" and ");
            //sb.Append(" (");
            //sb.Append(" ((select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) >1)");
            //sb.Append(" or");
            //sb.Append(" ((select top 1 flevel from cf_sys_managedept where fnumber = r.FManageDeptId) <=1)");
            //sb.Append(" )");
            sb.Append(" order by r.fReportCount,r.FOrder ");

            this.AppInfo_List.DataSource = rc.GetTable(sb.ToString());
            this.AppInfo_List.DataBind();
        }
    }

    #region 上报流程
    private void ReportProcess(string FBaseInfoId)
    {
        SaveIdea();
        string FLinkId = this.ViewState["lid"].ToString();
        int iCount = ReportInfo_List.Items.Count;
        SortedList[] sl = new SortedList[iCount];
        for (int i = 0; i < 1; i++)
        {
            string AFId = ReportInfo_List.Items[i].Cells[ReportInfo_List.Items[i].Cells.Count - 1].Text;
            string RFId = ReportInfo_List.Items[i].Cells[ReportInfo_List.Items[i].Cells.Count - 6].Text;
            EaProcessRecord Er = (EaProcessRecord)rc.GetEBase(EntityTypeEnum.EaProcessRecord, "FResult", "FID='" + RFId + "'");

            sl[i] = new SortedList();
            sl[i].Add("FID", RFId);
            sl[i].Add("FProcessInstanceID", AFId);

            sl[i].Add("FLinkId", FLinkId);

            sl[i].Add("FMeasure", 5);//dResult.SelectedValue); //上报
            sl[i].Add("FResult", Er.FResult);
            sl[i].Add("FUserId", Session["DFUserId"].ToString());
            EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + AFId + "'");
            DateTime fReportTime = epi.FReportDate;
            DateTime nowTime = DateTime.Now;
            TimeSpan spanTime = nowTime - fReportTime;
            sl[i].Add("FWaiteTime", spanTime.Days);

        }
        ra.GovReportProcessKCSJ(sl, FBaseInfoId);
    }
    #endregion

    #region 打回到企业
    private void BackToEnt()
    {
        if (Request["pid"] == null || Request["pid"] == "")
        {
            return;
        }
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + Request["pid"] + "'");
        if (epi == null)
        {
            return;
        }

        EaProcessRecord eap = ra.GetProcessRecord(Request["pid"], epi.FSubFlowId);
        if (eap == null)
        {
            return;
        }
        eap.FAppPerson = this.t_FAppPerson.Text;
        eap.FAppTime = EConvert.ToDateTime(this.t_FAppDate.Text);
        eap.FCompany = this.t_FAppPersonUnit.Text;
        eap.FFunction = this.t_FAppPersonJob.Text;
        eap.FIdea = this.t_FAppIdea.Text;
        eap.FLevel = EConvert.ToInt(Session["FLevel"].ToString());
        eap.FLinkId = epi.FLinkId;
        eap.FMeasure = GetMesure();
        eap.FOrder = 5000;
        eap.FUserId = Session["FUserId"].ToString();
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        eap.FWaiteTime = spanTime.Days;
        ra.BackProcessToEnt(Request["pid"], eap);
    }
    #endregion

    #region 打回到下级
    private void BackToSubGov()
    {
        if (Request["pid"] == null || Request["pid"] == "")
        {
            return;
        }
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + Request["pid"] + "'");
        if (epi == null)
        {
            return;
        }
        EaProcessRecord eap = ra.GetProcessRecord(Request["pid"], epi.FSubFlowId);
        if (eap == null)
        {
            return;
        }
        eap.FAppPerson = this.t_FAppPerson.Text;
        eap.FAppTime = EConvert.ToDateTime(this.t_FAppDate.Text);
        eap.FCompany = this.t_FAppPersonUnit.Text;
        eap.FFunction = this.t_FAppPersonJob.Text;
        eap.FIdea = this.t_FAppIdea.Text;
        eap.FLevel = EConvert.ToInt(Session["FLevel"].ToString());
        eap.FLinkId = epi.FLinkId;
        eap.FMeasure = GetMesure();
        eap.FOrder = 5000;
        eap.FUserId = Session["FUserId"].ToString();
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        eap.FWaiteTime = spanTime.Days;
        ra.BackProcessToSubGov(Request["pid"], eap);
    }
    #endregion

    #region 结束一个流程
    private void EndProcess()
    {
        if (Request["pid"] == null || Request["pid"] == "")
        {
            return;
        }
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "", "FId='" + Request["pid"] + "'");
        if (epi == null)
        {
            return;
        }
        EaProcessRecord eap = ra.GetProcessRecord(Request["pid"], epi.FSubFlowId);
        if (eap == null)
        {
            return;
        }
        eap.FAppPerson = this.t_FAppPerson.Text;
        eap.FAppTime = EConvert.ToDateTime(this.t_FAppDate.Text);
        eap.FCompany = this.t_FAppPersonUnit.Text;
        eap.FFunction = this.t_FAppPersonJob.Text;
        eap.FIdea = this.t_FAppIdea.Text;
        eap.FLevel = EConvert.ToInt(Session["FLevel"].ToString());
        eap.FLinkId = epi.FLinkId;
        eap.FMeasure = GetMesure();
        eap.FOrder = 5000;
        eap.FUserId = Session["FUserId"].ToString();
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        eap.FWaiteTime = spanTime.Days;
        ra.EndProcess(Request["pid"], eap);
    }
    #endregion

    private bool isEnd()
    {
        //bool isPass = true;
        StringBuilder sb = new StringBuilder();
        sb.Append(" select ep.fid from CF_App_ProcessInstance ep,CF_App_ProcessRecord er ");
        sb.Append(" where ep.flinkid='" + this.ViewState["lid"].ToString() + "' ");
        sb.Append(" and ep.fid=er.FProcessInstanceID and ep.fsubflowid=er.fsubflowid ");
        sb.Append(" and ep.froleid in(" + Session["DFRoleId"].ToString() + ") ");
        sb.Append(" and er.fresult='1' and er.fisQuali=1 ");
        DataTable dt = rc.GetTable(sb.ToString());
        int iCount = dt.Rows.Count;
        if (iCount > 0)

            return false;

        return true;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (!this.isEnd())
        {
            this.Response.Write("<script>alert('还没有保存备案编号，不能提交!');</script>");
            return;
        }
        ReportProcess(ViewState["FBaseinfoId"].ToString());
        //setCertiState();
        retList("1");
    }

    protected void btnBackSubDept_Click(object sender, EventArgs e)
    {
        BackToSubGov();
        retList("1");
    }



    /// <summary>
    /// 打回企业
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBackEnt_Click1(object sender, EventArgs e)
    {
        string backIdea = txtFBackIdea.Text;
        backIdea = backIdea.Length > 200 ? backIdea.Substring(0, 200) : backIdea;
        string FLinkId = this.ViewState["lid"].ToString();
        string fStepValue = ddlFStep.SelectedValue;
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessInstance, "FID", "flinkid='" + FLinkId + "' AND fstate=1");
        if (fStepValue == "-1")//打回企业
        {
            CF_App_ProcessInstance ProcessInstance = db.CF_App_ProcessInstance.Where(t => t.FLinkId == FLinkId).FirstOrDefault();

            if (ra.BatchBack(dt, FLinkId, backIdea))
            {
                if (ProcessInstance != null)
                {
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
                    idea.FReportCount = db.CF_App_List.Where(t => t.FId == FLinkId).Select(t => t.FCount).FirstOrDefault();
                    idea.FResult = "打回";
                    idea.FResultInt = 2;
                    idea.FContent = txtFBackIdea.Text;
                    idea.FAppTime = DateTime.Now;
                    idea.FSystemId = ProcessInstance.FSystemId;
                    idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                    idea.FType = ProcessInstance.FManageTypeId;
                    idea.FTime = DateTime.Now;

                }
            }
        }
        else//打回到上一级
        {
            BackToPre(fStepValue, backIdea, dt);
        }

        this.RegisterStartupScript("jj", "<script>window.returnValue=1;window.close();</script>");
    }
    /// <summary>
    /// 打回到上级
    /// </summary>
    protected void BackToPre(string fstepId, string backIdea, DataTable dt)
    {
        string FLinkId = this.ViewState["lid"].ToString();
        int fReportCount = EConvert.ToInt(rc.GetSignValue("select isnull(max(FReportCount),0) from cf_App_ProcessRecord where flinkId='" + FLinkId + "'"));//最大步骤
        fReportCount++;
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
                DataTable dtTemp = rc.GetTable("select * from cf_App_ProcessRecord where fid='" + fstepId + "'");
                string fSubFlowId = string.Empty;
                string fRoleId = string.Empty;
                if (dtTemp != null && dtTemp.Rows.Count > 0)//如果有这一步
                {
                    sl1.Clear();
                    DataRow drTemp = dtTemp.Rows[0];
                    sl1.Add("FID", System.Guid.NewGuid().ToString());
                    sl1.Add("FProcessInstanceID", fProcessInstanceId);
                    sl1.Add("FLinkId", FLinkId);
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

                    string fnewId = rc.GetSignValue("select fid from cf_App_ProcessRecord where fprocessInstanceId='" + fProcessInstanceId + "' and ftypeId='" + ViewState["fTypeId"] + "' and froleId in(" + Session["DFRoleId"] + ") order by FReportCount desc");
                    if (!string.IsNullOrEmpty(fnewId))
                    {
                        sl2.Clear();
                        sl2.Add("FID", fnewId);
                        sl2.Add("FMeasure", 3);//标识为打回到上一步状态
                        sl2.Add("FAppPerson", t_FAppPerson.Text.Trim());
                        sl2.Add("FIdea", backIdea);
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
            string title = "批量打回到上一级 操作人:" + Session["DFUserId"];
            string description = "FLinkId:" + FLinkId + " 企业名称:" + liter_FBaseName.Text + " 审核人:" + t_FAppPerson.Text + " 打回意见:" + backIdea;
            DataLog.Write(LogType.Info, LogSort.Operation, title, description);
        }
    }
    private void toEnd(string backIdea)
    {
        //dsb.ToEnd(fmanagetypeid,fappid,ftime,bz,result,matter,back_matter,qcfid)
        if (Session["DFId"].ToString() != ComFunction.GetDefaultDept())
            return;
        string FLinkId = this.ViewState["lid"].ToString();
        StringBuilder sb = new StringBuilder();
        //if (Session["DFId"].ToString() != "52")
        //    return;



    }
    protected void btnFinallyApp_Click(object sender, EventArgs e)
    {
        if (!this.isEnd())
        {
            this.Response.Write("<script>alert('还没有保存备案编号，不能提交!');</script>");
            return;
        }

        ReportProcess(ViewState["FBaseinfoId"].ToString());
        //setCertiState();
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>alert('操作成功');window.returnValue=1;window.close();</script>");
    }

    protected void Other_Listt_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
        }
    }

    private void retList(string ret)
    {
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue=" + ret + ";window.close();</script>");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (this.HSaveState.Value == "1")
        {
            retList("1");
        }
        else
        {
            retList("0");
        }
    }

    protected void btnQuery1_Click(object sender, EventArgs e)
    {
        //显示上报信息
        ShowReportInfo();
    }
    protected void btnAppSee_ServerClick(object sender, EventArgs e)
    {
        if (this.ViewState["lid"] == null)
        {
            return;
        }

        string sScript = "showApproveWindow('../AppQualiInfo/ShowAppInfo.aspx?lid=" + this.ViewState["lid"].ToString() + "',830,500);";
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>" + sScript + "</script>");
    }

    protected void btnSaveIdea_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SaveIdea();
        tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
        ShowReportInfo();
        ShowBtn();
    }

    private void SaveIdea()
    {
        pageTool tool = new pageTool(this.Page);
        string FLinkId = this.ViewState["lid"].ToString();
        if (tab_BJ.Visible)
        {//判断备案编号重复
            ProjectDB db = new ProjectDB();

            int n = db.CF_Prj_Certi.Count(t => t.FCertiNo == txtFCertiNo.Text && t.FCertiTypeId == EConvert.ToInt(ViewState["eqftype"]) && t.FAppId != FLinkId);

            if (n > 0)
            {
                tool.showMessage("批文号重复，请重新填写");
                return;
            }
        }
        int iCount = ReportInfo_List.Items.Count;
        SortedList[] sl = new SortedList[iCount];
        for (int i = 0; i < iCount; i++)
        {
            string AFId = ReportInfo_List.Items[i].Cells[ReportInfo_List.Items[i].Cells.Count - 1].Text;
            sl[i] = new SortedList();
            sl[i].Add("FID", ReportInfo_List.Items[i].Cells[ReportInfo_List.Items[i].Cells.Count - 6].Text);
            sl[i].Add("FProcessInstanceID", AFId);
            sl[i].Add("FAppPerson", this.t_FAppPerson.Text);
            sl[i].Add("FAppTime", EConvert.ToDateTime(this.t_FAppDate.Text));
            sl[i].Add("FCompany", this.t_FAppPersonUnit.Text);
            sl[i].Add("FFunction", this.t_FAppPersonJob.Text);
            sl[i].Add("FIdea", this.t_FAppIdea.Text);
            sl[i].Add("FLevel", EConvert.ToInt(Session["DFLevel"].ToString()));
            sl[i].Add("FLinkId", FLinkId);

            sl[i].Add("FMeasure", 1);//dResult.SelectedValue);
            sl[i].Add("FResult", t_FResult.SelectedValue);
            sl[i].Add("FUserId", Session["DFUserId"].ToString());
            EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + AFId + "'");
            DateTime fReportTime = epi.FReportDate;
            DateTime nowTime = DateTime.Now;
            TimeSpan spanTime = nowTime - fReportTime;
            sl[i].Add("FWaiteTime", spanTime.Days);
        }

        EntityTypeEnum[] en = new EntityTypeEnum[iCount];
        string[] skey = new string[iCount];
        SortedList[] slup = new SortedList[iCount];
        SaveOptionEnum[] su = new SaveOptionEnum[iCount];
        for (int i = 0; i < iCount; i++)
        {
            en[i] = EntityTypeEnum.EaProcessRecord;
            skey[i] = "FID";
            slup[i] = sl[i];
            su[i] = SaveOptionEnum.Update;
        }


        if (rc.SaveEBaseM(en, slup, skey, su))
        {

            CF_App_ProcessInstance ProcessInstance = db.CF_App_ProcessInstance.Where(t => t.FLinkId == FLinkId).FirstOrDefault();
            if (ProcessInstance != null)
            {
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

                idea.FResult = t_FResult.SelectedItem.Text;
                idea.FResultInt = EConvert.ToInt(t_FResult.SelectedValue);
                idea.FContent = t_FAppIdea.Text;
                idea.FAppTime = EConvert.ToDateTime(t_FAppDate.Text);
                idea.FSystemId = ProcessInstance.FSystemId;
                idea.FUserId = EConvert.ToString(Session["DFUserId"]);
                idea.FType = ProcessInstance.FManageTypeId;
                idea.FTime = DateTime.Now;
                CF_Prj_Certi certi = db.CF_Prj_Certi.Where(t => t.FAppId == FLinkId).FirstOrDefault();
                if (tab_BJ.Visible)
                {
                    if (certi == null)
                    {
                        certi = new CF_Prj_Certi();
                        certi.FId = Guid.NewGuid().ToString();
                        certi.FIsPrint = 0;
                        certi.FIsDeleted = 0;
                        certi.FCreateTime = DateTime.Now;
                        certi.FAppId = FLinkId;
                        db.CF_Prj_Certi.InsertOnSubmit(certi);
                    }
                    certi.FProjectId = ProcessInstance.FEmpId;
                    certi.FEntName = ProcessInstance.FEntName;
                    certi.FCertiTypeId = ProcessInstance.FManageTypeId;
                    certi.FAppDate = EConvert.ToDateTime(this.t_FAppDate.Text);
                    certi.FName = ProcessInstance.FEmpName;
                    certi.FBaseInfoId = ProcessInstance.FBaseInfoID;
                    certi.FAppDeptId = ProcessInstance.FManageDeptId;
                    certi.FCertiNo = txtFCertiNo.Text;
                    certi.FContent = txtFContent.Text;
                    certi.FTitle = txtFTitle.Text;
                    certi.FTime = DateTime.Now;
                    certi.FJSEntName = ProcessInstance.FLeadName;
                    certi.FJSEntId = ProcessInstance.FLeadId;
                    if (ProcessInstance.FManageTypeId == 294)
                    {
                        certi.FJSEntId = ProcessInstance.FBaseInfoID;
                        certi.FJSEntName = ProcessInstance.FEntName;
                        certi.FSJEntName = ProcessInstance.FLeadName;
                        certi.FSJEntId = ProcessInstance.FLeadId;
                        var result = (from a1 in db.CF_App_List
                                      join a2 in db.CF_App_List on a1.FPrjId equals a2.FPrjId
                                      join e in db.CF_Prj_Ent on a2.FId equals e.FAppId
                                      where a1.FId == FLinkId && a2.FManageTypeId == 291 && a2.FState == 6 && e.FEntType == 155
                                      orderby a2.FReportCount descending
                                      select e.FMoney).FirstOrDefault();

                        certi.FMoney = result;

                    }
                    else if (ProcessInstance.FManageTypeId == 283)
                    {
                        var result = (from a1 in db.CF_App_List
                                      join a2 in db.CF_App_List on a1.FLinkId equals a2.FLinkId
                                      join e in db.CF_Prj_Ent on a2.FLinkId equals e.FAppId
                                      where a1.FId == FLinkId && a2.FManageTypeId == 280 && a2.FState == 6 && e.FEntType == 15501
                                      orderby a2.FReportCount descending
                                      select e.FMoney).FirstOrDefault();

                        certi.FMoney = result;

                        var result1 = (from a1 in db.CF_App_List
                                       join a2 in db.CF_App_List on a1.FLinkId equals a2.FLinkId
                                       join e in db.CF_Prj_Ent on a2.FLinkId equals e.FAppId
                                       where a1.FId == FLinkId && a2.FManageTypeId == 280 && a2.FState == 6 && e.FEntType == 126
                                       orderby a2.FReportCount descending
                                       select new { e.FName, e.FBaseInfoId, e.FMoney }).FirstOrDefault();
                        if (result1 != null)
                        {
                            certi.FJSEntId = result1.FBaseInfoId;
                            certi.FJSEntName = result1.FName;
                            certi.FJZMoney = result1.FMoney;
                        }
                    }
                    else if (ProcessInstance.FManageTypeId == 28003)
                    {
                        var result1 = (from a1 in db.CF_App_List
                                       join a2 in db.CF_App_List on a1.FLinkId equals a2.FLinkId
                                       join e in db.CF_Prj_Ent on a2.FLinkId equals e.FAppId
                                       where a1.FId == FLinkId && a2.FManageTypeId == 280 && a2.FState == 6 && e.FEntType == 126
                                       orderby a2.FReportCount descending
                                       select new { e.FName, e.FBaseInfoId, e.FMoney }).FirstOrDefault();
                        if (result1 != null)
                        {
                            certi.FJSEntId = result1.FBaseInfoId;
                            certi.FJSEntName = result1.FName;
                            certi.FJZMoney = result1.FMoney;
                            certi.FMoney = result1.FMoney;
                        }



                    }
                    else if (ProcessInstance.FManageTypeId == 290)
                    {
                        var result = (from a1 in db.CF_App_List
                                      join a2 in db.CF_App_List on a1.FLinkId equals a2.FLinkId
                                      join e in db.CF_Prj_Ent on a2.FLinkId equals e.FAppId
                                      where a1.FId == FLinkId && a2.FManageTypeId == 287 && a2.FState == 6 && e.FEntType == 145
                                      orderby a2.FReportCount descending
                                      select e.FMoney).FirstOrDefault();
                        certi.FMoney = result;
                    }
                    else if (ProcessInstance.FManageTypeId == 305)
                    {
                        var result = (from a1 in db.CF_App_List
                                      join a2 in db.CF_App_List on a1.FLinkId equals a2.FLinkId
                                      join e in db.CF_Prj_Ent on a2.FLinkId equals e.FAppId
                                      where a1.FId == FLinkId && a2.FManageTypeId == 300 && a2.FState == 6 && e.FEntType == 145
                                      orderby a2.FReportCount descending
                                      select e.FMoney).FirstOrDefault();
                        certi.FMoney = result;
                    }
                    if (ProcessInstance.FManageTypeId == 411 || ProcessInstance.FManageTypeId == 421
                     || ProcessInstance.FManageTypeId == 412 || ProcessInstance.FManageTypeId == 422
                     || ProcessInstance.FManageTypeId == 413 || ProcessInstance.FManageTypeId == 423
                     || ProcessInstance.FManageTypeId == 414 || ProcessInstance.FManageTypeId == 424
                        )//合同备案
                    {
                        CF_Prj_Data data = (from t in db.CF_App_List
                                            join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                                            where t.FId == ProcessInstance.FLinkId
                                            select d).FirstOrDefault();
                        if (data != null)
                        {
                            certi.FCityId = data.FInt0;
                            certi.FMoney = data.FFloat1;
                            certi.FAddress = data.FDeptName;
                            certi.FContractType = data.FInt3;
                        }
                    }
                    else
                    {
                        certi.FCityId = EConvert.ToInt(db.CF_Prj_BaseInfo.Where(t => t.FId == ProcessInstance.FEmpId).Select(t => t.FAddressDept).FirstOrDefault());
                        certi.FAddress = db.CF_Prj_BaseInfo.Where(t => t.FId == ProcessInstance.FEmpId).Select(t => t.FAllAddress).FirstOrDefault();
                    }


                    //certi.FCityId = EConvert.ToInt(db.CF_Prj_BaseInfo.Where(t => t.FId == ProcessInstance.FEmpId).Select(t => t.FAddressDept).FirstOrDefault());
                    //certi.FAddress = db.CF_Prj_BaseInfo.Where(t => t.FId == ProcessInstance.FEmpId).Select(t => t.FAllAddress).FirstOrDefault();
                    certi.FContent = db.getManageTypeName(ProcessInstance.FManageTypeId);
                    if (!string.IsNullOrEmpty(hfTechnical.Value))
                    {
                       certi.FTechnical=EConvert.ToInt( hfTechnical.Value);
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" update CF_App_ProcessRecord set fisQuali=3 from CF_App_ProcessInstance ep  inner join CF_App_ProcessRecord er ");
                    sb.Append(" on  ep.flinkid='" + this.ViewState["lid"].ToString() + "' ");
                    sb.Append(" and ep.fid=er.FProcessInstanceID and ep.fsubflowid=er.fsubflowid ");
                    sb.Append("  where  ep.froleid in(" + Session["DFRoleId"].ToString() + ") ");
                    sb.Append(" and   er.fisQuali=1 ");
                    rc.PExcute(sb.ToString());
                }
                if (certi != null)
                {
                    if (t_FResult.SelectedValue == "3")
                    {
                        certi.FIsValid = 0;
                    }
                    else
                    {
                        certi.FIsValid = 1;
                    }
                }
                db.SubmitChanges();
            }


            HSaveState.Value = "1";
            this.btnReport.Enabled = true;
            this.btnFinallyApp.Enabled = true;
            ShowAppIdea();
        }
    }
    protected void AppInfo_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

            string fappResult = e.Item.Cells[7].Text;
            switch (fappResult)
            {
                case "1":
                    e.Item.Cells[7].Text = "通过";
                    break;

                case "3":
                    e.Item.Cells[7].Text = "不通过";
                    break;
            }
        }
    }

    protected void t_FResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t_FResult.SelectedValue == "3")
        {
            tab_BJ.Visible = false;
        }
        else
        {
            tab_BJ.Visible = true;
        }
    }
    void ShowFileList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select fid,FContent,FType from CF_Pub_Text where ftype='Approval' and  FLinkId='" + ViewState["lid"] +
                @"'  order by  ftime desc");
        DataTable dt = rc.GetTable(sb.ToString());
        DG_FileList.DataSource = dt;
        DG_FileList.DataBind();
    }
    protected void DG_FileList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();
            string fcontent = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FContent"));
            string fcontent1 = fcontent;
            if (!string.IsNullOrEmpty(fcontent) && fcontent.Contains("/"))
                fcontent = fcontent.Substring(fcontent.LastIndexOf("/") + 1).Substring(14);
            e.Item.Cells[1].Text = "<a href='" + ComFunction.FileServer(fcontent1) + "' target='_blank'>" + fcontent + "</a>";
            LinkButton lb = e.Item.Cells[3].Controls[0] as LinkButton;


            if (lb != null)
            {
                lb.Text = "删除";
                lb.Attributes.Add("onclick", "return confirm('确认要删除该附件吗？');");
            }
        }
    }
    protected void DG_FileList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            if (rc.PExcute("delete cf_Pub_Text where fid='" + fid + "'"))
                ShowFileList();
        }
    }
    protected void btnFileList_Click(object sender, EventArgs e)
    {
        ShowFileList();
    }
}
