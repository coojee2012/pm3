using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

public partial class EmpJZDW_AppMain_BusinessBZ : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType_o = 29701;//人员安排
    public int fMType = 29702;//人员意见
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
        var App = from t in db.CF_App_List
                  join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                  join ot in db.CF_App_List on t.FLinkId equals ot.FLinkId
                  where db.CF_Prj_Emp.Count(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId) > 0
                   && t.FManageTypeId == fMType
                   && ot.FManageTypeId == fMType_o && ot.FState == 6
                  orderby t.FState, t.FTime descending
                  select new
                   {
                       FId = t.FId,
                       FOldAppId = ot.FId,//人员安排的FAppId
                       FJsEnt = (from ll in db.CF_App_List
                                 join b in db.CF_Prj_Ent on ll.FId equals b.FAppId
                                 where ll.FLinkId == t.FLinkId && b.FEntType == 100
                                 select b.FName).FirstOrDefault(),
                       d.FPrjName,// = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                       d.FPriItemId,
                       t.FAppDate,
                       t.FCreateTime,
                       t.FPrjId,
                       t.FLinkId,
                       t.FYear,
                       t.FState,
                       //FType = db.CF_Prj_Emp.Where(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId && !m.FIsDeleted.GetValueOrDefault()).Select(m => m.FType).Min(m => m.Value),
                       FFunction = "项目负责人",
                       //施工图设计文件编制合同备案296 情况
                       FCount = db.CF_App_List.Where(l => t.FLinkId == l.FLinkId && l.FManageTypeId == 296).Select(l => l.FCount).FirstOrDefault()
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
            if (v.Count(t => t.FDataFrom.GetValueOrDefault()!=1 && t.FType == 1) > 0)
                os = "项目负责人";
            if (v.Count(t => t.FDataFrom.GetValueOrDefault() != 1 && t.FType == 2) > 0)
                os += (!string.IsNullOrEmpty(os) ? "、" : "") + "设计人员";

            //变更后是...
            if (v.Count(t => !t.FIsDeleted.GetValueOrDefault() && t.FType == 1) > 0)
                ns = "项目负责人";
            if (v.Count(t => !t.FIsDeleted.GetValueOrDefault() && t.FType == 2) > 0)
                ns += (!string.IsNullOrEmpty(ns) ? "、" : "") + "设计人员";

            ss += "人员安排有变化！<br/>";
            ss += "变更前是：<tt>" + (!string.IsNullOrEmpty(os) ? os : "无") + "</tt>";
            ss += "<br/>变更后是：<font color='#FFF400'>" + (!string.IsNullOrEmpty(ns) ? ns : "无") + "</font>";

            if (!string.IsNullOrEmpty(ns))
            {  //变更后有职务
                int fType = v.Where(t => !t.FIsDeleted.GetValueOrDefault()).Min(m => m.FType.GetValueOrDefault());

                string fStateDsec = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
                //状态办理结果
                string sUrl = "Report.aspx?FAppId=" + fid;
                if (fType > 1)//技术人员
                {
                    var emp = db.CF_Prj_Emp.Where(t => t.FAppId == fOldAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId && t.FType == fType).Select(t => new { t.FId, t.FFunction }).FirstOrDefault();
                    e.Item.Cells[3].Text = emp.FFunction;
                    sUrl = "AddReport.aspx?FAppId=" + fid + "&fid=" + emp.FId;
                }
                string s = "";
                string o = "<a href='javascript:showAddWindow(\"../applysjwjbzryyj/" + sUrl + "\",700,500);'>";

                //完成时间
                if (fType > 1)//技术人员
                {
                    string fAppTime = EConvert.ToShortDateString(db.CF_App_Idea.Where(t => t.FLinkId == fid && t.FUserId == CurrentEmpUser.EmpId).Select(t => t.FAppTime).FirstOrDefault());
                    e.Item.Cells[5].Text = fAppTime;
                }
                switch (fStateDsec)
                {
                    case "0":
                    case "1":
                        if (fType == 2 && !string.IsNullOrEmpty(e.Item.Cells[5].Text))
                        {
                            s = "<font color='blue'>已填写意见</font>";
                        }
                        else
                        {
                            s = "<font color='#888888'>还未完成</font>";
                            e.Item.Cells[5].Text = "<font color='#888888'>--</font>";
                        }
                        o += "填写意见";
                        break;
                    case "6":
                        s = "<font color='green'>已完成</font>";
                        o += "查看意见";
                        break;
                }
                fStateDsec = s;
                e.Item.Cells[7].Text = o + "</a>";
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


            //是否二次。
            int n = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FCount"));
            if (n > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + n + "次)");
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
    protected void DG_List1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();

            //如果还没有出见证报告可以重新安排人员
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            if (db.CF_App_List.Where(t => t.FPrjId == FPrjId && t.FManageTypeId == 293).Count() > 0)
            {
                e.Item.Cells[e.Item.Cells.Count - 3].Text = "--";
            }
        }
    }
}
