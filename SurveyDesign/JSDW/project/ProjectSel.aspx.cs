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
public partial class JSDW_project_ProjectSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
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
        var App = from t in dbContext.TC_Prj_Info
                where t.FJSDWID == CurrentEntUser.EntId
                orderby t.FId
                select new
                {
                    t.FId,
                    t.ProjectName,
                    t.ProjectNo,
                    t.Address,
                    AddressDeptName = dbContext.CF_Sys_ManageDept.Where(d => d.FNumber.Equals(t.AddressDept)).Select(d => d.FFullName).FirstOrDefault(),
                    ProjectTypeStr = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(t.ProjectType)).Select(d => d.FName).FirstOrDefault(),
                    t.JSDW
                };
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.ProjectName.Contains(t_FName.Text.Trim()));
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");



            //判断项目是不是被终止了 
            var stop = db.CF_Prj_Stop.Where(t => t.FPrjId == EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FID"))).FirstOrDefault();
            if (stop != null)
            {
                e.Item.Attributes["style"] = "background:#EEEEEE;";
                e.Item.ToolTip = "该项目已被中止，所有业务停止进行。";
                lb.Attributes["onclick"] = "alert('该项目已被中止，所有业务停止进行。');return false; ";
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
    protected void dg_List_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            if (e.CommandName == "Sel")
            {
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
}
