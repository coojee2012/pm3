using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using System.Linq;
using System.Data.SqlClient;
using ProjectData;

public partial class GFEnt_user_sbEnt : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["fid"] != null && !string.IsNullOrEmpty(Request["fid"]))
        {
            t_Fid.Value = Request["fid"].ToString(); bind();
        }
    }
    public void bind()
    {
        string sql = "select convert(nvarchar(10),FStime,121) FStime,convert(nvarchar(10),FEtime,121) FEtime from YW_CF_DICtime where FID='" + t_Fid.Value + "'";
        DataTable dt = sh.GetTable(sql);
        if (dt != null && dt.Rows.Count > 0)
        {
            string stime = dt.Rows[0]["FStime"].ToString();
            string etime = dt.Rows[0]["FEtime"].ToString();
            sql = string.Format(@"select l.* from CF_App_List l
                        where convert(nvarchar(10),l.FReportDate,121)>= '" + stime + "' and convert(nvarchar(10),l.FReportDate,121) <= '" + etime + "'");
            if (!string.IsNullOrEmpty(t_FEntName.Text.Trim()))
            { sql += " and FBaseName like '" + t_FEntName.Text.Trim() + "' "; }
            this.Pager1.className = "dbShare";
            this.Pager1.sql = sql;
            this.Pager1.pagecount = 20;
            this.Pager1.controltopage = "DG_List";
            this.Pager1.controltype = "DataGrid";
            this.Pager1.dataBind();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
        }
    }

}