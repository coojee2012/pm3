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

public partial class Share_WebSide_prjList : System.Web.UI.Page
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
        int FCol = EConvert.ToInt(Request.QueryString["FCol"]);




        //栏目名称
        lit_Title.Text = db.CF_Sys_Tree.Where(t => t.FNumber == EConvert.ToString(FCol)).Select(t => t.FName).FirstOrDefault();

        int fmanagetypeid = 0;

        switch (FCol)
        {
            case 60804:
                fmanagetypeid = 296;
                break;
            case 60805:
                fmanagetypeid = 280;
                break;
            case 60806:
                fmanagetypeid = 300;
                break;
        }


        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FId equals a.FLinkId
                 where p.FType == fmanagetypeid && a.FManageTypeId == fmanagetypeid && a.FIsDeleted == false && a.FState == 6
                 orderby p.FCreateTime descending
                 select new
                 {
                     a.FId,
                     p.FPrjName,
                     a.FManageTypeId
                 });



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

    protected void rep_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string type = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));
        string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
        string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
        Literal lit = e.Item.FindControl("prjName") as Literal;
        string url = "";
        switch (type)
        {
            case "296":
                url = "Article1.aspx";
                break;
            case "280":
                url = "Article2.aspx";
                break;
            case "300":
                url = "Article3.aspx";
                break;
        }
        lit.Text = "<a href='" + url + "?fid=" + fid + "' style=\"width: 495px;\" target=\"_blank\">" + FPrjName + "</a>";

    }
}
