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
    int[] types = { 291, 293 };
    int fMType1 = 291;//初步设计合同备案   

    int jMType1 = 296;//施工图设计文件编制合同备案   
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
                fType = "初步设计合同备案";
                break;
            case "F4":
                fType = "初步设计成果移交";
                break;

            case "J1":
                fType = "施工图设计文件编制合同备案";
                break;
            case "J4":
                fType = "施工图设计文件编制成果移交";
                break;
        }
        lTitle.Text = fType;
    }
    //显示 
    void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string fType = Request.QueryString["FType"];
        int fMType = EConvert.ToInt(Request.QueryString["FMType"]);
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        if (fType.Substring(1) == "1")
        {
            var App = from l1 in db.CF_App_List
                      join b in db.CF_Prj_Ent on l1.FId equals b.FAppId
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where l1.FManageTypeId == fMType
                      && l1.FToBaseinfoId == FBaseinfoID
                      && l1.FAppDate.Value.Year == FYear
                      && (FMonth > 0 ? l1.FAppDate.Value.Month == FMonth : true)
                      && l1.FState == 6 && b.FBaseInfoId == FBaseinfoID
                      group l1 by new
                      {
                          l1.FPrjId,
                          l1.FLinkId,
                          l1.FManageTypeId,
                          l1.FAppDate.Value.Month,
                          b.FMoney,
                          p.FAddressDept,
                          l1.FAppDate,
                          p.FPrjName,
                          p.FBaseinfoId
                      } into g

                      select new
                      {
                          g.Key.FLinkId,
                          FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == g.Key.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                          g.Key.FAppDate,
                          FUrl = types.Contains(g.Key.FManageTypeId.Value) ? "F" : "",
                          g.Key.FPrjName,
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == g.Key.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          g.Key.FMoney
                      };
            if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
                App = App.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
            Pager1.RecordCount = App.Count();
            dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
            dg_List.DataBind();
        }
        else
        {
            int iType = fMType1;
            if (fType.Substring(0, 1) == "J")
                iType = jMType1;
            var App = from l1 in db.CF_App_List
                      join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                      join b in db.CF_Prj_Ent on l1.FId equals b.FAppId
                      join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                      where l1.FManageTypeId == iType
                      && l1.FToBaseinfoId == FBaseinfoID && l1.FState == 6
                      && l2.FBaseinfoId == FBaseinfoID && l2.FState == 6
                      && l2.FManageTypeId == fMType
                      && l2.FAppDate.Value.Year == FYear
                      && (FMonth > 0 ? l2.FAppDate.Value.Month == FMonth : true)
                      && b.FBaseInfoId == FBaseinfoID
                      orderby l1.FAppDate descending
                      select new
                      {
                          l1.FLinkId,
                          FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == p.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                          l1.FAppDate,
                          FUrl = types.Contains(l1.FManageTypeId.Value) ? "F" : "",
                          p.FPrjName,
                          FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == p.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                          b.FMoney
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
