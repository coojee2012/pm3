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
        //勘察文件审查合同备案 287  
        //施工图设计文件审查合同备案 300

        string FBaseinfoId = CurrentEntUser.EntId;
        //确认过的项目
        var v = (from t in db.CF_App_List.Where(a => a.FToBaseinfoId == FBaseinfoId && a.FState > 1
                                            && (a.FManageTypeId == 287 || a.FManageTypeId == 300))
                 group t by t.FPrjId into g
                 select new
                 {
                     FID = g.Key,
                     //工程名
                     FPrjName = (from p in db.CF_Prj_BaseInfo
                                 where p.FId == g.Key
                                 select p.FPrjName).FirstOrDefault(),
                     //勘察文件审查合同备案
                     kc = (from a in g
                           where a.FPrjId == g.Key && a.FManageTypeId == 287
                           && a.FCreateTime == g.Where(g3 => g3.FManageTypeId == 287).Min(g3 => g3.FCreateTime)
                           select a).FirstOrDefault(),
                     //施工图设计文件审查合同备案
                     sj = (from a in g
                           where a.FPrjId == g.Key && a.FManageTypeId == 300
                           && a.FCreateTime == g.Where(g3 => g3.FManageTypeId == 300).Min(g3 => g3.FCreateTime)
                           select a).FirstOrDefault(),
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
            string jsdw = "";

            //勘察文件审查合同备案
            var kc = DataBinder.Eval(e.Item.DataItem, "kc") as CF_App_List;
            string s = "";
            if (kc != null)
            {
                jsdw = kc.FBaseName;
                s += "首次提交合同备案时间：" + kc.FReportDate.GetValueOrDefault().ToShortDateString();
                s += "<br/>首次确认结果：" + (kc.FState == 2 ? "<tt>退回，补充材料</tt>" : kc.FState == 6 ? "<font color='green'>已确认</font>" : kc.FState == 7 ? "<font color='red'>不予接受</font>" : "");
                //s += "<br/>审查情况：<img src=\"../../image/arrow02.gif\" /> <a href='javascript:showAddWindow(\"all.aspx?FID=" + FPrjId + "\",900,700);'>查看详情</a>";
            }
            else
            {
                s = "<font color='#888888'>未办理或不是本企业办理</font>";
            }
            e.Item.Cells[3].Text = s;

            //施工图设计文件审查合同备案
            var sj = DataBinder.Eval(e.Item.DataItem, "sj") as CF_App_List;
            s = "";
            if (sj != null)
            {
                jsdw = sj.FBaseName;
                s += "首次提交合同备案时间：" + sj.FReportDate.GetValueOrDefault().ToShortDateString();
                s += "<br/>首次确认结果：" + (sj.FState == 2 ? "<tt>退回，补充材料</tt>" : sj.FState == 6 ? "<font color='green'>已确认</font>" : sj.FState == 7 ? "<font color='red'>不予接受</font>" : "");
                //s += "<br/>审查情况：<img src=\"../../image/arrow02.gif\" /> <a href='javascript:showAddWindow(\"all.aspx?FID=" + FPrjId + "\",900,700);'>查看详情</a>";
            }
            else
            {
                s = "<font color='#888888'>未办理或不是本企业办理</font>";
            }
            e.Item.Cells[4].Text = s;


            //建设单位
            e.Item.Cells[2].Text = jsdw;

            //看有没有变更过，
            bool BG = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "BG"));
            if (BG)
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"该项目有过变更！\"/>";

        }
    }
}
