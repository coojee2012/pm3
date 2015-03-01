using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Approve.EntityBase;
using ProjectData;
using System.Linq;
using ProjectBLL;
using Tools;

public partial class JSDW_main_AppKCWJSCWT : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    public int fMType = 287;//勘察文件审查合同备案
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
        btnPup.Text = "新增" + db.getManageTypeName(fMType);

        for (int i = DateTime.Now.Year; i >= 2013; i--)
        {
            drop_FYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drop_FYear.Items.Insert(0, new ListItem("--全部--", ""));
    }

    //显示
    private void ShowInfo()
    {
        var v = (from p in db.CF_Prj_Data
                 join a in db.CF_App_List on p.FAppId equals a.FId
                 where p.FBaseInfoId == CurrentEntUser.EntId && p.FType == fMType && a.FManageTypeId == fMType
                 orderby p.FCreateTime descending
                 select new
                 {
                     p.FId,
                     p.FPrjName,
                     a.FYear,
                     a.FReportDate,
                     a.FState,
                     a.FName,
                     a.FLinkId,
                     a.FAppDate,
                     a.FPrjId,
                     a.FReportCount,
                     p.FAppId,
                     //企业名称
                     ToEntName = db.CF_Prj_Ent.Where(t => t.FAppId == a.FId && t.FEntType == 145).Select(t => t.FName).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FPrjId == a.FPrjId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).
                     GetValueOrDefault() > a.FCreateTime
                 });
        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            if (ddlFState.SelectedValue == "1")
            {
                v = v.Where(t => t.FState != 0 && t.FState != 6);
            }
            else
            {
                v = v.Where(t => t.FState.ToString() == ddlFState.SelectedValue);
            }
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }

    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            ProjectDB db = new ProjectDB();
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));//CF_Prj_Data.ID 
            string FState = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FState"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjName"));
            string FReportCount = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FReportDate"));//提交时间
            DateTime FAppDate = EConvert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "FAppDate"));//受理时间
            string FAppId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FAppId"));
            string ToEntName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "ToEntName"));

            //操作按钮
            LinkButton btnOp = (LinkButton)e.Item.FindControl("btnOp");
            LinkButton btnRe = ((LinkButton)e.Item.FindControl("btnRe"));

            //提交状态
            if (FState != "0")
            {
                e.Item.Cells[2].Text = "<font color='green'>已提交</font>";
                if (FState == "6")
                {
                    e.Item.Cells[2].Text = "<font color='green'>已确认</font>";
                }
            }
            else
            {
                e.Item.Cells[2].Text = "未提交";
            }

            bool isReApp = false;//需要重新办理的
            bool isNoFirst = false;//是否为二审（一审以后的，二审、三审。程序性审查、技术性审、人没审查没通过的，不含人员补正材料）


            //合同备案单位
            string s = "合同提交单位：<font color='#378BB0'>" + ToEntName + "</font>";

            //受理状态
            s += "</br>(1)合同确认：";
            switch (FState)
            {
                case "0"://未上报
                    s += "<font color='#888888'>未提交</font>";
                    break;
                case "1"://已上报
                    s += "<font color='#888888'>还未办理</font>";
                    break;
                case "2"://被退回
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>退回，补充材料</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";
                    isReApp = true;
                    break;
                case "6"://已办结
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='green'>已确认</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";
                    break;
                case "7"://不予接受
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCWJSCWTSL/Report.aspx?FAppId=" + FAppId + "',600,400);\"><font color='red'>不予接受</font> <font color='#666666'>[" + FAppDate.ToString("yyyy-MM-dd") + "]</font></a>";
                    isReApp = true;
                    break;
            }

            //操作
            if (FState == "0")
            {//没上报可删除
                btnOp.Text = "删除";
                btnOp.CommandName = "Del";
                btnOp.ToolTip = "删除业务";
                btnOp.Attributes["onclick"] = "return confirm('确定要删除该业务吗？')";
            }
            else if (FState == "1")
            {//刚上报还没有人批时可以撤消
                btnOp.Text = "撤消合同";
                btnOp.CommandName = "Back";
                btnOp.ToolTip = "撤消合同";
                btnOp.Attributes["onclick"] = "return confirm('确定要撤消吗？')";
            }
            else if (FState == "2")
            {//被退回了
                isReApp = true;
            }
            else if (FState == "6")
            {//其它情况都不允许操作
                btnOp.Text = "--";
                btnOp.CommandName = "";
                btnOp.Attributes["onclick"] = "return false;";
            }

            //合同备案情况 
            var JZHTBA = (from dd in db.CF_Prj_Data
                          join tt in db.CF_App_List on dd.FAppId equals tt.FId
                          where dd.FPriItemId == FID && tt.FManageTypeId == 413//审查合同
                          orderby tt.FCreateTime descending
                          select new
                          {
                              tt.FState,
                              //查询备案通过没
                              FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                          }).FirstOrDefault();

            s += "</br>(2)合同备案：";
            if (JZHTBA != null)
            {
                if (JZHTBA.FState == 6)
                {
                    if (JZHTBA.FResultInt == 1)
                        s += "<font color='green'>已办结，已同意</font>";
                    else
                    {
                        s += "<font color='red'>已办结，未同意</font>";

                        isReApp = true;//需要重新办理
                    }
                }
                else
                {
                    s += "<font color='blue'>正在办理</font>";
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";


            //程序性审查、技术性审查、勘察文件审查备案 结果
            var v = (from a in db.CF_App_List
                     join id in db.CF_App_Idea.Where(t => t.FUserId == null || t.FType == 290) on a.FId equals id.FLinkId into idea
                     from i in idea.DefaultIfEmpty()
                     where a.FLinkId == FLinkId && (a.FManageTypeId == 28801 || a.FManageTypeId == 28803 || a.FManageTypeId == 290)
                     select new
                     {
                         a.FId,
                         a.FState,
                         a.FManageTypeId,
                         a.FAppDate,
                         a.FCreateTime,
                         iDeaFID = i == null ? "" : i.FId,
                         FResult = i == null ? "" : i.FResult,
                         FResultInt = i == null ? 0 : i.FResultInt,
                         FAppTime = i == null ? DateTime.Now : i.FAppTime,
                         //技术性审查时，查人员是否有“补正材料”的意见
                         empIdea = (from em in db.CF_Emp_BaseInfo
                                    join id in db.CF_App_Idea on em.FId equals id.FUserId
                                    where id.FLinkId == a.FId && id.FResultInt == 7 && a.FManageTypeId == 28803
                                    select new
                                    {
                                        em.FId,
                                        em.FName,
                                        id.FAppTime
                                    }).FirstOrDefault()
                     }).ToList();

            //程序性审查
            s += "</br>(3)程序性审查：";
            var v28801 = v.Where(t => t.FManageTypeId == 28801).FirstOrDefault();
            if (v28801 != null)
            {
                if (!string.IsNullOrEmpty(v28801.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCCXXSC/Report.aspx?FAppId=" + v28801.FId + "',700,480);\">";
                    if (v28801.FResultInt == 1)//合格
                    {
                        s += "<font color='green'>" + v28801.FResult + "</font>";
                    }
                    else//不合格
                    {
                        s += "<font color='red'>" + v28801.FResult + "</font>";
                        isReApp = true;
                    }
                    s += " <font color='#666666'>[" + v28801.FAppTime.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //技术性审查
            s += "</br>(4)技术性审查：";
            var v28803 = v.Where(t => t.FManageTypeId == 28803).FirstOrDefault();
            if (v28803 != null)
            {

                if (!string.IsNullOrEmpty(v28803.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v28803.FId + "',700,680);\">";
                    if (v28803.FResultInt == 6)//合格
                    {
                        s += "<font color='green'>合格</font>";
                    }
                    else if (v28803.FResultInt == 7)//补正材料
                    {
                        s += "<font color='red'>补正材料</font>";
                        isReApp = true;
                    }
                    else if (v28803.FResultInt == 3)//不合格
                    {
                        s += "<font color='red'>不合格</font>";
                        isReApp = true;
                        isNoFirst = true;//☆★☆现在只有技术性审查为不合格时才二审。☆★☆
                    }
                    s += " <font color='#666666'>[" + v28803.FAppTime.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font></a>";

                //技术性审查时，查人员是否有“补正材料”的意见
                if (v28803.empIdea != null)
                {
                    s += "</br><tt style='padding-left:90px;'>（审查人“" + v28803.empIdea.FName + "”：补正材料）</tt>";

                    isReApp = true;
                }
            }
            else
                s += "<font color='#888888'>未开始</font>";

            //审查意见回复
            s += "</br>&nbsp;&nbsp;&nbsp;审查意见回复：";
            var r = db.CF_Prj_Reply.Where(t => t.FType == 1 && t.FLinkId == FLinkId).FirstOrDefault();
            if (r != null)
            {
                if (r.FState > 0)
                {
                    s += "<a href=\"javascript:showAddWindow('../../JSDW/ApplySGTSCYJHF/Reply.aspx?FID=" + r.FID + "',700,500);\"  style=\"text-decoration:underline;\">";
                    s += "<font color='green'>已回复</font>";
                    s += " <font color='#666666'>[" + r.FDate.Value.ToString("yyyy-MM-dd") + "]</font>";
                    s += "</a>";
                }
                else
                {
                    s += "<font color='#888888'>未回复</font>";
                }
            }
            else
            {
                s += "<font color='#888888'>未开始</font>";
            }


            //勘察文件审查备案
            s += "</br>(5)勘察文件审查备案：";
            var v290 = v.Where(t => t.FManageTypeId == 290).FirstOrDefault();
            if (v290 != null)
            {
                if (!string.IsNullOrEmpty(v290.iDeaFID))
                {
                    s += "<a style=\"text-decoration:underline;\" href=\"javascript:showAddWindow('ShowAppInfo.aspx?FID=" + v290.FId + "',500,350);\">";
                    if (v290.FResultInt == 1)//同意备案（管理部门审核）
                    {
                        s += "<font color='green'>" + v290.FResult + "</font>";
                    }
                    else//不同意（管理部门审核）
                    {
                        s += "<font color='red'>" + v290.FResult + "</font>";
                        isReApp = true;
                    }
                    s += " <font color='#666666'>[" + v290.FAppTime.Value.ToString("yyyy-MM-dd") + "]</font></a>";
                }
                else
                    s += "<font color='blue'>正在办理</font>";
            }
            else
                s += "<font color='#888888'>未开始</font>";


            //办理结果
            e.Item.Cells[4].Text = s;


            //需要重新办理合同备案申请的。
            if (isReApp)
            {
                bool isOld = EConvert.ToBool(DataBinder.Eval(e.Item.DataItem, "isOld"));
                //先验证是否正在重办
                //验证是否正在重办勘察280
                int kn = (from t in db.CF_App_List
                          join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                          where d.FPriItemId == FLinkId && t.FManageTypeId == 280
                          select t.FId).Count();
                if (kn > 0)
                {
                    btnOp.Text = "<font color='#666666'>已重办勘察</font>";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                }
                //验证是否正在重办审查287
                int sn = (from t in db.CF_App_List
                          join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                          where d.FPriItemId == FLinkId && t.FManageTypeId == 287
                          select t.FId).Count();
                if (sn > 0)
                {
                    btnOp.Text = "<font color='#666666'>已重办审查</font>";
                    btnOp.CommandName = "";
                    btnOp.Attributes["onclick"] = "return false;";
                }
                if (kn == 0 && sn == 0)
                {
                    if (!isOld)
                    {
                        btnOp.Text = "重办审查";
                        btnOp.CommandName = "ReApp";
                        btnOp.CommandArgument = FID + "," + FPrjId + "," + FPrjName + "," + FReportCount + "," + (isNoFirst ? "1" : "0");
                        btnOp.Attributes["onclick"] = "";

                        btnRe.Text = "重办勘察";
                        btnRe.CommandName = "ReAppKC";
                        btnRe.CommandArgument = FPrjId + "," + FLinkId;
                        btnRe.Attributes["onclick"] = "";
                        btnRe.Visible = true;
                    }
                    else
                    {
                        btnOp.Text = "--";
                        btnOp.CommandName = "";
                        btnOp.Attributes["onclick"] = "return false;";
                    }
                }
            }

            //是否二审。
            int n = EConvert.ToInt(FReportCount);
            if (n > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + n + "审)");

            }
            //查询项目的变更时间
            var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                .Select(t => new { t.FIsBG, t.FBGTime, t.FCount }).FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(t => t.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
                btnRe.Visible = false;
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "return false;";
                btnRe.Visible = false;
            }
        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "See") //打开业务
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 2)
            {
                pageTool tool = new pageTool(this);
                string FAppId = s[0];
                string FIsApprove = EConvert.ToInt(s[1]) > 0 ? "1" : "0";

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = fMType;
                Session["FIsApprove"] = FIsApprove;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
        }
        else if (e.CommandName == "Del") //删除业务
        {
            string FAppId = e.CommandArgument.ToString();
            DelApp(FAppId);
        }
        else if (e.CommandName == "Back") //撤消提交到其他企业的业务
        {
            string FAppId = e.CommandArgument.ToString();
            GetBack(FAppId);
        }
        else if (e.CommandName == "ReApp") //重新办理
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 5)
            {
                string OldFAppId = s[0];
                string FPrjId = s[1];
                string FPrjName = s[2];
                string FReportCount = s[3];//已经是第几次申请了
                string isNoFirst = s[4];//已经是第几次申请了


                //验证是不是正在做2次勘察
                //先查出最后一次勘察，并查出做过几次了。
                var v = (from t in db.CF_App_List
                         join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                         where t.FPrjId == FPrjId && t.FManageTypeId == 280
                         orderby t.FCreateTime descending
                         select new
                         {
                             t.FId,
                             t.FCount,
                             t.FState,
                             t.FLinkId,
                             d.FPrjName,
                             dfLinkId = d.FLinkId,
                             //查出成果移交业务
                             app = db.CF_App_List.Where(a => t.FLinkId == t.FLinkId && a.FManageTypeId == 284).Select(a => t.FState).FirstOrDefault()
                         }).FirstOrDefault();
                if (v != null && v.FState == 6 && v.app != null && v.app == 6)
                {
                    t_OldFAppId.Value = OldFAppId;
                    t_FPrjId.Value = FPrjId;
                    txtFPrjName.Text = FPrjName;
                    appTab.Visible = false;
                    applyInfo.Visible = true;
                    ViewState["FMNUMBER"] = fMType;
                    t_FYear.Text = DateTime.Now.Year.ToString();
                    t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
                    if (isNoFirst == "1")//二审
                        t_FReportCount.Value = EConvert.ToString(EConvert.ToInt(FReportCount) + 1);
                    else
                        t_FReportCount.Value = FReportCount;
                    t_FLinkId.Value = v.dfLinkId;//记录原来关联的勘察信息
                }
                else
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.showMessage("该项目正在做勘察" + (v.FCount > 1 ? "（" + v.FCount + "次）" : "") + "，请在勘察成果移交后再做审查业务。");
                }
            }

        }
        else if (e.CommandName == "ReAppKC") //重办勘察
        {
            string[] s = e.CommandArgument.ToString().Split(',');
            if (s.Length == 2)
            {
                string FPrjId = s[0];
                string FLinkId = s[1];
                Response.Redirect("AppXMKCWT.aspx?FPrjId=" + FPrjId + "&FLinkId=" + FLinkId);//到勘察合同备案页面重做勘察(2次勘察)
            }
        }
    }

    /// <summary>
    /// 删除业务
    /// </summary>
    /// <param name="FAppId">CF_App_List.FID</param>
    private void DelApp(string FAppId)
    {
        pageTool tool = new pageTool(this.Page);
        if (!string.IsNullOrEmpty(FAppId))
        {
            ProjectDB db = new ProjectDB();
            //业务
            CF_App_List App = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
            if (App != null)
                db.CF_App_List.DeleteOnSubmit(App);

            //基本信息
            CF_Prj_Data Data = db.CF_Prj_Data.Where(t => t.FAppId == FAppId).FirstOrDefault();
            if (Data != null)
                db.CF_Prj_Data.DeleteOnSubmit(Data);

            //相关企业
            IQueryable<CF_Prj_Ent> Ent = db.CF_Prj_Ent.Where(t => t.FAppId == FAppId);
            if (Ent != null)
                db.CF_Prj_Ent.DeleteAllOnSubmit(Ent);

            //附件
            IQueryable<CF_AppPrj_FileOther> oFile = db.CF_AppPrj_FileOther.Where(t => t.FAppId == FAppId);
            if (oFile != null)
                db.CF_AppPrj_FileOther.DeleteAllOnSubmit(oFile);

            //提交删除
            db.SubmitChanges();
            tool.showMessage("删除成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("删除失败");
        }
    }

    /// <summary>
    /// 撤消
    /// </summary>
    /// <param name="FAppId">CF_App_List.FID</param>
    private void GetBack(string FAppId)
    {
        pageTool tool = new pageTool(this.Page);
        ProjectDB db = new ProjectDB();
        //业务
        CF_App_List App = db.CF_App_List.Where(t => t.FId == FAppId).FirstOrDefault();
        if (App != null)
        {
            App.FState = 0;
            db.SubmitChanges();
            tool.showMessage("撤消成功");
            ShowInfo();
        }
        else
        {
            tool.showMessage("撤消失败");
        }
    }

    //创建业务
    private void SaveInfo(string fmtnumber)
    {
        //----------------------------------------------------------------
        //2014-04-09 SHL：
        //修改说明：审查委托由原来的从“项目登记”选，改为从“项目勘察”业务选；
        //          这样关联审查业务中的设计单位才能准确，设计（勘察）单位才能确定哪个审查文件应该由我回复。
        //----------------------------------------------------------------

        pageTool tool = new pageTool(this.Page);
        if (this.Session["FBaseId"] == null)
            return;
        //查询如果已经有了，则不再创建
        if (string.IsNullOrEmpty(t_FPrjId.Value))
        {
            tool.showMessage("请先选择项目！");
            return;
        }
        //验证前置条件（勘察成果移交、见证报告备案 都已完成）
        var v = from t in db.CF_App_List
                where t.FPrjId == t_FPrjId.Value
                && (((t.FManageTypeId == 28003
                || t.FManageTypeId == 28001
                || t.FManageTypeId == 284))//勘察成果移交、见证报告备案
                    || t.FManageTypeId == fMType)//本业务
                select new { t.FId, t.FManageTypeId, t.FState };
        if (v.Count(t => t.FManageTypeId == 28001) > 0)//如果选择了见证单位
        {
            if (v.Count(t => t.FManageTypeId == 284
                && t.FState == 6) < 1)//勘察成果移交没有完成
            {
                tool.showMessage("该项目“勘察成果移交”业务未完成，无法办理此业务。");
                return;
            }
            if (v.Count(t => t.FManageTypeId == 28003
                && t.FState == 6) < 1)//见证报告没有完成
            {
                tool.showMessage("该项目“见证报告备案”业务未完成，无法办理此业务。");
                return;
            }
        }
        else
        {
            if (v.Count(t => t.FManageTypeId == 284
                    && t.FState == 6) < 1)//勘察成果移交没有完成
            {
                tool.showMessage("该项目“勘察成果移交”业务未完成，无法办理此业务。");
                return;
            }
        }

        DateTime dTime = DateTime.Now;
        string FAppId = Guid.NewGuid().ToString();
        CF_App_List app = new CF_App_List();//业务
        app.FId = FAppId;
        app.FLinkId = FAppId;
        app.FPrjId = t_FPrjId.Value;
        app.FName = t_FName.Text.Trim();
        app.FManageTypeId = fMType;
        app.FwriteDate = dTime;
        app.FState = 0;
        app.FIsDeleted = false;
        app.FYear = EConvert.ToInt(t_FYear.Text.Trim());
        app.FMonth = DateTime.Now.Month;
        app.FBaseName = CurrentEntUser.EntName;
        app.FBaseinfoId = CurrentEntUser.EntId;
        app.FTime = dTime;
        app.FCreateTime = dTime;
        app.FReportCount = EConvert.ToInt(t_FReportCount.Value);
        db.CF_App_List.InsertOnSubmit(app);

        CF_Prj_Data data = new CF_Prj_Data();

        //判断是否为二审要导入原业务的信息
        if (app.FReportCount > 1 && !string.IsNullOrEmpty(t_OldFAppId.Value))
        {
            string oldFAppId = t_OldFAppId.Value;
            //导入主表
            CF_Prj_Data olddata = db.CF_Prj_Data.Where(t => t.FAppId == oldFAppId).FirstOrDefault();
            if (olddata != null)
            {
                //data = olddata.Copy(data);
                //但是要清空数据意见信息
                data.FTxt10 = olddata.FTxt10;
                data.FTxt11 = olddata.FTxt11;
            }
            //导入企业表
            IQueryable<CF_Prj_Ent> oldEnt = db.CF_Prj_Ent.Where(t => t.FAppId == oldFAppId);
            foreach (CF_Prj_Ent t in oldEnt)
            {
                CF_Prj_Ent ent = new CF_Prj_Ent();
                db.CF_Prj_Ent.InsertOnSubmit(ent);
                ent = t.Copy(ent);
                ent.FId = Guid.NewGuid().ToString();
                ent.FAppId = FAppId;
                ent.FTime = dTime;
                ent.FCreateTime = dTime;
                ent.FIsDeleted = false;
            }
            //导入附件
            IQueryable<CF_AppPrj_FileOther> oldFile = db.CF_AppPrj_FileOther.Where(t => t.FAppId == oldFAppId);
            foreach (CF_AppPrj_FileOther t in oldFile)
            {
                CF_AppPrj_FileOther file = new CF_AppPrj_FileOther();
                db.CF_AppPrj_FileOther.InsertOnSubmit(file);
                file = t.Copy(file);
                file.FId = Guid.NewGuid().ToString();
                file.FAppId = FAppId;
                file.FTime = dTime;
                file.FCreateTime = dTime;
                file.FIsDeleted = false;
            }
        }
        data.FId = FAppId;
        data.FAppId = FAppId;
        data.FPrjId = t_FPrjId.Value;
        data.FPrjName = txtFPrjName.Text.Trim();
        data.FBaseInfoId = CurrentEntUser.EntId;
        data.FType = fMType;
        data.FTime = dTime;
        data.FCreateTime = dTime;
        data.FIsDeleted = false;
        data.FPriItemId = t_OldFAppId.Value; //关联哪个业务重办的
        data.FLinkId = t_FLinkId.Value; //关联勘察业务
        db.CF_Prj_Data.InsertOnSubmit(data);

        db.SubmitChanges();

        Session["FAppId"] = FAppId;
        Session["FManageTypeId"] = fMType;
        Session["FIsApprove"] = 0;
        tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        this.appTab.Visible = false;
        this.applyInfo.Visible = true;
        this.ViewState["FMNUMBER"] = fMType;
        t_FYear.Text = DateTime.Now.Year.ToString();
        t_FName.Text = t_FYear.Text + "年 " + db.getManageTypeName(fMType);
        t_FReportCount.Value = "1";
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (this.ViewState["FMNUMBER"] == null)
        {
            return;
        }
        pageTool tool = new pageTool(this.Page);
        SaveInfo(this.ViewState["FMNUMBER"].ToString());
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        this.appTab.Visible = true;
        this.applyInfo.Visible = false;
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void btnSel_Click(object sender, EventArgs e)
    {
        //选择时，要还原。以免冲突错误 
        t_OldFAppId.Value = "";
        t_FReportCount.Value = "1";

        var v = (from t in db.CF_Prj_Data
                 where t.FId == t_FLinkId.Value
                 select new { t.FPrjName, t.FPrjId }).FirstOrDefault();
        if (v != null)
        {
            txtFPrjName.Text = v.FPrjName;
            t_FPrjId.Value = v.FPrjId;
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
