using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleApp;

public partial class KC_AppMain_KCCGYJlist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    int fMType = 293;//初步设计成果移交
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
                    d.FPrjName,
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                    t.FAppDate,
                    //初步设计合同备案296 情况
                    FCount = db.CF_App_List.Where(l => t.FLinkId == l.FLinkId && l.FManageTypeId == 291).Select(l => l.FCount).FirstOrDefault()

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
            string o = "<a href='javascript:showAddWindow(\"../applycbsjcgyj/ApplyBaseInfo.aspx?FAppId=" + FID + "\",700,600);'>";
            switch (FState)
            {
                case "0":
                case "1":
                    s = "<font color='#888888'>还未移交</font>";
                    o += "成果移交";
                    e.Row.Cells[4].Text = "<font color='#888888'>--</font>";

                    break;
                case "6":
                    s = "<font color='green'>已移交</font>";
                    o += "查看成果移交详情";
                    break;
            }
            e.Row.Cells[5].Text = s;
            e.Row.Cells[6].Text = o + "</a>";

            //是否二次。
            int n = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FCount"));
            if (n > 1)
            {
                ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + n + "次)");
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
