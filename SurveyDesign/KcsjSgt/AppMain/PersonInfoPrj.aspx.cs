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
public partial class SJ_appmain_PersonInfoPrj : System.Web.UI.Page
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
        fType = fType == "1" ? "勘察文件审查" : "施工图设计文件审查";
        lTitle.Text = fType;

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
        int baseType = 287;//勘察
        string fType = Request.QueryString["FType"];
        int iT = fMType;
        int iT_0 = fMType_0;
        if (fType == "2")
        {
            iT = fMType2;
            iT_0 = fMType_2;
            baseType = 300;//设计
        }
        string sTag = Request.QueryString["sTag"];
        int syear = EConvert.ToInt(Request.QueryString["syear"]);
        int smonth = EConvert.ToInt(Request.QueryString["smonth"]);
        string szdyB = Request.QueryString["sbegin"];
        if (string.IsNullOrEmpty(szdyB))
            szdyB = "1900-01-01";
        string szdyE = Request.QueryString["send"];
        if (string.IsNullOrEmpty(szdyE))
            szdyE = "9999-12-30";
        string FBaseinfoID = CurrentEntUser.EntId;
        string empId = Request.QueryString["empId"];
        var App = from l1 in db.CF_App_List
                  join l2 in db.CF_App_List on l1.FLinkId equals l2.FLinkId
                  join g in db.CF_Prj_BaseInfo on l2.FPrjId equals g.FId
                  where l1.FState == 6 && l2.FState > 1//技术性审查给出状态
                  && l1.FManageTypeId == iT
                  && l2.FManageTypeId == iT_0
                  && (db.CF_Prj_Emp.Where(m => m.FEmpBaseInfo == empId)
                  .Select(m => m.FAppId).Contains(l1.FId))
                  && l1.FBaseinfoId == l2.FBaseinfoId
                  && l1.FBaseinfoId == FBaseinfoID
                  && (syear > 0 ? l2.FAppDate.GetValueOrDefault().Year == syear : true)
                  && (smonth > 0 ? l2.FAppDate.GetValueOrDefault().Month == smonth : true)
                  && l2.FAppDate >= Convert.ToDateTime(szdyB)
                  && l2.FAppDate < Convert.ToDateTime(szdyE).AddDays(1)
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
                      //文件审查合同备案是否二审
                      FReportCount = db.CF_App_List.Where(li => li.FLinkId == l2.FLinkId && li.FManageTypeId == baseType).Select(li => li.FReportCount).FirstOrDefault(),
                      SUrl = fType == "1" ? "ApplyKCJSXSC" : "ApplySGTSJJSXSC",
                      FUrl = fType == "1" ? "ApplyKCWJSCWTSL" : "ApplySGTSJWJSCWTSL"
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
