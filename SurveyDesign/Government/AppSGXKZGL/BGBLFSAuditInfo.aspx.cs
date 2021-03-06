﻿using System;
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

public partial class Government_AppSGXKZGL_BGBLFSAuditInfo : System.Web.UI.Page
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
        bindFileInfo();
        bindAuditInfo();
        bindAuditList();
        showYZList();
        showBGPrj();
        showBGEnt();
        showBGEmp();
        
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
       TC_SGXKZ_PrjInfo info = (from a in db.TC_SGXKZ_PrjInfo
                                join
                                 b in db.TC_SGXKZ_BGPrjInfo
                                on a.FId equals b.FPrjInfoId
                                where b.FAppId.Equals(t_fLinkId.Value)
                                select a).FirstOrDefault();

       if (info != null)
       {
           pageTool tool = new pageTool(this.Page, "t_");
           tool.fillPageControl(info);
       }
   }
    //绑定项目附件信息
    private void bindFileInfo()
    {

        EgovaDB dbContext = new EgovaDB();
        //当前业务类型
        string FAppId = EConvert.ToString(Session["FAppId"]);

        var v = from t in dbContext.CF_Sys_PrjList
                orderby t.FId
                where t.FManageType == 11225
                select new
                {
                    t.FId,
                    t.FFileName,
                    FFileCount = t.FFileAmount,
                    AppFile = dbContext.TC_QA_File.Where(f => t.FId == f.FMaterialTypeId && f.FAppId == FAppId)
                };
        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
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
        HSeeReportInfo0.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");

        EgovaDB db = new EgovaDB();
        CF_App_List app = (from a in db.CF_App_List
                           join b in db.CF_App_List
                               on a.FLinkId equals b.FLinkId
                           where a.FManageTypeId == 11224 && b.FManageTypeId == 11223
                           select b).FirstOrDefault();
        fbid = t_fBaseInfoId.Value;
        faid = app.FId;
        fmid = "11223";
        frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        fsid = ea.FSystemId;
        fQurl = rc.getMTypeQurl(ea.FManageTypeId);
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

    //押证信息
    private void showYZList()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_Prj_YZ.Where(t => t.FPrjItemId == t_PrjItemId.Value);
        Pager2.RecordCount = v.Count();
        DG_ListYZ.DataSource = v;
        DG_ListYZ.DataBind();
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        showYZList();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(DG_ListYZ, dbContext.TC_Prj_YZ, tool_Deleting);
        showYZList();
    }
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {

    }
    protected void DG_ListYZ_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager2.PageSize * (this.Pager2.CurrentPageIndex - 1)).ToString();
            string fId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string fAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            //string fEntId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FEntId"));
            //string fPrjItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjItemId"));
            //string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('YZInfo.aspx?FId=" + fId
                 + "&fAppId=" + fAppId
                 + "&FPrjItemId=" + t_PrjItemId.Value
                 + "',700,500);\">" + e.Item.Cells[2].Text + "</a>";
        }
    }
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager2.CurrentPageIndex = e.NewPageIndex;
        showYZList();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString();

        }
    }
    //项目变更信息
    private void showBGPrj()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_SGXKZ_BGJG.Where(t => t.FAppId == t_fLinkId.Value);
        pagerPrj.RecordCount = v.Count();
        dgPrj.DataSource = v;
        dgPrj.DataBind();
    }
    //企业变更信息
    private void showBGEnt()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_SGXKZ_QYBGJG.Where(t => t.FAppId == t_fLinkId.Value);
        pagerEnt.RecordCount = v.Count();
        dgEnt.DataSource = v;
        dgEnt.DataBind();
    }
    //人员变更信息
    private void showBGEmp()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_SGXKZ_RYBGJG.Where(t => t.FAppId == t_fLinkId.Value);
        pagerEmp.RecordCount = v.Count();
        dgEmp.DataSource = v;
        dgEmp.DataBind();
    }
    protected void dgPrj_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.pagerPrj.PageSize * (this.pagerPrj.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void pagerPrj_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        pagerPrj.CurrentPageIndex = e.NewPageIndex;
        showBGPrj();

    }
    protected void dgEnt_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.pagerEnt.PageSize * (this.pagerEnt.CurrentPageIndex - 1)).ToString();
        }
    }
    protected void pagerEnt_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        pagerEnt.CurrentPageIndex = e.NewPageIndex;
        showBGEnt();

    }
    protected void dgEmp_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.pagerEmp.PageSize * (this.pagerEmp.CurrentPageIndex - 1)).ToString();
            if (EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "BGQK")) == "退出")
            {
                //如果是人员退出，则不用审核
                ((RadioButton)e.Item.FindControl("IYS")).Visible = false;
                ((RadioButton)e.Item.FindControl("IwS")).Visible = false;

            }
            else
            {
                object ckstate = DataBinder.Eval(e.Item.DataItem, "checkstate");
                if (ckstate == null || ckstate == DBNull.Value)
                {
                    ((Label)e.Item.FindControl("Notcheck")).Text = "<font color='red'>未审核</font>";
                }
                switch (EConvert.ToInt(ckstate))
                {
                    case 1:
                        ((RadioButton)e.Item.FindControl("IYS")).Checked = true;
                        break;
                    case 0:
                        ((RadioButton)e.Item.FindControl("IwS")).Checked = true;
                        break;
                    default:
                        ((Label)e.Item.FindControl("Notcheck")).Text = "<font color='red'>未审核</font>";
                        break;
                }
            }
        }
    }
    protected void pagerEmp_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        pagerEmp.CurrentPageIndex = e.NewPageIndex;
        showBGEmp();

    }
   #region 按钮事件
   private void DisableButton()
   {
       btnUPFS.Enabled = false;
       btnSave.Enabled = false;
       bthEndApp.Enabled = false;
       btnBackToEnt.Enabled = false;
   }
   //保存意见
   protected void btnSave_Click(object sender, EventArgs e)
   {
       try
       {
           SaveCkRy(); //保存对人员的审核
           WFApp.Assign(t_fProcessRecordID.Value, t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                   t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存成功');", true);
       }
       catch (Exception ee)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存失败');", true);
       }
       
   }

   //保存人员审核意见
   protected void SaveCkRy()
   {
       string FId = "";
       int istate = 3;
       StringBuilder sb = new StringBuilder();
       sb.Append(" update TC_SGXKZ_RYBGJG set checkstate =  b.ckstate from TC_SGXKZ_RYBGJG a,(select '1' as fid,2 as ckstate");
       int RowCount = dgEmp.Items.Count;

       for (int i = 0; i < dgEmp.Items.Count; i++)
       {
           FId = dgEmp.Items[i].Cells[dgEmp.Columns.Count - 1].Text.Trim();
           if (((RadioButton)dgEmp.Items[i].FindControl("IYS")).Checked)
           { istate = 1; }
           if (((RadioButton)dgEmp.Items[i].FindControl("IWS")).Checked)
           { istate = 0; }
           if (istate != 3)
           {
               sb.Append(" union select '" + FId.ToString() + "'," + istate.ToString());
           }
           istate = 3;
       }
       sb.Append(" ) b where a.Fid = b.fid and isnull(a.checkstate,3) <>  b.ckstate");   
       if (sb.Length > 10)
       {

           using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
           {
               if (conn.State == ConnectionState.Closed)
                   conn.Open();
               SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
               cmd.ExecuteNonQuery();

           }
       }
   }

   //复审
   protected void btnUPFS_Click(object sender, EventArgs e)
   {
       //变更办理复审需要做几件事情:1、审核状态调整，2、锁定新增人员/解锁退出人员，3、同步信息到归档库
       //try
       //{
           if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
           {
               string dfUserId = this.Session["DFUserId"].ToString();
               dResult.SelectedValue = "1";//接件操作强制选中同意项
               WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value, dfUserId,
                   t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                  t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);
               DisableButton();

               string fappid = t_fLinkId.Value;

               //锁定增加的人员
               lockperson(fappid);
               //解锁退出的人员
               unlockperson();
               //同步信息到归档库中，调用存储过程SP_GD_SGXKZ,add by psq 20150429,传入工程id（PrjItemId）和业务id（Fappid)
               RCenter rc = new RCenter("dbcenter");
               rc.PExcute("exec SP_GD_SGXKZ '" + t_PrjItemId.Value + "','" + fappid + "'");
               //
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('办结成功');", true);
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行接件操作');", true);
           }

       //}
       //catch (Exception ee)
       //{
       //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('办结失败');", true);
       //}
   }
   
   
    /// <summary>
   /// 解锁人员
   /// 解锁数据来源是人员变更的退出类型
    /// </summary>
   protected void unlockperson()
   {
       Common cm = new Common();
       string sql = "";
       using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
       {
           if (conn.State == ConnectionState.Closed)
               conn.Open();
           DataSet ds = new DataSet();
           sql = @"select b.FAppId,b.FPrjItemId,b.FIdCard,b.FHumanName  
                     from TC_PrjItem_Emp b
                    where b.needDel = 1
                      and b.FAppId  = '" + t_fLinkId.Value + "'";
           SqlDataAdapter da = new SqlDataAdapter(sql, conn);
           da.Fill(ds, "ds");
           DataTable dt = ds.Tables[0];
           //解锁退出的人员
           string sappid, fprjitemid,fidcard;//业务编号，工程编号，人员身份证号
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               sappid = dt.Rows[i]["FAppId"].ToString();      
               fprjitemid = dt.Rows[i]["FPrjItemId"].ToString();              
               fidcard = dt.Rows[i]["FIdCard"].ToString(); 
               //调用解锁方法
               cm.UnlockPerson(sappid,fprjitemid,fidcard);
           }
       }       
   }

   /// <summary>
   /// 锁定人员
   /// 锁定数据来源是人员变更的增加类型
   /// </summary>
   protected void  lockperson(string Fappid)
   {
       string errMsg = "";
       Common cm = new Common();
       if (cm.lockperson(Fappid, out errMsg) == false)
       {
           ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('锁定人员失败！/r/n错误信息：" + errMsg + "');", false);
       }
       #region
       // Common cm = new Common();
//        string sql = "";    
//        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
//        {
//            if (conn.State == ConnectionState.Closed)
//                conn.Open();
//            DataSet ds = new DataSet();
//            //只锁定施工总承包、专业承包、劳务分包、监理类企业的人员(2\3\4\7),勘察、设计类人员不锁定(5\6)
//            sql = @"select  a.FAppId,d.FId FPrjId,a.FPrjItemId,b.ProjectName,c.QYBM FEntId,'' FEntName,c.SFZH FIdCard,c.XM FHumanName 
//                      from  TC_SGXKZ_RYBGJG a,TC_PrjItem_Info b,JST_XZSPBaseInfo.dbo.RY_RYJBXX c,TC_Prj_Info d
//                    where a.FPrjItemId = b.FId
//                    and a.FLinkId = c.RYBH
//                    and b.FPrjId = d.FId
//                    and a.fenttype  in (2,3,4,7)
//                    and a.BGQK = '增加'  and FAppId  = '" + t_fLinkId.Value+"'";           
//            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
//            da.Fill(ds, "ds");
//            DataTable dt = ds.Tables[0];
//            //锁定增加的人员
//            string sappid, fprjid, fprjitemid, fentid, fentname, fhumanname, fidcard;//业务编号，项目编号，工程编号，所在企业编号，企业名称，人员姓名，人员身份证号
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                sappid = dt.Rows[i]["FAppId"].ToString();
//                fprjid = dt.Rows[i]["FPrjId"].ToString();
//                fprjitemid = dt.Rows[i]["FPrjItemId"].ToString();
//                fentid = dt.Rows[i]["FEntId"].ToString();
//                fentname = dt.Rows[i]["FEntName"].ToString();
//                fhumanname = dt.Rows[i]["FHumanName"].ToString();
//                fidcard = dt.Rows[i]["FIdCard"].ToString();
//                cm.lockperson(sappid, fprjid, fprjitemid, fentid, fidcard, fhumanname);
//            }
       // }
       #endregion
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

   protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
   {
       Pager1.CurrentPageIndex = e.NewPageIndex;
       bindFileInfo();
   }
   protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
   {
       if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
       {
           string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
           string FFileName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
           TextBox txtFileRemark = e.Item.FindControl("txtFileRemark") as TextBox;
           CheckBox chkIsReady = e.Item.FindControl("chkIsReady") as CheckBox;
           EgovaDB db = new EgovaDB();
           TC_Prj_FileReady data = db.TC_Prj_FileReady.Where(t => t.FAppId == t_fLinkId.Value
                                   && t.FMaterialTypeId == FId).FirstOrDefault();
           if (data != null)
           {
               txtFileRemark.Text = data.FRemarks;
               chkIsReady.Checked = EConvert.ToBool(data.FIsReady);

           }

           IQueryable<TC_QA_File> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<TC_QA_File>;
           if (AppFile != null && AppFile.Count() > 0)
           {
               //((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
               Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
               rep_File.DataSource = AppFile;
               rep_File.DataBind();
           }
       }

   }

   protected void rep_List_ItemCommand(object source, RepeaterCommandEventArgs e)
   {
       switch (e.CommandName)
       {
           case "update":
               string fMaterialTypeId = e.CommandArgument.ToString();//取得参数
               TextBox txtFileRemark = e.Item.FindControl("txtFileRemark") as TextBox;
               CheckBox chkIsReady = e.Item.FindControl("chkIsReady") as CheckBox;
               EgovaDB db = new EgovaDB();
               TC_Prj_FileReady data = db.TC_Prj_FileReady.Where(t => t.FAppId == t_fLinkId.Value
                                       && t.FMaterialTypeId == fMaterialTypeId).FirstOrDefault();
               if (data == null)
               {
                   data = new TC_Prj_FileReady();
                   data.FId = Guid.NewGuid().ToString();
                   data.FMaterialTypeId = fMaterialTypeId;
                   data.FAppId = t_fLinkId.Value;
                   data.FIsReady = chkIsReady.Checked;
                   data.FRemarks = txtFileRemark.Text;
                   db.TC_Prj_FileReady.InsertOnSubmit(data);
               }
               else
               {
                   data.FIsReady = chkIsReady.Checked;
                   data.FRemarks = txtFileRemark.Text;
               }
               db.SubmitChanges();
               MyPageTool.showMessage("保存成功", this.Page);
               break;
       }
   }
   
   #endregion
   
}