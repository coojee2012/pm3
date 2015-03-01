using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Approve;
using Approve.RuleCenter;
using Approve.Common;

public partial class ACZJ_main_selfAssessmentDetail : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); showInfo(); }
            if (Request["gcbh"] != null && !string.IsNullOrEmpty(Request["gcbh"]))
            { t_FID.Value = Request["gcbh"].ToString(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_selfAssessment where fid='" + t_FID.Value + "' ";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
            t_DJ.SelectedIndex = t_DJ.Items.IndexOf(t_DJ.Items.FindByValue(dt.Rows[0]["DJ"].ToString().Trim()));
        }
        if (t_ZT.Value == "5") { readOnly(); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_JD.Enabled = false;
        t_DJ.Enabled = false; t_BZ.Enabled = false; t_RQ.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "";
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql += " update YW_selfAssessment set JD='" + t_JD.Text + "',DJ='" + t_DJ.Text + "',RQ='" + t_RQ.Text
                + "',BZ='" + t_BZ.Text + "'WHERE FID='" + t_FID.Value + "' ";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql += " INSERT YW_selfAssessment (FID, JD, DJ, RQ, GCBH, BZ, ZT, creatTime) values ('" + t_FID.Value
                + "','" + t_JD.Text + "','" + t_DJ.Text + "','" + t_RQ.Text + "','" + t_GCBH.Value + "','"
                + t_BZ.Text + "',0,getdate() )";
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    protected void btnUP_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = " update YW_selfAssessment set ZT=5 WHERE FID='" + t_FID.Value + "' ";
        if (sh.PExcute(sql))
        { readOnly(); tool.showMessageAndRunFunction("上报成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("上报失败"); }
    }
}