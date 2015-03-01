using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using System.Data;
using Approve.RuleCenter;

public partial class audit_XM_XMdetailBottom : System.Web.UI.Page
{
    Share sh = new Share();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageTool tool = new pageTool(this.Page, "t_");
            string sql = "select * from JKC_V_viewDetail where XMBH='" + Session["XMBH"] + "'";
            DataTable dt = sh.GetTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                tool.fillPageControl(dt.Rows[0]);
            }
            dt = sh.GetTable("select * from JKC_V_viewCJDW where XMBH='" + Session["XMBH"] + "'");
            DG_List.DataSource = dt; DG_List.DataBind();
        }
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        { e.Item.Cells[0].Text = (e.Item.ItemIndex + 1).ToString(); }
    }
}