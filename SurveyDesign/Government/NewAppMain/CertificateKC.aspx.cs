using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleCenter;

public partial class Government_NewAppMain_CertificateKC : System.Web.UI.Page
{
    int fMType = 28803;//	技术性审查(勘察)--同意的数据 
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
            ShowPostion();
        }
    }
    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }
    ProjectDB db = new ProjectDB();
    //显示
    private void showInfo()
    {

        var v = from t in db.CF_App_List
                where t.FManageTypeId == fMType && t.FState == 6
                && t.FIsSign != 4
                orderby t.FTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FJSEnt = "",
                    FKCEnt = "",
                    t.FResult,
                    t.FAppDate,
                    t.FLinkId,
                    t.FReportCount
                };
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        if (txtFReportDate.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate >= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            v = v.Where(t => t.FAppDate <= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }



    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
    protected void btnOut_Click(object sender, EventArgs e)
    {
        Pager1.CurrentPageIndex = 1;
        Pager1.PageSize = Pager1.RecordCount;

        string fOutTitle = lPostion.Text;
        Approve.Common.SaveAsBase sab = new Approve.Common.SaveAsBase();
        showInfo();
        sab.SaveAsExc(this.DG_List, fOutTitle, this.Response, "gb2312", 0);
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.PageCount * (this.Pager1.CurrentPageIndex - 1)).ToString();

            string FID = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            string FPrjId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjId"));
            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }

            var JSEnt = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == FPrjId)
                .Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => new { b.FName, b.FId }).FirstOrDefault();

            if (JSEnt != null)
            {
                string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=100 ");

                sUrl += "?fbid=" + JSEnt.FId;

                e.Item.Cells[2].Text = "<a href='#' class='link5' onclick=\"showAddWindow('" + sUrl + "',980,450)\"  >" + JSEnt.FName + "</a>";
            }
            var KCEnt = db.CF_Prj_Ent.Where(b => (db.CF_App_List.Where(l => l.FManageTypeId == 280 && l.FPrjId == FPrjId)
                .Select(l => l.FLinkId).Contains(b.FAppId)) && b.FEntType == 15501).Select(b => new { b.FBaseInfoId, b.FName }).FirstOrDefault();
            if (KCEnt != null)
            {
                string sUrl = rc.GetSignValue("select FDAQurl from cf_Sys_SystemName where fnumber=15501 ");

                sUrl += "?fbid=" + KCEnt.FBaseInfoId;

                e.Item.Cells[3].Text = "<a href='#' class='link5' onclick=\"showAddWindow('" + sUrl + "',800,600)\"  >" + KCEnt.FName + "</a>";
            }
            CF_Prj_Certi Certi = db.CF_Prj_Certi.Where(t => t.FAppId == (db.CF_App_List.Where(l => l.FManageTypeId == 290 && l.FPrjId == FPrjId).Select(l => l.FId).FirstOrDefault())).FirstOrDefault();
            if (Certi != null)
            {
                e.Item.Cells[6].Text = EConvert.ToShortDateString(Certi.FAppDate);
                e.Item.Cells[7].Text = Certi.FCertiNo;
            }

            else
            {
                e.Item.Cells[7].Text = "-";
                e.Item.Cells[6].Text = "未备案";
            }
            //查询该项目是否变更 
            if (!string.IsNullOrEmpty(FPrjId))
            {
                var prjBG = db.CF_Prj_BaseInfo.Where(t => t.FId == FPrjId)
                    .Select(t => new { t.FBGTime, t.FCount })
                    .FirstOrDefault();
                if (prjBG != null && prjBG.FCount > 0)
                {
                    ((Literal)e.Item.FindControl("prj_Count")).Text = "<br/><font color='#000000;'>(第" + prjBG.FCount + "次变更：" + EConvert.ToShortDateString(prjBG.FBGTime) + ")</font>";
                }
            }
        }
    }
}
