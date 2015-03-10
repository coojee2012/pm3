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
public partial class JSDW_APPSGXKZGL_EmpSel : System.Web.UI.Page
{
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // t_FAppId.Value = EConvert.ToString(Session["FAppId"]);
            if (Request["FAppId"] != null && !string.IsNullOrEmpty(Request["FAppId"]))
            {
                t_FAppId.Value = Request["FAppId"];
            }
         
            showInfo();
        }
    }
    //显示 
    void showInfo()
    {
        EgovaDB dbContext = new EgovaDB();
        IQueryable<TC_PrjItem_Emp> App = dbContext.TC_PrjItem_Emp.Where(t => t.FAppId == t_FAppId.Value).OrderByDescending(t => t.FEntId);
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FHumanName.Contains(t_FName.Text.Trim()));
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
                //string fid = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FId"));
                //string FHumanName = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FHumanName"));
                string fid = e.Item.Cells[6].Text;
                string FHumanName = e.Item.Cells[2].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "@" + FHumanName + "';window.close();");
            }
        }
    }
}
