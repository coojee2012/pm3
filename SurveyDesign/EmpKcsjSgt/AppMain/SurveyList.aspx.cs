using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using Approve.RuleCenter;
using ProjectBLL;
using ProjectData;
using System.Text;

public partial class EmpKcsjSgt_AppMain_SurveyList : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType_o = 30102;//设计人员安排
    public int fMType = 30103;//技术性审查(设计)
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            ShowInfo();
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

    private void ShowInfo()
    {
        var App =
              from t in db.CF_App_List
              join ot in db.CF_App_List
              on t.FLinkId equals ot.FLinkId
              where db.CF_Prj_Emp.Count(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId) > 0
               && t.FManageTypeId == fMType
               && ot.FManageTypeId == fMType_o && ot.FState == 6
              orderby t.FCreateTime descending
              select new
              {
                  FId = t.FId,
                  FOldAppId = ot.FId,//人员安排的FAppId
                  FJsEnt = db.CF_Prj_Ent.Where(l => l.FPrjId == t.FPrjId && l.FEntType == 100)
                  .Select(l => l.FName).FirstOrDefault(),
                  FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                  t.FAppDate,
                  t.FCreateTime,
                  t.FPrjId,
                  t.FLinkId,
                  t.FYear,
                  kcState = db.CF_App_List.Where(a => a.FLinkId == t.FLinkId && a.FManageTypeId == 280).Select(a => a.FState).FirstOrDefault(),//勘察单位
                  t.FState,
                  //FType = db.CF_Prj_Emp.Where(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId).Select(m => m.FType).Min(m => m.Value),
                  FFunction = "项目负责人",
                  //施工图设计文件审查合同备案(300)是否二审
                  FReportCount = db.CF_App_List.Where(li => li.FLinkId == t.FLinkId && li.FManageTypeId == 300).Select(li => li.FReportCount).FirstOrDefault()

              };

        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            App = App.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            App = App.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = App.Count();
        DG_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fOldAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FOldAppId"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 3].Controls[0] as LinkButton;

            //查出变更前后，我在其中的角色
            var v = (from t in db.CF_Prj_Emp
                     where t.FAppId == fOldAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId
                     select new { t.FIsDeleted, t.FDataFrom, t.FType }).ToList();

            string os = "", ns = "", ss = "";

            //变更前是...
            if (v.Count(t => t.FDataFrom.GetValueOrDefault() != 1 && t.FType == 1) > 0)
                os = "项目负责人";
            if (v.Count(t => t.FDataFrom.GetValueOrDefault() != 1 && t.FType == 2) > 0)
                os += (!string.IsNullOrEmpty(os) ? "、" : "") + "注册人员";
            if (v.Count(t => t.FDataFrom.GetValueOrDefault() != 1 && t.FType == 3) > 0)
                os += (!string.IsNullOrEmpty(os) ? "、" : "") + "非注册人员";

            //变更后是...
            if (v.Count(t => !t.FIsDeleted.GetValueOrDefault() && t.FType == 1) > 0)
                ns = "项目负责人";
            if (v.Count(t => !t.FIsDeleted.GetValueOrDefault() && t.FType == 2) > 0)
                ns += (!string.IsNullOrEmpty(ns) ? "、" : "") + "设计人员";
            if (v.Count(t => !t.FIsDeleted.GetValueOrDefault() && t.FType == 3) > 0)
                ns += (!string.IsNullOrEmpty(ns) ? "、" : "") + "非注册人员";

            ss += "人员安排有变化！<br/>";
            ss += "变更前是：<tt>" + (!string.IsNullOrEmpty(os) ? os : "无") + "</tt>";
            ss += "<br/>变更后是：<font color='#FFF400'>" + (!string.IsNullOrEmpty(ns) ? ns : "无") + "</font>";

            if (!string.IsNullOrEmpty(ns))
            { //变更后有职务
                int fType = v.Where(t => !t.FIsDeleted.GetValueOrDefault()).Min(m => m.FType.GetValueOrDefault());

                string fStateDsec = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
                //状态办理结果
                string sUrl = "Report.aspx?FAppId=" + fid;
                if (fType > 1)//非主要负责人 
                {
                    var emp = db.CF_Prj_Emp.Where(t => t.FAppId == fOldAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId && t.FType == fType && t.FType > 1).Select(t => new { t.FId, t.FFunction }).FirstOrDefault();
                    e.Item.Cells[3].Text = emp.FFunction;
                    sUrl = "EmpReport.aspx?FAppId=" + fid + "&fid=" + emp.FId;
                }
                string o = "<a href='javascript:showAddWindow(\"../ApplySGTSJJSXSC/" + sUrl + "\",700,650);'>";
                string s = string.Empty;
                //完成时间
                if (fType > 1)//非主要负责人
                {
                    string fAppTime = EConvert.ToShortDateString(db.CF_App_Idea.Where(t => t.FLinkId == fid && t.FUserId == CurrentEmpUser.EmpId).Select(t => t.FAppTime).FirstOrDefault());
                    e.Item.Cells[5].Text = fAppTime;
                }
                switch (fStateDsec)
                {
                    case "0":
                    case "1":
                        if (fType > 1 && !string.IsNullOrEmpty(e.Item.Cells[5].Text))
                        {
                            s = "<font color='blue'>已填写意见</font>";
                        }
                        else
                        {
                            s = "<font color='#888888'>还未完成</font>";
                            e.Item.Cells[5].Text = "<font color='#888888'>--</font>";
                        }
                        o += "填写审查意见";
                        break;
                    case "3"://修改
                        s = "<a href=\"javascript:showAddWindow('LookIdea.aspx?FAppId=" + fid + "',500,350);\"><font color='red'>不合格</font></a>";
                        o += "查看审查详情";
                        break;
                    case "6"://合格
                        s = "<font color='green'>合格</font>";
                        o += "查看审查详情";
                        break;
                    case "7"://
                        s = "<a href=\"javascript:showAddWindow('LookIdea.aspx?FAppId=" + fid + "',500,350);\"><font color='red'>补正材料</font></a>";
                        o += "查看审查详情";
                        break;
                }
                fStateDsec = s;
                e.Item.Cells[7].Text = o + "</a>";

                string fKcState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "KCState"));
                if (fKcState == "2")
                    fStateDsec = "<font color='green'>勘察单位打回</font>";
                else if (fKcState == "7")
                    fStateDsec = "<font color='green'>勘察单位不予受理</font>";
                e.Item.Cells[6].Text = fStateDsec;
            }
            else
            {//变更后没有职务了
                e.Item.Cells[7].Text = "<font color='#888888'>人员安排变更后无职责，<br/>无需办理</font>";
                e.Item.Cells[6].Text = "--";
            }

            //查出有没有变化过
            if (v.Count(t => t.FIsDeleted.GetValueOrDefault() || t.FDataFrom == 1) > 0)
            {//有过变化
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img title=\"" + ss + "\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
            }



            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }

            //是否发生了变更
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == fPrjId)
             .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
             .FirstOrDefault();
            if (prjBG != null && prjBG.FCount > 0)
            {
                Literal prjCount = e.Item.FindControl("prj_Count") as Literal;
                if (prjCount != null)
                    prjCount.Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了  
            var stop = db.CF_Prj_Stop.Count(st => st.FPrjId == fPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            pageTool tool = new pageTool(this.Page);
            string FId = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fPrjId = e.Item.Cells[e.Item.Cells.Count - 2].Text;
            if (e.CommandName == "See")
            {
                this.Session["FAppId"] = FId;
                this.Session["FManageTypeId"] = fMType;
                HiddenField hfState = e.Item.FindControl("hfState") as HiddenField;
                if (hfState != null)
                {
                    if (hfState.Value == "1" || hfState.Value == "6")
                    {
                        Session["FIsApprove"] = 1;
                    }
                    else
                    {
                        Session["FIsApprove"] = 0;
                    }
                }

                Response.Write("<script language='javascript'>parent.parent.document.location='../Appmain/aindex.aspx';</script>");
            }

            else if (e.CommandName == "Delete")//删除业务
            {
                string fAppId = EConvert.ToString(e.CommandArgument);
                if (!string.IsNullOrEmpty(fAppId))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("delete cf_App_list where fid='" + fAppId + "';");
                    sb.Append("delete cf_Prj_Emp where FAppId='" + fAppId + "';");
                    rc.PExcute(sb.ToString());

                    ShowInfo();
                    tool.showMessage("删除成功！");
                }
            }
            else if (e.CommandName == "Report")
            {

                CF_App_List app = db.CF_App_List.Where(t => t.FLinkId == FId && t.FManageTypeId == fMType).FirstOrDefault();
                if (app != null)
                {
                    string fLinkId = app.FId;
                    app.FState = 6;

                    db.SubmitChanges();
                    ShowInfo();
                    tool.showMessage("操作成功！");
                }
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
