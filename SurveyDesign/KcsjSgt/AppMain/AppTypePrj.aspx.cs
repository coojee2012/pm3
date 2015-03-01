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
public partial class KC_appmain_AppTypePrj : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
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
        string tag = string.Empty;
        if (fType.Substring(0, 1) == "F")
            tag = "--房屋建筑工程";
        else
            tag = "--市政基础工程";
        switch (fType.Substring(1))
        {
            case "1":
                fType = "合同备案项目";
                break;
            case "1_1":
                fType = "受理项目";
                break;
            case "4":
                fType = "程序性审查项目";
                break;
            case "5":
                fType = "技术性审查项目";
                break;
            case "6":
                fType = "审查备案项目";
                break;
            case "7":
                fType = "打印合格证项目";
                break;
            case "8":
                fType = "打印告知书项目";
                break;
        }
        lTitle.Text = fType + tag;
    }
    //显示 
    void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string fType = Request.QueryString["FType"];
        int iType = 2000101; ;//房屋建筑
        if (fType.Substring(0, 1) == "J")
            iType = 2000102;//市政公用 
        string fMType = Request.QueryString["FMType"];
        string[] mtypes = fMType.Split(',');
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        if (fType == "F1" || fType == "J1")//合同备案项目
        {
            var App = from l1 in db.CF_App_List
                      where mtypes.Contains(l1.FManageTypeId.ToString())
                      && l1.FToBaseinfoId == FBaseinfoID
                      && l1.FReportDate.Value.Year == FYear
                      && (FMonth > 0 ? l1.FReportDate.Value.Month == FMonth : true)
                      && l1.FState > 0
                      orderby l1.FPrjId descending
                      group l1 by l1.FPrjId
                          into g
                          join p in db.CF_Prj_BaseInfo on g.Key equals p.FId
                          where p.FType == iType
                          select new
                          {
                              FLinkId = p.FId,
                              FPrjId = p.FId,
                              p.FPrjName,
                              FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                              FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                              FMoney = ""
                          };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else if (fType == "F7" || fType == "J7" || fType == "F8" || fType == "J8")
        {
            int fstate = 6;
            if (fType == "F8" || fType == "J8")
                fstate = 3;//告知书 不合格
            var App = from l1 in db.CF_App_List
                      where mtypes.Contains(l1.FManageTypeId.ToString())
                      && l1.FState == fstate && l1.FIsSign == 1//已打印
                      && l1.FBaseinfoId == FBaseinfoID
                      && l1.FReportDate.Value.Year == FYear
                     && (FMonth > 0 ? l1.FReportDate.Value.Month == FMonth : true)
                      group l1 by l1.FPrjId
                          into g
                          join p in db.CF_Prj_BaseInfo on g.Key equals p.FId
                          where p.FType == iType
                          select new
                              {
                                  FLinkId = p.FId,
                                  FPrjId = p.FId,
                                  p.FPrjName,
                                  FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                                  FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                                  FMoney = ""
                              };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else
        {
            var App = from l1 in db.CF_App_List
                      where mtypes.Contains(l1.FManageTypeId.ToString())
                      && (l1.FBaseinfoId == FBaseinfoID
                      || l1.FToBaseinfoId == FBaseinfoID)
                      && l1.FState == 6
                      && l1.FReportDate.Value.Year == FYear
                     && (FMonth > 0 ? l1.FReportDate.Value.Month == FMonth : true)
                      group l1 by l1.FPrjId
                          into g
                          join p in db.CF_Prj_BaseInfo on g.Key equals p.FId
                          where p.FType == iType
                          select new
                          {
                              FLinkId = p.FId,
                              FPrjId = p.FId,
                              p.FPrjName,
                              FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                              FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault()
                          };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
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
