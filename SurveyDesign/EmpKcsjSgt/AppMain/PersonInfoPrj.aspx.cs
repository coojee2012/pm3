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
public partial class KC_appmain_PersonInfoPrj : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    RCenter rc = new RCenter();
    int fMType = 28802;//审查人员安排(勘察) 
    int fMType_0 = 28803;//技术性审查

    int fMType2 = 30102;//审查人员安排(设计)
    int fMType_2 = 30103;//技术性审查 
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
        string tag = "勘察文件审查";
        if (fType.Substring(0, 1) == "J")
            tag = "施工图设计文件审查";
        switch (fType.Substring(1))
        {
            case "0":
                fType = string.Empty;
                break;
            case "6":
                fType = "_审查合格";
                break;
            case "3":
                fType = "_审查不合格";
                break;
        }
        lTitle.Text = tag + fType;
    }
    //显示 
    void showInfo()
    {
        string empId = CurrentEmpUser.EmpId;
        string fType = Request.QueryString["FType"];
        string tag = fType.Substring(0, 1);
        int iResult = EConvert.ToInt(fType.Substring(1));
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        int iT = fMType;
        int iT_0 = fMType_0;
        if (tag == "J")
        {
            iT = fMType2;
            iT_0 = fMType_2;
        }
        var App = from l1 in db.CF_App_List
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                  join g in db.CF_Prj_BaseInfo on l2.FPrjId equals g.FId
                  where l1.FState == 6 && l2.FState > 1//技术性审查给过状态了
                  && l1.FManageTypeId == iT
                  && l2.FManageTypeId == iT_0
                  && (db.CF_Prj_Emp.Where(m => m.FEmpBaseInfo == empId)
                  .Select(m => m.FAppId).Contains(l1.FId))
                  && l1.FBaseinfoId == l2.FBaseinfoId
                  && (iResult > 0 ? (db.CF_App_Idea.Count(i => i.FResultInt == iResult && i.FUserId == empId && i.FLinkId == l2.FId) > 0 || l2.FState == iResult) : true)
                  && l2.FAppDate.Value.Year == FYear
                  && (FMonth > 0 ? l2.FAppDate.Value.Month == FMonth : true)
                  orderby l2.FAppDate descending
                  select new
                  {
                      l2.FId,
                      l2.FLinkId,
                      FPrjId = g.FId,
                      g.FPrjName,
                      FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == g.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                      FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == g.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                      prjEmpId = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId && t.FAppId == l1.FId && t.FType > 1).Select(t => t.FId).FirstOrDefault(),
                      FFunction = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId && t.FAppId == l1.FId && t.FType > 1).Select(t => t.FFunction).FirstOrDefault(),
                      SUrl = tag == "F" ? "ApplyKCJSXSC" : "ApplySGTSJJSXSC"
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
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fid"));
            string s = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "SUrl"));
            string prjEmpId = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "prjEmpId"));

            string sUrl = string.Empty;
            if (string.IsNullOrEmpty(prjEmpId.Trim()))
            {
                e.Item.Cells[4].Text = "项目负责人";
                //项目负责人查看整个意见
                sUrl = "Report.aspx?FAppId=" + fid;
            }
            else
            {
                //查看个人意见
                sUrl = "EmpReport.aspx?FAppId=" + fid + "&fid=" + prjEmpId;
            }
            e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='javascript:showAddWindow(\"../" + s + "/" + sUrl + "\",700,650);'>查看意见</a>";
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
