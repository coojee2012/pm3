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
    int fMType1 = 280;//勘察项目合同备案  
    int fMType4 = 284;//勘察成果移交

    int jMType1 = 28001;//项目见证受理
    int entType = 15501;//勘察单位
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
                fType = "合同备案受理项目";
                break;
            case "F4":
                fType = "成果移交项目";
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
        var App = from l1 in db.CF_App_List
                  join b in db.CF_Prj_Ent on l1.FLinkId equals b.FAppId
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId//其他单位（见证/勘察单位）的受理
                  join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                  where l1.FManageTypeId == fMType && l2.FManageTypeId == jMType1
                  && l1.FState == 6 && l2.FState == 6
                  && l1.FAppDate.Value.Year == FYear
                  && (FMonth > 0 ? l1.FAppDate.Value.Month == FMonth : true)
                  && (l1.FToBaseinfoId == FBaseinfoID
                  || l1.FBaseinfoId == FBaseinfoID)
                  && b.FEntType == entType && b.FBaseInfoId == FBaseinfoID
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
