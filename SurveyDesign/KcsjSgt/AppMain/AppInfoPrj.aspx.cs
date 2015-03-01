using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Text;
using Approve.RuleCenter;
public partial class KC_appmain_AppInfoPrj : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    int[] types = { 287, 28801, 28803, 290 };
    int fMType6 = 290;//勘察文件审查备案 
    int jMType6 = 305;//施工图设计文件备案
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
            showInfo();
        }
    }
    void BindControl()
    {
        string fType = Request.QueryString["FType"];
        switch (fType)
        {
            case "F1":
                fType = "项目合同备案_受理";
                break;
            case "F4":
                fType = "程序性审查_合格";
                break;
            case "F5":
                fType = "技术性审查_合格";
                break;
            case "F6":
                fType = "审查备案_通过";
                break;
            case "F7":
                fType = "打印合格证";
                break;
            case "F8":
                fType = "打印告知书";
                break;

            case "J1":
                fType = "项目合同备案_不予受理";
                break;
            case "J4":
                fType = "程序性审查_不合格";
                break;
            case "J5":
                fType = "技术性审查_不合格";
                break;
            case "J6":
                fType = "审查备案_不通过";
                break;
        }
        lTitle.Text = fType;
    }
    //显示 
    void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string fType = Request.QueryString["FType"];
        string[] fMType = Request.QueryString["FMType"].Split(',');
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        string fResult = string.Empty;
        switch (fType)
        {
            case "F1":
                fResult = "<font color='green'>已受理</font>";
                break;
            case "F4":
            case "F5":
                fResult = "<font color='green'>合格</font>";
                break;
            case "F6":
                fResult = "<font color='green'>已通过</font>";
                break;
            case "J1":
                fResult = "<tt>不予受理</tt>";
                break;
            case "J4":
            case "J5":
                fResult = "<tt>不合格</tt>";
                break;
            case "J6":
                fResult = "<tt>不通过</tt>";
                break;
        }
        int isBA = 0;
        int iBAState = fType.Substring(0, 1) == "F" ? 1 : 3;
        int iState = fType.Substring(0, 1) == "F" ? 6 : 3;
        if (fMType.Contains(fMType6.ToString()))
        {
            iState = 6;
            isBA = 1;
        }
        if (fType == "J1")
            iState = 7;//不予受理
        if (!(fType == "F7" || fType == "F8"))
        {
            var App = from l1 in db.CF_App_List
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where fMType.Contains(l1.FManageTypeId.ToString())
                      && (l1.FToBaseinfoId == FBaseinfoID
                      || l1.FBaseinfoId == FBaseinfoID)
                      && l1.FAppDate.Value.Year == FYear
                      && (FMonth > 0 ? l1.FAppDate.Value.Month == FMonth : true)
                      && l1.FState == iState
                      && (isBA == 1 ? (from i in db.CF_App_Idea
                                       join b in db.CF_App_List
                                       on i.FLinkId equals b.FId
                                       where b.FLinkId == l1.FLinkId
                                       && fMType.Contains(b.FManageTypeId.ToString())
                                       orderby i.FTime
                                       select i.FResultInt).FirstOrDefault() == iBAState : true)
                      orderby l1.FAppDate descending
                      select new
                      {
                          l1.FLinkId,
                          l1.FManageTypeId,
                          MType = db.CF_Sys_ManageType.Where(t => t.FNumber == l1.FManageTypeId).Select(t => t.FName).FirstOrDefault(),
                          l1.FAppDate,
                          FUrl = types.Contains(l1.FManageTypeId.Value) ? "F" : "",
                          p.FPrjName,
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          fState = fResult,
                          FReportCount = db.CF_App_List.Where(li => li.FLinkId == l1.FLinkId && (li.FManageTypeId == 300 || li.FManageTypeId == 287)).Select(li => li.FReportCount).FirstOrDefault()
                      };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else
        {
            dg_List.Visible = false;
            dg_List2.Visible = true;
            var App = from l1 in db.CF_App_List
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where fMType.Contains(l1.FManageTypeId.ToString())
                      && l1.FBaseinfoId == FBaseinfoID
                      && l1.FIsSign == 1
                      && l1.FToBaseinfoId != null && l1.FToBaseinfoId != ""
                      && Convert.ToDateTime(l1.FToBaseinfoId).Year == FYear
                      && (FMonth > 0 ? Convert.ToDateTime(l1.FToBaseinfoId).Month == FMonth : true)
                      && l1.FState == iState
                      orderby l1.FToBaseinfoId descending
                      select new
                      {
                          l1.FLinkId,
                          MType = types.Contains(l1.FManageTypeId.Value) ? "勘察" : "设计",
                          l1.FToBaseinfoId,
                          FUrl = types.Contains(l1.FManageTypeId.Value) ? "F" : "",
                          p.FPrjName,
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          l1.FResult,
                          FReportCount = db.CF_App_List.Where(li => li.FLinkId == l1.FLinkId && (li.FManageTypeId == 300 || li.FManageTypeId == 287)).Select(li => li.FReportCount).FirstOrDefault()
                      };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List2.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List2.DataBind();
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Item.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }
        }
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
