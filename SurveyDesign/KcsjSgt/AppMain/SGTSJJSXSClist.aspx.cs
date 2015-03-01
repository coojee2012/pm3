using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;

public partial class KcsjSgt_AppMain_KCJSXSClist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    int fMType = 30103;//施工图设计文件审查--技术性审查 
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
                    t.FLinkId,
                    //施工图设计文件审查合同备案(300)是否二审
                    FReportCount = db.CF_App_List.Where(li => li.FLinkId == t.FLinkId && li.FManageTypeId == 300).Select(li => li.FReportCount).FirstOrDefault()

                };
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
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
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));

            //状态办理结果
            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../ApplySGTSJJSXSC/Report.aspx?FAppId=" + FID + "\",700,680);'>";
            switch (FState)
            {
                case "0":
                case "1":
                    s = "<font color='#888888'>还未审查</font>";
                    o += "查看审查意见";
                    e.Row.Cells[4].Text = "<font color='#888888'>--</font>";
                    break;
                case "3"://修改
                    s = "<a href=\"javascript:showApproveWindow('LookIdea.aspx?FAppId=" + FID + "',500,350);\"><font color='red'>不合格</font></a>";
                    o += "查看审查详情";
                    break;
                case "6"://合格
                    s = "<font color='green'>合格</font>";
                    o += "查看审查详情";
                    break;
                case "7"://
                    s = "<a href=\"javascript:showApproveWindow('LookIdea.aspx?FAppId=" + FID + "',500,350);\"><font color='red'>补正材料</font></a>";
                    o += "查看审查详情";
                    break;
            }
            e.Row.Cells[5].Text = s;
            e.Row.Cells[6].Text = o + "</a>";


            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }

            //查询项目的变更时间   
            ProjectDB db = new ProjectDB();
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
    //列表事件
    protected void DG_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "See")
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 3)
            {
                pageTool tool = new pageTool(this);
                string FAppId = s[0];
                string FManageTypeId = s[1];
                string FIsApprove = EConvert.ToInt(s[2]) > 0 ? "1" : "0";

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = FManageTypeId;
                Session["FIsApprove"] = FIsApprove;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
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
