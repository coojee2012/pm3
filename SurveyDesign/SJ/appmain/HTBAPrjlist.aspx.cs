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

public partial class SJ_AppMain_HTBAPrjlist : System.Web.UI.Page
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


        int fMType = 414;//合同备案

        ViewState["fMType"] = fMType;

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
                 where d.FIsDeleted == false && (a.FManageTypeId == fMType) && a.FIsDeleted == false
                    && ((db.CF_Prj_Ent.Count(t => t.FAppId == a.FId && (t.FEntType == 155 || t.FEntType == 15503) && t.FBaseInfoId == FBaseinfoID) > 0))
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
                     d.FTxt7,//主勘承包人
                     d.FDate1,
                     a.FIsDeleted,
                 });

        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));

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

            //判断是不是从上面的业务来的。 
            if (!string.IsNullOrEmpty(FPriItemId))
            {//确认合同备案后自动创建
                ((Literal)e.Row.FindControl("lit_TS")).Text += "<img src=\"../../image/tip.gif\" style=\"cursor:pointer\" title=\"确认合同备案后自动创建，可删除，删除后可恢复\"/>";
            }

            switch (FState)
            {
                case "0"://未上报 
                    t = "<font color='#888888'>--</font>";
                    s = "<font color='#888888'>未上报</font>";
                    break;
                case "1"://已上报 
                    t = FReportDate.ToShortDateString();
                    //初步设计文件审查申报（是报主管部门审批的）
                    RApp ra = new RApp();
                    if (ra.isBeginApp(FID))
                    {
                        s = "<font color='blue'>已上报，已开始审核</font>";
                        s = "<a href=\"javascript:showAddWindow('../main/ShowAppInfo1.aspx?FID=" + FID + "',760,400);\">" + s + "</a>";
                    }
                    else
                    {
                        s = "<font color='444444'>已上报，还未审核</font>";
                    }
                    break;
                case "2"://被退回  
                    t = FReportDate.ToShortDateString();
                    s = "<a href=\"javascript:showApproveWindow('../main/ShowAppInfo1.aspx?fid=" + FID + "',534,600);\"><font color='red'>退回</font></a>";
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
                    break;
            }

            e.Row.Cells[4].Text = t;
            e.Row.Cells[5].Text = s;

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

                Session["FAppId"] = FAppId;
                Session["FManageTypeId"] = FManageTypeId;
                Session["FIsApprove"] = 1;

                tool.ExecuteScript("top.document.location='../Appmain/aindex.aspx';");
            }
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

}
