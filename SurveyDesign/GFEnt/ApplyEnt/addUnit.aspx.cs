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

public partial class GFEnt_ApplyEnt_addUnit : System.Web.UI.Page
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
        string sql = "select * from YW_GF_ZYWCDW where FID='" + Request["fid"] + "'";
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
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql = "update YW_GF_ZYWCDW set FName='" + t_FName.Text.Trim() + "',FAddress='" + t_FAddress.Text.Trim()
                + "',FPostcode='" + t_FPostcode.Text.Trim() + "',FLinkMan='" + t_FLinkMan.Text.Trim() + "',FTel='"
                + t_FTel.Text.Trim() + "',FMobile='" + t_FMobile.Text.Trim() + "',FRemark='" + t_FRemark.Text.Trim()
                + "' where FID='" + t_FID.Value + "'";
        }
        else
        {
            sql = string.Format(@"insert YW_GF_ZYWCDW (FID,FName,FAddress,FPostcode,FLinkMan,FTel
                            ,FMobile,FRemark,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted,FTime,YWBM)
                values 
                (newid(),'" + t_FName.Text.Trim() + "','" + t_FAddress.Text + "','" + t_FPostcode.Text + "','"
                    + t_FLinkMan.Text + "','" + t_FTel.Text.Trim() + "','" + t_FMobile.Text.Trim()
                    + "','" + t_FRemark.Text + "','" + t_FBaseInfoId.Value + "','" + t_FSystemId.Value
                    + "',getdate(),0,getdate(),'" + t_YWBM.Value + "' )");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }

    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_FName.Enabled = false;
        t_FAddress.Enabled = false; t_FPostcode.Enabled = false;
        t_FLinkMan.Enabled = false; t_FTel.Enabled = false;
        t_FMobile.Enabled = false; t_FRemark.Enabled = false;
    }
}