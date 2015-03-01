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
public partial class KC_appmain_AnPaiInfoPrj : System.Web.UI.Page
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
        string empId = Request.QueryString["empId"];
        if (!string.IsNullOrEmpty(empId))
        {
            var p = db.CF_Emp_BaseInfo.Where(t => t.FId == empId)
                .Select(t => new { t.FName, t.FIdCard }).FirstOrDefault();
            if (p != null)
            {
                t_FName.Text = "<a href=\"javascript:showAddWindow('../main/AddEmpInfo.aspx?FId=" + empId + "&IsView=1',600,450);\" style='color:red'>" + p.FName + "</a>";
                t_FIdCard.Text = p.FIdCard;
            }
        }
    }
    //显示 
    void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        string empId = Request.QueryString["empId"];
        var App = from l1 in db.CF_App_List
                  join l3 in db.CF_App_List on l1.FLinkId equals l3.FLinkId
                  join g in db.CF_Prj_BaseInfo on l1.FPrjId equals g.FId
                  where l1.FState == 6 && l1.FManageTypeId == fMType
                    && (db.CF_App_List.Count(l2 => l2.FState == 6
                    && l2.FManageTypeId == fMType_0
                    && l2.FLinkId == l1.FLinkId
                    && l2.FBaseinfoId == FBaseinfoID) <= 0)//见证报告未办结 
                    && l3.FManageTypeId == fMType_3
                  && (db.CF_Prj_Emp.Where(m => m.FEmpBaseInfo == empId)
                  .Select(m => m.FAppId).Contains(l1.FId))
                  && l1.FBaseinfoId == FBaseinfoID
                  orderby l1.FAppDate descending
                  select new
                  {
                      l3.FId,//见证人员意见
                      l1.FLinkId,
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
            e.Item.Cells[e.Item.Cells.Count - 2].Text = "<a href='javascript:showAddWindow(\"../ApplyXMJZ/" + sUrl + "\",700,650);'>查看/填写意见</a>";
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
