using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using System.Data.SqlClient;
using System.Threading;
using ProjectData;
using System.Text;
using System.Collections;


public partial class GFEnt_ApplyEnt_addEmp : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEntInfo();
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_RYID.Value = Request["fid"].ToString(); ;showInfo(); }
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
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from YW_GF_ZYWCR where RYID='" + t_RYID.Value + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_RYID.Value))
        {
            sql = "update YW_GF_ZYWCR set XM='" + t_XM.Text.Trim() + "',SJ='" + t_SJ.Text.Trim()
                + "',GZDW='" + t_GZDW.Text + "',ZW='" + t_ZW.Text + "' where RYID='" + t_RYID.Value + "'";
        }
        else
        {
            t_RYID.Value = Guid.NewGuid().ToString();
            sql = String.Format(@"insert YW_GF_ZYWCR (RYID,YWBM,XM,ZW,GZDW,SJ,FTime,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted) values (
                      '" + t_RYID.Value + "','" + t_YWBM.Value + "','" + t_XM.Text.Trim() + "','"
                         + t_ZW.Text.Trim() + "','" + t_GZDW.Text.Trim() + "','" + t_SJ.Text.Trim() + "',getdate(),'"
                         + t_FBaseInfoId.Value + "','" + t_FSystemId.Value + "',getdate(),0)");
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_XM.Enabled = false;
        t_SJ.Enabled = false; t_GZDW.Enabled = false;
        t_ZW.Enabled = false;
    }
}