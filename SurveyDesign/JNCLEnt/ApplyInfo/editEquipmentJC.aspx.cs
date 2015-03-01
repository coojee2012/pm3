using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Approve.Common;
using ProjectBLL;
using System.Data;
using Approve.RuleCenter;
using System.Text;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class JNCLEnt_mangeInfo_editEquipmentJC : System.Web.UI.Page
{
    RCenter rc = new RCenter(); Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnSave.Attributes.Add("onclick", "if(checkInfo()){return true;}else{return false;}");
            if (Session["FAppId"] != null && !string.IsNullOrEmpty(Session["FAppId"].ToString()))
            { t_fappid.Value = Session["FAppId"].ToString(); }
            if (Session["FIsApprove"] != null && !string.IsNullOrEmpty(Session["FIsApprove"].ToString()))
            { if (Session["FIsApprove"].ToString() == "1") { readOnly(); } }
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_JN_AppEquipment where fid='" + t_FID.Value + "' ";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = ""; pageTool tool = new pageTool(this.Page, "t_");
        if (!string.IsNullOrEmpty(t_FID.Value))
        {
            sql += " update YW_JN_AppEquipment set editTime=getdate(), SBMC='" + t_SBMC.Text + "', XH='" + t_XH.Text + "', GL='" + t_GL.Text
                + "', NU='" + t_NU.Text + "', DW='" + t_DW.Text + "', SFZY='" + t_SFZY.SelectedValue + "', YZ='" + t_YZ.Text + "', JZ='" + t_JZ.Text
                + "', ZT='" + t_ZT.SelectedValue + "' where fid='" + t_FID.Value + "' ";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql += " insert YW_JN_AppEquipment (FID, Fappid, fbaseid, ftime, editTime, SBMC, XH, GL, NU, DW, SFZY, YZ, JZ, ZT,type) values ('" + t_FID.Value
                + "', '"+t_fappid.Value+"', '" + CurrentEntUser.EntId + "', getdate(), getdate(), '" + t_SBMC.Text + "', '" + t_XH.Text
                + "', '" + t_GL.Text + "', '" + t_NU.Text + "', '" + t_DW.Text + "', '" + t_SFZY.SelectedValue + "', '" + t_YZ.Text
                + "', '" + t_JZ.Text + "','" + t_ZT.SelectedValue + "','1') ";
        }
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
    public void readOnly()
    {
        btnSave.Enabled = false; t_DW.Enabled = false;
        t_GL.Enabled = false; t_JZ.Enabled = false;
        t_NU.Enabled = false; t_SBMC.Enabled = false;
        t_SFZY.Enabled = false; t_XH.Enabled = false;
        t_YZ.Enabled = false; t_ZT.Enabled = false;
    }
}