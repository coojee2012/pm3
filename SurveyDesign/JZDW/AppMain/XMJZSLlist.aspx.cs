using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleCenter;

public partial class JZDW_AppMain_XMJZSLlist : System.Web.UI.Page
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
                where t.FToBaseinfoId == FBaseinfoID && t.FState >= 1 && t.FManageTypeId == 28001
                orderby t.FTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    d.FPrjName,
                    d.FPriItemId,
                    t.FName,
                    t.FCount,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    t.FBaseName,
                    t.FAppDate,
                    t.FLinkId,
                    app = db.CF_App_List.Where(a => a.FLinkId == t.FLinkId && a.FManageTypeId == 280).FirstOrDefault()
                };
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));

            string ReportServer = db.getSysObjectContent("_ReportServer");
            if (string.IsNullOrEmpty(ReportServer))
            {
                ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
            }

            //状态办理结果
            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../ApplyKCXMWT/Report.aspx?FAppId=" + FID + "\",700,480);'>";
            string t = "";
            switch (FState)
            {
                case "1":
                    s = "<font color='#888888'>还未确认</font>";
                    e.Row.Cells[4].Text = "<font color='#888888'>--</font>";
                    o += "确认合同";
                    t += "<font color='#888888'>还未确认</font>";
                    break;
                case "2":
                    s = "<font color='red'>已退回</font>";
                    o += "查看确认详情";
                    t += "<font color='#888888'>已退回</font>";
                    break;
                case "6":
                    s = "<font color='green'>已确认</font>";
                    o += "查看确认详情";
                    t += "<a href='" + ReportServer + "SLD-XMJZ.cpt&FAppId=" + FID + "' target='_blank'>打印合同确认单</a>";
                    break;
                case "7":
                    s = "<font color='red'>不予接受</font>";
                    o += "查看确认详情";
                    t += "<font color='#888888'>不予接受</font>";
                    break;
            }
            CF_App_List app = DataBinder.Eval(e.Row.DataItem, "app") as CF_App_List;
            if (app != null)
            {
                if (app.FState == 2)
                {
                    s += "</br><font color='red'>（已被勘察单位退回，业务终止）</font>";
                }
                else if (app.FState == 7)
                {
                    s += "</br><font color='red'>（合同备案的勘察单位不予接受，业务终止）</font>";
                }
            }

            e.Row.Cells[5].Text = s;
            e.Row.Cells[6].Text = o + "</a>";
            e.Row.Cells[7].Text = t;


            //是否二次。
            int n = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FCount"));
            string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
            if (n > 1)
            {
                //查询出不合格的意见（从勘查文件审查业务的技术性审查28803中查）
                var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                if (v != null)
                {
                    string txt = "<a style=\"text-decoration:underline;\" ";
                    txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                    txt += "查看审图机构意见</a>";
                    ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + n + "次," + txt + ")");
                }
            }


            //查询项目的变更时间
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
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
