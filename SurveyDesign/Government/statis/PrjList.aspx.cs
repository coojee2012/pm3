using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;

public partial class Government_statis_PrjList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            conBind();
            showInfo();
        }
    }

    private void conBind()
    {
        //工程类别
        t_FType.DataSource = db.getDicList("20001");
        t_FType.DataTextField = "FName";
        t_FType.DataValueField = "FNumber";
        t_FType.DataBind();
        t_FType.Items.Insert(0, new ListItem("全部", ""));


        //工程所属区域
        govd_FRegistDeptId.fNumber = EConvert.ToString(Session["DFID"]);
        govd_FRegistDeptId.Dis(EConvert.ToInt(Session["DFLevel"]));
    }

    //显示
    private void showInfo()
    {
        //初步设计  --	初步设计成果提交	293
        //初步设计文件审查 -- 初步设计文件审查申报	294
        //项目勘察  --  勘察成果移交	284、见证报告备案	28003
        //勘察文件审查  --  勘察文件审查备案	290
        //施工图设计文件编制  --  施工图设计文件编制成果移交	298
        //施工图设计文件审查  --  施工图设计文件备案	305	

        var v = from t in db.CF_Prj_BaseInfo
                where db.CF_App_List.Count(a => a.FState > 1 && a.FPrjId == t.FId) > 0 && t.FIsBG != 1
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    t.FPrjName,
                    t.FType,
                    t.FAddressDept,
                    jsdw = db.CF_Ent_BaseInfo.Where(a => a.FId == t.FBaseinfoId).Select(a => a.FName).FirstOrDefault(),
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


        //工程所属区域
        if (!string.IsNullOrEmpty(govd_FRegistDeptId.FNumber))
        {
            v = v.Where(t => t.FAddressDept.Contains(govd_FRegistDeptId.FNumber));
        }
        //工程类别
        if (!string.IsNullOrEmpty(t_FType.SelectedValue))
        {
            v = v.Where(t => t.FType.ToString() == t_FType.SelectedValue);
        }
        //工程名称
        if (!string.IsNullOrEmpty(t_prjName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(t_prjName.Text));
        }
        //建设单位
        if (!string.IsNullOrEmpty(t_jsBaseName.Text))
        {
            v = v.Where(t => t.jsdw.Contains(t_jsBaseName.Text));
        }

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



            //地区
            e.Item.Cells[3].Text = db.getDeptName(DataBinder.Eval(e.Item.DataItem, "FAddressDept"));
            //工程类别
            e.Item.Cells[4].Text = db.getDicName(DataBinder.Eval(e.Item.DataItem, "FType"));


            string y = "<img src=\"../../image/s_yes.gif\" style=\"cursor:pointer;\" title=\"已办理完成\"/>";
            string n = "<img src=\"../../image/s_no.gif\" style=\"cursor:pointer;\" title=\"未办理或未办理完\"/>";

            List<int> a = DataBinder.Eval(e.Item.DataItem, "app") as List<int>;


            //初步设计
            e.Item.Cells[5].Text = (a != null && a.Contains(293)) ? y : n;
            //初步设计文件审查
            e.Item.Cells[6].Text = (a != null && a.Contains(294)) ? y : n;
            //项目勘察
            e.Item.Cells[7].Text = (a != null && a.Contains(284) && a.Contains(28003)) ? y : n;
            //勘察文件审查
            e.Item.Cells[8].Text = (a != null && a.Contains(290)) ? y : n;
            //施工图设计文件编制
            e.Item.Cells[9].Text = (a != null && a.Contains(298)) ? y : n;
            //施工图设计文件审查
            e.Item.Cells[10].Text = (a != null && a.Contains(305)) ? y : n;

            //看有没有变更过，
            bool BG = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "BG"));
            if (BG)
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"该项目有过变更！\"/>";

        }
    }

    //查询按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
