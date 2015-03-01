using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using System.Linq;
using ProjectBLL;


public partial class EvaluateEntApp_main_AppCBSJRY : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    public int fMType = 29701;//施工图设计文件人员安排 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            showInfo();
        }
    }

    //绑定选项
    private void conBind()
    {
        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));
    }

    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;

        var v = from t in db.CF_App_List
                join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                where t.FBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    t.FLinkId,
                    DataFID = d.FId,
                    d.FPrjName,
                    d.FPriItemId,
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                    t.FAppDate,
                    //施工图设计文件编制合同备案296 情况
                    app = db.CF_App_List.Where(l => t.FLinkId == l.FLinkId && l.FManageTypeId == 296).FirstOrDefault()
                };

        if (!string.IsNullOrEmpty(t_FPrjName.Text))
        {
            v = v.Where(t => t.FPrjName.Contains(t_FPrjName.Text));
        }
        if (!string.IsNullOrEmpty(t_FState.SelectedValue))
        {
            int FState = EConvert.ToInt(t_FState.SelectedValue);
            v = v.Where(t => t.FState == FState);
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);


        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            string FName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FName"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLinkId"));

            //状态办理结果
            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../applysjwjbzwt/PlanPerson.aspx?FAppId=" + FID + "\",700,680);'>";
            switch (FState)
            {
                case "0":
                case "1":
                    s = "<font color='#888888'>还未安排</font>";
                    o += "安排人员";
                    e.Row.Cells[4].Text = "<font color='#888888'>--</font>";

                    break;
                case "6":
                    s = "<font color='green'>已安排</font>";
                    o += "查看详情";
                    break;
            }
            e.Row.Cells[5].Text = s;
            e.Row.Cells[6].Text = o + "</a>";

            //判断是否已做到"	施工图设计文件编制成果移交298"，否则还可以变更
            if (FState == "6" && db.CF_App_List.Count(t => t.FLinkId == FLinkId && t.FManageTypeId == 298) == 0)
            {
                e.Row.Cells[6].Text += "<a style='margin-left:10px;' title='人员安排变更' ";
                e.Row.Cells[6].Text += "href='javascript:showAddWindow(\"../applysjwjbzwt/PlanPerson.aspx?c=1&FAppId=" + FID + "\",700,680);'>";
                e.Row.Cells[6].Text += "变更</a>";
            }

            CF_App_List app = DataBinder.Eval(e.Row.DataItem, "app") as CF_App_List;
            if (app != null)
            {
                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
                if (app.FCount > 1)
                {
                    ((Literal)e.Row.FindControl("lit_Count")).Text = "(" + app.FCount + "次";
                    //查询出不合格的意见
                    var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 30103).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplySGTSJJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看上次审查意见</a>";
                        ((Literal)e.Row.FindControl("lit_Count")).Text = "," + txt;
                    }
                    ((Literal)e.Row.FindControl("lit_Count")).Text += ")";
                }
            }

            //查询项目的变更时间 
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == FPrjId)
                .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
                .FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Row.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(st => st.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
            }


            //查出人员安排有没有变化过 
            if (db.CF_Prj_Emp.Count(t => t.FAppId == FID && (t.FIsDeleted.GetValueOrDefault() || t.FDataFrom == 1)) > 0)
            {//有过变化
                ((Literal)e.Row.FindControl("lit_TS")).Text = "<img title=\"人员安排有过变更\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
            }


            if (FState == "0" || FState == "1")//还没安排的就做此判断
            {
                //合同备案同况
                string DataFID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "DataFID"));
                var HTBA = (from dd in db.CF_Prj_Data
                            join tt in db.CF_App_List on dd.FAppId equals tt.FId
                            where dd.FPriItemId == DataFID && tt.FManageTypeId == 414
                            orderby tt.FCreateTime descending
                            select new
                            {
                                tt.FState,
                                //查询备案通过没
                                FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                            }).FirstOrDefault();
                string c = "<img title=\"合同备案未完成，无法安排\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
                if (HTBA != null)
                {
                    if (HTBA.FState != 6 || HTBA.FResultInt != 1)
                        e.Row.Cells[6].Text = c;
                }
                else
                    e.Row.Cells[6].Text = c;
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
