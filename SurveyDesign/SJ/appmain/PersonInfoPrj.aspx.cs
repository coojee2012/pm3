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
    int fMType = 29201;//初步设计人员安排 
    int fMType_0 = 293;//初步设计成果提交

    int fMType2 = 29701;//施工图设计文件编制人员安排
    int fMType_2 = 298;//施工图设计文件编制成果移交 
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
        fType = fType == "1" ? "初步设计" : "设计文件编制";
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
        string fType = Request.QueryString["FType"];
        int iT = fMType;
        int iT_0 = fMType_0;
        if (fType == "2")
        {
            iT = fMType2;
            iT_0 = fMType_2;
        }
        string FBaseinfoID = CurrentEntUser.EntId;
        string empId = Request.QueryString["empId"];
       
        IQueryable<CF_Prj_Emp> emps = db.CF_Prj_Emp.Where(t => t.FEmpBaseInfo == empId);
        IQueryable<CF_App_List> app = db.CF_App_List.Where(t => emps.Select(e => e.FAppId).Contains(t.FId)&&t.FManageTypeId==iT);
        Pager1.RecordCount = app.Count();
        dg_List.DataSource = app.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "fid"));
            string prjid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FPrjID"));
            string FLinkid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FLinkid"));

            CF_Prj_BaseInfo prj = db.CF_Prj_BaseInfo.Where(t => t.FId == prjid).FirstOrDefault();
            string fType = Request.QueryString["FType"];
            e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('../applycbsjwt/ApplyBaseInfo.aspx?FDataID=" + FLinkid + "',800,700);\">" + prj.FPrjName + "</a>";
            if (fType == "2")
            {
                e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('../applysjwjbzwt/ApplyBaseInfo.aspx?FDataID=" + FLinkid + "',800,700);\">" + prj.FPrjName + "</a>";
            }
           
            e.Item.Cells[2].Text = prj.FAllAddress;
            e.Item.Cells[3].Text = prj.JSDW;

            CF_Prj_Emp emp = db.CF_Prj_Emp.Where(t => t.FAppId == fid && t.FEmpBaseInfo == Request.QueryString["empId"]).FirstOrDefault();
            if (emp != null)
            {
                e.Item.Cells[4].Text = emp.FFunction;
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
