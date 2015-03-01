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

public partial class KC_AppMain_HTBAlist : System.Web.UI.Page
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


        int fMType = 411;//合同备案
        //是否省外合同
        if (Request.QueryString["t"] == "2")
        {
            lit_SW.Text = "（<tt>省外项目合同</tt>）";
            fMType = 421;//合同备案（省外）

            DG_List.Columns[3].Visible = false;

        }
        ViewState["fMType"] = fMType;

        btnPup.Text = "新增" + db.getManageTypeName(ViewState["fMType"]);
    }


    //显示
    private void showInfo()
    {
        //显示待办
        showToDo();
    }


    #region 显示待办

    //显示待办
    private void showToDo()
    {
        int fMType = EConvert.ToInt(ViewState["fMType"]);
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = (from d in db.CF_Prj_Data
                 join a in db.CF_App_List on d.FId equals a.FLinkId
                 where d.FIsDeleted == false
                    && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == fMType
                 orderby a.FCreateTime descending
                 select new
                 {
                     a.FId,
                     a.FManageTypeId,
                     a.FPrjId,
                     a.FCreateTime,
                     a.FReportDate,
                     a.FState,
                     a.FYear,
                     a.FAppDate,
                     d.FPriItemId,//与之前做的合同备案业务关联CF_Prj_Data.FID
                     d.FPrjName,
                     d.FTxt1,
                     d.FDate1,
                     a.FIsDeleted,
                 });

        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
            v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageCount);//不足一页时隐藏分页控件
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
            string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FReportDate"));

            //本业务办理状态
            string t = "", s = "", n = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");

            //判断是不是从上面的业务来的。 
            if (!string.IsNullOrEmpty(FPriItemId))
            {//确认合同备案后自动创建
                ((Literal)e.Row.FindControl("lit_TS")).Text += "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"确认合同备案后自动创建，可删除，删除后可恢复\"/>";

                var app280 = (from a in db.CF_App_List
                              join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                              where a.FLinkId == FPriItemId && a.FManageTypeId == 280
                              select new
                              {
                                  a.FCount,
                                  d.FPriItemId
                              }).FirstOrDefault();
                if (app280 != null && app280.FCount > 1)
                {
                    //查询出不合格的意见（从勘查文件审查业务的技术性审查28803中查）
                    var v = db.CF_App_List.Where(a => a.FLinkId == app280.FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看审图机构意见</a>";
                        ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + app280.FCount + "次," + txt + ")");
                    }
                }
            }
            else
            {//手动添加的
                e.Row.Cells[3].Text = "<font color='#888888'>--</font>";
            }
            switch (FState)
            {
                case "0"://未上报 
                    t = "<font color='#888888'>--</font>";
                    s = "<font color='#888888'>未上报</font>";
                    btnOp.Attributes["onclick"] = "return confirm('确定要删除吗？');";
                    btnOp.Text = "删除";
                    btnOp.ToolTip = "删除业务";
                    btnOp.CommandArgument = FID;

                    if (string.IsNullOrEmpty(FPriItemId))
                        btnOp.CommandName = "Del";//自建业务 可直接删除
                    else
                        btnOp.CommandName = "LogicDel";//合同备案过来的业务 只能逻辑删除并能恢复

                    break;
                case "1"://已上报 
                    t = FReportDate.ToShortDateString();
                    //初步设计文件审查申报（是报主管部门审批的）
                    RApp ra = new RApp();
                    if (ra.isBeginApp(FID))
                    {
                        s = "<font color='blue'>已上报，已开始审核</font>";
                        s = "<a href=\"javascript:showAddWindow('../main/ShowAppInfo1.aspx?FID=" + FID + "',760,400);\">" + s + "</a>";
                        btnOp.Attributes["onclick"] = "return false;";
                        btnOp.Text = "--";
                    }
                    else
                    {
                        s = "<font color='444444'>已上报，还未审核</font>";

                        btnOp.Text = "撤消上报";
                        btnOp.ToolTip = "撤消上报";
                        btnOp.CommandArgument = FID;
                        btnOp.CommandName = "Back";
                        btnOp.Attributes["onclick"] = "return confirm('确定要撤消上报吗？');";
                    }
                    break;
                case "2"://被退回 
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    t = FReportDate.ToShortDateString();
                    s = "<a href=\"javascript:showApproveWindow('../appmain/LookIdea.aspx?FAppId=" + FID + "',600,400);\"><font color='red'>退回</font></a>";
                    break;
                case "6"://办结
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    t = FReportDate.ToShortDateString();
                    s = "<font color='green'>已办结</font>";

                    //查询备案通过没
                    var v = (from i in db.CF_App_Idea
                             where i.FLinkId == FID
                             orderby i.FCreateTime descending
                             select new
                             {
                                 i.FId,
                                 i.FResult,
                                 i.FResultInt,
                                 i.FAppTime,
                             }).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.FResultInt == 1)//同意备案（管理部门审核）
                        {
                            s += "，<font color='green'>" + v.FResult + "</font>";
                        }
                        else//不同意（管理部门审核）
                        {
                            s += "，<font color='red'>" + v.FResult + "</font>";
                        }
                    }

                    s = "<a href=\"javascript:showAddWindow('../main/ShowAppInfo1.aspx?FID=" + FID + "',760,400);\">" + s + "</a>";
                    break;
                default:
                    btnOp.Attributes["onclick"] = "return false;";
                    btnOp.Text = "--";
                    break;
            }

            e.Row.Cells[4].Text = t;
            e.Row.Cells[5].Text = s;



            //判断是否已被逻辑删除

            if (EConvert.ToBool(DataBinder.Eval(e.Row.DataItem, "FIsDeleted")))
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#888888;";
                ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('业务已删除，若要重新办理请先恢复删除');return false;";
                btnOp.Attributes["onclick"] = "return confirm('确定要恢复吗？');";
                btnOp.Text = "恢复";
                btnOp.ToolTip = "恢复删除业务";
                btnOp.CommandArgument = FID;
                btnOp.CommandName = "LogicDelRest";// 逻辑删除业务恢复
            }
            //查询项目的变更时间 
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == FPrjId)
                .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
                .FirstOrDefault();
            if (prjBG != null && prjBG.FCount > 0)
            {
                ((Literal)e.Row.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(st => st.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "alert('该项目已被中止，所有业务停止进行。');return false;";
            }
            else if (prjBG != null && prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "alert('该项目已经做了变更，变更前的业务停止进行。');return false;";
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
                string FIsApprove = (s[2] == "0" || s[2] == "2") ? "0" : "1";

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = FManageTypeId;
                Session["FIsApprove"] = FIsApprove;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
        }
        else if (e.CommandName == "Del")
        { //删除（手动加的业务做物理删除）
            string FAPPID = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            if (!string.IsNullOrEmpty(FAPPID))
            {
                DelApp(FAPPID);

                tool.showMessage("删除成功");
                showInfo();
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
        else if (e.CommandName == "LogicDel")
        { //逻辑删除
            string FAPPID = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            if (!string.IsNullOrEmpty(FAPPID))
            {
                DelAndRest(FAPPID, true);

                tool.showMessage("删除成功");
                showInfo();
            }
            else
            {
                tool.showMessage("删除失败");
            }
        }
        else if (e.CommandName == "LogicDelRest")
        { //逻辑删除恢复
            string FAPPID = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            if (!string.IsNullOrEmpty(FAPPID))
            {
                DelAndRest(FAPPID, false);

                tool.showMessage("恢复成功");
                showInfo();
            }
            else
            {
                tool.showMessage("恢复失败");
            }
        }
        else if (e.CommandName == "Back")
        { //撤消上报
            string FAPPID = e.CommandArgument.ToString();
            pageTool tool = new pageTool(this.Page);
            if (!string.IsNullOrEmpty(FAPPID))
            {
                RQuali rq = new RQuali();
                rq.CancelApply(FAPPID);
                tool.showMessage("撤消成功");
                showInfo();
            }
            else
            {
                tool.showMessage("撤消失败");
            }
        }
    }

    //删除业务
    private void DelApp(string FAppId)
    {
        //业务表
        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
            db.CF_App_List.DeleteOnSubmit(app);
        //数据主表
        CF_Prj_Data data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
        if (data != null)
            db.CF_Prj_Data.DeleteOnSubmit(data);
        //附件
        IQueryable<CF_AppPrj_FileOther> file = db.CF_AppPrj_FileOther.Where(t => t.FAppId == FAppId);
        if (file != null)
            db.CF_AppPrj_FileOther.DeleteAllOnSubmit(file);

        db.SubmitChanges();
    }

    //逻辑 删除业务和恢复
    private void DelAndRest(string FAppId, bool isDel)
    {
        CF_App_List app = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (app != null)
        {
            app.FIsDeleted = isDel;
            db.SubmitChanges();
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showToDo();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    #endregion


    #region 新建业务

    //创建业务
    private void SaveInfo()
    {
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(CurrentEntUser.EntId))
            return;
        int fMType = EConvert.ToInt(ViewState["fMType"]);
        int thisMType = 280;//项目勘察合同备案（关联什么类型的合同）

        DateTime dTime = DateTime.Now;
        string FAppId = Guid.NewGuid().ToString();
        CF_App_List app = new CF_App_List();//业务
        app.FId = FAppId;
        app.FLinkId = FAppId;
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FName = t_FName.Text.Trim();//业务名
        app.FYear = EConvert.ToInt(t_FYear.Text.Trim());//年份
        app.FMonth = DateTime.Now.Month;//月份
        app.FBaseName = CurrentEntUser.EntName;//单位名
        app.FBaseinfoId = CurrentEntUser.EntId;//单位ID
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(app);

        CF_Prj_Data data = new CF_Prj_Data();
        data.FId = FAppId;
        data.FAppId = FAppId;
        data.FInt3 = thisMType;//本次业务类型
        data.FPrjName = t_FPrjName.Text.Trim();//工程名
        data.FTxt1 = t_FTxt1.Text;//建设单位 
        data.FTxt7 = CurrentEntUser.EntName;//勘察单位（自已）
        data.FBaseInfoId = CurrentEntUser.EntId;
        data.FType = fMType;
        data.FTime = dTime;
        data.FCreateTime = dTime;
        data.FIsDeleted = false;
        db.CF_Prj_Data.InsertOnSubmit(data);

        db.SubmitChanges();

        Session["FAppId"] = FAppId;
        Session["FManageTypeId"] = fMType;
        Session["FIsApprove"] = 0;
        tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
    }

    //确定按钮
    protected void btnOk_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        SaveInfo();
    }

    //新建业务 按钮 
    protected void btn_Click(object sender, EventArgs e)
    {
        int fMType = EConvert.ToInt(ViewState["fMType"]);
        appTab.Visible = false;
        applyInfo.Visible = true;
        t_FYear.Text = DateTime.Now.Year.ToString();
        t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
        t_FPrjName.Text = "";
    }
    //取消
    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        appTab.Visible = true;
        applyInfo.Visible = false;
    }

    #endregion
}
