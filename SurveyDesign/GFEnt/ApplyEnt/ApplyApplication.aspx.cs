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

public partial class GFEnt_ApplyEnt_ApplyApplication : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowEntInfo();
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

        pageTool tools = new pageTool(this.Page, "t_");
        sql = "select * from YW_GF_JS where YWBM='" + Session["FAppId"] + "'";
        dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tools.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql = "update YW_GF_JS set GFYYQKJYYQJ='" + t_GFYYQKJYYQJ.Text + "',FTime=getdate() where FID='" + t_FID.Value + "'";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql = string.Format(@"insert YW_GF_JS (FID,GFYYQKJYYQJ,FBaseInfoId,FSystemId,FCreateTime,FIsDeleted,FTime)
                    values ('" + t_FID.Value + "','" + t_GFYYQKJYYQJ.Text + "','" + t_FBaseInfoId.Value + "','"
                                       + t_FSystemId.Value + "',getdate(),0,getdate())");
        }
        if (sh.PExcute(sql))
        { tool.showMessage("保存成功"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_GFYYQKJYYQJ.Enabled = false;
    }
}