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
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            { t_YWBM.Value = Request["FLinkId"].ToString(); }
            if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
            { t_fSubFlowId.Value = Request["fSubFlowId"].ToString(); }
            getTitel(); bindAuditList();
        }
    }
    //显示处理 初步处理
    public void getTitel()
    {
        if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
        {
            t_ftype.Value = Request["ftype"].ToString(); bindFile();
            if (Request["ftype"].ToString() == "1")
            {
                lbTile.Text = "节能材料接件"; accept.Visible = true;
                sqInfo.Visible = false;
                btnUPCS.Attributes.Add("style", "display:none;");
                btnUPFS.Attributes.Add("style", "display:none;");
                btnUPEND.Attributes.Add("style", "display:none;");
                bindAccept();
            }
            else if (Request["ftype"].ToString() == "10")
            {
                lbTile.Text = "节能材料初审";
                btnSave.Attributes.Add("style", "display:none;");
                bindSqInfo();
            }
            else if (Request["ftype"].ToString() == "15")
            {
                lbTile.Text = "节能材料复审";
                btnSave.Attributes.Add("style", "display:none;");
                bindSqInfo();
            }
            else if (Request["ftype"].ToString() == "5")
            {
                lbTile.Text = "节能材料领导审批";
                btnSave.Attributes.Add("style", "display:none;");
                bindSqInfo();
            }
        }
    }
    //材料绑定
    public void bindFile()
    {
        //《四川省建筑节能材料和产品备案表》（见附表）
        string sql = string.Format(@"select COUNT(1) cou from YW_FileList l 
                        inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId 
                        where l.FAppid='" + t_YWBM.Value + "' and l.FType=3005");
        DataTable dt = sh.GetTable(sql);
        btnOne.Attributes.Add("value", "查看(" + dt.Rows[0]["cou"].ToString() + ")");
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3005 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lOne.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=1000',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbOne.Text = dt.Rows[0]["Fremark"].ToString(); t_One.Value = dt.Rows[0]["FID"].ToString();
            cbOne.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lOne.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3005',400,300);\"><tt>未检测</tt></a>";
        }
        //企业营业执照（复印件加盖企业印章）
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3000 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lTwo.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=3000',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbTwo.Text = dt.Rows[0]["Fremark"].ToString(); t_Two.Value = dt.Rows[0]["FID"].ToString();
            cbTwo.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lTwo.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3000',400,300);\"><tt>未检测</tt></a>";
        }
        //代理商的代理合同
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3001 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lThree.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=3001',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbThree.Text = dt.Rows[0]["Fremark"].ToString(); t_Three.Value = dt.Rows[0]["FID"].ToString();
            cbThree.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lThree.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3001',400,300);\"><tt>未检测</tt></a>";
        }
        //设计施工验收技术规程（规范、标准）、标准图集、使用手册等相关技术资料
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3002 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lSix.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=3002',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbSix.Text = dt.Rows[0]["Fremark"].ToString(); t_Six.Value = dt.Rows[0]["FID"].ToString();
            cbSix.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lSix.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3002',400,300);\"><tt>未检测</tt></a>";
        }
        //法定检测机构出具的有效期内的产品型式检验报告（查验原件，收复印件，复印件须加盖企业公章）
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3003 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lSeven.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=3003',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbSeven.Text = dt.Rows[0]["Fremark"].ToString(); t_Seven.Value = dt.Rows[0]["FID"].ToString();
            cbSeven.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lSeven.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3003',400,300);\"><tt>未检测</tt></a>";
        }
        //质量管理有关资料或质量保证体系认证证书
        dt = sh.GetTable("select * from YW_FileState where FAppid='" + t_YWBM.Value + "' and FType=3004 ");
        if (dt != null && dt.Rows.Count > 0)
        {
            lEight.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
                + "&ftype=3004',400,300);\"><tt>" + dt.Rows[0]["Fstate"].ToString() + "</tt></a>";
            tbEight.Text = dt.Rows[0]["Fremark"].ToString(); t_Eight.Value = dt.Rows[0]["FID"].ToString();
            cbEight.Checked = dt.Rows[0]["Fhave"].ToString() == "0" ? true : false;
        }
        else
        {
            lEight.Text = "<a href='#' onclick=\"showAddWindow('fileState.aspx?fid=" + t_YWBM.Value
            + "&ftype=3004',400,300);\"><tt>未检测</tt></a>";
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
        sb.Append(" flinkid='" + t_YWBM.Value + "' and FMeasure=0 and FSubFlowId='" + t_fSubFlowId.Value + "'");
        sb.Append(" and froleid='" + this.Session["DFRoleId"].ToString() + "' and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
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
                t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
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
    //绑定申请信息
    public void bindSqInfo()
    {
        string sql = "select * from YW_JN_AppProduct where fappid='" + t_YWBM.Value + "' ";

    }
    //保存意见
    protected void btnSaveYJ_Click(object sender, EventArgs e)
    {
        save();
    }
    //保存方法本级审批意见
    protected void save()
    {
        if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
        {
            pageTool tool = new pageTool(this.Page);
            SortedList osort = new SortedList();
            if (Request["ftype"].ToString() == "1")
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

            }
            rc.SaveEBase(EntityTypeEnum.EaProcessRecord, osort, "FID", SaveOptionEnum.Update);
            savFile();
        }
    }
    //保存材料信息
    protected void savFile()
    {
        pageTool tool = new pageTool(this.Page);
        StringBuilder sb = new StringBuilder();
        sb.Append(" begin ");
        //《四川省建筑节能材料和产品备案表》（见附表）
        string have = cbOne.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_One.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbOne.Text
              + "' where FID='" + t_One.Value + "' and Ftype=3005 ");
        }
        else
        {
            t_One.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_One.Value
                + "','" + tbOne.Text + "','" + t_YWBM.Value + "','3005'," + have + ",'未检测')");
        }
        //企业营业执照（复印件加盖企业印章）
        have = cbTwo.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Two.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbTwo.Text
              + "' where FID='" + t_Two.Value + "' and Ftype=3000 ");
        }
        else
        {
            t_One.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Two.Value
                + "','" + tbTwo.Text + "','" + t_YWBM.Value + "','3000'," + have + ",'未检测')");
        }
        //代理商的代理合同
        have = cbThree.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Three.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbThree.Text
              + "' where FID='" + t_Three.Value + "' and Ftype=3001 ");
        }
        else
        {
            t_Three.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Three.Value
                + "','" + tbThree.Text + "','" + t_YWBM.Value + "','3001'," + have + ",'未检测')");
        }
        //设计施工验收技术规程（规范、标准）、标准图集、使用手册等相关技术资料
        have = cbSix.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Six.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbSix.Text
              + "' where FID='" + t_Six.Value + "' and Ftype=3002 ");
        }
        else
        {
            t_Six.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Six.Value
                + "','" + tbSix.Text + "','" + t_YWBM.Value + "','3002'," + have + ",'未检测')");
        }
        //法定检测机构出具的有效期内的产品型式检验报告（查验原件，收复印件，复印件须加盖企业公章）
        have = cbSeven.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Seven.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbSeven.Text
              + "' where FID='" + t_Seven.Value + "' and Ftype=3003 ");
        }
        else
        {
            t_Seven.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Seven.Value
                + "','" + tbSeven.Text + "','" + t_YWBM.Value + "','3003'," + have + ",'未检测')");
        }
        //质量管理有关资料或质量保证体系认证证书
        have = cbEight.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Eight.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbEight.Text
              + "' where FID='" + t_Eight.Value + "' and Ftype=3004 ");
        }
        else
        {
            t_Eight.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Eight.Value
                + "','" + tbEight.Text + "','" + t_YWBM.Value + "','3004'," + have + ",'未检测')");
        }
        //省外进入我省的建筑节能材料和产品除提供上述资料外，还应提供当地省级建设行政主管部门备案(认定)证明
        have = cbNight.Checked == true ? "0" : "1";
        if (!string.IsNullOrEmpty(t_Night.Value))
        {
            sb.Append(" update YW_FileState set Fhave=" + have + ",Fremark='" + tbNight.Text
              + "' where FID='" + t_Night.Value + "' and Ftype=3006 ");
        }
        else
        {
            t_Night.Value = Guid.NewGuid().ToString();
            sb.Append(" insert YW_FileState (FID,Fremark,FAppid,Ftype,Fhave,Fstate) values ('" + t_Night.Value
                + "','" + tbNight.Text + "','" + t_YWBM.Value + "','3006'," + have + ",'未检测')");
        }
    }
    //材料全选
    protected void btnAll_Click(object sender, EventArgs e)
    {
        cbOne.Checked = true; cbTwo.Checked = true; cbThree.Checked = true;
        cbFour.Checked = true; cbFive.Checked = true; cbSix.Checked = true;
        cbSeven.Checked = true; cbEight.Checked = true; cbNight.Checked = true;
    }
    //接件
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("接件成功", "window.returnValue='1';window.close();");
    }
    //初审
    protected void btnCS_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("初审成功", "window.returnValue='1';window.close();");
    }
    //复审
    protected void btnFS_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("复审成功", "window.returnValue='1';window.close();");
    }
    //领导审批
    protected void btnEnd_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        ReportProcess();
        tool.showMessageAndRunFunction("领导审批成功", "window.returnValue='1';window.close();");
    }

    #region 上报流程
    #region 初次加载版本库，修复报错添加的空
    protected void btnSee_Click(object sender, EventArgs e)
    { 
    
    }
    protected void DG_List_ItemDataBound(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {

    }
    protected void dgSQinfo_ItemDataBound(object sender, EventArgs e)
    {

    }
    protected void dResult_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnBackNext_Click(object sender, EventArgs e)
    {

    }


    #endregion
    private void ReportProcess()
    {
        save();
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
        //else sl[0].Add("FResult", dAudit.SelectedValue.Trim());
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
                       // sl2.Add("FAppPerson", t_Auditer.Text);
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


}