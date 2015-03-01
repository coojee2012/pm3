using System;
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
    ProjectDB db = new ProjectDB();
    public int fMType_o = 29201;//初步设计人员安排
    public int fMType = 29202;//初步设计人员意见

    public int fMType2_o = 29701;//文件编制人员安排
    public int fMType2 = 29702;//文件编制人员意见
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            conBind();
            ViewState["LookMSG"] = "NOREAD";//只看没读的消息
            showInfo();
        }
    }
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEmpUser.EmpId;
        if (!string.IsNullOrEmpty(FBaseinfoID))
        {
            lit_EntName.Text = CurrentEmpUser.Name;
            lit_TS.Text = "欢迎登录四川省勘察设计科技管理信息平台，我们将竭诚为您服务，请及时更新系统信息。";

            //显示系统消息
            showMSG();

            //显示业务
            showAppList();
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
    #region 显示系统消息

    //显示系统消息
    private void showMSG()
    {
        string FBaseinfoID = CurrentEmpUser.EmpId;

        //var v = (from t in db.CF_Sys_Message
        //         where t.FAccept == FBaseinfoID
        //         orderby t.FIsRead, t.FSendTime descending
        //         select new
        //         {
        //             t.FId,
        //             t.FIsRead,
        //             t.FSendTime,
        //             t.FText
        //         });



        //lit_MSGCount.Text = v.Count().ToString();
        //lit_MSGNoRead.Text = v.Count(t => t.FIsRead == 0).ToString();
        //btnAllRead.Visible = v.Count(t => t.FIsRead == 0) > 0;


        //v = (v.Where(t => (EConvert.ToString(ViewState["LookMSG"]) == "NOREAD" ? t.FIsRead == 0 : true)));

        //Pager1.RecordCount = v.Count();
        //DG_MSG.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        //DG_MSG.DataBind();
        //Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示 
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

    }

    protected void DG_MSG_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FIsRead = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FIsRead"));
            string FText = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FText"));

            LinkButton btnRead = (LinkButton)e.Item.FindControl("btnRead");
            LinkButton btnDel = (LinkButton)e.Item.FindControl("btnDel");
            if (FIsRead == "0")
            {
                FText = "<b>" + FText + "</b>";
            }
            else
            {
                btnRead.Enabled = false;
            }
            e.Item.Cells[1].Text = FText;

            btnDel.Attributes["onclick"] = "return confirm('确定要删除吗？');";
        }
    }
    //列表事件
    protected void DG_MSG_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Read")//标记为已读
        {
            string FID = e.CommandArgument.ToString();
            CF_Sys_Message msg = db.CF_Sys_Message.Where(t => t.FId == FID).FirstOrDefault();
            if (msg != null)
            {
                msg.FIsRead = 1;
                db.SubmitChanges();
                showMSG();
            }
        }
        if (e.CommandName == "Del")//删除
        {
            string FID = e.CommandArgument.ToString();
            CF_Sys_Message msg = db.CF_Sys_Message.Where(t => t.FId == FID).FirstOrDefault();
            if (msg != null)
            {
                db.CF_Sys_Message.DeleteOnSubmit(msg);
                db.SubmitChanges();
                showMSG();
            }
        }
    }

    //全部标记为已读
    protected void btnAllRead_Click(object sender, EventArgs e)
    {
        string FBaseinfoID = CurrentEmpUser.EmpId;
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

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showMSG();
    }

    #endregion

    #region 显示业务

    //显示业务
    private void showAppList()
    {
        string FBaseinfoID = CurrentEmpUser.EmpId;

        var v = from t in db.CF_App_List
                join ot in db.CF_App_List
                on t.FLinkId equals ot.FLinkId
                where db.CF_Prj_Emp.Count(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId) > 0
                && ot.FState == 6
                && ((t.FManageTypeId == fMType && ot.FManageTypeId == fMType_o)
                || (t.FManageTypeId == fMType2 && ot.FManageTypeId == fMType2_o))
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    FOldAppId = ot.FId,//人员安排的FAppId
                    t.FName,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    t.FAppDate,
                    t.FPrjId,
                    t.FLinkId,
                    t.FBaseinfoId,
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    t.FCreateTime,
                    t.FState,
                    //FType = db.CF_Prj_Emp.Where(m => m.FAppId == ot.FId && m.FEmpBaseInfo == CurrentEmpUser.EmpId && !m.FIsDeleted.GetValueOrDefault()).Select(m => m.FType).Min(m => m.Value),
                    FFunction = "项目负责人",
                    FUrl = t.FManageTypeId == fMType ? "applycbsjryyj" : "applysjwjbzryyj"
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
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            string fOldAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FOldAppId"));
            string dir = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FUrl"));
            string fPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));


            //查出变更前后，我在其中的角色
            var v = (from t in db.CF_Prj_Emp
                     where t.FAppId == fOldAppId && t.FEmpBaseInfo == CurrentEmpUser.EmpId
                     select new { t.FIsDeleted, t.FDataFrom, t.FType }).ToList();

            string os = "", ns = "", ss = "";
            //变更前是... 
            if (v.Count(t => t.FDataFrom.GetValueOrDefault() != 1 && t.FType == 1) > 0)
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
            { //变更后有职责
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

                //完成时间
                if (fType > 1)//技术人员
                {
                    string fAppTime = EConvert.ToShortDateString(db.CF_App_Idea.Where(t => t.FLinkId == fid && t.FUserId == CurrentEmpUser.EmpId).Select(t => t.FAppTime).FirstOrDefault());
                    e.Item.Cells[4].Text = fAppTime;
                }
                switch (fStateDsec)
                {
                    case "0":
                    case "1":
                        if (fType == 2 && !string.IsNullOrEmpty(e.Item.Cells[4].Text))
                        {
                            s = "<font color='blue'>已填写意见</font>";
                        }
                        else
                        {
                            s = "<font color='#888888'>还未完成</font>";
                        }
                        break;
                    case "6":
                        s = "<font color='green'>已完成</font>";
                        break;
                }
                fStateDsec = s;
                e.Item.Cells[5].Text = fStateDsec;
                string o = "showAddWindow(\"../" + dir + "/" + sUrl + "\",700,500);";
                ((LinkButton)e.Item.FindControl("btnItemSee")).Attributes["onclick"] = o + "return false;";
            }
            else
            {//变更后没有职务了
                e.Item.Cells[5].Text = "<font color='#888888'>人员安排变更后无职责，<br/>无需办理</font>";

                ((LinkButton)e.Item.FindControl("btnItemSee")).Attributes["onclick"] = "alert('人员安排变更后无任何职责，您无需办理');return false;";
            }

            //查出有没有变化过
            if (v.Count(t => t.FIsDeleted.GetValueOrDefault() || t.FDataFrom == 1) > 0)
            {//有过变化
                ((Literal)e.Item.FindControl("lit_TS")).Text = "<img title=\"" + ss + "\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";
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
                string FIsApprove = EConvert.ToInt(s[2]) > 0 ? "1" : "0";

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
    protected void btnReload_Click(object sender, EventArgs e)
    {
        //显示业务
        showAppList();
    }
}
