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
                fType = "初步设计合同备案_受理";
                break;
            case "F4":
                fType = "初步设计成果移交";
                break;
            case "F5":
                fType = "施工图设计文件编制合同备案_受理";
                break;
            case "F6":
                fType = "施工图设计文件编制成果移交";
                break;

            case "J1":
                fType = "初步设计合同备案_不予受理";
                break;
            case "J5":
                fType = "施工图设计文件编制合同备案_不予受理";
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
        string fResult = string.Empty;
        switch (fType)
        {
            case "F1":
            case "F5":
                fResult = "<font color='green'>已受理</font>";
                break;
            case "F4":
            case "F6":
                fResult = "<font color='green'>已移交</font>";
                break;
            case "J1":
            case "J5":
                fResult = "<tt>不予受理</tt>";
                break;
        }
        int iState = fType.Substring(0, 1) == "F" ? 6 : 3;
        if (fType == "J1" || fType == "J5")
            iState = 7;//不予受理 
        var App = from l1 in db.CF_App_List
                  join p in db.CF_Prj_BaseInfo on l1.FPrjId equals p.FId
                  where l1.FManageTypeId == fMType
                  && (l1.FToBaseinfoId == FBaseinfoID
                  || l1.FBaseinfoId == FBaseinfoID)
                  && l1.FAppDate.Value.Year == FYear
                  && (FMonth > 0 ? l1.FAppDate.Value.Month == FMonth : true)
                  && l1.FState == iState
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
                      fState = fResult
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
