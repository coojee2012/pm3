using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_AppMain_gfzsEdit : System.Web.UI.Page
{
    Share sh = new Share(); RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"].ToString(); ;showInfo(); }
            if (Request["isend"] != null && !string.IsNullOrEmpty(Request["isend"]))
            {
                if (Request["isend"] == "-1") readOnly();
            }
        }
    }
    public void showInfo()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from YW_GF_ZS where FID='" + t_FID.Value + "'");
        DataTable dt = sh.GetTable(sb.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }
    public void readOnly()
    {
        t_FNu.Enabled = false; t_WH.Enabled = false;
        t_Fpztime.Enabled = false; t_FDep.Enabled = false;
        t_DW.Enabled = false; t_RY.Enabled = false; btnSave.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = string.Format(@" update YW_GF_ZS set fnu='" + t_FNu.Text.Trim() + "',wh='" + t_WH.Text.Trim()
            + "',fpztime='" + t_Fpztime.Text + "',fdep='" + t_FDep.Text.Trim() + "',dw='" + t_DW.Text.Trim()
            + "',ry='" + t_RY.Text.Trim() + "' where fid='" + t_FID.Value + "' ");
        if (sh.PExcute(sql))
        { tool.showMessageAndRunFunction("修改成功", "window.returnValue='1';"); }
        else
        { tool.showMessage("修改失败"); }
    }
}