using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
//using Approve.Common;
using Approve.RuleApp;
using Approve.RuleCenter;
//////
using Tools;
using System.Text;

public partial class JZDW_AppMain_XMJZBGlist : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    int fMType = 28003;//项目见证报告
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
                 where d.FIsDeleted == false && a.FIsDeleted == false
                    && a.FBaseinfoId == FBaseinfoID && a.FManageTypeId == fMType
                 orderby a.FCreateTime descending
                 select new
                 {
                     a.FName,
                     a.FManageTypeId,
                     a.FCreateTime,
                     a.FReportDate,//备案时间
                     a.FState,
                     a.FYear,
                     d.FPrjName,
                     d.FPriItemId,
                     a.FId,
                     a.FLinkId,
                     a.FPrjId,
                     a.FAppDate,
                     FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == a.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                     //勘察合同备案受理业务
                     App = db.CF_App_List.Where(t => t.FLinkId == d.FId && t.FManageTypeId == 280).FirstOrDefault(),
                     //备案号
                     NO = db.CF_Prj_Certi.Where(t => t.FAppId == a.FId).Select(t => t.FCertiNo).FirstOrDefault()
                 });

        if (!string.IsNullOrEmpty(ttFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(ttFPrjName.Text.Trim()));
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
            string FManageTypeId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FManageTypeId"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPrjId"));
            string FState = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FState"));
            string NO = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "NO"));



            //本业务办理状态
            string s = "";
            LinkButton btnOp = (LinkButton)e.Row.FindControl("btnOp");
            LinkButton btnBack = (LinkButton)e.Row.FindControl("btnBack");

            bool RE = false;
            if (FState == "0")
            {
                s = "<font color='#888888'>正在办理，未上报</font>";
                btnOp.Text = "继续办理...";

                //上报时间
                e.Row.Cells[4].Text = "<font color='#888888'>--</font>";

                //备案号
                e.Row.Cells[5].Text = "<font color='#888888'>--</font>";
            }
            else if (FState == "1")
            {
                btnOp.Text = "进入查看...";

                RApp ra = new RApp();
                if (ra.isBeginApp(FID))
                {
                    s = "<font color='#blue'>已上报，已开始审批</font>";
                }
                else
                {
                    s = "<font color='#444444'>已上报，未审批</font>";

                    btnBack.CommandArgument = FID;
                    btnBack.Visible = true;
                    btnBack.ToolTip = "撤消上报";
                    btnBack.Text = "撤消上报";
                    btnBack.Attributes["onclick"] = "return confirm('确定要撤消上报吗？')";
                }


                //备案号
                e.Row.Cells[5].Text = "<font color='#888888'>--</font>";
            }
            else if (FState == "2")
            {
                s = "<font color='red'>被退回</font>";
                btnOp.Text = "进入查看...";


                //备案号
                e.Row.Cells[5].Text = "<font color='#888888'>--</font>";
            }
            //else if (FState == "6")
            //{
            //    s = "<font color='green'>已办结</font>";
            //    btnOp.Text = "进入查看...";

            //    //备案号
            //    e.Row.Cells[5].Text = NO;
            //}


            //添加见证报告备案不通过时“重新备案”
            else if (FState == "6")
            {
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
                    e.Row.Cells[5].Text = db.CF_Prj_Certi.Where(c => c.FAppId == FID).Select(c => c.FCertiNo).FirstOrDefault();


                    if (v.FResultInt == 1)//同意备案（管理部门审核）
                    {
                        s += "，<font color='green'>" + v.FResult + "</font>";
                        btnOp.Text = "进入查看...";

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
            }
            //这部分是原来就有的
            btnOp.CommandName = "See";
            btnOp.CommandArgument = FID + "," + FManageTypeId + "," + FState;
            //////

            if (RE)//可以重新备案
            {
                btnOp.Text = "重新备案";
                btnOp.ToolTip = "重新备案";
                btnOp.CommandArgument = FID;
                btnOp.CommandName = "ReApp";
                btnOp.Attributes["onclick"] = "return confirm('确定要重新备案吗？')";
            }
            ////////////////////////////






            CF_App_List app = DataBinder.Eval(e.Row.DataItem, "app") as CF_App_List;
            if (app != null)
            {
                if (app.FState == 2)
                {
                    s += "</br><font color='red'>（已被勘察单位退回，业务终止）</font>";
                    if (FState != "6")
                        e.Row.Cells[7].Text = "<a href=\"javascript:alert('该勘察已被勘察单位退回，业务终止。无法再继续办理！');\">" + btnOp.Text + "</a>";
                }
                else if (app.FState == 7)
                {
                    s += "</br><font color='red'>（合同备案的勘察单位不予受理，业务终止）</font>";

                    if (FState != "6")
                        e.Row.Cells[7].Text = "<a href=\"javascript:alert('该勘察已被合同备案的勘察单位不予受理，业务终止。无法再继续办理！');\">" + btnOp.Text + "</a>";
                }


                //是否二次。 
                string FPriItemId = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FPriItemId"));
                if (app.FCount > 1)
                {
                    //查询出不合格的意见（从勘查文件审查业务的技术性审查28803中查）
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

            e.Row.Cells[6].Text = s;
            //查询项目的变更时间
            var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                .Select(t => new { t.FIsBG, t.FBGTime, t.FCount }).FirstOrDefault();
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
                btnOp.Attributes["onclick"] = "alert('该项目已被中止，所有业务停止进行。');return false;";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Row.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Row.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
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
        //新添加
        else if (e.CommandName == "ReApp")
        { //重新备案
            string FAppId = e.CommandArgument.ToString();
            ReApp(FAppId);
        }
    }
    //新添加功能
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

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showToDo();
    }
    #endregion

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }

}
