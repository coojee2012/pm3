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
using Approve.RuleCenter;

public partial class KcsjSgt_AppMain_KCWJBAlist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    int fMType = 305;//施工图文件审查备案
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
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    t.FName,
                    FCreateTime = t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    FReportDate = t.FwriteDate,
                    t.FUpDeptId,
                    t.FYear,
                    FBaseName = db.CF_App_List.Where(a => a.FLinkId == d.FId && a.FManageTypeId == 300).Select(a => a.FBaseName).FirstOrDefault(),
                    t.FAppDate,
                    APPFReportCount = t.FReportCount,
                    FCertiNo = db.CF_Prj_Certi.Where(c => c.FProjectId == t.FPrjId && c.FAppId == t.FId && c.FCertiTypeId == fMType).Select(c => c.FCertiNo).FirstOrDefault(),
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
    RApp ra = new RApp();
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

            //状态办理结果
            string s = "";
            switch (FState)
            {
                case "0":
                    s = "<font color='#888888'>未上报</font>";
                    e.Row.Cells[8].Text = "--";
                    break;
                case "1":
                    s = "<font color='blue'>已上报</font>";
                    if (ra.isBeginApp(FID))
                    {
                        s = "<font color='blue'>正在审批</font>";
                        e.Row.Cells[8].Text = "--";
                    }
                    else
                    {
                        LinkButton lb = e.Row.Cells[8].Controls[1] as LinkButton;
                        lb.Text = "撤消上报";
                        lb.Attributes.Add("onclick", "return confirm('确认要撤消上报吗？');");
                    }
                    break;
                case "2":
                    s = "<a href='javascript:void(0)' onclick=\"showAddWindow('ShowAppInfo.aspx?fid=" + FID + "',520,400)\"><font color='red'>已退回</font></a>";
                    e.Row.Cells[8].Text = "--";
                    break;
                case "6"://合格
                    int APPFReportCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "APPFReportCount"));
                    var i = (from t in db.CF_App_Idea
                             where t.FLinkId == FID
                             orderby t.FCreateTime descending
                             select new { t.FResultInt, t.FResult, t.FId }).FirstOrDefault();
                    if (i != null)
                    {
                        s += "<a href=\"javascript:showAddWindow('LookIdea.aspx?FID=" + i.FId + "',500,350);\">";
                        if (i.FResultInt == 1)//同意备案（管理部门审核）
                        {
                            s += "<font color='green'>" + i.FResult + "</font>";
                        }
                        else//不同意（管理部门审核）
                        {
                            s += "<font color='red'>" + i.FResult + "</font>";
                        }
                        s += "</a>";
                    }

                    e.Row.Cells[8].Text = "--";
                    break;
            }
            e.Row.Cells[7].Text = s;

            string fDate = EConvert.ToShortDateString(DataBinder.Eval(e.Row.DataItem, "FAppDate"));
            if (string.IsNullOrEmpty(fDate))
                fDate = "--";
            e.Row.Cells[5].Text = fDate;

            string fCertiNo = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FCertiNo"));
            if (string.IsNullOrEmpty(fCertiNo))
                fCertiNo = "--";
            e.Row.Cells[6].Text = fCertiNo;



            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
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
                int fState = EConvert.ToInt(s[2]);
                string FIsApprove = (fState == 6 || fState == 1) ? "1" : "0";
                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = FManageTypeId;
                Session["FIsApprove"] = FIsApprove;
                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
        }
        else if (e.CommandName == "CX")//撤销上报
        {
            pageTool tool = new pageTool(this);
            string FAppId = e.CommandArgument.ToString();
            RQuali rq = new RQuali();
            rq.CancelApply(FAppId);
            tool.showMessage("撤消成功！");
            showInfo();
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
