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
using ProjectBLL;
using ProjectData;

public partial class Government_AppMain_AcceptReportInfoGF : System.Web.UI.Page
{
    RCenter rc = new RCenter(); RApp ra = new RApp(); ProjectDB db = new ProjectDB();
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            { t_YWBM.Value = Request["FLinkId"].ToString(); }
            if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
            { t_fSubFlowId.Value = Request["fSubFlowId"].ToString(); }
            btnJW.Attributes.Add("onclick", "if(checkJW()){return true;}else{return false;}");
            getTitel(); bindFile(); bindGFInfo(); bindAuditOne(); bindAuditList(); bindJW(); bindDetail();
        }
    }
    //显示处理 初步处理
    public void getTitel()
    {
        if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
        {
            t_ftype.Value = Request["ftype"].ToString();
            if (Request["ftype"].ToString() == "1")
            {
                lbTile.Text = "工法接件"; accept.Visible = true;
                oneAudit.Visible = false; //btnSave.Attributes.Add("style", "display:none;");
                btnUPCS.Attributes.Add("style", "display:none;");
                btnUPFS.Attributes.Add("style", "display:none;");
                btnUPEND.Attributes.Add("style", "display:none;");
                btnBackNext.Visible = false; file.Attributes.Add("style", "display:block;");
                btnFile.Visible = false;
            }
            else if (Request["ftype"].ToString() == "10")
            {
                lbTile.Text = "工法初审"; oneAudit.Visible = true;
                accept.Visible = false; btnSave.Attributes.Add("style", "display:none;");
                //btnUPCS.Attributes.Add("style", "display:none;");
                btnUPFS.Attributes.Add("style", "display:none;");
                btnUPEND.Attributes.Add("style", "display:none;");
                btnBack.Visible = false; btnBackNext.Visible = true;
            }
            else if (Request["ftype"].ToString() == "15")
            {
                lbTile.Text = "工法复审"; oneAudit.Visible = true;
                accept.Visible = false; btnSave.Attributes.Add("style", "display:none;");
                btnUPCS.Attributes.Add("style", "display:none;");
                //btnUPFS.Attributes.Add("style", "display:none;");
                btnUPEND.Attributes.Add("style", "display:none;");
                btnBack.Visible = false; btnBackNext.Visible = false;
            }
            else if (Request["ftype"].ToString() == "5")
            {
                lbTile.Text = "工法领导审批"; oneAudit.Visible = true;
                accept.Visible = false; btnSave.Attributes.Add("style", "display:none;");
                btnUPCS.Attributes.Add("style", "display:none;");
                btnUPFS.Attributes.Add("style", "display:none;");
                btnUPEND.Attributes.Add("style", "display:none;");
                btnBack.Visible = false; btnBackNext.Visible = false;
                jiuwei.Visible = true;
                btnSaveYJ.Attributes.Add("onclick", "if(checkJW()){return true;}else{return false;}");
            }
        }
    }
    //绑定工法信息
    public void bindGFInfo()
    {
        string sql = "select j.Fname,j.GFMC,l.FReportDate,j.FListName,j.FTypeName,j.Linkman,j.LinkmanMobile from CF_App_List l,YW_GF_JBQK  j where l.FId=j.YWBM and l.FBaseinfoId=j.FBaseInfoId and l.FID='" + t_YWBM.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_Fname.Text = dt.Rows[0]["Fname"].ToString();
            t_GFMC.Text = dt.Rows[0]["GFMC"].ToString();
            t_FReportDate.Text = dt.Rows[0]["FReportDate"].ToString();
            t_FListName.Text = dt.Rows[0]["FListName"].ToString();
            t_FTypeName.Text = dt.Rows[0]["FTypeName"].ToString();
            t_Linkman.Text = dt.Rows[0]["Linkman"].ToString();
            t_LinkmanMobile.Text = dt.Rows[0]["LinkmanMobile"].ToString();
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
    //绑定当前审批意见
    public void bindAuditOne()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + t_YWBM.Value + "' and FMeasure=0 and FSubFlowId='" + t_fSubFlowId.Value + "'");
        sb.Append(" and froleid='" + this.Session["DFRoleId"].ToString() + "' and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_Auditer.Text = t_FAppPerson.Text;
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_AuditUnit.Text = t_FAppPersonUnit.Text;
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_AuditFunction.Text = t_FAppPersonJob.Text;
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_AuditTime.Text = t_FAppDate.Text;
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
            t_AuditIdear.Text = t_FAppIdea.Text;
            t_ProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
            t_FProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
            if (dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dResult.SelectedValue = dResult.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;
            if (dAudit.Items.FindByValue(dt.Rows[0]["FResult"].ToString()) != null)
                dAudit.SelectedValue = dAudit.Items.FindByValue(dt.Rows[0]["FResult"].ToString()).Value;
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
                t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
                t_Auditer.Text = t_FAppPerson.Text; t_AuditFunction.Text = t_FAppPersonJob.Text;
                t_AuditUnit.Text = t_FAppPersonUnit.Text; t_AuditTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            string sSql = string.Format(@"select er.FID,a.FID as PFID from CF_App_ProcessInstance a,CF_App_ProcessRecord er
               where er.FMeasure='0' and a.fid=er.FProcessInstanceID and a.fsubflowid=er.fsubflowid and a.fstate<>6 
               and a.FLinkId='" + t_YWBM.Value + "' and a.FRoleId='" + this.Session["DFRoleId"].ToString()
                                + "' and er.FSubFlowId='" + t_fSubFlowId.Value + "'");
            dt = rc.GetTable(sSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                t_ProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
                t_FProcessInstanceID.Value = dt.Rows[0]["PFID"].ToString();
            }

        }
    }
    //材料绑定
    public void bindFile()
    {
        //工法内容材料
        string sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                        where l.FAppid='" + t_YWBM.Value + "' and l.FType=1000");
        DataTable dt = sh.GetTable(sql);
        btnUP.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1000 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=1000',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ.Text = dt.Rows[0]["Fremark"].ToString(); t_FID0.Value = dt.Rows[0]["FID"].ToString();
            cb.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1000',400,300);\"><tt>未检测</tt></a>";
        }
        //省工法报表
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lBaoBiao.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=1',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBaoBiao.Text = dt.Rows[0]["Fremark"].ToString(); t_BaoBiao.Value = dt.Rows[0]["FID"].ToString();
            cbBaoBiao.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lBaoBiao.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1',400,300);\"><tt>未检测</tt></a>";
        }
        //企业级工法批准文件复印件
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=2 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lFYJ.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=2',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbFYJ.Text = dt.Rows[0]["Fremark"].ToString(); t_FYJ.Value = dt.Rows[0]["FID"].ToString();
            cbFYJ.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lFYJ.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=2',400,300);\"><tt>未检测</tt></a>";
        }
        //关键技术评估意见
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1006");
        dt = sh.GetTable(sql);
        btnUP4.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1006 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY4.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1006',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ4.Text = dt.Rows[0]["Fremark"].ToString(); t_FID4.Value = dt.Rows[0]["FID"].ToString();
            cb4.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY4.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1006',400,300);\"><tt>未检测</tt></a>";
        }
        //工程应用证明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1002");
        dt = sh.GetTable(sql);
        btnUP2.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1002 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY2.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=1002',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ2.Text = dt.Rows[0]["Fremark"].ToString(); t_FID2.Value = dt.Rows[0]["FID"].ToString();
            cb2.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY2.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1002',400,300);\"><tt>未检测</tt></a>";
        }
        //经济效益证明
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1005");
        dt = sh.GetTable(sql);
        btnUP7.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1005 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY7.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1005',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ7.Text = dt.Rows[0]["Fremark"].ToString(); t_FID7.Value = dt.Rows[0]["FID"].ToString();
            cb7.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY7.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1005',400,300);\"><tt>未检测</tt></a>";
        }
        //完成单位意见、无争议声明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1001");
        dt = sh.GetTable(sql);
        btnUP1.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1001 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY1.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=1001',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ1.Text = dt.Rows[0]["Fremark"].ToString(); t_FID1.Value = dt.Rows[0]["FID"].ToString();
            cb1.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY1.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1001',400,300);\"><tt>未检测</tt></a>";
        }
        //专业技术情报部门提供的科技查新报告复印件
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=3 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lCL.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=3',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbCL.Text = dt.Rows[0]["Fremark"].ToString(); t_CL.Value = dt.Rows[0]["FID"].ToString();
            cbCL.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lCL.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3',400,300);\"><tt>未检测</tt></a>";
        }
        //科技成果获奖证明相关附件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1004");
        dt = sh.GetTable(sql);
        btnUP5.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1004 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY5.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1004',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ5.Text = dt.Rows[0]["Fremark"].ToString(); t_FID5.Value = dt.Rows[0]["FID"].ToString();
            cb5.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY5.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1004',400,300);\"><tt>未检测</tt></a>";
        }
        //专业技术专利证明文件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1007");
        dt = sh.GetTable(sql);
        btnUP6.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1007 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY6.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1007',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ6.Text = dt.Rows[0]["Fremark"].ToString(); t_FID6.Value = dt.Rows[0]["FID"].ToString();
            cb6.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY6.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1007',400,300);\"><tt>未检测</tt></a>";
        }
        //工法操作要点照片（10到15张）
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1008");
        dt = sh.GetTable(sql);
        btnUP8.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1008 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY8.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
              + "&ftype=1008',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ8.Text = dt.Rows[0]["Fremark"].ToString(); t_FID8.Value = dt.Rows[0]["FID"].ToString();
            cb8.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY8.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1008',400,300);\"><tt>未检测</tt></a>";
        }
        //工法成熟可靠性说明文件
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1003");
        dt = sh.GetTable(sql);
        btnUP3.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1003 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY3.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1003',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ3.Text = dt.Rows[0]["Fremark"].ToString(); t_FID3.Value = dt.Rows[0]["FID"].ToString();
            cb3.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY3.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1003',400,300);\"><tt>未检测</tt></a>";
        }
        //技术转让的证明材料
        sql = string.Format(@"select COUNT(1) cou from YW_GF_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1009");
        dt = sh.GetTable(sql);
        btnUP9.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_GF_FileState where FAppid='" + t_YWBM.Value + "' and FType=1009 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lJY9.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
               + "&ftype=1009',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbBZ9.Text = dt.Rows[0]["Fremark"].ToString(); t_FID9.Value = dt.Rows[0]["FID"].ToString();
            cb9.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lJY9.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=1009',400,300);\"><tt>未检测</tt></a>";
        }
    }
    //就位信息
    public void bindJW()
    {
        string sql = "select FNu,Fwh,CONVERT(nvarchar(10),Fpztime,121) Fpztime,FDep from YW_GF_JBQK  where YWBM='" + t_YWBM.Value + "'";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FNu.Text = dt.Rows[0]["FNu"].ToString();
            t_Fwh.Text = dt.Rows[0]["Fwh"].ToString();
            t_Fpztime.Text = dt.Rows[0]["Fpztime"].ToString();
            t_FDep.Text = dt.Rows[0]["FDep"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["FNu"].ToString()))
            { //btnUPEND.Attributes.Add("style", "display:block;");
                btnUPEND.Attributes.Remove("style");
            }
        }
    }
    //绑定专家要素信息
    public void bindDetail()
    {
        string sql = string.Format(@"select * from YW_GF_ExpertDetail
                     where Fappid='" + t_YWBM.Value + "' and FSteps ='" + t_ftype.Value + "'  ");
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["FType"].ToString())
                {
                    case "1":
                        rblOne.SelectedValue = rblOne.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbOne.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_one.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "2":
                        rblTWO.SelectedValue = rblTWO.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbTWO.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_TWO.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "3":
                        rblThree.SelectedValue = rblThree.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbThree.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Three.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "4":
                        rblFour.SelectedValue = rblFour.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbFour.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Four.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "5":
                        rblFive.SelectedValue = rblFive.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbFive.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Five.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "6":
                        rblSix.SelectedValue = rblSix.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbSix.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Six.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "7":
                        rblSeven.SelectedValue = rblSeven.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbSeven.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Seven.Value = dt.Rows[i]["FID"].ToString();
                        break;
                    case "8":
                        rblEghit.SelectedValue = rblEghit.Items.FindByValue(dt.Rows[i]["Fstate"].ToString()).Value;
                        tbEghit.Text = dt.Rows[i]["Fremark"].ToString();
                        ts_Eghit.Value = dt.Rows[i]["FID"].ToString();
                        break;
                }
            }
        }
    }
    //接件
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("接件成功", "window.returnValue='1';window.close();");
    }
    //保存意见
    protected void btnSaveYJ_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        try
        {
            saveFile(); saveJW(); saveZJIdear(); saveIdear();// btnUPEND.Attributes.Add("style", "display:block;");
            btnUPEND.Attributes.Remove("style");
            tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';");
        }
        catch (ExecutionEngineException ex) { tool.showMessage("保存失败，请重试"); }

    }
    public void saveIdear()
    {
        pageTool tool = new pageTool(this.Page);
        SortedList osort = new SortedList();
        if (t_ftype.Value == "1")
        {
            osort.Add("FID", t_ProcessRecordID.Value);
            osort.Add("FAppPerson", t_FAppPerson.Text);
            osort.Add("FCompany", t_FAppPersonUnit.Text);
            osort.Add("FFunction", t_FAppPersonJob.Text);
            osort.Add("FAppTime", t_FAppDate.Text);
            osort.Add("FIdea", t_FAppIdea.Text);
            osort.Add("FResult", dResult.SelectedValue.Trim());
        }
        else
        {
            osort.Add("FID", t_ProcessRecordID.Value);
            osort.Add("FAppPerson", t_Auditer.Text);
            osort.Add("FCompany", t_AuditUnit.Text);
            osort.Add("FFunction", t_AuditFunction.Text);
            osort.Add("FAppTime", t_AuditTime.Text);
            osort.Add("FIdea", t_AuditIdear.Text);
            osort.Add("FResult", dAudit.SelectedValue.Trim());
        }
        rc.SaveEBase(EntityTypeEnum.EaProcessRecord, osort, "FID", SaveOptionEnum.Update);
    }
    //保存专家要素信息
    public void saveZJIdear()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = null;      

        if (!string.IsNullOrEmpty(ts_one.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblOne.SelectedValue
              + "',Fremark='" + tbOne.Text + "',FTime=getdate() where FID='" + ts_one.Value + "' ;");
        }
        else
        {
            ts_one.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_one.Value + "',1,'" + rblOne.SelectedValue
                                       + "','" + tbOne.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_TWO.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblTWO.SelectedValue
              + "',Fremark='" + tbTWO.Text + "',FTime=getdate() where FID='" + ts_TWO.Value + "' ;");
        }
        else
        {
            ts_TWO.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_TWO.Value + "',2,'" + rblTWO.SelectedValue
                                       + "','" + tbTWO.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Three.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblThree.SelectedValue
              + "',Fremark='" + tbThree.Text + "',FTime=getdate() where FID='" + ts_Three.Value + "' ;");
        }
        else
        {
            ts_Three.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Three.Value + "',3,'" + rblThree.SelectedValue
                                       + "','" + tbThree.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Four.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblFour.SelectedValue
              + "',Fremark='" + tbFour.Text + "',FTime=getdate() where FID='" + ts_Four.Value + "' ;");
        }
        else
        {
            ts_Four.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Four.Value + "',4,'" + rblFour.SelectedValue
                                       + "','" + tbFour.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Five.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblFive.SelectedValue
              + "',Fremark='" + tbFive.Text + "',FTime=getdate() where FID='" + ts_Five.Value + "' ;");
        }
        else
        {
            ts_Five.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Five.Value + "',5,'" + rblFive.SelectedValue
                                       + "','" + tbFive.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Six.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblSix.SelectedValue
              + "',Fremark='" + tbSix.Text + "',FTime=getdate() where FID='" + ts_Six.Value + "' ;");
        }
        else
        {
            ts_Six.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Six.Value + "',6,'" + rblSix.SelectedValue
                                       + "','" + tbSix.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Seven.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblSeven.SelectedValue
              + "',Fremark='" + tbSeven.Text + "',FTime=getdate() where FID='" + ts_Seven.Value + "' ;");
        }
        else
        {
            ts_Seven.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Seven.Value + "',7,'" + rblSeven.SelectedValue
                                       + "','" + tbSeven.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        if (!string.IsNullOrEmpty(ts_Eghit.Value))
        {
            sql += string.Format(@" update YW_GF_ExpertDetail set Fstate='" + rblEghit.SelectedValue
              + "',Fremark='" + tbEghit.Text + "',FTime=getdate() where FID='" + ts_Eghit.Value + "' ;");
        }
        else
        {
            ts_Eghit.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_GF_ExpertDetail (FID,FType,Fstate,Fremark,FTime,Fappid,FSteps)
                            values ('" + ts_Eghit.Value + "',8,'" + rblEghit.SelectedValue
                                       + "','" + tbEghit.Text + "',getdate(),'" + t_YWBM.Value + "','" + t_ftype.Value + "') ;");
        }
        rc.PExcute(sql);
    }
    //保存附件信息
    public void saveFile()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" begin ");
        //工法内容材料
        string have = cb.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID0.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ.Text
              + "' where FID='" + t_FID0.Value + "' and Ftype=1000 ");
        }
        else
        {
            t_FID0.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID0.Value
                + "','" + tbBZ.Text + "','" + t_YWBM.Value + "','1000'," + have + ",'未检测')");
        }
        //省工法报表
        have = cbBaoBiao.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_BaoBiao.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBaoBiao.Text
              + "' where FID='" + t_BaoBiao.Value + "' and Ftype=1 ");
        }
        else
        {
            t_BaoBiao.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_BaoBiao.Value
                + "','" + tbBaoBiao.Text + "','" + t_YWBM.Value + "','1'," + have + ",'未检测')");
        }
        //企业级工法批准文件复印件
        have = cbFYJ.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FYJ.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbFYJ.Text
              + "' where FID='" + t_FYJ.Value + "' and Ftype=2 ");
        }
        else
        {
            t_FYJ.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FYJ.Value
                + "','" + tbFYJ.Text + "','" + t_YWBM.Value + "','2'," + have + ",'未检测')");
        }
        //关键技术评估意见
        have = cb4.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID4.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ4.Text
              + "' where FID='" + t_FID4.Value + "' and Ftype=1006 ");
        }
        else
        {
            t_FID4.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID4.Value
                + "','" + tbBZ4.Text + "','" + t_YWBM.Value + "','1006'," + have + ",'未检测')");
        }
        //工程应用证明相关附件
        have = cb2.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID2.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ2.Text
              + "' where FID='" + t_FID2.Value + "' and Ftype=1002 ");
        }
        else
        {
            t_FID2.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID2.Value
                + "','" + tbBZ2.Text + "','" + t_YWBM.Value + "','1002'," + have + ",'未检测')");
        }
        //经济效益证明
        have = cb7.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID7.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ7.Text
              + "' where FID='" + t_FID7.Value + "' and Ftype=1005 ");
        }
        else
        {
            t_FID7.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID7.Value
                + "','" + tbBZ7.Text + "','" + t_YWBM.Value + "','1005'," + have + ",'未检测')");
        }
        //完成单位意见、无争议声明相关附件
        have = cb1.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID1.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ.Text
              + "' where FID='" + t_FID1.Value + "' and Ftype=1001 ");
        }
        else
        {
            t_FID1.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID1.Value
                + "','" + tbBZ1.Text + "','" + t_YWBM.Value + "','1001'," + have + ",'未检测')");
        }
        //专业技术情报部门提供的科技查新报告复印件
        have = cbCL.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_CL.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbCL.Text
              + "' where FID='" + t_CL.Value + "' and Ftype=3 ");
        }
        else
        {
            t_CL.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_CL.Value
                + "','" + tbCL.Text + "','" + t_YWBM.Value + "','3'," + have + ",'未检测')");
        }
        //科技成果获奖证明相关附件
        have = cb5.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID5.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ5.Text
              + "' where FID='" + t_FID5.Value + "' and Ftype=1004 ");
        }
        else
        {
            t_FID5.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID5.Value
                + "','" + tbBZ5.Text + "','" + t_YWBM.Value + "','1004'," + have + ",'未检测')");
        }
        //专业技术专利证明文件
        have = cb6.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID6.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ6.Text
              + "' where FID='" + t_FID6.Value + "' and Ftype=1007 ");
        }
        else
        {
            t_FID6.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID6.Value
                + "','" + tbBZ6.Text + "','" + t_YWBM.Value + "','1007'," + have + ",'未检测')");
        }
        //工法成熟可靠性说明文件
        have = cb3.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID3.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ3.Text
              + "' where FID='" + t_FID3.Value + "' and Ftype=1003 ");
        }
        else
        {
            t_FID3.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID3.Value
                + "','" + tbBZ3.Text + "','" + t_YWBM.Value + "','1003'," + have + ",'未检测')");
        }
        //法操作要点照片（10到15张）
        have = cb8.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID8.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ8.Text
              + "' where FID='" + t_FID8.Value + "' and Ftype=1008 ");
        }
        else
        {
            t_FID8.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID8.Value
                + "','" + tbBZ8.Text + "','" + t_YWBM.Value + "','1008'," + have + ",'未检测')");
        }
        //技术转让的证明材料
        have = cb9.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_FID9.Value))
        {
            sb.Append(" update YW_GF_FileState set Fhave=" + have + ",Fremark='" + tbBZ9.Text
              + "' where FID='" + t_FID9.Value + "' and Ftype=1009 ");
        }
        else
        {
            t_FID9.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_GF_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_FID9.Value
                + "','" + tbBZ9.Text + "','" + t_YWBM.Value + "','1009'," + have + ",'未检测')");
        }
        sb.Append(" end ");
        rc.PExcute(sb.ToString());
    }
    //打回下级
    protected void btnBackNext_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        try
        {
            BackToPre(t_YWBM.Value);
            tool.showMessageAndRunFunction("打回成功", "window.returnValue='1';;window.close();");
        }
        catch (Exception ex) { tool.showMessage("打回失败"); }
    }
    //初审
    protected void btnCS_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("初审提交成功", "window.returnValue='1';window.close();");
    }
    //复审
    protected void btnFS_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("复审提交成功", "window.returnValue='1';window.close();");
    }
    //领导审批 办结
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        string sql = "select count(1) from YW_GF_JBQK where isnull(FNu,'')<>'' and isnull(Fwh,'')<>'' and isnull(Fpztime,'')<>'' and isnull(FDep,'')<>'' and YWBM='" + t_YWBM.Value + "' ";
        int cou = int.Parse(rc.GetSignValue(sql));
        if (cou > 0)
        {
            ReportProcess();
            sh.PExcute(" exec JKC_PRO_ProcessEnd '" + t_YWBM.Value + "' ");
            tool.showMessageAndRunFunction("办结成功", "window.returnValue='1';window.close();");
        }
        else { tool.showMessage("请先就位工法证书"); }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

        }
    }

    #region 上报流程
    private void ReportProcess()
    {
        saveIdear();
        string FLinkId = t_YWBM.Value;
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
        else sl[0].Add("FResult", dAudit.SelectedValue.Trim());
        sl[0].Add("FUserId", this.Session["DFUserId"].ToString());
        EaProcessInstance epi = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FReportDate", "fid='" + t_FProcessInstanceID.Value + "'");
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        sl[0].Add("FWaiteTime", spanTime.Days);
        //ra.BatchAppKCSJ(sl);
        ra.GovReportProcessKCSJ(sl, "");
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
        eap.FMeasure = 0;
        eap.FOrder = 5000;
        eap.FUserId = Session["DFUserId"].ToString();
        DateTime fReportTime = epi.FReportDate;
        DateTime nowTime = DateTime.Now;
        TimeSpan spanTime = nowTime - fReportTime;
        eap.FWaiteTime = spanTime.Days;
        ra.BackProcessToEnt(Request["pid"], eap);
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
                        sl2.Add("FAppPerson", t_Auditer.Text);
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

    //就位工法
    protected void btnJW_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        saveJW();
        tool.showMessage("就位成功");
    }
    public void saveJW()
    {
        string sql = "update YW_GF_JBQK set FNu='" + t_FNu.Text.Trim() + "',Fwh='" + t_Fwh.Text.Trim() + "',Fpztime='" + t_Fpztime.Text.Trim()
            + "',FDep='" + t_FDep.Text.Trim() + "' where YWBM='" + t_YWBM.Value + "' ";
        rc.PExcute(sql);
    }
    //刷新材料绑定
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindFile();
    }
    //查看业务
    protected void btnSee_Click(object sender, EventArgs e)
    {
        this.Session["FAppId"] = t_YWBM.Value;
        this.Session["FManageTypeId"] = "4000";
        Session["FIsApprove"] = 1;
        Response.Write("<script language='javascript'>window.open('../../GFEnt/AppMain/aIndex.aspx');</script>");
    }
   
}