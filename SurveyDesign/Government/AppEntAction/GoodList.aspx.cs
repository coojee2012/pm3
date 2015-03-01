using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Tools;
using Approve.RuleCenter;

public partial class Government_AppEntAction_GoodList : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ShowInfo();
            ShowPostion();
        }
    }

    private void ShowPostion()
    {
        this.lPostion.Text = rc.GetMenuName(Request["fcol"]);
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        var v = db.CF_Ent_GoodAction.Where(t => t.FState == 1 || t.FState == 6);
        if (txtFEntName.Text.Trim() != "")
        {
            v = v.Where(t => t.FProjectName.Contains(txtFEntName.Text));
        }
        if (txtFReportDate.Text.Trim() != "")
        {
            v = v.Where(t => t.FHTime >= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        if (txtFReportDate1.Text.Trim() != "")
        {
            v = v.Where(t => t.FHTime <= EConvert.ToDateTime(txtFReportDate.Text.Trim()));

        }
        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        ShowInfo();
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + this.Pager1.PageCount * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            int FState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            if (FState == 1)
            {
                e.Item.Cells[5].Text = "未处理";
                e.Item.Cells[6].Text = "处理";
            }
            else if(FState==6)
            {
                e.Item.Cells[5].Text = "通过审核";
                e.Item.Cells[6].Text = "查看";
            }
            else if (FState == 9)
            {
                e.Item.Cells[5].Text = "未通过审核";
                e.Item.Cells[6].Text = "查看";
            }
            e.Item.Cells[1].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GoodInfo.aspx?fid=" + fid + "',800,600);\">" + e.Item.Cells[1].Text + "</a>";
            e.Item.Cells[6].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('GoodInfo.aspx?fid=" + fid + "',800,600);\">" + e.Item.Cells[6].Text + "</a>";

        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = e.CommandArgument.ToString();
        pageTool tool = new pageTool(this.Page);
        if (e.CommandName == "Report") // 
        {
            CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == FId).FirstOrDefault();
            if (report != null)
            {
                report.FState = 1;
                db.SubmitChanges();
                tool.showMessage("操作成功");
            }
        }
        else if (e.CommandName == "Delete") // 
        {

            CF_Prj_BadReport report = db.CF_Prj_BadReport.Where(t => t.FId == FId).FirstOrDefault();
            if (report != null)
            {
                db.CF_Prj_BadReport.DeleteOnSubmit(report);
                db.SubmitChanges();
                tool.showMessage("删除成功");
            }
        }

        ShowInfo();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
}
