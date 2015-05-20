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
public partial class JSDW_project_ProjectItemSel : System.Web.UI.Page
{
    ProjectDB db = new ProjectDB();
    EgovaDB dbContext = new EgovaDB();
    RCenter rc = new RCenter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // showInfo();  根据测试要求，默认时不显示数据，需按查询条件显示数据
        }
    }
    //显示 
    void showInfo()
    {
        //IQueryable<TC_PrjItem_Info> App = dbContext.TC_PrjItem_Info.OrderByDescending(t => t.FId);          //去掉本单位的条件.Where(t => t.FJSDWID == CurrentEntUser.EntId)
        //if (!string.IsNullOrEmpty(t_FName.Text.Trim()))
        //    App = App.Where(t => t.PrjItemName.Contains(t_FName.Text.Trim()));
        //Pager1.RecordCount = App.Count();
        //dg_List.DataSource = App.Skip((Pager1.CurrentPageIndex - 1) * Pager1.PageSize).Take(Pager1.PageSize);
        //dg_List.DataBind();

        string sql = @"select  a.DWGCMC PrjItemName,a.XMBH,b.JSDW,b.XMMC,b.XMSD,b.XMLX,a.DWGCBH as FId,b.XMLX PrjItemType from   xm_baseinfo.dbo.GC_DWGCXX a, xm_baseinfo.dbo.XM_XMJBXX b
                     where a.XMBH = b.XMBH";                    
        if (!string.IsNullOrEmpty(this.t_FName.Text.Trim()))
        {
            sql += "   and a.DWGCMC like '%" + this.t_FName.Text.Trim() + "%'";
        }
        this.pager1.className = "dbCenter";
        this.pager1.sql = sql.ToString();
        this.pager1.pagecount = 20;
        this.pager1.controltopage = "dg_List";
        this.pager1.controltype = "DataGrid";
        this.pager1.dataBind();      

    }
    protected void App_List_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            //e.Item.Cells[1].Text = (e.Item.ItemIndex + 1 + this.Pager1.PageSize * (this.Pager1.CurrentPageIndex - 1)).ToString();
            String PrjItemType = e.Item.Cells[3].Text;

            if (PrjItemType.ToString().Trim() != "" && PrjItemType.ToString().Trim() != "&nbsp;")  //存在项目类型的才转换       modify by psq 20150319
            {
                e.Item.Cells[3].Text = dbContext.CF_Sys_Dic.Where(d => d.FNumber == Convert.ToInt32(PrjItemType)).Select(d => d.FName).FirstOrDefault();
            }
            else
            {
                e.Item.Cells[3].Text = "未知";
            }
            LinkButton lb = e.Item.Cells[e.Item.Cells.Count - 2].Controls[0] as LinkButton;
            lb.Text = "选择";
            lb.Attributes.Add("onclick", "return confirm('确认要选择该项目吗?');");
        }
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
