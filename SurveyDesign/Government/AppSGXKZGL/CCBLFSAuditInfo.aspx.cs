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
using System.Drawing;
using System.Linq;

using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;

using EgovaDAO;
using EgovaBLL;
using ProjectBLL;
using Tools;
using System.Data.SqlClient;

public partial class Government_AppSGXKZGL_CCBLFSAuditInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
            {
               t_fLinkId.Value = Request["FLinkId"].ToString();
            }
            if (Request["fpid"] != null && !string.IsNullOrEmpty(Request["fpid"]))
            {
                t_fProcessInstanceID.Value = Request["fpid"].ToString();
            }
            if (Request["ferid"] != null && !string.IsNullOrEmpty(Request["ferid"]))
            {
                t_fProcessRecordID.Value = Request["ferid"].ToString();
            }
            if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
            {
                t_fSubFlowId.Value = Request["fSubFlowId"].ToString();
            }
            if (Request["fBaseInfoId"] != null && !string.IsNullOrEmpty(Request["fBaseInfoId"]))
            {
                t_fBaseInfoId.Value = Request["fBaseInfoId"].ToString();
            }
            if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
            {
                t_fTypeId.Value = Request["ftype"].ToString();
            }
            
            init();
        }
    }


    //初始化各种信息
   protected void init()
    {
        initLayout();
        BindControl();
        bindBaseInfo();
        bindStateInfo();
        bindFileInfo();
        bindAuditInfo();
        bindAuditList();
        showYZList();
        showTKJLList();
        BuildSGXKZBH();
        
    }
   private void bindStateInfo()
   {
       string sql = " SELECT FId,SGXKZBB, FPublish from TC_SGXKZ_PrjState where FId= '" + t_fLinkId.Value + "'";
       using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
       {
           if (conn.State == ConnectionState.Closed)
               conn.Open();
           DataSet ds = new DataSet();

           SqlDataAdapter da = new SqlDataAdapter(sql, conn);
           da.Fill(ds, "ds");
           DataTable dt = ds.Tables[0];

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               string test = dt.Rows[i]["SGXKZBB"].ToString();
               s_FId.Value = EConvert.ToString(dt.Rows[i]["Fid"]);
               dResult0.SelectedValue = EConvert.ToString(dt.Rows[i]["SGXKZBB"]);
               dResult1.SelectedValue = EConvert.ToString(dt.Rows[i]["FPublish"]);
               break;
           }


       }
   }
   //初始化各种信息
   protected void initLayout()
   {
       if (Request["ftype"] != null && !string.IsNullOrEmpty(Request["ftype"]))
       {
           
       }
   }
    //绑定项目基本信息
    private void bindBaseInfo()
   {
       EgovaDB db = new EgovaDB();
       TC_SGXKZ_PrjInfo info = db.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == t_fLinkId.Value).FirstOrDefault();
      
       if (info != null)
       {
           pageTool tool = new pageTool(this.Page, "t_");
           tool.fillPageControl(info);
       }
       t_FAppSGXKZBH.Text = info.SGXKZBH;
       t_FAppFZJG.Text = info.FZJG;
       t_FAppFZRQ.Text = info.FZTime == null ? "" : info.FZTime.Value.ToString("yyyy-MM-dd");
   }
    //绑定项目附件信息
    private void bindFileInfo()
    {
        
    }
    //绑定当前审批意见（或接件意见）
    private void bindAuditInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" flinkid='" + t_fLinkId.Value + "' and FMeasure=0 and FSubFlowId='" + t_fSubFlowId.Value + "'");
        sb.Append(" and FRoleId in(" + this.Session["DFRoleId"].ToString() + ") and isnull(FAppPerson,'') <> ''");
        DataTable dt = rc.GetTable(EntityTypeEnum.EaProcessRecord, "", sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FAppPerson.Text = dt.Rows[0]["FAppPerson"].ToString();
            t_FAppPersonUnit.Text = dt.Rows[0]["FCompany"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppDate.Text = rc.StrToDate(dt.Rows[0]["FAppTime"].ToString());
            t_FAppIdea.Text = dt.Rows[0]["FIdea"].ToString();
            t_fProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
            t_fProcessInstanceID.Value = dt.Rows[0]["FProcessinstanceId"].ToString();
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

//            string sSql = string.Format(@"select er.FID,a.FID as PFID from CF_App_ProcessInstance a,CF_App_ProcessRecord er
//               where er.FMeasure='0' and a.fid=er.FProcessInstanceID and a.fsubflowid=er.fsubflowid and a.fstate<>6 
//               and a.FLinkId='" + t_fLinkId.Value + "' and a.FRoleId='" + this.Session["DFRoleId"].ToString()
//                                + "' and er.FSubFlowId='" + t_fSubFlowId.Value + "'");
//            dt = rc.GetTable(sSql);
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                t_fProcessRecordID.Value = dt.Rows[0]["FId"].ToString();
//                t_fProcessInstanceID.Value = dt.Rows[0]["PFID"].ToString();
//            }

        }
    }
    private void BindControl()
    {

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
        //查看上报资料按钮
        StringBuilder sb = new StringBuilder();
        sb.Append("FLinkId='" + t_fLinkId.Value + "'");
        EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FManageTypeId,Fsystemid", sb.ToString());
        if (ea == null)
        {
            return;
        }
        string fbid = t_fBaseInfoId.Value;
        string faid = t_fLinkId.Value;
        string fmid = ea.FManageTypeId;
        string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        string fsid = ea.FSystemId;
        string fQurl = rc.getMTypeQurl(ea.FManageTypeId); ;

        string fUrl = fQurl;
        HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");
    }
    //绑定审批意见列表
    private void bindAuditList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Remove(0, sb.Length);
        sb.Append(" select r.fid,fentname,");
        sb.Append(" p.flistid,R.FAppTime ,r.FReportTime,");
        sb.Append(" p.ftypeid,r.FIdea,r.FRoleDesc,p.flevelid,");
        sb.Append(" p.FManageTypeId,");
        sb.Append(" case r.fresult when 1 then '通过' when 3 then '不通过' end fresult,");
        sb.Append(" r.FCompany,r.FFunction,r.FAppPerson,s.FName ");
        sb.Append(" from CF_App_ProcessInstance p,CF_App_ProcessRecord r,CF_App_SubFlow s where ");
        sb.Append(" p.fid=r.FProcessInstanceID ");
        sb.Append(" and p.FProcessId=s.FProcessId and r.FTypeId=s.FTypeId ");
        sb.Append(" and p.fid='" + t_fProcessInstanceID.Value + "' ");
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
    /// <summary>
    /// 同步到标准库
    /// </summary>
    /// <param name="appid"></param>
    private void SyncBase(string appid)
    {
        const string sql = "Proc_SGXKZ_Sync @appid";
      var  a =  rc.PExcute(sql, new SqlParameter() { ParameterName = "@appid",Value = appid,SqlDbType = SqlDbType.VarChar});
    }

    //踏勘记录信息
    private void showTKJLList()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_Prj_XCTKJL.Where(t => t.FAppId == t_fLinkId.Value);
        dg_TKJL.DataSource = v;
        dg_TKJL.DataBind();
    }
    //押证信息
    private void showYZList()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_Prj_YZ.Where(t => t.FAppId == t_fLinkId.Value);
        DG_ListYZ.DataSource = v;
        DG_ListYZ.DataBind();
    }
   protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
   {
       if (e.Item.ItemIndex > -1)
       {
           e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

       }
   }
   protected void DG_ListTKJL_ItemDataBound(object sender, DataGridItemEventArgs e)
   {
       if (e.Item.ItemIndex > -1)
       {
           e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager_TKJL.PageSize * (this.Pager_TKJL.CurrentPageIndex - 1)).ToString();
           string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
           string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
           //string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
           //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
           //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
           e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('TKJLInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "',600,450);\">" + e.Item.Cells[2].Text + "</a>";
       }
   }
   protected void DG_ListYZ_ItemDataBound(object sender, DataGridItemEventArgs e)
   {
       if (e.Item.ItemIndex > -1)
       {
           e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager_YZ.PageSize * (this.Pager_YZ.CurrentPageIndex - 1)).ToString();
           string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
           string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
           //string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
           //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
           //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
           e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('YZInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "',600,450);\">" + e.Item.Cells[2].Text + "</a>";
       }
   }
   protected void Pager_TKJL_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
   {
       Pager_TKJL.CurrentPageIndex = e.NewPageIndex;

   }
   protected void Pager_YZ_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
   {
       Pager_YZ.CurrentPageIndex = e.NewPageIndex;

   }
   #region 按钮事件
   private void DisableButton()
   {
       btnUPFS.Enabled = false;
       btnSave.Enabled = false;
       bthEndApp.Enabled = false;
       btnBackToEnt.Enabled = false;
   }
   protected void btn_del_TKJL_Click(object sender, EventArgs e)
   {
       EgovaDB dbContext = new EgovaDB();
       pageTool tool = new pageTool(this.Page);
       tool.DelInfoFromGrid(dg_TKJL, dbContext.TC_Prj_XCTKJL, tool_Deleting);
       showTKJLList();

   }
   protected void btn_del_YZ_Click(object sender, EventArgs e)
   {
       EgovaDB dbContext = new EgovaDB();
       pageTool tool = new pageTool(this.Page);
       tool.DelInfoFromGrid(DG_ListYZ, dbContext.TC_Prj_YZ, tool_Deleting_TKJL);
       showYZList();

   }
   private void tool_Deleting_TKJL(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
   {

   }
   protected void btnRefresh_Click(object sender, EventArgs e)
   {
       showTKJLList();
   }
   protected void btnReload_Click(object sender, EventArgs e)
   {
       showYZList();
   }
   private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
   {

   }
   //保存意见
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try
       {
           WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
           //MODIFY 林勇
           //委婉的实现保存额外的信息
           using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
           {
               string sql = "UPDATE TC_SGXKZ_PrjInfo SET FZJG='" + t_FAppFZJG.Text + "',FZTime='" + t_FAppFZRQ.Text + "',SGXKZBH='" + t_FAppSGXKZBH.Text + "' WHERE FAppId='" + t_fLinkId.Value+ "'";

               if (conn.State == ConnectionState.Closed)
                   conn.Open();
               DataSet ds = new DataSet();
               SqlCommand cmd = new SqlCommand(sql, conn);
               //conn.Open();
               int a = cmd.ExecuteNonQuery();
           }
           saveStateInfo();
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存失败');", true);
       }
       
   }

   private void saveStateInfo()
   {
       string fId = s_FId.Value;
       //EgovaDB db = new EgovaDB();

       //pageTool tool = new pageTool(this.Page,"s_");
       //TC_SGXKZ_PrjState info = new TC_SGXKZ_PrjState();
       //s_FPrjItemId.Value = t_PrjItemId.Value ;
       //if (!string.IsNullOrEmpty(fId))
       //{
       //    info = db.TC_SGXKZ_PrjState.Where(t => t.FPrjItemId == t_PrjItemId.Value).FirstOrDefault();
       //}
       //else
       //{
       //    fId = t_fLinkId.Value;// Guid.NewGuid().ToString();
       //    info.FId = fId;
       //    db.TC_SGXKZ_PrjState.InsertOnSubmit(info);
       //}
       //info = tool.getPageValue(info);
       //db.SubmitChanges();


       string sql = "";
       if (string.IsNullOrEmpty(fId))
       {
           fId = t_fLinkId.Value;
           sql = "INSERT INTO TC_SGXKZ_PrjState (FId,FPrjId,FPrjItemId,SGXKZBB,FPublish) VALUES ( '" + fId + "','','";
           sql += t_PrjItemId.Value + "'," + dResult0.SelectedValue + "," + dResult1.SelectedValue + ")";
       }
       else
       {
           sql = "UPDATE TC_SGXKZ_PrjState SET FPublish = " + dResult1.SelectedValue + ",SGXKZBB = " + dResult0.SelectedValue + " WHERE FId  = '" + fId + "' ";
       }
       using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
       {
           if (conn.State == ConnectionState.Closed)
               conn.Open();
           DataSet ds = new DataSet();
           SqlCommand cmd = new SqlCommand(sql, conn);

           cmd.ExecuteNonQuery();

       }
       s_FId.Value = fId;

   }
    /// <summary>
    /// 锁定人员
    /// </summary>
   private void lockEmp()
   {
       //EgovaDB db = new EgovaDB();
       //var v = from a in db.TC_PrjItem_Emp
       //        where !db.TC_PrjItem_Emp_Lock.Any(t=>t.FIdCard==a.FIdCard)  
       //        select a;
       string sql = "";
       try {
           using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
           {
               if (conn.State == ConnectionState.Closed)
                   conn.Open();
               DataSet ds = new DataSet();
               //sql = @"select * from TC_PrjItem_Emp where FIdCard not in (select FIdCard from TC_PrjItem_Emp_Lock where FAppId='"+t_fLinkId.Value+"')";
               //modify by psq  20150322  锁定人员限制范围是本业务id的，并且没有被锁定的
               sql = @"select * from TC_PrjItem_Emp where FAppId = '"+t_fLinkId.Value + "'"
                   +" and FIdCard not in (select FIdCard from TC_PrjItem_Emp_Lock where FAppId='" + t_fLinkId.Value + "')";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "ds");
            DataTable dt = ds.Tables[0];
            
            for (int i = 0; i < dt.Rows.Count; i++) {
                sql = "INSERT INTO TC_PrjItem_Emp_Lock (FId,FIdCard,FHumanName,FAppId,FPrjId,FPrjItemId,FEntId,FEntName,IsLock,SelectedCount) VALUES ";
                sql += "('" + Guid.NewGuid().ToString();
                sql += "','" + dt.Rows[i]["FIdCard"].ToString();
                sql += "','" + dt.Rows[i]["FHumanName"].ToString();//item.FHumanName;
                sql += "','" + dt.Rows[i]["FAppId"].ToString();
                sql += "','" + dt.Rows[i]["FPrjId"].ToString(); 
                sql += "','" + dt.Rows[i]["FPrjItemId"].ToString(); 
                sql += "','" + dt.Rows[i]["FEntId"].ToString(); 
                sql += "','" + dt.Rows[i]["FEntName"].ToString();
                sql += "',1,1)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                int a = cmd.ExecuteNonQuery();
                sql = "";//每次执行完成sql后清空sql
            }

              // foreach (var item in v.ToList<TC_PrjItem_Emp>())
             //  {
                  
                   //TC_PrjItem_Emp_Lock lockInfo = new TC_PrjItem_Emp_Lock();
                   //lockInfo.FId = Guid.NewGuid().ToString();
                   //lockInfo.FIdCard = item.FIdCard;
                   //lockInfo.FHumanName = item.FHumanName;
                   //lockInfo.FAppId = item.FAppId;
                   //lockInfo.FPrjId = item.FPrjId;
                   //lockInfo.FPrjItemId = item.FPrjItemId;
                   //lockInfo.FEntId = item.FEntId;
                   //lockInfo.FEntName = item.FEntName;
                   //db.TC_PrjItem_Emp_Lock.InsertOnSubmit(lockInfo);




              // }
           }
       }
       catch(Exception ex){
           string test=sql;
           throw ex;
           }
       
      // sql = sql.Substring(0, sql.Length - 1);

      
      // db.SubmitChanges();
       
   }
   /// <summary>
   /// 提交打证
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   protected void btnUPFS_Click(object sender, EventArgs e)
   {
       try
       {
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = this.Session["DFUserId"].ToString();
               lockEmp();
               dResult.SelectedValue = "1";//接件操作强制选中同意项
               WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                   t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                  t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               string appid = EConvert.ToString(Session["FAppId"].ToString());
               DisableButton();
               //同步到标准库
               if (!string.IsNullOrEmpty(appid))
                   SyncBase(appid);
               //同步信息到归档库中，调用存储过程SP_GD_SGXKZ,add by psq 20150429,传入工程id（PrjItemId）和业务id（Fappid)
               RCenter rc = new RCenter("dbcenter");
               rc.PExcute("exec SP_GD_SGXKZ '" + t_PrjItemId.Value + "','" + appid + "'");
               //
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('办理成功！');", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行相关操作');", true);
           }

       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('办理失败！');", true);
       }
   }
  
    //审核不通过，直接结案
   protected void bthEndApp_Click(object sender, EventArgs e)
   {
       try
       {
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = SConvert.ToString(Session["DFUserId"]);
               string sIdea = t_FAppIdea.Text;
               dResult.SelectedValue = "3";//强制选中不同意项
               WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                      t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               WFApp.EndApp(t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId, sIdea);
               DisableButton();


               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作成功');", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行该操作');", true);
           }

       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作失败');", true);
       }
   }
   //回退到企业
   protected void btnBackToEnt_Click(object sender, EventArgs e)
   {
       try
       {
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = SConvert.ToString(Session["DFUserId"]);
               string fLevel = SConvert.ToString(Session["FLevel"]);
               dResult.SelectedValue = "3";//强制选中不同意项
               WFApp.BackToEnt(t_fProcessInstanceID.Value, dfUserId, fLevel,
                   t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                       t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               DisableButton();
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位成功');", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行该操作');", true);
           }

       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位失败');", true);
       }
   }
   protected void btnLockHuman_Click(object sender, EventArgs e)
   {

   }
   
   #endregion

   #region 生成施工许可证编号
   protected void BuildSGXKZBH() {
       //EgovaDB db = new EgovaDB();
       //TC_SGXKZ_PrjInfo info = db.TC_SGXKZ_PrjInfo.Where(t => t.FAppId == t_fLinkId.Value).FirstOrDefault();
       using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
       {
           string sql = " select top 1 PrjAddressDept,PrjItemType, getdate() as today,(select COUNT(1) from TC_SGXKZ_PrjInfo where DATEDIFF(DAY,FZTime,GETDATE()) = 0)  AS  YBL from TC_SGXKZ_PrjInfo WHERE FAppId ='" + t_fLinkId.Value + "'";

           if (conn.State == ConnectionState.Closed)
               conn.Open();
           DataSet ds = new DataSet();
           SqlCommand cmd = new SqlCommand(sql, conn);
           //conn.Open();
           //int a = cmd.ExecuteNonQuery();

           SqlDataAdapter da = new SqlDataAdapter(sql, conn);
           da.Fill(ds, "ds");
           DataTable dt = ds.Tables[0];
           string PrjAddressDept = "";
           string PrjItemType = "";
           string date = "";
           string BH = "";
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               PrjAddressDept = dt.Rows[0]["PrjAddressDept"].ToString();
              
               switch (dt.Rows[0]["PrjItemType"].ToString())
               { 
                   case "2000103":
                       PrjItemType = "03";
                       break;
                   case "2000101":
                       PrjItemType = "01";
                       break;
                   case "2000102":
                       PrjItemType = "02";
                       break;
                   default:
                       PrjItemType = "03";
                       break;
               }
               DateTime today = DateTime.Parse(dt.Rows[0]["today"].ToString());
               date = today.ToString("yyyyMMdd");
               if (string.IsNullOrEmpty(PrjAddressDept))
               {
                   throw new Exception("项目所属地不能为空");
               }
               else if (PrjAddressDept.Length == 2) {
                   BH = PrjAddressDept + "0000";
               }
               else if (PrjAddressDept.Length == 4)
               {
                   BH = PrjAddressDept + "00";
               }
               else
               {
                   BH = PrjAddressDept;
               }
               BH += date;
               int xh = int.Parse(dt.Rows[0]["YBL"].ToString());

               if (xh > 9)
               {
                   BH += xh.ToString();
               }
               else {
                   BH += "0"+xh.ToString();
               }
               
               BH += PrjItemType;



           }

           if (string.IsNullOrEmpty(t_FAppSGXKZBH.Text)) {
               t_FAppSGXKZBH.Text = BH;
           }
           
           
       }
 
       
       


   }
    #endregion

}