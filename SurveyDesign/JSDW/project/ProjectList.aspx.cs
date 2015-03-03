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
using EgovaDAO;

public partial class JSDW_project_ProjectList : System.Web.UI.Page
{
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        var v = from t in dbContext.TC_Prj_Info
                orderby t.ProjectTime
                select new
                {
                    t.FId,
                    t.ProjectNo,
                    t.ProjectName,
                    Province = "四川",
                    t.Address,
                    t.ProjectTime,
                    t.JSDW,
                    t.City,
                    t.FJSDWID
                };
        if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
        {
            v = v.Where(t => t.ProjectName.Contains(this.txtProjectName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtProjectNo.Text.Trim()))
        {
            v = v.Where(t => t.ProjectNo.Contains(this.txtProjectNo.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(CurrentEntUser.EntId))
        {
            v = v.Where(t => t.FJSDWID.Contains(CurrentEntUser.EntId.Trim()));
        }
        Pager1.RecordCount = v.Count();
        this.dg_List.DataSource = v.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
       
    }

    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"));
            e.Item.Cells[3].Text = "<a href='javascript:void(0)' onclick=\"showAddWindow('ProjectInfo.aspx?fid=" + fid + "',1000,700);\">" + e.Item.Cells[3].Text + "</a>";
        }
    }

    //分页面控件翻页事件
    protected void Pager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Pager1.CurrentPageIndex = e.NewPageIndex;
        showInfo();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        pageTool tool = new pageTool(this.Page);
        tool.DelInfoFromGrid(dg_List, dbContext.TC_Prj_Info, tool_Deleting);
        showInfo();
    }
    //工程项目删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        if (dbContext != null)
        {
            //工程项目
            var pro = dbContext.TC_Prj_Info.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_Prj_Info.DeleteAllOnSubmit(pro);
            //单体工程
            var para = dbContext.TC_PrjItem_Info.Where(t => FIdList.ToArray().Contains(t.FPrjId));
            dbContext.TC_PrjItem_Info.DeleteAllOnSubmit(para);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
