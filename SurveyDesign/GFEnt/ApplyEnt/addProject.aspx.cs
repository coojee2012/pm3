using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve.RuleCenter;
using Tools;

public partial class GFEnt_ApplyEnt_addProject : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEntInfo();
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"].ToString()))
                showInfo();
        }
    }
    public void ShowEntInfo()
    {
        string sql = "select * from CF_Sys_User where FBaseInfoId='" + CurrentEntUser.EntId + "' ";
        DataTable dt = rc.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            t_FBaseInfoId.Value = CurrentEntUser.EntId;
            t_FSystemId.Value = dt.Rows[0]["FSystemId"].ToString();
        }
        t_YWBM.Value = Session["FAppId"].ToString();
        if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
        { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        t_YWBM.Value = Session["FAppId"].ToString();
        string sql = "select * from YW_GF_YYGC where GCID='" + Request["fid"] + "'";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_GCID.Value))
        {
            sql = "update YW_GF_YYGC set GCMC='" + t_GCMC.Text.Trim() + "',SZD='" + t_SZD.Text.Trim()
                + "',KSSJ='" + t_KSSJ.Text + "',JSSJ='" + t_JSSJ.Text + "' where GCID='" + t_GCID.Value + "'";
        }
        else
        {
            t_GCID.Value = Guid.NewGuid().ToString();
            sql = string.Format(@"insert YW_GF_YYGC (GCID,GCMC,SZD,KSSJ,JSSJ,YWBM,FTime,FBaseInfoId
                    ,FSystemId,FCreateTime,FIsDeleted)
                    values 
                    ('" + t_GCID.Value + "','" + t_GCMC.Text.Trim() + "','" + t_SZD.Text.Trim() + "','"
                        + t_KSSJ.Text + "','" + t_JSSJ.Text + "','" + t_YWBM.Value + "',getdate(),'"
                        + t_FBaseInfoId.Value + "','" + t_FSystemId.Value + "',getdate(),0)");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_GCMC.Enabled = false;
        t_SZD.Enabled = false; t_KSSJ.Enabled = false;
        t_JSSJ.Enabled = false;
    }
}