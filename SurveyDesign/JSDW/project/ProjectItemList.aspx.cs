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
using System.Data;

public partial class JSDW_project_ProjectItemList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();

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

        //工程类别
        DataTable dt = rc.getDicTbByFNumber("20001");
        t_PrjItemType.DataSource = dt;
        t_PrjItemType.DataTextField = "FName";
        t_PrjItemType.DataValueField = "FNumber";
        t_PrjItemType.DataBind();
        t_PrjItemType.Items.Insert(0, new ListItem("--请选择--", ""));

        //结构类型
        dt = rc.getDicTbByFNumber("509");
        t_ConstrType.DataSource = dt;
        t_ConstrType.DataTextField = "FName";
        t_ConstrType.DataValueField = "FNumber";
        t_ConstrType.DataBind();
        t_ConstrType.Items.Insert(0, new ListItem("--请选择--", ""));
    }
    //显示 
    void showInfo()
    {
        var v = from t in dbContext.TC_PrjItem_Info
                join p in dbContext.TC_Prj_Info
                on t.FPrjId equals p.FId
                orderby t.PrjItemType
                select new
                {
                    t.PrjItemName,
                    t.PrjItemType,
                    PrjItemTypeStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.PrjItemType)).Select(d => d.FName).FirstOrDefault(),
                    t.ProjectName,
                    t.ConstrType,
                    ConstrTypeStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.ConstrType)).Select(d => d.FName).FirstOrDefault(),
                    t.Cost,
                    t.FId,
                    t.FPrjId,
                    t.FJSDWID
                };
        if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
        {
            v = v.Where(t => t.ProjectName.Contains(this.txtProjectName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.txtProjectItemName.Text.Trim()))
        {
            v = v.Where(t => t.PrjItemName.Contains(this.txtProjectItemName.Text.Trim()));
        }
        if (!string.IsNullOrEmpty(this.t_PrjItemType.SelectedValue))
        {
            v = v.Where(t => t.PrjItemType.Contains(this.t_PrjItemType.SelectedValue));
        }
        if (!string.IsNullOrEmpty(this.t_ConstrType.SelectedValue))
        {
            v = v.Where(t => t.ConstrType.Contains(this.t_ConstrType.SelectedValue));
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
        }
    }
    //列表操作
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {

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
        tool.DelInfoFromGrid(dg_List, dbContext.TC_PrjItem_Info, tool_Deleting);
        showInfo();
    }
    //工程项目删除
    private void tool_Deleting(System.Collections.Generic.IList<string> FIdList, System.Data.Linq.DataContext context)
    {
        if (dbContext != null)
        {
            //单体工程
            var para = dbContext.TC_PrjItem_Info.Where(t => FIdList.ToArray().Contains(t.FId));
            dbContext.TC_PrjItem_Info.DeleteAllOnSubmit(para);
        }
    }
    protected void btnReload_Click(object sender, EventArgs e)
    {
        showInfo();
    }
}
