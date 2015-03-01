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
    int fMType = 284;//勘察成果移交
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

        //显示待办
        showToDo();

    }


    #region 显示待办

    //显示待办
    private void showToDo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = (from a in db.CF_App_List
                 join d in db.CF_Prj_Data on a.FLinkId equals d.FId
                 join b in db.CF_App_List on a.FLinkId equals b.FLinkId
                 where d.FIsDeleted == false && a.FIsDeleted == false
                    && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == 28301
                    && a.FState == 6//已经完成勘察项目人员意见
                    && b.FManageTypeId == fMType
                 orderby a.FCreateTime descending
                 select new
                 {
                     b.FId,
                     a.FLinkId,
                     dd = (from dd1 in db.CF_Prj_Data
                           join dd2 in db.CF_App_List
                           on dd1.FAppId equals dd2.FId
                           where dd2.FLinkId == a.FLinkId
                           && dd2.FManageTypeId == 283
                           && dd2.FState == 6
                           && dd2.FPrjId == a.FPrjId
                           select new { dd1.FDate1, dd1.FDate2 })
                           .FirstOrDefault(),//提取勘察信息备案的数据
                     d.FDate3,//实际开始时间
                     d.FDate4,//实际结束时间
                     d.FPrjName,
                     d.FPriItemId,
                     a.FBaseName,//建设单位 
                     a.FYear,
                     a.FPrjId,
                     b.FState,
                     //合同备案受理时间 和 建设单位 从280勘察合同备案业务取
                     App280 = db.CF_App_List.Where(t => t.FLinkId == d.FId && t.FToBaseinfoId == FBaseinfoID && t.FManageTypeId == 280 && t.FState == 6).FirstOrDefault(),
                     App28001 = db.CF_App_List.Where(l => a.FLinkId == l.FLinkId && l.FManageTypeId == 28001).FirstOrDefault()
                 });

        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        if (ddlFState.SelectedValue == "6")
            v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
        else if (ddlFState.SelectedValue == "0")
            v = v.Where(t => t.FState == 0 || t.FState == null);
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页时隐藏分页控件
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLinkId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjName"));
            int FState = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FState"));
            
            string FDate1 = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "dd.FDate1"));
            string FDate2 = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "dd.FDate2"));
            e.Row.Cells[4].Text = !string.IsNullOrEmpty(FDate1) ? EConvert.ToShortDateString(FDate1) : "";
            e.Row.Cells[5].Text = !string.IsNullOrEmpty(FDate2) ? EConvert.ToShortDateString(FDate2) : "";
            
            //本业务办理状态
            string s = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");
            LinkButton btnBack = (LinkButton)e.Row.FindControl("btnBack");

            if (FState == 0)
            {
                s = "<font color='blue'>正在办理</font>";
                //用a标记打开业务
                e.Row.Cells[9].Text = "<a href=\"javascript:showAddWindow('../ApplyKCCGYJ/ApplyBaseInfo.aspx?FAppId=" + FID + "',700,700);\">继续办理...</a>";
            }
            else if (FState == 6)
            {
                s = "<font color='green'>已经移交</font>";
                //用a标记打开业务
                e.Row.Cells[9].Text = "<a href=\"javascript:showAddWindow('../ApplyKCCGYJ/ApplyBaseInfo.aspx?FAppId=" + FID + "',700,700);\">查看...</a>";
            }
            //建设单位、受理合同备案时间
            CF_App_List App280 = DataBinder.Eval(e.Row.DataItem, "App280") as CF_App_List;
            if (App280 != null)
            {
                e.Row.Cells[2].Text = App280.FBaseName;
                e.Row.Cells[3].Text = string.Format("{0:d}", App280.FAppDate);
                
                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
                if (App280.FCount > 1)
                {
                    //查询出不合格的意见
                    var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看审图机构意见</a>";
                        ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + App280.FCount + "次," + txt + ")");
                    }
                }
            }

            CF_App_List App28001 = DataBinder.Eval(e.Row.DataItem, "App28001") as CF_App_List;
            if (App28001 != null)
            {
                if (App28001.FState == 2)
                {
                    s += "</br><font color='red'>（已被见证单位退回，业务终止）</font>";
                }
                else if (App28001.FState == 7)
                {
                    s += "</br><font color='red'>（见证单位不予受理，业务终止）</font>";
                }

            }

            e.Row.Cells[8].Text = s;

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
        //if (e.CommandName == "AppNew")
        //{//创建业务并打开
        //    string[] s = e.CommandArgument.ToString().Split(',');
        //    if (s.Length == 3)
        //    {
        //        string FPrjId = s[0];
        //        string FLinkId = s[1];
        //        string FPrjName = s[2];
        //        saveApp(FPrjId, FLinkId, FPrjName);
        //    }
        //}
    }

    /// <summary>
    /// 新建业务，并进入业务办理页面
    /// </summary>
    /// <param name="FPrjId"></param>
    private void saveApp(string FPrjId, string FLinkId, string FPrjName)
    {
        ProjectDB db = new ProjectDB();
        pageTool tool = new pageTool(this.Page);
        if (string.IsNullOrEmpty(CurrentEntUser.EntId))
            return;

        string FAppID = Guid.NewGuid().ToString();
        DateTime dTime = DateTime.Now;

        //业务表
        CF_App_List app = new CF_App_List();//勘察企业业务
        app.FId = FAppID;
        app.FLinkId = FLinkId;
        app.FBaseinfoId = CurrentEntUser.EntId;
        app.FPrjId = FPrjId;
        app.FName = dTime.Year + " " + db.getManageTypeName(fMType);
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FYear = dTime.Year;
        app.FMonth = dTime.Month;
        app.FBaseName = CurrentEntUser.EntName;
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = 1;
        db.CF_App_List.InsertOnSubmit(app);

        //提交保存
        db.SubmitChanges();

        //跳转到办理页面
        tool.ExecuteScript("showAddWindow('../ApplyKCCGYJ/ApplyBaseInfo.aspx?FAppId=" + app.FId + "',700,700);");
    }


    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showToDo();
    }

    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

    #endregion
}
