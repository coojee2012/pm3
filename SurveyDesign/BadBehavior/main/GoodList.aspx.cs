using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectData;
using ProjectBLL;
using Tools;

public partial class BadBehavior_main_GoodList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ShowInfo();
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        ShowInfo();
    }
    private void ShowInfo()
    {
        var v = db.CF_Ent_GoodAction.Where(t => t.FBaseInfoId == CurrentEntUser.EntId).OrderByDescending(t => t.FCreateTime).AsEnumerable();

        Pager1.RecordCount = v.Count();
        DG_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        DG_List.DataBind();
        Pager1.Visible = (Pager1.RecordCount > Pager1.PageSize);
    }
    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
    }
    protected void DG_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[0].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            int FState = EConvert.ToInt(DataBinder.Eval(e.Item.DataItem, "FState"));
            if (FState == 0)
            {
                e.Item.Cells[DG_List.Columns.Count - 4].Text = "未提交";
            }
            else if (FState == 1)
            {
                e.Item.Cells[DG_List.Columns.Count - 4].Text = "已提交";
            }
            else if (FState == 6)
            {
                e.Item.Cells[DG_List.Columns.Count - 4].Text = "通过审核";
            }
            else if (FState == 9)
            {
                e.Item.Cells[DG_List.Columns.Count - 4].Text = "未通过";
            }
            if (FState != 0)
            {
                LinkButton lbReport = e.Item.FindControl("lbReport") as LinkButton;
                if (lbReport != null)
                {
                    lbReport.Visible = false;
                }
                LinkButton lbDel = e.Item.FindControl("lbDel") as LinkButton;
                if (lbDel != null)
                {
                    lbDel.Visible = false;
                }
            }
            e.Item.Cells[1].Text = "<a href=\"javascript:showAddWindow('GoodInfo.aspx?fid=" + fid + "',800,600);\">" + e.Item.Cells[1].Text + "</a>";

        }
    }
    protected void DG_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string FId = e.CommandArgument.ToString();
        pageTool tool = new pageTool(this.Page);
        if (e.CommandName == "Report") // 
        {
            CF_Ent_GoodAction report = db.CF_Ent_GoodAction.Where(t => t.FID == FId).FirstOrDefault();
            if (report != null)
            {
                report.FState = 1;
                db.SubmitChanges();
                tool.showMessage("操作成功");
            }
        }
        else if (e.CommandName == "Delete") // 
        {

            CF_Ent_GoodAction report = db.CF_Ent_GoodAction.Where(t => t.FID == FId).FirstOrDefault();
            if (report != null)
            {
                db.CF_Ent_GoodAction.DeleteOnSubmit(report);
                db.SubmitChanges();
                tool.showMessage("删除成功");
            }
        }

        ShowInfo();
    }
}
