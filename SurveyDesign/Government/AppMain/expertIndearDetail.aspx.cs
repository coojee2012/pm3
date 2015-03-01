using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_AppMain_expertIndearDetail : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
            { t_FID.Value = Request["fid"]; bind(); }
            if (Request["read"] != null && Request["read"] == "0")
            { t_Ftime.Enabled = false; t_Fresult.Enabled = false; }

        }
    }
    private void bind()
    {
        pageTool tool = new pageTool(this.Page, "t_");
        string sql = string.Format(@"select e.Fresult,CONVERT(nvarchar(10),e.Ftime,121) Ftime,ps.ExpertName
                from YW_GF_Expert e
                left join LINKER_95.dbCenterSC.dbo.CF_Pro_PsExpertInfo ps on ps.psid=e.psid");
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            tool.fillPageControl(dt.Rows[0]);
        }
    }

}