using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class JSDW_appmain_PrjList : System.Web.UI.Page
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
        //初步设计  --	初步设计成果提交	293
        //初步设计文件审查 -- 初步设计文件审查申报	294
        //项目勘察  --  勘察成果移交	284、见证报告备案	28003
        //勘察文件审查  --  勘察文件审查备案	289
        //施工图设计文件编制  --  施工图设计文件编制成果移交	298
        //施工图设计文件审查  --  施工图设计文件备案	305	


        string FBaseinfoId = CurrentEntUser.EntId;
        var v = from t in db.CF_Prj_BaseInfo
                where t.FBaseinfoId == FBaseinfoId && t.FIsBG != 1
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    t.FPrjName,
                    app = (from a in db.CF_App_List
                           where a.FPrjId == t.FId && a.FState == 6
                           && (a.FManageTypeId == 293 //初步设计  --	初步设计成果提交	293
                            || a.FManageTypeId == 294 //初步设计文件审查 -- 初步设计文件审查申报	294
                            || a.FManageTypeId == 284 //项目勘察  --  勘察成果移交	284、见证报告备案	28003
                            || a.FManageTypeId == 28003
                            || a.FManageTypeId == 290 //勘察文件审查  --  勘察文件审查备案	289
                            || a.FManageTypeId == 298 //施工图设计文件编制  --  施工图设计文件编制成果移交	298
                            || a.FManageTypeId == 305) //施工图设计文件审查  --  施工图设计文件备案	305	
                           select a.FManageTypeId.GetValueOrDefault()).ToList(),
                    //看有没有变更过
                    BG = (db.CF_Prj_BaseInfo.Count(b => b.FLinkId == t.FLinkId) > 1)
                };

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

            string y = "<img src=\"../../image/s_yes.gif\" style=\"cursor:pointer;\" title=\"已办理完成\"/>";
            string n = "<img src=\"../../image/s_no.gif\" style=\"cursor:pointer;\" title=\"未办理或未办理完\"/>";

            List<int> a = DataBinder.Eval(e.Item.DataItem, "app") as List<int>;

            //初步设计
            e.Item.Cells[2].Text = (a != null && a.Contains(293)) ? y : n;
            //初步设计文件审查
            e.Item.Cells[3].Text = (a != null && a.Contains(294)) ? y : n;
            //项目勘察
            e.Item.Cells[4].Text = (a != null && a.Contains(284)) ? y : n;
            //勘察文件审查
            e.Item.Cells[5].Text = (a != null && a.Contains(290)) ? y : n;
            //施工图设计文件编制
            e.Item.Cells[6].Text = (a != null && a.Contains(298)) ? y : n;
            //施工图设计文件审查
            e.Item.Cells[7].Text = (a != null && a.Contains(305)) ? y : n;


            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(t => t.FPrjId == EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"))).FirstOrDefault();
            if (stop != null)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
            }

            //看有没有变更过，
            bool BG = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "BG"));
            if (BG)
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"该项目有过变更！\"/>";

        }
    }
}
