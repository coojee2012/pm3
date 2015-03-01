﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Tools;
using System.Text;
using Approve.RuleCenter;
using Approve.RuleApp;


public partial class KC_Main_zWXTS : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["LookMSG"] = "NOREAD";//只看没读的消息
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
        if (!string.IsNullOrEmpty(FBaseinfoID))
        {
            lit_EntName.Text = CurrentEntUser.EntName;
            lit_TS.Text = "欢迎登录四川省勘察设计科技管理信息平台，我们将竭诚为您服务，请及时更新系统信息。";

            //显示系统消息
            showMSG();

            //显示业务
            showAppList();
        }
    }

    #region 显示系统消息

    //显示系统消息
    private void showMSG()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        ProjectDB db = new ProjectDB();

        IQueryable<CF_Sys_Message> v = db.CF_Sys_Message.Where(t => t.FAccept == FBaseinfoID).OrderByDescending(t => t.FIsRead).OrderByDescending(t => t.FSendTime);
        lit_MSGCount.Text = v.Count().ToString();
        lit_MSGNoRead.Text = v.Count(t => t.FIsRead == 0).ToString();
        btnAllRead.Visible = v.Count(t => t.FIsRead == 0) > 0;
        v = (v.Where(t => (EConvert.ToString(ViewState["LookMSG"]) == "NOREAD" ? t.FIsRead == 0 : true)));


        StringBuilder sb = new StringBuilder();
        sb.Append("<marquee onmouseout='this.start()' onmouseover='this.stop()' scrollamount='2' scrolldelay='10' direction=left  ");
        sb.Append("width='98%' height='32px'>");
        foreach (CF_Sys_Message msg in v.Take(10))
        {
            sb.Append("   " + msg.FText);
        }
        sb.Append("</marquee>");

        this.xtxx.Text = sb.ToString();

        //Pager1.RecordCount = v.Count();
        //DG_MSG.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        //DG_MSG.DataBind();

        //Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }



    //全部标记为已读
    protected void btnAllRead_Click(object sender, EventArgs e)
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        ProjectDB db = new ProjectDB();
        IQueryable<CF_Sys_Message> msg = db.CF_Sys_Message.Where(t => t.FAccept == FBaseinfoID && t.FIsRead == 0);
        foreach (CF_Sys_Message t in msg)
        {
            t.FIsRead = 1;
        }
        db.SubmitChanges();
        showMSG();
    }


    //查看全部
    protected void btnAll_Click(object sender, EventArgs e)
    {
        ViewState["LookMSG"] = "ALL";//查看全部
        showMSG();
    }
    //查看未读
    protected void btnNoRead_Click(object sender, EventArgs e)
    {
        ViewState["LookMSG"] = "NOREAD";//查看未读
        showMSG();
    }



    #endregion

    #region 显示业务

    //显示业务
    private void showAppList()
    {
        ProjectDB db = new ProjectDB();
        string FBaseinfoID = CurrentEntUser.EntId;

        var v = from t in db.CF_App_List
                where (t.FBaseinfoId == FBaseinfoID || (t.FToBaseinfoId == FBaseinfoID && t.FState > 0))
                orderby t.FReportDate descending
                orderby t.FState
                select new
                {
                    t.FId,
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    FReportDate = (t.FManageTypeId == 290
                    || t.FManageTypeId == 305) ? t.FReportDate : t.FAppDate,
                    t.FUpDeptId,
                    t.FYear,
                    t.FAppDate,
                    t.FPrjId,
                    //工程名称 
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    t.FLinkId,
                    t.FBaseinfoId,
                    t.FToBaseinfoId,
                    //勘察文件审查合同备案(287)是否二审
                    FReportCount = db.CF_App_List.Where(li => li.FLinkId == t.FLinkId && li.FManageTypeId == 287).Select(li => li.FReportCount).FirstOrDefault(),

                    FUrl = (t.FManageTypeId == 291 || t.FManageTypeId == 29201
                   || t.FManageTypeId == 292 || t.FManageTypeId == 293) ? "ApplyKCWJSCWTSL" : "ApplySGTSJWJSCWTSL",
                };

        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
        {
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);
        }

        Pager2.RecordCount = v.Count();
        App_List.DataSource = v.Skip((Pager2.CurrentPageIndex - 1) * Pager2.PageSize).Take(Pager2.PageSize);
        App_List.DataBind();
        Pager2.Visible = (Pager2.RecordCount > Pager2.PageSize);//不足一页不显示 
    }



    //列表
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ProjectDB db = new ProjectDB();
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager2.PageSize * (this.Pager2.CurrentPageIndex - 1)).ToString();
            string MyFBaseinfoId = CurrentEntUser.EntId;//当前用户BaseinfoId
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//业务ID
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FManageTypeId"));//业务类型
            string FPrjID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjID"));//工程ID
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));//业务信息主表ID（CF_Prj_Data.FID）
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));//业务状态
            string FBaseinfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FBaseinfoId"));//业务发起者BaseinfoId
            string FToBaseinfoId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FToBaseinfoId"));//业务接收者BaseinfoId 

            //操作按钮
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");
            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }
            //查询项目的变更时间 
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == FPrjID)
                .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
                .FirstOrDefault();
            if (prjBG != null && prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //状态
            string s = "", r = "";
            if (FBaseinfoId == MyFBaseinfoId)
            { //当前用户为业务发起者
                switch (FState)
                {
                    case "0"://未上报
                        s = "<font color='#444444'>未办理</font>";
                        r = "<font color='#444444'>未办理</font>";
                        btnOp.Text = "删除";
                        btnOp.CommandName = "Del";
                        btnOp.ToolTip = "删除业务";
                        btnOp.Attributes["onclick"] = "return confirm('确定要删除该业务吗？')";
                        break;
                    case "1"://已上报
                        s = "<font color='blue'>已上报</font>";
                        r = "<font color='#444444'>还未办理</font>";
                        if (FManageTypeId == "290")
                        {   //勘察文件审查备案（是报主管部门审批的）
                            RApp ra = new RApp();
                            if (ra.isBeginApp(FID))
                            {
                                r = "<font color='#33E5F7'>已开始审批</font>";
                            }
                            else
                            {
                                r = "<font color='#444444'>未审批</font>";

                                btnOp.Text = "撤消上报";
                                btnOp.CommandName = "appBack";
                                btnOp.ToolTip = "撤消上报";
                                btnOp.Attributes["onclick"] = "return confirm('确定要撤消上报吗？')";
                            }
                        }
                        else
                        {
                            btnOp.Text = "撤消合同备案";
                            btnOp.CommandName = "Back";
                            btnOp.ToolTip = "撤消合同备案";
                            btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')";
                        }
                        break;
                    case "2"://被退回
                        s = "<font color='red'>被退回</font>";
                        r = "<font color='red'>退回</font>";
                        btnOp.Text = "--";
                        btnOp.CommandName = "";
                        btnOp.Attributes["onclick"] = "return false;";
                        break;
                    case "3"://不合格、不通过 
                        s = "<font color='red'>不合格</font>";
                        r = "<font color='red'>不合格</font>";
                        btnOp.Text = "--";
                        btnOp.CommandName = "";
                        btnOp.Attributes["onclick"] = "return false;";
                        break;
                    case "6"://已办结
                        s = "<font color='green'>已办结</font>";
                        r = "<font color='green'>同意</font>";
                        btnOp.Text = "--";
                        btnOp.CommandName = "";
                        btnOp.Attributes["onclick"] = "return false;";
                        break;
                    case "7"://
                        s = "<font color='red'>已办理</font>";
                        r = "<font color='red'>补正材料</font>";
                        break;
                }
            }
            else if (FToBaseinfoId == MyFBaseinfoId)
            {
                //当前用户为业务接收者
                switch (FState)
                {
                    case "1"://已上报
                        s = "<font color='444444'>还未办理</font>";
                        r = "<font color='#444444'>未办理</font>";
                        break;
                    case "2"://被退回
                        s = "<font color='red'>已退回</font>";
                        r = "<font color='red'>退回</font>";
                        break;
                    case "6"://已办结
                        s = "<font color='green'>已办结</font>";
                        r = "<font color='green'>同意</font>";
                        break;
                    case "7":
                        s = "<font color='red'>已办理</font>";
                        r = "<font color='red'>不予受理</font>";
                        break;
                }
                btnOp.Text = "--";
                btnOp.CommandName = "";
                btnOp.Attributes["onclick"] = "return false;";
            }

            //状态
            e.Item.Cells[4].Text = s;
            //办理结果
            e.Item.Cells[5].Text = r;

            //如果不是备案业务
            if (!(FManageTypeId == "290" || FManageTypeId == "305"
                || FManageTypeId == "413" || FManageTypeId == "423"))
            {
                string sUrl = string.Empty;
                //则根据业务打开页面
                switch (FManageTypeId)
                {
                    //******勘察文件审查**********//
                    case "287"://勘察文件审查合同备案
                        sUrl = "showAddWindow(\"../ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FID + "\",700,480);";
                        break;
                    case "28801"://程序性审查
                        sUrl = "showAddWindow(\"../ApplyKCCXXSC/Report.aspx?FAppId=" + FID + "\",700,480);";
                        break;
                    case "28802"://审查人员安排
                        sUrl = "showAddWindow(\"../ApplyKCSCRYAP/Report.aspx?FAppId=" + FID + "\",700,680);";
                        break;
                    case "28803"://技术性审查
                        sUrl = "showAddWindow(\"../ApplyKCJSXSC/Report.aspx?FAppId=" + FID + "\",700,680);";
                        break;


                    //******施工图文件审查**********//
                    case "300"://施工图设计文件审查合同备案
                        sUrl = "showAddWindow(\"../ApplySGTSJWJSCWTSL/Report.aspx?FAppId=" + FID + "\",700,580);";
                        break;
                    case "30101"://程序性审查
                        sUrl = "showAddWindow(\"../ApplySGTSJCXXSC/Report.aspx?FAppId=" + FID + "\",700,480);";
                        break;
                    case "30102"://审查人员安排
                        sUrl = "showAddWindow(\"../ApplySGTSJSCRYAP/Report.aspx?FAppId=" + FID + "\",700,680);";
                        break;
                    case "30103"://技术性审查
                        sUrl = "showAddWindow(\"../ApplySGTSJJSXSC/Report.aspx?FAppId=" + FID + "\",700,680);";
                        break;

                }

                ((LinkButton)e.Item.FindControl("btnItemSee")).Attributes["onclick"] = sUrl + "return false;";
            }
            else if (FManageTypeId == "413" || FManageTypeId == "423")
            {
                //工程名称
                string pName = db.CF_Prj_Data.Where(t => t.FId == FID).Select(t => t.FPrjName).FirstOrDefault();
                if (FReportCount > 1)
                {
                    pName += ("(" + FReportCount + "审)");
                }
                if (prjBG != null && prjBG.FCount > 0)
                {
                    pName += "<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")";
                }
                ((Literal)e.Item.FindControl("lFName")).Text = pName;
            }
            DateTime dtime = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FCreateTime"));
            if (dtime.AddMonths(3) < DateTime.Now)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text += "<tt>已超过3个月。</tt>";
            }
        }
    }
    //列表事件
    protected void App_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "See") //打开业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 3)
            {
                pageTool tool = new pageTool(this);
                string FAppId = s[0];
                string FManageTypeId = s[1];
                string FIsApprove = "";
                if (FManageTypeId == "287")//受理合同备案业务
                {
                    FIsApprove = (s[2] == "2" || s[2] == "6") ? "1" : "0";
                }
                else//其它常规业务
                {
                    FIsApprove = EConvert.ToInt(s[2]) > 0 ? "1" : "0";
                }

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = FManageTypeId;
                Session["FIsApprove"] = FIsApprove;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
        }
        else if (e.CommandName == "Del") //删除业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 3)
            {
                string FAppId = s[0];
                DelApp(FAppId);
            }
        }
        else if (e.CommandName == "appBack") //撤消上报管理部门的业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 3)
            {
                string FAppId = s[0];
                GetAppBack(FAppId);
            }
        }
        else if (e.CommandName == "Back") //撤消提交到其他企业的业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 3)
            {
                string FAppId = s[0];
                GetBack(FAppId);
            }
        }
    }

    /// <summary>
    /// 删除业务
    /// </summary>
    /// <param name="FAPPID">CF_App_List.FID</param>
    private void DelApp(string FAPPID)
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(FAPPID))
        {
            ProjectDB db = new ProjectDB();
            //业务
            CF_App_List App = db.CF_App_List.Where(t => t.FId == FAPPID).FirstOrDefault();
            if (App != null)
                db.CF_App_List.DeleteOnSubmit(App);

            //基本信息
            CF_Prj_Data Data = db.CF_Prj_Data.Where(t => t.FAppId == FAPPID).FirstOrDefault();
            if (Data != null)
                db.CF_Prj_Data.DeleteOnSubmit(Data);

            //相关企业
            IQueryable<CF_Prj_Ent> Ent = db.CF_Prj_Ent.Where(t => t.FAppId == FAPPID);
            if (Ent != null)
                db.CF_Prj_Ent.DeleteAllOnSubmit(Ent);

            //相关人员
            IQueryable<CF_Prj_Emp> Emp = db.CF_Prj_Emp.Where(t => t.FAppId == FAPPID);
            if (Emp != null)
                db.CF_Prj_Emp.DeleteAllOnSubmit(Emp);


            //提交删除
            db.SubmitChanges();
            tool.showMessage("删除成功");
            showInfo();
        }
        else
        {
            tool.showMessage("删除失败");
        }
    }

    /// <summary>
    /// 上报主管部门的业务撤消
    /// </summary>
    /// <param name="FAPPID">CF_App_List.FID</param>
    private void GetAppBack(string FAPPID)
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(FAPPID))
        {
            RQuali rq = new RQuali();
            rq.CancelApply(hidd_FID.Value);
            tool.showMessage("撤消成功");
            showInfo();
        }
        else
        {
            tool.showMessage("撤消失败");
        }
    }

    /// <summary>
    /// 提交到其他企业的业务撤消
    /// </summary>
    /// <param name="FAPPID"></param>
    private void GetBack(string FAPPID)
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(FAPPID))
        {
            ProjectDB db = new ProjectDB();
            //业务
            CF_App_List App = db.CF_App_List.Where(t => t.FId == FAPPID).FirstOrDefault();
            if (App != null)
            {
                App.FState = 0;

                db.SubmitChanges();
                tool.showMessage("撤消成功");
                showInfo();
            }
            else
            {
                tool.showMessage("撤消失败");
            }
        }
        else
        {
            tool.showMessage("撤消失败");
        }
    }
    //分页面控件翻页事件
    protected void Pager2_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager2.CurrentPageIndex = e.NewPageIndex;
        showAppList();
    }
    //年度选择事件
    protected void drop_FYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //显示业务
        showAppList();
    }
    #endregion
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
