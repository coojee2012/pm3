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
    int fMType = 28002;//见证人员安排-人员安排 
    int fMType_3 = 28004;//见证人员意见 
    int fMType_0 = 28003;//见证报告备案
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
            case "0":
                fType = string.Empty;
                break;
            case "1":
                fType = "（见证合格）";
                break;
            case "2":
                fType = "（见证不合格）";
                break;
        }
        lTitle.Text = fType;
    }
    //显示 
    void showInfo()
    {
        string empId = CurrentEmpUser.EmpId;
        int iResult = EConvert.ToInt(Request.QueryString["FType"]);
        int FYear = EConvert.ToInt(Request.QueryString["FYear"]);
        int FMonth = EConvert.ToInt(Request.QueryString["FMonth"]);
        var App = from l1 in db.CF_App_List
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                  join l3 in db.CF_App_List on l2.FLinkId equals l3.FLinkId
                  join g in db.CF_Prj_BaseInfo on l2.FPrjId equals g.FId
                  where l1.FState == 6 && l2.FState == 6 && l3.FState == 6
                  && l1.FManageTypeId == fMType
                  && l2.FManageTypeId == fMType_0
                   && l3.FManageTypeId == fMType_3
                  && (db.CF_Prj_Emp.Where(m => m.FEmpBaseInfo == empId)
                  .Select(m => m.FAppId).Contains(l1.FId))
                  && l1.FBaseinfoId == l2.FBaseinfoId
                  && (iResult > 0 ? db.CF_App_Idea.Count(i => i.FResultInt == iResult && i.FUserId == empId && i.FLinkId == l3.FId) > 0 : true)
                  && l2.FTime.Value.Year == FYear
                  && (FMonth > 0 ? l2.FTime.Value.Month == FMonth : true)
                  && l2.FBaseinfoId == l3.FBaseinfoId
                  orderby l2.FTime descending
                  select new
                  {
                      l3.FId,
                      l2.FLinkId,
                      FPrjId = g.FId,
                      g.FPrjName,
                      FAddress = db.CF_Sys_ManageDept.Where(t => t.FNumber.ToString() == g.FAddressDept).Select(t => t.FName).FirstOrDefault(),
                      FJSEnt = db.CF_Ent_BaseInfo.Where(t => t.FId == g.FBaseinfoId).Select(t => t.FName).FirstOrDefault(),
                      prjEmpId = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId && t.FAppId == l1.FId && t.FType == 2).Select(t => t.FId).FirstOrDefault(),
                      FFunction = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId && t.FAppId == l1.FId && t.FType == 2).Select(t => t.FFunction).FirstOrDefault()
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
                sUrl = "AddReport.aspx?FAppId=" + fid + "&fid=" + prjEmpId;
            }
            e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='javascript:showAddWindow(\"../ApplyXMJZ/" + sUrl + "\",700,650);'>查看意见</a>";
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
