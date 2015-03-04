using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using Approve.RuleCenter;
using EgovaBLL;
using ProjectBLL;
using Tools;

public partial class Government_AppWY_SHAuditInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
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
        if (Request["fManagetypeid"] != null && !string.IsNullOrEmpty(Request["fManagetypeid"]))
        {
            hidFManageTypeId.Value = Request["fManagetypeid"].ToString();
        }
        init();
        if (!IsPostBack)
        {
            isZG();
        }
    }
    /// <summary>
    /// 初始化界面
    /// </summary>
    private void init()
    {
        BindXMBaseInfo();
        bindAuditInfo();
    }

    private void BindXMBaseInfo()
    {
        DataTable dt = new DataTable();
        string strsql = "select j.XMMC,l.FID,l.FManageTypeId from CF_App_ProcessInstance p,CF_App_List l,YW_WY_XM_JBXX j " +
                        "where p.FLinkId=l.FId and j.FAppID=l.FId and p.FID='" + t_fProcessInstanceID.Value + "'";
        dt = rc.GetTable(strsql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_ProjectName.Text = dt.Rows[0]["XMMC"].ToString();
            switch (dt.Rows[0]["FManageTypeId"].ToString())
            {
                case "14401":
                    t_Fmanagetype.Text = "项目在管申请";
                    break;
                case "14402":
                    t_Fmanagetype.Text = "项目失去申请";
                    break;
                case "14403":
                    t_Fmanagetype.Text = "项目变更申请";
                    break;
                case "14404":
                    t_Fmanagetype.Text = "项目合同备案";
                    break;
                case "14405":
                    t_Fmanagetype.Text = "项目业委会备案";
                    break;
                case "14406":
                    t_Fmanagetype.Text = "项目财务年报";
                    break;
                default: break;
            }
            Fappid.Value = dt.Rows[0]["FID"].ToString();
            //hidFManageTypeId.Value = dt.Rows[0]["FManageTypeId"].ToString();
        }
    }
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

        }
    }
    protected void btnUPCS_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        if (dResult.SelectedValue == "1")
        {
            try
            {
                if (WFApp.ValidateCanDo(t_fProcessRecordID.Value))
                {
                    string dfUserId = this.Session["DFUserId"].ToString();
                    dResult.SelectedValue = "1";//接件操作强制选中同意项
                    //信息归档
                    //WYApp.FileInfo(Fappid.Value, hidFManageTypeId.Value, dfUserId);
                    WFApp.ReportProcess(t_fLinkId.Value, t_fProcessInstanceID.Value, t_fProcessRecordID.Value,
                        dfUserId,
                        t_FAppIdea.Text, dResult.SelectedValue.Trim(), t_FAppPerson.Text,
                        t_FAppPersonUnit.Text, t_FAppPersonJob.Text, t_FAppDate.Text);

                    //DisableButton();

                    string sql = "update CF_App_List set FState=6 where FId='" + Fappid.Value + "'";
                    rc.PExcute(sql);
                    tool.showMessageAndRunFunction("办结成功！", "window.returnValue = 'ok';window.close()");
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行接件操作');", true);
                    tool.showMessage("该条案卷已经进行了处理，不能再进行接件操作！");
                }

            }
            catch (Exception ee)
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('办结失败');", true);
                tool.showMessage("办结失败！");
            }
        }
        else
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
                    //DisableButton();
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作成功');", true);

                    tool.showMessageAndRunFunction("办结成功！", "window.returnValue = 'ok';window.close();");
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('该条案卷已经进行了处理，不能再进行该操作');", true);
                    tool.showMessage("该条案卷已经进行了处理！");
                }

            }
            catch (Exception ee)
            {
                //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "js", "alert('操作失败');", true);
                tool.showMessage("操作失败！");
            }
        }
    }
    #region 标记是否在管

    private void isZG()
    {
        pageTool tool = new pageTool(this.Page);
        string fbiid = "";
        string sql = "select c.FBaseinfoId,c.FName,j.XMBH from CF_App_List c,YW_WY_XM_JBXX j where c.FId=j.FAppID and c.FId='" + Fappid.Value + "'";
        DataTable dt = new DataTable();
        dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            fbiid = rc.GetSignValue("select FBaseInfoId from WY_XM_JBXX where XMBH='" + dt.Rows[0]["XMBH"] + "'");
            if (!string.IsNullOrEmpty(fbiid))
            {
                if (dt.Rows[0]["FBaseInfoId"].ToString() != fbiid)
                {
                    tool.showMessage("该项目已经被另外的企业在管。");
                }
            }
        }
    }

    #endregion
}