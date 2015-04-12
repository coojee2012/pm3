
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Approve.RuleCenter;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.RuleApp;

using EgovaDAO;
using EgovaBLL;
using ProjectBLL;
using Tools;
using System.Text;
public partial class JSDW_ApplySGXKZGL_GCXX : System.Web.UI.Page
{
   
    

    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["FAppId"] != null && !string.IsNullOrEmpty(Request["FAppId"]))
        {
            t_fLinkId.Value = Request["FAppId"].ToString();
        }
        if (Request["FId"] != null && !string.IsNullOrEmpty(Request["FId"]))
        {
            t_fProcessInstanceID.Value = Request["FId"].ToString();
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

    //初始化各种信息
    protected void init()
    {
        initLayout();
        BindControl();
        bindBaseInfo();
        bindFileInfo();

        bindAuditList();
        showTKJLList();
     

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

    }
    //绑定项目附件信息
    private void bindFileInfo()
    {

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
        EaProcessInstanceBackup ea = (EaProcessInstanceBackup)rc.GetEBase(EntityTypeEnum.EaProcessInstanceBackup, "FManageTypeId,Fsystemid", sb.ToString());
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
        HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["FUserId"].ToString() + "')");
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
        sb.Append(" from CF_App_ProcessInstanceBackup p,CF_App_ProcessRecordBackup r,CF_App_SubFlow s where ");
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
        string sql = sb.ToString();
        this.DG_List.DataSource = rc.GetTable(sql);
        this.DG_List.DataBind();
    }
    //踏勘记录信息
    private void showTKJLList()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = dbContext.TC_Prj_XCTKJL.Where(t => t.FAppId == t_fLinkId.Value);
        dg_TKJL.DataSource = v;
        dg_TKJL.DataBind();
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

            string sql = "SELECT * FROM TC_Prj_XCTKJL_File WHERE FLinkId='" + fId + "'";
            string content = "";
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbCenter"].ConnectionString))
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                DataSet ds = new DataSet();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds, "ds");
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    content += "<a href='" + dt.Rows[i]["FFilePath"].ToString() + "' target='_blank' title='点击查看文件'>" + dt.Rows[i]["FFileName"].ToString() + "</a><br/>";
                }
              

            }
            
            //e.Item.Cells[2].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('../AppSGXKZGL/TKJLInfo.aspx?FId=" + fId + "&FAppId=" + fAppId + "',600,450);\">" + e.Item.Cells[2].Text + "</a>";

            e.Item.Cells[8].Text = content;
        }
    }

    protected void Pager_TKJL_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager_TKJL.CurrentPageIndex = e.NewPageIndex;

    }

    #region 按钮事件
    private void DisableButton()
    {
     
    }
    protected void btn_del_TKJL_Click(object sender, EventArgs e)
    {
        EgovaDB dbContext = new EgovaDB();
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_TKJL, dbContext.TC_Prj_XCTKJL, tool_Deleting);
        showTKJLList();

    }

    private void tool_Deleting_TKJL(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {

    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        showTKJLList();
    }

    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {

    }


    protected void btnLockHuman_Click(object sender, EventArgs e)
    {

    }

    #endregion


    
}