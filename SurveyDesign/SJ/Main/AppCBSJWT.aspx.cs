﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Approve.RuleCenter;
using Approve.Common;
using Approve.EntityBase;
using Approve.EntitySys;
using Approve.RuleApp;
using ProjectData;
using System.Linq;
using ProjectBLL;
public partial class EvaluateEntApp_main_AppCBSJWT : System.Web.UI.Page
{
    RApp ra = new RApp();
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    public int fMType = 291;//初步设计合同备案确认
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


    //显示
    private void ShowInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in db.CF_App_List
                join d in db.CF_Prj_Data on t.FLinkId equals d.FId
                where t.FToBaseinfoId == FBaseinfoID && t.FManageTypeId == fMType && t.FState > 0
                orderby t.FCreateTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    t.FLinkId,
                    d.FPrjName,
                    d.FPriItemId,
                    t.FName,
                    t.FCreateTime,
                    t.FCount,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FBaseName = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                    t.FAppDate
                };

        //查询条件
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
        {
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(ddlFState.SelectedValue))
        {
            int fState = EConvert.ToInt(ddlFState.SelectedValue);
            v = v.Where(t => t.FState == fState);
        }
        if (!string.IsNullOrEmpty(drop_FYear.SelectedValue))
            v = v.Where(t => t.FYear.ToString() == drop_FYear.SelectedValue);

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }


    //列表
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = e.Item.Cells[e.Item.Cells.Count - 1].Text;
            int FState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));

            string ReportServer = rc.GetSysObjectContent("_ReportServer");
            if (string.IsNullOrEmpty(ReportServer))
            {
                ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
            }

            string s = "";
            string o = "<a href='javascript:showAddWindow(\"../applycbsjwt/Report.aspx?FAppId=" + FID + "\",700,480);'>";
            //string Url = ReportServer + "&FAppId=" +FID;
            string t = "";
            switch (FState)
            {
                case 1:
                    s = "<font color='#888888'>还未确认</font>";
                    e.Item.Cells[4].Text = "<font color='#888888'>--</font>";
                    o += "确认合同";
                    t += "<font color='#888888'>还未确认</font>";
                    break;
                case 2:
                    s = "<a href=\"javascript:showAddWindow('LookIdea.aspx?FAppId=" + FID + "',500,350);\"><font color='red'>已退回</font></a>";
                    o += "查看详情";
                    t += "<font color='#888888'>已退回</font>";
                    break;
                case 6:
                    s = "<font color='green'>已确认</font>";
                    o += "查看详情";
                    t += "<a href='" + ReportServer + "SLD-CBSJ.cpt&FAppId=" + FID + "' target='_blank'>打印合同确认单</a>";
                    break;
                case 7:
                    s = "<a href=\"javascript:showAddWindow('LookIdea.aspx?FAppId=" + FID + "',500,350);\"><font color='red'>不予接受</font></a>";
                    o += "查看详情";
                    t += "<font color='#888888'>不予接受</font>";
                    break;
            }

            e.Item.Cells[5].Text = s;
            e.Item.Cells[6].Text = o + "</a>";
            e.Item.Cells[7].Text = t;

            //是否二次。
            int n = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FCount"));
            if (n > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + n + "次)");
            }

            //查询项目的变更时间 
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            var prjBG = db.CF_Prj_BaseInfo.Where(st => st.FId == FPrjId)
                .Select(st => new { st.FIsBG, st.FBGTime, st.FCount })
                .FirstOrDefault();
            if (prjBG.FCount > 0)
            {
                ((Literal)e.Item.FindControl("prj_Count")).Text = ("<br/>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")");
            }
            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Count(st => st.FPrjId == FPrjId) > 0;
            if (stop)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
            }
            else if (prjBG.FIsBG == 1)//判断该项目是否变更
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;color:#999999;";
                e.Item.ToolTip = "该项目已经做了变更，变更前的业务停止进行。";
            }
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
