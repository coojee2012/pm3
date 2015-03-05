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
using EgovaBLL;

public partial class JSDW_ApplyZBBA_TBSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fLinkId"]))
            {
                showInfo();
            }
        }
    }
    //显示 
    void showInfo()
    {
        var App = from t in dbContext.TC_PBBG_TBQY
                  where t.FLinkId == Request.QueryString["fLinkId"]
                  orderby t.FId
                  select new
                  {
                      t.FId,
                      t.QYMC,
                      t.LXDH,
                      t.TBJ,
                      t.PBJ,
                      t.TBTime
                  };
        if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
            App = App.Where(t => t.QYMC.Contains(t_FName.Text.Trim()));
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
            lb.Attributes.Add("onclick", "return confirm('确认要选择该投标人吗?');");


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
                string QYMC = e.Item.Cells[e.Item.Cells.Count - 7].Text;
                TC_PBBG_ZBHXR pb = dbContext.TC_PBBG_ZBHXR.Where(t => t.HXRMC == QYMC).FirstOrDefault();
                TC_PBBG_FBYY fb = dbContext.TC_PBBG_FBYY.Where(t => t.TBR == QYMC).FirstOrDefault();
                if (pb != null)
                {
                    MyPageTool.showMessage("该企业已是中标候选人，不能选择!", this.Page);
                }
                else if (fb != null)
                {
                    MyPageTool.showMessage("该标已是废标，不能选择!", this.Page);
                }
                else
                {
                    pageTool tool = new pageTool(this.Page);
                    tool.ExecuteScript("window.returnValue='" + fid + "';window.close();");
                }
            }
        }
    }
}