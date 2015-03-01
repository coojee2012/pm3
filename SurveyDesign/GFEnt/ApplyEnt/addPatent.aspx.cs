using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class GFEnt_ApplyEnt_addPatent : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEntInfo();
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"].ToString()))
                showInfo();
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
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
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        t_YWBM.Value = Session["FAppId"].ToString();
        string sql = "select * from YW_GF_ZLQK where FID='" + Request["fid"] + "'";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }

        sql = string.Format(@"select count(1) from YW_GF_FileList l 
                     inner join CF_AppPrj_FileOther f on l.FFileID=f.FID and l.FAppid=f.FAppId
                     where l.FAppid='" + t_YWBM.Value + "' and l.FType=1007");
        string cou = sh.GetSignValue(sql);
        btnUP.Attributes.Add("value", "文件上传(" + cou + ")");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql = "update YW_GF_ZLQK set ZLMC='" + t_ZLMC.Text.Trim() + "',ZLH='" + t_ZLH.Text.Trim() + "',ZLQR='"
                + t_ZLQR.Text.Trim() + "',JJNR='" + t_JJNR.Text + "',FTime=getdate() where FID='" + t_FID.Value + "'";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql = string.Format(@"insert YW_GF_ZLQK (FID,YWBM,ZLMC,ZLH,ZLQR,JJNR,FTime,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted)
                                values ('" + t_FID.Value + "','" + t_YWBM.Value + "','" + t_ZLMC.Text.Trim()
                                           + "','" + t_ZLH.Text.Trim() + "','" + t_ZLQR.Text.Trim() + "','" + t_JJNR.Text
                                           + "',getdate(),'" + t_FBaseInfoId.Value + "','" + t_FSystemId.Value + "',getdate(),0 ) ");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_ZLMC.Enabled = false;
        t_ZLH.Enabled = false; t_ZLQR.Enabled = false;
        t_JJNR.Enabled = false;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}