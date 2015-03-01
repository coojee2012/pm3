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

public partial class OA_Bulletin_zList : System.Web.UI.Page
{
    bool temp = true;
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            showInfo();
        }
    }


    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_OA_Bulletin where FIsDeleted=0 and FState=1 order by FCreateTime desc ");

        Pager1.controltopage = "BulList";
        Pager1.className = "dbOA";
        Pager1.sql = sb.ToString();
        Pager1.pagecount = 15;
        Pager1.controltype = "GridView";
        Pager1.dataBind();
        sb.Remove(0, sb.Length);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    protected void BulList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FTitle = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTitle"));

            e.Row.Cells[1].Text = "<a href=\"javascript:showAddWindow('zLook.aspx?fid=" + FID + "',730,500);\">" + FTitle + "</a>";

        }

    }
}
