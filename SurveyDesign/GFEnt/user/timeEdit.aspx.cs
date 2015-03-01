using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using System.Data.SqlClient;
using System.Threading;
using ProjectData;

public partial class GFEnt_user_timeEdit : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = "select * from YW_CF_DICtime where FID='" + t_FID.Value + "'";
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
            sql = "update YW_CF_DICtime set FYear='" + t_FYear.Text.Trim() + "',FStime='" + t_FStime.Text
              + "',FEtime='" + t_FEtime.Text + "' where FID='" + t_FID.Value + "' ";
        }
        else
        {
            t_FID.Value = Guid.NewGuid().ToString();
            sql = "insert YW_CF_DICtime (FID,FYear,FStime,FEtime,FcreatUser) values ('" + t_FID.Value + "','" + t_FYear.Text.Trim()
                + "','" + t_FStime.Text + "','" + t_FEtime.Text + "','" + CurrentEntUser.UserId + "')";
        }
        if (rc.PExcute(sql))
        { tool.showMessageAndRunFunction("保存成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("保存失败"); }
    }
}