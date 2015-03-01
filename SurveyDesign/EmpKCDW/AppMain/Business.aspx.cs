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

public partial class EmpJZDW_AppMain_Business : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType_o = 283;//勘察项目备案
    public int fMType = 28301;//勘察项目人员意见
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
              join d in db.CF_Prj_Data on t.FLinkId equals d.FId
              where db.CF_App_List.Count(ot => t.FLinkId == ot.FLinkId && ot.FManageTypeId == fMType_o && ot.FState == 6 &&
                  db.CF_Prj_Emp.Count(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId) > 0) > 0
               && t.FManageTypeId == fMType
              orderby t.FCreateTime descending
              select new
               {
                   FId = t.FId,
                   FOldAppId = t.FLinkAppId,//勘察备案FAppid
                   FJsEnt = db.CF_Prj_Ent.Where(l => l.FPrjId == t.FPrjId && l.FEntType == 100).Select(l => l.FName).FirstOrDefault(),
                   d.FPrjName,
                   d.FPriItemId,
                   t.FAppDate,
                   t.FCreateTime,
                   t.FPrjId,
                   t.FLinkId,
                   t.FYear,
                   app = db.CF_App_List.Where(a => a.FLinkId == t.FLinkId && a.FManageTypeId == 28001).FirstOrDefault(),//见证单位
                   t.FState,
                   FType = db.CF_Prj_Emp.Where(m => m.FAppId == t.FLinkAppId && m.FEmpBaseInfo == CurrentEmpUser.EmpId).Min(m => m.FType),
                   FFunction = "项目负责人",
                   //查询勘察信息备案业务是否已变更并删除
                   BakAppIsDeleted = (db.CF_App_List.Where(l => l.FId == t.FLinkAppId).Select(l => l.FIsDeleted).FirstOrDefault())
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
            //显示主要职责
            int fType = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FType"));

            string fStateDsec = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            //状态办理结果
            string sUrl = "Report.aspx?FAppId=" + fid;
            if (fType == 2)//勘察人员
            {
                var emp = db.CF_Prj_Emp.Where(t => t.FAppId == fOldAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId && t.FType == fType).Select(t => new { t.FId, t.FFunction }).FirstOrDefault();
                e.Item.Cells[3].Text = emp.FFunction;
                sUrl = "AddReport.aspx?FAppId=" + fid + "&fid=" + emp.FId;
            }
            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../ApplyRYYJ/" + sUrl + "\",700,500);'>";

            //完成时间
            if (fType == 2)//勘察人员
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
            CF_App_List app = DataBinder.Eval(e.Item.DataItem, "app") as CF_App_List;
            if (app != null)
            {
                if (app.FState == 2)
                    fStateDsec += "<br /><font color='red'>见证单位打回</font>";
                else if (app.FState == 7)
                    fStateDsec += "<br /><font color='red'>见证单位不予受理</font>";
                e.Item.Cells[6].Text = fStateDsec;


                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPriItemId"));
                if (app.FCount > 1)
                {
                    //查询出不合格的意见
                    var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看审图机构意见</a>";
                        ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + app.FCount + "次," + txt + ")");
                    }
                }
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


            int BakAppIsDeleted = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "BakAppIsDeleted"));
            if (BakAppIsDeleted == 1)
            {
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img title=\"上一步“勘察项目信息备案”已重办，本业务也无效。”\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
                e.Item.Cells[6].Text = "--";
                e.Item.Cells[7].Text = "--";
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
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

            //如果还没有出成果移交可以重新安排人员
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            if (db.CF_App_List.Where(t => t.FPrjId == FPrjId && t.FManageTypeId == 284).Count() > 0)
            {
                e.Item.Cells[e.Item.Cells.Count - 3].Text = "--";

            }
        }
    }
}
