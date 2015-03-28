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
using Approve.Common;
using Approve.EntityBase;
using Approve.EntityCenter;
using Approve.EntityQuali;
using Approve.RuleApp;
using Approve.RuleCenter;
using System.Text;
using Approve.PersistBase;
using ProjectBLL;
using System.Linq;
using EgovaDAO;
using EgovaBLL;

public partial class Government_AppZLJDBA_seeOneReportInfo : govBasePage
{
    RCenter rc = new RCenter();
    RQuali rq = new RQuali();
    RApp ra = new RApp();
    
    WorkFlowApp wf = new WorkFlowApp();
    ArrayList arrCon = new ArrayList();
    private string sTemp = "";
    private StringBuilder sScript = new StringBuilder();
    private string fLinkId = null;
    private string fSubFlowId = null;
    private string fBaseInfoId = null;
    private string fpid = null;
    private string ferid = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        BindControl();
        if (Request["FLinkId"] != null && !string.IsNullOrEmpty(Request["FLinkId"]))
        {
            t_YWBM.Value = Request["FLinkId"].ToString();
            fLinkId = Request["FLinkId"].ToString();
        }
        if (Request["fpid"] != null && !string.IsNullOrEmpty(Request["fpid"]))
        {
            fpid = Request["fpid"].ToString();
        }
        if (Request["ferid"] != null && !string.IsNullOrEmpty(Request["ferid"]))
        {
            ferid = Request["ferid"].ToString();
        }
        if (Request["fSubFlowId"] != null && !string.IsNullOrEmpty(Request["fSubFlowId"]))
        {
            t_fSubFlowId.Value = Request["fSubFlowId"].ToString();
            fSubFlowId = Request["fSubFlowId"].ToString();
        }
        if (Request["fBaseInfoId"] != null && !string.IsNullOrEmpty(Request["fBaseInfoId"]))
        {
            t_fBaseInfoId.Value = Request["fBaseInfoId"].ToString();
            fBaseInfoId = Request["fBaseInfoId"].ToString();
        }
        if (!IsPostBack)
        {           
            this.ShowLinkInfo();
            this.ShowInfo();          
        }
        
    }

    void BindControl()
    {

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
    }

    //显示连接信息
    public void ShowLinkInfo()
    {
        if (fBaseInfoId == null)
        {
            return;
        }
        if (fLinkId == null)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("FLinkId='" + fLinkId + "'");
        EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FManageTypeId,Fsystemid", sb.ToString());
        if (ea == null)
        {
            return;
        }
        string fbid = this.fBaseInfoId;
        string faid = this.fLinkId;
        string fmid = ea.FManageTypeId;
        string frid = rc.GetSignValue(EntityTypeEnum.EsUser, "FMenuRoleId", "FBaseInfoId='" + fbid + "'");
        string fsid = ea.FSystemId;
        string fQurl = rc.getMTypeQurl(ea.FManageTypeId); ;

        string fUrl = fQurl;
        HSeeReportInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString() + "')");
    //    fUrl = fQurl;
    //    HSeePrintInfo.Attributes.Add("onclick", "openWinNew('" + fUrl + "?sysid=" + fsid + "&fBaseId=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&isPrint=1')");

    }

    private void ShowInfo()
    {
        StringBuilder sb = new StringBuilder();
        DataTable dt = new DataTable();
        object fuserId = Session["DFUserId"];
        sb.Remove(0, sb.Length);
        sb.Append(" select FLinkMan,fcompany,FFunction,FTel,FDepartmentID ");
        sb.Append(" from cf_sys_user where fid='" + fuserId + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            txtFSeeTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now); ;
            t_FAppPerson.Text = dt.Rows[0]["FLinkMan"].ToString();
            t_FAppPersonJob.Text = dt.Rows[0]["FFunction"].ToString();
            t_FAppPersonUnit.Text = RBase.GetDepartmentName(dt.Rows[0]["FDepartmentID"].ToString()) + RBase.GetDepartmentName(dt.Rows[0]["FCompany"].ToString());
        }
        sb.Remove(0, sb.Length);
        sb.Append(" select pr.FIdea,qa.FAppID, qa.ProjectName, qa.PrjItemName, qa.PrjItemType, qa.RecordNo, i.JSDW, i.JSDWDZ ");
        sb.Append(" from TC_QA_Record qa, CF_App_ProcessInstance pi, TC_Prj_Info i, CF_App_ProcessRecord pr ");
        sb.Append(" where pi.FManageDeptId like '" + Session["DFId"].ToString() + "%' ");
        sb.Append(" and pi.flinkId = qa.FAppId and i.FId = qa.FPrjId and pi.fId = pr.FProcessInstanceID and pi.FID = '" + fpid + "'");
        dt = rc.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["FAppId"] = dt.Rows[0]["FAppID"].ToString();
            t_ProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
            t_RecordNo.Text = dt.Rows[0]["RecordNo"].ToString();
            t_PrjItemName.Text = dt.Rows[0]["PrjItemName"].ToString();
            t_PrjItemType.SelectedValue = dt.Rows[0]["PrjItemType"].ToString();
            t_JSDW.Text = dt.Rows[0]["JSDW"].ToString();
            t_Address.Text = dt.Rows[0]["JSDWDZ"].ToString();
            t_FSeeOpinion.Text = dt.Rows[0]["FIdea"].ToString();
        }
        EgovaDB dbContext = new EgovaDB();
        var v = from t in dbContext.TC_QA_NeedFile
                orderby t.FId
                select new
                {
                    t.FId,
                    t.FFileName,
                    FFileCount = t.FFileCount,
                    AppFile = dbContext.TC_QA_File.Where(f => t.FId == f.FMaterialTypeId && f.FAppId == ViewState["FAppId"])
                };
        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1).ToString();
            string pId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fsSeeState = e.Item.Cells[e.Item.Cells.Count - 8].Text;
            string fLinkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            string fMeasure = e.Item.Cells[e.Item.Cells.Count - 3].Text;
            string fManageTypeId = e.Item.Cells[3].Text;
            string fSubFlowId = e.Item.Cells[9].Text;
            string fListId = e.Item.Cells[4].Text;
            string fTypeId = e.Item.Cells[5].Text;
            string fLevelId = e.Item.Cells[6].Text;
            string fsystemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FSystemId"));
            e.Item.Cells[3].Text = rc.GetSignValue(EntityTypeEnum.EsManageType, "FName", "FNumber='" + fManageTypeId + "'");
            e.Item.Cells[9].Text = rc.GetSignValue(EntityTypeEnum.EaSubFlow, "FName", "FId='" + fSubFlowId + "'");

            if (fListId == "" || fListId == "&nbsp;")
            {
                fListId = "''";
            }
            if (fTypeId == "" || fTypeId == "&nbsp;")
            {
                fTypeId = "''";
            }
            if (fLevelId == "" || fLevelId == "&nbsp;")
            {
                fLevelId = "''";
            }


            e.Item.Cells[4].Text = "";
            fListId = rc.GetDicRemark(fListId);
            if (fListId != "")
            {
                e.Item.Cells[4].Text += fListId; //+ "<br>";
            }
            fTypeId = rc.GetDicRemark(fTypeId);
            if (fTypeId != "")
            {
                e.Item.Cells[4].Text += fTypeId; //+ "<br>";
            }

            if (fLevelId != "")
            {
                fLevelId = rc.GetSignValue(EntityTypeEnum.EsQualiLevel, "FName", "FNumber=" + fLevelId);
                if (string.IsNullOrEmpty(fLevelId))
                {
                    if (!string.IsNullOrEmpty(e.Item.Cells[3].Text))
                    {
                        e.Item.Cells[4].Text += e.Item.Cells[3].Text;
                    }
                }
                else
                {
                    if (fLevelId.Trim() == "所有等级")
                    {
                        if (!string.IsNullOrEmpty(e.Item.Cells[3].Text))
                        {
                            e.Item.Cells[4].Text += e.Item.Cells[3].Text;
                        }
                    }
                    else
                    {
                        e.Item.Cells[4].Text += fLevelId;
                    }
                }
            }



            string linkId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            EaProcessInstance ea = (EaProcessInstance)rc.GetEBase(EntityTypeEnum.EaProcessInstance, "FBaseInfoId,FManageTypeId,fsystemid", "fid='" + pId + "'");
            if (ea == null)
            {
                return;
            }
            string fbid = ea.FBaseInfoId;
            string faid = linkId;
            string fmid = ea.FManageTypeId;
            string fEntName = e.Item.Cells[2].Text;
            //查询查询地址
            string sUrl = "";
            //sUrl = rc.getSysQurl(ea.FSystemId);
            //sUrl += "&fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fuid=" + Session["DFUserId"].ToString();
            //string fDesc = "查询企业审核通过的信息,首次申请的企业没有数据";
            //e.Item.Cells[2].Text = "<a class='link5' href='" + sUrl + "' target='_blank' title='" + fDesc + "'>" + e.Item.Cells[2].Text + "</a>";
            sUrl = rc.GetSignValue("select FQurl from cf_Sys_SystemName where fnumber='8814'");
            if (!string.IsNullOrEmpty(sUrl))
            {
                sUrl += "?sysid=" + fsystemId + "&FBaseId=" + fbid + "&faid=" + faid + "&fmid=" + fManageTypeId + "&fly=1";
                //e.Item.Cells[2].Text = "<a class='link7' href='" + sUrl + "' target='_blank'>" + e.Item.Cells[2].Text + "</a>";
            }
            //sUrl = rc.getMTypeQurl(ea.FManageTypeId);
            //sUrl += "?fbid=" + fbid + "&faid=" + faid + "&frid=" + frid + "&fmid=" + fmid + "&fly=1&fuid=" + Session["DFUserId"].ToString();
            //e.Item.Cells[2].Text = e.Item.Cells[2].Text;


            sUrl = "AppDetails.aspx?flinkid=" + faid + "&fbid=" + fbid;

            e.Item.Cells[e.Item.Cells.Count - 10].Text = "<a href='" + sUrl + "' class='link5' target='_blank'>相关表格维护</a>";
            CheckBox box = (CheckBox)e.Item.Cells[0].Controls[1];
            box.Checked = false;
            string FMeasure = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FMeasure"));
            if (FMeasure == "0")
            {
                box.Checked = true;
                box.ToolTip = "未接件";
            }
            if (FMeasure == "4")
            {
                box.Checked = true;
                box.ToolTip = "被打回";
            }
            if (FMeasure == "5" && EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult")) == "1")
            {
                box.ToolTip = "准予受理";
                box.Checked = false;
            }
            if (EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState")) == "6" && EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFResult")) == "3")
            {
                box.ToolTip = "不准予受理";
                box.Checked = false;
            }
            if (EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState")) == "2")
            {
                box.ToolTip = "打回企业,重新上报";
                box.Checked = false;
            }

            if (e.Item.Cells[15].Text == "1900-01-01")
            {
                e.Item.Cells[15].Text = "";
            }
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
       // this.AcceptApp();
        string userID = Session["DFUSerId"].ToString();
        int userLevel = int.Parse(Session["DFLevel"].ToString());
        string idea = t_FSeeOpinion.Text;
        if(wf.Accept(ferid, userID,userLevel, idea))
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件成功');", true);      
        }else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('接件失败');", true);     
        }
        
        //ShowInfo();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
       // this.BatchApp();
        string userID = Session["DFUSerId"].ToString();
        string id = Session["DFId"].ToString();
        string roleId = Session["DFRoleId"].ToString();
        string idea = t_FSeeOpinion.Text;
        if (wf.Rollback(fLinkId, userID, roleId, id, idea))
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位成功');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('回退到建设单位失败');", true);
        }
        //ShowInfo();
    }
    protected void btnEndApp_Click(object sender, EventArgs e)
    {
       // this.BatchEndApp();
        string userID = Session["DFUSerId"].ToString();
        string idea = t_FSeeOpinion.Text;
        if (wf.BackAndEnd(fLinkId, userID, idea))
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('不予受理成功');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('不予受理失败');", true);
        }
        //ShowInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        try
        {
            EgovaDB db = new EgovaDB();
            string userID = Session["DFUSerId"].ToString();
            int userLevel = int.Parse(Session["DFLevel"].ToString());
            string idea = t_FSeeOpinion.Text;
            ViewState["Idea"] = t_FSeeOpinion.Text;
            CF_Sys_User user = db.CF_Sys_User.Where(t => t.FID == userID).FirstOrDefault();
            CF_App_ProcessRecord er = db.CF_App_ProcessRecord.Where(t => t.FID == ferid).FirstOrDefault();
            //CF_App_ProcessRecord
            er.FAppPerson = user.FName;
            er.FCompany = user.FCompany;
            er.FFunction = user.FFunction;
            er.FIdea = idea;
            er.FLevel = userLevel;
            er.FUserId = userID;

            db.SubmitChanges();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存成功');", true);
       
        }
        catch (Exception ee)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('保存失败');", true);
       
        }
        
    }
    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.RegisterStartupScript(Guid.NewGuid().ToString(), "<script>window.returnValue='1';window.close();</script>");
    }

    //一层列表
    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string FId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
            string FFileName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FFileName"));
            IQueryable<TC_QA_File> AppFile = DataBinder.Eval(e.Item.DataItem, "AppFile") as IQueryable<TC_QA_File>;
            if (AppFile != null && AppFile.Count() > 0)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = AppFile.Count().ToString();
                Repeater rep_File = (Repeater)e.Item.FindControl("rep_File");
                rep_File.DataSource = AppFile;
                rep_File.DataBind();
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }

}
