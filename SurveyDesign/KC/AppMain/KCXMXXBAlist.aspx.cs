using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.RuleApp;
using Approve.RuleCenter;
using Tools;
using System.Text;

public partial class KC_AppMain_KCXMXXBAlist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    int fMType = 283;
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
        //SHL：2014-01-23
        //勘察信息备案业务  CF_Prj_Data表改为单独 不与合同备案业务共用。
        //关联为CF_Prj_Data.FAppId == CF_App_List.FId
        //与主线关联仍为CF_App_List.FLinkId == (合同备案业务的)CF_Prj_Data.FID 

        string FBaseinfoID = CurrentEntUser.EntId;
        var v = (from d in db.CF_Prj_Data
                 join a in db.CF_App_List on d.FAppId equals a.FId  //
                 where d.FIsDeleted == false
                    && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == fMType
                 orderby a.FCreateTime descending
                 select new
                 {
                     a.FId,
                     DataFID = d.FId,
                     a.FManageTypeId,
                     a.FPrjId,
                     a.FLinkId,
                     a.FCreateTime,
                     a.FReportDate,
                     a.FState,
                     a.FYear,
                     a.FAppDate,
                     a.FCount,
                     a.FLinkAppId,
                     a.FIsDeleted,
                     d.FPrjName,
                     d.FPriItemId,
                     //建设单位
                     FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == a.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                     //查询见证单位受理业务情况,
                     app = db.CF_App_List.Where(l => a.FLinkId == l.FLinkId && l.FManageTypeId == 28001).FirstOrDefault(),
                     //判断是不是最新的那条
                     isOld = db.CF_App_List.Where(t => t.FLinkId == a.FLinkId && t.FManageTypeId == fMType).Max(t => t.FCreateTime).GetValueOrDefault() > a.FCreateTime
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

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showToDo();
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
            string FPrjName = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjName"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            string FLinkId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLinkId"));
            string FLinkAppId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FLinkAppId"));
            DateTime FReportDate = EConvert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FReportDate"));

            bool isOld = EConvert.ToBool(DataBinder.Eval(e.Row.DataItem, "isOld"));
            //本业务办理状态
            string t = "", s = "", n = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");
            bool RE = false;
            switch (FState)
            {
                case "0"://未上报 
                    t = "<font color='#888888'>--</font>";
                    s = "<font color='#888888'>未上报</font>";

                    btnOp.Text = "--";
                    btnOp.Enabled = false;
                    //btnOp.Text = "删除";
                    //btnOp.CommandArgument = FID + "," + FLinkAppId;
                    //btnOp.CommandName = "Del";
                    //btnOp.Attributes["onclick"] = "return confirm('确认要删除该业务吗?');";
                    break;
                case "1"://已上报 
                    t = FReportDate.ToShortDateString();
                    //初步设计文件审查申报（是报主管部门审批的）
                    RApp ra = new RApp();
                    if (ra.isBeginApp(FID))
                    {
                        s = "<font color='blue'>已上报，已开始审批</font>";
                    }
                    else
                    {
                        s = "<font color='444444'>已上报，未审批</font>";

                        btnOp.Text = "撤消上报";
                        btnOp.ToolTip = "撤消上报";
                        btnOp.CommandArgument = FID;
                        btnOp.CommandName = "Back";
                        btnOp.Attributes["onclick"] = "return confirm('确定要撤消上报吗？')";
                    }
                    break;
                case "2"://被退回
                    e.Row.Cells[7].Text = "<a>--</a>";
                    t = FReportDate.ToShortDateString();
                    s = "<a href=\"javascript:showApproveWindow('../../jsdw/main/ShowAppInfo1.aspx?FID=" + FID + "',760,600);\"><font color='red'>退回</font></a>";
                    break;
                case "6"://办结 
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

                        //备案号
                        e.Row.Cells[6].Text = db.CF_Prj_Certi.Where(c => c.FAppId == FID).Select(c => c.FCertiNo).FirstOrDefault();


                        if (v.FResultInt == 1)//同意备案（管理部门审核）
                        {
                            s += "，<font color='green'>" + v.FResult + "</font>";

                            //判断是否已完成"勘察项目人员意见28301"，否则还可以变更
                            if (!isOld && db.CF_App_List.Count(a => a.FLinkId == FLinkId && a.FManageTypeId == 28301 && (a.FState == 0)) > 0)
                            {
                                RE = true;
                            }
                            else
                            {
                                btnOp.Text = "--";
                                btnOp.Enabled = false;
                            }
                        }
                        else//不同意（管理部门审核）
                        {
                            s += "，<font color='red'>" + v.FResult + "</font>";
                            RE = true;
                        }

                    }
                    else
                    {
                        btnOp.Text = "--";
                        btnOp.Enabled = false;
                    }

                    s = "<a href=\"javascript:showAddWindow('../../jsdw/main/ShowAppInfo1.aspx?FID=" + FID + "',760,400);\">" + s + "</a>";
                    break;
            }

            if (RE)//可以重新备案
            {
                btnOp.Text = "重新备案";
                btnOp.ToolTip = "重新备案";
                btnOp.CommandArgument = FID;
                btnOp.CommandName = "ReApp";
                btnOp.Attributes["onclick"] = "return confirm('确定要重新备案吗？')";
            }


            CF_App_List app = DataBinder.Eval(e.Row.DataItem, "app") as CF_App_List;
            if (app != null && !string.IsNullOrEmpty(app.FToBaseinfoId))
            {
                if (app.FState == 1)
                {
                    ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('见证单位还未受理。无法再继续办理！');return false;";
                }
                if (app.FState == 2)
                {
                    s += "</br><font color='red'>（已被见证单位退回，业务终止）</font>";
                    if (FState != "6")
                        ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('已被见证单位退回，业务终止。无法再继续办理！');return false;";
                }
                else if (app.FState == 7)
                {
                    s += "</br><font color='red'>（见证单位不予受理，业务终止）</font>";
                    if (FState != "6")
                        ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('见证单位不予受理，业务终止。无法再继续办理！');return false;";
                }

                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
                if (app.FCount > 1)
                {
                    //查询出不合格的意见
                    var v = db.CF_App_List.Where(a => a.FLinkId == FPriItemId && a.FManageTypeId == 28803).FirstOrDefault();
                    if (v != null)
                    {
                        string txt = "<a style=\"text-decoration:underline;\" ";
                        txt += "href=\"javascript:showAddWindow('../../KcsjSgt/ApplyKCJSXSC/Report.aspx?FAppId=" + v.FId + "',700,680);\">";
                        txt += "查看审图机构意见</a>";
                        ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + app.FCount + "次," + txt + ")");
                    }
                }
            }


            e.Row.Cells[4].Text = t;
            e.Row.Cells[5].Text = s;

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
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "alert('该项目已被中止，所有业务停止进行。');return false;";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
                btnOp.Enabled = false;
                btnOp.Attributes["onclick"] = "alert('该项目已经做了变更，变更前的业务停止进行。');return false;";
            }


            int FIsDeleted = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FIsDeleted"));
            if (FIsDeleted == 1)
            {
                ((Literal)e.Row.FindControl("lit_TS")).Text = "<img title=\"已重新办理，本业务失效”\" src=\"../../image/info.jpg\" style=\"cursor:pointer;\"/>";

                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
            }

            if (FState == "0")//还没安排的就做此判断
            {
                //合同备案同况
                string DataFID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "DataFID"));
                var HTBA = (from dd in db.CF_Prj_Data
                            join tt in db.CF_App_List on dd.FAppId equals tt.FId
                            where dd.FPriItemId == FLinkId && tt.FManageTypeId == 411
                            orderby tt.FCreateTime descending
                            select new
                            {
                                tt.FState,
                                //查询备案通过没
                                FResultInt = db.CF_App_Idea.Where(i => i.FLinkId == tt.FId).OrderByDescending(i => i.FCreateTime).Select(i => i.FResultInt).FirstOrDefault(),
                            }).FirstOrDefault();

                if (HTBA != null)
                {
                    if (HTBA.FState != 6 || HTBA.FResultInt != 1)
                        ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('合同备案未完成，无法办理该业务。');return false;";
                }
                else
                    ((LinkButton)e.Row.FindControl("btnItemSee")).Attributes["onclick"] = "alert('合同备案未完成，无法办理该业务。');return false;";
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
        //else if (e.CommandName == "Del")
        //{
        //    string[] s = e.CommandArgument.ToString().Split(',');
        //    if (s.Length == 2)
        //    {
        //        string FId = s[0];
        //        string FLinkAppId = s[1];

        //        RCenter rc = new RCenter();
        //        //删除申报信息和企业数据 
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("update cf_App_list set FIsDeleted=0 where FId='" + FLinkAppId + "';");//恢复重做业务时删除的上个业务
        //        sb.Append("delete cf_App_list where FId='" + FId + "';");
        //        sb.Append("delete cf_Prj_data where FAppId='" + FId + "';");
        //        sb.Append("delete cf_Prj_Emp where FAppId='" + FId + "';");
        //        sb.Append("delete CF_AppPrj_FileOther where FAppId='" + FId + "';");
        //        rc.PExcute(sb.ToString());
        //        pageTool tool = new pageTool(this.Page);
        //        showInfo();
        //        tool.showMessage("删除成功！");
        //    }
        //}
        else if (e.CommandName == "Back")
        {   //撤消上报
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
        else if (e.CommandName == "ReApp")
        { //重新备案
            string FAppId = e.CommandArgument.ToString();
            ReApp(FAppId);
        }
    }

    //重新备案 业务创建
    private void ReApp(string OldFAppId)
    {
        pageTool tool = new pageTool(this);
        DateTime dTime = DateTime.Now;

        //查出旧的CF_App_List
        CF_App_List oldApp = db.CF_App_List.Where(t => t.FId == OldFAppId).FirstOrDefault();
        if (oldApp != null)
        {
            oldApp.FIsDeleted = true;

            //新建业务表
            string FAppId = Guid.NewGuid().ToString();
            CF_App_List app = new CF_App_List();//业务
            app.FId = FAppId;
            app.FLinkId = oldApp.FLinkId;
            app.FPrjId = oldApp.FPrjId;
            app.FName = dTime.Year + "年 " + db.getManageTypeName(fMType);
            app.FManageTypeId = fMType;
            app.FwriteDate = DateTime.Now;
            app.FState = 0;
            app.FIsDeleted = false;
            app.FYear = dTime.Year;
            app.FMonth = DateTime.Now.Month;
            app.FBaseName = CurrentEntUser.EntName;
            app.FBaseinfoId = CurrentEntUser.EntId;
            app.FTime = DateTime.Now;
            app.FCreateTime = DateTime.Now;
            app.FReportCount = 1;
            app.FCount = oldApp.FCount.GetValueOrDefault() + 1;//变更次数加1
            app.FLinkAppId = OldFAppId;//关联之前的业务
            db.CF_App_List.InsertOnSubmit(app);

            //查出旧的CF_Prj_Data
            CF_Prj_Data oldData = db.CF_Prj_Data.Where(t => t.FAppId == OldFAppId).FirstOrDefault();
            if (oldData != null)
            {
                //新建数据表
                CF_Prj_Data data = new CF_Prj_Data();
                data = oldData.Copy(data);//导入旧数据
                data.FId = FAppId;
                data.FAppId = FAppId;
                data.FTime = dTime;
                data.FCreateTime = dTime;
                data.FIsDeleted = false;
                db.CF_Prj_Data.InsertOnSubmit(data);
            }

            db.SubmitChanges();

            Session["FAppId"] = FAppId;
            Session["FManageTypeId"] = fMType;
            Session["FIsApprove"] = 0;

            tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
        }
    }


    #endregion

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
