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

public partial class OA_Bulletin_zAppList : System.Web.UI.Page
{
    bool temp = true;
    OA oa = new OA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnDel.Attributes.Add("onclick", "return confirm('确认要删除么?');");

            showInfo();
        }
    }



    private void showInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("select * from CF_OA_Bulletin where FIsDeleted=0 order by FCreateTime desc ");

        Pager1.controltopage = "BulList";
        Pager1.className = "dbOA";
        Pager1.sql = sb.ToString();
        Pager1.pagecount = 15;
        Pager1.controltype = "GridView";
        Pager1.dataBind();
        sb.Remove(0, sb.Length);
    }

    protected void BulList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = (e.Row.RowIndex + 1 + this.Pager1.pagecount * (this.Pager1.curpage - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FTitle = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FTitle"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));

            e.Row.Cells[2].Text = "<a href=\"javascript:showAddWindow('zAdd.aspx?fid=" + FID + "',730,500);\">" + FTitle + "</a>";

            e.Row.Cells[3].Text = FState == "1" ? "是" : "否";
        }

    }

    //删除按钮 
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SortedList sl = new SortedList();
        sl.Add("CF_OA_Bulletin", "FID");
        tool.DelInfoFromGrid(BulList, sl, "dbOA");

        showInfo();
    }
    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}
