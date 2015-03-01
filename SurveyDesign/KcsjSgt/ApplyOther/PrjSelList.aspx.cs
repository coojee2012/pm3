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
public partial class JSDW_appmain_PrjSelList : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
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
        int iMType = 30103;//施工图设计技术性审查通过的 2000101 房屋建筑工程
        IQueryable<CF_Prj_BaseInfo> App = db.CF_Prj_BaseInfo
            .Where(t => t.FType == 2000101 && t.FIsBG == 0)
            .Where(t => db.CF_App_List.Where(l => l.FState == 6
             && l.FManageTypeId == iMType
             && l.FBaseinfoId == CurrentEntUser.EntId)
             .Select(l => l.FPrjId).Contains(t.FId))
            .OrderByDescending(t => t.FCreateTime);
        string filt = Request.QueryString["type"];
        if (filt == "kzsf")
        {
            App = App.Where(t => !(db.CF_Prj_KZSFInfo.Select(k => k.FLinkId).Contains(t.FId)));//排除已经添加抗震设防的工程
        }
        else if (filt == "jzjn")
        {
            int pType = EConvert.ToInt(Request.QueryString["pType"]);
            App = App.Where(t => !(db.CF_Prj_JZJNInfo.Where(k => k.FPrjType == pType).Select(k => k.FLinkId).Contains(t.FId)));//排除已经添加建筑节能的工程
        }
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.FPrjName.Contains(t_FName.Text.Trim()));
        Pager1.RecordCount = App.Count();
        dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        dg_List.DataBind();
    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            string fType = EConvert.ToString(DataBinder.Eval(e.Item.DataItem, "FType"));
            if (!string.IsNullOrEmpty(fType))
                fType = rc.GetDicName(fType);
            e.Item.Cells[4].Text = fType;
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
                string fid = e.Item.Cells[e.Item.Cells.Count - 1].Text;
                pageTool tool = new pageTool(this.Page);
                tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
            }
        }
    }
}
