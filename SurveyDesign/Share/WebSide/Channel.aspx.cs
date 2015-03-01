using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Approve.RuleCenter;
using System.Data;
using System.Data.SqlClient;
using ProjectData;

public partial class Share_WebSide_Channel : System.Web.UI.Page
{
    Share rc = new Share();

    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        string FCol = Request.QueryString["FCol"];
        //栏目名称
        lit_Title.Text = db.CF_Sys_Tree.Where(t => t.FNumber == FCol).Select(t => t.FName).FirstOrDefault();


        var v = from t in db.CF_News_Title
                join c in db.CF_News_Col on t.FID equals c.FNewsId
                where (c.FColNumber == FCol) && c.FState == 1
                orderby t.FOrder, t.FCreateTime descending
                select new { t.FName, t.FPubTime, c.FColor, t.FID, c.FColNumber, t.FOperType, t.FWebId };


        Pager1.RecordCount = v.Count();
        rep_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        rep_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

}
