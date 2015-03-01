using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using Tools;
using ProjectBLL;
using System.Data;
public partial class BadBehavior_main_AddBad : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                ViewState["FID"] = Request.QueryString["fid"];
                showInfo();
            }
        }
    }

    //显示
    private void showInfo()
    {
        pageTool tool = new pageTool(this.Page);
        CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == Convert.ToString(ViewState["FID"])).FirstOrDefault();
        if (report != null)
        {
            tool.fillPageControl(report);
            if (report.FState == 0)
            {
                liState.Text = "未提交";
            }
            else if (report.FState == 1)
            {
                liState.Text = "已提交";
                btnReport.Visible = false;
                btnSave.Visible = false;
            }
            else if (report.FState == 6)
            {
                liState.Text = "已处理";
                btnReport.Visible = false;
                btnSave.Visible = false;
            }
        }
        else
        {
            t_FReportTime.Text = EConvert.ToShortDateString(DateTime.Now);
        }
    }

    //保存
    private void saveInfo(int state)
    {
        pageTool tool = new pageTool(this.Page);
        DateTime dTime = DateTime.Now;
        string fId = EConvert.ToString(ViewState["FID"]);
        CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == fId).FirstOrDefault();
        if (report == null)
        {
            report = new CF_Prj_BadReport();
            db.CF_Prj_BadReport.InsertOnSubmit(report);
            report.FId = Guid.NewGuid().ToString();
            report.FIsDeleted = false;
            report.FCreateTime = dTime;
            report.FBaseInfoId = CurrentEntUser.EntId;
        }
        report.FState = state;
        report = tool.getPageValue(report);
        report.FTime = dTime;


        db.SubmitChanges();
        tool.showMessageAndRunFunction((state == 1 ? "提交成功" : "保存成功"), "window.returnValue='1';");
        ViewState["FID"] = report.FId;
    }

    //保存按钮
    protected void btnSave_Click(object sender, CommandEventArgs e)
    {
        saveInfo(EConvert.ToInt(e.CommandArgument));
        showInfo();
    }
}
