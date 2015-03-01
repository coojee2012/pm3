using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectBLL;
using ProjectData;
using Approve.Common;
using Approve.RuleCenter;
using System.Text;

public partial class KcsjSgt_AppMain_Print_KCHGZ : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    public string ReportServer = "";
    public string FAppId = "";
    int fMType = 28803;//	技术性审查(勘察)--同意的数据 
    protected void Page_Load(object sender, EventArgs e)
    {
        FAppId = EConvert.ToString(Session["FAppId"]);
        ReportServer = rc.GetSysObjectContent("_ReportServer");
        if (string.IsNullOrEmpty(ReportServer))
        {
            ReportServer = "http://" + Request.Url.Host + ":8075/WebReport/ReportServer?reportlet=";
        }
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    ProjectDB db = new ProjectDB();
    //显示
    private void showInfo()
    {
        string FBaseinfoID = CurrentEntUser.EntId;
        var v = from t in db.CF_App_List
                where t.FManageTypeId == fMType && t.FState == 6
                 && t.FBaseinfoId == FBaseinfoID
                orderby t.FTime descending
                select new
                {
                    t.FId,
                    t.FPrjId,
                    FPrjName = db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FPrjName).FirstOrDefault(),
                    t.FName,
                    t.FCreateTime,
                    t.FState,
                    t.FManageTypeId,
                    t.FReportDate,
                    t.FUpDeptId,
                    t.FYear,
                    FJSEnt = db.CF_Ent_BaseInfo.Where(b => b.FId == (db.CF_Prj_BaseInfo.Where(p => p.FId == t.FPrjId).Select(p => p.FBaseinfoId).FirstOrDefault())).Select(b => b.FName).FirstOrDefault(),
                    FKCEnt = db.CF_Prj_Ent.Where(b => (db.CF_App_List.Where(l => l.FManageTypeId == 280 && l.FPrjId == t.FPrjId).Select(l => l.FLinkId).Contains(b.FAppId)) && b.FEntType == 15501).Select(b => b.FName).FirstOrDefault(),
                    t.FResult,
                    t.FAppDate,
                    t.FLinkId,
                    t.FIsSign,
                    FPrintDate = t.FToBaseinfoId,
                    //勘察文件审查合同备案(287)是否二审
                    FReportCount = db.CF_App_List.Where(li => li.FLinkId == t.FLinkId && li.FManageTypeId == 287).Select(li => li.FReportCount).FirstOrDefault(),
                    FBAJG = (from i in db.CF_App_Idea
                             join b in db.CF_App_List
                             on i.FLinkId equals b.FId
                             where b.FLinkId == t.FLinkId && b.FManageTypeId == 290
                             orderby i.FTime
                             select i.FResultInt).FirstOrDefault()
                };
        if (!string.IsNullOrEmpty(txtFPrjName.Text.Trim()))
            v = v.Where(t => t.FPrjName.Contains(txtFPrjName.Text.Trim()));
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);//不足一页不显示
    }

    //列表
    protected void DG_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbautoid = (Label)e.Row.FindControl("lbautoid");
            lbautoid.Text = (e.Row.RowIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string FID = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FID"));
            int isSign = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FIsSign"));
            CheckBox box = e.Row.FindControl("ck_State") as CheckBox;
            if (box != null)
            {
                box.Checked = isSign == 1;
                LinkButton btn = e.Row.FindControl("btnSave") as LinkButton;
                if (box.Checked)
                {
                    //限制流水号不能修改  
                    TextBox txt = e.Row.FindControl("t_LSH") as TextBox;
                    btn.Enabled = false;
                }
                else
                {
                    btn.Attributes["onclick"] = "return checkLSH(this);";
                }
            }
            string FBAJG = EConvert.ToString(DataBinder.Eval(e.Row.DataItem, "FBAJG"));
            if (FBAJG == "1")
                FBAJG = "<font color='green'>通过</font>";
            else if (FBAJG == "3")
                FBAJG = "<font color='red'>不通过</font>";
            else
                FBAJG = "正在办理";
            e.Row.Cells[6].Text = FBAJG;
            //是否二审。
            int FReportCount = EConvert.ToInt(DataBinder.Eval(e.Row.DataItem, "FReportCount"));
            if (FReportCount > 1)
            {
                ((Literal)e.Row.FindControl("lit_Count")).Text = ("(" + FReportCount + "审)");
            }
        }
    }
    //列表事件
    protected void DG_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DoSave")
        {
            pageTool tool = new pageTool(this);
            string FAppId = e.CommandArgument.ToString();
            //保存流水号 
            RCenter rc = new RCenter();
            StringBuilder sb = new StringBuilder();
            string printDate = h_State.Value == "1"
                ? DateTime.Now.ToString("yyyy-MM-dd") : "";
            sb.Append("update cf_App_list set FResult='" + h_LSH.Value + "',");
            sb.Append("FToBaseInfoId='" + printDate + "',");
            sb.Append("FIsSign='" + h_State.Value + "' where fid='" + FAppId + "'");
            rc.PExcute(sb.ToString());
            showInfo();
            tool.showMessage("保存成功！");
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }

    //刷新按钮 
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
