using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class KcsjSgt_Statistics_PrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }

    //显示
    private void showInfo()
    {
        //只查确认过的以下业务
        //项目见证确认	28001

        string FBaseinfoId = CurrentEntUser.EntId;
        //确认过的项目
        var v = (from t in db.CF_App_List.Where(a => a.FToBaseinfoId == FBaseinfoId && a.FState > 1 && (a.FManageTypeId == 28001))
                 group t by t.FPrjId into g
                 select new
                 {
                     FID = g.Key,
                     //工程名
                     FPrjName = (from p in db.CF_Prj_BaseInfo
                                 where p.FId == g.Key
                                 select p.FPrjName).FirstOrDefault(),
                     //项目勘察合同备案
                     a = (from a in g
                          where a.FPrjId == g.Key && a.FManageTypeId == 28001
                          && a.FCreateTime == g.Where(g3 => g3.FManageTypeId == 28001).Min(g3 => g3.FCreateTime)
                          select new { a.FBaseName, a.FReportDate, a.FState }).FirstOrDefault(),
                     //看有没有变更过
                     BG = (db.CF_Prj_BaseInfo.Count(b => b.FLinkId == g.Key) > 1)
                 });

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));

            //建设单位
            e.Item.Cells[2].Text = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "a.FBaseName"));

            //首次提交合同备案时间
            e.Item.Cells[3].Text = EConvert.ToShortDateString(DataBinder.Eval(e.Item.DataItem, "a.FReportDate"));

            //首次确认结果
            int FState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "a.FState"));
            e.Item.Cells[4].Text = (FState == 2 ? "<tt>退回，补充材料</tt>" : FState == 6 ? "<font color='green'>已确认</font>" : FState == 7 ? "<font color='red'>不予接受</font>" : "");

            //看有没有变更过，
            bool BG = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "BG"));
            if (BG)
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"该项目有过变更！\"/>";

        }
    }
}
