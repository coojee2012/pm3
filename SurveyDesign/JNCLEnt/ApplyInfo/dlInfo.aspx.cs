using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_ApplyInfo_dlInfo : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            if (Session["FAppId"] != null && !string.IsNullOrEmpty(Session["FAppId"].ToString()))
            { t_fappid.Value = Session["FAppId"].ToString(); }
            showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        t_fappid.Value = Session["FAppId"].ToString();
        string sql = "select * from YW_JN_AppDJ where FAppId='" + Session["FAppId"] + "'";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            if (t_SMSQ.Items.FindByValue(dt.Rows[0]["SMSQ"].ToString()) != null)
                t_SMSQ.SelectedValue = t_SMSQ.Items.FindByValue(dt.Rows[0]["SMSQ"].ToString()).Value;
        }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_DH.Enabled = false;
        t_FR.Enabled = false; t_MC.Enabled = false;
        t_QYDZ.Enabled = false; t_SJ.Enabled = false;
        t_SMSQ.Enabled = false; t_YYZZ.Enabled = false;
        t_ZCZJ.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = ""; pageTool tool = new pageTool(this.Page, "t_");
        if (!string.IsNullOrEmpty(t_fid.Value))
        {
            sql += string.Format(@" update YW_JN_AppDJ set MC='" + t_MC.Text + "', QYDZ='" + t_QYDZ.Text + "', YYZZ='" + t_YYZZ.Text
                + "', ZCZJ='" + t_ZCZJ.Text + "', FR='" + t_FR.Text + "', SJ='" + t_SJ.Text + "', DH='" + t_DH.Text + "', SMSQ='" + t_SMSQ.Text
                + "', editTime=getdate() where fid='" + t_fid.Value + "' ");
        }
        else
        {
            t_fid.Value = Guid.NewGuid().ToString();
            sql += string.Format(@" insert YW_JN_AppDJ (FID, MC, QYDZ, YYZZ, ZCZJ, FR, SJ, DH, SMSQ, Fappid, fbaseid
                    , ftime, editTime) vaules ('" + t_fid.Value + "', '" + t_MC.Text + "', '" + t_QYDZ.Text + "', '" + t_YYZZ.Text
                    + "', '" + t_ZCZJ.Text + "', '" + t_FR.Text + "','" + t_SJ.Text + "', DH='" + t_DH.Text + "', '" + t_SMSQ.Text + "', '"
                    + t_fappid.Value + "', '" + CurrentEntUser.EntId + "', GETDATE(), GETDATE() ) ");
        }
        if (sh.PExcute(sql))
        { tool.showMessage("保存成功"); showInfo(); }
        else
        { tool.showMessage("保存失败"); }
    }
}